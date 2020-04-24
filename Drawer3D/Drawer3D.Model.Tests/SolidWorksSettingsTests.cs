using System;
using System.Collections;
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
        ///     Проверка свойства Guid
        /// </summary>
        [Test]
        public void GuidTest_ShouldBeEqual()
        {
            var guid = Guid.NewGuid();

            var settings = new SolidWorksSettings {Guid = guid};
            Assert.AreEqual(guid, settings.Guid);
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
    }
}