using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            FormHome.SetTheme(this);
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

            foreach (KeyValuePair<string, Identity> identityPair in FormvIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identityPair.Key);
            }
        }

        private void OpenFile(int formatIndex)
        {
            openFileDialog.Filter = GetExtension(formatIndex);

            openFileDialog.ShowDialog();
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
            OpenFile(cmbImportFormat.SelectedIndex);
        }

        private void Import(int formatIndex, bool replace, string fileName)
        {
            MasterAccount.VaultFormat format = GetFormat(formatIndex);

            if (FormvIDsafe.Main.User.ImportVault(format, fileName, replace))
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
            SelectFolder(cmbExportFormat.SelectedIndex);
        }

        //Todo: refactor
        private void Export(int formatIndex, int selectedIdentityIndex, string fileName)
        {
            string selectedEmail = "";

            if (selectedIdentityIndex >= 0)
            {
                selectedEmail = cmbIdentity.SelectedItem.ToString();
            }

            MasterAccount.VaultFormat format = GetFormat(formatIndex);

            if (FormvIDsafe.Main.User.ExportVault(format, selectedEmail, fileName))
            {
                KeyValuePair<DateTime, string> log = FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Porting, "Exported data");
                DisplayLog(log.Key, log.Value);
            }
            else
            {
                Console.WriteLine("Cannot export");
            }
        }

        private void SelectFolder(int formatIndex)
        {
            saveFileDialog.Filter = GetExtension(formatIndex);

            string fileName = FormvIDsafe.Main.User.Name + "_export_" + DateTime.Now.ToString("yyyyMMddHHmms");

            saveFileDialog.FileName = fileName;

            saveFileDialog.ShowDialog();
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

            if (rdbIdentity.Checked)
            {
                btnExport.Enabled = cmbIdentity.SelectedIndex >= 0;
            }
            else
            {
                cmbIdentity.SelectedIndex = -1;
            }
        }

        private void rdbAllData_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = rdbAllData.Checked;
        }

        private void FormImportExport_Resize(object sender, EventArgs e)
        {
            FixColumnWidths();
        }

        private void cmbImportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnImport.Enabled = cmbImportFormat.SelectedIndex >= 0;
        }

        private void cmbExportFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = cmbExportFormat.SelectedIndex >= 0;
        }

        private void rdbReplace_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Warning: This will erase your current vault details!");
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = cmbIdentity.SelectedIndex >= 0;
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            Import(cmbImportFormat.SelectedIndex, rdbReplace.Checked, openFileDialog.FileName);
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            Export(cmbExportFormat.SelectedIndex, cmbIdentity.SelectedIndex, saveFileDialog.FileName);
        }
    }
}
