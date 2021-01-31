using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace vIDsafe
{
    public partial class FormOverview : Form
    {
        public FormOverview()
        {
            InitializeComponent();

            LoadFormComponents();
        }

        //Todo: cleanup all "Load/GetFormComponents" methods by renaming?
        private void LoadFormComponents()
        {
            RecalculateHealthScore();
            FormHome.SetTheme(this);
        }

        private void RecalculateHealthScore()
        {
            FormvIDsafe.Main.User.Vault.CalculateOverallHealthScore();
            DisplayHealthScores();
            DisplayCredentialInformation();
            DisplaySecurityAlerts();
        }

        private void DisplayHealthScores()
        {
            tlpIdentities.ColumnStyles.Clear();
            tlpIdentities.Controls.Clear();

            foreach (KeyValuePair<string,Identity> identityPair in FormvIDsafe.Main.User.Vault.Identities)
            {
                tlpIdentities.ColumnCount++;
                tlpIdentities.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

                Identity identity = FormvIDsafe.Main.User.Vault.Identities[identityPair.Key];

                Panel identityPanel = CreatePanel(CalculateHealthColor(identity.HealthScore));

                identityPanel.Controls.Add(CreateLabel(identity.Name));
                identityPanel.Controls.Add(CreateLabel(identity.HealthScore.ToString() + "%"));

                tlpIdentities.Controls.Add(identityPanel, tlpIdentities.ColumnCount-2, 0);
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
            int maxGoodScore = 100;
            int maxMediumScore = 75;
            int maxBadScore = 50;

            double colourAmplifier = 50;

            Color scoreColor = new Color();

            if (healthScore < maxBadScore)
            {
                scoreColor = Color.DarkSalmon;
            }
            else if (healthScore < maxMediumScore)
            {
                scoreColor = Color.Khaki;
            }
            else if (healthScore <= maxGoodScore)
            {
                scoreColor = Color.LimeGreen;
            }

            double colorMultiplier = (healthScore / (100 / colourAmplifier)) / 100;

            scoreColor = ChangeColorBrightness(scoreColor, (float)colorMultiplier);

            //Console.WriteLine(color);

            return scoreColor;
        }

        //https://gist.github.com/zihotki/09fc41d52981fb6f93a81ebf20b35cd5
        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = color.R;
            float green = color.G;
            float blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = ((255 - red) * correctionFactor) + red;
                green = ((255 - green) * correctionFactor) + green;
                blue = ((255 - blue) * correctionFactor) + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
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

        private void DisplaySecurityAlerts()
        {
            foreach (KeyValuePair<string, Identity> identityPair in FormvIDsafe.Main.User.Vault.Identities)
            {
                Identity identity = identityPair.Value;

                int breachCount = identity.CompromisedCredentials;

                if (breachCount > 0)
                {
                    string alert = breachCount + " exposed credential(s)";

                    DisplayAlert(identity.Name, alert);
                }

                int conflictCount = identity.ConflictCredentials;

                if (conflictCount > 0)
                {
                    string alert = conflictCount + " conflicted credential(s)";

                    DisplayAlert(identity.Name, alert);
                }

                int weakCount = identity.WeakCredentials;

                if (weakCount > 0)
                {
                    string alert = weakCount + " weak credential(s)";

                    DisplayAlert(identity.Name, alert);
                }
            }
        }
        private void DisplayAlert(string identityName, string alert)
        {
            ListViewItem lvi = new ListViewItem("");
            lvi.SubItems.Add(identityName);
            lvi.SubItems.Add(alert);

            lvSecurityAlerts.Items.Add(lvi);
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
                    Tag = "MainPanel", //Todo: Not working, constant black
                    //ForeColor = Color.Gainsboro
                };
                //ta.Alignment = ContentAlignment.MiddleCenter;

                chartCredentials.Annotations.Clear();
                chartCredentials.Annotations.Add(ta);
            }
        }
    }
}
