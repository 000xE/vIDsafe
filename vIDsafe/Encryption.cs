using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace vIDsafe
{
    class Encryption
    {

        public static ICryptoService cryptoService = new PBKDF2();
        private const int HASH_ITERATIONS = 100000; //Work factor, higher = longer

        private static string IV = "fu1$c!j2d8limk6x";

        private const int HASH_SIZE = 32;

        //https://shawnmclean.com/simplecrypto-net-a-pbkdf2-hashing-wrapper-for-net-framework/
        public static byte[] hashPassword(string newPassword, string salt)
        {
            //a new password hash is generated from a generated salt with the passed settings
            //return cryptoService.Compute(newPassword, HASH_ITERATIONS + "." + salt);

            byte[] convertedSalt = ASCIIEncoding.ASCII.GetBytes(salt);

            // Generate the hash
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(newPassword, convertedSalt, HASH_ITERATIONS);
            return pbkdf2.GetBytes(HASH_SIZE);
        }

        //https://shawnmclean.com/simplecrypto-net-a-pbkdf2-hashing-wrapper-for-net-framework/
        public static bool validatePassword(string password, string hashedPassword, string salt)
        {
            //hash the password with the saved salt for that user
            string hashed = cryptoService.Compute(password, salt);
            //return true if both hashes are the same
            return hashed == hashedPassword;
        }

        public static string aesEncrypt(string plainText, byte[] key)
        {
            byte[] textBytes = ASCIIEncoding.ASCII.GetBytes(plainText);

            AesCryptoServiceProvider AES = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = key,
                IV = ASCIIEncoding.ASCII.GetBytes(IV),
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            ICryptoTransform Encryptor = AES.CreateEncryptor(AES.Key, AES.IV);

            byte[] encryptedText = Encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
            Encryptor.Dispose();

            return Convert.ToBase64String(encryptedText);
        }


        public static string aesDecrypt(string encryptedText, byte[] key)
        {
            byte[] textBytes = Convert.FromBase64String(encryptedText);
            AesCryptoServiceProvider AES = new AesCryptoServiceProvider
            {
                BlockSize = 128,
                KeySize = 256,
                Key = key,
                IV = ASCIIEncoding.ASCII.GetBytes(IV),
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            ICryptoTransform Encryptor = AES.CreateDecryptor(AES.Key, AES.IV);
            try
            {
                byte[] decryptedText = Encryptor.TransformFinalBlock(textBytes, 0, textBytes.Length);
                Encryptor.Dispose();

                return ASCIIEncoding.ASCII.GetString(decryptedText);
            }
            catch (CryptographicException)
            {
                return null;
            }
        }
    }
}
