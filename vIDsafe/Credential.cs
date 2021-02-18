using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    [Serializable]
    public class Credential
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

        private readonly object _lock = new object();

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
            lock (_lock)
            {
                if (CheckBreached(identity.BreachedDomains, URL))
                {
                    Status = CredentialStatus.Compromised;
                }
                else if (CheckConflict(vault.Identities, Username, Password))
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
            string host = new Uri(url).Host;

            try
            {
                string domain = host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);

                return domain;
            }
            catch (Exception)
            {
                return host;
            }
        }

        /// <summary>
        /// Checks if the URL has been breached 
        /// </summary>
        /// <returns>
        /// True if the URL is valid and in the list, false if not
        /// </returns>
        private bool CheckBreached(Dictionary<string, string> breachedDomains, string url)
        {
            if (url.Length > 0)
            {
                if (breachedDomains.ContainsKey(GetDomain(url)))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the username or the password is present somewhere else
        /// </summary>
        /// <returns>
        /// True if either is present, false if not
        /// </returns>
        private bool CheckConflict(Dictionary<string, Identity> identities, string username, string password)
        {
            if (username.Length > 0 || password.Length > 0)
            {
                foreach (KeyValuePair<string, Identity> identityPair in identities)
                {
                    if (identityPair.Value.Credentials.Any(c => (c.Value.CredentialID != CredentialID)
                    && (c.Value.Username.Equals(username, StringComparison.OrdinalIgnoreCase) || c.Value.Password.Equals(password))))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a password's strength is below a threshold
        /// </summary>
        /// <returns>
        /// True if the URL is valid and in the list, false if not
        /// </returns>
        private bool CheckWeak(string password)
        {
            if (password.Length > 0)
            {
                double strengthThreshold = 30.0;

                if (CredentialGeneration.CheckStrength(password) < strengthThreshold)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
