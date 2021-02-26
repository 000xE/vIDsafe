using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace vIDsafe
{
    public static class CredentialGeneration
    {
        public static readonly int MinPasswordLength = 5;
        public static readonly int MaxPasswordLength = 128;

        public static readonly int MinPassphraseLength = 3;
        public static readonly int MaxPassphraseLength = 20;

        private const int _defaultUsernameLength = 12;
        private const int _defaultPasswordLength = 12;
        private const int _defaultPassphraseLength = 3;

        public static int CurrentUsernameLength = _defaultUsernameLength;
        public static int CurrentPasswordLength = _defaultPasswordLength;
        public static int CurrentPassphraseLength = _defaultPassphraseLength;

        public static bool Passphrase = false;

        private static readonly char[] _lowerAZ = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static readonly char[] _upperAZ = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static readonly char[] _numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};
        private static readonly char[] _special = { '!', '$', '@', '^', '%', '#', '*' };

        private static readonly Dictionary<int, char[]> _usableCharacters = new Dictionary<int, char[]>
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

        /// <summary>
        /// Generates a username based on a derived name
        /// </summary>
        /// <returns>
        /// The username
        /// </returns>
        public static string GenerateUsername(string deriveName)
        {
            deriveName = deriveName.Replace(" ", "");

            StringBuilder username = new StringBuilder("");

            char[] lowerUsername = deriveName.ToLower(CultureInfo.InvariantCulture).ToCharArray();
            char[] upperUsername = deriveName.ToUpper(CultureInfo.InvariantCulture).ToCharArray();

            List<char[]> characters = new List<char[]>
            {
                lowerUsername,
                upperUsername,
                _numbers
            };

            if (CurrentUsernameLength > characters.Count)
            {
                AppendRandomCharacters(characters, CurrentUsernameLength, username);
            }

            SecurelyRandomizeStringBuilder(username);

            return username.ToString();
        }

        /// <summary>
        /// Appends random characters of a chosen list over iterations onto a string builder
        /// </summary>
        private static void AppendRandomCharacters(List<char[]> characters, int iterations, StringBuilder stringBuilder)
        {
            freakcode.Cryptography.CryptoRandom cryptoRandom = new freakcode.Cryptography.CryptoRandom();

            int nextCharacterChoice = cryptoRandom.Next(0, characters.Count);

            for (int i = 0; i < iterations; i++)
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

                stringBuilder.Append(randomChar);
            }

            SecurelyRandomizeStringBuilder(stringBuilder);
        }

        /// <summary>
        /// Randomises a string builder securely using CryptoRandom
        /// </summary>
        //https://stackoverflow.com/a/12646864
        private static void SecurelyRandomizeStringBuilder(StringBuilder sb)
        {
            freakcode.Cryptography.CryptoRandom cryptoRandom = new freakcode.Cryptography.CryptoRandom();

            for (int i = sb.Length - 1; i > 0; i--)
            {
                int randomIndex = cryptoRandom.Next(0, i);
                char temp = sb[i];
                sb[i] = sb[randomIndex];
                sb[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Generates a password
        /// </summary>
        /// <returns>
        /// The password
        /// </returns>
        public static string GeneratePassword()
        {
            StringBuilder password = new StringBuilder("");

            if (!Passphrase)
            {
                List<char[]> characters = new List<char[]>();

                foreach (KeyValuePair<int, bool> setting in PasswordSettings)
                {
                    if (setting.Value)
                    {
                        characters.Add(_usableCharacters[setting.Key]);
                    }
                }

                AppendRandomCharacters(characters, CurrentPasswordLength, password);
            }
            else
            {
                freakcode.Cryptography.CryptoRandom cryptoRandom = new freakcode.Cryptography.CryptoRandom();

                int wordListLength = WordList.EEFLongWordList.Length;

                string wordSeparator = "-";

                for (int i = 0; i < CurrentPassphraseLength; i++)
                {
                    string randomWord = WordList.EEFLongWordList[cryptoRandom.Next(0, wordListLength)];

                    if (password.Length.Equals(0))
                    {
                        password.Append(randomWord);
                    }
                    else
                    {
                        password.Append(wordSeparator + randomWord);
                    }
                }
            }

            return password.ToString();
        }

        /// <summary>
        /// Checks the strength of a password
        /// </summary>
        /// <returns>
        /// The percentage of the strength of the password
        /// </returns>
        public static double CheckStrength(string password)
        {
            double score = password.Length;

            double regexMultiplier = 10;

            string[] regexPatterns =
            {
                    @"\d+",
                    @"[a-z]",
                    @"[A-Z]",
                    @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]",
            };

            int bestPasswordLength = MaxPasswordLength;

            double maxScore = bestPasswordLength + (regexPatterns.Length * regexMultiplier);

            foreach (string pattern in regexPatterns)
            {
                if (Regex.Match(password, pattern, RegexOptions.ECMAScript).Success)
                {
                    score += regexMultiplier;
                }
            }

            double percent = (score / maxScore) * 100;

            if (percent > 100)
            {
                percent = 100;
            }

            return percent;
        }
    }
}
