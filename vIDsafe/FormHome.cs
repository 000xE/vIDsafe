using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vIDsafe.Properties;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace vIDsafe
{
    public partial class FormHome : Form
    {
        private static Control.ControlCollection _ctrlsFormControls = null;

        private readonly List<Theme> _themes = new List<Theme>() { new DarkTheme(), new LightTheme() };

        private Theme _currentTheme;

        public FormHome()
        {
            InitializeComponent();
            InitialMethods();
        }

        /// <summary>
        /// Initial methods to run when the form starts
        /// </summary>
        private void InitialMethods()
        {
            GetFormComponents();
            GetSettings();
            SetName(FormvIDsafe.Main.User.Name);
            OpenChildForm(new FormOverview());
        }

        private void GetFormComponents()
        {
            _ctrlsFormControls = Controls;
        }

        //https://stackoverflow.com/questions/22935285/change-color-of-all-controls-inside-the-form-in-c-sharp/22935406#22935406
        /// <summary>
        /// Appends a form into a panel
        /// </summary>
        private void UpdateControlColors(Theme theme, Control control)
        {
            theme.SetControlColors(control);

            foreach (Control ctrlSub in control.Controls)
            {
                UpdateControlColors(theme, ctrlSub);
            }
        }

        /// <summary>
        /// Gets the form settings
        /// </summary>
        private void GetSettings()
        {
            onCloseToolStripMenuItem.Checked = Settings.Default.HideToTrayClose;
            onMinimizeToolStripMenuItem.Checked = Settings.Default.HideToTrayMinimize;
            onStartToolStripMenuItem.Checked = Settings.Default.HideToTrayStart;
            alwaysOnTopToolStripMenuItem.Checked = Settings.Default.AlwaysOnTop;

            foreach (Theme theme in _themes)
            {
                cmbTheme.Items.Add(theme.ToString());
            }

            cmbTheme.SelectedIndex = Settings.Default.Theme;
        }

        /// <summary>
        /// Sets the master account name
        /// </summary>
        public static void SetName(string name)
        {
            Label lblMasterAccountName = (Label)_ctrlsFormControls.Find("lblMAName", true)[0];
            lblMasterAccountName.Text = name;
        }

        /// <summary>
        /// Sets the overall health score
        /// </summary>
        public static void SetHealthScore(int healthScore, Color healthColor)
        {
            Panel pnlProgressBar = (Panel)_ctrlsFormControls.Find("pnlProgressBar", true)[0];
            pnlProgressBar.BackColor = healthColor;

            int panelWidth = (int)((double)pnlProgressBar.MaximumSize.Width / 100 * healthScore);

            pnlProgressBar.Size = new Size(panelWidth, pnlProgressBar.Height);

            Panel pnlProgressBack = (Panel)_ctrlsFormControls.Find("pnlProgressBack", true)[0];

            Label lblHealthScore = (Label)pnlProgressBack.Controls.Find("lblHealthScore", true)[0];

            lblHealthScore.Text = healthScore.ToString() + "%";
        }

        //https://stackoverflow.com/a/28811266
        /// <summary>
        /// Appends a form into a panel
        /// </summary>
        private void OpenChildForm(Form frmChildForm)
        {
            while (pnlChildForm.Controls.Count > 0)
            {
                pnlChildForm.Controls[0].Dispose();
            }

            frmChildForm.TopLevel = false;
            frmChildForm.FormBorderStyle = FormBorderStyle.None;
            frmChildForm.Dock = DockStyle.Fill;
            pnlChildForm.Enabled = true;
            pnlChildForm.Controls.Add(frmChildForm);
            pnlChildForm.Tag = frmChildForm;
            frmChildForm.BringToFront();
            frmChildForm.Show();

            UpdateControlColors(_currentTheme, frmChildForm);
        }

        /// <summary>
        /// Changes the selected navigation button
        /// </summary>
        private void ChangeSelectedButton(object sender)
        {
            Button selectedButton = (Button)sender;
            //selectedButton.BackColor = Color.FromArgb(47, 47, 47);
            selectedButton.Tag = "NavButton selected";

            _currentTheme.SetControlColors(selectedButton);

            //Todo: cleanup

            Panel pnlNav = (Panel) _ctrlsFormControls.Find("pnlNavigation", true)[0];

            foreach (Control ctrlMain in pnlNav.Controls)
            {
                foreach (Control ctrlNav in ctrlMain.Controls)
                {
                    if (ctrlNav != selectedButton)
                    {
                        ctrlNav.Tag = "NavButton";
                        //navigationControls.BackColor = Color.FromArgb(26, 26, 26);
                        //navigationControls.BackColor = Color.FromArgb(32, 32, 32);

                        _currentTheme.SetControlColors(ctrlNav);
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

        /// <summary>
        /// Logs out of the master account
        /// </summary>
        private void Logout()
        {
            FormvIDsafe.Main.User.Logout();
            FormvIDsafe.Main.Show();
            Close();
        }

        /// <summary>
        /// Checks if the user is logged in
        /// </summary>
        /// <returns>
        /// True if logged in, false if not
        /// </returns>
        private bool LoggedIn()
        {
            if (FormvIDsafe.Main.User.Vault == null)
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

        private void FormHome_Load(object sender, EventArgs e)
        {
            if (Settings.Default.HideToTrayStart)
            {
                //https://stackoverflow.com/questions/70272/single-form-hide-on-startup/189045#189045
                BeginInvoke(new MethodInvoker(delegate
                {
                    HideToTray(true);
                }));
            }
        }

        /// <summary>
        /// Hide to tray or show the form
        /// </summary>
        private void HideToTray(bool hide)
        {
            trayIcon.Visible = hide;
            trayIcon.Icon = SystemIcons.Application;

            if (hide)
            {
                Opacity = 0;
                Hide();
            }
            else
            {
                Opacity = 100;
                Show();
            }
        }

        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            HideToTray(false);
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout();
            Application.Exit();
        }

        private void generateAPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateAndCopyPassword();
        }

        /// <summary>
        /// Generates a password and copies it
        /// </summary>
        private void GenerateAndCopyPassword()
        {
            string password = CredentialGeneration.GeneratePassword();
            Clipboard.SetText(password);

            FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Passwords, password.ToString());

            FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Password", "Successfully generated and copied");
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

        private void cmbTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = cmbTheme.SelectedIndex;
            Settings.Default.Save();

            _currentTheme = _themes[cmbTheme.SelectedIndex];

            UpdateControlColors(_currentTheme, this);
        }
    }
}
