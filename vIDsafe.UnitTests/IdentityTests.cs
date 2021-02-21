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
    }
}