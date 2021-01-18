using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormGeneratePassword : Form
    {
        public FormGeneratePassword()
        {
            InitializeComponent();
        }

        private void btnRegenerate_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void GeneratePassword()
        {
            CredentialGeneration.PasswordLength = tbPasswordLength.Value;
            CredentialGeneration.PassPhrase = rbPassphrase.Checked;

            lblGeneratedPassword.Text = CredentialGeneration.GeneratePassword();

            //TODO: ENABLE CHECKBOXES FOR CHARACTERS AND HAVE ATLEAST ONE OF THEM ENABLED
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
            lblLength.Text = tbPasswordLength.Value.ToString();
            GeneratePassword();
        }

        private void rbPassword_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void rbPassphrase_CheckedChanged(object sender, EventArgs e)
        {
            GeneratePassword();

            if (rbPassphrase.Checked)
            {
                clbSettings.Enabled = false;
            }
            else
            {
                clbSettings.Enabled = true;
            }
        }
    }
}
