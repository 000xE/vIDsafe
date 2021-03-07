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

            identity.BreachedDomains.Add("google.com", "TestDate");

            Credential credential1 = identity.GenerateCredential();
            Credential credential2 = identity.GenerateCredential();
            Credential credential3 = identity.GenerateCredential();
            Credential credential4 = identity.GenerateCredential();

            //Act
            credential1.URL = "https://www.google.com";

            //Assert
            Status.CredentialStatus expectedStatus = Status.CredentialStatus.Compromised;
            credential1.CalculateStatus();

            Assert.AreEqual(expectedStatus, credential1.Status);

            //Act
            credential2.Password = credential1.Password;

            //Assert
            expectedStatus = Status.CredentialStatus.Conflicted;
            credential2.CalculateStatus();

            Assert.AreEqual(expectedStatus, credential2.Status);

            //Act
            credential3.Password = "12345";

            //Assert
            expectedStatus = Status.CredentialStatus.Weak;
            credential3.CalculateStatus();

            Assert.AreEqual(expectedStatus, credential3.Status);

            //Act
            credential4.Password = "This-Is-A-Very-Very-Strong-Password";

            //Assert
            expectedStatus = Status.CredentialStatus.Safe;
            credential4.CalculateStatus();

            Assert.AreEqual(expectedStatus, credential4.Status);
        }
    }
}