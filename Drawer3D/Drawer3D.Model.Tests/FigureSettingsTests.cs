using System;
using System.Collections;
using System.Collections.Generic;
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

        [TestCaseSource(typeof(FigureSettingsTestsData), nameof(FigureSettingsTestsData.SizeRage)
            , new object[]
            {
                "Проверка диапазона значений для длины. " +
                "Переданы валидные диапазоны"
            })]
        public void SizeXTest_ShouldBeNotNull(SizeRange sizeRange)
        {
            _figureSettings.SizeX = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeX);
        }

        [TestCase(TestName = "Негативный тест. " +
                             "Диапазону значений для длины присвоен null аргумент")]
        public void SizeXTest_GivenNullValue_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeX = null);
        }

        [TestCaseSource(typeof(FigureSettingsTestsData), nameof(FigureSettingsTestsData.SizeRage)
            , new object[]
            {
                "Проверка диапазона значений для ширины. " +
                "Переданы валидные диапазоны"
            })]
        public void SizeYTest_ShouldBeNotNull(SizeRange sizeRange)
        {
            _figureSettings.SizeY = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeY);
        }

        [TestCase(TestName = "Негативный тест. " +
                             "Диапазону значений для ширины присвоен null аргумент")]
        public void SizeYTest_GivenNullValue_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeY = null);
        }

        [TestCaseSource(typeof(FigureSettingsTestsData), nameof(FigureSettingsTestsData.SizeRage)
            , new object[]
            {
                "Проверка диапазона значений для высоты. " +
                "Переданы валидные диапазоны"
            })]
        public void SizeZTest_ShouldBeNotNull(SizeRange sizeRange)
        {
            _figureSettings.SizeZ = sizeRange;
            Assert.IsNotNull(_figureSettings.SizeZ);
        }

        [TestCase(TestName = "Негативный тест. " +
                             "Диапазону значений для высоты присвоен null аргумент")]
        public void SizeZTest_GivenNullValue_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => _figureSettings.SizeZ = null);
        }

        [TestCase(5, TestName = "Толщина стен. Присвоена валидная толщина 5")]
        [TestCase(10, TestName = "Толщина стен. Присвоена валидная толщина 10")]
        [TestCase(15, TestName = "Толщина стен. Присвоена валидная толщина 15")]
        public void WallThicknessTest_ShouldBeEqual(int wallThickness)
        {
            _figureSettings.WallThickness = wallThickness;
            Assert.AreEqual(wallThickness, _figureSettings.WallThickness);
        }

        [TestCase(-5,
            TestName = "Негативный тест. Толщина стен. Присвоена недопустимая толщина -5")]
        [TestCase(-10,
            TestName = "Негативный тест. Толщина стен. Присвоена недопустимая толщина -10")]
        [TestCase(-15,
            TestName = "Негативный тест. Толщина стен. Присвоена недопустимая толщина -15")]
        public void WallThicknessTest_GivenInvalidData_ThrowsException(int wallThickness)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.WallThickness = wallThickness);
        }

        [TestCase(20, TestName = "Минимальная длина между стенок вдоль вектора X. " +
                                 "Присвоено значение 20")]
        [TestCase(30, TestName = "Минимальная длина между стенок вдоль вектора X. " +
                                 "Присвоено значение 30")]
        [TestCase(40, TestName = "Минимальная длина между стенок вдоль вектора X. " +
                                 "Присвоено значение 40")]
        public void MinLengthBetweenWallsXTest_ShouldBeEqual(int length)
        {
            _figureSettings.MinLengthBetweenWallsX = length;
            Assert.AreEqual(length, _figureSettings.MinLengthBetweenWallsX);
        }

        [TestCase(-20, TestName = "Негативный тест. " +
                                  "Минимальная длина между стенок вдоль вектора X. " +
                                  "Присвоено значение -20")]
        [TestCase(-30, TestName = "Негативный тест. " +
                                  "Минимальная длина между стенок вдоль вектора X. " +
                                  "Присвоено значение -30")]
        [TestCase(-40, TestName = "Негативный тест. " +
                                  "Минимальная длина между стенок вдоль вектора X. " +
                                  "Присвоено значение -40")]
        public void MinLengthBetweenWallsXTest_GivenInvalidData_ThrowsException(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.MinLengthBetweenWallsX = length);
        }

        [TestCase(20, TestName = "Минимальная длина между стенок вдоль вектора Y. " +
                                 "Присвоено значение 20")]
        [TestCase(30, TestName = "Минимальная длина между стенок вдоль вектора Y. " +
                                 "Присвоено значение 30")]
        [TestCase(40, TestName = "Минимальная длина между стенок вдоль вектора Y. " +
                                 "Присвоено значение 40")]
        public void MinLengthBetweenWallsYTest_ShouldBeEqual(int length)
        {
            _figureSettings.MinLengthBetweenWallsY = length;
            Assert.AreEqual(length, _figureSettings.MinLengthBetweenWallsY);
        }

        [TestCase(-20, TestName = "Негативный тест. " +
                                  "Минимальная длина между стенок вдоль вектора Y. " +
                                  "Присвоено значение -20")]
        [TestCase(-30, TestName = "Негативный тест. " +
                                  "Минимальная длина между стенок вдоль вектора Y. " +
                                  "Присвоено значение -30")]
        [TestCase(-40, TestName = "Негативный тест. " +
                                  "Минимальная длина между стенок вдоль вектора Y. " +
                                  "Присвоено значение -40")]
        public void MinLengthBetweenWallsYTest_GivenInvalidData_ThrowsException(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureSettings.MinLengthBetweenWallsY = length);
        }

        [TestCase(TestName = "Проверка клонирования настроек фигуры FigureSettings")]
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
        /// <param name="testName">Название теста</param>
        public static IEnumerable SizeRage(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(new SizeRange {Min = 200, Max = 300}),
                new TestCaseData(new SizeRange {Min = 100, Max = 200}),
                new TestCaseData(new SizeRange {Min = 100, Max = 400})
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }
    }
}