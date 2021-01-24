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
        private static Panel _pnlChildForm;
        private static Control.ControlCollection ctrlsFormControls = null;

        public FormHome()
        {
            InitializeComponent();
            GetFormComponents();
            LoadFormComponents();
        }

        private void GetFormComponents()
        {
            _pnlChildForm = panelForm;
            ctrlsFormControls = Controls;
        }

        private void LoadFormComponents()
        {
            OpenChildForm(new FormOverview());
            SetName(FormvIDsafe.Main.User.Name);
        }

        public static void SetName(string name)
        {
            Label lblMasterAccountName = (Label)ctrlsFormControls.Find("lblMAName", true)[0];
            lblMasterAccountName.Text = name;
        }

        public static void SetHealthScore(int healthScore, Color healthColor)
        {
            Panel pnlProgressBar = (Panel)ctrlsFormControls.Find("pnlProgressBar", true)[0];
            pnlProgressBar.BackColor = healthColor;

            int panelWidth = (int)((double)pnlProgressBar.MaximumSize.Width / 100 * healthScore);

            pnlProgressBar.Size = new Size(panelWidth, pnlProgressBar.Height);

            Panel pnlProgressBack = (Panel)ctrlsFormControls.Find("pnlProgressBack", true)[0];

            Label lblHealthScore = (Label)pnlProgressBack.Controls.Find("lblHealthScore", true)[0];

            lblHealthScore.Text = healthScore.ToString() + "%";

        }

        //https://stackoverflow.com/a/28811266
        public void OpenChildForm(Form frmChildForm)
        {
            while (_pnlChildForm.Controls.Count > 0)
            {
                _pnlChildForm.Controls[0].Dispose();
            }

            frmChildForm.TopLevel = false;
            frmChildForm.FormBorderStyle = FormBorderStyle.None;
            frmChildForm.Dock = DockStyle.Fill;
            _pnlChildForm.Enabled = true;
            _pnlChildForm.Controls.Add(frmChildForm);
            _pnlChildForm.Tag = frmChildForm;
            frmChildForm.BringToFront();
            frmChildForm.Show();
        }

        public void ChangeSelectedButton(object sender)
        {
            Button selectedButton = (Button)sender;
            selectedButton.ForeColor = Color.Black;
            //selectedButton.BackColor = Color.FromArgb(47, 47, 47);
            selectedButton.BackColor = Color.Gainsboro;

            //Todo: cleanup

            Panel pnlNav = (Panel) ctrlsFormControls.Find("pnlNavigation", true)[0];

            foreach (Control ctrlsMain in pnlNav.Controls)
            {
                foreach (Control ctrlsNav in ctrlsMain.Controls)
                {
                    if (ctrlsNav.Tag != null)
                    {
                        if (ctrlsNav.Tag.ToString().Equals("navButton"))
                        {
                            if (ctrlsNav != selectedButton)
                            {
                                ctrlsNav.ForeColor = Color.FromArgb(204, 204, 204);
                                //navigationControls.BackColor = Color.FromArgb(26, 26, 26);
                                //navigationControls.BackColor = Color.FromArgb(32, 32, 32);
                                ctrlsNav.BackColor = Color.FromArgb(29, 32, 36);                        
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
            pnlPMSubMenu.Visible = !pnlPMSubMenu.Visible;
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            pnlDataSubMenu.Visible = !pnlDataSubMenu.Visible;
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
