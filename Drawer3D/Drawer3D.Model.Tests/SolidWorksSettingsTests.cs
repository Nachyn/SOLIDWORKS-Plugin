using System;
using System.Collections;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    public class SolidWorksSettingsTests
    {
        [Test]
        public void NameTest()
        {
            var appName = "SLDWORKS";
            var settings = new SolidWorksSettings {Name = appName};
            Assert.AreEqual(appName, settings.Name);
        }

        [TestCaseSource(typeof(SolidWorksSettingsTestsData)
            , nameof(SolidWorksSettingsTestsData.NameNegative))]
        public void NameTest_Negative(string name = null)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new SolidWorksSettings {Name = name});
        }
    }

    public static class SolidWorksSettingsTestsData
    {
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