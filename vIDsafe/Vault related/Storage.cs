using System;
using System.IO;

namespace vIDsafe
{
    public abstract class Storage : Porting
    {
        private readonly string _vaultFolder = "Vaults/";

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
    }
}