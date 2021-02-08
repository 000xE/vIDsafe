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
            Assert.Fail();
        }

        [TestMethod()]
        public void AesDecryptTest()
        {
            Assert.Fail();
        }
    }
}