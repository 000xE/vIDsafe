﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            identity2.GenerateCredential().Status = Credential.CredentialStatus.Weak;
            identity2.CalculateHealthScore(false);

            //Act
            vault.CalculateOverallHealthScore(false);

            //Assert
            int expectedHealthScore = 50;

            Assert.AreEqual(expectedHealthScore, vault.OverallHealthScore);
        }

        [TestMethod()]
        public void TryChangeIdentityEmailTest()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity = vault.GenerateIdentity();

            string newEmail = "test123456@test.com";

            //Act
            vault.TryChangeIdentityEmail(identity.Email, newEmail);

            //Assert
            Assert.IsTrue(vault.Identities.ContainsKey(newEmail));
        }

        [TestMethod()]
        public void DeleteIdentityTest()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity = vault.GenerateIdentity();

            //Act
            vault.DeleteIdentity(identity.Email);

            //Assert
            Assert.IsFalse(vault.Identities.ContainsKey(identity.Email));
        }

        [TestMethod()]
        public void DeleteAllCredentialsTest()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity = vault.GenerateIdentity();

            identity.GenerateCredential();
            identity.GenerateCredential();

            //Act
            vault.DeleteAllCredentials();

            //Assert
            int expectedCredentialCount = 0;

            Assert.AreEqual(expectedCredentialCount, vault.TotalCredentialCount);
        }

        [TestMethod()]
        public void DeleteAllIdentitiesTest()
        {
            //Arrange
            Vault vault = new Vault();

            vault.GenerateIdentity();
            vault.GenerateIdentity();

            //Act
            vault.DeleteAllIdentities();

            //Assert
            int expectedIdentityCount = 0;

            Assert.AreEqual(expectedIdentityCount, vault.Identities.Count);
        }
    }
}