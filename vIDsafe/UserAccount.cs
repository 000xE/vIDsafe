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
        private string name;
        private string password;

        private UserVault vault;

        private readonly string vaultFolder = "Vaults/";
        public UserAccount(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public string getName()
        {
            return name;
        }

        private bool accountExists()
        {
            if (File.Exists(vaultFolder + name))
            {
                return true;
            }

            return false;
        }

        public int tryLogin()
        {
            if (accountExists())
            {
                string encryptedVault = File.ReadAllText(vaultFolder + name);

                this.vault = decryptVault(encryptedVault);

                if (this.vault == null)
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
        public int tryRegister()
        {
            if (!accountExists())
            {
                vault = new UserVault();
                string encryptedVault = encryptVault();

                FileInfo file = new FileInfo(vaultFolder + name);
                file.Directory.Create(); // If the directory already exists, this method does nothing.
                File.WriteAllText(file.FullName, encryptedVault);

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void logout()
        {
            this.name = "";
            this.password = "";
            vault = new UserVault();
        }


        private void hashPassword()
        {
            //password = Encryption.hashPassword(password, name);

            password = Convert.ToBase64String(Encryption.hashPassword(password, name));
        }

        private string encryptVault()
        {
            hashPassword();

            string serialisedVault = objectToString(this.vault);

            return Encryption.aesEncrypt(serialisedVault, Convert.FromBase64String(password));
        }
        private UserVault decryptVault(string encryptedVault)
        {
            hashPassword();

            string decryptedVault = Encryption.aesDecrypt(encryptedVault, Convert.FromBase64String(password));

            if (decryptedVault != null)
            {
                return (UserVault)stringToObject(decryptedVault);
            }
            else
            {
                return null;
            }                   
        }

        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        public string objectToString(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, obj);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        //https://stackoverflow.com/questions/6979718/c-sharp-object-to-string-and-back/6979843#6979843
        public object stringToObject(string base64String)
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
