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
    public sealed class MasterAccount : Porting
    {
        public static readonly MasterAccount User = new MasterAccount();

        ///<value>Get or set the account name</value>
        public string Name { get; private set; } = "";

        ///<value>Get or set the account password</value>
        private string Password { get; set; } = "";

        /// <summary>
        /// Creates a master account singleton (private)
        /// </summary>
        /// <returns>
        /// The master account
        /// </returns>
        private MasterAccount() { }

        /// <summary>
        /// Tries to login to the account
        /// </summary>
        /// <returns>
        /// True if the logged in, false if not
        /// </returns>
        public bool TryLogin(string name, string password)
        {
            if (VaultExists(name))
            {
                Password = HashPassword(password, name);
                Name = name;

                Vault = RetrieveVault(Name, Password);

                if (Vault != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to register an account
        /// </summary>
        /// <returns>
        /// True if the registered, false if not
        /// </returns>
        public bool TryRegister(string name, string password)
        {
            if (!VaultExists(name))
            {
                Name = name;
                Password = HashPassword(password, name);

                Vault = new Vault();

                SaveVault(Vault, Name, Password);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the entered password is correct
        /// </summary>
        /// <returns>
        /// True if the correct, false if not
        /// </returns>
        private bool VerifyPassword(string password)
        {
            if (HashPassword(password, Name).Equals(Password))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to change the password of the account
        /// </summary>
        /// <returns>
        /// True if the password is changed, false if not
        /// </returns>
        public bool TryChangePassword(string password, string newPassword)
        {
            if (VerifyPassword(password).Equals(true))
            {
                Password = HashPassword(newPassword, Name);

                SaveVault(Vault, Name, Password);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to change the name of the account
        /// </summary>
        /// <returns>
        /// True if the name is changed, false if not
        /// </returns>
        public bool TryChangeName(string password, string newName)
        {
            if (VerifyPassword(password).Equals(true))
            {
                Name = newName;
                Password = HashPassword(password, newName);

                DeleteVault(Name);

                SaveVault(Vault, Name, Password);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to import a vault
        /// </summary>
        /// <returns>
        /// True if vault is imported, false if not
        /// </returns>
        public bool TryImportVault(VaultFormat format, string fileName, bool replace)
        {
            string serialisedVault = File.ReadAllText(fileName);

            Vault importedVault = TryDeserializeObject(format, serialisedVault, Password);

            if (importedVault != null)
            {
                AddImportedData(importedVault, replace);

                return true;
            }

            return false;
        }

        //Todo: Refactor the selected email thing
        /// <summary>
        /// Tries to export the vault
        /// </summary>
        /// <returns>
        /// True if the vault is export, false if not
        /// </returns>
        public bool TryExportVault(VaultFormat format, string email, string fileName)
        {
            Vault vault = new Vault();

            if (email.Length > 0)
            {
                Identity identity = vault.TryGetIdentity(email);

                vault.TryAddIdentity(identity);
            }
            else
            {
                vault = Vault;
            }

            string serialisedVault = TrySerializeVault(format, vault, Password);

            if (serialisedVault.Length > 0)
            {
                CreateFile(fileName, serialisedVault);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Hashes the password
        /// </summary>
        /// <returns>
        /// The hashed password if it's hashed, null if not
        /// </returns>
        private string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(Hashing.DeriveKey(Hashing.KeyDerivationFunction.PBKDF2, password, salt));
        }

        /// <summary>
        /// Saves the vault and empties all values in the memory
        /// </summary>
        public void Logout()
        {
            SaveVault(Vault, Name, Password);

            Name = "";
            Password = "";
            Vault = null;
        }

        /// <summary>
        /// Deletes the vault and saves it before deleting it
        /// </summary>
        public void DeleteAccount()
        {
            Vault = new Vault();

            SaveVault(Vault, Name, Password);

            DeleteVault(Name);
        }
    }
}
