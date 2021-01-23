using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    [Serializable]
    public class Credential
    {
        private string _userName;
        private string _password;
        private string _url;
        private string _notes;

        private CredentialStatus _status = CredentialStatus.Safe;

        //Compromised = top prio, conflict 2nd top, etc. upon recalculation the next prio should be set

        public enum CredentialStatus
        {
            Safe,
            Weak,
            Conflicted,
            Compromised
        }

        public Credential(string username, string password)
        {
            _userName = username;
            _password = password;
        }

        public string Username => _userName;

        public string Password => _password;

        public string URL => _url;

        public string Notes => _notes;

        public CredentialStatus Status => _status;

        public string GetDomain()
        {
            //Todo: validation
            if (_url != null)
            {
                string host = new Uri(_url).Host;
                return host.Substring(host.LastIndexOf('.', host.LastIndexOf('.') - 1) + 1);
            }
            else
            {
                return "";
            }
        }

        public void SetDetails(string username, string password, string url, string notes)
        {
            Vault.DecrementConflictCount(_userName, _password); //decrement conflict count from the old username

            _userName = username;
            _password = password;
            _url = url;
            _notes = notes;

            Vault.IncrementConflictCount(username, password); //increment conflict count for the new username

            FormvIDsafe.Main.User.SaveVault();
        }

        public void SetStatus(CredentialStatus status)
        {
            _status = status;

            FormvIDsafe.Main.User.SaveVault();
        }
    }
}
