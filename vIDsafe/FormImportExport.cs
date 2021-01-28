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
            GetLogs();
        }
        private void GetLogs()
        {
            lvLogs.Items.Clear();
            foreach (KeyValuePair<DateTime, string> log in FormvIDsafe.Main.User.Vault.GetLogs(Vault.LogType.Porting))
            {
                DisplayLog(log.Key, log.Value);
            }
        }

        private void DisplayLog(DateTime dateTime, string log)
        {
            ListViewItem lvi = new ListViewItem("");
            lvi.SubItems.Add(dateTime.ToString());
            lvi.SubItems.Add(log);

            lvLogs.Items.Add(lvi);
        }

        private void FixColumnWidths()
        {
            lvLogs.Columns[1].Width = lvLogs.Width / (lvLogs.Columns.Count - 1);
            lvLogs.Columns[2].Width = lvLogs.Width / (lvLogs.Columns.Count - 1);
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
            OpenFile(cmbExportFormat.SelectedIndex);
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            SelectFolder(cmbExportFormat.SelectedIndex);
        }

        private void OpenFile(int formatIndex)
        {
            openFileDialog.Filter = GetExtension(formatIndex);

            openFileDialog.ShowDialog();
        }

        private void SelectFolder(int formatIndex)
        {
            saveFileDialog.Filter = GetExtension(formatIndex);

            saveFileDialog.ShowDialog();
        }

        private string GetExtension(int formatIndex)
        {
            string extension = "All files (*.*)|*.*";

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

            return extension;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Import(cmbImportFormat.SelectedIndex, openFileDialog.FileName, rdbReplace.Checked);
        }

        private void Import(int formatIndex, string selectedFile, bool replace)
        {
            MasterAccount.VaultFormat format = GetFormat(formatIndex);

            if (FormvIDsafe.Main.User.ImportVault(format, selectedFile, replace))
            {
                KeyValuePair<DateTime, string> log = FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Porting, "Imported data");
                DisplayLog(log.Key, log.Value);
            }
            else
            {
                Console.WriteLine("Cannot import");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Export(cmbExportFormat.SelectedIndex, saveFileDialog.FileName, cmbIdentity.SelectedIndex);
        }

        private void Export(int formatIndex, string selectedPath, int identityIndex)
        {
            MasterAccount.VaultFormat format = GetFormat(formatIndex);

            if (FormvIDsafe.Main.User.ExportVault(format, identityIndex, selectedPath))
            {
                KeyValuePair<DateTime, string> log = FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Porting, "Exported data");
                DisplayLog(log.Key, log.Value);
            }
            else
            {
                Console.WriteLine("Cannot export");
            }
        }

        private MasterAccount.VaultFormat GetFormat(int formatIndex)
        {
            MasterAccount.VaultFormat format = MasterAccount.VaultFormat.Encrypted;

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

            return format;
        }

        private void rdbIdentity_CheckedChanged(object sender, EventArgs e)
        {
            cmbIdentity.Enabled = rdbIdentity.Checked;

            if (!rdbIdentity.Checked)
            {
                cmbIdentity.SelectedIndex = -1;
            }
        }

        private void FormImportExport_Resize(object sender, EventArgs e)
        {
            FixColumnWidths();
        }
    }
}
