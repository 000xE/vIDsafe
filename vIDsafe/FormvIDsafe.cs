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
    public partial class FormvIDsafe : Form
    {
        public static Panel ChildFormPanel;

        public MasterAccount User;

        public static FormvIDsafe Main;

        public FormvIDsafe()
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
            OpenChildForm(new FormLogin());
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
