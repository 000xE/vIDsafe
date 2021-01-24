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
        private string _userName;
        private string _password;
        private string _url;
        private string _notes;

        private int _identityIndex;
        private readonly string _credentialID;

        private CredentialStatus _status = CredentialStatus.Safe;

        public string Username => _userName;

        public string Password => _password;

        public string URL => _url;

        public string Notes => _notes;

        public int IdentityIndex => _identityIndex;

        public string CredentialID => _credentialID;

        public CredentialStatus Status => _status;

        public enum CredentialStatus
        {
            Safe,
            Compromised,
            Conflicted,
            Weak
        }

        public Credential(int identityIndex, string username, string password)
        {
            _identityIndex = identityIndex;

            _userName = username;
            _password = password;

            _credentialID = Guid.NewGuid().ToString();

            _status = GetStatus();
        }

        public string GetDomain()
        {
            //Todo: validation
            if (_url != null)
            {
                string host = new Uri(_url).Host;
                return host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);
            }
            else
            {
                return "";
            }
        }

        public void SetDetails(string username, string password, string url, string notes)
        {
            _userName = username;
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
            if (CheckBreached())
            {
                return CredentialStatus.Compromised;
            }
            else if (CheckConflict())
            {
                return CredentialStatus.Conflicted;
            }
            else if (CheckWeak())
            {
                return CredentialStatus.Weak;
            }
            else
            {
                return CredentialStatus.Safe;
            }
        }

        private bool CheckBreached()
        {
            if (FormvIDsafe.Main.User.Vault.Identities[_identityIndex].BreachedDomains.ContainsKey(GetDomain()))
            {
                return true;
            }

            return false;
        }

        private bool CheckConflict()
        {
            foreach (Identity identity in FormvIDsafe.Main.User.Vault.Identities)
            {
                if (identity.Credentials.Any(c => (c.Value.CredentialID != _credentialID) 
                && (c.Value.Username.Equals(_userName, StringComparison.OrdinalIgnoreCase) || c.Value.Password.Equals(_password))))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckWeak()
        {
            double strengthThreshold = 30.0;

            if (CredentialGeneration.CheckStrength(_password) < strengthThreshold)
            {
                return true;
            }

            return false;
        }
    }
}
