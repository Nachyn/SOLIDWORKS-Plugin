using System;
using System.Collections;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для класса FigureSettings
    /// </summary>
    public class FigureSettingsTests
    {
        /// <summary>
        ///     Настройки фигуры
        /// </summary>
        private FigureSettings _figureSettings;

        /// <summary>
        ///     Инициализировать перед каждым тестом
        /// </summary>
        [SetUp]
        public void InitializeEachTest()
        {
            _figureSettings = new FigureSettings();
        }

        /// <summary>
        ///     Проверка свойства SizeX
        /// </summary>
        /// <param name="sizeRange">Валидные диапазоны значений</param>
        [TestCaseSource(typeof(FigureSettingsTestsData), nameof(FigureSettingsTestsData.SizeRage))]
        public void SizeXTest_ShouldBeNotNull(SizeRange sizeRange)
        {
            _figureSettings.SizeX = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeX);
        }

        /// <summary>
        ///     Негативная проверка свойства SizeX
        /// </summary>
        [Test]
        public void SizeXTest_GivenNullValue_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeX = null);
        }

        /// <summary>
        ///     Проверка свойства SizeY
        /// </summary>
        /// <param name="sizeRange">Валидные диапазоны значений</param>
        [TestCaseSource(typeof(FigureSettingsTestsData), nameof(FigureSettingsTestsData.SizeRage))]
        public void SizeYTest_ShouldBeNotNull(SizeRange sizeRange)
        {
            _figureSettings.SizeY = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeY);
        }

        /// <summary>
        ///     Негативная проверка свойства SizeY
        /// </summary>
        [Test]
        public void SizeYTest_GivenNullValue_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeY = null);
        }

        /// <summary>
        ///     Проверка свойства SizeZ
        /// </summary>
        /// <param name="sizeRange">Валидные диапазоны значений</param>
        [TestCaseSource(typeof(FigureSettingsTestsData), nameof(FigureSettingsTestsData.SizeRage))]
        public void SizeZTest_ShouldBeNotNull(SizeRange sizeRange)
        {
            _figureSettings.SizeZ = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeZ);
        }

        /// <summary>
        ///     Негативная проверка свойства SizeZ
        /// </summary>
        [Test]
        public void SizeZTest_GivenNullValue_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeZ = null);
        }

        /// <summary>
        ///     Проверка свойства WallThickness
        /// </summary>
        /// <param name="wallThickness">Толщина стен</param>
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(15)]
        public void WallThicknessTest_ShouldBeEqual(int wallThickness)
        {
            _figureSettings.WallThickness = wallThickness;
            Assert.AreEqual(wallThickness, _figureSettings.WallThickness);
        }

        /// <summary>
        ///     Негативная проверка свойства WallThickness
        /// </summary>
        /// <param name="wallThickness">Толщина стен</param>
        [TestCase(-5)]
        [TestCase(-10)]
        [TestCase(-15)]
        public void WallThicknessTest_GivenInvalidData_ThrowsException(int wallThickness)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.WallThickness = wallThickness);
        }

        /// <summary>
        ///     Проверка свойства MinLengthBetweenWallsX
        /// </summary>
        /// <param name="length">Длина</param>
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(40)]
        public void MinLengthBetweenWallsXTest_ShouldBeEqual(int length)
        {
            _figureSettings.MinLengthBetweenWallsX = length;
            Assert.AreEqual(length, _figureSettings.MinLengthBetweenWallsX);
        }

        /// <summary>
        ///     Негативная проверка свойства MinLengthBetweenWallsX
        /// </summary>
        /// <param name="length">Длина</param>
        [TestCase(-20)]
        [TestCase(-30)]
        [TestCase(-40)]
        public void MinLengthBetweenWallsXTest_GivenInvalidData_ThrowsException(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.MinLengthBetweenWallsX = length);
        }

        /// <summary>
        ///     Проверка свойства MinLengthBetweenWallsY
        /// </summary>
        /// <param name="length">Длина</param>
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(40)]
        public void MinLengthBetweenWallsYTest_ShouldBeEqual(int length)
        {
            _figureSettings.MinLengthBetweenWallsY = length;
            Assert.AreEqual(length, _figureSettings.MinLengthBetweenWallsY);
        }

        /// <summary>
        ///     Негативная проверка свойства MinLengthBetweenWallsY
        /// </summary>
        /// <param name="length">Длина</param>
        [TestCase(-20)]
        [TestCase(-30)]
        [TestCase(-40)]
        public void MinLengthBetweenWallsYTest_GivenInvalidData_ThrowsException(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.MinLengthBetweenWallsY = length);
        }

        /// <summary>
        ///     Проверка клонирования FigureSettings
        /// </summary>
        [Test]
        public void CloneTest_ShouldBeClonedWithDifferentReferences()
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

    /// <summary>
    ///     Тестовые данные для FigureSettingsTests
    /// </summary>
    public static class FigureSettingsTestsData
    {
        /// <summary>
        ///     Валидные диапазоны значений
        /// </summary>
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