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
    public class UserAccount
    {
        private string _name;
        private string _password;

        public UserVault Vault = new UserVault();

        private readonly string _vaultFolder = "Vaults/";
        public UserAccount(string name, string password)
        {
            this._name = name;
            this._password = password;
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

        public int TryLogin()
        {
            if (AccountExists())
            {
                _password = HashPassword(_password);

                this.Vault = GetVault();

                if (this.Vault == null)
                {
                    return 2;
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }

        //https://stackoverflow.com/a/2955425
        public int TryRegister()
        {
            if (!AccountExists())
            {
                _password = HashPassword(_password);

                SaveVault();

                return 1;
            }
            else
            {
                return 0;
            }
        }

        private int VerifyPassword(string oldPassword)
        {
            if (HashPassword(oldPassword) == this._password)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int TryChangePassword(string oldPassword, string password)
        {
            if (VerifyPassword(oldPassword) == 1)
            {
                this._password = HashPassword(password);
                SaveVault();

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int TryChangeName(string oldPassword, string name)
        {
            if (VerifyPassword(oldPassword) == 1)
            {
                File.Move(_vaultFolder + this._name, _vaultFolder + name);

                this._name = name;
                this._password = HashPassword(oldPassword);
                SaveVault();


                return 1;
            }
            else
            {
                return 0;
            }
        }

        private UserVault GetVault()
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
            this._name = "";
            this._password = "";
            Vault = new UserVault();
        }

        private string HashPassword(string password)
        {
            //password = Encryption.hashPassword(password, name);

            return Convert.ToBase64String(Encryption.HashPassword(password, _name));
        }

        private string EncryptVault()
        {
            string serialisedVault = ObjectToString(this.Vault);

            return Encryption.AesEncrypt(serialisedVault, Convert.FromBase64String(_password));
        }
        private UserVault DecryptVault(string encryptedVault)
        {
            string decryptedVault = Encryption.AesDecrypt(encryptedVault, Convert.FromBase64String(_password));

            if (decryptedVault != null)
            {
                return (UserVault)StringToObject(decryptedVault);
            }
            else
            {
                return null;
            }                   
        }

        public void DeleteAccount()
        {
            Vault = new UserVault();

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
                return (UserVault) new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
