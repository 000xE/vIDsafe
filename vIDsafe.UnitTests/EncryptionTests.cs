using Microsoft.VisualStudio.TestTools.UnitTesting;
using vIDsafe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vIDsafe.Tests
{
    [TestClass()]
    public class EncryptionTests
    {
        [TestMethod()]
        public void DeriveKeyTest()
        {
            //Arrange
            Encryption.KeyDerivationFunction keyDerivationFunction = Encryption.KeyDerivationFunction.PBKDF2;
            string secret = "This is a secret";
            string salt = "TestSalt123";

            //Act
            byte[] key = Encryption.DeriveKey(keyDerivationFunction, secret, salt);

            string hashed = Convert.ToBase64String(key);
            Console.WriteLine(hashed);

            //Asert
            string expected = "izqZNy0kuStl/VVrWwJ8sGrdgQVqycxKQGZcXQXWXQg=";

            Assert.AreEqual(expected, hashed);
        }

        [TestMethod()]
        public void AesEncryptTest()
        {
            //Arrange
            Encryption.KeyDerivationFunction keyDerivationFunction = Encryption.KeyDerivationFunction.PBKDF2;
            string secret = "Password";
            string salt = "TestUsername123";

            byte[] key = Encryption.DeriveKey(keyDerivationFunction, secret, salt);

            string toEncrypt = "Test";

            //Act
            string encrypted = Encryption.AesEncrypt(toEncrypt, key);

            //Assert
            string decrypted = Encryption.AesDecrypt(encrypted, key);
            Assert.AreEqual(toEncrypt, decrypted);
        }

        [TestMethod()]
        public void AesDecryptTest()
        {
            //Arrange
            Encryption.KeyDerivationFunction keyDerivationFunction = Encryption.KeyDerivationFunction.PBKDF2;
            string secret = "Password";
            string salt = "TestUsername123";

            byte[] key = Encryption.DeriveKey(keyDerivationFunction, secret, salt);

            string toDecrypt = "/cme8tTlgqA5VBoHvxe0tynlUwNsE8CCwA8RQycIWAY=";

            //Act
            string decrypted = Encryption.AesDecrypt(toDecrypt, key);

            //Assert
            string expected = "Test";

            Assert.AreEqual(expected, decrypted);
        }
    }
}