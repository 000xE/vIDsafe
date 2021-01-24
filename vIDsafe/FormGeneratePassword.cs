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
            LoadFormComponents();
        }

        private void LoadFormComponents()
        {
            GetPasswordHistory();
            GetSettings();
        }

        private void GetPasswordHistory()
        {
            Dictionary<DateTime, string> passwords = FormvIDsafe.Main.User.Vault.GetLogs(Vault.LogType.Passwords);

            DisplayPasswords(passwords);
        }

        private void DisplayPasswords(Dictionary<DateTime, string> passwords)
        {
            lvPasswordHistory.Items.Clear();

            foreach (KeyValuePair<DateTime, string> password in passwords)
            {
                DisplayPassword(password.Key, password.Value);
            }
        }

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

        private void GeneratePassword()
        {
            string password = CredentialGeneration.GeneratePassword();
            CheckStrength(password);

            KeyValuePair<DateTime, string> passwordLog = FormvIDsafe.Main.User.Vault.Log(Vault.LogType.Passwords, password.ToString());
            DisplayPassword(passwordLog.Key, passwordLog.Value);

            lblGeneratedPassword.Text = password;
        }

        private void CheckStrength(string password)
        {
            double score = CredentialGeneration.CheckStrength(password);

            double colorMultiplier = score / 100;

            Color color = Color.SpringGreen;

            Color newColor = Color.FromArgb((int) (color.R * colorMultiplier), (int) (color.G * colorMultiplier), (int) (color.B * colorMultiplier));

            panelPasswordStrength.BackColor = newColor;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyPassword(lblGeneratedPassword.Text);
        }

        private void CopyPassword(string password)
        {
            Clipboard.SetText(password);
        }

        private void tbPasswordLength_Scroll(object sender, EventArgs e)
        {
            if (rbPassphrase.Checked)
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
            if (e.NewValue == CheckState.Unchecked)
            {
                if (clbSettings.CheckedItems.Count == 1)
                {
                    e.NewValue = e.CurrentValue;
                }
            }
        }

        private void GetSettings()
        {
            foreach (KeyValuePair<int, bool> setting in CredentialGeneration.PasswordSettings)
            {
                clbSettings.SetItemChecked(setting.Key, setting.Value); 
            }

            rbPassphrase.Checked = CredentialGeneration.Passphrase;

            ResetPasswordLengths(rbPassphrase.Checked);
        }

        private void SetSettings(int checkboxIndex, bool value)
        {
            CredentialGeneration.PasswordSettings[checkboxIndex] = value;
        }
    }
}
