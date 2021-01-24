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
            Register(txtName.Text, txtPassword.Text);
        }

        private void Register(string name, string password)
        {
            //Todo: cleanup (put isvalid in btnregister and maybe pass in confirm pass as parameter)
            if (IsValid(name, password))
            {
                FormvIDsafe.Main.User = new MasterAccount(name, password);

                if (FormvIDsafe.Main.User.TryRegister().Equals(true))
                {
                    FormHome form = new FormHome();
                    form.Show();

                    ParentForm.Hide();
                }
                else
                {
                    Console.WriteLine("Account already exist");
                }
            }
        }

        private bool IsValid(string name, string password)
        {
            if (name.Length >= 8)
            {
                if (password.Equals(txtConfirmPassword.Text))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Passwords are not the same");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Name is lower than 8 characters");
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormvIDsafe.OpenChildForm(new FormLogin());
        }
    }
}
