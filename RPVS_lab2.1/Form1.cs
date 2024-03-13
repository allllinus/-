using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace RPVS_lab2._1
{
    public partial class Form1 : Form
    {
        Integral[] integrals = new Integral[3];

        double a1, b1, h1, a2, b2, h2;
        double[] IntegralEquels = new double[3];
        int[] amount = new int[3];
        bool run;

        public Form1()
        {
            InitializeComponent();
        }

        private void Execute_Click(object sender, EventArgs e)
        {     
            Initialize();
            if (!run) return;
            for (int i = 0; i < 3; i++)
            {
                integrals[i].DrawIntegral();
                integrals[i].DrawFigures();

                IntegralEquels[i] = integrals[i].TrapezoidalRule();
            }
            DrawIntervals();
        }

        
        private void Initialize()
        {
            if (Double.TryParse(textBox1.Text, out a1) &&
                Double.TryParse(textBox7.Text, out a2) &&
                Double.TryParse(textBox2.Text, out b1) &&
                Double.TryParse(textBox6.Text, out b2) &&
                Double.TryParse(textBox3.Text, out h1) &&
                Double.TryParse(textBox5.Text, out h2))
            {
                if (b1 <= a1 || b2 <= a2)
                {
                    MessageBox.Show("Введите верные данные");
                    run = false;
                    return;
                }

                run = true;
                chart1.Series[0].Points.Clear();
                chart1.Series[1].Points.Clear();

                if (b1 > a2 && b1 > b2)
                {
                    integrals[0] = new Integral(chart1, a1, a2, h1);
                    integrals[1] = new Integral(chart1, a2 + h1, b2, h2);
                    integrals[2] = new Integral(chart1, b2 + h1, b1, h2);
                    integrals[1].AreaColor = ColorGeneral.BackColor;
                    integrals[1].LineColor = Color.Black;

                    integrals[0].LineColor = ColorLine1.BackColor;
                    integrals[0].AreaColor = ColorArea1.BackColor;

                    integrals[2].LineColor = ColorLine1.BackColor;
                    integrals[2].AreaColor = ColorArea1.BackColor;

                    amount[0] = Convert.ToInt32((a2 - a1) / integrals[0].H);
                    amount[1] = Convert.ToInt32((b2 - a2) / integrals[0].H);
                    amount[2] = Convert.ToInt32((b1 - b2) / integrals[0].H);
                }
                else
                {
                    if (b1 > a2)
                    {
                        integrals[0] = new Integral(chart1, a1, a2, h1);
                        integrals[1] = new Integral(chart1, a2 + h1, b1, h2);
                        integrals[2] = new Integral(chart1, b1 + h1, b2, h2);
                        integrals[1].AreaColor = ColorGeneral.BackColor;
                        integrals[1].LineColor = Color.Black;

                        amount[0] = Convert.ToInt32((a2 - a1) / integrals[0].H) + 1;
                        amount[1] = Convert.ToInt32((b1 - a2) / h1);
                        amount[2] = Convert.ToInt32((b2 - b1) / h2); 
                    }
                    else
                    {
                        integrals[0] = new Integral(chart1, a1, b1, h1);
                        integrals[1] = new Integral(chart1, b1 + h1, a2, h2);
                        integrals[2] = new Integral(chart1, a2 + h1, b2, h2);
                        integrals[1].AreaColor = Color.Transparent;
                        integrals[1].LineColor = Color.Transparent;

                        amount[0] = Convert.ToInt32((b1 - a1) / integrals[0].H) + 1;
                        amount[1] = Convert.ToInt32((a2 - b1) / h1);
                        amount[2] = Convert.ToInt32((b2 - a2) / h2);
                    }
                    integrals[0].LineColor = ColorLine1.BackColor;
                    integrals[2].LineColor = ColorLine2.BackColor;
                    integrals[0].AreaColor = ColorArea1.BackColor;
                    integrals[2].AreaColor = ColorArea2.BackColor;
                }
            }
            else MessageBox.Show("Введите правильные числа"); 
        }


        private void DrawIntervals()
        {
            int n = amount[0];

            SetColorsForPoints(0, n, integrals[0].AreaColor, integrals[0].LineColor);

            int k = n;
            n += amount[1];
            SetColorsForPoints(k, n, integrals[1].AreaColor, integrals[1].LineColor);

            k = n;
            n += amount[2];
            SetColorsForPoints(k, n, integrals[2].AreaColor, integrals[2].LineColor);
        }


        private void SetColorsForPoints(int start, int end, Color areaColor, Color lineColor)
        {
            for (int i = start; i < end; i++)
            {
                DataPoint point = chart1.Series[1].Points[i];
                point.Color = areaColor;
                point = chart1.Series[0].Points[i];
                point.Color = lineColor;
            }
        }


        private void ColorLine1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            Button b = (Button)sender;
            b.BackColor = colorDialog1.Color;

            if (b == ColorArea1) ColorArea1.BackColor = b.BackColor;
            else if (b == ColorArea2) ColorArea2.BackColor = b.BackColor;
            else if (b == ColorLine1) ColorLine1.BackColor = b.BackColor;
            else if (b == ColorLine2) ColorLine2.BackColor = b.BackColor;
            else ColorGeneral.BackColor = b.BackColor;
        }

        private void CountSum_Click(object sender, EventArgs e)
        {
            ShowSum.Text = (Math.Round((IntegralEquels[0] + IntegralEquels[2]), 3)).ToString();
        }

        private void CountDiff_Click(object sender, EventArgs e)
        {
            ShowDiff.Text = (Math.Round((IntegralEquels[0] - IntegralEquels[2]), 3)).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
