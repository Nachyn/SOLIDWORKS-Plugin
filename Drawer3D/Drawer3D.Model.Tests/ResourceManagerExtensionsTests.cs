using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Drawer3D.Model.Extensions;
using NSubstitute;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для ResourceManager расширений
    /// </summary>
    public class ResourceManagerExtensionsTests
    {
        [TestCaseSource(typeof(RmExtensionsTestsData)
            , nameof(RmExtensionsTestsData.GetFormattedStringsNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Проверка возвращаемого значения GetFormattedString " +
                "Передан недопустимый ключ"
            })]
        public void GetFormattedStringTest_GivenInvalidKey_ThrowsException(string key)
        {
            var resourceManager = Substitute.For<ResourceManager>();
            Assert.Throws<ArgumentNullException>(() =>
                resourceManager.GetFormattedString(key));
        }
    }

    /// <summary>
    ///     Тестовые данные для класса ResourceManagerExtensionsTests
    /// </summary>
    public static class RmExtensionsTestsData
    {
        /// <summary>
        ///     Недопустимые ключи для ResourceManager
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable GetFormattedStringsNegative(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(null),
                new TestCaseData(string.Empty),
                new TestCaseData("   ")
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }
    }
}