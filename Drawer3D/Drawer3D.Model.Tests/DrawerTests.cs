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
        ///     Инициализировать каждый тест
        /// </summary>
        [SetUp]
        public void InitializeEachTest()
        {
            _commander = Substitute.For<ISolidWorksCommander>();
            _commander.IsConnectedToApp.Returns(true);
            _drawer = new Drawer(new FigureSettings(), _commander);
        }

        /// <summary>
        ///     Негативный тест конструктора
        /// </summary>
        [Test]
        public void ConstructorTest_GivenInvalidArguments_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new Drawer(null, null));
        }

        /// <summary>
        ///     Проверка свойства FigureSettings
        /// </summary>
        [Test]
        public void FigureSettingsTest_ShouldReturnCorrectInstance()
        {
            Assert.IsInstanceOf<FigureSettings>(_drawer.FigureSettings);
        }

        /// <summary>
        ///     Проверка метода CheckFigure
        /// </summary>
        /// <param name="figure">Валидные параметры фигуры</param>
        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures))]
        public void CheckFigureTest_ShouldBeComplete(Figure figure)
        {
            _drawer.CheckFigure(figure);
        }

        /// <summary>
        ///     Негативная проверка метода CheckFigure (null аргумент)
        /// </summary>
        [Test]
        public void CheckFigureTest_GivenNullArgument_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _drawer.CheckFigure(null));
        }

        /// <summary>
        ///     Негативная проверка метода CheckFigure (недопустимый аргумент)
        /// </summary>
        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.FiguresNegative))]
        public void CheckFigureTest_GivenInvalidArgument_ThrowsException(Figure figure)
        {
            Assert.Throws<FigureException>(() => _drawer.CheckFigure(figure));
        }

        /// <summary>
        ///     Проверка метода BuildFigure
        /// </summary>
        /// <param name="figure">Валидные параметры фигуры</param>
        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures))]
        public void BuildFigureTest_ShouldBeComplete(Figure figure)
        {
            _drawer.BuildFigure(figure);
        }

        /// <summary>
        ///     Проверка метода BuildFigure (должен перестроить)
        /// </summary>
        /// <param name="figure">Валидные параметры фигуры</param>
        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures))]
        public void BuildFigureTest_ShouldBeRebuild(Figure figure)
        {
            _commander.BuildedPartFiguresCount.Returns(1);
            _drawer.BuildFigure(figure);
        }

        /// <summary>
        ///     Проверка метода BuildFigure (должен перестроить)
        /// </summary>
        [Test]
        public void BuildFigureTest_GivenNullArgument_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _drawer.BuildFigure(null));
        }

        /// <summary>
        ///     Негативная проверка метода BuildFigure
        /// </summary>
        /// <param name="figure">Валидные параметры фигуры</param>
        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures))]
        public void BuildFigureTest_GivenDisconnect_ThrowsException(Figure figure)
        {
            _commander.IsConnectedToApp.Returns(false);
            Assert.Throws<FigureException>(() =>
                _drawer.BuildFigure(figure));
        }

        /// <summary>
        ///     Негативная проверка метода BuildFigure (недопустимый аргумент)
        /// </summary>
        /// <param name="figure">Недопустимые параметры фигуры</param>
        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.FiguresNegative))]
        public void BuildFigureTest_GivenInvalidArguments_ThrowsException(Figure figure)
        {
            Assert.Throws<FigureException>(() => _drawer.BuildFigure(figure));
        }

        /// <summary>
        ///     Проверка метода ConnectToApp
        /// </summary>
        [Test]
        public void ConnectToAppTest_ShouldBeComplete()
        {
            _drawer.ConnectToApp();
        }

        /// <summary>
        ///     Проверка метода SaveToFile
        /// </summary>
        [Test]
        public void SaveToFileTest_ShouldBeComplete()
        {
            _drawer.SaveToFile(string.Empty);
        }

        /// <summary>
        ///     Негативная проверка метода SaveToFile
        /// </summary>
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
        public static IEnumerable Figures
        {
            get
            {
                yield return new TestCaseData(new Figure {X = 200, Y = 200, Z = 50});
                yield return new TestCaseData(new Figure {X = 400, Y = 400, Z = 150});
                yield return new TestCaseData(new Figure
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
                });
            }
        }

        /// <summary>
        ///     Некорректные параметры фигуры
        /// </summary>
        public static IEnumerable FiguresNegative
        {
            get
            {
                yield return new TestCaseData(new Figure {X = 100, Y = 100, Z = 5});
                yield return new TestCaseData(new Figure {X = -1, Y = -1, Z = 0});
                yield return new TestCaseData(new Figure
                {
                    X = 350, Y = 300, Z = 100, WallsX = new Walls
                    {
                        Height = 95, Points = new List<int>
                        {
                            25, 25, 100, 125, 150
                        }
                    },
                    WallsY = new Walls
                    {
                        Height = 95, Points = new List<int>
                        {
                            25, 26, 100, 125, 150
                        }
                    }
                });
            }
        }
    }
}