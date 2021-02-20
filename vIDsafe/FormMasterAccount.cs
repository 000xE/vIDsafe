using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormMasterAccount : Form
    {
        public FormMasterAccount()
        {
            InitializeComponent();
            InitialMethods();
        }

        /// <summary>
        /// Initial methods to run when the form starts
        /// </summary>
        private void InitialMethods()
        {
            DisplayLogs();
        }

        /// <summary>
        /// Displays the logs
        /// </summary>
        private void DisplayLogs()
        {
            Dictionary<DateTime, string> logs = MasterAccount.User.Vault.GetLogs(Vault.LogType.Account);

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

        private void btnDeleteCredentials_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete all credentials?", "Credential deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result.Equals(DialogResult.Yes))
            {
                DeleteCredentials();
            }
        }

        /// <summary>
        /// Deletes all credentials in the vault
        /// </summary>
        private void DeleteCredentials()
        {
            MasterAccount.User.Vault.DeleteAllCredentials();

            FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Credential deletion", "Successfully deleted all credentials");

            KeyValuePair<DateTime, string> log = MasterAccount.User.Vault.Log(Vault.LogType.Account, "All credentials deleted");
            DisplayLog(log.Key, log.Value);
        }

        private void btnDeleteIdentities_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete all identities?", "Identity deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result.Equals(DialogResult.Yes))
            {
                DeleteIdentities();
            }
        }

        /// <summary>
        /// Deletes all identities in the vault
        /// </summary>
        private void DeleteIdentities()
        {
            MasterAccount.User.Vault.DeleteAllIdentities();

            FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Identity deletion", "Successfully deleted all identities");

            KeyValuePair<DateTime, string> log = MasterAccount.User.Vault.Log(Vault.LogType.Account, "All identities deleted");
            DisplayLog(log.Key, log.Value);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete your account?", "Account deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result.Equals(DialogResult.Yes))
            {
                DeleteAccount();
            }
        }

        /// <summary>
        /// Deletes the account
        /// </summary>
        private void DeleteAccount()
        {
            MasterAccount.User.DeleteAccount();

            FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Account deletion", "Successfully deleted account");

            ParentForm.Close();
        }

        private void btnChangeDetails_Click(object sender, EventArgs e)
        {
            ChangeNameAsync(txtCurrentPassword.Text, txtName.Text);
        }

        /// <summary>
        /// Enables or disables form components
        /// </summary>
        private void EnableMasterAccountComponents(bool enable)
        {
            btnChangeDetails.Enabled = enable;
            btnChangePassword.Enabled = enable;
        }

        /// <summary>
        /// Tries to change the name
        /// </summary>
        private async void ChangeNameAsync(string currentPassword, string newName)
        {
            if (IsValidUsername(newName, currentPassword))
            {
                EnableMasterAccountComponents(false);

                bool canChangeName = false;

                await Task.Run(() =>
                {
                    canChangeName = MasterAccount.User.TryChangeName(currentPassword, newName);
                });

                if (canChangeName.Equals(true))
                {
                    FormHome.SetName(newName);

                    FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Name change", "Successfully changed name");

                    KeyValuePair<DateTime, string> log = MasterAccount.User.Vault.Log(Vault.LogType.Account, "Name changed");
                    DisplayLog(log.Key, log.Value);
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Password error", "Wrong old password");
                }

                EnableMasterAccountComponents(true);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordAsync(txtCurrentPassword2.Text, txtNewPassword.Text);
        }

        /// <summary>
        /// Tries to change the password
        /// </summary>
        private async void ChangePasswordAsync(string currentPassword, string newPassword)
        {
            if (IsValidPassword(newPassword, currentPassword))
            {
                EnableMasterAccountComponents(false);

                bool canChangePass = false;

                await Task.Run(() =>
                {
                    canChangePass = MasterAccount.User.TryChangePassword(currentPassword, newPassword);
                });

                if (canChangePass.Equals(true))
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Password change", "Successfully changed password");

                    KeyValuePair<DateTime, string> log = MasterAccount.User.Vault.Log(Vault.LogType.Account, "Password changed");
                    DisplayLog(log.Key, log.Value);
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Password error", "Wrong old password");
                }

                EnableMasterAccountComponents(true);
            }
        }

        /// <summary>
        /// Checks if the username is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private bool IsValidUsername(string newName, string password)
        {
            if (newName.Length >= 8)
            {
                if (password.Length > 0)
                {
                    return true;
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter your password"); FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter a password");
                    return false;
                }
            }
            else
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Name is lower than 8 characters");
                return false;
            }
        }

        /// <summary>
        /// Checks if the password is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private bool IsValidPassword(string newPassword, string oldPassword)
        {
            if (oldPassword.Length > 0)
            {
                if (newPassword.Length > 0)
                {
                    if (newPassword.Equals(txtConfirmPassword.Text))
                    {
                        return true;
                    }
                    else
                    {
                        FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Password error", "New passwords are not the same");
                        return false;
                    }
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Password error", "Please enter a new password");
                    return false;
                }
            }
            else
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter your old password");
                return false;
            }
        }

        private void FormMasterAccount_Resize(object sender, EventArgs e)
        {
            FixColumnWidths();
        }
    }
}
