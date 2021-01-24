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

        private CredentialStatus _status = CredentialStatus.Safe;

        private int _identityIndex;

        private readonly string _credentialID;
        public string Username => _userName;

        public string Password => _password;

        public string URL => _url;

        public string Notes => _notes;

        public CredentialStatus Status => _status;
        public string CredentialID => _credentialID;

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

            _status = CheckStatus();
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

            _status = CheckStatus();

            FormvIDsafe.Main.User.SaveVault();
        }

        public void SetStatus(CredentialStatus status)
        {
            _status = status;
        }

        public CredentialStatus CheckStatus()
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
            else
            {
                return false;
            }
        }

        private bool CheckWeak()
        {
            double strengthThreshold = 30.0;

            if (CredentialGeneration.CheckStrength(_password) < strengthThreshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckConflict()
        {
            foreach (Identity identity in FormvIDsafe.Main.User.Vault.Identities)
            {
                foreach (KeyValuePair<string, Credential> credentialPair in identity.Credentials)
                {
                    Credential credential = credentialPair.Value;

                    if (credential.CredentialID != _credentialID)
                    {
                        if (credential.Username == _userName || credential.Password == _password)
                        {
                            credential.SetStatus(CredentialStatus.Conflicted);
                            return true;
                        }
                    }
                }
                /*if (identity.Credentials.Any(c => (c.Value.GUID != _guid) && (c.Value.Username == _userName || c.Value.Password == _password)))
                {
                    return true;
                }*/
            }

            return false;
        }
    }
}
