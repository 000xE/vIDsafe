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
            loadTestGraph();
        }

        void loadTestGraph()
        {
            chart1.Series["Credentials"].Points[0].SetValueXY("Safe", 146);
            chart1.Series["Credentials"].Points[1].SetValueXY("Weak", 30);
            chart1.Series["Credentials"].Points[2].SetValueXY("Conflicts", 7);
            chart1.Series["Credentials"].Points[3].SetValueXY("Compromised", 17);

            chart1.Series["Credentials"].IsValueShownAsLabel = true;
        }

        private void chart1_PrePaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            if (e.ChartElement is System.Windows.Forms.DataVisualization.Charting.ChartArea)
            {
                var ta = new System.Windows.Forms.DataVisualization.Charting.TextAnnotation
                {
                    Text = "200",
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
    }
}
