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
        private int _weakCredentials;
        private int _conflictCredentials;
        private int _compromisedCredentials;
        private int _safeCredentials;

        private Dictionary<string, string> _breachedDomains = new Dictionary<string, string>();

        public Identity(string name)
        {
            this._name = name;
        }

        private void ResetCredentialCounts()
        {
            _healthScore = 100;

            _weakCredentials = 0;
            _conflictCredentials = 0;
            _compromisedCredentials = 0;
            _safeCredentials = 0;
        }

        public void CalculateHealthScore()
        {
            ResetCredentialCounts();

            GetBreaches();
            CheckBreaches();

            _safeCredentials = _credentials.Count - (_weakCredentials + _conflictCredentials + _compromisedCredentials);

            if (_credentials.Count > 0)
            {
                _healthScore = (int) (((double) _safeCredentials) / _credentials.Count * 100);
            }
        }

        public int GetCredentialCount()
        {
            if (_credentials.Count > 0)
            {
                return _credentials.Count;
            }

            return 0;
        }

        public Credential GetCredential(string key)
        {
            if (_credentials.ContainsKey(key))
            {
                return _credentials[key];
            }
            else
            {
                return new Credential("");
            }
        }

        public Dictionary<string, Credential> Credentials => _credentials;

        public string NewCredential(string username)
        {
            Credential credential = new Credential(username);

            string uniqueID = Guid.NewGuid().ToString();

            _credentials.Add(uniqueID, credential);

            FormvIDsafe.Main.User.SaveVault();

            return uniqueID;
        }

        public void DeleteCredential(string key)
        {
            if (_credentials.ContainsKey(key))
            {
                _credentials.Remove(key);
                FormvIDsafe.Main.User.SaveVault();
            }
        }

        public string Name =>_name;

        public string Email => _email;

        public string Usage => _usage;

        public int HealthScore => _healthScore;
        public int WeakCredentials => _weakCredentials;

        public int ConflictCredentials => _conflictCredentials;

        public int CompromisedCredentials => _compromisedCredentials;

        public int SafeCredentials => _safeCredentials;
        public Dictionary<string, string> BreachedDomains => _breachedDomains;

        public void SetDetails(string name, string email, string usage)
        {
            this._name = name;
            this._email = email;
            this._usage = usage;

            GetBreaches();

            FormvIDsafe.Main.User.SaveVault();
        }

        private void GetBreaches()
        {
            //TODO: Cleanup
            if (_email != null)
            {
                if (_email.Length > 0)
                {
                    List<ExposureDetails> exposureDetails = EnzoicAPI.GetExposureDetails(_email);

                    foreach (ExposureDetails detail in exposureDetails)
                    {
                        if (!_breachedDomains.ContainsKey(detail.Title))
                        {
                            _breachedDomains.Add(detail.Title, detail.Title);
                        }
                    }
                }
            }
        }

        private void CheckBreaches()
        {
            //TODO: Cleanup
            foreach (KeyValuePair<string, Credential> credential in _credentials)
            {
                if (_breachedDomains.ContainsKey(credential.Value.GetDomain()))
                {
                    credential.Value.SetStatus(Credential.CredentialStatus.Compromised);
                    _compromisedCredentials++;
                }
            }
        }
    }
}
