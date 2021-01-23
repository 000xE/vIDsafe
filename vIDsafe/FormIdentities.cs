﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormIdentities : Form
    {
        public FormIdentities()
        {
            InitializeComponent();

            LoadFormComponents();
        }

        private void LoadFormComponents()
        {
            GetIdentities();
        }

        private void chartCredentials_PrePaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                if (e.ChartElement is System.Windows.Forms.DataVisualization.Charting.ChartArea)
                {
                    var ta = new System.Windows.Forms.DataVisualization.Charting.TextAnnotation
                    {
                        Text = Convert.ToString(FormvIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).GetCredentialCount()),
                        Width = e.Position.Width,
                        Height = e.Position.Height,
                        X = e.Position.X - (e.Position.Width / 100),
                        Y = e.Position.Y + (e.Position.Height / 100),
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        ForeColor = Color.Gainsboro
                    };
                    //ta.Alignment = ContentAlignment.MiddleCenter;

                    chartCredentials.Annotations.Clear();
                    chartCredentials.Annotations.Add(ta);
                }
            }
        }

        private void btnNewIdentity_Click(object sender, EventArgs e)
        {
            NewIdentity();
        }

        private void NewIdentity()
        {
            string defaultIdentityName = "New identity";

            FormvIDsafe.Main.User.Vault.NewIdentity(defaultIdentityName);

            int lastIndex = cmbIdentity.Items.Add(defaultIdentityName);
            cmbIdentity.SelectedIndex = lastIndex;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetIdentityDetails(cmbIdentity.SelectedIndex, txtIdentityName.Text, txtIdentityEmail.Text, txtIdentityUsage.Text);
        }

        private bool IsValid(string email)
        {
            if (email.Length > 0)
            {
                //Todo: validate address
                return true;
            }
            else
            {
                Console.WriteLine("Please check your email address");
                return false;
            }
        }

        private void SetIdentityDetails(int selectedIdentityIndex, string identityName, string identityEmail, string identityUsage)
        {
            if (IsValid(identityEmail))
            {
                FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).SetDetails(identityName, identityEmail, identityUsage);

                cmbIdentity.Items[selectedIdentityIndex] = identityName;

                GetBreachedData(selectedIdentityIndex, true);
            }
        }
        
        private void GetBreachedData(int selectedIdentityIndex, bool useAPI)
        {
            Dictionary<string, string> breachedDomains = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex).GetBreaches(useAPI);

            DisplayBreaches(breachedDomains);
        }

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

        private void btnDeleteDiscard_Click(object sender, EventArgs e)
        {
            DeleteIdentity(cmbIdentity.SelectedIndex);
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetIdentityDetails(cmbIdentity.SelectedIndex);
        }

        private void GetIdentities()
        {
            ResetDetails();
            cmbIdentity.Items.Clear();

            foreach (Identity identity in FormvIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identity.Name);
            }
        }

        private void GetIdentityDetails(int selectedIdentityIndex)
        {
            ResetDetails();

            if (selectedIdentityIndex >= 0)
            {
                Identity currentIdentity = FormvIDsafe.Main.User.Vault.GetIdentity(selectedIdentityIndex);

                currentIdentity.CalculateHealthScore();

                txtIdentityName.Text = currentIdentity.Name;
                txtIdentityEmail.Text = currentIdentity.Email;
                txtIdentityUsage.Text = currentIdentity.Usage;

                int safeCount = currentIdentity.SafeCredentials;
                int weakCount = currentIdentity.WeakCredentials;
                int conflictCount = currentIdentity.ConflictCredentials;
                int compromisedCount = currentIdentity.CompromisedCredentials;

                chartCredentials.Series["Credentials"].Points[0].SetValueXY("Safe", safeCount);
                chartCredentials.Series["Credentials"].Points[1].SetValueXY("Weak", weakCount);
                chartCredentials.Series["Credentials"].Points[2].SetValueXY("Conflicts", conflictCount);
                chartCredentials.Series["Credentials"].Points[3].SetValueXY("Compromised", compromisedCount);
                chartCredentials.Series["Credentials"].IsValueShownAsLabel = true;

                GetBreachedData(selectedIdentityIndex, false);
            }  
        }

        private void DeleteIdentity(int selectedIdentityIndex)
        {
            if (selectedIdentityIndex >= 0)
            {
                FormvIDsafe.Main.User.Vault.DeleteIdentity(selectedIdentityIndex);

                cmbIdentity.Items.RemoveAt(selectedIdentityIndex);

                ResetDetails();
            }
        }

        private void ResetDetails()
        {
            ClearInputs();

            int selectedIdentityIndex = cmbIdentity.SelectedIndex;

            if (selectedIdentityIndex >= 0)
            {
                EnableIdentityComponents(true);

                FixColumnWidths();
            }
            else
            {
                EnableIdentityComponents(false);
            }
        }

        private void EnableIdentityComponents(bool enabled)
        {
            txtIdentityName.Enabled = enabled;
            txtIdentityEmail.Enabled = enabled;
            txtIdentityUsage.Enabled = enabled;

            btnSave.Enabled = enabled;
            btnDeleteDiscard.Enabled = enabled;

            panel5.Visible = enabled;

            panel7.Visible = enabled;
            panel7.BringToFront();
        }

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
        private void FixColumnWidths()
        {
            lvBreachedData.Columns[1].Width = lvBreachedData.Width / (lvBreachedData.Columns.Count - 1);
            lvBreachedData.Columns[2].Width = lvBreachedData.Width / (lvBreachedData.Columns.Count - 1);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetBreachedData(cmbIdentity.SelectedIndex, true);
        }
    }
}
