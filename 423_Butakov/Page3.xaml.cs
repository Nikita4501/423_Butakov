using System;
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

        /// <summary>
        /// Вычисляет значение y = 9 * (x³ + b³) * tan(x)
        /// </summary>
        /// <param name="x">Аргумент x</param>
        /// <param name="b">Параметр b</param>
        /// <param name="result">Результат</param>
        /// <returns>true, если вычисление успешно; false при недопустимых значениях (tan разрывен)</returns>
        public bool TryComputeY(double x, double b, out double result)
        {
            result = 0;
            try
            {
                double tanX = Math.Tan(x);
                // Проверка на точки разрыва тангенса (pi/2 + pi*k)
                double cosX = Math.Cos(x);
                if (Math.Abs(cosX) < 1e-10)
                    return false;

                result = 9 * (Math.Pow(x, 3) + Math.Pow(b, 3)) * tanX;

                return !double.IsNaN(result) && !double.IsInfinity(result);
            }
            catch
            {
                return false;
            }
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
                    if (TryComputeY(x, b, out double y))
                    {
                        txtResult3.AppendText($"x = {x:F4}\t y = {y:F4}{Environment.NewLine}");
                        ChartFunc.Series[0].Points.AddXY(x, y);
                    }
                    else
                    {
                        txtResult3.AppendText($"x = {x:F4}\t y = undefined{Environment.NewLine}");
                    }
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