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
#pragma warning disable IDE1006 // Naming Styles
    public partial class vIDsafe : Form
#pragma warning restore IDE1006 // Naming Styles
    {
        public static Form activeForm = null;
        public static Panel formPanel;

        public UserAccount user;

        public static vIDsafe main;

        public vIDsafe()
        {
            InitializeComponent();
            getFormComponents();
            loadFormComponents();
        }

        private void getFormComponents()
        {
            formPanel = panelForm;

            main = this;
        }

        private void loadFormComponents()
        {
            openChildForm(new Login());
        }

        public static void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            formPanel.Enabled = true;
            formPanel.Controls.Add(childForm);
            formPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
