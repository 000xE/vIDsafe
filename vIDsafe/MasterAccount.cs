using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace vIDsafe
{
    [Serializable]
    public class MasterAccount
    {
        private string _name;
        private string _password;

        public Vault Vault = new Vault();

        private readonly string _vaultFolder = "Vaults/";
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
                _password = HashPassword(_password);

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
                _password = HashPassword(_password);

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
            if (HashPassword(oldPassword) == _password)
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
            if (VerifyPassword(oldPassword) == true)
            {
                _password = HashPassword(password);
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
            if (VerifyPassword(oldPassword) == true)
            {
                File.Move(_vaultFolder + _name, _vaultFolder + name);

                _name = name;
                _password = HashPassword(oldPassword);
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
            string encryptedVault = File.ReadAllText(_vaultFolder + _name);

            return (DecryptVault(encryptedVault));           
        }

        public void SaveVault()
        {
            string encryptedVault = EncryptVault();

            FileInfo file = new FileInfo(_vaultFolder + _name);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            File.WriteAllText(file.FullName, encryptedVault);
        }

        public void Logout()
        {
            _name = "";
            _password = "";
            Vault = new Vault();
        }

        private string HashPassword(string password)
        {
            return Convert.ToBase64String(Encryption.DeriveKey(Encryption.KeyDerivationFunction.PBKDF2, password, _name));
        }

        private string EncryptVault()
        {
            string serialisedVault = ObjectToString(Vault);

            return Encryption.AesEncrypt(serialisedVault, Convert.FromBase64String(_password));
        }
        private Vault DecryptVault(string encryptedVault)
        {
            string decryptedVault = Encryption.AesDecrypt(encryptedVault, Convert.FromBase64String(_password));

            if (decryptedVault != null)
            {
                return (Vault)StringToObject(decryptedVault);
            }
            else
            {
                return null;
            }                   
        }

        public void DeleteAccount()
        {
            Vault = new Vault();

            SaveVault();

            File.Delete(_vaultFolder + _name);
        }

        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        public string ObjectToString(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        public object StringToObject(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return (Vault) new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
