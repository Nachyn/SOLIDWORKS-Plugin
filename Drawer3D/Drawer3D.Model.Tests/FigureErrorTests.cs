using System;
using Drawer3D.Model.Exceptions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для класса FigureError
    /// </summary>
    public class FigureErrorTests
    {
        [TestCase(TestName = "Присвоение и получение свойства Key")]
        public void KeyTest_ShouldReturnValidData()
        {
            var guid = Guid.NewGuid().ToString();
            var error = new FigureError {Key = guid};

            Assert.AreEqual(guid, error.Key);
        }

        [TestCase(TestName = "Присвоение и получение свойства Message")]
        public void MessageTest_ShouldReturnValidData()
        {
            var guid = Guid.NewGuid().ToString();
            var error = new FigureError {Message = guid};

            Assert.AreEqual(guid, error.Message);
        }
    }
}