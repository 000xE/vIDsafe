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
        private List<Identity> _identities = new List<Identity>();

        private int _overallHealthScore;

        private int _totalCredentialCount;
        private int _totalWeakCredentials;
        private int _totalConflictCredentials;
        private int _totalCompromisedCredentials;
        private int _totalSafeCredentials;

        public enum LogType
        {
            Account,
            Passwords
        }

        private Dictionary<LogType, Dictionary<DateTime, string>> _logs = new Dictionary<LogType, Dictionary<DateTime, string>>
        {
            [LogType.Account] = new Dictionary<DateTime, string>(),
            [LogType.Passwords] = new Dictionary<DateTime, string>()
        };

        public int OverallHealthScore => _overallHealthScore;

        public int TotalCredentialCount => _totalCredentialCount;
        public int TotalWeakCredentials => _totalWeakCredentials;

        public int TotalConflictCredentials => _totalConflictCredentials;

        public int TotalCompromisedCredentials => _totalCompromisedCredentials;

        public int TotalSafeCredentials => _totalSafeCredentials;


        public Vault()
        {

        }

        private void ResetCredentialCounts()
        {
            _overallHealthScore = 100;

            _totalCredentialCount = 0;
            _totalWeakCredentials = 0;
            _totalConflictCredentials = 0;
            _totalCompromisedCredentials = 0;
            _totalSafeCredentials = 0;
        }

        public void CalculateHealthScore()
        {
            ResetCredentialCounts();

            foreach (Identity identity in Identities)
            {
                identity.CalculateHealthScore();

                _totalCredentialCount += identity.GetCredentialCount();
                _totalWeakCredentials += identity.WeakCredentials;
                _totalConflictCredentials += identity.ConflictCredentials;
                _totalCompromisedCredentials += identity.CompromisedCredentials;
                _totalSafeCredentials += identity.SafeCredentials;
            }

            if (_totalCredentialCount > 0)
            {
                _overallHealthScore = (_totalSafeCredentials) / _totalCredentialCount * 100;
            }
        }

        public void NewIdentity(string name)
        {
            Identity identity = new Identity(name);
            _identities.Add(identity);

            FormvIDsafe.Main.User.SaveVault();
        }

        public Identity GetIdentity(int index)
        {
            if (_identities.Count > 0)
            {
                _identities[index].CalculateHealthScore();
                return _identities[index];
            }
            else
            {
                return new Identity("");
            }
        }

        public List<Identity> Identities => _identities;

        public void DeleteIdentity(int index)
        {
            if (_identities.Count > index)
            {
                _identities.RemoveAt(index);
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
            foreach (Identity identity in _identities)
            {
                identity.Credentials.Clear();

                FormvIDsafe.Main.User.SaveVault(); 
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
            FormvIDsafe.Main.User.SaveVault();

            return new KeyValuePair<DateTime, string>(currentTime, log);
        }
    }
}
