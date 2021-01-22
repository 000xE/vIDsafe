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

        public static Dictionary<string, int> UniqueUsernames = new Dictionary<string, int>();
        public static Dictionary<string, int> UniquePasswords = new Dictionary<string, int>();

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

        private void ResetTotalCredentialCounts()
        {
            _overallHealthScore = 100;

            _totalCredentialCount = 0;
            _totalWeakCredentials = 0;
            _totalConflictCredentials = 0;
            _totalCompromisedCredentials = 0;
            _totalSafeCredentials = 0;
        }

        public void CalculateTotalHealthScore()
        {
            ResetTotalCredentialCounts();

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

            FormvIDsafe.Main.User.SaveVault();
        }

        public void NewIdentity(string name)
        {
            Identity identity = new Identity(name);
            _identities.Add(identity);

            FormvIDsafe.Main.User.SaveVault();
        }

        public int GetIdentityCount()
        {
            if (Identities.Count > 0)
            {
                return Identities.Count;
            }

            return 0;
        }

        public Identity GetIdentity(int index)
        {
            if (_identities.Count > 0)
            {
                //_identities[index].CalculateHealthScore();
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

        public static void DecrementConflictCount(string username, string password)
        {
            if (UniqueUsernames.ContainsKey(username))
            {
                if (UniqueUsernames[username] > 0)
                {
                    UniqueUsernames[username]--;
                }
            }

            if (UniquePasswords.ContainsKey(password))
            {
                if (UniquePasswords[password] > 0)
                {
                    UniquePasswords[password]--;
                }
            }
        }

        public static void IncrementConflictCount(string username, string password)
        {
            if (UniqueUsernames.ContainsKey(username))
            {
                UniqueUsernames[username]++;
            }
            else
            {
                UniqueUsernames.Add(username, 1);
            }

            if (UniquePasswords.ContainsKey(password))
            {
                UniquePasswords[password]++;
            }
            else
            {
                UniquePasswords.Add(password, 1);
            }
        }
    }
}
