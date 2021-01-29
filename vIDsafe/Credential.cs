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
        private readonly Identity _identity;

        private readonly string _credentialID;

        private string _username;
        private string _password;
        private string _url;
        private string _notes;

        private CredentialStatus _status = CredentialStatus.Safe;

        [Name("credentialID")]
        public string CredentialID => _credentialID;

        [Name("username")]
        public string Username => _username;

        [Name("password")]
        public string Password => _password;

        [Name("url")]
        public string URL => _url;

        [Name("notes")]
        public string Notes => _notes;

        [Ignore]
        [JsonIgnore]
        public CredentialStatus Status => _status;

        public enum CredentialStatus
        {
            Safe,
            Compromised,
            Conflicted,
            Weak
        }

        public Credential(Identity identity, string credentialID, string username, string password, string url, string notes)
        {
            _identity = identity;

            _credentialID = credentialID;

            _username = username;
            _password = password;
            _url = url;
            _notes = notes;

            _status = GetStatus();
        }

        public void SetDetails(string username, string password, string url, string notes)
        {
            _username = username;
            _password = password;
            _url = url;
            _notes = notes;

            _status = GetStatus();

            FormvIDsafe.Main.User.SaveVault();
        }

        public void SetStatus(CredentialStatus status)
        {
            _status = status;
        }

        public CredentialStatus GetStatus()
        {
            if (CheckBreached(_url))
            {
                return CredentialStatus.Compromised;
            }
            else if (CheckConflict(_username, _password))
            {
                return CredentialStatus.Conflicted;
            }
            else if (CheckWeak(_password))
            {
                return CredentialStatus.Weak;
            }
            else
            {
                return CredentialStatus.Safe;
            }
        }

        private string GetDomain(string url)
        {
            string host = new Uri(url).Host;
            string domain = host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);

            return domain;
        }
        private bool CheckBreached(string url)
        {
            if (url.Length > 0)
            {
                if (_identity.BreachedDomains.ContainsKey(GetDomain(url)))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckConflict(string username, string password)
        {
            if (username.Length > 0 || password.Length > 0)
            {
                foreach (KeyValuePair<string, Identity> identityPair in FormvIDsafe.Main.User.Vault.Identities)
                {
                    if (identityPair.Value.Credentials.Any(c => (c.Value != this)
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
