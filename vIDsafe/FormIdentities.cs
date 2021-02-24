using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace vIDsafe
{
    public partial class FormIdentities : Form
    {
        public FormIdentities()
        {
            InitializeComponent();

            InitialMethods();
        }

        /// <summary>
        /// Initial methods to run when the form starts
        /// </summary>
        private void InitialMethods()
        {
            DisplayIdentities();

            EnableIdentityComponents(-1);
        }

        private void chartCredentials_PrePaint(object sender, ChartPaintEventArgs e)
        {
            DisplayCredentialCount(e);
        }

        /// <summary>
        /// Displays the total credential count of an identity
        /// </summary>
        private void DisplayCredentialCount(ChartPaintEventArgs e)
        {
            string selectedEmail = cmbIdentity.SelectedItem.ToString();

            if (selectedEmail.Length > 0)
            {
                Identity identity = MasterAccount.User.Vault.TryGetIdentity(selectedEmail);

                if (e.ChartElement is ChartArea)
                {
                    TextAnnotation ta = CreateTextAnnotation(Convert.ToString(identity.Credentials.Count), e);

                    chartCredentials.Annotations.Clear();
                    chartCredentials.Annotations.Add(ta);
                }
            }
        }

        /// <summary>
        /// Creates a custom text annotation
        /// </summary>
        /// <returns>
        /// The text anotation
        /// </returns>
        private TextAnnotation CreateTextAnnotation(string text, ChartPaintEventArgs e)
        {
            TextAnnotation ta = new TextAnnotation
            {
                Text = text,
                Width = e.Position.Width,
                Height = e.Position.Height,
                X = e.Position.X - (e.Position.Width / 100),
                Y = e.Position.Y + (e.Position.Height / 100),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.RoyalBlue,
            };

            return ta;
        }

        private void btnNewIdentity_Click(object sender, EventArgs e)
        {
            GenerateIdentity();
        }

        /// <summary>
        /// Generates an identity
        /// </summary>
        private void GenerateIdentity()
        {
            Identity identity = MasterAccount.User.Vault.GenerateIdentity();

            int lastIndex = cmbIdentity.Items.Add(identity.Email);
            cmbIdentity.SelectedIndex = lastIndex;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetIdentityDetails(cmbIdentity.SelectedIndex, cmbIdentity.SelectedItem.ToString(), txtIdentityName.Text, txtIdentityEmail.Text.ToLower(), txtIdentityUsage.Text);
        }

        /// <summary>
        /// Sets the details of an identity
        /// </summary>
        private void SetIdentityDetails(int selectedIdentityIndex, string email, string name, string newEmail, string usage)
        {
            if (IdentityValidator.IsIdentityValid(name, newEmail))
            {
                Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

                if (email != newEmail)
                {
                    if (TryChangeIdentityEmail(identity, newEmail))
                    {
                        cmbIdentity.Items[selectedIdentityIndex] = newEmail;
                        GetBreachedDataAsync(newEmail, true);
                    }
                }

                identity.Name = name;
                identity.Usage = usage;
            }
        }

        /// <summary>
        /// Tries to change an identity's email (ID)
        /// </summary>
        private bool TryChangeIdentityEmail(Identity identity, string newEmail)
        {
            if (MasterAccount.User.Vault.TryChangeIdentityEmail(identity, newEmail))
            {
                return true;
            }
            else
            {
                NotificationManager.ShowError("Email change error", "Email already exists");

                return false;
            }
        }

        /// <summary>
        /// Gets the breached data for an email
        /// </summary>
        private async void GetBreachedDataAsync(string email, bool useAPI)
        {
            if (useAPI)
            {
                NotificationManager.ShowInfo("Breach checking", "Please wait until the breaches are checked");
            }

            Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

            await Task.Run(() =>
            {
                Dictionary<string, string> breachedDomains = identity.GetBreaches(email, useAPI);

                DisplayBreaches(breachedDomains);
            });
        }

        /// <summary>
        /// Displays the breaches for an identity
        /// </summary>
        private void DisplayBreaches(Dictionary<string, string> domains)
        {
            lvBreachedData.Items.Clear();
            foreach (KeyValuePair<string, string> domain in domains)
            {
                ListViewItem lvi = new ListViewItem("");
                lvi.SubItems.Add(domain.Key);
                lvi.SubItems.Add(domain.Value);

                lvBreachedData.Items.Add(lvi);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteIdentity(cmbIdentity.SelectedItem.ToString());
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetIdentityDetails(cmbIdentity.SelectedItem.ToString());
            EnableIdentityComponents(cmbIdentity.SelectedIndex);
        }

        /// <summary>
        /// Displays the identities in the vault
        /// </summary>
        private void DisplayIdentities()
        {
            cmbIdentity.Items.Clear();

            foreach (KeyValuePair<string, Identity> identityPair in MasterAccount.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identityPair.Key);
            }
        }

        /// <summary>
        /// Gets the details of an identity
        /// </summary>
        private void GetIdentityDetails(string email)
        {
            Identity identity = MasterAccount.User.Vault.TryGetIdentity(email);

            GetBreachedDataAsync(email, false);

            txtIdentityName.Text = identity.Name;
            txtIdentityEmail.Text = identity.Email;
            txtIdentityUsage.Text = identity.Usage;

            DisplayCredentialInformation(identity);
        }

        /// <summary>
        /// Displays the credential status count for an identity
        /// </summary>
        private void DisplayCredentialInformation(Identity identity)
        {
            identity.CalculateHealthScore(true);

            int safeCount = identity.SafeCredentialCount;
            int weakCount = identity.WeakCredentialCount;
            int conflictCount = identity.ConflictCredentialCount;
            int compromisedCount = identity.CompromisedCredentialCount;

            chartCredentials.Series["Credentials"].Points[0].SetValueXY("Safe", safeCount);
            chartCredentials.Series["Credentials"].Points[1].SetValueXY("Weak", weakCount);
            chartCredentials.Series["Credentials"].Points[2].SetValueXY("Conflicts", conflictCount);
            chartCredentials.Series["Credentials"].Points[3].SetValueXY("Compromised", compromisedCount);
            chartCredentials.Series["Credentials"].IsValueShownAsLabel = true;
        }

        /// <summary>
        /// Deletes an identity
        /// </summary>
        private void DeleteIdentity(string email)
        {
            MasterAccount.User.Vault.TryDeleteIdentity(email);

            cmbIdentity.Items.Remove(email);
        }

        /// <summary>
        /// Enables or disables form components
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

            txtIdentityName.Enabled = enabled;
            txtIdentityEmail.Enabled = enabled;
            txtIdentityUsage.Enabled = enabled;

            btnSave.Enabled = enabled;
            btnDelete.Enabled = enabled;

            pnlIdentityComponents.Visible = enabled;
        }

        /// <summary>
        /// Clears the input texts
        /// </summary>
        private void ClearInputs()
        {
            txtIdentityName.Clear();
            txtIdentityEmail.Clear();
            txtIdentityUsage.Clear();
        }

        private void FormIdentities_Resize(object sender, EventArgs e)
        {
            FixColumnWidths();
        }

        /// <summary>
        /// Fixes the column widths based on listview width and column count
        /// </summary>
        private void FixColumnWidths()
        {
            lvBreachedData.Columns[1].Width = lvBreachedData.Width / (lvBreachedData.Columns.Count - 1);
            lvBreachedData.Columns[2].Width = lvBreachedData.Width / (lvBreachedData.Columns.Count - 1);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetBreachedDataAsync(cmbIdentity.SelectedItem.ToString(), true);
        }
    }
}
