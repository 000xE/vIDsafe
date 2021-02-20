using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace vIDsafe
{
    public partial class FormGeneratePassword : Form
    {
        public FormGeneratePassword()
        {
            InitializeComponent();
            InitialMethods();
        }

        /// <summary>
        /// Initial methods to run when the form starts
        /// </summary>
        private void InitialMethods()
        {
            GetSettings();

            DisplayPasswordHistory();
        }

        /// <summary>
        /// Displays the password history
        /// </summary>
        private void DisplayPasswordHistory()
        {
            Dictionary<DateTime, string> passwords = MasterAccount.User.Vault.GetLogs(Vault.LogType.Passwords);

            lvPasswordHistory.Items.Clear();

            foreach (KeyValuePair<DateTime, string> password in passwords)
            {
                DisplayPassword(password.Key, password.Value);
            }
        }

        /// <summary>
        /// Displays a specific password
        /// </summary>
        private void DisplayPassword(DateTime dateTime, string password)
        {
            ListViewItem lvi = new ListViewItem("");
            lvi.SubItems.Add(dateTime.ToString());
            lvi.SubItems.Add(password);

            lvPasswordHistory.Items.Add(lvi);
        }

        private void btnRegenerate_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        /// <summary>
        /// Generate a password
        /// </summary>
        private void GeneratePassword()
        {
            string password = CredentialGeneration.GeneratePassword();
            CheckStrength(password);

            KeyValuePair<DateTime, string> passwordLog = MasterAccount.User.Vault.Log(Vault.LogType.Passwords, password.ToString());
            DisplayPassword(passwordLog.Key, passwordLog.Value);

            lblGeneratedPassword.Text = password;
        }

        /// <summary>
        /// Checks the strength of a password
        /// </summary>
        private void CheckStrength(string password)
        {
            double score = CredentialGeneration.CheckStrength(password);     

            panelPasswordStrength.BackColor = CalculateStrengthColor(score);
        }

        /// <summary>
        /// Calculates the strength panel colour based on the strength of a password
        /// </summary>
        private Color CalculateStrengthColor(double score)
        {
            double colorMultiplier = score / 100;

            Color color = Color.SpringGreen;

            Color newColor = Color.FromArgb((int)(color.R * colorMultiplier), (int)(color.G * colorMultiplier), (int)(color.B * colorMultiplier));

            return newColor;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyPassword(lblGeneratedPassword.Text);
        }

        /// <summary>
        /// Copies a password to clipboard
        /// </summary>
        private void CopyPassword(string password)
        {
            Clipboard.SetText(password);
            FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Password", "Successfully copied");
        }

        private void tbPasswordLength_Scroll(object sender, EventArgs e)
        {
            SetPasswordLengths(rbPassphrase.Checked);
        }

        private void SetPasswordLengths (bool passphrase)
        {
            if (passphrase)
            {
                CredentialGeneration.CurrentPassphraseLength = tbPasswordLength.Value;
            }
            else
            {
                CredentialGeneration.CurrentPasswordLength = tbPasswordLength.Value;
            }

            lblLength.Text = tbPasswordLength.Value.ToString();
        }

        private void rbPassword_CheckedChanged(object sender, EventArgs e)
        {
            clbSettings.Enabled = rbPassword.Checked;
        }

        private void rbPassphrase_CheckedChanged(object sender, EventArgs e)
        {
            ResetPasswordLengths(rbPassphrase.Checked);
        }

        /// <summary>
        /// Resets the password length values
        /// </summary>
        private void ResetPasswordLengths(bool passphrase)
        {
            CredentialGeneration.Passphrase = passphrase;

            if (passphrase)
            {
                tbPasswordLength.Minimum = CredentialGeneration.MinPassphraseLength;
                tbPasswordLength.Maximum = CredentialGeneration.MaxPassphraseLength;

                tbPasswordLength.Value = CredentialGeneration.CurrentPassphraseLength;
            }
            else
            {
                tbPasswordLength.Minimum = CredentialGeneration.MinPasswordLength;
                tbPasswordLength.Maximum = CredentialGeneration.MaxPasswordLength;

                tbPasswordLength.Value = CredentialGeneration.CurrentPasswordLength;
            }

            lblLength.Text = tbPasswordLength.Value.ToString();
        }

        private void clbSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSettings(clbSettings.SelectedIndex, clbSettings.GetItemChecked(clbSettings.SelectedIndex));
        }

        private void clbSettings_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.Equals(CheckState.Unchecked))
            {
                if (clbSettings.CheckedItems.Count.Equals(1))
                {
                    e.NewValue = e.CurrentValue;
                }
            }
        }

        /// <summary>
        /// Gets the password settings
        /// </summary>
        private void GetSettings()
        {
            foreach (KeyValuePair<int, bool> setting in CredentialGeneration.PasswordSettings)
            {
                clbSettings.SetItemChecked(setting.Key, setting.Value); 
            }

            rbPassphrase.Checked = CredentialGeneration.Passphrase;

            ResetPasswordLengths(rbPassphrase.Checked);
        }

        /// <summary>
        /// Sets the password settings
        /// </summary>
        private void SetSettings(int checkboxIndex, bool value)
        {
            CredentialGeneration.PasswordSettings[checkboxIndex] = value;
        }
    }
}
