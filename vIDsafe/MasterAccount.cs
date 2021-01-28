using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CsvHelper;
using System.Globalization;

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

                if (Vault.Equals(null))
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

        public void ImportVault(VaultFormat format, string fileName, bool replace)
        {
            Vault vault = new Vault();

            switch (format)
            {
                case VaultFormat.CSV:




                    break;
                case VaultFormat.JSON:
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
                foreach (Identity identity in vault.Identities)
                {
                    if (FormvIDsafe.Main.User.Vault.Identities.Any(c => (c.Email.Equals(identity.Email, StringComparison.OrdinalIgnoreCase))))
                    {
                        Identity existingIdentity = Vault.Identities.FirstOrDefault(i => i.Email.Equals(identity.Email));

                        foreach (KeyValuePair<string, Credential> credential in identity.Credentials)
                        {
                            if (!existingIdentity.Credentials.ContainsKey(credential.Key))
                            {
                                existingIdentity.Credentials.Add(credential.Key, credential.Value);
                            }
                        }
                    }
                    else
                    {
                        Vault.Identities.Add(identity);
                    }
                }
            }
        }

        public void ExportVault(VaultFormat format, int identityIndex, string fileName)
        {
            Vault vault = new Vault();

            if (identityIndex > -1)
            {
                vault.Identities.Add(Vault.Identities[identityIndex]);
            }
            else
            {
                vault = Vault;
            }

            switch (format)
            {
                case VaultFormat.CSV:
                    string[] headers = new string[] { "Identity Name", "Identity Email", "Usage", "URL", "Username", "Password", "Notes" };

                    using (CsvWriter csv = new CsvWriter(new StreamWriter(fileName), CultureInfo.InvariantCulture))
                    {
                        foreach (string header in headers)
                        {
                            csv.WriteField(header);
                        }

                        csv.NextRecord();

                        foreach (Identity identity in vault.Identities)
                        {
                            foreach (KeyValuePair<string, Credential> credential in identity.Credentials)
                            {
                                csv.WriteField(identity.Name);
                                csv.WriteField(identity.Email);
                                csv.WriteField(identity.Usage);
                                csv.WriteField(credential.Value.URL);
                                csv.WriteField(credential.Value.Username);
                                csv.WriteField(credential.Value.Password);
                                csv.WriteField(credential.Value.Notes);

                                csv.NextRecord();
                            }
                        }
                    }

                    break;
                case VaultFormat.JSON:

                    break;
                case VaultFormat.Encrypted:
                    string encryptedVault = EncryptVault(vault, _password);

                    FileInfo file = new FileInfo(fileName);
                    file.Directory.Create(); // If the directory already exists, this method does nothing.
                    File.WriteAllText(file.FullName, encryptedVault.ToString());
                    break;
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
