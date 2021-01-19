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
            //TODO: OPTIMIZE! Maybe add a maximum num of passwords
            lvPasswordHistory.Items.Clear();
            foreach (KeyValuePair<DateTime, string> password in FormvIDsafe.Main.User.Vault.GetLogs(Vault.LogType.Passwords))
            {
                ListViewItem lvi = new ListViewItem("");
                lvi.SubItems.Add(password.Key.ToString());
                lvi.SubItems.Add(password.Value);
                lvPasswordHistory.Items.Add(lvi);
            }
        }

        private void btnRegenerate_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void GeneratePassword()
        {
            lblGeneratedPassword.Text = CredentialGeneration.GeneratePassword();

            GetPasswordHistory();
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
