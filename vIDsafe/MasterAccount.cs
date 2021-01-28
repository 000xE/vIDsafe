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

        public string Name => _name;

        private bool AccountExists()
        {
            if (File.Exists(_vaultFolder + _name))
            {
                return true;
            }

            return false;
        }

        public bool TryLogin()
        {
            if (AccountExists())
            {
                _password = HashPassword(_password, _name);

                Vault = GetVault();

                if (Vault == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool TryRegister()
        {
            if (!AccountExists())
            {
                _password = HashPassword(_password, _name);

                SaveVault();

                return true;
            }
            else
            {
                return false;
            }
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

        public bool TryChangePassword(string oldPassword, string password)
        {
            if (VerifyPassword(oldPassword).Equals(true))
            {
                _password = HashPassword(password, _name);
                SaveVault();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryChangeName(string oldPassword, string name)
        {
            if (VerifyPassword(oldPassword).Equals(true))
            {
                File.Move(_vaultFolder + _name, _vaultFolder + name);

                _name = name;
                _password = HashPassword(oldPassword, name);
                SaveVault();

                return true;
            }
            else
            {
                return false;
            }
        }

        private Vault GetVault()
        {
            string fileName = _vaultFolder + _name;
            string encryptedVault = File.ReadAllText(fileName);

            return (DecryptVault(encryptedVault, _password));           
        }

        public void SaveVault()
        {
            string encryptedVault = EncryptVault(Vault, _password);
            string fileName = _vaultFolder + _name;

            FileInfo file = new FileInfo(fileName);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, encryptedVault);
        }

        public void Logout()
        {
            _name = "";
            _password = "";
            Vault = new Vault();
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

        //Todo: Refactor
        public bool ImportVault(VaultFormat format, string fileName, bool replace)
        {
            Vault vault = new Vault();

            try
            {
                switch (format)
                {
                    case VaultFormat.CSV:
                        using (StreamReader streamReader = new StreamReader(fileName))
                        using (var csv = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                        {
                            csv.Read();
                            csv.ReadHeader();

                            while (csv.Read())
                            {
                                string identityName = csv.GetField(0);
                                string identityEmail = csv.GetField(1);
                                string identityUsage = csv.GetField(2);

                                Identity identity = vault.CreateIdentity(identityName, identityEmail, identityUsage);

                                string credentialID = csv.GetField(3);
                                string credentialURL = csv.GetField(4);
                                string credentialUsername = csv.GetField(5);
                                string credentialPassword = csv.GetField(6);
                                string credentialNotes = csv.GetField(7);

                                identity.CreateCredential(credentialID, credentialUsername, credentialPassword, credentialURL, credentialNotes);
                            }
                        }
                        break;
                    case VaultFormat.JSON:
                        string json = File.ReadAllText(fileName);
                        vault = JsonConvert.DeserializeObject<Vault>(json);
                        break;
                    case VaultFormat.Encrypted:
                        string encryptedVault = File.ReadAllText(fileName);
                        vault = DecryptVault(encryptedVault, _password);
                        break;
                }

                if (replace)
                {
                    Vault = vault;
                }
                else
                {
                    foreach (KeyValuePair<string, Identity> identityPair in vault.Identities)
                    {
                        string identityEmail = identityPair.Key;
                        Identity importedIdentity = identityPair.Value;

                        Identity identity = Vault.CreateIdentity(importedIdentity.Name, identityEmail, importedIdentity.Usage);

                        foreach (KeyValuePair<string, Credential> credentialPair in importedIdentity.Credentials)
                        {
                            string credentialID = credentialPair.Key;
                            Credential importedCredential = credentialPair.Value;

                            identity.CreateCredential(credentialID, importedCredential.Username, importedCredential.Password, importedCredential.URL, importedCredential.Notes);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        //Todo: Refactor
        public bool ExportVault(VaultFormat format, string selectedEmail, string fileName)
        {
            Vault vault = new Vault();
            string writeContent = "";

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
                        string[] headers = new string[] { "Identity Name", "Identity Email", "Usage", "ID", "URL", "Username", "Password", "Notes" };

                        using (StringWriter stringWriter = new StringWriter())
                        using (CsvWriter csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture))
                        {
                            foreach (string header in headers)
                            {
                                csv.WriteField(header);
                            }

                            csv.NextRecord();

                            foreach (KeyValuePair<string, Identity> identityPair in vault.Identities)
                            {
                                Identity identity = identityPair.Value;

                                foreach (KeyValuePair<string, Credential> credentialPair in identity.Credentials)
                                {
                                    Credential credential = credentialPair.Value;

                                    csv.WriteField(identity.Name);
                                    csv.WriteField(identity.Email);
                                    csv.WriteField(identity.Usage);
                                    csv.WriteField(credentialPair.Key);
                                    csv.WriteField(credential.URL);
                                    csv.WriteField(credential.Username);
                                    csv.WriteField(credential.Password);
                                    csv.WriteField(credential.Notes);

                                    csv.NextRecord();
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

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
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
