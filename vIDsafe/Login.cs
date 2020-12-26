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
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            vIDsafe.OpenChildForm(new Register());
        }
    }
}
