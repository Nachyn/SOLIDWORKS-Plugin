using Drawer3D.Model.Extensions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    public class NumberExtensionsTests
    {
        [TestCase(0.05, 50)]
        [TestCase(0.15, 150)]
        [TestCase(7.75, 7750)]
        public void IntegerToMilliTest(double expected, int actual)
        {
            Assert.AreEqual(expected, actual.ToMilli(), double.Epsilon);
        }

        [TestCase(0.05, 50)]
        [TestCase(0.15, 150)]
        [TestCase(7.75, 7750)]
        public void DoubleToMilliTest(double expected, double actual)
        {
            Assert.AreEqual(expected, actual.ToMilli(), double.Epsilon);
        }
    }
}