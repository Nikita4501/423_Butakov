using System;
using System.Windows;
using System.Windows.Controls;

namespace _423_Butakov
{
    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private double GetFX(double x)
        {
            if (rbSh.IsChecked == true)
                return Math.Sinh(x);
            else if (rbX2.IsChecked == true)
                return x * x;
            else
                return Math.Exp(x);
        }

        private void Calculate2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(txtX2.Text);
                double y = double.Parse(txtY2.Text);
                double f = GetFX(x);

                double b;

                if (y == 0)
                {
                    b = 0;
                }
                else if (x == 0)
                {
                    b = Math.Pow(f * f + y, 3);
                }
                else
                {
                    double ratio = x / y;
                    if (ratio > 0)
                    {
                        if (f <= 0)
                        {
                            MessageBox.Show("Для ветви x/y > 0 необходимо f(x) > 0, чтобы вычислить ln(f(x)).", "Ошибка");
                            return;
                        }
                        b = Math.Log(f) + Math.Pow(f * f + y, 3);
                    }
                    else if (ratio < 0)
                    {
                        b = Math.Log(Math.Abs(f / y)) + Math.Pow(f + y, 3);
                    }
                    else
                    {
                        b = Math.Pow(f * f + y, 3);
                    }
                }

                txtResult2.Text = b.ToString("F4");
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

        private void Clear2_Click(object sender, RoutedEventArgs e)
        {
            txtX2.Clear();
            txtY2.Clear();
            txtResult2.Clear();
            rbSh.IsChecked = true;
        }
    }
}