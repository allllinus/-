using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace RPVS_lab2._1
{
    internal class Integral
    {        
        public Integral(Chart chart, double a,  double b, double h)
        {
            chart1 = chart;
            this.a = a; this.b = b; this.h = h;
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.#";
        }

        public double A
        {

            get { return a; }
            set
            {
                if (value > 10) MessageBox.Show("a > 10");
                a = value;
            }
        }
        public double B
        {
            get { return b; }
            set
            {
                if (b < a) MessageBox.Show("b < a");
                b = value;
            }
        }
        public double H
        {
            get { return h;}
            set
            {
                if (value > 5) MessageBox.Show("Слишком большой шаг");
                h = value;
            }
        }

        private Chart chart1;
        private double a;
        private double b;
        private double h;

        //private List<PointF> points = new List<PointF>();

        public Color LineColor { get; set; }
        public Color AreaColor { get; set; }



        public static double Function(double x)
        {
            return Math.Log(1 + x * x) / (1 + x * x);
        }

        /// <summary>
        /// Функция, для наглядного отображения работы, выбранного метода
        /// </summary>
        public void DrawFigures()
        {
            chart1.Series[1].ChartType = SeriesChartType.Area;
            for (double x = a; Math.Round(x, 5) <= b; x += h)
            {
                chart1.Series[1].Points.AddXY(x, Function(x));
            }
        }


        /// <summary>
        /// Функция, для отображения графика интеграла
        /// </summary>
        public void DrawIntegral()
        {
            for (double x = a; Math.Round(x, 5) <= b; x += h)
            {
                chart1.Series[0].Points.AddXY(x, Function(x));
            }
        }


        /// <summary>
        /// Функция, для решения интеграла методом тарпеций
        /// </summary>
        public double TrapezoidalRule()
        {
            double n = (b - a) / h;
            double sum = 0; 
       
            for (int i = 0; i < n; i++)
            {
                double x = a + i * h;
                double xi1 = a + (i + 1) * h;
                sum += Function(x) + Function(xi1);
            }
            return sum * h / 2;
        }


        ///// <summary>
        ///// Функция, для решения интеграла методом левых прямоугольников
        ///// </summary>
        //public double LeftRectangleRule()
        //{
        //    chart1.Series[0].Points.Clear();
        //    double n = (b - a) / h;
        //    double sum = 0;

        //    double xstart = a + 0 * h;

        //    for (int i = 0; i <= n - 1; i++)
        //    {
        //        double x = xstart + i * h;
        //        points.Add(new PointF((float)(x + h / 2), (float)Function(x)));
        //        sum += Function(x);
        //        chart1.Series[0].Points.AddXY(x, Function(x));
        //    }

        //    return sum * h;
        //}


        ///// <summary>
        ///// Функция, для решения интеграла методом правых прямоугольников
        ///// </summary>
        //public double RightRectangleRule()
        //{
        //    chart1.Series[0].Points.Clear();

        //    double n = (b - a) / h;
        //    double sum = 0;

        //    double xstart = a + 1 * h;

        //    for (int i = 1; i <= n; i++)
        //    {
        //        double x = xstart + i * h;
        //        points.Add(new PointF((float)(x - h / 2), (float)Function(x)));
        //        sum += Function(x);
        //        chart1.Series[0].Points.AddXY(x, Function(x));
        //    }
        //    return sum * h;
        //}


        ///// <summary>
        ///// Функция, для решения интеграла методом средних прямоугольников
        ///// </summary>
        //public double MidpointRule()
        //{
        //    chart1.Series[0].Points.Clear();
        //    double n = (b - a) / h;
        //    double sum = (Function(a) + Function(b)) / 2;

        //    double xstart = a + 0.5 * h;

        //    for (int i = 0; i < n; i++)
        //    {
        //        double x = xstart + i * h;
        //        points.Add(new PointF((float) x, (float)Function(x)));
        //        sum += Function(x);
        //        chart1.Series[0].Points.AddXY(x, Function(x));
        //    }
        //    return sum * h;
        //}
    
    }
}

