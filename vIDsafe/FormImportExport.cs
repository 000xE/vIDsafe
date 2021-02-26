using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormImportExport : Form
    {
        public FormImportExport()
        {
            InitializeComponent();

            InitialMethods();
        }

        /// <summary>
        /// Initial methods to run when the form starts
        /// </summary>
        private void InitialMethods()
        {
            GetIdentities();
            DisplayLogs();
        }

        /// <summary>
        /// Displays the logs
        /// </summary>
        private void DisplayLogs()
        {
            Dictionary<DateTime, string> logs = MasterAccount.User.Vault.GetLogs(Vault.LogType.Porting);

            lvLogs.Items.Clear();

            foreach (KeyValuePair<DateTime, string> log in logs)
            {
                DisplayLog(log.Key, log.Value);
            }
        }

        /// <summary>
        /// Displays a specific log
        /// </summary>
        private void DisplayLog(DateTime dateTime, string log)
        {
            ListViewItem lvi = new ListViewItem("");
            lvi.SubItems.Add(dateTime.ToString());
            lvi.SubItems.Add(log);

            lvLogs.Items.Add(lvi);
        }

        /// <summary>
        /// Fixes the column widths based on listview width and column count
        /// </summary>
        private void FixColumnWidths()
        {
            lvLogs.Columns[1].Width = lvLogs.Width / (lvLogs.Columns.Count - 1);
            lvLogs.Columns[2].Width = lvLogs.Width / (lvLogs.Columns.Count - 1);
        }

        /// <summary>
        /// Gets and displays the identities
        /// </summary>
        private void GetIdentities()
        {
            cmbIdentity.Items.Clear();

            foreach (KeyValuePair<string, Identity> identityPair in MasterAccount.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identityPair.Key);
            }
        }

        /// <summary>
        /// Opens a file dialog for the selected format
        /// </summary>
        private void OpenFile(int formatIndex)
        {
            openFileDialog.Filter = GetExtension(formatIndex);

            openFileDialog.ShowDialog();
        }

        /// <summary>
        /// Gets the extension filter based on the selected format
        /// </summary>
        /// <returns>
        /// The extension filter
        /// </returns>
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

        /// <summary>
        /// Enables or disables form components
        /// </summary>
        private void EnableImportExportComponents(bool enable)
        {
            btnImport.Enabled = enable;
            btnExport.Enabled = enable;
        }

        /// <summary>
        /// Imports the selected file
        /// </summary>
        private async void ImportAsync(int formatIndex, bool replace, string fileName)
        {
            EnableImportExportComponents(false);

            Porting.VaultFormat format = GetFormat(formatIndex);

            bool canImport = await Task.Run(() =>     
                MasterAccount.User.TryImportVault(format, fileName, replace)
            );

            if (canImport)
            {
                NotificationManager.ShowInfo("Import", "Successfully imported");

                KeyValuePair<DateTime, string> log = MasterAccount.User.Vault.CreateLog(Vault.LogType.Porting, "Imported data");
                DisplayLog(log.Key, log.Value);
            }
            else
            {
                NotificationManager.ShowError("Import error", "Check the file contents or its path");
            }

            EnableImportExportComponents(true);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SelectFolder(cmbExportFormat.SelectedIndex);
        }

        /// <summary>
        /// Exports the selected data
        /// </summary>
        private async void ExportAsync(int formatIndex, int selectedIdentityIndex, string fileName)
        {
            EnableImportExportComponents(false);

            string selectedEmail = "";

            if (selectedIdentityIndex >= 0)
            {
                selectedEmail = cmbIdentity.SelectedItem.ToString();
            }

            Porting.VaultFormat format = GetFormat(formatIndex);

            bool canExport = await Task.Run(() =>
                MasterAccount.User.TryExportVault(format, selectedEmail, fileName)
            );

            if (canExport)
            {
                NotificationManager.ShowInfo("Export", "Successfully exported");

                KeyValuePair<DateTime, string> log = MasterAccount.User.Vault.CreateLog(Vault.LogType.Porting, "Exported data");
                DisplayLog(log.Key, log.Value);
            }
            else
            {
                NotificationManager.ShowError("Export error", "Check the file path");
            }

            EnableImportExportComponents(true);
        }

        /// <summary>
        /// Selects a folder to import or export
        /// </summary>
        private void SelectFolder(int formatIndex)
        {
            string extension = GetExtension(formatIndex);
            string fileName = MasterAccount.User.Name + "_export_" + DateTime.Now.ToString("yyyyMMddHHmms");

            saveFileDialog.Filter = extension;
            saveFileDialog.FileName = fileName;

            saveFileDialog.ShowDialog();
        }

        /// <summary>
        /// Gets the format to import/export based on the selection
        /// </summary>
        /// <returns>
        /// The format to import/export
        /// </returns>
        private Porting.VaultFormat GetFormat(int formatIndex)
        {
            Porting.VaultFormat format = Porting.VaultFormat.Encrypted;

            switch (formatIndex)
            {
                case 0:
                    format = Porting.VaultFormat.CSV;
                    break;
                case 1:
                    format = Porting.VaultFormat.JSON;
                    break;
                case 2:
                    format = Porting.VaultFormat.Encrypted;
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
            if (rdbReplace.Checked)
            {
                NotificationManager.ShowWarning("Warning", "This will erase your current vault details!");
            }
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = cmbIdentity.SelectedIndex >= 0;
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            ImportAsync(cmbImportFormat.SelectedIndex, rdbReplace.Checked, openFileDialog.FileName);
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            ExportAsync(cmbExportFormat.SelectedIndex, cmbIdentity.SelectedIndex, saveFileDialog.FileName);
        }
    }
}
