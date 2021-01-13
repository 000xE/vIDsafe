using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    [Serializable]
    public class UserVault
    {
        private List<Identity> _identities = new List<Identity>();

        private int _overallHealthScore;

        private int _totalCredentialCount;
        private int _totalWeakCredentials;
        private int _totalConflictCredentials;
        private int _totalCompromisedCredentials;
        private int _totalSafeCredentials;

        public UserVault()
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
                _totalCredentialCount += identity.GetCredentialCount();

                _totalWeakCredentials += identity.WeakCredentials;
                _totalConflictCredentials += identity.ConflictCredentials;
                _totalCompromisedCredentials += identity.CompromisedCredentials;
                _totalSafeCredentials += identity.SafeCredentials;
            }

            _overallHealthScore = (_totalSafeCredentials) / _totalCredentialCount * 100;
        }

        public void NewIdentity(string name)
        {
            Identity identity = new Identity(name);
            _identities.Add(identity);

            vIDsafe.Main.User.SaveVault();
        }

        public Identity GetIdentity(int index)
        {
            _identities[index].CalculateHealthScore();
            return _identities[index];
        }

        public List<Identity> Identities => _identities;

        public void DeleteIdentity(int index)
        {
            _identities.RemoveAt(index);
            vIDsafe.Main.User.SaveVault();
        }

        public void DeleteAllIdentities()
        {
            _identities.Clear();
            vIDsafe.Main.User.SaveVault();
        }
        public void DeleteAllCredentials()
        {
            foreach (Identity identity in _identities)
            {
                identity.Credentials.Clear();

                vIDsafe.Main.User.SaveVault(); 
            }
        }
    }
}
