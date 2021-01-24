using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnzoicClient;

namespace vIDsafe
{
    [Serializable]
    public class Identity
    {
        private Dictionary<string, Credential> _credentials = new Dictionary<string, Credential>();

        private string _name;
        private string _email;
        private string _usage;

        private int _healthScore;

        private Dictionary<Credential.CredentialStatus, int> _credentialCounts = new Dictionary<Credential.CredentialStatus, int>()
        {
            [Credential.CredentialStatus.Safe] = 0,
            [Credential.CredentialStatus.Compromised] = 0,
            [Credential.CredentialStatus.Conflicted] = 0,
            [Credential.CredentialStatus.Weak] = 0
        };

        private Dictionary<string, string> _breachedDomains = new Dictionary<string, string>();

        public string Name => _name;

        public string Email => _email;

        public string Usage => _usage;

        public int HealthScore => _healthScore;

        public Dictionary<Credential.CredentialStatus, int> CredentialCounts => _credentialCounts;

        public int SafeCredentials => _credentialCounts[Credential.CredentialStatus.Safe];

        public int CompromisedCredentials => _credentialCounts[Credential.CredentialStatus.Compromised];

        public int WeakCredentials => _credentialCounts[Credential.CredentialStatus.Weak];

        public int ConflictCredentials => _credentialCounts[Credential.CredentialStatus.Conflicted];

        public Dictionary<string, string> BreachedDomains => _breachedDomains;

        public Identity(string name)
        {
            _name = name;
        }

        public void CalculateHealthScore()
        {
            CountCrentials();

            if (_credentials.Count > 0)
            {
                _healthScore = (int)((double)_credentialCounts[Credential.CredentialStatus.Safe] / _credentials.Count * 100);
            }
            else
            {
                _healthScore = 0;
            }

            FormvIDsafe.Main.User.SaveVault();
        }

        private void ResetCredentialCounts()
        {
            foreach (Credential.CredentialStatus status in Enum.GetValues(typeof(Credential.CredentialStatus)))
            {
                _credentialCounts[status] = 0;
            }
        }

        private void CountCrentials()
        {
            ResetCredentialCounts();

            foreach (KeyValuePair<string, Credential> credential in _credentials)
            {
                _credentialCounts[credential.Value.Status]++;
            }
        }

        public Dictionary<string, Credential> Credentials => _credentials;

        public string NewCredential(int identityIndex, string username, string password)
        {
            Credential credential = new Credential(identityIndex, username, password);

            string GUID = credential.CredentialID;

            _credentials.Add(GUID, credential);

            FormvIDsafe.Main.User.SaveVault();

            return GUID;
        }

        public void DeleteCredential(string key)
        {
            if (_credentials.ContainsKey(key))
            {
                _credentials.Remove(key);

                FormvIDsafe.Main.User.SaveVault();
            }
        }

        public void SetDetails(string name, string email, string usage)
        {
            _name = name;
            _email = email;
            _usage = usage;

            FormvIDsafe.Main.User.SaveVault();
        }

        public Dictionary<string, string> GetBreaches(bool useAPI)
        {
            if (useAPI)
            {
                List<ExposureDetails> exposureDetails = EnzoicAPI.GetExposureDetails(_email);

                if (exposureDetails.Count > 0)
                {
                    _breachedDomains.Clear();

                    foreach (ExposureDetails detail in exposureDetails)
                    {
                        if (!_breachedDomains.ContainsKey(detail.Title))
                        {
                            _breachedDomains.Add(detail.Title, detail.Date.ToString());
                        }
                    }

                    foreach (KeyValuePair<string, Credential> credential in _credentials)
                    {
                        credential.Value.CheckBreached();
                    }

                    FormvIDsafe.Main.User.SaveVault();
                }
            }

            return _breachedDomains;
        }
    }
}
