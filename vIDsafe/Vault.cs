using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class Vault : Form
    {
        public Vault()
        {
            InitializeComponent();

            LoadFormComponents();
        }

        private void LoadFormComponents()
        {
            GetIdentities();
            EnableDisableInputs();
        }


        private void btnGenerateUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {

        }

        private void btnNewCredential_Click(object sender, EventArgs e)
        {
            string defaultIdentityName = "Credential " + (lstCredentials.Items.Count + 1);

            vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).NewCredential(defaultIdentityName);

            int lastIndex = lstCredentials.Items.Add(defaultIdentityName);
            lstCredentials.SelectedIndex = lastIndex;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).GetCredential(lstCredentials.SelectedIndex).SetDetails(txtUsername.Text, txtPassword.Text, txtURL.Text, txtNotes.Text);

            lstCredentials.Items[lstCredentials.SelectedIndex] = txtUsername.Text + "  " + txtURL.Text;
        }

        private void btnDeleteDiscard_Click(object sender, EventArgs e)
        {
            DeleteCredential();
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableInputs();
            GetCredentials();
        }

        private void lstCredentials_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableInputs();
            GetCredentialDetails();
        }

        private void GetIdentities()
        {
            foreach (Identity identity in vIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identity.Name);
            }
        }

        private void GetCredentials()
        {
            lstCredentials.Items.Clear();
            foreach (Credential credential in vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).Credentials)
            {
                lstCredentials.Items.Add(credential.Username + " " + credential.URL);
            }
        }

        private void GetCredentialDetails()
        {
            if (lstCredentials.SelectedIndex >= 0)
            {
                Credential currentCredential = vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).GetCredential(lstCredentials.SelectedIndex);

                txtURL.Text = currentCredential.URL;
                txtUsername.Text = currentCredential.Username;
                txtPassword.Text = currentCredential.Password;
                txtNotes.Text = currentCredential.Notes;
            }
        }

        private void DeleteCredential()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                if (lstCredentials.SelectedIndex >= 0)
                {
                    vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).DeleteCredential(lstCredentials.SelectedIndex);

                    lstCredentials.Items.RemoveAt(lstCredentials.SelectedIndex);
                }
            }
        }

        private void EnableDisableInputs()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                btnNewCredential.Enabled = true;
            }
            else
            {
                btnNewCredential.Enabled = false;
            }

            if (lstCredentials.SelectedIndex >= 0)
            {
                txtURL.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
                txtNotes.Enabled = true;

                btnSave.Enabled = true;
                btnDeleteDiscard.Enabled = true;

                btnGenerateUsername.Enabled = true;
                btnGeneratePassword.Enabled = true;
            }
            else
            {
                txtURL.Clear();
                txtUsername.Clear();
                txtPassword.Clear();
                txtNotes.Clear();
                cmbIdentity.Text = "";

                txtURL.Enabled = false;
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
                txtNotes.Enabled = false;

                btnSave.Enabled = false;
                btnDeleteDiscard.Enabled = false;

                btnGenerateUsername.Enabled = false;
                btnGeneratePassword.Enabled = false;
            }
        }
    }
}
