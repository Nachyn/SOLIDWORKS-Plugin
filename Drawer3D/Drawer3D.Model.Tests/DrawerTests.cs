using System;
using System.Collections;
using System.Collections.Generic;
using Drawer3D.Model.Exceptions;
using Drawer3D.Model.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для класса Drawer
    /// </summary>
    public class DrawerTests
    {
        /// <summary>
        ///     API Команды к программе SOLIDWORKS
        /// </summary>
        private ISolidWorksCommander _commander;

        /// <summary>
        ///     Построитель-рисовальщик фигуры
        /// </summary>
        private Drawer _drawer;

        /// <summary>
        ///     Инициализировать перед каждым тестом
        /// </summary>
        [SetUp]
        public void InitializeEachTest()
        {
            _commander = Substitute.For<ISolidWorksCommander>();
            _commander.IsConnectedToApp.Returns(true);
            _drawer = new Drawer(new FigureSettings(), _commander);
        }

        [TestCase(TestName = "Негативный тест. Конструктору переданы null аргументы")]
        public void ConstructorTest_GivenInvalidArguments_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Drawer(null, null));
        }

        [TestCase(TestName = "Получение настроек фигуры FigureSettings")]
        public void FigureSettingsTest_ShouldReturnCorrectInstance()
        {
            Assert.IsInstanceOf<FigureSettings>(_drawer.FigureSettings);
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures)
            , new object[]
            {
                "Проверка пользовательских параметров фигуры. " +
                "Переданы валидные параметры"
            })]
        public void CheckFigureTest_ShouldBeComplete(Figure figure)
        {
            _drawer.CheckFigure(figure);
        }

        [TestCase(TestName = "Негативный тест. " +
                             "Проверка пользовательских параметров фигуры. " +
                             "Передан null аргумент)")]
        public void CheckFigureTest_GivenNullArgument_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _drawer.CheckFigure(null));
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.FiguresNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Проверка пользовательских параметров фигуры. " +
                "Переданы недопустимые аргументы"
            })]
        public void CheckFigureTest_GivenInvalidArgument_ThrowsException(Figure figure)
        {
            Assert.Throws<FigureException>(() => _drawer.CheckFigure(figure));
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures)
            , new object[]
            {
                "Построение фигуры. " +
                "Переданы валидные параметры"
            })]
        public void BuildFigureTest_ShouldBeComplete(Figure figure)
        {
            _drawer.BuildFigure(figure);
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures)
            , new object[]
            {
                "Перестроение фигуры. " +
                "Переданы валидные параметры"
            })]
        public void BuildFigureTest_ShouldBeRebuild(Figure figure)
        {
            _commander.BuildedPartFiguresCount.Returns(1);
            _drawer.BuildFigure(figure);
        }

        [TestCase(TestName = "Негативный тест. Построение фигуры. " +
                             "Передан null аргумент")]
        public void BuildFigureTest_GivenNullArgument_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _drawer.BuildFigure(null));
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures)
            , new object[]
            {
                "Негативный тест. " +
                "Построение фигуры. " +
                "Нет подключения к SolidWorks"
            })]
        public void BuildFigureTest_GivenDisconnect_ThrowsException(Figure figure)
        {
            _commander.IsConnectedToApp.Returns(false);
            Assert.Throws<FigureException>(() =>
                _drawer.BuildFigure(figure));
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.FiguresNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Построение фигуры. " +
                "Переданы некорректные параметры"
            })]
        public void BuildFigureTest_GivenInvalidArguments_ThrowsException(Figure figure)
        {
            Assert.Throws<FigureException>(() => _drawer.BuildFigure(figure));
        }

        [TestCase(TestName = "Подключение к SolidWorks")]
        public void ConnectToAppTest_ShouldBeComplete()
        {
            _drawer.ConnectToApp();
        }

        [TestCase(TestName = "Сохранение проекта в файл")]
        public void SaveToFileTest_ShouldBeComplete()
        {
            _drawer.SaveToFile(string.Empty);
        }

        [TestCase(TestName = "Негативный тест. " +
                             "Сохранение проекта в файл. " +
                             "Нет подключения в SolidWorks")]
        [Test]
        public void SaveToFileTest_GivenDisconnect_ThrowsException()
        {
            _commander.IsConnectedToApp.Returns(false);
            Assert.Throws<FigureException>(() => _drawer.SaveToFile(string.Empty));
        }
    }

    /// <summary>
    ///     Тестовые данные для класса DrawerTests
    /// </summary>
    public static class DrawerTestsData
    {
        /// <summary>
        ///     Валидные параметры фигуры
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable Figures(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(new Figure {X = 200, Y = 200, Z = 50}),
                new TestCaseData(new Figure {X = 400, Y = 400, Z = 150}),
                new TestCaseData(new Figure
                {
                    X = 350, Y = 300, Z = 100, WallsX = new Walls
                    {
                        Height = 95, Points = new List<int>
                        {
                            25, 75, 100, 125, 150
                        }
                    },
                    WallsY = new Walls
                    {
                        Height = 95, Points = new List<int>
                        {
                            25, 75, 100, 125, 150
                        }
                    }
                })
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Некорректные параметры фигуры
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable FiguresNegative(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(new Figure {X = 100, Y = 100, Z = 5}),
                new TestCaseData(new Figure {X = -1, Y = -1, Z = 0}),
                new TestCaseData(new Figure
                {
                    X = 350,
                    Y = 300,
                    Z = 100,
                    WallsX = new Walls
                    {
                        Height = 95,
                        Points = new List<int>
                        {
                            25, 25, 100, 125, 150
                        }
                    },
                    WallsY = new Walls
                    {
                        Height = 95,
                        Points = new List<int>
                        {
                            25, 26, 100, 125, 150
                        }
                    }
                })
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }
    }
}