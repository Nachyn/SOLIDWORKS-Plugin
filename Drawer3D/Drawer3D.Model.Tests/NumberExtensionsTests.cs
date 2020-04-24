using Drawer3D.Model.Extensions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для численных расширений
    /// </summary>
    public class NumberExtensionsTests
    {
        /// <summary>
        ///     Проверка расширения ToMilli (int)
        /// </summary>
        /// <param name="expected">Ожидаемое значение</param>
        /// <param name="actual">Преобразованное значение</param>
        [TestCase(0.05, 50)]
        [TestCase(0.15, 150)]
        [TestCase(7.75, 7750)]
        public void IntegerToMilliTest_ShouldBeEqual(double expected, int actual)
        {
            Assert.AreEqual(expected, actual.ToMilli(), double.Epsilon);
        }

        /// <summary>
        ///     Проверка расширения ToMilli (double)
        /// </summary>
        /// <param name="expected">Ожидаемое значение</param>
        /// <param name="actual">Преобразованное значение</param>
        [TestCase(0.05, 50)]
        [TestCase(0.15, 150)]
        [TestCase(7.75, 7750)]
        public void DoubleToMilliTest_ShouldBeEqual(double expected, double actual)
        {
            Assert.AreEqual(expected, actual.ToMilli(), double.Epsilon);
        }
    }
}