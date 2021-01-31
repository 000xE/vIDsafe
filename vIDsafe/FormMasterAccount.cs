using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormMasterAccount : Form
    {
        public FormMasterAccount()
        {
            InitializeComponent();
            LoadFormComponents();
        }

        private void LoadFormComponents()
        {
            GetLogs();
        }

        private void GetLogs()
        {
            lvLogs.Items.Clear();
            foreach (KeyValuePair<DateTime, string> log in FormvIDsafe.Main.User.Vault.GetLogs(Vault.LogType.Account))
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

        private void btnDeleteCredentials_Click(object sender, EventArgs e)
        {
            DeleteCredentials();
            GetLogs();
        }

        private void DeleteCredentials()
        {
            FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Credential deletion", "Successfully deleted all credentials");
            FormvIDsafe.Main.User.Vault.DeleteAllCredentials();

            KeyValuePair<DateTime, string> log = FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Account, "All credentials deleted");
            DisplayLog(log.Key, log.Value);
        }

        private void btnDeleteIdentities_Click(object sender, EventArgs e)
        {
            DeleteIdentities();
            GetLogs();
        }

        private void DeleteIdentities()
        {
            FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Identity deletion", "Successfully deleted all identities");
            FormvIDsafe.Main.User.Vault.DeleteAllIdentities();

            KeyValuePair<DateTime, string> log = FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Account, "All identities deleted");
            DisplayLog(log.Key, log.Value);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            DeleteAccount();
        }

        private void DeleteAccount()
        {
            FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Account deletion", "Successfully deleted account");
            FormvIDsafe.Main.User.DeleteAccount();

            ParentForm.Close();
        }

        private void btnChangeDetails_Click(object sender, EventArgs e)
        {
            ChangeName(txtCurrentPassword.Text, txtName.Text);
            GetLogs();
        }
        private void ChangeName(string currentPassword, string newName)
        {
            if (IsValidUsername(newName))
            {
                if (FormvIDsafe.Main.User.TryChangeName(currentPassword, newName).Equals(true))
                {
                    FormHome.SetName(newName);

                    FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Name change", "Successfully changed name");

                    KeyValuePair<DateTime, string> log = FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Account, "Name changed");
                    DisplayLog(log.Key, log.Value);
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Password error", "Wrong old password");
                }
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword(txtCurrentPassword2.Text, txtNewPassword.Text);
            GetLogs();
        }

        private void ChangePassword(string currentPassword, string newPassword)
        {
            //Todo: cleanup (put isvalid in btnlogin and maybe pass in confirm pass as parameter)
            if (IsValidPassword(newPassword))
            {
                if (FormvIDsafe.Main.User.TryChangePassword(currentPassword, newPassword).Equals(true))
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Password change", "Successfully changed password");

                    KeyValuePair<DateTime, string> log = FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Account, "Password changed");
                    DisplayLog(log.Key, log.Value);
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Password error", "Wrong old password");
                }
            }
        }

        private bool IsValidUsername(string name)
        {
            if (name.Length >= 8)
            {
                return true;
            }
            else
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Name is lower than 8 characters");
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            if (password.Equals(txtConfirmPassword.Text))
            {
                return true;
            }
            else
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Password error", "New passwords are not the same");
                return false;
            }
        }

        private void FormMasterAccount_Resize(object sender, EventArgs e)
        {
            FixColumnWidths();
        }
    }
}
