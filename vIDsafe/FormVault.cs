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
            GenerateUsername(cmbIdentity.Text);
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            GeneratePassword();
        }

        private void btnNewCredential_Click(object sender, EventArgs e)
        {
            NewCredential(cmbIdentity.SelectedIndex);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetCredentialDetails(cmbIdentity.SelectedIndex, lvCredentials.SelectedItems.Count, txtUsername.Text, txtPassword.Text, txtURL.Text, txtNotes.Text);
        }

        private bool IsValid(string URL, string username, string password)
        {
            if (URL.Length > 0 && username.Length > 0 && password.Length > 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("Please enter all details");
                return false;
            }
        }

        private void btnDeleteDiscard_Click(object sender, EventArgs e)
        {
            DeleteCredential(cmbIdentity.SelectedIndex, lvCredentials.SelectedItems.Count);
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCredentials(cmbIdentity.SelectedIndex);
        }

        private void lvCredentials_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCredentialDetails(cmbIdentity.SelectedIndex, lvCredentials.SelectedItems.Count);
        }

        private void txtSearchCredential_TextChanged(object sender, EventArgs e)
        {
            SearchCredentials(cmbIdentity.SelectedIndex, txtSearchCredential.Text);
        }

        private void GenerateUsername(string identityName)
        {
            txtUsername.Text = CredentialGeneration.GenerateUsername(identityName);
        }

        private void GeneratePassword()
        {
            txtPassword.Text = CredentialGeneration.GeneratePassword();
        }

        private void NewCredential(int selectedIdentityIndex)
        {
            string identityName = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).Name;

            string defaultUsername = CredentialGeneration.GenerateUsername(identityName);
            string defaultPassword = CredentialGeneration.GeneratePassword();

            int credentialCount = lvCredentials.Items.Count;

            string credentialID = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).NewCredential(defaultUsername, defaultPassword);

            FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).ManualCheckStatus(credentialID);

            Credential credential = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).GetCredential(credentialID);

            DisplayCredential(credentialID, credential);
            lvCredentials.Items[credentialCount].Selected = true;
        }

        private void SetCredentialDetails(int selectedIdentityIndex, int selectedCredentialCount, string credentialUsername, string credentialPassword, string credentialURL, string credentialNotes)
        {
            if (IsValid(credentialURL, credentialUsername, credentialPassword))
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem currentItem = lvCredentials.SelectedItems[0];
                    string currentCredentialID = currentItem.SubItems[0].Text;

                    FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).GetCredential(currentCredentialID).SetDetails(credentialUsername, credentialPassword, credentialURL, credentialNotes);
                    FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).ManualCheckStatus(currentCredentialID);

                    Credential.CredentialStatus status = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).GetCredential(currentCredentialID).Status;

                    lvCredentials.SelectedItems[0].SubItems[1].Text = credentialUsername;
                    lvCredentials.SelectedItems[0].SubItems[2].Text = credentialURL;
                    lvCredentials.SelectedItems[0].SubItems[3].Text = status.ToString();
                }
            }
        }

        private void SearchCredentials(int selectedIdentityIndex, string searchedText)
        {
            if (searchedText.Length > 0)
            {
                Dictionary<string, Credential> credentials = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).Credentials;

                credentials = credentials.Where(pair => pair.Value.Username.ToLower().Contains(searchedText.ToLower().Trim())).ToDictionary(pair => pair.Key, pair => pair.Value);

                DisplayCredentials(credentials);
            }
            else
            {
                GetCredentials(selectedIdentityIndex);
            }

            ResetDetails();
        }

        private void GetIdentities()
        {
            foreach (Identity identity in FormvIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identity.Name);
            }

            ResetDetails();
        }

        private void GetCredentials(int selectedIdentityIndex)
        {
            FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).CalculateHealthScore();
            Dictionary<string, Credential> credentials = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).Credentials;

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

        private void GetCredentialDetails(int selectedIdentityIndex, int selectedCredentialCount)
        {
            if (selectedCredentialCount > 0)
            {
                ListViewItem currentItem = lvCredentials.SelectedItems[0];
                string currentCredentialID = currentItem.SubItems[0].Text;

                Credential credential = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).GetCredential(currentCredentialID);

                txtURL.Text = credential.URL;
                txtUsername.Text = credential.Username;
                txtPassword.Text = credential.Password;
                txtNotes.Text = credential.Notes;
            }

            ResetDetails();
        }

        private void DeleteCredential(int selectedIdentityIndex, int selectedCredentialCount)
        {
            if (selectedIdentityIndex >= 0)
            {
                if (selectedCredentialCount > 0)
                {
                    ListViewItem currentItem = lvCredentials.SelectedItems[0];
                    string currentCredentialID = currentItem.SubItems[0].Text;

                    FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).DeleteCredential(currentCredentialID);

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
            txtSearchCredential.Visible = enabled;

            lvCredentials.Visible = enabled;
            lvCredentials.BringToFront();
        }

        private void EnableCredentialComponents(bool enabled)
        {
            txtURL.Enabled = enabled;
            txtUsername.Enabled = enabled;
            txtPassword.Enabled = enabled;
            txtNotes.Enabled = enabled;

            btnSave.Enabled = enabled;
            btnDeleteDiscard.Enabled = enabled;

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
            GetCredentials(cmbIdentity.SelectedIndex);
        }
    }
}
