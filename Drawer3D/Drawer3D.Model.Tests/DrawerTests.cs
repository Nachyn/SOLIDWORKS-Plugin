using System.Collections;
using System.Collections.Generic;
using Drawer3D.Model.Exceptions;
using Drawer3D.Model.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    public class DrawerTests
    {
        private Drawer _drawer;

        private ISolidWorksCommander _commander;

        [SetUp]
        public void InitializeTest()
        {
            _commander = Substitute.For<ISolidWorksCommander>();
            _commander.IsConnectedToApp.Returns(true);
            _drawer = new Drawer(new FigureSettings(), _commander);
        }

        [Test]
        public void FigureSettingsTest()
        {
            Assert.IsInstanceOf<FigureSettings>(_drawer.FigureSettings);
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures))]
        public void CheckFigureTest(Figure figure)
        {
            _drawer.CheckFigure(figure);
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.FiguresNegative))]
        public void CheckFigureTest_Negative(Figure figure)
        {
            Assert.Throws<FigureException>(() => _drawer.CheckFigure(figure));
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.Figures))]
        public void BuildFigureTest(Figure figure)
        {
            _drawer.BuildFigure(figure);
        }

        [TestCaseSource(typeof(DrawerTestsData), nameof(DrawerTestsData.FiguresNegative))]
        public void BuildFigureTest_Negative(Figure figure)
        {
            Assert.Throws<FigureException>(() => _drawer.BuildFigure(figure));
        }

        [Test]
        public void ConnectToApp()
        {
            _drawer.ConnectToApp();
        }

        [Test]
        public void SaveToFile()
        {
            _drawer.SaveToFile(string.Empty);
        }

        [Test]
        public void SaveToFile_Negative()
        {
            _commander.IsConnectedToApp.Returns(false);
            Assert.Throws<FigureException>(() => _drawer.SaveToFile(string.Empty));
        }
    }

    public static class DrawerTestsData
    {
        public static IEnumerable Figures
        {
            get
            {
                yield return new TestCaseData(new Figure {X = 200, Y = 200, Z = 50});
                yield return new TestCaseData(new Figure {X = 400, Y = 400, Z = 150});
                yield return new TestCaseData(new Figure
                {
                    X = 350,
                    Y = 300,
                    Z = 100,
                    WallsX = new Walls
                    {
                        Height = 95,
                        Points = new List<int>
                        {
                            25,
                            75,
                            100,
                            125,
                            150
                        }
                    },
                    WallsY = new Walls
                    {
                        Height = 95,
                        Points = new List<int>
                        {
                            25,
                            75,
                            100,
                            125,
                            150
                        }
                    }
                });
            }
        }

        public static IEnumerable FiguresNegative
        {
            get
            {
                yield return new TestCaseData(new Figure {X = 100, Y = 100, Z = 5});
                yield return new TestCaseData(new Figure {X = -1, Y = -1, Z = 0});
                yield return new TestCaseData(new Figure
                {
                    X = 350,
                    Y = 300,
                    Z = 100,
                    WallsX = new Walls
                    {
                        Height = 95,
                        Points = new List<int>
                        {
                            25,
                            25,
                            100,
                            125,
                            150
                        }
                    },
                    WallsY = new Walls
                    {
                        Height = 95,
                        Points = new List<int>
                        {
                            25,
                            26,
                            100,
                            125,
                            150
                        }
                    }
                });
            }
        }
    }
}