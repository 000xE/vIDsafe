using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace vIDsafe
{
    class Encryption
    {
        private const int HASH_ITERATIONS = 100000; //Work factor, higher = longer
        private const int blockSize = 128;
        private const int keySize = 256;

        private const int ivSize = blockSize / 8;
        private const int hashSize = keySize / 8;

        public static byte[] hashPassword(string newPassword, string salt)
        {
            //a new password hash is generated from a generated salt with the passed settings
            //https://shawnmclean.com/simplecrypto-net-a-pbkdf2-hashing-wrapper-for-net-framework/
            //return cryptoService.Compute(newPassword, HASH_ITERATIONS + "." + salt);

            byte[] convertedSalt = ASCIIEncoding.ASCII.GetBytes(salt);

            // Generate the hash
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(newPassword, convertedSalt, HASH_ITERATIONS);
            return pbkdf2.GetBytes(hashSize);
        }


        public static string aesEncrypt(string plainText, byte[] key)
        {
            byte[] textBytes = ASCIIEncoding.ASCII.GetBytes(plainText);

            using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
            {
                AES.BlockSize = blockSize;
                AES.KeySize = keySize;
                AES.Key = key;
                AES.Padding = PaddingMode.PKCS7;
                AES.Mode = CipherMode.CBC;

                //https://stackoverflow.com/q/8041451
                using (var encryptor = AES.CreateEncryptor(AES.Key, AES.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(AES.IV, 0, ivSize);
                        using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            cs.Write(textBytes, 0, textBytes.Length);
                            cs.FlushFinalBlock();
                        }

                        Console.WriteLine(Convert.ToBase64String(AES.IV));
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            };
        }


        public static string aesDecrypt(string encryptedText, byte[] key)
        {
            byte[] textBytes = Convert.FromBase64String(encryptedText);
            using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
            {
                AES.BlockSize = blockSize;
                AES.KeySize = keySize;
                AES.Key = key;
                AES.Padding = PaddingMode.PKCS7;
                AES.Mode = CipherMode.CBC;

                //https://stackoverflow.com/q/8041451
                using (var ms = new MemoryStream(textBytes))
                {
                    byte[] buffer = new byte[ivSize];
                    ms.Read(buffer, 0, ivSize);
                    AES.IV = buffer;
                    try
                    {
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
                    catch (CryptographicException)
                    {
                        return null;
                    }
                }
            };
        }
    }
}
