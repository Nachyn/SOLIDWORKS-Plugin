using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для класса SolidWorksSettings
    /// </summary>
    public class SolidWorksSettingsTests
    {
        /// <summary>
        ///     Проверка свойства Name
        /// </summary>
        [Test]
        public void NameTest_ShouldBeEqual()
        {
            var appName = "SLDWORKS";
            var settings = new SolidWorksSettings {Name = appName};
            Assert.AreEqual(appName, settings.Name);
        }

        /// <summary>
        ///     Негативная проверка свойства Name
        /// </summary>
        [TestCaseSource(typeof(SolidWorksSettingsTestsData)
            , nameof(SolidWorksSettingsTestsData.NameNegative))]
        public void NameTest_GivenInvalidName_ThrowsException(string name = null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new SolidWorksSettings {Name = name});
        }

        /// <summary>
        ///     Проверка свойства ApiNumbers
        /// </summary>
        [Test]
        public void ApiNumbersTest_ShouldBeNotEmpty()
        {
            var apiNumbers = new List<int> {22, 28};

            var settings = new SolidWorksSettings {ApiNumbers = apiNumbers};
            Assert.That(settings.ApiNumbers, Is.Not.Null.And.Not.Empty);
            settings.ApiNumbers.ForEach(n => apiNumbers.Contains(n));
        }

        /// <summary>
        ///     Негативная проверка свойства ApiNumbers
        /// </summary>
        [TestCaseSource(typeof(SolidWorksSettingsTestsData)
            , nameof(SolidWorksSettingsTestsData.ApiNumbersNegative))]
        public void NameTest_GivenEmptyNumbers_ThrowsException(List<int> numbers)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new SolidWorksSettings {ApiNumbers = numbers});
        }
    }

    /// <summary>
    ///     Тестовые данные для класса SolidWorksSettingsTests
    /// </summary>
    public static class SolidWorksSettingsTestsData
    {
        /// <summary>
        ///     Недопустимые имена
        /// </summary>
        public static IEnumerable NameNegative
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData(string.Empty);
                yield return new TestCaseData("    ");
            }
        }

        /// <summary>
        ///     Недопустимые номера API
        /// </summary>
        public static IEnumerable ApiNumbersNegative
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData(new List<int>());
            }
        }
    }
}