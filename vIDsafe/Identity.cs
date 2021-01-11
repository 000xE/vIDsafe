using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    [Serializable]
    public class Identity
    {
        private List<Credential> _credentials = new List<Credential>();

        private string _name;
        private string _email;
        private string _usage;

        private int _healthScore;
        private int _weakCredentials;
        private int _conflictCredentials;
        private int _compromisedCredentials;
        private int _safeCredentials;

        public Identity(string name)
        {
            this._name = name;
        }

        private void CalculateHealthScore()
        {
            _healthScore = (_weakCredentials + _conflictCredentials + _compromisedCredentials) / _credentials.Count * 100;
        }

        private void CalculateSafeCredentials()
        {
            _safeCredentials = _credentials.Count - (_weakCredentials + _conflictCredentials + _compromisedCredentials);
        }

        public Credential GetCredential(int index)
        {
            return _credentials[index];
        }

        public List<Credential> Credentials => _credentials;

        public void NewCredential(string username)
        {
            Credential credential = new Credential(username);
            _credentials.Add(credential);

            vIDsafe.Main.User.SaveVault();
        }

        public void DeleteCredential(int index)
        {
            _credentials.RemoveAt(index);

            vIDsafe.Main.User.SaveVault();
        }

        public string Name =>_name;

        public string Email => _email;

        public string Usage => _usage;

        public int HealthScore => _healthScore;
        public int WeakCredentials => _weakCredentials;

        public int ConflictCredentials => _conflictCredentials;

        public int CompromisedCredentials => _compromisedCredentials;

        public int SafeCredentials => _safeCredentials;

        public void SetDetails(string name, string email, string usage)
        {
            this._name = name;
            this._email = email;
            this._usage = usage;

            vIDsafe.Main.User.SaveVault();
        }

        public int GetCredentialCount()
        {
            if (_credentials.Count > 0)
            {
                return _credentials.Count;
            }

            return 0;
        }
    }
}
