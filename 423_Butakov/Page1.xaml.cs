using System;
using System.Windows;
using System.Windows.Controls;

namespace _423_Butakov
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Вычисляет значение c по формуле:
        /// c = 5*x*y - (y * (atan(z) - π)) / (2 + |x| + y²)
        /// </summary>
        /// <param name="x">Параметр x</param>
        /// <param name="y">Параметр y</param>
        /// <param name="z">Параметр z</param>
        /// <param name="result">Результат вычисления (корректен при возврате true)</param>
        /// <returns>true, если вычисление успешно; false при делении на ноль или других ошибках</returns>
        public bool TryComputeC(double x, double y, double z, out double result)
        {
            result = 0;
            try
            {
                double denominator = 2 + Math.Abs(x) + y * y;
                if (Math.Abs(denominator) < 1e-15)
                    return false; // деление на ноль

                double numerator = y * (Math.Atan(z) - Math.PI);
                result = 5 * x * y - numerator / denominator;

                if (double.IsNaN(result) || double.IsInfinity(result))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void Calculate1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(txtX1.Text);
                double y = double.Parse(txtY1.Text);
                double z = double.Parse(txtZ1.Text);

                if (TryComputeC(x, y, z, out double c))
                    txtResult1.Text = c.ToString("F4");
                else
                    MessageBox.Show("Ошибка вычисления (возможно деление на ноль).", "Ошибка");
            }
            catch (FormatException)
            {
                MessageBox.Show("Ошибка ввода: проверьте заполнение полей числовыми значениями.", "Ошибка");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void Clear1_Click(object sender, RoutedEventArgs e)
        {
            txtX1.Clear();
            txtY1.Clear();
            txtZ1.Clear();
            txtResult1.Clear();
        }
    }
}