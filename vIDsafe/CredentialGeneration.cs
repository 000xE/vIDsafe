using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe
{
    class CredentialGeneration
    {
        public static int UsernameLength = 10;
        public static int PasswordLength = 10;

        public static bool PassPhrase = false;

        private static readonly char[] _lowerAZ = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static readonly char[] _upperAZ = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static readonly char[] _numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        private static readonly char[] _special = new char[] { '!', '$', '@', '^', '%', '#', '*' };

        private static readonly Dictionary<int, char[]> _usableCharacters = new Dictionary<int, char[]>()
        {
            [0] = _lowerAZ,
            [1] = _numbers,
            [2] = _upperAZ,
            [3] = _special
        };

        public static Dictionary<int, bool> PasswordSettings = new Dictionary<int, bool>
        {
            [0] = true,
            [1] = true,
            [2] = true,
            [3] = true
        };

        public static string GenerateUsername(string deriveName)
        {
            deriveName = deriveName.Replace(" ", "");

            StringBuilder username = new StringBuilder("");

            char[] lowerUsername = deriveName.ToLower().ToCharArray();
            char[] upperUsername = deriveName.ToUpper().ToCharArray();

            freakcode.Cryptography.CryptoRandom cryptoRandom = new freakcode.Cryptography.CryptoRandom();

            List<char[]> characters = new List<char[]>
            {
                lowerUsername,
                upperUsername,
                _numbers
            };

            int nextCharacterChoice = cryptoRandom.Next(0, characters.Count);

            for (int i = 0; i < UsernameLength; i++)
            {
                int length = characters[nextCharacterChoice].Length;

                char randomChar = characters[nextCharacterChoice][cryptoRandom.Next(0, length)];

                if (nextCharacterChoice + 1 < characters.Count)
                {
                    nextCharacterChoice++;
                }
                else
                {
                    nextCharacterChoice = 0;
                }

                username.Append(randomChar);

                //TODO: ensure length is bigger than list count, and maybe add min chars for each
            }

            Encryption.SecurelyRandomizeArray(username);

            return username.ToString();
        }

        public static string GeneratePassword()
        {
            StringBuilder password = new StringBuilder("");

            freakcode.Cryptography.CryptoRandom cryptoRandom = new freakcode.Cryptography.CryptoRandom();

            if (!PassPhrase)
            {
                List<char[]> characters = new List<char[]>();

                foreach (KeyValuePair<int, bool> setting in PasswordSettings)
                {
                    if (setting.Value)
                    {
                        characters.Add(_usableCharacters[setting.Key]);
                    }
                }

                int nextCharacterChoice = cryptoRandom.Next(0, characters.Count);

                for (int i = 0; i < PasswordLength; i++)
                {
                    int length = characters[nextCharacterChoice].Length;

                    char randomChar = characters[nextCharacterChoice][cryptoRandom.Next(0, length)];

                    if (nextCharacterChoice+1 < characters.Count)
                    {
                        nextCharacterChoice++;
                    }
                    else
                    {
                        nextCharacterChoice = 0;
                    }

                    password.Append(randomChar);
                }

                Encryption.SecurelyRandomizeArray(password);
            }
            else
            {
                int wordListLength = WordList.EEFLongWordList.Length;

                string wordSeparator = "-";

                for (int i = 0; i < PasswordLength; i++)
                {
                    string randomWord = WordList.EEFLongWordList[cryptoRandom.Next(0, wordListLength)];

                    if (password.Length == 0)
                    {
                        password.Append(randomWord);
                    }
                    else
                    {
                        password.Append(wordSeparator + randomWord);
                    }
                }
            }

            FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Passwords, password.ToString());

            return password.ToString();
        }
    }
}
