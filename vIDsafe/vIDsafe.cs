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
        public static Panel ChildFormPanel;

        public UserAccount User;

        public static vIDsafe Main;

        public vIDsafe()
        {
            InitializeComponent();
            GetFormComponents();
            LoadFormComponents();
        }

        private void GetFormComponents()
        {
            ChildFormPanel = panelForm;

            Main = this;
        }

        private void LoadFormComponents()
        {
            OpenChildForm(new Login());
        }

        //https://stackoverflow.com/a/28811266
        public static void OpenChildForm(Form childForm)
        {
            while (ChildFormPanel.Controls.Count > 0)
            {
                ChildFormPanel.Controls[0].Dispose();
            }

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
