using System;
using System.Collections;
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
        /// <summary>
        ///     Негативная проверка расширения GetFormattedString
        /// </summary>
        /// <param name="key">Ключ</param>
        [TestCaseSource(typeof(RmExtensionsTestsData)
            , nameof(RmExtensionsTestsData.GetFormattedStringsNegative))]
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
        public static IEnumerable GetFormattedStringsNegative
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData(string.Empty);
                yield return new TestCaseData("   ");
            }
        }
    }
}