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
            FormvIDsafe.Main.User = new MasterAccount(txtName.Text, txtPassword.Text);

            if (IsValid())
            {
                int loginStatusCode = FormvIDsafe.Main.User.TryLogin();

                switch (loginStatusCode)
                {
                    case 0:
                        Console.WriteLine("Account doesn't exist");
                        break;
                    case 1:
                        FormHome form = new FormHome();
                        form.Show();

                        ParentForm.Hide();
                        break;
                    case 2:
                        Console.WriteLine("Wrong password");
                        break;
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
