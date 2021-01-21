using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class FormOverview : Form
    {
        public FormOverview()
        {
            InitializeComponent();

            LoadFormComponents();
        }

        private void LoadFormComponents()
        {
            AddIdentityColumns();
            DisplayHealthScores();
            DisplayCredentialInformation();
        }

        private void AddIdentityColumns()
        {
            //TODO: Cleanup
            tlpIdentities.ColumnStyles.Clear();
            foreach (Identity identity in FormvIDsafe.Main.User.Vault.Identities)
            {
                tlpIdentities.ColumnCount += 1;
                tlpIdentities.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            }
        }

        private void DisplayHealthScores()
        {
            //TODO: Cleanup
            for (int i = 0; i < FormvIDsafe.Main.User.Vault.Identities.Count; i++)
            {
                Identity identity = FormvIDsafe.Main.User.Vault.GetIdentity(i);

                ContentAlignment textAlign = ContentAlignment.MiddleCenter;
                Font labelFont = new Font("Segoe UI", 9.75f);
                Color labelColor = Color.FromArgb(64, 64, 64);

                Label identityName = new Label
                {
                    AutoSize = false,
                    Text = identity.Name,
                    TextAlign = textAlign,
                    Dock = DockStyle.Top,
                    Font = labelFont,
                    ForeColor = labelColor
                };

                Label identityScore = new Label
                {
                    AutoSize = false,
                    Text = identity.HealthScore.ToString() + "%",
                    TextAlign = textAlign,
                    Dock = DockStyle.Bottom,
                    Font = labelFont,
                    ForeColor = labelColor
                };

                int panelPadding = 12;

                Panel identityPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(panelPadding),
                    BackColor = CalculateHealthColor(identity.HealthScore)
                };

                tlpIdentities.Controls.Remove(tlpIdentities.GetControlFromPosition(i, 0));

                identityPanel.Controls.Add(identityName);
                identityPanel.Controls.Add(identityScore);

                tlpIdentities.Controls.Add(identityPanel, i, 0);
            }

            foreach (ColumnStyle style in tlpIdentities.ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 50F;
            }
        }

        private Color CalculateHealthColor(int healthScore)
        {
            Color color = new Color();

            double colorMultiplier = (double)healthScore / 100;

            //Todo: fix colours
            if (healthScore >= 75)
            {
                Color good = Color.MediumSeaGreen;
                color = Color.FromArgb(good.A, good.R, good.G * ((int)(colorMultiplier)), good.B);
            }
            else if (healthScore >= 50)
            {
                Color medium = Color.Khaki;
                color = Color.FromArgb(medium.A, medium.R * ((int)(colorMultiplier)), medium.G * ((int)(colorMultiplier)), medium.B);
            }
            else if (healthScore >= 0)
            {
                Color bad = Color.DarkSalmon;
                color = Color.FromArgb(bad.A, bad.R * ((int)(colorMultiplier)), bad.G, bad.B);
            }

            Console.WriteLine(color);

            return color;
        }

        private void DisplayCredentialInformation()
        {
            int safeCount = FormvIDsafe.Main.User.Vault.TotalSafeCredentials;
            int weakCount = FormvIDsafe.Main.User.Vault.TotalWeakCredentials;
            int conflictCount = FormvIDsafe.Main.User.Vault.TotalConflictCredentials;
            int compromisedCount = FormvIDsafe.Main.User.Vault.TotalCompromisedCredentials;

            chart1.Series["Credentials"].Points[0].SetValueXY("Safe", safeCount);
            chart1.Series["Credentials"].Points[1].SetValueXY("Weak", weakCount);
            chart1.Series["Credentials"].Points[2].SetValueXY("Conflicts", conflictCount);
            chart1.Series["Credentials"].Points[3].SetValueXY("Compromised", compromisedCount);

            chart1.Series["Credentials"].IsValueShownAsLabel = true;
        }

        private void chart1_PrePaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            if (e.ChartElement is System.Windows.Forms.DataVisualization.Charting.ChartArea)
            {
                var ta = new System.Windows.Forms.DataVisualization.Charting.TextAnnotation
                {
                    Text = Convert.ToString(FormvIDsafe.Main.User.Vault.TotalCredentialCount),
                    Width = e.Position.Width,
                    Height = e.Position.Height,
                    X = e.Position.X - (e.Position.Width / 100),
                    Y = e.Position.Y + (e.Position.Height / 100),
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.Gainsboro
                };
                //ta.Alignment = ContentAlignment.MiddleCenter;

                chart1.Annotations.Clear();
                chart1.Annotations.Add(ta);
            }
        }

        private void btnViewVault_Click(object sender, EventArgs e)
        {
            object btnVault = FormHome.FormControls.Find("btnVault", true)[0];
            FormHome.ChangeSelectedButton(btnVault);
            FormHome.OpenChildForm(new FormVault());
        }
        private void btnManageIdentities_Click(object sender, EventArgs e)
        {
            object btnIdentities = FormHome.FormControls.Find("btnIdentities", true)[0];
            FormHome.ChangeSelectedButton(btnIdentities);
            FormHome.OpenChildForm(new FormIdentities());
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            FormvIDsafe.Main.User.Vault.CalculateHealthScore();
            DisplayHealthScores();
            DisplayCredentialInformation();
        }
    }
}
