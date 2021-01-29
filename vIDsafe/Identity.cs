using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnzoicClient;
using Newtonsoft.Json;

namespace vIDsafe
{
    [Serializable]
    public class Identity
    {
        private string _name;
        private string _usage;

        private int _healthScore;

        private Dictionary<string, Credential> _credentials = new Dictionary<string, Credential>();

        private Dictionary<Credential.CredentialStatus, int> _credentialCounts = new Dictionary<Credential.CredentialStatus, int>()
        {
            [Credential.CredentialStatus.Safe] = 0,
            [Credential.CredentialStatus.Compromised] = 0,
            [Credential.CredentialStatus.Conflicted] = 0,
            [Credential.CredentialStatus.Weak] = 0
        };

        private Dictionary<string, string> _breachedDomains = new Dictionary<string, string>();

        public string Name => _name;

        public string Usage => _usage;

        [JsonIgnore]
        public int HealthScore => _healthScore;

        [JsonIgnore]
        public Dictionary<Credential.CredentialStatus, int> CredentialCounts => _credentialCounts;

        [JsonIgnore]
        public int SafeCredentials => _credentialCounts[Credential.CredentialStatus.Safe];

        [JsonIgnore]
        public int CompromisedCredentials => _credentialCounts[Credential.CredentialStatus.Compromised];

        [JsonIgnore]
        public int WeakCredentials => _credentialCounts[Credential.CredentialStatus.Weak];

        [JsonIgnore]
        public int ConflictCredentials => _credentialCounts[Credential.CredentialStatus.Conflicted];

        public Dictionary<string, string> BreachedDomains => _breachedDomains;

        public Dictionary<string, Credential> Credentials => _credentials;

        public Identity(string name, string usage)
        {
            _name = name;
            _usage = usage;
        }

        public string NewCredential(string identityName)
        {
            string GUID = Guid.NewGuid().ToString();

            string url = "";
            string notes = "";

            string username = CredentialGeneration.GenerateUsername(identityName);
            string password = CredentialGeneration.GeneratePassword();

            CreateCredential(GUID, username, password, url, notes);

            FormvIDsafe.Main.User.SaveVault();

            return GUID;
        }

        public Credential CreateCredential(string GUID, string username, string password, string url, string notes)
        {
            Credential credential;

            if (Credentials.ContainsKey(GUID))
            {
                credential = Credentials[GUID];
            }
            else
            {
                 credential = new Credential(this, username, password, url, notes);
                _credentials.Add(GUID, credential);
            }

            return credential;
        }

        public void DeleteAllCredentials()
        {
            Credentials.Clear();

            FormvIDsafe.Main.User.SaveVault();
        }

        public void DeleteCredential(string key)
        {
            if (_credentials.ContainsKey(key))
            {
                _credentials.Remove(key);

                FormvIDsafe.Main.User.SaveVault();
            }
        }

        public void SetDetails(string name, string usage)
        {
            _name = name;
            _usage = usage;

            FormvIDsafe.Main.User.SaveVault();
        }

        public Dictionary<string, string> GetBreaches(string email, bool useAPI)
        {
            if (useAPI)
            {
                List<ExposureDetails> exposureDetails = EnzoicAPI.GetExposureDetails(email);

                _breachedDomains.Clear();

                if (exposureDetails.Count > 0)
                {
                    foreach (ExposureDetails detail in exposureDetails)
                    {
                        if (!_breachedDomains.ContainsKey(detail.Title))
                        {
                            _breachedDomains.Add(detail.Title, detail.Date.ToString());
                        }
                    }
                }
            }

            return _breachedDomains;
        }

        private void ResetCredentialCounts()
        {
            foreach (Credential.CredentialStatus status in Enum.GetValues(typeof(Credential.CredentialStatus)))
            {
                _credentialCounts[status] = 0;
            }
        }

        private void CountCredentialStatus()
        {
            ResetCredentialCounts();

            foreach (KeyValuePair<string, Credential> credential in _credentials)
            {
                _credentialCounts[credential.Value.Status]++;
            }
        }

        private void SetCredentialStatuses()
        {
            foreach (KeyValuePair<string, Credential> credential in _credentials)
            {
                credential.Value.SetStatus(credential.Value.GetStatus());
            }
        }

        public void CalculateHealthScore()
        {
            SetCredentialStatuses();

            CountCredentialStatus();

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
    }
}
