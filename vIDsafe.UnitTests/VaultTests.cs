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
    public class VaultTests
    {
        [TestMethod()]
        public void CalculateOverallHealthScoreTest()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity1 = vault.GenerateIdentity();

            identity1.GenerateCredential();
            identity1.CalculateHealthScore(false);

            Identity identity2 = vault.GenerateIdentity();

            identity2.GenerateCredential().SetStatus(Credential.CredentialStatus.Weak);
            identity2.CalculateHealthScore(false);

            //Act
            vault.CalculateOverallHealthScore(false);

            //Assert
            int expectedHealthScore = 50;

            Assert.AreEqual(expectedHealthScore, vault.OverallHealthScore);
        }
    }
}