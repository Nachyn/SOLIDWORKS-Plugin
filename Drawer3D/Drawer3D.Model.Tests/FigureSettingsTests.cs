using System;
using System.Collections;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    public class FigureSettingsTests
    {
        private FigureSettings _figureSettings;

        [SetUp]
        public void InitializeTest()
        {
            _figureSettings = new FigureSettings();
        }

        [TestCaseSource(typeof(FigureSettingsTestsData),
            nameof(FigureSettingsTestsData.SizeRage))]
        public void SizeXTest(SizeRange sizeRange)
        {
            _figureSettings.SizeX = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeX);
        }

        [Test]
        public void SizeXTest_Negative()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeX = null);
        }

        [TestCaseSource(typeof(FigureSettingsTestsData),
            nameof(FigureSettingsTestsData.SizeRage))]
        public void SizeYTest(SizeRange sizeRange)
        {
            _figureSettings.SizeY = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeY);
        }

        [Test]
        public void SizeYTest_Negative()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeY = null);
        }

        [TestCaseSource(typeof(FigureSettingsTestsData),
            nameof(FigureSettingsTestsData.SizeRage))]
        public void SizeZTest(SizeRange sizeRange)
        {
            _figureSettings.SizeZ = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeZ);
        }

        [Test]
        public void SizeZTest_Negative()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeZ = null);
        }

        [TestCase(5)]
        [TestCase(10)]
        [TestCase(15)]
        public void WallThicknessTest(int wallThickness)
        {
            _figureSettings.WallThickness = wallThickness;
            Assert.AreEqual(wallThickness, _figureSettings.WallThickness);
        }

        [TestCase(-5)]
        [TestCase(-10)]
        [TestCase(-15)]
        public void WallThicknessTest_Negative(int wallThickness)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.WallThickness = wallThickness);
        }

        [TestCase(20)]
        [TestCase(30)]
        [TestCase(40)]
        public void MinLengthBetweenWallsXTest(int length)
        {
            _figureSettings.MinLengthBetweenWallsX = length;
            Assert.AreEqual(length, _figureSettings.MinLengthBetweenWallsX);
        }

        [TestCase(-20)]
        [TestCase(-30)]
        [TestCase(-40)]
        public void MinLengthBetweenWallsXTest_Negative(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.MinLengthBetweenWallsX = length);
        }

        [TestCase(20)]
        [TestCase(30)]
        [TestCase(40)]
        public void MinLengthBetweenWallsYTest(int length)
        {
            _figureSettings.MinLengthBetweenWallsY = length;
            Assert.AreEqual(length, _figureSettings.MinLengthBetweenWallsY);
        }

        [TestCase(-20)]
        [TestCase(-30)]
        [TestCase(-40)]
        public void MinLengthBetweenWallsYTest_Negative(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.MinLengthBetweenWallsY = length);
        }

        [Test]
        public void CloneTest()
        {
            var clonedSettings = (FigureSettings) _figureSettings.Clone();
            Assert.AreNotEqual(clonedSettings, _figureSettings);
            Assert.AreEqual(clonedSettings.WallThickness, _figureSettings.WallThickness);
            Assert.AreNotEqual(clonedSettings.SizeX, _figureSettings.SizeX);
            Assert.AreNotEqual(clonedSettings.SizeY, _figureSettings.SizeY);
            Assert.AreNotEqual(clonedSettings.SizeZ, _figureSettings.SizeZ);
            Assert.AreEqual(clonedSettings.MinLengthBetweenWallsX,
                _figureSettings.MinLengthBetweenWallsX);

            Assert.AreEqual(clonedSettings.MinLengthBetweenWallsY,
                _figureSettings.MinLengthBetweenWallsY);

            Assert.AreEqual(clonedSettings.SizeX.Max, _figureSettings.SizeX.Max);
            Assert.AreEqual(clonedSettings.SizeY.Max, _figureSettings.SizeY.Max);
            Assert.AreEqual(clonedSettings.SizeZ.Max, _figureSettings.SizeZ.Max);
            Assert.AreEqual(clonedSettings.SizeX.Min, _figureSettings.SizeX.Min);
            Assert.AreEqual(clonedSettings.SizeY.Min, _figureSettings.SizeY.Min);
            Assert.AreEqual(clonedSettings.SizeZ.Min, _figureSettings.SizeZ.Min);
        }
    }

    public static class FigureSettingsTestsData
    {
        public static IEnumerable SizeRage
        {
            get
            {
                yield return new TestCaseData(new SizeRange {Min = 200, Max = 300});
                yield return new TestCaseData(new SizeRange {Min = 100, Max = 200});
                yield return new TestCaseData(new SizeRange {Min = 100, Max = 400});
            }
        }
    }
}