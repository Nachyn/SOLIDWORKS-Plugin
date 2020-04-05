using System.Collections.Generic;
using Drawer3D.Model.Extensions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    public class EnumerableExtensionsTests
    {
        [Test]
        public void IsNullOrEmptyTest()
        {
            Assert.That(!new List<int> {1, 2, 3}.IsNullOrEmpty());
            Assert.That(new List<int>().IsNullOrEmpty());

            List<int> nullableList = null;
            Assert.That(nullableList.IsNullOrEmpty());
        }
    }
}