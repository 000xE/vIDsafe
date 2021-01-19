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
            lvPasswordHistory.Items.Clear();
            foreach (KeyValuePair<DateTime, string> password in FormvIDsafe.Main.User.Vault.GetLogs(Vault.LogType.Passwords))
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
            //https://stackoverflow.com/questions/12899876/checking-strings-for-a-strong-enough-password
            //https://www.ryadel.com/en/passwordcheck-c-sharp-password-class-calculate-password-strength-policy-aspnet/
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyPassword();
        }

        private void CopyPassword()
        {
            Clipboard.SetText(lblGeneratedPassword.Text);
        }

        private void tbPasswordLength_Scroll(object sender, EventArgs e)
        {
            CredentialGeneration.PasswordLength = tbPasswordLength.Value;
            lblLength.Text = tbPasswordLength.Value.ToString();
        }

        private void rbPassword_CheckedChanged(object sender, EventArgs e)
        {
            clbSettings.Enabled = rbPassword.Checked;
        }

        private void rbPassphrase_CheckedChanged(object sender, EventArgs e)
        {
            CredentialGeneration.PassPhrase = rbPassphrase.Checked;
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
                    Console.WriteLine(e.NewValue);
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

            tbPasswordLength.Value = CredentialGeneration.PasswordLength;

            rbPassphrase.Checked = CredentialGeneration.PassPhrase;

            lblLength.Text = tbPasswordLength.Value.ToString();
        }

        private void SetSettings(int checkboxIndex, bool value)
        {
            CredentialGeneration.PasswordSettings[checkboxIndex] = value;
        }
    }
}
