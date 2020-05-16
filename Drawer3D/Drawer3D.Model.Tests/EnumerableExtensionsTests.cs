using System.Collections.Generic;
using Drawer3D.Model.Extensions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для Enumerable расширений
    /// </summary>
    public class EnumerableExtensionsTests
    {
        [TestCase(TestName = "Проверка возвращаемого значения IsNullOrEmpty")]
        public void IsNullOrEmptyTest_ShouldReturnValidResponse()
        {
            Assert.That(!new List<int> {1, 2, 3}.IsNullOrEmpty());
            Assert.That(new List<int>().IsNullOrEmpty());

            List<int> nullableList = null;
            Assert.That(nullableList.IsNullOrEmpty());
        }
    }
}