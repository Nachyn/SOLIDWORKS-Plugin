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

        /// <summary>
        ///     Метод ThrowAppNotConnected должен выбросить исключение FigureException
        /// </summary>
        [Test]
        public void ThrowAppNotConnectedTest()
        {
            Assert.Throws<FigureException>(() => _figureValidator.ThrowAppNotConnected());
        }

        /// <summary>
        ///     Проверка метода GetMinLengthBetweenWalls
        /// </summary>
        /// <param name="vector">Вектор</param>
        /// <returns>Длина</returns>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MinLengthBetweenWallsX))]
        public int GetMinLengthBetweenWallsTest_ShouldReturnValidLength(Vector vector)
        {
            return _figureValidator.GetMinLengthBetweenWalls(vector);
        }

        /// <summary>
        ///     Негативная проверка метода GetMinLengthBetweenWalls
        /// </summary>
        [Test]
        public void GetMinLengthBetweenWallsTest_GivenVectorZ_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureValidator.GetMinLengthBetweenWalls(Vector.Z));
        }

        /// <summary>
        ///     Проверка метода CheckSize
        /// </summary>
        /// <param name="size">Валидные размеры</param>
        /// <param name="vector">Валидные вектора</param>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.VectorSizes))]
        public void CheckSizeTest_ShouldBeComplete(int size, Vector vector)
        {
            _figureValidator.CheckSize(size, vector);
        }

        /// <summary>
        ///     Проверка метода CheckSize
        /// </summary>
        /// <param name="size">Недопустимые размеры</param>
        /// <param name="vector">Недопустимые вектора</param>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.VectorSizesNegative))]
        public void CheckSizeTest_GivenInvalidData_ThrowsException(int size, Vector vector)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckSize(size, vector));
        }

        /// <summary>
        ///     Проверка метода GetMaxLengthBetweenWalls
        /// </summary>
        /// <param name="size">Валидные размеры</param>
        /// <param name="vector">Валидные вектора</param>
        /// <returns>Длина</returns>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MaxLengthBetweenWallsX))]
        public int GetMaxLengthBetweenWallsTest_ShouldReturnValidLength(int size, Vector vector)
        {
            return _figureValidator.GetMaxLengthBetweenWalls(size, vector);
        }

        /// <summary>
        ///     Проверка метода GetMaxCountWalls
        /// </summary>
        /// <param name="size">Валидные размеры</param>
        /// <param name="vector">Валидные вектора</param>
        /// <returns>Количество стен</returns>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MaxCountWalls))]
        public int GetMaxCountWallsTest(int size, Vector vector)
        {
            return _figureValidator.GetMaxCountWalls(size, vector);
        }

        /// <summary>
        ///     Проверка метода CheckWalls
        /// </summary>
        /// <param name="size">Валидные размеры</param>
        /// <param name="vector">Валидные вектора</param>
        /// <param name="walls">Стены</param>
        /// <param name="sizeVectorZ">Размер вектора Z</param>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.Walls))]
        public void CheckWallsTest_ShouldBeComplete(int size, Vector vector
            , Walls walls, int sizeVectorZ)
        {
            _figureValidator.CheckWalls(size, vector, walls, sizeVectorZ);
        }

        /// <summary>
        ///     Негативная проверка метода CheckWalls
        /// </summary>
        /// <param name="size">Валидные размеры</param>
        /// <param name="vector">Валидные вектора</param>
        /// <param name="walls">Стены</param>
        /// <param name="sizeVectorZ">Размер вектора Z</param>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.WallsNegative))]
        public void CheckWallsTest_GivenInvalidData_ThrowsException(int size, Vector vector
            , Walls walls, int sizeVectorZ)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckWalls(size, vector, walls, sizeVectorZ));
        }

        /// <summary>
        ///     Негативная проверка метода CheckWalls (недопустимое значение количества стен)
        /// </summary>
        /// <param name="size">Валидные размеры</param>
        /// <param name="vector">Валидные вектора</param>
        /// <param name="walls">Стены</param>
        /// <param name="sizeVectorZ">Размер вектора Z</param>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.WallsCountNegative))]
        public void CheckWallsTest_GivenInvalidCountWalls_ThrowsException(int size, Vector vector
            , Walls walls, int sizeVectorZ)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckWalls(size, vector, walls, sizeVectorZ));
        }

        /// <summary>
        ///     Проверка метода CheckHeightWalls
        /// </summary>
        /// <param name="height">Высота</param>
        /// <param name="vector">Вектор</param>
        /// <param name="sizeVectorZ">Размер вектора Z</param>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.HeightWalls))]
        public void CheckHeightWalls_ShouldBeCompete(int height, Vector vector, int sizeVectorZ)
        {
            _figureValidator.CheckHeightWalls(height, vector, sizeVectorZ);
        }

        /// <summary>
        ///     Проверка метода CheckHeightWalls
        /// </summary>
        /// <param name="height">Высота</param>
        /// <param name="vector">Вектор</param>
        /// <param name="sizeVectorZ">Размер вектора Z</param>
        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.HeightWallsNegative))]
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
        public static IEnumerable MinLengthBetweenWallsX
        {
            get
            {
                yield return new TestCaseData(Vector.X).Returns(20);
                yield return new TestCaseData(Vector.Y).Returns(20);
            }
        }

        /// <summary>
        ///     Валидные максимальные длины между стенами
        ///     вдоль вектора X
        /// </summary>
        public static IEnumerable MaxLengthBetweenWallsX
        {
            get
            {
                yield return new TestCaseData(200, Vector.X).Returns(165);
                yield return new TestCaseData(200, Vector.Y).Returns(165);
                yield return new TestCaseData(400, Vector.X).Returns(365);
                yield return new TestCaseData(400, Vector.Y).Returns(365);
            }
        }

        /// <summary>
        ///     Валидные вектора с размерами
        /// </summary>
        public static IEnumerable VectorSizes
        {
            get
            {
                yield return new TestCaseData(200, Vector.X);
                yield return new TestCaseData(400, Vector.X);
                yield return new TestCaseData(200, Vector.Y);
                yield return new TestCaseData(400, Vector.Y);
                yield return new TestCaseData(50, Vector.Z);
                yield return new TestCaseData(150, Vector.Z);
            }
        }

        /// <summary>
        ///     Недопустимые вектора с размерами
        /// </summary>
        public static IEnumerable VectorSizesNegative
        {
            get
            {
                yield return new TestCaseData(199, Vector.X);
                yield return new TestCaseData(401, Vector.X);
                yield return new TestCaseData(199, Vector.Y);
                yield return new TestCaseData(401, Vector.Y);
                yield return new TestCaseData(49, Vector.Z);
                yield return new TestCaseData(151, Vector.Z);
            }
        }

        /// <summary>
        ///     Валидные векторы с их размерами
        /// </summary>
        public static IEnumerable MaxCountWalls
        {
            get
            {
                yield return new TestCaseData(200, Vector.X).Returns(6);
                yield return new TestCaseData(200, Vector.Y).Returns(6);
                yield return new TestCaseData(400, Vector.X).Returns(14);
                yield return new TestCaseData(400, Vector.Y).Returns(14);
            }
        }

        /// <summary>
        ///     Валидные стены
        /// </summary>
        public static IEnumerable Walls
        {
            get
            {
                yield return new TestCaseData(200, Vector.X, null, 50);
                yield return new TestCaseData(400, Vector.X, null, 150);
                yield return new TestCaseData(200, Vector.Y, null, 50);
                yield return new TestCaseData(400, Vector.Y, null, 150);
                yield return new TestCaseData(200, Vector.Y,
                    new Walls {Height = 45, Points = new List<int> {25, 50, 75, 100}},
                    50);

                yield return new TestCaseData(400, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {75, 100}}, 50);
            }
        }

        /// <summary>
        ///     Недопустимые стены
        /// </summary>
        public static IEnumerable WallsNegative
        {
            get
            {
                yield return new TestCaseData(199, Vector.Y,
                    new Walls {Height = 50, Points = new List<int> {25, 50, 75, 100}}
                    , 50);

                yield return new TestCaseData(400, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {75, 76, 100}}
                    , 50);

                yield return new TestCaseData(400, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {1, 2, 401}}
                    , 50);
            }
        }

        /// <summary>
        ///     Недопустимое количество стен
        /// </summary>
        public static IEnumerable WallsCountNegative
        {
            get
            {
                yield return new TestCaseData(200, Vector.Y,
                    new Walls {Height = 45, Points = new List<int> {25, 50, 75, 100, 125, 150, 175}}
                    , 50);

                yield return new TestCaseData(100, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {25, 50, 75, 100, 125, 150, 175}},
                    50);
            }
        }


        /// <summary>
        ///     Валидные данные для проверки высоты стен
        /// </summary>
        public static IEnumerable HeightWalls
        {
            get
            {
                yield return new TestCaseData(145, Vector.X, 150);
                yield return new TestCaseData(135, Vector.X, 140);
                yield return new TestCaseData(100, Vector.X, 150);
                yield return new TestCaseData(45, Vector.Y, 50);
                yield return new TestCaseData(55, Vector.Y, 60);
                yield return new TestCaseData(65, Vector.Y, 70);
            }
        }

        /// <summary>
        ///     Недопустимые данные для проверки высоты стен
        /// </summary>
        public static IEnumerable HeightWallsNegative
        {
            get
            {
                yield return new TestCaseData(0, Vector.X, 0);
                yield return new TestCaseData(50, Vector.Y, 50);
                yield return new TestCaseData(101, Vector.X, 100);
                yield return new TestCaseData(149, Vector.X, 150);
            }
        }
    }
}