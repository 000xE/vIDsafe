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
using System.Windows.Forms.DataVisualization.Charting;

namespace vIDsafe
{
    public partial class FormOverview : Form
    {
        public FormOverview()
        {
            InitializeComponent();

            InitialMethods();
        }

        /// <summary>
        /// Initial methods to run when the form starts
        /// </summary>
        private void InitialMethods()
        {
            RecalculateHealthScore();
        }

        /// <summary>
        /// Recalculates the health score and displays details
        /// </summary>
        private void RecalculateHealthScore()
        {
            MasterAccount.User.Vault.CalculateHealthScore(true);
            DisplayHealthScores();
            DisplayCredentialStatusCounts();
            DisplaySecurityAlerts();
        }

        /// <summary>
        /// Displays the health scores and identity details
        /// </summary>
        private void DisplayHealthScores()
        {
            tlpIdentities.ColumnStyles.Clear();
            tlpIdentities.Controls.Clear();

            foreach (KeyValuePair<string,Identity> identityPair in MasterAccount.User.Vault.Identities)
            {
                tlpIdentities.ColumnCount++;
                tlpIdentities.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

                Identity identity = MasterAccount.User.Vault.TryGetIdentity(identityPair.Key);

                Panel identityPanel = CreatePanel(CalculateHealthColor(identity.HealthScore));

                identityPanel.Controls.Add(CreateLabel(identity.Name));
                identityPanel.Controls.Add(CreateLabel(identity.HealthScore.ToString() + "%"));

                tlpIdentities.Controls.Add(identityPanel, tlpIdentities.ColumnCount-2, 0);
            }

            int totalHealthScore = MasterAccount.User.Vault.HealthScore;

            FormHome.SetHealthScore(totalHealthScore, CalculateHealthColor(totalHealthScore));
        }

        /// <summary>
        /// Displays the total credential status counts in the vault
        /// </summary>
        private void DisplayCredentialStatusCounts()
        {
            int safeCount = MasterAccount.User.Vault.SafeCredentialCount;
            int weakCount = MasterAccount.User.Vault.WeakCredentialCount;
            int conflictCount = MasterAccount.User.Vault.ConflictCredentialCount;
            int compromisedCount = MasterAccount.User.Vault.CompromisedCredentialCount;

            chartCredentials.Series["Credentials"].Points[0].SetValueXY("Safe", safeCount);
            chartCredentials.Series["Credentials"].Points[1].SetValueXY("Weak", weakCount);
            chartCredentials.Series["Credentials"].Points[2].SetValueXY("Conflicts", conflictCount);
            chartCredentials.Series["Credentials"].Points[3].SetValueXY("Compromised", compromisedCount);

            chartCredentials.Series["Credentials"].IsValueShownAsLabel = true;
        }

        /// <summary>
        /// Displays the security alerts
        /// </summary>
        private void DisplaySecurityAlerts()
        {
            foreach (KeyValuePair<string, Identity> identityPair in MasterAccount.User.Vault.Identities)
            {
                Identity identity = identityPair.Value;

                int breachCount = identity.CompromisedCredentialCount;

                if (breachCount > 0)
                {
                    string alert = breachCount + " exposed credential(s)";

                    DisplayAlert(identity.Name, alert);
                }

                int conflictCount = identity.ConflictCredentialCount;

                if (conflictCount > 0)
                {
                    string alert = conflictCount + " conflicted credential(s)";

                    DisplayAlert(identity.Name, alert);
                }

                int weakCount = identity.WeakCredentialCount;

                if (weakCount > 0)
                {
                    string alert = weakCount + " weak credential(s)";

                    DisplayAlert(identity.Name, alert);
                }
            }
        }

        /// <summary>
        /// Displays a specific security alert
        /// </summary>
        private void DisplayAlert(string identityName, string alert)
        {
            ListViewItem lvi = new ListViewItem("");
            lvi.SubItems.Add(identityName);
            lvi.SubItems.Add(alert);

            lvSecurityAlerts.Items.Add(lvi);
        }

        /// <summary>
        /// Creates a custom panel
        /// </summary>
        /// <returns>
        /// The panel
        /// </returns>
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

        /// <summary>
        /// Creates a custom label
        /// </summary>
        /// <returns>
        /// The label
        /// </returns>
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

        /// <summary>
        /// Calculates a colour based on the health score
        /// </summary>
        /// <returns>
        /// The health score color
        /// </returns>
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
        /// <summary>
        /// Changes the brightness of a colour
        /// </summary>
        /// <returns>
        /// The changed colour
        /// </returns>
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

        private void chartCredentials_PrePaint(object sender, ChartPaintEventArgs e)
        {
            DisplayCredentialCount(e);
        }

        /// <summary>
        /// Displays the total credential count
        /// </summary>
        private void DisplayCredentialCount(ChartPaintEventArgs e)
        {
            if (e.ChartElement is ChartArea)
            {
                TextAnnotation ta = CreateTextAnnotation(Convert.ToString(MasterAccount.User.Vault.TotalCredentialCount), e);

                chartCredentials.Annotations.Clear();
                chartCredentials.Annotations.Add(ta);
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
    }
}
