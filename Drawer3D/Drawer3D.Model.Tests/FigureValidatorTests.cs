using System;
using System.Collections;
using System.Collections.Generic;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Exceptions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    /// <summary>
    ///     Модульные тесты для класса FigureValidator
    /// </summary>
    public class FigureValidatorTests
    {
        /// <summary>
        ///     Валидатор фигуры
        /// </summary>
        private FigureValidator _figureValidator;

        /// <summary>
        ///     Инициализировать перед каждым тестом
        /// </summary>
        [SetUp]
        public void InitializeEachTest()
        {
            _figureValidator =
                new FigureValidator(FigureValidatorTestsData.FigureSettings);
        }

        [TestCase(TestName = "Негативный тест. Нет подключения к SolidWorks")]
        public void ThrowAppNotConnectedTest()
        {
            Assert.Throws<FigureException>(() => _figureValidator.ThrowAppNotConnected());
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MinLengthBetweenWallsX)
            , new object[] {"Расчет минимальной длины между стенками вдоль векторов"})]
        public int GetMinLengthBetweenWallsTest_ShouldReturnValidLength(Vector vector)
        {
            return _figureValidator.GetMinLengthBetweenWalls(vector);
        }

        [TestCase(TestName = "Негативный тест. " +
                             "Расчет минимальной длины между стенками вдоль векторов. " +
                             "Передан вектор Z")]
        public void GetMinLengthBetweenWallsTest_GivenVectorZ_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureValidator.GetMinLengthBetweenWalls(Vector.Z));
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.VectorSizes)
            , new object[]
            {
                "Проверка размеров (д/ш/в). " +
                "Переданы размер, вектор"
            })]
        public void CheckSizeTest_ShouldBeComplete(int size, Vector vector)
        {
            _figureValidator.CheckSize(size, vector);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.VectorSizesNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Проверка размеров (д/ш/в). " +
                "Передан недопустимый размер для вектора"
            })]
        public void CheckSizeTest_GivenInvalidData_ThrowsException(int size, Vector vector)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckSize(size, vector));
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MaxLengthBetweenWallsX)
            , new object[] {"Расчет максимальной длины между стенками вдоль векторов"})]
        public int GetMaxLengthBetweenWallsTest_ShouldReturnValidLength(int size, Vector vector)
        {
            return _figureValidator.GetMaxLengthBetweenWalls(size, vector);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MaxCountWalls)
            , new object[] {"Расчет максимального количества стенок по вектору."})]
        public int GetMaxCountWallsTest(int size, Vector vector)
        {
            return _figureValidator.GetMaxCountWalls(size, vector);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.Walls)
            , new object[]
            {
                "Проверка стен вдоль векторов." +
                "Переданы размер (д/ш/в), вектор, стены, размер вектора Z"
            })]
        public void CheckWallsTest_ShouldBeComplete(int size, Vector vector
            , Walls walls, int sizeVectorZ)
        {
            _figureValidator.CheckWalls(size, vector, walls, sizeVectorZ);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.WallsNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Проверка стен вдоль векторов." +
                "Переданы недопустимые параметры - (д/ш/в), вектор, стены, размер вектора Z"
            })]
        public void CheckWallsTest_GivenInvalidData_ThrowsException(int size, Vector vector
            , Walls walls, int sizeVectorZ)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckWalls(size, vector, walls, sizeVectorZ));
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.WallsCountNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Проверка стен вдоль векторов." +
                "Передано недопустимое количество стен"
            })]
        public void CheckWallsTest_GivenInvalidCountWalls_ThrowsException(int size, Vector vector
            , Walls walls, int sizeVectorZ)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckWalls(size, vector, walls, sizeVectorZ));
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.HeightWalls)
            , new object[]
            {
                "Расчет высоты стен. " +
                "Переданы высота, вектор, размер вектора Z"
            })]
        public void CheckHeightWalls_ShouldBeCompete(int height, Vector vector, int sizeVectorZ)
        {
            _figureValidator.CheckHeightWalls(height, vector, sizeVectorZ);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.HeightWallsNegative)
            , new object[]
            {
                "Негативный тест. " +
                "Расчет высоты стен. " +
                "Переданы недопустимые значения - высота, вектор, размер вектора Z"
            })]
        public void CheckHeightWalls_GivenInvalidData_ThrowsException(int height, Vector vector,
            int sizeVectorZ)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckHeightWalls(height, vector, sizeVectorZ));
        }
    }

    /// <summary>
    ///     Тестовые данные для FigureValidatorTests
    /// </summary>
    public static class FigureValidatorTestsData
    {
        /// <summary>
        ///     Валидные настройки фигуры
        /// </summary>
        public static readonly FigureSettings FigureSettings = new FigureSettings
        {
            MinLengthBetweenWallsX = 20, MinLengthBetweenWallsY = 20,
            SizeX = new SizeRange {Min = 200, Max = 400},
            SizeY = new SizeRange {Min = 200, Max = 400},
            SizeZ = new SizeRange {Min = 50, Max = 150}, WallThickness = 5
        };

        /// <summary>
        ///     Валидные минимальные длины между стенами
        ///     вдоль вектора X
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable MinLengthBetweenWallsX(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(Vector.X).Returns(20),
                new TestCaseData(Vector.Y).Returns(20)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Валидные максимальные длины между стенами
        ///     вдоль вектора X
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable MaxLengthBetweenWallsX(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(200, Vector.X).Returns(165),
                new TestCaseData(200, Vector.Y).Returns(165),
                new TestCaseData(400, Vector.X).Returns(365),
                new TestCaseData(400, Vector.Y).Returns(365)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Валидные вектора с размерами
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable VectorSizes(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(200, Vector.X),
                new TestCaseData(400, Vector.X),
                new TestCaseData(200, Vector.Y),
                new TestCaseData(400, Vector.Y),
                new TestCaseData(50, Vector.Z),
                new TestCaseData(150, Vector.Z)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Недопустимые вектора с размерами
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable VectorSizesNegative(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(199, Vector.X),
                new TestCaseData(401, Vector.X),
                new TestCaseData(199, Vector.Y),
                new TestCaseData(401, Vector.Y),
                new TestCaseData(49, Vector.Z),
                new TestCaseData(151, Vector.Z)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Валидные векторы с их размерами
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable MaxCountWalls(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(200, Vector.X).Returns(6),
                new TestCaseData(200, Vector.Y).Returns(6),
                new TestCaseData(400, Vector.X).Returns(14),
                new TestCaseData(400, Vector.Y).Returns(14)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Валидные стены
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable Walls(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(200, Vector.X, null, 50),
                new TestCaseData(400, Vector.X, null, 150),
                new TestCaseData(200, Vector.Y, null, 50),
                new TestCaseData(400, Vector.Y, null, 150),
                new TestCaseData(200, Vector.Y,
                    new Walls {Height = 45, Points = new List<int> {25, 50, 75, 100}},
                    50),

                new TestCaseData(400, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {75, 100}}, 50)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Недопустимые стены
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable WallsNegative(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(199, Vector.Y,
                    new Walls {Height = 50, Points = new List<int> {25, 50, 75, 100}}
                    , 50),

                new TestCaseData(400, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {75, 76, 100}}
                    , 50),

                new TestCaseData(400, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {1, 2, 401}}
                    , 50)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Недопустимое количество стен
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable WallsCountNegative(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(200, Vector.Y,
                    new Walls {Height = 45, Points = new List<int> {25, 50, 75, 100, 125, 150, 175}}
                    , 50),

                new TestCaseData(100, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {25, 50, 75, 100, 125, 150, 175}},
                    50)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }


        /// <summary>
        ///     Валидные данные для проверки высоты стен
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable HeightWalls(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(145, Vector.X, 150),
                new TestCaseData(135, Vector.X, 140),
                new TestCaseData(100, Vector.X, 150),
                new TestCaseData(45, Vector.Y, 50),
                new TestCaseData(55, Vector.Y, 60),
                new TestCaseData(65, Vector.Y, 70)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }

        /// <summary>
        ///     Недопустимые данные для проверки высоты стен
        /// </summary>
        /// <param name="testName">Название теста</param>
        public static IEnumerable HeightWallsNegative(string testName)
        {
            var testCases = new List<TestCaseData>
            {
                new TestCaseData(0, Vector.X, 0),
                new TestCaseData(50, Vector.Y, 50),
                new TestCaseData(101, Vector.X, 100),
                new TestCaseData(149, Vector.X, 150)
            };

            testCases.ForEach(t => t.SetName(testName));
            return testCases;
        }
    }
}