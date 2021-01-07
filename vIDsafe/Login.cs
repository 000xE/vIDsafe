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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            string testkey = Convert.ToBase64String(Encryption.hashPassword("TestPass", "TestName"));

            string encrypted = Encryption.aesEncrypt("TestEncryption one two three four five", Convert.FromBase64String(testkey));


            string decrypted = Encryption.aesDecrypt(encrypted, Convert.FromBase64String(testkey));

            Console.WriteLine("Encrypted: " + encrypted);

            Console.WriteLine("Decrypted: " + decrypted);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            vIDsafe.main.user = new UserAccount(txtName.Text, txtPassword.Text);

            if (isValid())
            {
                int loginStatusCode = vIDsafe.main.user.returnLoginSuccess();

                switch (loginStatusCode)
                {
                    case 0:
                        Console.WriteLine("Account doesn't exist");
                        break;
                    case 1:
                        Form1 form = new Form1();
                        form.Show();

                        ParentForm.Hide();
                        break;
                    case 2:
                        Console.WriteLine("Wrong password");
                        break;
                }
            }
        }

        private bool isValid()
        {
            return true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            vIDsafe.openChildForm(new Register());
        }
    }
}
