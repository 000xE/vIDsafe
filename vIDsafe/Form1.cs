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
        public static Form CurrentChildForm = null;
        public static Panel ChildFormPanel;
        public static Control.ControlCollection FormControls = null;

        public Form1()
        {
            InitializeComponent();
            getFormComponents();
            loadFormComponents();
        }

        private void getFormComponents()
        {
            ChildFormPanel = panelForm;
            FormControls = base.Controls;
        }

        private void loadFormComponents()
        {
            OpenChildForm(new Overview());
            lblMAName.Text = vIDsafe.Main.User.Name;
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

        public static void ChangeSelectedButton(object sender)
        {
            Button selectedButton = (Button)sender;
            selectedButton.ForeColor = Color.Black;
            //selectedButton.BackColor = Color.FromArgb(47, 47, 47);
            selectedButton.BackColor = Color.Gainsboro;

            Control navPanel = FormControls.Find("panelNavigation", true)[0];

            foreach (Control mainControls in navPanel.Controls)
            {
                foreach (Control navigationControls in mainControls.Controls)
                {
                    if (navigationControls.Tag != null)
                    {
                        if (navigationControls.Tag.ToString() == "navButton")
                        {
                            if (navigationControls != selectedButton)
                            {
                                navigationControls.ForeColor = Color.FromArgb(204, 204, 204);
                                //navigationControls.BackColor = Color.FromArgb(26, 26, 26);
                                //navigationControls.BackColor = Color.FromArgb(32, 32, 32);
                                navigationControls.BackColor = Color.FromArgb(29, 32, 36);                        
                            }
                        }
                    }
                }
            }
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new Overview());
        }

        private void btnIdentities_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new Identities());
        }

        private void btnVault_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new Vault());
        }

        private void btnImportExport_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new ImportExport());
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new GeneratePassword());
        }

        private void btnMasterAccount_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new MasterAccount());
        }

        private void btnPasswordManager_Click(object sender, EventArgs e)
        {
            panelPMSubMenu.Visible = !panelPMSubMenu.Visible;
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            panelDataSubMenu.Visible = !panelDataSubMenu.Visible;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            logout();
        }
        
        private void logout()
        {
            vIDsafe.Main.User.Logout();

            Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            vIDsafe.Main.Show();
        }
    }
}
