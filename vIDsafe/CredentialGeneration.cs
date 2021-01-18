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

        public static string GenerateUsername(string deriveName)
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

            for (int i = 0; i < UsernameLength; i ++)
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

            Encryption.SecurelyRandomizeArray(username);

            return username.ToString();
        }

        public static string GeneratePassword()
        {
            StringBuilder password = new StringBuilder("");

            freakcode.Cryptography.CryptoRandom cryptoRandom = new freakcode.Cryptography.CryptoRandom();

            if (!PassPhrase)
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

                for (int i = 0; i < PasswordLength; i++)
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


                Encryption.SecurelyRandomizeArray(password);
            }
            else
            {
                int wordListLength = WordList.EEFLongWordList.Length;

                string[] passWordList = new string[PasswordLength];

                string wordSeparator = "-";

                for (int i = 0; i < PasswordLength; i++)
                {
                    string randomWord = WordList.EEFLongWordList[cryptoRandom.Next(0, wordListLength)];

                    passWordList[i] = randomWord;
                }

                password.Append(string.Join(wordSeparator, passWordList));
            }

            return password.ToString();
        }
    }
}
