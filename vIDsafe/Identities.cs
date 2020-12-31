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
            loadTestGraph();
        }

        void loadTestGraph()
        {

            chart2.Series["Credentials"].Points[0].SetValueXY("Safe", 28);
            chart2.Series["Credentials"].Points[1].SetValueXY("Weak", 12);
            chart2.Series["Credentials"].Points[2].SetValueXY("Conflicts", 5);
            chart2.Series["Credentials"].Points[3].SetValueXY("Compromised", 5);

            chart2.Series["Credentials"].IsValueShownAsLabel = true;
        }

        private void chart2_PrePaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
            if (e.ChartElement is System.Windows.Forms.DataVisualization.Charting.ChartArea)
            {
                var ta = new System.Windows.Forms.DataVisualization.Charting.TextAnnotation
                {
                    Text = "50",
                    Width = e.Position.Width,
                    Height = e.Position.Height,
                    X = e.Position.X - (e.Position.Width / 100),
                    Y = e.Position.Y,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = Color.Gainsboro
                };
                //ta.Alignment = ContentAlignment.MiddleCenter;

                chart2.Annotations.Clear();
                chart2.Annotations.Add(ta);

            }
        }
    }
}
