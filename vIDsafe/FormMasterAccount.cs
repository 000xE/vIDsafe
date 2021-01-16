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
            FormvIDsafe.Main.User.Vault.DeleteAllCredentials();
        }

        private void btnDeleteIdentities_Click(object sender, EventArgs e)
        {
            DeleteIdentities();
            GetLogs();
        }

        private void DeleteIdentities()
        {
            FormvIDsafe.Main.User.Vault.DeleteAllIdentities();
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            DeleteAccount();
        }

        private void DeleteAccount()
        {
            FormvIDsafe.Main.User.DeleteAccount();

            ParentForm.Close();
        }

        private void btnChangeDetails_Click(object sender, EventArgs e)
        {
            ChangeName();
            GetLogs();
        }
        private void ChangeName()
        {
            if (FormvIDsafe.Main.User.TryChangeName(txtCurrentPassword.Text, txtName.Text) == true)
            {
                Console.WriteLine("Name changed");
            }
            else
            {
                Console.WriteLine("Wrong old password");
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword();
            GetLogs();
        }

        private void ChangePassword()
        {
            if (FormvIDsafe.Main.User.TryChangePassword(txtCurrentPassword2.Text, txtNewPassword.Text) == true)
            {
                Console.WriteLine("Pass changed");
            }
            else
            {
                Console.WriteLine("Wrong old password");
            }
        }

        private void FormMasterAccount_Resize(object sender, EventArgs e)
        {
            FixColumnWidths();
        }
    }
}
