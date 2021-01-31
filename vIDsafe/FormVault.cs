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
        }

        private void btnGenerateUsername_Click(object sender, EventArgs e)
        {
            GenerateUsername(cmbIdentity.SelectedItem.ToString());
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void btnNewCredential_Click(object sender, EventArgs e)
        {
            NewCredential(cmbIdentity.SelectedItem.ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetCredentialDetails(cmbIdentity.SelectedItem.ToString(), lvCredentials.SelectedItems.Count, txtUsername.Text, txtPassword.Text, txtURL.Text, txtNotes.Text);
        }

        private bool IsValid(string URL, string username, string password)
        {
            if (URL.Length > 0 && username.Length > 0 && password.Length > 0)
            {
                bool result = Uri.TryCreate(URL, UriKind.Absolute, out Uri uriResult)
                    && (uriResult.Scheme.Equals(Uri.UriSchemeHttp) || uriResult.Scheme.Equals(Uri.UriSchemeHttps));

                if (result)
                {
                    return true;
                }
                else
                {
                    FormvIDsafe.ShowError("Validation error", "Please check your URL");
                    return false;
                }
            }
            else
            {
                FormvIDsafe.ShowError("Validation error", "Please enter all details");
                return false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCredential(cmbIdentity.SelectedItem.ToString(), lvCredentials.SelectedItems.Count);
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCredentials(cmbIdentity.SelectedItem.ToString());
        }

        private void lvCredentials_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCredentialDetails(cmbIdentity.SelectedItem.ToString(), lvCredentials.SelectedItems.Count);
        }

        private void txtSearchCredential_TextChanged(object sender, EventArgs e)
        {
            SearchCredentials(cmbIdentity.SelectedItem.ToString(), txtSearchCredential.Text);
        }

        private void GenerateUsername(string selectedEmail)
        {
            Identity identity = FormvIDsafe.Main.User.Vault.Identities[selectedEmail];

            txtUsername.Text = CredentialGeneration.GenerateUsername(identity.Name);
        }

        private void GeneratePassword()
        {
            txtPassword.Text = CredentialGeneration.GeneratePassword();
        }

        private void NewCredential(string selectedEmail)
        {
            Identity identity = FormvIDsafe.Main.User.Vault.Identities[selectedEmail];

            int credentialCount = lvCredentials.Items.Count;

            string credentialID = identity.NewCredential(identity.Name);

            Credential credential = identity.Credentials[credentialID];

            DisplayCredential(credentialID, credential);
            lvCredentials.Items[credentialCount].Selected = true;
        }

        private void SetCredentialDetails(string selectedEmail, int selectedCredentialCount, string credentialUsername, string credentialPassword, string credentialURL, string credentialNotes)
        {
            if (IsValid(credentialURL, credentialUsername, credentialPassword))
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem selectedCredential = lvCredentials.SelectedItems[0];
                    string credentialID = selectedCredential.SubItems[0].Text;

                    Identity identity = FormvIDsafe.Main.User.Vault.Identities[selectedEmail];

                    Credential credential = identity.Credentials[credentialID];

                    credential.SetDetails(credentialUsername, credentialPassword, credentialURL, credentialNotes);

                    lvCredentials.SelectedItems[0].SubItems[1].Text = credentialUsername;
                    lvCredentials.SelectedItems[0].SubItems[2].Text = credentialURL;
                    lvCredentials.SelectedItems[0].SubItems[3].Text = credential.Status.ToString();
                }
            }
        }

        private void SearchCredentials(string selectedEmail, string searchedText)
        {
            if (searchedText.Length > 0)
            {
                Identity identity = FormvIDsafe.Main.User.Vault.Identities[selectedEmail];

                Dictionary<string, Credential> credentials = identity.Credentials;

                credentials = credentials.Where(pair => pair.Value.Username.ToLower().Contains(searchedText.ToLower().Trim())).ToDictionary(pair => pair.Key, pair => pair.Value);

                credentials = credentials.Where(pair => pair.Value.Username.IndexOf(searchedText, StringComparison.OrdinalIgnoreCase) >= 0).ToDictionary(pair => pair.Key, pair => pair.Value);

                DisplayCredentials(credentials);
            }
            else
            {
                GetCredentials(selectedEmail);
            }

            ResetDetails();
        }

        private void GetIdentities()
        {
            foreach (KeyValuePair<string, Identity> identityPair in FormvIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identityPair.Key);
            }

            ResetDetails();
        }

        private void GetCredentials(string selectedEmail)
        {
            Identity identity = FormvIDsafe.Main.User.Vault.Identities[selectedEmail];
            identity.CalculateHealthScore();

            Dictionary<string, Credential> credentials = identity.Credentials;

            DisplayCredentials(credentials);
        }

        private void DisplayCredentials(Dictionary<string, Credential> credentials)
        {
            lvCredentials.Items.Clear();
            foreach (KeyValuePair<string, Credential> credential in credentials)
            {
                DisplayCredential(credential.Key, credential.Value);
            }

            ResetDetails();
        }

        private void DisplayCredential(string credentialID, Credential credential)
        {
            ListViewItem lvi = new ListViewItem(credentialID);
            lvi.SubItems.Add(credential.Username);
            lvi.SubItems.Add(credential.URL);
            lvi.SubItems.Add(credential.Status.ToString());

            lvCredentials.Items.Add(lvi);
        }

        private void GetCredentialDetails(string selectedEmail, int selectedCredentialCount)
        {
            if (selectedCredentialCount > 0)
            {
                ListViewItem selectedCredential = lvCredentials.SelectedItems[0];
                string credentialID = selectedCredential.SubItems[0].Text;

                Identity identity = FormvIDsafe.Main.User.Vault.Identities[selectedEmail];

                Credential credential = identity.Credentials[credentialID];

                txtURL.Text = credential.URL;
                txtUsername.Text = credential.Username;
                txtPassword.Text = credential.Password;
                txtNotes.Text = credential.Notes;
            }

            ResetDetails();
        }

        private void DeleteCredential(string selectedEmail, int selectedCredentialCount)
        {
            if (selectedEmail.Length > 0)
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem currentItem = lvCredentials.SelectedItems[0];
                    string currentCredentialID = currentItem.SubItems[0].Text;

                    Identity identity = FormvIDsafe.Main.User.Vault.Identities[selectedEmail];

                    identity.DeleteCredential(currentCredentialID);

                    lvCredentials.Items.RemoveAt(currentItem.Index);
                }
            }
        }

        private void ResetDetails()
        {
            int selectedIdentityIndex = cmbIdentity.SelectedIndex;
            int selectedCredentialCount = lvCredentials.SelectedItems.Count;

            if (selectedIdentityIndex >= 0)
            {
                EnableIdentityComponents(true);

                FixColumnWidths();
            }
            else
            {
                ClearInputs();
                EnableIdentityComponents(false);
            }

            if (selectedCredentialCount > 0)
            {
                EnableCredentialComponents(true);
            }
            else
            {
                ClearInputs();
                EnableCredentialComponents(false);
            }
        }

        private void EnableIdentityComponents(bool enabled)
        {
            btnNewCredential.Enabled = enabled;

            pnlVaultComponents.Visible = enabled;
        }

        private void EnableCredentialComponents(bool enabled)
        {
            txtURL.Enabled = enabled;
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            txtNotes.Enabled = enabled;

            btnSave.Enabled = enabled;
            btnDelete.Enabled = enabled;

            btnGenerateUsername.Enabled = enabled;
            btnGeneratePassword.Enabled = enabled;
        }

        private void ClearInputs()
        {
            txtURL.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtNotes.Clear();
        }

        private void lvCredentials_Resize(object sender, EventArgs e)
        {
            FixColumnWidths();
        }

        private void FixColumnWidths()
        {
            lvCredentials.Columns[1].Width = lvCredentials.Width / (lvCredentials.Columns.Count - 1);
            lvCredentials.Columns[2].Width = lvCredentials.Width / (lvCredentials.Columns.Count - 1);
            lvCredentials.Columns[3].Width = lvCredentials.Width / (lvCredentials.Columns.Count - 1);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetCredentials(cmbIdentity.SelectedItem.ToString());
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DeleteCredential(cmbIdentity.SelectedItem.ToString());
        }

        private void DeleteCredential(string selectedEmail)
        {
            if (selectedEmail.Length > 0)
            {
                Identity identity = FormvIDsafe.Main.User.Vault.Identities[selectedEmail];

                identity.DeleteAllCredentials();

                GetCredentials(selectedEmail);
            }
        }
    }
}
