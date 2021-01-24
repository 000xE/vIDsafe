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

            RecalculateHealthScore();
        }

        private void RecalculateHealthScore()
        {
            FormvIDsafe.Main.User.Vault.CalculateOverallHealthScore();
            DisplayHealthScores();
            DisplayCredentialInformation();
        }

        private void AddIdentityColumns()
        {
            tlpIdentities.ColumnStyles.Clear();
            for (int i = 0; i < FormvIDsafe.Main.User.Vault.Identities.Count; i++)
            {
                tlpIdentities.ColumnCount += 1;
                tlpIdentities.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            }
        }

        private void DisplayHealthScores()
        {
            tlpIdentities.Controls.Clear();

            for (int i = 0; i < FormvIDsafe.Main.User.Vault.Identities.Count; i++)
            {
                Identity identity = FormvIDsafe.Main.User.Vault.Identities[i];

                Panel identityPanel = CreatePanel(CalculateHealthColor(identity.HealthScore));

                identityPanel.Controls.Add(CreateLabel(identity.Name));
                identityPanel.Controls.Add(CreateLabel(identity.HealthScore.ToString() + "%"));

                tlpIdentities.Controls.Add(identityPanel, i, 0);
            }

            foreach (ColumnStyle style in tlpIdentities.ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 50F;
            }

            int totalHealthScore = FormvIDsafe.Main.User.Vault.OverallHealthScore;

            FormHome.SetHealthScore(totalHealthScore, CalculateHealthColor(totalHealthScore));
        }

        private Panel CreatePanel(Color backColor)
        {
            int panelPadding = 12;

            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(panelPadding),
                BackColor = backColor
            };

            return panel;
        }

        private Label CreateLabel(string text)
        {
            ContentAlignment textAlign = ContentAlignment.MiddleCenter;
            Font labelFont = new Font("Segoe UI", 9.75f);
            Color labelColor = Color.FromArgb(64, 64, 64);
            DockStyle dock = DockStyle.Bottom;

            Label label = new Label
            {
                AutoSize = false,
                Text = text,
                TextAlign = textAlign,
                Dock = dock,
                Font = labelFont,
                ForeColor = labelColor
            };

            return label;
        }

        private Color CalculateHealthColor(int healthScore)
        {
            Color color = new Color();

            int maxGoodScore = 100;
            int maxMediumScore = 75;
            int maxBadScore = 50;

            if (healthScore <= maxBadScore)
            {
                Color bad = Color.DarkSalmon;
                //Color bad = Color.FromArgb(255, 233, 150, 61);

                double colorMultiplier = ((100 - maxBadScore) + (double)healthScore) / 100;

                color = Color.FromArgb(bad.A, bad.R, bad.G, (int)(bad.B * colorMultiplier));
            }
            else if (healthScore <= maxMediumScore)
            {
                Color medium = Color.Khaki;

                double colorMultiplier = ((100 - maxMediumScore) + (double)healthScore) / 100;

                color = Color.FromArgb(medium.A, medium.R, medium.G, (int)(medium.B * colorMultiplier));
            }
            else if (healthScore <= maxGoodScore)
            {
                Color good = Color.MediumSeaGreen;

                double colorMultiplier = ((100 - maxGoodScore) + (double)healthScore) / 100;

                color = Color.FromArgb(good.A, good.R, (int)(good.G * colorMultiplier), good.B);
            }

            //Console.WriteLine(color);

            return color;
        }

        private void DisplayCredentialInformation()
        {
            int safeCount = FormvIDsafe.Main.User.Vault.TotalSafeCredentials;
            int weakCount = FormvIDsafe.Main.User.Vault.TotalWeakCredentials;
            int conflictCount = FormvIDsafe.Main.User.Vault.TotalConflictCredentials;
            int compromisedCount = FormvIDsafe.Main.User.Vault.TotalCompromisedCredentials;

            chartCredentials.Series["Credentials"].Points[0].SetValueXY("Safe", safeCount);
            chartCredentials.Series["Credentials"].Points[1].SetValueXY("Weak", weakCount);
            chartCredentials.Series["Credentials"].Points[2].SetValueXY("Conflicts", conflictCount);
            chartCredentials.Series["Credentials"].Points[3].SetValueXY("Compromised", compromisedCount);

            chartCredentials.Series["Credentials"].IsValueShownAsLabel = true;
        }

        private void chartCredentials_PrePaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            DisplayCredentialCount(e);
        }

        private void DisplayCredentialCount(System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            if (e.ChartElement is System.Windows.Forms.DataVisualization.Charting.ChartArea)
            {
                //Todo: cleanup, maybe separate method called createtextannotation? or maybe use label
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

                chartCredentials.Annotations.Clear();
                chartCredentials.Annotations.Add(ta);
            }
        }
    }
}
