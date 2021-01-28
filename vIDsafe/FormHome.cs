using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vIDsafe.Properties;

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

        private void GetTheme()
        {
            foreach (Control ctrlsMain in ctrlsFormControls)
            {
                UpdateColorControls(ctrlsMain);
            }
        }

        //https://stackoverflow.com/questions/22935285/change-color-of-all-controls-inside-the-form-in-c-sharp/22935406#22935406
        public void UpdateColorControls(Control control)
        {
            //Todo: themes
            control.BackColor = Color.Black;
            control.ForeColor = Color.White;
            foreach (Control ctrlSub in control.Controls)
            {
                UpdateColorControls(ctrlSub);
            }
        }

        private void LoadFormComponents()
        {
            OpenChildForm(new FormOverview());
            SetName(FormvIDsafe.Main.User.Name);
            GetSettings();
            //GetTheme();
        }
        private void GetSettings()
        {
            onCloseToolStripMenuItem.Checked = Settings.Default.HideToTrayClose;
            onMinimizeToolStripMenuItem.Checked = Settings.Default.HideToTrayMinimize;
            onStartToolStripMenuItem.Checked = Settings.Default.HideToTrayStart;
            alwaysOnTopToolStripMenuItem.Checked = Settings.Default.AlwaysOnTop;
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

            foreach (Control ctrlMain in pnlNav.Controls)
            {
                foreach (Control ctrlNav in ctrlMain.Controls)
                {
                    if (ctrlNav.Tag != null)
                    {
                        if (ctrlNav.Tag.ToString().Equals("navButton"))
                        {
                            if (ctrlNav != selectedButton)
                            {
                                ctrlNav.ForeColor = Color.FromArgb(204, 204, 204);
                                //navigationControls.BackColor = Color.FromArgb(26, 26, 26);
                                //navigationControls.BackColor = Color.FromArgb(32, 32, 32);
                                ctrlNav.BackColor = Color.FromArgb(29, 32, 36);                        
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
            Close();
        }
        
        private void Logout()
        {
            FormvIDsafe.Main.User.Logout();
            FormvIDsafe.Main.Show();
        }

        private bool LoggedIn()
        {
            if (FormvIDsafe.Main.User.Name.Equals(""))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void FormHome_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (LoggedIn())
            {
                if (Settings.Default.HideToTrayClose)
                {
                    e.Cancel = true;
                    HideToTray(true);
                }
                else
                {
                    Logout();
                }
            }
        }

        private void FormHome_Resize(object sender, EventArgs e)
        {
            if (WindowState.Equals(FormWindowState.Minimized))
            {
                if (Settings.Default.HideToTrayMinimize)
                {
                    HideToTray(true);
                }
            }
        }

        private void HideToTray(bool hide)
        {
            notifyIcon.Visible = hide;
            notifyIcon.Icon = SystemIcons.Application;

            if (hide)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HideToTray(false);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void generateAPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateAndCopyPassword();
        }

        private void GenerateAndCopyPassword()
        {
            string password = CredentialGeneration.GeneratePassword();
            Clipboard.SetText(password);

            FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Passwords, password.ToString());
        }

        private void alwaysOnTopToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AlwaysOnTop = alwaysOnTopToolStripMenuItem.Checked;
            Settings.Default.Save();

            TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }

        private void onCloseToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.HideToTrayClose = onCloseToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void onMinimizeToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.HideToTrayMinimize = onMinimizeToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void onStartToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.HideToTrayStart = onStartToolStripMenuItem.Checked;
            Settings.Default.Save();
        }
    }
}
