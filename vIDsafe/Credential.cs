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
        [Name("credentialID")]
        public string CredentialID { get; private set; } = "";

        [Name("username")]
        public string Username { get; set; } = "";

        [Name("password")]
        public string Password { get; set; } = "";

        [Name("url")]
        public string URL { get; set; } = "";

        [Name("notes")]
        public string Notes { get; set; } = "";

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

        public Credential(string credentialID, string username, string password, string url, string notes)
        {
            CredentialID = credentialID;

            Username = username;
            Password = password;
            URL = url;
            Notes = notes;
        }

        public void CalculateStatus(Vault vault, Identity identity)
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
