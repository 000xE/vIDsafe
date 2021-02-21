using System.Security.Cryptography;
using System.Text;

namespace vIDsafe
{
    public class Hashing
    {
        //HASHING VARIABLES
        protected const int KeySize = 256;
        private const int _hashSize = KeySize / 8;
        private const int _hashIterations = 100000; //Work factor, higher = longer

        public enum KeyDerivationFunction
        {
            PBKDF2
        }

        /// <summary>
        /// Derives a salted key from a secret
        /// </summary>
        /// <returns>
        /// The derived key
        /// </returns>
        protected static byte[] DeriveKey(KeyDerivationFunction function, string secret, byte[] salt)
        {
            byte[] derivedKey = null;

            switch (function)
            {
                case KeyDerivationFunction.PBKDF2:
                    derivedKey = PBKDF2(secret, salt);
                    break;
            }

            return derivedKey;
        }

        /// <summary>
        /// Derives a salted key from a secret using PBKDF2
        /// </summary>
        /// <returns>
        /// The derived key
        /// </returns>
        private static byte[] PBKDF2(string secret, byte[] salt)
        {
            byte[] derivedKey = null;

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(secret, salt, _hashIterations))
            {
                derivedKey = pbkdf2.GetBytes(_hashSize);
            }

            return derivedKey;
        }
    }
}