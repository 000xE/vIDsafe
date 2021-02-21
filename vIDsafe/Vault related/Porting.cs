using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

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

        protected string ExportVault(VaultFormat format, string selectedEmail, string fileName)
        {
            Vault vault = Vault;

            string writeContent = "";

            if (selectedEmail.Length > 0)
            {
                vault.DeleteAllIdentities();

                Identity identity = vault.TryGetIdentity(selectedEmail);

                vault.TryAddIdentity(identity);
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
                        string encryptedVault = EncryptVault(vault, Password);
                        writeContent = encryptedVault;
                        break;
                }

                CreateFile(fileName, writeContent);

                return writeContent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        protected Vault ImportVault(VaultFormat format, string fileName)
        {
            Vault importedVault = new Vault();

            try
            {
                string readContent = File.ReadAllText(fileName);

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
                        importedVault = DecryptVault(readContent, Password);
                        break;
                }

                return importedVault;
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
        protected void AddImportedData(Vault importedVault, bool replace)
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
    }
}