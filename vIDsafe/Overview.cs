using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace vIDsafe
{
    public partial class Overview : Form
    {
        public Overview()
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
                var ta = new System.Windows.Forms.DataVisualization.Charting.TextAnnotation();
                ta.Text = "200";
                ta.Width = e.Position.Width;
                ta.Height = e.Position.Height;
                ta.X = e.Position.X-(ta.Width/100);
                ta.Y = e.Position.Y;
                ta.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                ta.ForeColor = Color.Gainsboro;
                //ta.Alignment = ContentAlignment.MiddleCenter;

                chart1.Annotations.Clear();
                chart1.Annotations.Add(ta);

            }
        }
    }
}
