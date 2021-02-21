using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace vIDsafe
{
    public class Encryption : Hashing
    {
        //AES VARIABLES
        private const int _blockSize = 128;
        private const int _ivSize = _blockSize / 8;

        /// <summary>
        /// Hashes the password
        /// </summary>
        /// <returns>
        /// The hashed password if it's hashed, null if not
        /// </returns>
        public static string HashPassword(KeyDerivationFunction function, string password, string salt)
        {
            byte[] convertedSalt = Encoding.ASCII.GetBytes(salt);

            string hashedPassword = Convert.ToBase64String(DeriveKey(function, password, convertedSalt));

            return hashedPassword;
        }

        /// <summary>
        /// Encrypts plaintext using a key with AES256 CBC
        /// </summary>
        /// <returns>
        /// The encrypted text
        /// </returns>
        public static string AesEncrypt(string plainText, byte[] key)
        {
            byte[] textBytes = Encoding.ASCII.GetBytes(plainText);

            using (AesCryptoServiceProvider AES = GetAES(key))
            {
                using (var encryptor = AES.CreateEncryptor(AES.Key, AES.IV))
                {
                    //https://stackoverflow.com/q/8041451
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(AES.IV, 0, _ivSize);

                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(textBytes, 0, textBytes.Length);
                            cs.FlushFinalBlock();
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts ciphertext using a key with AES256 CBC
        /// </summary>
        /// <returns>
        /// The decrypted text
        /// </returns>
        public static string AesDecrypt(string encryptedText, byte[] key)
        {
            byte[] textBytes = Convert.FromBase64String(encryptedText);

            using (AesCryptoServiceProvider AES = GetAES(key))
            {
                try
                {
                    //https://stackoverflow.com/q/8041451
                    using (var ms = new MemoryStream(textBytes))
                    {
                        byte[] buffer = new byte[_ivSize];
                        ms.Read(buffer, 0, _ivSize);
                        AES.IV = buffer;

                        using (var decryptor = AES.CreateDecryptor(AES.Key, AES.IV))
                        {
                            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] decrypted = new byte[textBytes.Length];
                                var byteCount = cs.Read(decrypted, 0, textBytes.Length);

                                return Encoding.UTF8.GetString(decrypted, 0, byteCount);
                            }
                        }
                    }
                }
                catch (CryptographicException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the AES crypto service provider for encryption/decryption
        /// </summary>
        /// <returns>
        /// The AES crpto service provider
        /// </returns>
        private static AesCryptoServiceProvider GetAES (byte [] key)
        {
            AesCryptoServiceProvider AES = new AesCryptoServiceProvider
            {
                BlockSize = _blockSize,
                KeySize = KeySize,
                Key = key,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            return AES;
        }
    }
}
