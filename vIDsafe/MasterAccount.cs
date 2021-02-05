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

        public Vault Vault = new Vault();

        private readonly string _vaultFolder = "Vaults/";

        public string Name => _name;

        public enum VaultFormat
        {
            CSV,
            JSON,
            Encrypted
        }
        public MasterAccount(string name, string password)
        {
            _name = name;
            _password = password;
        }

        private bool AccountExists()
        {
            if (File.Exists(_vaultFolder + _name))
            {
                return true;
            }

            return false;
        }

        public async Task<bool> TryLogin()
        {
            bool canLogin = false;

            await Task.Run(() =>
            {
                if (AccountExists())
                {
                    _password = HashPassword(_password, _name);

                    Vault = GetVault();

                    if (Vault == null)
                    {
                        canLogin = false;
                    }
                    else
                    {
                        canLogin = true;
                    }
                }
                else
                {
                    canLogin = false;
                }
            });

            return canLogin;
        }

        public async Task<bool> TryRegister()
        {
            bool canRegister = false;

            await Task.Run(() =>
            {
                if (!AccountExists())
                {
                    _password = HashPassword(_password, _name);

                    SaveVault();

                    canRegister = true;
                }
                else
                {
                    canRegister = false;
                }
            });

            return canRegister;
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
            bool canChangePass = false;

            await Task.Run(() =>
                {
                if (VerifyPassword(oldPassword).Equals(true))
                {
                    _password = HashPassword(password, _name);
                    SaveVault();

                    canChangePass = true;
                }
                else
                {
                    canChangePass = false;
                }
            });

            return canChangePass;
        }

        public async Task<bool> TryChangeName(string oldPassword, string name)
        {
            bool canChangeName = false;

            await Task.Run(() =>
            {
                if (VerifyPassword(oldPassword).Equals(true))
                {
                    File.Move(_vaultFolder + _name, _vaultFolder + name);

                    _name = name;
                    _password = HashPassword(oldPassword, name);
                    SaveVault();

                    return canChangeName = true;
                }
                else
                {
                    return canChangeName = false;
                }
            });

            return canChangeName;
        }

        private Vault GetVault()
        {
            string fileName = _vaultFolder + _name;
            string encryptedVault = File.ReadAllText(fileName);

            return (DecryptVault(encryptedVault, _password));           
        }

        //Todo: refactor and maybe have it call on closing or logging out of form only?
        public void SaveVault()
        {
            string encryptedVault = EncryptVault(Vault, _password);
            string fileName = _vaultFolder + _name;

            FileInfo file = new FileInfo(fileName);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, encryptedVault);
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

            string readContent = File.ReadAllText(fileName);

            bool canImport = false;

            await Task.Run(() =>
            {
                try
                {
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
            Vault vault = new Vault();

            string writeContent = "";

            bool canExport = false;

            await Task.Run(() =>
            {
                if (selectedEmail.Length > 0)
                {
                    vault.Identities.Add(selectedEmail, Vault.Identities[selectedEmail]);
                }
                else
                {
                    vault = Vault;
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

        public void DeleteAccount()
        {
            Vault = new Vault();

            SaveVault();

            File.Delete(_vaultFolder + _name);
        }

        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        public string VaultToString(Vault vault)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, vault);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        public Vault StringToVault(string base64String)
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
