using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace vIDsafe
{
    public abstract class Porting : Storage
    {
        public enum VaultFormat
        {
            CSV,
            JSON,
            Encrypted
        }

        /// <summary>
        /// Tries to serialise a vault to a format
        /// </summary>
        /// <returns>
        /// Serialised vault, null if not
        /// </returns>
        protected string TrySerializeVault(VaultFormat format, Vault vault, string password)
        {
            string serialisedVault = "";

            try
            {
                switch (format)
                {
                    case VaultFormat.CSV:
                        serialisedVault = SerializeCSV(vault);
                        break;
                    case VaultFormat.JSON:
                        serialisedVault = SerializeJSON(vault);
                        break;
                    case VaultFormat.Encrypted:
                        serialisedVault = SerializeEncrypted(vault, password);
                        break;
                }

                return serialisedVault;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Tries to deserialise a vault from a format
        /// </summary>
        /// <returns>
        /// Deserialised vault, null if not
        /// </returns>
        protected Vault TryDeserializeObject(VaultFormat format, string vault, string password)
        {
            Vault deserialisedVault = new Vault();

            try
            {
                switch (format)
                {
                    case VaultFormat.CSV:
                        deserialisedVault = DeserializeCSV(vault);
                        break;
                    case VaultFormat.JSON:
                        deserialisedVault = DeserializeJSON(vault);
                        break;
                    case VaultFormat.Encrypted:
                        deserialisedVault = DeserializeEncrypted(vault, password);
                        break;
                }

                return deserialisedVault;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Tries to serialise a vault to CSV
        /// </summary>
        /// <returns>
        /// Serialised vault, empty vault if not
        /// </returns>
        private string SerializeCSV(Vault vault)
        {
            string serialisedVault = "";

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

                serialisedVault = stringWriter.ToString();
            }

            return serialisedVault;
        }

        /// <summary>
        /// Tries to serialise a vault to JSON
        /// </summary>
        /// <returns>
        /// Serialised vault, empty vault if not
        /// </returns>
        private string SerializeJSON(Vault vault)
        {
            string serialisedVault = JsonConvert.SerializeObject(vault, Formatting.Indented);

            return serialisedVault;
        }

        /// <summary>
        /// Tries to serialise a vault to encrypted
        /// </summary>
        /// <returns>
        /// Serialised vault, empty vault if not
        /// </returns>
        private string SerializeEncrypted(Vault vault, string password)
        {
            string serialisedVault = EncryptVault(vault, password);

            return serialisedVault;
        }

        /// <summary>
        /// Tries to deserialise a vault from CSV
        /// </summary>
        /// <returns>
        /// Deserialised vault, empty vault if not
        /// </returns>
        private Vault DeserializeCSV(string vault)
        {
            Vault deserialisedVault = new Vault();

            using (StringReader stringReader = new StringReader(vault))
            using (CsvReader csvReader = new CsvReader(stringReader, CultureInfo.InvariantCulture))
            {
                csvReader.Read();
                csvReader.ReadHeader();

                while (csvReader.Read())
                {
                    Identity identity = csvReader.GetRecord<Identity>();

                    identity = deserialisedVault.FindOrCreateIdentity(identity.Name, identity.Email, identity.Usage);

                    Credential credential = csvReader.GetRecord<Credential>();

                    identity.FindOrCreateCredential(credential.CredentialID, credential.Username, credential.Password, credential.URL, credential.Notes);
                }
            }

            return deserialisedVault;
        }

        /// <summary>
        /// Tries to deserialise a vault from JSON
        /// </summary>
        /// <returns>
        /// Deserialised vault, empty vault if not
        /// </returns>
        private Vault DeserializeJSON(string vault)
        {
            Vault deserialisedVault = JsonConvert.DeserializeObject<Vault>(vault);

            return deserialisedVault;
        }

        /// <summary>
        /// Tries to deserialise a vault from encrypted
        /// </summary>
        /// <returns>
        /// Deserialised vault, empty vault if not
        /// </returns>
        private Vault DeserializeEncrypted(string vault, string password)
        {
            Vault deserialisedVault = DecryptVault(vault, password);

            return deserialisedVault;
        }

        /// <summary>
        /// Adds the data inside the imported vault
        /// </summary>
        protected void AddImportedData(Vault vault, Vault importedVault, bool replace)
        {
            if (replace)
            {
                vault.DeleteAllIdentities();
            }

            foreach (KeyValuePair<string, Identity> identityPair in importedVault.Identities)
            {
                string email = identityPair.Key;
                Identity importedIdentity = identityPair.Value;

                Identity identity = vault.FindOrCreateIdentity(importedIdentity.Name, email, importedIdentity.Usage);

                foreach (KeyValuePair<string, Credential> credentialPair in importedIdentity.Credentials)
                {
                    string credentialID = credentialPair.Key;
                    Credential credential = credentialPair.Value;

                    identity.FindOrCreateCredential(credentialID, credential.Username, credential.Password, credential.URL, credential.Notes);
                }
            }
        }
    }
}