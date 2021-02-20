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
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterAsync(txtName.Text, txtPassword.Text);
        }

        /// <summary>
        /// Tries to register an account
        /// </summary>
        private async void RegisterAsync(string name, string password)
        {
            //Todo: cleanup (put isvalid in btnregister and maybe pass in confirm pass as parameter)
            if (IsValid(name, password))
            {
                EnableRegisterComponents(false);

                bool canRegister = false;

                await Task.Run(() =>
                {
                    canRegister = MasterAccount.User.TryRegister(name, password);
                });

                if (canRegister.Equals(true))
                {
                    FormHome form = new FormHome();
                    form.Show();

                    ParentForm.Hide();
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Registration error", "Account already exist");
                }

                EnableRegisterComponents(true);
            }
        }

        /// <summary>
        /// Enables or disables form components
        /// </summary>
        private void EnableRegisterComponents(bool enable)
        {
            btnLogin.Enabled = enable;
            btnRegister.Enabled = enable;
        }

        /// <summary>
        /// Checks if the name and password are valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
        private bool IsValid(string name, string password)
        {
            if (name.Length >= 8)
            {
                if (password.Length > 0)
                {
                    if (password.Equals(txtConfirmPassword.Text))
                    {
                        return true;
                    }
                    else
                    {
                        FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Passwords are not the same");
                        return false;
                    }
                }
                else
                {
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter a password");
                    return false;
                }
            }
            else
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Name is lower than 8 characters");
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormvIDsafe.OpenChildForm(new FormLogin());
        }
    }
}
