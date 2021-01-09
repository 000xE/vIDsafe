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

        public Identity(string name)
        {
            this._name = name;
        }

        private void calculateHealthScore()
        {
            _healthScore = (_weakCredentials + _conflictCredentials + _compromisedCredentials) / _credentials.Count * 100;
        }

        private void deleteCredential(int index)
        {
            _credentials.RemoveAt(index);
        }

        public string Name =>_name;

        public string Email => _email;

        public string Usage => _usage;

        public void SetDetails(string name, string email, string usage)
        {
            this._name = name;
            this._email = email;
            this._usage = usage;

            vIDsafe.Main.User.SaveVault();
        }
    }
}
