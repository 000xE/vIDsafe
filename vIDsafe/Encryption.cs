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
        //AES VARIABLES
        private const int _blockSize = 128;
        private const int _keySize = 256;

        private const int _ivSize = _blockSize / 8;
        private const int _hashSize = _keySize / 8;

        public enum KeyDerivationFunction
        {
            PBKDF2
        }

        public static byte[] DeriveKey(string secret, string salt, KeyDerivationFunction keyDerivationFunction)
        {
            //a new password hash is generated from a generated salt with the passed settings
            //https://shawnmclean.com/simplecrypto-net-a-pbkdf2-hashing-wrapper-for-net-framework/
            //return cryptoService.Compute(newPassword, HASH_ITERATIONS + "." + salt);

            byte[] convertedSalt = Encoding.ASCII.GetBytes(salt);

            byte[] derivedKey = null;

            switch (keyDerivationFunction)
            {
                case KeyDerivationFunction.PBKDF2:
                    int _hashIterations = 100000; //Work factor, higher = longer
                    using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(secret, convertedSalt, _hashIterations))
                    {
                        derivedKey = pbkdf2.GetBytes(_hashSize);
                    }
                    break;
            }

            return derivedKey;
        }

        public static string AesEncrypt(string plainText, byte[] key)
        {
            byte[] textBytes = Encoding.ASCII.GetBytes(plainText);


            //https://stackoverflow.com/q/8041451

            using (AesCryptoServiceProvider AES = GetAES(key))
            {
                using (var encryptor = AES.CreateEncryptor(AES.Key, AES.IV))
                {
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(AES.IV, 0, _ivSize);

                        AES.Dispose();

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

        public static string AesDecrypt(string encryptedText, byte[] key)
        {
            byte[] textBytes = Convert.FromBase64String(encryptedText);

            using (AesCryptoServiceProvider AES = GetAES(key))
            {
                //https://stackoverflow.com/q/8041451
                try
                {
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
        private static AesCryptoServiceProvider GetAES (byte [] key)
        {
            AesCryptoServiceProvider AES = new AesCryptoServiceProvider
            {
                BlockSize = _blockSize,
                KeySize = _keySize,
                Key = key,
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC
            };

            return AES;
        }

        //https://stackoverflow.com/a/12646864
        public static void SecurelyRandomizeArray(StringBuilder sb)
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
    }
}
