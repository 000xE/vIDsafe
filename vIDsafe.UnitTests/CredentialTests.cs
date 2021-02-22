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
    public class CredentialTests
    {
        [TestMethod()]
        public void CalculateStatus()
        {
            //Arrange
            Vault vault = new Vault();
            Identity identity = vault.GenerateIdentity();

            //Act
            Credential credential1 = identity.GenerateCredential();
            Credential credential2 = identity.GenerateCredential();

            credential2.Username = credential2.Username;
            credential2.Password = credential1.Password;
            credential2.URL = credential2.URL;
            credential2.Notes = credential2.Notes;

            //Assert
            Status.CredentialStatus expectedStatus = Status.CredentialStatus.Conflicted;
            credential2.CalculateStatus();

            Assert.AreEqual(expectedStatus, credential2.Status);
        }
    }
}