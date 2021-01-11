using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class Identities : Form
    {
        public Identities()
        {
            InitializeComponent();

            LoadFormComponents();
        }

        private void LoadFormComponents()
        {
            GetIdentities();
            EnableDisableInputs();
        }

        private void chart2_PrePaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                if (e.ChartElement is System.Windows.Forms.DataVisualization.Charting.ChartArea)
                {
                    var ta = new System.Windows.Forms.DataVisualization.Charting.TextAnnotation
                    {
                        Text = Convert.ToString(vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).GetCredentialCount()),
                        Width = e.Position.Width,
                        Height = e.Position.Height,
                        X = e.Position.X - (e.Position.Width / 100),
                        Y = e.Position.Y + (e.Position.Height / 100),
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        ForeColor = Color.Gainsboro
                    };
                    //ta.Alignment = ContentAlignment.MiddleCenter;

                    chart2.Annotations.Clear();
                    chart2.Annotations.Add(ta);

                }
            }
        }

        private void btnNewIdentity_Click(object sender, EventArgs e)
        {
            string defaultIdentityName = "Identity " + (cmbIdentity.Items.Count + 1);

            vIDsafe.Main.User.Vault.NewIdentity(defaultIdentityName);

            int lastIndex = cmbIdentity.Items.Add(defaultIdentityName);
            cmbIdentity.SelectedIndex = lastIndex;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).SetDetails(txtIdentityName.Text, txtIdentityEmail.Text, txtIdentityUsage.Text);

            cmbIdentity.Items[cmbIdentity.SelectedIndex] = txtIdentityName.Text;
        }

        private void btnDeleteDiscard_Click(object sender, EventArgs e)
        {
            DeleteIdentity();
        }

        private void cmbIdentity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadDetails();
        }

        private void ReloadDetails()
        {
            EnableDisableInputs();
            GetIdentityDetails();
            GetCredentialInformation();
        }

        private void GetIdentities()
        {
            cmbIdentity.Items.Clear();

            foreach (Identity identity in vIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identity.Name);
            }
        }

        private void GetIdentityDetails()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                Identity currentIdentity = vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex);

                txtIdentityName.Text = currentIdentity.Name;
                txtIdentityEmail.Text = currentIdentity.Email;
                txtIdentityUsage.Text = currentIdentity.Usage;
            }  
        }

        private void GetCredentialInformation()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                Identity currentIdentity = vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex);

                chart2.Series["Credentials"].Points[0].SetValueXY("Safe", currentIdentity.SafeCredentials);
                chart2.Series["Credentials"].Points[1].SetValueXY("Weak", currentIdentity.WeakCredentials);
                chart2.Series["Credentials"].Points[2].SetValueXY("Conflicts", currentIdentity.ConflictCredentials);
                chart2.Series["Credentials"].Points[3].SetValueXY("Compromised", currentIdentity.CompromisedCredentials);

                chart2.Series["Credentials"].IsValueShownAsLabel = true;
            }
        }

        private void DeleteIdentity()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                vIDsafe.Main.User.Vault.DeleteIdentity(cmbIdentity.SelectedIndex);

                cmbIdentity.Items.RemoveAt(cmbIdentity.SelectedIndex);

                ReloadDetails(); //SelectedIndex doesn't work with -1 for comboboxes for some reason?
            }
        }

        private void EnableDisableInputs()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                txtIdentityName.Enabled = true;
                txtIdentityEmail.Enabled = true;
                txtIdentityUsage.Enabled = true;

                btnSave.Enabled = true;
                btnDeleteDiscard.Enabled = true;
            }
            else
            {
                txtIdentityName.Clear();
                txtIdentityEmail.Clear();
                txtIdentityUsage.Clear();
                cmbIdentity.Text = "";

                txtIdentityName.Enabled = false;
                txtIdentityEmail.Enabled = false;
                txtIdentityUsage.Enabled = false;

                btnSave.Enabled = false;
                btnDeleteDiscard.Enabled = false;
            }
        }
    }
}
