using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Concurrent;

namespace vIDsafe
{
    public partial class FormVault : Form
    {
        public FormVault()
        {
            InitializeComponent();

            InitialMethods();
        }

        /// <summary>
        /// Initial methods to run when the form starts
        /// </summary>
        private void InitialMethods()
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
            GenerateCredential(cmbIdentity.SelectedItem.ToString());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetCredentialDetails(cmbIdentity.SelectedItem.ToString(), lvCredentials.SelectedItems.Count, txtUsername.Text, txtPassword.Text, txtURL.Text, txtNotes.Text);
        }

        /// <summary>
        /// Checks if the URL, username and password are valid
        /// </summary>
        /// <returns>
        /// True if valid, false if not
        /// </returns>
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
                    FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please check your URL");
                    return false;
                }
            }
            else
            {
                FormvIDsafe.ShowNotification(ToolTipIcon.Error, "Validation error", "Please enter all details");
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

        /// <summary>
        /// Generates a username based on an identity name
        /// </summary>
        private void GenerateUsername(string selectedEmail)
        {
            Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);

            txtUsername.Text = CredentialGeneration.GenerateUsername(identity.Name);
        }

        /// <summary>
        /// Generates a password
        /// </summary>
        private void GeneratePassword()
        {
            txtPassword.Text = CredentialGeneration.GeneratePassword();
        }

        /// <summary>
        /// Generates a credential for an identity
        /// </summary>
        private void GenerateCredential(string selectedEmail)
        {
            Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);

            Credential credential = identity.GenerateCredential();

            DisplayCredential(credential);

            int lastIndex = lvCredentials.Items.Count - 1;

            lvCredentials.Items[lastIndex].Selected = true;
        }

        /// <summary>
        /// Sets the details of a credential
        /// </summary>
        private void SetCredentialDetails(string selectedEmail, int selectedCredentialCount, string credentialUsername, string credentialPassword, string credentialURL, string credentialNotes)
        {
            if (IsValid(credentialURL, credentialUsername, credentialPassword))
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem selectedCredential = lvCredentials.SelectedItems[0];
                    string credentialID = selectedCredential.SubItems[0].Text;

                    Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);

                    Credential credential = identity.TryGetCredential(credentialID);

                    credential.Username = credentialUsername;
                    credential.Password = credentialPassword;
                    credential.URL = credentialURL;
                    credential.Notes = credentialNotes;

                    lvCredentials.SelectedItems[0].SubItems[1].Text = credentialUsername;
                    lvCredentials.SelectedItems[0].SubItems[2].Text = credentialURL;
                    lvCredentials.SelectedItems[0].SubItems[3].Text = credential.Status.ToString();
                }
            }
        }

        /// <summary>
        /// Searches for a credential
        /// </summary>
        private void SearchCredentials(string selectedEmail, string searchedText)
        {
            if (searchedText.Length > 0)
            {
                Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);

                ConcurrentDictionary<string, Credential> searchedCredentials = new ConcurrentDictionary<string, Credential>();

                foreach (KeyValuePair<string, Credential> credentialPair in identity.Credentials)
                {
                    if (credentialPair.Value.Username.IndexOf(searchedText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        searchedCredentials.TryAdd(credentialPair.Key, credentialPair.Value);
                    }
                }

                DisplayCredentials(searchedCredentials);
            }
            else
            {
                GetCredentials(selectedEmail);
            }

            ResetDetails();
        }

        /// <summary>
        /// Gets the identitie sin the vault
        /// </summary>
        private void GetIdentities()
        {
            foreach (KeyValuePair<string, Identity> identityPair in MasterAccount.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identityPair.Key);
            }

            ResetDetails();
        }

        /// <summary>
        /// Gets the credentials for an identity
        /// </summary>
        private void GetCredentials(string selectedEmail)
        {
            Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);
            identity.CalculateHealthScore(true);

            ConcurrentDictionary<string, Credential> credentials = identity.Credentials;

            DisplayCredentials(credentials);
        }

        /// <summary>
        /// Displays the credentials for an identity
        /// </summary>
        private void DisplayCredentials(ConcurrentDictionary<string, Credential> credentials)
        {
            lvCredentials.Items.Clear();
            foreach (KeyValuePair<string, Credential> credentialPair in credentials)
            {
                DisplayCredential(credentialPair.Value);
            }

            ResetDetails();
        }

        /// <summary>
        /// Display a specific credential
        /// </summary>
        private void DisplayCredential(Credential credential)
        {
            ListViewItem lvi = new ListViewItem(credential.CredentialID);
            lvi.SubItems.Add(credential.Username);
            lvi.SubItems.Add(credential.URL);
            lvi.SubItems.Add(credential.Status.ToString());

            lvCredentials.Items.Add(lvi);
        }

        /// <summary>
        /// Gets the details of a credential
        /// </summary>
        private void GetCredentialDetails(string selectedEmail, int selectedCredentialCount)
        {
            if (selectedCredentialCount > 0)
            {
                ListViewItem selectedCredential = lvCredentials.SelectedItems[0];
                string credentialID = selectedCredential.SubItems[0].Text;

                Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);

                Credential credential = identity.TryGetCredential(credentialID);

                txtURL.Text = credential.URL;
                txtUsername.Text = credential.Username;
                txtPassword.Text = credential.Password;
                txtNotes.Text = credential.Notes;
            }

            ResetDetails();
        }

        /// <summary>
        /// Deletes a credential
        /// </summary>
        private void DeleteCredential(string selectedEmail, int selectedCredentialCount)
        {
            if (selectedEmail.Length > 0)
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem currentItem = lvCredentials.SelectedItems[0];
                    string currentCredentialID = currentItem.SubItems[0].Text;

                    Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);

                    identity.TryDeleteCredential(currentCredentialID);

                    lvCredentials.Items.RemoveAt(currentItem.Index);
                }
            }
        }

        //Todo: maybe refactor the way this method gets called?
        /// <summary>
        /// Resets the form components
        /// </summary>
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

        /// <summary>
        /// Enables or disables form's identity components
        /// </summary>
        private void EnableIdentityComponents(bool enabled)
        {
            btnNewCredential.Enabled = enabled;

            pnlVaultComponents.Visible = enabled;
        }

        /// <summary>
        /// Enables or disables form's credential components
        /// </summary>
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

        /// <summary>
        /// Clears the input texts
        /// </summary>
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

        /// <summary>
        /// Fixes the column widths based on listview width and column count
        /// </summary>
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
            DialogResult result = MessageBox.Show("Are you sure you want to delete all credentials for this identity?", "Credential deletion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result.Equals(DialogResult.Yes))
            {
                DeleteAllCredentials(cmbIdentity.SelectedItem.ToString());
            }
        }

        /// <summary>
        /// Deletes all credentials for an identity
        /// </summary>
        private void DeleteAllCredentials(string selectedEmail)
        {
            if (selectedEmail.Length > 0)
            {
                Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);

                identity.DeleteAllCredentials();

                FormvIDsafe.ShowNotification(ToolTipIcon.Info, "Credential deletion", "Successfully deleted all credentials");

                GetCredentials(selectedEmail);
            }
        }
    }
}
