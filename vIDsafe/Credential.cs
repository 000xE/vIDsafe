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

        //TODO: make enum for safe/weak/compromised etc credentials and set to a variable here and retrieve for health score calculation
        //Compromised = top prio, conflict 2nd top, etc. upon recalculation the next prio should be set

        public Credential(string username)
        {
            this._userName = username;
        }

        public string Username => _userName;

        public string Password => _password;

        public string URL => _url;

        public string Notes => _notes;

        public void SetDetails(string username, string password, string url, string notes)
        {
            this._userName = username;
            this._password = password;
            this._url = url;
            this._notes = notes;

            vIDsafe.Main.User.SaveVault();
        }

        private void CheckBreach()
        {

        }

        private void CheckStrength()
        {

        }
    }
}
