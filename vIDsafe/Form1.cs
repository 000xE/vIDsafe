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
    public partial class Form1 : Form
    {
        public static Form activeForm = null;
        public static Panel formPanel;

        public Form1()
        {
            InitializeComponent();
            GetFormComponents();
            LoadFormComponents();
        }

        private void GetFormComponents()
        {
            formPanel = panelForm;
        }

        private void LoadFormComponents()
        {
            OpenChildForm(new Overview());
        }

        private static void OpenChildForm(Form childForm)
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

        private void BtnOverview_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Overview());
        }

        private void BtnIdentities_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Identities());
        }

        private void BtnVault_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Vault());
        }

        private void BtnImportExport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ImportExport());
        }

        private void BtnGeneratePassword_Click(object sender, EventArgs e)
        {
            OpenChildForm(new GeneratePassword());
        }

        private void BtnMasterAccount_Click(object sender, EventArgs e)
        {
            OpenChildForm(new MasterAccount());
        }

        private void BtnApplicationSettings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ApplicationSettings());
        }

        private void BtnPasswordManager_Click(object sender, EventArgs e)
        {
            panelPMSubMenu.Visible = !panelPMSubMenu.Visible;
        }

        private void BtnData_Click(object sender, EventArgs e)
        {
            panelDataSubMenu.Visible = !panelDataSubMenu.Visible;
        }

        private void BtnMisc_Click(object sender, EventArgs e)
        {
            panelMiscSubMenu.Visible = !panelMiscSubMenu.Visible;
        }
    }
}
