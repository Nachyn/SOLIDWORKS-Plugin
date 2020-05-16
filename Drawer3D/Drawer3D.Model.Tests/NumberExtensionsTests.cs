using Drawer3D.Model.Extensions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для численных расширений
    /// </summary>
    public class NumberExtensionsTests
    {
        [TestCase(0.05, 50, TestName = "Проверка возвращаемого значения ToMilli (int)")]
        [TestCase(0.15, 150, TestName = "Проверка возвращаемого значения ToMilli (int)")]
        [TestCase(7.75, 7750, TestName = "Проверка возвращаемого значения ToMilli (int)")]
        public void IntegerToMilliTest_ShouldBeEqual(double expected, int actual)
        {
            Assert.AreEqual(expected, actual.ToMilli(), double.Epsilon);
        }

        [TestCase(0.05, 50, TestName = "Проверка возвращаемого значения ToMilli (double)")]
        [TestCase(0.15, 150, TestName = "Проверка возвращаемого значения ToMilli (double)")]
        [TestCase(7.75, 7750, TestName = "Проверка возвращаемого значения ToMilli (double)")]
        public void DoubleToMilliTest_ShouldBeEqual(double expected, double actual)
        {
            Assert.AreEqual(expected, actual.ToMilli(), double.Epsilon);
        }
    }
}