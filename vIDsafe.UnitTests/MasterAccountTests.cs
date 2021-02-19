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
    public class MasterAccountTests
    {
        [TestMethod()]
        public void TryImportVaultTest()
        {
            //Arrange
            MasterAccount user = new MasterAccount("TestAccountName", "TestAccountPassword");
            user.TryRegister();
            Vault vault = user.Vault;

            Identity identity = vault.GenerateIdentity();
            identity.GenerateCredential();

            //CSV files
            //Act
            user.TryExportVault(MasterAccount.VaultFormat.CSV, "", "ImportTestData/testvault.csv");

            vault.DeleteAllCredentials();
            user.TryImportVault(MasterAccount.VaultFormat.CSV, "ImportTestData/testvault.csv", false);

            vault.CalculateOverallHealthScore(false);

            //Assert
            Assert.AreEqual(1, vault.TotalCredentialCount);

            //Arrange
            vault.DeleteAllCredentials();
            identity.GenerateCredential();

            //JSON files
            //Act
            user.TryExportVault(MasterAccount.VaultFormat.JSON, "", "ImportTestData/testvault.json");

            vault.DeleteAllCredentials();
            user.TryImportVault(MasterAccount.VaultFormat.JSON, "ImportTestData/testvault.json", false);

            vault.CalculateOverallHealthScore(false);

            //Assert
            Assert.AreEqual(1, vault.TotalCredentialCount);

            //Arrange
            vault.DeleteAllCredentials();
            identity.GenerateCredential();

            //Encrypted files
            //Act
            user.TryExportVault(MasterAccount.VaultFormat.Encrypted, "", "ImportTestData/testvault");

            vault.DeleteAllCredentials();
            user.TryImportVault(MasterAccount.VaultFormat.Encrypted, "ImportTestData/testvault", false);

            vault.CalculateOverallHealthScore(false);

            //Assert
            Assert.AreEqual(1, vault.TotalCredentialCount);

            user.DeleteAccount();
        }

        [TestMethod()]
        public void TryExportVaultTest()
        {
            //Arrange
            MasterAccount user = new MasterAccount("TestAccountName", "TestAccountPassword");
            user.TryRegister();
            Vault vault = user.Vault;

            Identity identity = vault.GenerateIdentity();
            identity.GenerateCredential();

            //CSV files
            //Act
            bool exportedCSV = user.TryExportVault(MasterAccount.VaultFormat.CSV, "", "ExportTestData/testvault.csv");

            //Assert
            Assert.IsTrue(exportedCSV);

            //JSON files
            //Act
            bool exportedJSON = user.TryExportVault(MasterAccount.VaultFormat.CSV, "", "ExportTestData/testvault.json");

            //Assert
            Assert.IsTrue(exportedJSON);

            //Encrypted files
            //Act
            bool exportedEncrypted = user.TryExportVault(MasterAccount.VaultFormat.CSV, "", "ExportTestData/testvault");

            //Assert
            Assert.IsTrue(exportedEncrypted);

            user.DeleteAccount();
        }
    }
}