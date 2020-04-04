using System;
using Drawer3D.Model.Exceptions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    public class FigureErrorTests
    {
        public void KeyTest()
        {
            var guid = Guid.NewGuid().ToString();
            var error = new FigureError {Key = guid};

            Assert.AreEqual(guid, error.Key);
        }

        public void MessageTest()
        {
            var guid = Guid.NewGuid().ToString();
            var error = new FigureError {Message = guid};

            Assert.AreEqual(guid, error.Message);
        }
    }
}