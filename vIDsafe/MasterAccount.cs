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
        private string _name;
        private string _password;

        public Vault Vault;

        private readonly string _vaultFolder = "Vaults/";

        public string Name => _name;

        public enum VaultFormat
        {
            CSV,
            JSON,
            Encrypted
        }
        public MasterAccount()
        {
            _name = "";
            _password = "";
            Vault = new Vault();
        }

        private bool AccountExists(string name)
        {
            if (File.Exists(_vaultFolder + name))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> TryLogin(string name, string password)
        {
            bool loggedin = false;

            await Task.Run(() =>
            {
                if (AccountExists(name))
                {
                    _password = HashPassword(password, name);

                    Vault = GetVault(name, _password);

                    if (Vault != null)
                    {
                        _name = name;

                        loggedin = true;
                    }
                }
            });

            return loggedin;
        }

        public async Task<bool> TryRegister(string name, string password)
        {
            bool registered = false;

            await Task.Run(() =>
            {
                if (!AccountExists(name))
                {
                    _name = name;
                    _password = HashPassword(password, name);

                    CreateVault(Vault, _name, _password);

                    registered = true;
                }
            });

            return registered;
        }

        private bool VerifyPassword(string oldPassword)
        {
            if (HashPassword(oldPassword, _name).Equals(_password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> TryChangePassword(string oldPassword, string password)
        {
            bool changed = false;

            await Task.Run(() =>
            {
                if (VerifyPassword(oldPassword).Equals(true))
                {
                    _password = HashPassword(password, _name);

                    SaveVault(Vault, _name, _password);

                    changed = true;
                }
            });

            return changed;
        }

        public async Task<bool> TryChangeName(string oldPassword, string newName)
        {
            bool changed = false;

            await Task.Run(() =>
            {
                if (VerifyPassword(oldPassword).Equals(true))
                {
                    if (AccountExists(_name))
                    {
                        File.Move(_vaultFolder + _name, _vaultFolder + newName);
                    }

                    _name = newName;
                    _password = HashPassword(oldPassword, newName);

                    SaveVault(Vault, _name, _password);

                    changed = true;
                }
            });

            return changed;
        }

        private Vault GetVault(string name, string password)
        {
            string fileName = _vaultFolder + name;
            string encryptedVault = File.ReadAllText(fileName);

            return (DecryptVault(encryptedVault, password));           
        }

        private void CreateVault(Vault vault, string name, string password)
        {
            string encryptedVault = EncryptVault(vault, password);
            string fileName = _vaultFolder + name;

            FileInfo file = new FileInfo(fileName);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, encryptedVault);
        }

        //Todo: refactor and maybe have it call on closing or logging out of form only?
        private void SaveVault(Vault vault, string name, string password)
        {
            string encryptedVault = EncryptVault(vault, password);
            string fileName = _vaultFolder + name;

            FileInfo file = new FileInfo(fileName);

            if (file.Exists)
            {
                File.WriteAllText(file.FullName, encryptedVault);
            }
        }

        private string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(Encryption.DeriveKey(Encryption.KeyDerivationFunction.PBKDF2, password, salt));
        }

        private string EncryptVault(Vault vault, string key)
        {
            string serialisedVault = VaultToString(vault);

            return Encryption.AesEncrypt(serialisedVault, Convert.FromBase64String(key));
        }
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

        public async Task<bool> ImportVault(VaultFormat format, string fileName, bool replace)
        {
            Vault importedVault = new Vault();

            bool canImport = false;

            await Task.Run(() =>
            {
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
            });

            return canImport;
        }

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

        //Todo: Refactor the asynchronisation?
        public async Task<bool> ExportVaultAsync(VaultFormat format, string selectedEmail, string fileName)
        {
            Vault vault = Vault;

            bool canExport = false;

            await Task.Run(() =>
            {
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
            });

            return canExport;
        }

        public void Logout()
        {
            SaveVault(Vault, _name, _password);

            _name = "";
            _password = "";

            Vault = null;
        }

        public void DeleteAccount()
        {
            Vault = new Vault();

            SaveVault(Vault, _name, _password);

            File.Delete(_vaultFolder + _name);
        }

        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        private string VaultToString(Vault vault)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, vault);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

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
