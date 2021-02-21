using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace vIDsafe
{
    public abstract class Storage
    {
        ///<value>Get or set the vault</value>
        public Vault Vault { get; protected set; } = new Vault();

        private readonly string _vaultFolder = "Vaults/";

        ///<value>Get or set the vault name</value>
        public string Name { get; protected set; } = "";

        ///<value>Get or set the vault password</value>
        protected string Password { get; set; } = "";

        /// <summary>
        /// Decrypts and deserialises the vault
        /// </summary>
        /// <returns>
        /// The decrypted vault if it's deserialised, null if not
        /// </returns>
        protected Vault DecryptVault(string encryptedVault, string key)
        {
            string decryptedVault = Encryption.AesDecrypt(encryptedVault, Convert.FromBase64String(key));

            if (decryptedVault != null)
            {
                return StringToVault(decryptedVault);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Serialises and encrypts the vault
        /// </summary>
        /// <returns>
        /// The encrypted vault if it's encrypted, cleartext vault if not
        /// </returns>
        protected string EncryptVault(Vault vault, string key)
        {
            string serialisedVault = VaultToString(vault);

            return Encryption.AesEncrypt(serialisedVault, Convert.FromBase64String(key));
        }

        /// <summary>
        /// Decrypts and sets the vault to the account
        /// </summary>
        /// <returns>
        /// The decrypted vault if it exists, null if not 
        /// </returns>
        protected Vault RetrieveVault(string name, string password)
        {
            string fileName = _vaultFolder + name;
            string encryptedVault = File.ReadAllText(fileName);

            return (DecryptVault(encryptedVault, password));
        }

        /// <summary>
        /// Encrypts and saves the vault
        /// </summary>
        protected void SaveVault(Vault vault, string name, string password)
        {
            string encryptedVault = EncryptVault(vault, password);
            string fileName = _vaultFolder + name;

            CreateFile(fileName, encryptedVault);
        }

        /// <summary>
        /// Creates a file
        /// </summary>
        protected void CreateFile(string fileName, string writeContent)
        {
            FileInfo file = new FileInfo(fileName);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, writeContent);
        }

        /// <summary>
        /// Deletes the vault
        /// </summary>
        protected void DeleteVault(string name)
        {
            if (VaultExists(name))
            {
                File.Delete(_vaultFolder + name);
            }
        }

        /// <summary>
        /// Checks if the account exists in the directory
        /// </summary>
        /// <returns>
        /// True if the account exists, false if not
        /// </returns>
        protected bool VaultExists(string name)
        {
            if (File.Exists(_vaultFolder + name))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Deserialises a string to a vault
        /// </summary>
        /// <returns>
        /// The vault if deserialised
        /// </returns>
        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        private Vault StringToVault(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return (Vault)new BinaryFormatter().Deserialize(ms);
            }
        }

        /// <summary>
        /// Serialises the vault to a string
        /// </summary>
        /// <returns>
        /// The vault as a string if serialised
        /// </returns>
        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        private string VaultToString(Vault vault)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, vault);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}