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
            _name = name;
        }

        public void CalculateHealthScore()
        {
            CheckStatus();

            _weakCredentials = CountWeakCredentials();
            _conflictCredentials = CountConflictsCredentials();
            _compromisedCredentials = CountCompromisedCredentials();

            _safeCredentials = _credentials.Count - (_weakCredentials + _conflictCredentials + _compromisedCredentials);

            if (_credentials.Count > 0)
            {
                _healthScore = (int)(((double)_safeCredentials) / _credentials.Count * 100);
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
                return new Credential("", "");
            }
        }

        public Dictionary<string, Credential> Credentials => _credentials;

        public string NewCredential(string username, string password)
        {
            Credential credential = new Credential(username, password);

            string uniqueID = Guid.NewGuid().ToString();

            _credentials.Add(uniqueID, credential);

            Vault.IncrementConflictCount(username, password);

            FormvIDsafe.Main.User.SaveVault();

            return uniqueID;
        }

        public void DeleteCredential(string key)
        {
            if (_credentials.ContainsKey(key))
            {
                string username = _credentials[key].Username;
                string password = _credentials[key].Password;

                Vault.DecrementConflictCount(username, password);

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
            List<ExposureDetails> exposureDetails = EnzoicAPI.GetExposureDetails(_email);

            foreach (ExposureDetails detail in exposureDetails)
            {
                if (!_breachedDomains.ContainsKey(detail.Title))
                {
                    _breachedDomains.Add(detail.Title, detail.Title);
                }
            }
        }

        private int CountCompromisedCredentials()
        {
            int compromisedCount = 0;

            foreach (KeyValuePair<string, Credential> credential in _credentials)
            {
                if (credential.Value.Status == Credential.CredentialStatus.Compromised)
                {
                    compromisedCount++;
                }
            }

            return compromisedCount;
        }

        private int CountWeakCredentials()
        {
            int weakCount = 0;

            foreach (KeyValuePair<string, Credential> credential in _credentials)
            {
                if (credential.Value.Status == Credential.CredentialStatus.Weak)
                {
                    weakCount++;
                }
            }

            return weakCount;
        }

        private int CountConflictsCredentials()
        {
            int conflictCount = 0;

            foreach (KeyValuePair<string, Credential> credential in _credentials)
            {
                if (credential.Value.Status == Credential.CredentialStatus.Conflicted)
                {
                    conflictCount++;
                }
            }

            return conflictCount;
        }

        public void CheckStatus()
        {
            foreach (KeyValuePair<string, Credential> credential in _credentials)
            {
                if (CheckBreached(credential.Value))
                {
                    credential.Value.SetStatus(Credential.CredentialStatus.Compromised);
                }
                else if (CheckConflict(credential.Value))
                {
                    credential.Value.SetStatus(Credential.CredentialStatus.Conflicted);
                }
                else if (CheckWeak(credential.Value))
                {
                    credential.Value.SetStatus(Credential.CredentialStatus.Weak);
                }
                else
                {
                    credential.Value.SetStatus(Credential.CredentialStatus.Safe);
                }
            }
        }

        public bool CheckBreached(Credential credential)
        {
            if (_breachedDomains.ContainsKey(credential.GetDomain()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckWeak(Credential credential)
        {
            string password = credential.Password;

            double strengthThreshold = 30.0;

            if (CredentialGeneration.CheckStrength(password) < strengthThreshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckConflict(Credential credential)
        {
            string username = credential.Username;
            string password = credential.Password;

            if (Vault.UniqueUsernames.ContainsKey(username))
            {
                if (Vault.UniqueUsernames[username] > 1)
                {
                    return true;
                }
            }
            else
            {
                Vault.UniqueUsernames.Add(username, 1);
            }

            if (Vault.UniquePasswords.ContainsKey(password))
            {
                if (Vault.UniquePasswords[password] > 1)
                {
                    return true;
                }
            }
            else
            {
                Vault.UniquePasswords.Add(password, 1);
            }

            return false;
        }
    }
}
