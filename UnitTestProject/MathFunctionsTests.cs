using _423_Butakov;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class MathFunctionsTests
    {
        [TestMethod]
        public void Page1_TryComputeC_ValidInput_ReturnsExpected()
        {
            var page = new Page1();
            bool success = page.TryComputeC(1, 2, 3, out double result);
            Assert.IsTrue(success);
            Assert.AreEqual(10.5407, result, 1e-3);
        }

        [TestMethod]
        public void Page1_TryComputeC_DivideByZero_ReturnsFalse()
        {
            var page = new Page1();
            bool success = page.TryComputeC(1, 1e200, 1, out _);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Page2_TryComputeB_Sinh_YisZero_ReturnsZero()
        {
            var page = new Page2();
            bool success = page.TryComputeB(2, 0, FuncChoice.Sinh, out double result);
            Assert.IsTrue(success);
            Assert.AreEqual(0, result, 1e-9);
        }

        [TestMethod]
        public void Page2_TryComputeB_Square_XisZero_ReturnsExpected()
        {
            var page = new Page2();
            bool success = page.TryComputeB(0, 2, FuncChoice.Square, out double result);
            Assert.IsTrue(success);
            Assert.AreEqual(8, result, 1e-9);
        }

        [TestMethod]
        public void Page2_TryComputeB_Exp_RatioPositive_ReturnsExpected()
        {
            var page = new Page2();
            bool success = page.TryComputeB(1, 2, FuncChoice.Exp, out double result);
            Assert.IsTrue(success);
            Assert.AreEqual(828.68, result, 1e-1);
        }

        [TestMethod]
        public void Page2_TryComputeB_Sinh_RatioPositive_FNegative_ReturnsFalse()
        {
            var page = new Page2();
            bool success = page.TryComputeB(-1, -2, FuncChoice.Sinh, out _);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Page2_TryComputeB_Square_RatioNegative_ReturnsExpected()
        {
            var page = new Page2();
            bool success = page.TryComputeB(2, -3, FuncChoice.Square, out double result);
            Assert.IsTrue(success);
            Assert.AreEqual(1.2877, result, 1e-3);
        }

        [TestMethod]
        public void Page3_TryComputeY_ValidInput_ReturnsExpected()
        {
            var page = new Page3();
            bool success = page.TryComputeY(0.5, 1, out double result);
            Assert.IsTrue(success);
            Assert.AreEqual(5.5313, result, 1e-3);
        }

        [TestMethod]
        public void Page3_TryComputeY_TanUndefined_ReturnsFalse()
        {
            var page = new Page3();
            bool success = page.TryComputeY(Math.PI / 2, 1, out _);
            Assert.IsFalse(success);
        }

        [TestMethod]
        public void Page3_TryComputeY_XisZero_ReturnsZero()
        {
            var page = new Page3();
            bool success = page.TryComputeY(0, 5, out double result);
            Assert.IsTrue(success);
            Assert.AreEqual(0, result, 1e-9);
        }
    }
}