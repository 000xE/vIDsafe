using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormImportExport : Form
    {
        public FormImportExport()
        {
            InitializeComponent();

            LoadFormComponents();
        }

        private void LoadFormComponents()
        {
            GetIdentities();
        }

        private void GetIdentities()
        {
            cmbIdentity.Items.Clear();

            foreach (Identity identity in FormvIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identity.Name + " - " + identity.Email);
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            string extension = "All files (*.*)|*.*";

            int formatIndex = cmbImportFormat.SelectedIndex;

            switch (formatIndex)
            {
                case 0:
                    extension = "CSV files (*.csv)|*.csv";
                    break;
                case 1:
                    extension = "JSON files (*.json)|*.json";
                    break;
                case 2:
                    extension = "All files (*.*)|*.*";
                    break;
            }


            openFileDialog.Filter = extension;

            openFileDialog.ShowDialog();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            string extension = "All files (*.*)|*.*";

            int formatIndex = cmbExportFormat.SelectedIndex;

            switch (formatIndex)
            {
                case 0:
                    extension = "CSV files (*.csv)|*.csv";
                    break;
                case 1:
                    extension = "JSON files (*.json)|*.json";
                    break;
                case 2:
                    extension = "All files (*.*)|*.*";
                    break;
            }

            saveFileDialog.Filter = extension;

            saveFileDialog.ShowDialog();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            MasterAccount.VaultFormat format = MasterAccount.VaultFormat.Encrypted;

            int formatIndex = cmbImportFormat.SelectedIndex;

            switch (formatIndex)
            {
                case 0:
                    format = MasterAccount.VaultFormat.CSV;
                    break;
                case 1:
                    format = MasterAccount.VaultFormat.JSON;
                    break;
                case 2:
                    format = MasterAccount.VaultFormat.Encrypted;
                    break;
            }

            string selectedFile = openFileDialog.FileName;

            bool replace = rdbReplace.Checked;

            FormvIDsafe.Main.User.ImportVault(format, selectedFile, replace);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            MasterAccount.VaultFormat format = MasterAccount.VaultFormat.Encrypted;

            int formatIndex = cmbExportFormat.SelectedIndex;

            switch (formatIndex)
            {
                case 0:
                    format = MasterAccount.VaultFormat.CSV;
                    break;
                case 1:
                    format = MasterAccount.VaultFormat.JSON;
                    break;
                case 2:
                    format = MasterAccount.VaultFormat.Encrypted;
                    break;
            }

            string selectedPath = saveFileDialog.FileName;

            int identityIndex = cmbIdentity.SelectedIndex;

            FormvIDsafe.Main.User.ExportVault(format, identityIndex, selectedPath);
        }

        private void rdbIdentity_CheckedChanged(object sender, EventArgs e)
        {
            cmbIdentity.Enabled = rdbIdentity.Checked;

            if (!rdbIdentity.Checked)
            {
                cmbIdentity.SelectedIndex = -1;
            }
        }
    }
}
