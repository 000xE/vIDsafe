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
            Hashing.KeyDerivationFunction function = Hashing.KeyDerivationFunction.PBKDF2;
            string secret = "This is a secret";
            string salt = "TestSalt123";

            //Act
            string hashed = Encryption.HashPassword(function, secret, salt);

            //Asert
            string expected = "izqZNy0kuStl/VVrWwJ8sGrdgQVqycxKQGZcXQXWXQg=";

            Assert.AreEqual(expected, hashed);
        }

        [TestMethod()]
        public void AesEncryptTest()
        {
            //Arrange
            Hashing.KeyDerivationFunction function = Hashing.KeyDerivationFunction.PBKDF2;
            string secret = "Password";
            string salt = "TestUsername123";

            string key = Encryption.HashPassword(function, secret, salt);

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
            Hashing.KeyDerivationFunction function = Hashing.KeyDerivationFunction.PBKDF2;
            string secret = "Password";
            string salt = "TestUsername123";

            string key = Encryption.HashPassword(function, secret, salt);

            string toDecrypt = "/cme8tTlgqA5VBoHvxe0tynlUwNsE8CCwA8RQycIWAY=";

            //Act
            string decrypted = Encryption.AesDecrypt(toDecrypt, key);

            //Assert
            string expected = "Test";

            Assert.AreEqual(expected, decrypted);
        }
    }
}