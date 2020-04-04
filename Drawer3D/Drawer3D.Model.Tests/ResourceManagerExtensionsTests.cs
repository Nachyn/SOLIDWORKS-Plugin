using System;
using System.Collections;
using System.Resources;
using Drawer3D.Model.Extensions;
using NSubstitute;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    public class ResourceManagerExtensionsTests
    {
        [TestCaseSource(typeof(RmExtensionsTestsData)
            , nameof(RmExtensionsTestsData.GetFormattedStringsNegative))]
        public void GetFormattedStringTest_Negative(string key)
        {
            var resourceManager = Substitute.For<ResourceManager>();
            Assert.Throws<ArgumentNullException>(() =>
                resourceManager.GetFormattedString(key));
        }
    }

    public static class RmExtensionsTestsData
    {
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