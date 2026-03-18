using System;
using System.Windows;
using System.Windows.Controls;

namespace _423_Butakov
{
    public enum FuncChoice
    {
        Sinh,
        Square,
        Exp
    }

    public partial class Page2 : Page
    {
        public Page2()
        {
            InitializeComponent();
        }

        private FuncChoice GetCurrentChoice()
        {
            if (rbSh.IsChecked == true) return FuncChoice.Sinh;
            if (rbX2.IsChecked == true) return FuncChoice.Square;
            return FuncChoice.Exp;
        }

        /// <summary>
        /// Вычисляет значение f(x) в зависимости от выбранной функции.
        /// </summary>
        private double ComputeF(double x, FuncChoice choice)
        {
            switch (choice)
            {
                case FuncChoice.Sinh: return Math.Sinh(x);
                case FuncChoice.Square: return x * x;
                case FuncChoice.Exp: return Math.Exp(x);
                default: throw new ArgumentException("Неизвестный выбор функции");
            }
        }

        /// <summary>
        /// Вычисляет значение b по условиям варианта.
        /// </summary>
        /// <param name="x">Параметр x</param>
        /// <param name="y">Параметр y</param>
        /// <param name="choice">Выбор функции f(x)</param>
        /// <param name="result">Результат вычисления</param>
        /// <returns>true, если вычисление успешно; false при недопустимых значениях</returns>
        public bool TryComputeB(double x, double y, FuncChoice choice, out double result)
        {
            result = 0;
            try
            {
                double f = ComputeF(x, choice);

                if (Math.Abs(y) < 1e-15) // y == 0
                {
                    result = 0;
                    return true;
                }

                if (Math.Abs(x) < 1e-15) // x == 0
                {
                    result = Math.Pow(f * f + y, 3);
                    return !double.IsNaN(result) && !double.IsInfinity(result);
                }

                double ratio = x / y;
                if (ratio > 0)
                {
                    if (f <= 0) return false; // ln(f) не определён
                    result = Math.Log(f) + Math.Pow(f * f + y, 3);
                }
                else if (ratio < 0)
                {
                    result = Math.Log(Math.Abs(f / y)) + Math.Pow(f + y, 3);
                }
                else // ratio == 0 (невозможно при x!=0, но оставлено)
                {
                    result = Math.Pow(f * f + y, 3);
                }

                return !double.IsNaN(result) && !double.IsInfinity(result);
            }
            catch
            {
                return false;
            }
        }

        private void Calculate2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double x = double.Parse(txtX2.Text);
                double y = double.Parse(txtY2.Text);
                FuncChoice choice = GetCurrentChoice();

                if (TryComputeB(x, y, choice, out double b))
                    txtResult2.Text = b.ToString("F4");
                else
                    MessageBox.Show("Ошибка вычисления (недопустимые значения).", "Ошибка");
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