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
    public partial class vIDsafe : Form
    {
        public static Form CurrentChildForm = null;
        public static Panel ChildFormPanel;

        public UserAccount User;

        public static vIDsafe Main;

        public vIDsafe()
        {
            InitializeComponent();
            getFormComponents();
            loadFormComponents();
        }

        private void getFormComponents()
        {
            ChildFormPanel = panelForm;

            Main = this;
        }

        private void loadFormComponents()
        {
            OpenChildForm(new Login());
        }

        public static void OpenChildForm(Form childForm)
        {
            if (CurrentChildForm != null)
            {
                CurrentChildForm.Close();
            }
            CurrentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            ChildFormPanel.Enabled = true;
            ChildFormPanel.Controls.Add(childForm);
            ChildFormPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
    }
}
