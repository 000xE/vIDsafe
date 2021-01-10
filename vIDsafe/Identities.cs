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

        private void GetIdentities()
        {
            foreach (Identity identity in vIDsafe.Main.User.Vault.Identities)
            {
                cmbIdentity.Items.Add(identity.Name);
            }
        }

        private void ReloadDetails()
        {
            GetIdentityDetails();
            GetCredentialInformation();
        }

        private void GetIdentityDetails()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                txtIdentityName.Text = vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).Name;
                txtIdentityEmail.Text = vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).Email;
                txtIdentityUsage.Text = vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).Usage;
            }
            else
            {
                txtIdentityName.Clear();
                txtIdentityEmail.Clear();
                txtIdentityUsage.Clear();
                cmbIdentity.Text = "";
            }
        }

        private void GetCredentialInformation()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                chart2.Series["Credentials"].Points[0].SetValueXY("Safe", vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).SafeCredentials);
                chart2.Series["Credentials"].Points[1].SetValueXY("Weak", vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).WeakCredentials);
                chart2.Series["Credentials"].Points[2].SetValueXY("Conflicts", vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).ConflictCredentials);
                chart2.Series["Credentials"].Points[3].SetValueXY("Compromised", vIDsafe.Main.User.Vault.GetIdentity(cmbIdentity.SelectedIndex).CompromisedCredentials);

                chart2.Series["Credentials"].IsValueShownAsLabel = true;
            }
        }

        private void DeleteIdentity()
        {
            if (cmbIdentity.SelectedIndex >= 0)
            {
                vIDsafe.Main.User.Vault.DeleteIdentity(cmbIdentity.SelectedIndex);

                cmbIdentity.Items.RemoveAt(cmbIdentity.SelectedIndex);

                ReloadDetails();
            }
        }
    }
}
