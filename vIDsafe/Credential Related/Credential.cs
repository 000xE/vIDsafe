using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    [Serializable]
    public class Credential : Status
    {
        ///<value>Get or set the credential ID</value>
        [Name("credentialID")]
        public string CredentialID { get; private set; } = "";

        ///<value>Get or set the credential username</value>
        [Name("username")]
        public string Username { get; set; } = "";

        ///<value>Get or set the credential password</value>
        [Name("password")]
        public string Password { get; set; } = "";

        ///<value>Get or set the credential URL</value>
        [Name("url")]
        public string URL { get; set; } = "";

        ///<value>Get or set the credential notes</value>
        [Name("notes")]
        public string Notes { get; set; } = "";

        ///<value>Get or set the credential status</value>
        [Ignore]
        [JsonIgnore]
        public CredentialStatus Status { get; set; } = CredentialStatus.Safe;

        public enum CredentialStatus
        {
            Safe,
            Compromised,
            Conflicted,
            Weak
        }

        /// <summary>
        /// Creates a credential
        /// </summary>
        /// <returns>
        /// The credential
        /// </returns>
        public Credential(string credentialID, string username, string password, string url, string notes)
        {
            CredentialID = credentialID;

            Username = username;
            Password = password;
            URL = url;
            Notes = notes;
        }

        /// <summary>
        /// Calculates the status of the credential using its vault and identity
        /// </summary>
        public void CalculateStatus(Vault vault, Identity identity)
        {
            if (CheckBreached(identity.BreachedDomains, GetDomain(URL)))
            {
                Status = CredentialStatus.Compromised;
            }
            else if (CheckConflict(vault.Identities, CredentialID, Username, Password))
            {
                Status = CredentialStatus.Conflicted;
            }
            else if (CheckWeak(Password))
            {
                Status = CredentialStatus.Weak;
            }
            else
            {
                Status = CredentialStatus.Safe;
            }
        }

        /// <summary>
        /// Extracts the domain from a URL
        /// </summary>
        /// <returns>
        /// The domain
        /// </returns>
        //https://www.csharp-console-examples.com/general/c-get-domain-name-from-url/
        private string GetDomain(string url)
        {
            string domain = "";

            if (url.Length > 0)
            {
                string host = new Uri(url).Host;

                try
                {
                    domain = host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);
                }
                catch (Exception)
                {
                    return domain;
                }
            }

            return domain;
        }
    }
}
