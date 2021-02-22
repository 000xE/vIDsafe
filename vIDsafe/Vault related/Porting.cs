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
        /// True if vault is serialised, false if not
        /// </returns>
        protected string TrySerializeVault(VaultFormat format, Vault vault, string password)
        {
            string serialisedVault = "";

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

                            serialisedVault = stringWriter.ToString();
                        }
                        break;
                    case VaultFormat.JSON:
                        serialisedVault = JsonConvert.SerializeObject(vault, Formatting.Indented);
                        break;
                    case VaultFormat.Encrypted:
                        serialisedVault = EncryptVault(vault, password);
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
        /// True if vault is deserialise, false if not
        /// </returns>
        protected Vault TryDeserializeObject(VaultFormat format, string serialisedVault, string password)
        {
            Vault deserialisedVault = new Vault();

            try
            {
                switch (format)
                {
                    case VaultFormat.CSV:
                        using (StringReader stringReader = new StringReader(serialisedVault))
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
                        break;
                    case VaultFormat.JSON:
                        deserialisedVault = JsonConvert.DeserializeObject<Vault>(serialisedVault);
                        break;
                    case VaultFormat.Encrypted:
                        deserialisedVault = DecryptVault(serialisedVault, password);
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