using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class MasterAccount : Form
    {
        public MasterAccount()
        {
            InitializeComponent();
        }

        private void btnDeleteCredentials_Click(object sender, EventArgs e)
        {
            DeleteCredentials();
        }

        private void DeleteCredentials()
        {
            vIDsafe.Main.User.Vault.DeleteAllCredentials();
        }

        private void btnDeleteIdentities_Click(object sender, EventArgs e)
        {
            DeleteIdentities();
        }

        private void DeleteIdentities()
        {
            vIDsafe.Main.User.Vault.DeleteAllIdentities();
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            DeleteAccount();
        }

        private void DeleteAccount()
        {
            vIDsafe.Main.User.DeleteAccount();

            ParentForm.Close();
        }

        private void btnChangeDetails_Click(object sender, EventArgs e)
        {
            ChangeName();
        }
        private void ChangeName()
        {
            if (vIDsafe.Main.User.TryChangeName(txtCurrentPassword.Text, txtName.Text) == 1)
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
        }

        private void ChangePassword()
        {
            if (vIDsafe.Main.User.TryChangePassword(txtCurrentPassword2.Text, txtNewPassword.Text) == 1)
            {
                Console.WriteLine("Pass changed");
            }
            else
            {
                Console.WriteLine("Wrong old password");
            }
        }
    }
}
