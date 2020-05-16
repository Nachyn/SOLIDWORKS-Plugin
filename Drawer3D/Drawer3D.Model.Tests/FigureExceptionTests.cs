using Drawer3D.Model.Exceptions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для класса FigureException
    /// </summary>
    public class FigureExceptionTests
    {
        [TestCase(TestName = "Присвоение и получение для всех свойств")]
        public void FigureErrorTest_PropertiesShouldReturnValidData()
        {
            const string key = "key";
            const string message = "msg";

            var exception = new FigureException(key, message);
            Assert.AreEqual(key, exception.FigureError.Key);
            Assert.AreEqual(message, exception.FigureError.Message);

            var figureError = new FigureError();
            exception.FigureError = figureError;
            Assert.AreEqual(figureError, exception.FigureError);

            exception.FigureError = null;
            Assert.IsNull(exception.FigureError);
        }
    }
}