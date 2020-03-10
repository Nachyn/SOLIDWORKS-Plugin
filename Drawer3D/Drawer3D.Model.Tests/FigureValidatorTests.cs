using System;
using System.Collections;
using System.Collections.Generic;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Exceptions;
using NUnit.Framework;

namespace Drawer3D.Model.Tests
{
    public class FigureValidatorTests
    {
        private FigureValidator _figureValidator;

        [SetUp]
        public void InitializeTest()
        {
            _figureValidator =
                new FigureValidator(FigureValidatorTestsData.FigureSettings);
        }

        [Test]
        public void ThrowAppNotConnectedTest()
        {
            Assert.Throws<FigureException>(() => _figureValidator.ThrowAppNotConnected());
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MinLengthBetweenWallsX))]
        public int GetMinLengthBetweenWallsTest(Vector vector)
        {
            return _figureValidator.GetMinLengthBetweenWalls(vector);
        }

        [Test]
        public void GetMinLengthBetweenWallsTest_Negative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _figureValidator.GetMinLengthBetweenWalls(Vector.Z));
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.VectorSizes))]
        public void CheckSizeTest(int size, Vector vector)
        {
            _figureValidator.CheckSize(size, vector);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.VectorSizesNegative))]
        public void CheckSizeTest_Negative(int size, Vector vector)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckSize(size, vector));
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MaxLengthBetweenWallsX))]
        public int GetMaxLengthBetweenWallsTest(int size, Vector vector)
        {
            return _figureValidator.GetMaxLengthBetweenWalls(size, vector);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.MaxCountWalls))]
        public int GetMaxCountWallsTest(int size, Vector vector)
        {
            return _figureValidator.GetMaxCountWalls(size, vector);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.Walls))]
        public void CheckWallsTest(int size, Vector vector
            , Walls walls, int sizeVectorZ)
        {
            _figureValidator.CheckWalls(size, vector, walls, sizeVectorZ);
        }

        [TestCaseSource(typeof(FigureValidatorTestsData)
            , nameof(FigureValidatorTestsData.WallsNegative))]
        public void CheckWallsTest_Negative(int size, Vector vector
            , Walls walls, int sizeVectorZ)
        {
            Assert.Throws<FigureException>(() =>
                _figureValidator.CheckWalls(size, vector, walls, sizeVectorZ));
        }
    }

    public static class FigureValidatorTestsData
    {
        public static readonly FigureSettings FigureSettings = new FigureSettings
        {
            MinLengthBetweenWallsX = 20,
            MinLengthBetweenWallsY = 20,
            SizeX = new SizeRange {Min = 200, Max = 400},
            SizeY = new SizeRange {Min = 200, Max = 400},
            SizeZ = new SizeRange {Min = 50, Max = 150},
            WallThickness = 5
        };

        public static IEnumerable MinLengthBetweenWallsX
        {
            get
            {
                yield return new TestCaseData(Vector.X).Returns(20);
                yield return new TestCaseData(Vector.Y).Returns(20);
            }
        }

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

        public static IEnumerable WallsNegative
        {
            get
            {
                yield return new TestCaseData(199, Vector.Y,
                    new Walls {Height = 50, Points = new List<int> {25, 50, 75, 100}}
                    , 50);

                yield return new TestCaseData(400, Vector.Y,
                    new Walls {Height = 5, Points = new List<int> {75, 76, 100}}, 50);
            }
        }
    }
}