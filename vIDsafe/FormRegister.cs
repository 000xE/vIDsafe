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
            FormvIDsafe.Main.User = new MasterAccount(txtName.Text, txtPassword.Text);

            if (IsValid())
            {
                int registerStatusCode = FormvIDsafe.Main.User.TryRegister();

                switch (registerStatusCode)
                {
                    case 0:
                        Console.WriteLine("Account already exist");
                        break;
                    case 1:
                        FormHome form = new FormHome();
                        form.Show();

                        ParentForm.Hide();
                        break;
                }
            }
        }

        private bool IsValid()
        {
            return true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormvIDsafe.OpenChildForm(new FormLogin());
        }
    }
}
