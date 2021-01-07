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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            vIDsafe.main.user = new UserAccount(txtName.Text, txtPassword.Text);

            if (isValid())
            {
                int registerStatusCode = vIDsafe.main.user.returnRegisterSuccess();

                switch (registerStatusCode)
                {
                    case 0:
                        Console.WriteLine("Account already exist");
                        break;
                    case 1:
                        Form1 form = new Form1();
                        form.Show();

                        ParentForm.Hide();
                        break;
                }
            }
        }

        private bool isValid()
        {
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            vIDsafe.openChildForm(new Login());
        }
    }
}
