using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace _423_Butakov
{
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            ChartFunc.ChartAreas.Add(new ChartArea("MainArea"));
            ChartFunc.Series.Add(new Series("y(x)")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 2,
                Color = System.Drawing.Color.Blue,
                IsValueShownAsLabel = false
            });
        }

        private void Calculate3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x0 = double.Parse(txtX0.Text);
                double xk = double.Parse(txtXk.Text);
                double dx = double.Parse(txtDx.Text);
                double b = double.Parse(txtB.Text);

                if (dx <= 0)
                {
                    MessageBox.Show("Шаг должен быть положительным числом.", "Ошибка");
                    return;
                }

                if (x0 > xk)
                {
                    MessageBox.Show("Начало отрезка не может быть больше конца.", "Ошибка");
                    return;
                }

                txtResult3.Clear();
                ChartFunc.Series[0].Points.Clear();

                for (double x = x0; x <= xk; x += dx)
                {
                    double tanX;
                    try
                    {
                        tanX = Math.Tan(x);
                    }
                    catch
                    {
                        tanX = double.NaN;
                    }

                    double y = 9 * (Math.Pow(x, 3) + Math.Pow(b, 3)) * tanX;

                    txtResult3.AppendText($"x = {x:F4}\t y = {y:F4}{Environment.NewLine}");

                    if (!double.IsNaN(y) && !double.IsInfinity(y))
                        ChartFunc.Series[0].Points.AddXY(x, y);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода: проверьте заполнение полей числовыми значениями.", "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка вычисления: {ex.Message}", "Ошибка");
            }
        }

        private void Clear3_Click(object sender, RoutedEventArgs e)
        {
            txtX0.Clear();
            txtXk.Clear();
            txtDx.Clear();
            txtB.Clear();
            txtResult3.Clear();
            ChartFunc.Series[0].Points.Clear();
        }
    }
}