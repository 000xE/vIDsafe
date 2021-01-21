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

            txtName.Text = "testacc123";
            txtPassword.Text = "123";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login(txtName.Text, txtPassword.Text);
        }

        private void Login(string name, string password)
        {
            FormvIDsafe.Main.User = new MasterAccount(name, password);

            if (IsValid())
            {
                if (FormvIDsafe.Main.User.TryLogin() == true)
                {
                    FormHome form = new FormHome();
                    form.Show();

                    ParentForm.Hide();
                }
                else
                {
                    Console.WriteLine("Account doesn't exist or wrong password");
                }
            }
        }

        private bool IsValid()
        {
            return true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormvIDsafe.OpenChildForm(new FormRegister());
        }
    }
}
