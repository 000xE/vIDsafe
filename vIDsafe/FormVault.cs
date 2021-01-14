using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormVault : Form
    {
        public FormVault()
        {
            InitializeComponent();

            LoadFormComponents();
        }

        private void LoadFormComponents()
        {
            GetIdentities();
            EnableDisableDetails();
        }

        private void btnGenerateUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {

        }

        private void btnNewCredential_Click(object sender, EventArgs e)
        {
            NewCredential();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCredential();
        }

        private void btnDeleteDiscard_Click(object sender, EventArgs e)
        {
            DeleteCredential();
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableDetails();
            GetCredentials();
        }

        private void lvCredentials_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableDisableDetails();
            GetCredentialDetails();
        }

        private void txtSearchCredential_TextChanged(object sender, EventArgs e)
        {
            SearchCredentials();
        }

        private void NewCredential()
        {
            string defaultIdentityName = "New credential";

            int selectedIdentityIndex = cmbIdentity.SelectedIndex;
            int credentialCount = lvCredentials.Items.Count;

            FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).NewCredential(defaultIdentityName);

            DisplayCredential(credentialCount, defaultIdentityName, "", Credential.CredentialStatus.Safe);
            lvCredentials.Items[credentialCount].Selected = true;
        }

        private void SaveCredential()
        {
            int selectedCredentialCount = lvCredentials.SelectedItems.Count;

            if (selectedCredentialCount > 0)
            {
                string credentialUsername = txtUsername.Text;
                string credentialPassword = txtPassword.Text;
                string credentialURL = txtURL.Text;
                string credentialNotes = txtNotes.Text;

                ListViewItem currentItem = lvCredentials.SelectedItems[0];
                int currentCredentialIndex = Convert.ToInt32(currentItem.SubItems[0].Text);
                int selectedIdentityIndex = cmbIdentity.SelectedIndex;

                FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).GetCredential(currentCredentialIndex).SetDetails(credentialUsername, credentialPassword, credentialURL, credentialNotes);
                lvCredentials.Items[currentCredentialIndex].SubItems[1].Text = credentialUsername;
                lvCredentials.Items[currentCredentialIndex].SubItems[2].Text = credentialURL;
            }
        }

        private void SearchCredentials()
        {
            string searchedText = txtSearchCredential.Text;

            if (searchedText.Length > 0)
            {
                int selectedIdentityIndex = cmbIdentity.SelectedIndex;
                List<Credential> credentials = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).Credentials;
                credentials = credentials.FindAll(crd => crd.Username.ToLower().Contains(searchedText.ToLower().Trim()));

                DisplayCredentials(credentials);
            }
            else
            {
                GetCredentials();
            }
        }

        private void GetIdentities()
        {
            foreach (Identity identity in FormvIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identity.Name);
            }
        }

        private void GetCredentials()
        {
            int selectedIdentityIndex = cmbIdentity.SelectedIndex;

            List<Credential> credentials = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).Credentials;

            DisplayCredentials(credentials);
        }

        private void DisplayCredentials(List<Credential> credentials)
        {
            lvCredentials.Items.Clear();
            foreach (Credential credential in credentials)
            {
                int currentCredentialIndex = credentials.IndexOf(credential);

                DisplayCredential(currentCredentialIndex, credential.Username, credential.URL, credential.Status);
            }
        }

        private void DisplayCredential(int index, string username, string url, Credential.CredentialStatus status)
        {
            ListViewItem lvi = new ListViewItem(index.ToString());
            lvi.SubItems.Add(username);
            lvi.SubItems.Add(url);
            lvi.SubItems.Add(status.ToString());

            lvCredentials.Items.Add(lvi);
        }

        private void GetCredentialDetails()
        {
            int selectedCredentialCount = lvCredentials.SelectedItems.Count;

            if (selectedCredentialCount > 0)
            {
                ListViewItem currentItem = lvCredentials.SelectedItems[0];
                int currentCredentialIndex = Convert.ToInt32(currentItem.SubItems[0].Text);
                int selectedIdentityIndex = cmbIdentity.SelectedIndex;

                Credential currentCredential = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).GetCredential(currentCredentialIndex);

                txtURL.Text = currentCredential.URL;
                txtUsername.Text = currentCredential.Username;
                txtPassword.Text = currentCredential.Password;
                txtNotes.Text = currentCredential.Notes;
            }
        }

        private void DeleteCredential()
        {
            int selectedIdentityIndex = cmbIdentity.SelectedIndex;
            int selectedCredentialCount = lvCredentials.SelectedItems.Count;

            if (selectedIdentityIndex >= 0)
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem currentItem = lvCredentials.SelectedItems[0];
                    int currentCredentialIndex = Convert.ToInt32(currentItem.SubItems[0].Text);

                    FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).DeleteCredential(currentCredentialIndex);

                    lvCredentials.Items.RemoveAt(currentItem.Index);

                    GetCredentials(); //To update the indexes
                }
            }
        }

        private void EnableDisableDetails()
        {
            ClearInputs();

            int selectedIdentityIndex = cmbIdentity.SelectedIndex;
            int selectedCredentialCount = lvCredentials.SelectedItems.Count;

            if (selectedIdentityIndex >= 0)
            {
                btnNewCredential.Enabled = true;
            }
            else
            {
                btnNewCredential.Enabled = false;
            }

            if (selectedCredentialCount > 0)
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

        private void ClearInputs()
        {
            txtURL.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtNotes.Clear();
            cmbIdentity.Text = "";
        }
    }
}
