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
        [TestCase(TestName = "Присвоение и получение свойства Name")]
        public void NameTest_ShouldBeEqual()
        {
            var appName = "SLDWORKS";
            var settings = new SolidWorksSettings {Name = appName};
            Assert.AreEqual(appName, settings.Name);
        }

        [TestCaseSource(typeof(SolidWorksSettingsTestsData)
            , nameof(SolidWorksSettingsTestsData.NameNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Присвоение и получение свойства Name. " +
                "Переданы некорректные параметры"
            })]
        public void NameTest_GivenInvalidName_ThrowsException(string name = null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new SolidWorksSettings {Name = name});
        }

        [TestCase(TestName = "Присвоение и получение свойства ApiNumbers")]
        public void ApiNumbersTest_ShouldBeNotEmpty()
        {
            var apiNumbers = new List<int> {22, 28};

            var settings = new SolidWorksSettings {ApiNumbers = apiNumbers};
            Assert.That(settings.ApiNumbers, Is.Not.Null.And.Not.Empty);
            settings.ApiNumbers.ForEach(n => apiNumbers.Contains(n));
        }

        [TestCaseSource(typeof(SolidWorksSettingsTestsData)
            , nameof(SolidWorksSettingsTestsData.ApiNumbersNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Присвоение и получение свойства ApiNumbers. " +
                "Переданы некорректные параметры"
            })]
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
        /// <param name="testName">Название теста</param>
        public static IEnumerable NameNegative(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(null),
                new TestCaseData(string.Empty),
                new TestCaseData("    ")
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Недопустимые номера API
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable ApiNumbersNegative(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(null),
                new TestCaseData(new List<int>())
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }
    }
}