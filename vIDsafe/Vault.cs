using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    [Serializable]
    public class Vault
    {
        private int _overallHealthScore;

        private readonly Dictionary<string, Identity> _identities;

        private int _totalCredentialCount;

        public enum LogType
        {
            Account,
            Passwords,
            Porting
        }

        private readonly Dictionary<LogType, Dictionary<DateTime, string>> _logs;

        private readonly Dictionary<Credential.CredentialStatus, int> _totalCredentialCounts;

        [JsonIgnore]
        public int OverallHealthScore => _overallHealthScore;

        [JsonIgnore]
        public int TotalCredentialCount => _totalCredentialCount;

        [JsonIgnore]
        public int TotalSafeCredentials => _totalCredentialCounts[Credential.CredentialStatus.Safe];

        [JsonIgnore]
        public int TotalCompromisedCredentials => _totalCredentialCounts[Credential.CredentialStatus.Compromised];

        [JsonIgnore]
        public int TotalConflictCredentials => _totalCredentialCounts[Credential.CredentialStatus.Conflicted];

        [JsonIgnore]
        public int TotalWeakCredentials => _totalCredentialCounts[Credential.CredentialStatus.Weak];

        public Dictionary<string, Identity> Identities => _identities;

        public Vault()
        {
            _identities = new Dictionary<string, Identity>();

            _logs = new Dictionary<LogType, Dictionary<DateTime, string>>
            {
                [LogType.Account] = new Dictionary<DateTime, string>(),
                [LogType.Passwords] = new Dictionary<DateTime, string>(),
                [LogType.Porting] = new Dictionary<DateTime, string>()
            };

            _totalCredentialCounts = new Dictionary<Credential.CredentialStatus, int>()
            {
                [Credential.CredentialStatus.Safe] = 0,
                [Credential.CredentialStatus.Compromised] = 0,
                [Credential.CredentialStatus.Conflicted] = 0,
                [Credential.CredentialStatus.Weak] = 0
            };
        }

        public Identity GenerateIdentity()
        {
            string nameToRandomise = "abcdefghijklmnopqrstuvwxyz";

            string email = CredentialGeneration.GenerateUsername(nameToRandomise).ToLower() + "@test.com";
            string name = CredentialGeneration.GenerateUsername(nameToRandomise);
            string usage = "";

            Identity identity = FindOrCreateIdentity(name, email, usage);

            return identity;
        }

        public Identity FindOrCreateIdentity(string name, string email, string usage)
        {
            Identity identity;

            if (Identities.ContainsKey(email))
            {
                identity = _identities[email];
            }
            else
            {
                identity = new Identity(this, name, email, usage);
                _identities.Add(email, identity);
            }

            return identity;
        }

        public bool TryChangeIdentityEmail(string oldEmail, string newEmail)
        {
            Identity identity = _identities[oldEmail];

            if (_identities.ContainsKey(newEmail))
            {
                return false;
            }
            else
            {
                DeleteIdentity(oldEmail);

                identity.Email = newEmail;
                Identities.Add(newEmail, identity);

                return true;
            }
        }

        public void DeleteIdentity(string email)
        {
            if (_identities.ContainsKey(email))
            {
                _identities.Remove(email);
            }
        }

        public void DeleteAllIdentities()
        {
            _identities.Clear();
        }

        public void DeleteAllCredentials()
        {
            foreach (KeyValuePair<string, Identity> identity in _identities)
            {
                identity.Value.Credentials.Clear();
            }
        }

        public Dictionary<DateTime, string> GetLogs(LogType key)
        {
            return _logs[key];
        }

        public KeyValuePair<DateTime, string> Log(LogType key, string log)
        {
            DateTime currentTime = DateTime.Now;

            _logs[key].Add(currentTime, log);

            return new KeyValuePair<DateTime, string>(currentTime, log);
        }

        private void ResetTotalCredentialCounts()
        {
            _totalCredentialCount = 0;

            foreach (Credential.CredentialStatus status in Enum.GetValues(typeof(Credential.CredentialStatus)))
            {
                _totalCredentialCounts[status] = 0;
            }
        }

        private void CountTotalCredentialStatus(bool calculateStatuses)
        {
            ResetTotalCredentialCounts();

            foreach (KeyValuePair<string, Identity> identityPair in Identities)
            {
                Identity identity = identityPair.Value;

                identity.CalculateHealthScore(calculateStatuses);

                foreach (KeyValuePair<Credential.CredentialStatus, int> status in identity.CredentialCounts)
                {
                    _totalCredentialCounts[status.Key] += status.Value;
                }

                _totalCredentialCount += identity.Credentials.Count;
            }
        }

        public void CalculateOverallHealthScore(bool calculateStatuses)
        {
            CountTotalCredentialStatus(calculateStatuses);

            if (_totalCredentialCount > 0)
            {
                _overallHealthScore = (int)((double)_totalCredentialCounts[Credential.CredentialStatus.Safe] / _totalCredentialCount * 100);
            }
            else
            {
                _overallHealthScore = 0;
            }
        }
    }
}
