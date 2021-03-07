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
    public class IdentityTests
    {
        [TestMethod()]
        public void CalculateHealthScoreTest()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity = vault.GenerateIdentity();

            //Act
            identity.GenerateCredential();
            identity.GenerateCredential().Status = Status.CredentialStatus.Weak;

            identity.CalculateHealthScore(false);

            //Assert
            int expectedHealthScore = 50;

            Assert.AreEqual(expectedHealthScore, identity.HealthScore);
        }

        [TestMethod()]
        public void DeleteAllCredentialsTest()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity = vault.GenerateIdentity();

            //Act
            vault.DeleteAllCredentials();

            //Assert
            int expectedCredentialCount = 0;

            Assert.AreEqual(expectedCredentialCount, identity.Credentials.Count);
        }

        [TestMethod()]
        public void TryGetCredentialTest()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity = vault.GenerateIdentity();
            Credential credential = identity.GenerateCredential();
            string credentialID = credential.CredentialID;

            //Act
            Credential retrievedCredential = identity.TryGetCredential(credentialID);

            //Assert
            Assert.AreEqual(credential, retrievedCredential);
        }

        [TestMethod()]
        public void TryDeleteCredentialTest()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity = vault.GenerateIdentity();
            Credential credential = identity.GenerateCredential();
            string credentialID = credential.CredentialID;

            //Act
            bool deleted = identity.TryDeleteCredential(credentialID);

            //Assert
            Assert.IsTrue(deleted);
        }
    }
}