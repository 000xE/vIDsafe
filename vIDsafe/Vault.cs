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

        private Dictionary<string, Identity> _identities = new Dictionary<string, Identity>();

        private int _totalCredentialCount;
        public enum LogType
        {
            Account,
            Passwords,
            Porting
        }

        private Dictionary<LogType, Dictionary<DateTime, string>> _logs = new Dictionary<LogType, Dictionary<DateTime, string>>
        {
            [LogType.Account] = new Dictionary<DateTime, string>(),
            [LogType.Passwords] = new Dictionary<DateTime, string>(),
            [LogType.Porting] = new Dictionary<DateTime, string>()
        };

        private Dictionary<Credential.CredentialStatus, int> _totalCredentialCounts = new Dictionary<Credential.CredentialStatus, int>()
        {
            [Credential.CredentialStatus.Safe] = 0,
            [Credential.CredentialStatus.Compromised] = 0,
            [Credential.CredentialStatus.Conflicted] = 0,
            [Credential.CredentialStatus.Weak] = 0
        };

        public int OverallHealthScore => _overallHealthScore;

        public int TotalCredentialCount => _totalCredentialCount;

        public int TotalSafeCredentials => _totalCredentialCounts[Credential.CredentialStatus.Safe];

        public int TotalCompromisedCredentials => _totalCredentialCounts[Credential.CredentialStatus.Compromised];

        public int TotalConflictCredentials => _totalCredentialCounts[Credential.CredentialStatus.Conflicted];

        public int TotalWeakCredentials => _totalCredentialCounts[Credential.CredentialStatus.Weak];

        public Dictionary<string, Identity> Identities => _identities;

        public Vault()
        {

        }

        public string NewIdentity()
        {
            string nameToRandomise = "IdentityEmail";

            string email = CredentialGeneration.GenerateUsername(nameToRandomise) + "@test.com";
            string name = CredentialGeneration.GenerateUsername(nameToRandomise);
            string usage = "";

            CreateIdentity(name, email, usage);

            FormvIDsafe.Main.User.SaveVault();

            return email;
        }

        public Identity CreateIdentity(string name, string email, string usage)
        {
            Identity identity;

            if (Identities.ContainsKey(email))
            {
                identity = _identities[email];
            }
            else
            {
                identity = new Identity(name, email, usage);
                _identities.Add(email, identity);
            }

            return identity;
        }

        public void DeleteIdentity(string email)
        {
            if (_identities.ContainsKey(email))
            {
                _identities.Remove(email);
                FormvIDsafe.Main.User.SaveVault();
            }
        }

        public void DeleteAllIdentities()
        {
            _identities.Clear();
            FormvIDsafe.Main.User.SaveVault();
        }

        public void DeleteAllCredentials()
        {
            foreach (KeyValuePair<string, Identity> identity in _identities)
            {
                identity.Value.Credentials.Clear();
            }

            FormvIDsafe.Main.User.SaveVault();
        }

        public Dictionary<DateTime, string> GetLogs(LogType key)
        {
            return _logs[key];
        }

        public KeyValuePair<DateTime, string> Log(LogType key, string log)
        {
            DateTime currentTime = DateTime.Now;

            _logs[key].Add(currentTime, log);
            FormvIDsafe.Main.User.SaveVault();

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

        private void CountTotalCredentialStatus()
        {
            ResetTotalCredentialCounts();

            foreach (KeyValuePair<string, Identity> identityPair in Identities)
            {
                Identity identity = identityPair.Value;

                identity.CalculateHealthScore();

                foreach (KeyValuePair<Credential.CredentialStatus, int> status in identity.CredentialCounts)
                {
                    _totalCredentialCounts[status.Key] += status.Value;
                }

                _totalCredentialCount += identity.Credentials.Count;
            }
        }

        public void CalculateOverallHealthScore()
        {
            CountTotalCredentialStatus();

            if (_totalCredentialCount > 0)
            {
                _overallHealthScore = (int)((double)_totalCredentialCounts[Credential.CredentialStatus.Safe] / _totalCredentialCount * 100);
            }
            else
            {
                _overallHealthScore = 0;
            }

            FormvIDsafe.Main.User.SaveVault();
        }
    }
}
