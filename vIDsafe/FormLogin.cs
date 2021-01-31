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
            Login(txtName.Text, txtPassword.Text);
        }

        private void Login(string name, string password)
        {
            if (IsValid(name))
            {
                FormvIDsafe.Main.User = new MasterAccount(name, password);

                if (FormvIDsafe.Main.User.TryLogin().Equals(true))
                {
                    FormHome form = new FormHome();
                    form.Show();

                    ParentForm.Hide();
                }
                else
                {
                    FormvIDsafe.ShowError("Login error", "Account doesn't exist or wrong password");
                }
            }
        }

        private bool IsValid(string name)
        {
            if (name.Length >= 8)
            {
                return true;
            }
            else
            {
                FormvIDsafe.ShowError("Validation error", "Name is lower than 8 characters");
                return false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormvIDsafe.OpenChildForm(new FormRegister());
        }
    }
}
