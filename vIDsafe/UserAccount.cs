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

        private bool accountExists()
        {
            if (File.Exists(_vaultFolder + _name))
            {
                return true;
            }

            return false;
        }

        public int TryLogin()
        {
            if (accountExists())
            {
                hashPassword();

                this.Vault = getVault();

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
            if (!accountExists())
            {
                hashPassword();

                SaveVault();

                return 1;
            }
            else
            {
                return 0;
            }
        }

        private UserVault getVault()
        {
            string encryptedVault = File.ReadAllText(_vaultFolder + _name);

            return (decryptVault(encryptedVault));           
        }

        public void SaveVault()
        {
            string encryptedVault = encryptVault();

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

        private void hashPassword()
        {
            //password = Encryption.hashPassword(password, name);

            _password = Convert.ToBase64String(Encryption.HashPassword(_password, _name));
        }

        private string encryptVault()
        {
            string serialisedVault = ObjectToString(this.Vault);

            return Encryption.AesEncrypt(serialisedVault, Convert.FromBase64String(_password));
        }
        private UserVault decryptVault(string encryptedVault)
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
