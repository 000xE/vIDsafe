using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginAsync(txtName.Text, txtPassword.Text);
        }

        /// <summary>
        /// Tries to login to an account
        /// </summary>
        private async void LoginAsync(string name, string password)
        {
            if (IsValid(name))
            {
                EnableLoginComponents(false);

                bool canLogin = false;

                await Task.Run(() =>
                {
                    canLogin = MasterAccount.GetUser().TryLogin(name, password);
                });

                if (canLogin.Equals(true))
                {
                    FormHome form = new FormHome();
                    form.Show();

                    ParentForm.Hide();
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Login error", "Account doesn't exist or wrong password");
                }

                EnableLoginComponents(true);
            }
        }

        /// <summary>
        /// Enables or disables form components
        /// </summary>
        private void EnableLoginComponents(bool enable)
        {
            btnLogin.Enabled = enable;
            btnRegister.Enabled = enable;
        }

        /// <summary>
        /// Checks if the name is valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private bool IsValid(string name)
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormvIDsafe.OpenChildForm(new FormRegister());
        }
    }
}
