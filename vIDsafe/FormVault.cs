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

            EnableIdentityComponents(-1);
            EnableCredentialComponents(-1);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCredential(cmbIdentity.SelectedItem.ToString(), lvCredentials.SelectedItems.Count);
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCredentials(cmbIdentity.SelectedItem.ToString());

            EnableIdentityComponents(cmbIdentity.SelectedIndex);
        }

        private void lvCredentials_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCredentialDetails(cmbIdentity.SelectedItem.ToString(), lvCredentials.SelectedItems.Count);

            EnableCredentialComponents(lvCredentials.SelectedItems.Count);
        }

        private void txtSearchCredential_TextChanged(object sender, EventArgs e)
        {
            SearchCredentials(cmbIdentity.SelectedItem.ToString(), txtSearchCredential.Text);

            EnableCredentialComponents(lvCredentials.SelectedItems.Count);
        }

        /// <summary>
        /// Generates a username based on an identity name
        /// </summary>
        private void GenerateUsername(string email)
        {
            Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

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
        private void GenerateCredential(string email)
        {
            Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

            Credential credential = identity.GenerateCredential();

            DisplayCredential(credential);

            int lastIndex = lvCredentials.Items.Count - 1;

            lvCredentials.Items[lastIndex].Selected = true;
        }

        /// <summary>
        /// Sets the details of a credential
        /// </summary>
        private void SetCredentialDetails(string email, int selectedCredentialCount, string username, string password, string URL, string Notes)
        {
            if (CredentialValidator.IsValid(URL, username, password))
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem selectedCredential = lvCredentials.SelectedItems[0];
                    string credentialID = selectedCredential.SubItems[0].Text;

                    Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

                    Credential credential = identity.TryGetCredential(credentialID);

                    credential.Username = username;
                    credential.Password = password;
                    credential.URL = URL;
                    credential.Notes = Notes;

                    lvCredentials.SelectedItems[0].SubItems[1].Text = username;
                    lvCredentials.SelectedItems[0].SubItems[2].Text = URL;
                    lvCredentials.SelectedItems[0].SubItems[3].Text = credential.Status.ToString();
                }
            }
        }

        /// <summary>
        /// Searches for a credential
        /// </summary>
        private void SearchCredentials(string email, string searchedText)
        {
            GetCredentials(email);

            if (searchedText.Length > 0)
            {
                foreach (ListViewItem lvi in lvCredentials.Items)
                {
                    string username = lvi.SubItems[1].Text;

                    if (username.IndexOf(searchedText, StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        lvCredentials.Items.Remove(lvi);
                    }
                }
            }
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
        }

        /// <summary>
        /// Gets the credentials for an identity
        /// </summary>
        private void GetCredentials(string email)
        {
            Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);
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
        private void GetCredentialDetails(string email, int selectedCredentialCount)
        {
            if (selectedCredentialCount > 0)
            {
                ListViewItem selectedCredential = lvCredentials.SelectedItems[0];
                string credentialID = selectedCredential.SubItems[0].Text;

                Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

                Credential credential = identity.TryGetCredential(credentialID);

                txtURL.Text = credential.URL;
                txtUsername.Text = credential.Username;
                txtPassword.Text = credential.Password;
                txtNotes.Text = credential.Notes;
            }
        }

        /// <summary>
        /// Deletes a credential
        /// </summary>
        private void DeleteCredential(string email, int selectedCredentialCount)
        {
            if (email.Length > 0)
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem currentItem = lvCredentials.SelectedItems[0];
                    string currentCredentialID = currentItem.SubItems[0].Text;

                    Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

                    identity.TryDeleteCredential(currentCredentialID);

                    lvCredentials.Items.RemoveAt(currentItem.Index);
                }
            }
        }

        /// <summary>
        /// Enables or disables form's identity components
        /// </summary>
        private void EnableIdentityComponents(int selectedIdentityIndex)
        {
            bool enabled = false;

            if (selectedIdentityIndex >= 0)
            {
                enabled = true;
            }
            else
            {
                ClearInputs();
            }

            btnNewCredential.Enabled = enabled;
            pnlVaultComponents.Visible = enabled;
        }

        /// <summary>
        /// Enables or disables form's credential components
        /// </summary>
        private void EnableCredentialComponents(int selectedCredentialCount)
        {
            bool enabled = false;

            if (selectedCredentialCount > 0)
            {
                enabled = true;
            }
            else
            {
                ClearInputs();
            }

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
        private void DeleteAllCredentials(string email)
        {
            if (email.Length > 0)
            {
                Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

                identity.DeleteAllCredentials();

                NotificationManager.ShowInfo("Credential deletion", "Successfully deleted all credentials");

                GetCredentials(email);
            }
        }
    }
}
