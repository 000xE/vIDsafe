﻿using System;
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

        private void RecalculateHealthScore()
        {
            FormvIDsafe.Main.User.Vault.CalculateTotalHealthScore();
            DisplayHealthScores();
            DisplayCredentialInformation();
        }

        private void AddIdentityColumns()
        {
            tlpIdentities.ColumnStyles.Clear();
            for (int i = 0; i < FormvIDsafe.Main.User.Vault.GetIdentityCount(); i++)
            {
                tlpIdentities.ColumnCount += 1;
                tlpIdentities.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            }
        }

        private void DisplayHealthScores()
        {
            tlpIdentities.Controls.Clear();

            for (int i = 0; i < FormvIDsafe.Main.User.Vault.GetIdentityCount(); i++)
            {
                Identity identity = FormvIDsafe.Main.User.Vault.GetIdentity(i);

                int panelPadding = 12;

                Panel identityPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    Padding = new Padding(panelPadding),
                    BackColor = CalculateHealthColor(identity.HealthScore)
                };

                identityPanel.Controls.Add(CreateLabel(identity.Name));
                identityPanel.Controls.Add(CreateLabel(identity.HealthScore.ToString() + "%"));

                tlpIdentities.Controls.Add(identityPanel, i, 0);
            }

            foreach (ColumnStyle style in tlpIdentities.ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 50F;
            }
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

            double colorMultiplier = (double)healthScore / 100;

            if (healthScore >= 75)
            {
                Color good = Color.MediumSeaGreen;
                /*Console.WriteLine(good.G);
                Console.WriteLine(colorMultiplier);*/
                color = Color.FromArgb(good.A, good.R, (int) (good.G * colorMultiplier), good.B);
            }
            else if (healthScore >= 50)
            {
                Color medium = Color.Khaki;
                color = Color.FromArgb(medium.A, medium.R, medium.G, (int) (medium.B * colorMultiplier));
            }
            else if (healthScore >= 0)
            {
                Color bad = Color.DarkSalmon;
                color = Color.FromArgb(bad.A, bad.R, bad.G, (int) (bad.B * colorMultiplier));
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

            chartCredentials.Series["Credentials"].Points[0].SetValueXY("Safe", safeCount);
            chartCredentials.Series["Credentials"].Points[1].SetValueXY("Weak", weakCount);
            chartCredentials.Series["Credentials"].Points[2].SetValueXY("Conflicts", conflictCount);
            chartCredentials.Series["Credentials"].Points[3].SetValueXY("Compromised", compromisedCount);

            chartCredentials.Series["Credentials"].IsValueShownAsLabel = true;
        }

        private void chartCredentials_PrePaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
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

                chartCredentials.Annotations.Clear();
                chartCredentials.Annotations.Add(ta);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RecalculateHealthScore();
        }
    }
}
