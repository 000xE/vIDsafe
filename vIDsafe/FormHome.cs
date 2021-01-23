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
    public partial class FormHome : Form
    {
        public static Panel ChildFormPanel;
        public static Control.ControlCollection FormControls = null;

        public FormHome()
        {
            InitializeComponent();
            GetFormComponents();
            LoadFormComponents();
        }

        private void GetFormComponents()
        {
            ChildFormPanel = panelForm;
            FormControls = this.Controls;
        }

        private void LoadFormComponents()
        {
            OpenChildForm(new FormOverview());
            SetName();
        }

        public static void SetName()
        {
            ((Label) FormControls.Find("lblMAName", true)[0]).Text = FormvIDsafe.Main.User.Name;
        }

        //https://stackoverflow.com/a/28811266
        public void OpenChildForm(Form childForm)
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

        public void ChangeSelectedButton(object sender)
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
            OpenChildForm(new FormOverview());
        }

        private void btnIdentities_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new FormIdentities());
        }

        private void btnVault_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new FormVault());
        }

        private void btnImportExport_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new FormImportExport());
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new FormGeneratePassword());
        }

        private void btnMasterAccount_Click(object sender, EventArgs e)
        {
            ChangeSelectedButton(sender);
            OpenChildForm(new FormMasterAccount());
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
            Logout();
        }
        
        private void Logout()
        {
            FormvIDsafe.Main.User.Logout();

            Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormvIDsafe.Main.Show();
        }
    }
}
