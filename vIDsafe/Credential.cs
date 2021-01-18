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

        public void SetStatus(CredentialStatus credentialStatus)
        {
            _status = credentialStatus;
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

            StringBuilder username = new StringBuilder("");

            char[] lowers = deriveName.ToLower().ToCharArray();
            char[] uppers = deriveName.ToUpper().ToCharArray();

            char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            int l = lowers.Length;
            int u = uppers.Length;
            int n = numbers.Length;

            freakcode.Cryptography.CryptoRandom cryptoRandom = new freakcode.Cryptography.CryptoRandom();

            int nextCharacterChoice = cryptoRandom.Next(0, 3);

            //Console.WriteLine(cryptoRandom.Next(0, 1));

            while (0 < length--)
            {
                char randomChar = '0';

                switch (nextCharacterChoice)
                {
                    case 0:
                        randomChar = lowers[cryptoRandom.Next(0, l)];
                        nextCharacterChoice = 1;
                        break;
                    case 1:
                        randomChar = uppers[cryptoRandom.Next(0, u)];
                        nextCharacterChoice = 2;
                        break;
                    case 2:
                        randomChar = numbers[cryptoRandom.Next(0, n)];
                        nextCharacterChoice = 0;
                        break;
                }

                username.Append(randomChar);

                //TODO: ensure length is bigger than list count, and maybe add min chars for each
            }

            Console.WriteLine(username);

            Encryption.SecurelyRandomizeArray(username);

            Console.WriteLine(username);

            return username.ToString();
        }

        public static string GeneratePassword(bool passPhrase, int length)
        {
            StringBuilder password = new StringBuilder("");

            freakcode.Cryptography.CryptoRandom cryptoRandom = new freakcode.Cryptography.CryptoRandom();

            if (!passPhrase)
            {
                char[] lowers = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                char[] uppers = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                char[] numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                char[] special = new char[] { '!', '$', '@', '^', '%', '&', '#', '*' };

                int l = lowers.Length;
                int u = uppers.Length;
                int n = numbers.Length;
                int s = special.Length;

                int nextCharacterChoice = cryptoRandom.Next(0, 4);

                while (0 < length--)
                {
                    char randomChar = '0';

                    switch (nextCharacterChoice)
                    {
                        case 0:
                            randomChar = lowers[cryptoRandom.Next(0, l)];
                            nextCharacterChoice = 1;
                            break;
                        case 1:
                            randomChar = uppers[cryptoRandom.Next(0, u)];
                            nextCharacterChoice = 2;
                            break;
                        case 2:
                            randomChar = numbers[cryptoRandom.Next(0, n)];
                            nextCharacterChoice = 3;
                            break;
                        case 3:
                            randomChar = numbers[cryptoRandom.Next(0, s)];
                            nextCharacterChoice = 0;
                            break;
                    }

                    password.Append(randomChar);
                }
            }
            else
            {
                int wordListLength = WordList.EEFLongWordList.Length;

                string[] passWordList = new string[length];

                string wordSeparator = "-";

                while (0 < length--)
                {
                    string randomWord = WordList.EEFLongWordList[cryptoRandom.Next(0, wordListLength)];

                    passWordList[length] = randomWord;
                }

                password.Append(string.Join(wordSeparator, passWordList));
            }

            Encryption.SecurelyRandomizeArray(password);

            return password.ToString();
        }
    }
}
