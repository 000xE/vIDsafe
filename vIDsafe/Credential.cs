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

        //TODO: make enum for safe/weak/compromised etc credentials and set to a variable here and retrieve for health score calculation
        //Compromised = top prio, conflict 2nd top, etc. upon recalculation the next prio should be set

        public enum CredentialStatus
        {
            Safe,
            Weak,
            Conflicted,
            Compromised
        }

        public Credential(string username)
        {
            this._userName = username;
        }

        public string Username => _userName;

        public string Password => _password;

        public string URL => _url;

        public string Notes => _notes;

        public CredentialStatus Status => _status;

        public void SetDetails(string username, string password, string url, string notes)
        {
            this._userName = username;
            this._password = password;
            this._url = url;
            this._notes = notes;

            FormvIDsafe.Main.User.SaveVault();
        }

        private void CheckBreach()
        {

        }

        private void CheckStrength()
        {

        }

        public static string GenerateUsername(string deriveName, int length)
        {
            deriveName = deriveName.Replace(" ", "");

            string username = "";

            char[] lowers = deriveName.ToLower().ToCharArray();
            char[] uppers = deriveName.ToUpper().ToCharArray();

            //char[] lowers = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            //char[] uppers = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            int l = lowers.Length;
            int u = uppers.Length;
            int n = numbers.Length;

            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int randomCharacterChoice = random.Next(0, 3);

                char randomChar = '0';

                switch (randomCharacterChoice)
                {
                    case 0:
                        randomChar = lowers[random.Next(0, l)];
                        break;
                    case 1:
                        randomChar = uppers[random.Next(0, u)];
                        break;
                    case 2:
                        randomChar = numbers[random.Next(0, n)];
                        break;
                }

                username += randomChar;
            }

            return username;
        }

        public static string GeneratePassword(bool passPhrase)
        {
            string password = "";

            return password;
        }
    }
}
