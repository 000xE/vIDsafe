using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CsvHelper;
using System.Globalization;
using Newtonsoft.Json;

namespace vIDsafe
{
    public class MasterAccount
    {
        private string _password = "";

        ///<value>Get or set the master account name</value>
        public string Name { get; private set; } = "";

        ///<value>Get or set the vault</value>
        public Vault Vault { get; private set; } = new Vault();

        private readonly string _vaultFolder = "Vaults/";

        private readonly object _lock = new object();

        public enum VaultFormat
        {
            CSV,
            JSON,
            Encrypted
        }

        /// <summary>
        /// Checks if the account exists in the directory
        /// </summary>
        /// <returns>
        /// True if the account exists, false if not
        /// </returns>
        private bool AccountExists(string name)
        {
            if (File.Exists(_vaultFolder + name))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to login to the account
        /// </summary>
        /// <returns>
        /// True if the logged in, false if not
        /// </returns>
        public bool TryLogin(string name, string password)
        {
            lock (_lock)
            {
                bool loggedin = false;

                if (AccountExists(name))
                {
                    _password = HashPassword(password, name);

                    Vault = GetVault(name, _password);

                    if (Vault != null)
                    {
                        Name = name;

                        loggedin = true;
                    }
                }

                return loggedin;
            }
        }

        /// <summary>
        /// Tries to register an account
        /// </summary>
        /// <returns>
        /// True if the registered, false if not
        /// </returns>
        public bool TryRegister(string name, string password)
        {
            lock (_lock)
            {
                bool registered = false;

                if (!AccountExists(name))
                {
                    Name = name;
                    _password = HashPassword(password, name);

                    CreateVault(Vault, Name, _password);

                    registered = true;
                }

                return registered;
            }
        }

        /// <summary>
        /// Checks if the entered password is correct
        /// </summary>
        /// <returns>
        /// True if the correct, false if not
        /// </returns>
        private bool VerifyPassword(string oldPassword)
        {
            bool correct = false;

            if (HashPassword(oldPassword, Name).Equals(_password))
            {
                correct = true;
            }

            return correct;
        }

        /// <summary>
        /// Tries to change the password of the account
        /// </summary>
        /// <returns>
        /// True if the password is changed, false if not
        /// </returns>
        public bool TryChangePassword(string oldPassword, string password)
        {
            lock (_lock)
            {
                bool changed = false;

                if (VerifyPassword(oldPassword).Equals(true))
                {
                    _password = HashPassword(password, Name);

                    SaveVault(Vault, Name, _password);

                    changed = true;
                }

                return changed;
            }
        }

        /// <summary>
        /// Tries to change the name of the account
        /// </summary>
        /// <returns>
        /// True if the name is changed, false if not
        /// </returns>
        public bool TryChangeName(string oldPassword, string newName)
        {
            lock (_lock)
            {
                bool changed = false;

                if (VerifyPassword(oldPassword).Equals(true))
                {
                    if (AccountExists(Name))
                    {
                        File.Move(_vaultFolder + Name, _vaultFolder + newName);
                    }

                    Name = newName;
                    _password = HashPassword(oldPassword, newName);

                    SaveVault(Vault, Name, _password);

                    changed = true;
                }

                return changed;
            }
        }

        /// <summary>
        /// Decrypts and sets the vault to the account
        /// </summary>
        /// <returns>
        /// The decrypted vault if it exists, null if not 
        /// </returns>
        private Vault GetVault(string name, string password)
        {
            string fileName = _vaultFolder + name;
            string encryptedVault = File.ReadAllText(fileName);

            return (DecryptVault(encryptedVault, password));           
        }

        /// <summary>
        /// Creates the vault and encrypts it
        /// </summary>
        private void CreateVault(Vault vault, string name, string password)
        {
            string encryptedVault = EncryptVault(vault, password);
            string fileName = _vaultFolder + name;

            FileInfo file = new FileInfo(fileName);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, encryptedVault);
        }

        /// <summary>
        /// Encrypts and saves the vault
        /// </summary>
        private void SaveVault(Vault vault, string name, string password)
        {
            string encryptedVault = EncryptVault(vault, password);
            string fileName = _vaultFolder + name;

            if (AccountExists(name))
            {
                File.WriteAllText(fileName, encryptedVault);
            }
            else
            {
                CreateVault(vault, name, password);
            }
        }

        /// <summary>
        /// Hashes the password
        /// </summary>
        /// <returns>
        /// The hashed password if it's hashed, null if not
        /// </returns>
        private string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(Encryption.DeriveKey(Encryption.KeyDerivationFunction.PBKDF2, password, salt));
        }

        /// <summary>
        /// Serialises and encrypts the vault
        /// </summary>
        /// <returns>
        /// The encrypted vault if it's encrypted, cleartext vault if not
        /// </returns>
        private string EncryptVault(Vault vault, string key)
        {
            string serialisedVault = VaultToString(vault);

            return Encryption.AesEncrypt(serialisedVault, Convert.FromBase64String(key));
        }

        /// <summary>
        /// Decrypts and deserialises the vault
        /// </summary>
        /// <returns>
        /// The decrypted vault if it's deserialised, null if not
        /// </returns>
        private Vault DecryptVault(string encryptedVault, string key)
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
        /// Tries to import a vault
        /// </summary>
        /// <returns>
        /// True if vault is imported, false if not
        /// </returns>
        public bool TryImportVault(VaultFormat format, string fileName, bool replace)
        {
            lock (_lock)
            {
                Vault importedVault = new Vault();

                bool canImport;

                try
                {
                    string readContent = File.ReadAllText(fileName);

                    switch (format)
                    {
                        case VaultFormat.CSV:
                            using (StringReader stringReader = new StringReader(readContent))
                            using (CsvReader csvReader = new CsvReader(stringReader, CultureInfo.InvariantCulture))
                            {
                                csvReader.Read();
                                csvReader.ReadHeader();

                                while (csvReader.Read())
                                {
                                    Identity identity = csvReader.GetRecord<Identity>();

                                    identity = importedVault.FindOrCreateIdentity(identity.Name, identity.Email, identity.Usage);

                                    Credential credential = csvReader.GetRecord<Credential>();

                                    identity.FindOrCreateCredential(credential.CredentialID, credential.Username, credential.Password, credential.URL, credential.Notes);
                                }
                            }
                            break;
                        case VaultFormat.JSON:
                            importedVault = JsonConvert.DeserializeObject<Vault>(readContent);
                            break;
                        case VaultFormat.Encrypted:
                            importedVault = DecryptVault(readContent, _password);
                            break;
                    }

                    AddImportedData(importedVault, replace);

                    canImport = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    canImport = false;
                }

                return canImport;
            }
        }

        /// <summary>
        /// Adds the data inside the imported vault
        /// </summary>
        private void AddImportedData(Vault importedVault, bool replace)
        {
            if (replace)
            {
                Vault = importedVault;
            }
            else
            {
                foreach (KeyValuePair<string, Identity> identityPair in importedVault.Identities)
                {
                    string identityEmail = identityPair.Key;
                    Identity importedIdentity = identityPair.Value;

                    Identity identity = Vault.FindOrCreateIdentity(importedIdentity.Name, identityEmail, importedIdentity.Usage);

                    foreach (KeyValuePair<string, Credential> credentialPair in importedIdentity.Credentials)
                    {
                        string credentialID = credentialPair.Key;
                        Credential importedCredential = credentialPair.Value;

                        identity.FindOrCreateCredential(credentialID, importedCredential.Username, importedCredential.Password, importedCredential.URL, importedCredential.Notes);
                    }
                }
            }
        }

        //Todo: Refactor the selected email thing
        /// <summary>
        /// Tries to export the vault
        /// </summary>
        /// <returns>
        /// True if the vault is export, false if not
        /// </returns>
        public bool TryExportVault(VaultFormat format, string selectedEmail, string fileName)
        {
            lock (_lock)
            {
                Vault vault = Vault;

                bool canExport;

                string writeContent = "";

                if (selectedEmail.Length > 0)
                {
                    vault.DeleteAllIdentities();
                    vault.Identities.Add(selectedEmail, Vault.Identities[selectedEmail]);
                }

                try
                {
                    switch (format)
                    {
                        case VaultFormat.CSV:
                            using (StringWriter stringWriter = new StringWriter())
                            using (CsvWriter csvWriter = new CsvWriter(stringWriter, CultureInfo.InvariantCulture))
                            {
                                List<Identity> identities = vault.Identities.Values.ToList();

                                csvWriter.WriteHeader<Identity>();
                                csvWriter.WriteHeader<Credential>();
                                csvWriter.NextRecord();

                                foreach (Identity identity in identities)
                                {
                                    foreach (Credential credential in identity.Credentials.Values.ToList())
                                    {
                                        csvWriter.WriteRecord(identity);
                                        csvWriter.WriteRecord(credential);
                                        csvWriter.NextRecord();
                                    }
                                }
                                writeContent = stringWriter.ToString();
                            }
                            break;
                        case VaultFormat.JSON:
                            string json = JsonConvert.SerializeObject(vault, Formatting.Indented);
                            writeContent = json;
                            break;
                        case VaultFormat.Encrypted:
                            string encryptedVault = EncryptVault(vault, _password);
                            writeContent = encryptedVault;
                            break;
                    }

                    FileInfo file = new FileInfo(fileName);
                    file.Directory.Create(); // If the directory already exists, this method does nothing.
                    File.WriteAllText(file.FullName, writeContent);

                    canExport = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    canExport = false;
                }

                return canExport;
            }
        }

        /// <summary>
        /// Saves the vault and empties all values in the memory
        /// </summary>
        public void Logout()
        {
            lock (_lock)
            {
                SaveVault(Vault, Name, _password);

                Name = "";
                _password = "";

                Vault = null;
            }
        }

        /// <summary>
        /// Deletes the vault and saves it before deleting it
        /// </summary>
        public void DeleteAccount()
        {
            lock (_lock)
            {
                Vault = new Vault();

                SaveVault(Vault, Name, _password);

                File.Delete(_vaultFolder + Name);
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
                return (Vault) new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
