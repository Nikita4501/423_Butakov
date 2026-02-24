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

        private void Calculate1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(txtX1.Text);
                double y = double.Parse(txtY1.Text);
                double z = double.Parse(txtZ1.Text);

                double numerator = y * (Math.Atan(z) - Math.PI);
                double denominator = 2 + Math.Abs(x) + y * y;

                if (denominator == 0)
                {
                    MessageBox.Show("Знаменатель не может быть равен нулю.", "Ошибка");
                    return;
                }

                double c = 5 * x * y - numerator / denominator;
                txtResult1.Text = c.ToString("F4");
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