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
            RegisterAsync(txtName.Text, txtPassword.Text, txtConfirmPassword.Text);
        }

        /// <summary>
        /// Tries to register an account
        /// </summary>
        private async void RegisterAsync(string name, string password, string confirmPassword)
        {
            if (AccountValidator.IsRegisterValid(name, password, confirmPassword))
            {
                EnableRegisterComponents(false);

                bool canRegister = await Task.Run(() =>           
                    MasterAccount.User.TryRegister(name, password)
                );

                if (canRegister.Equals(true))
                {
                    FormHome form = new FormHome();
                    form.Show();

                    ParentForm.Hide();
                }
                else
                {
                    NotificationManager.ShowError("Registration error", "Account already exist");
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormvIDsafe.OpenChildForm(new FormLogin());
        }
    }
}
