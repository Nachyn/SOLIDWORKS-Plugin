using System;
using System.Resources;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Exceptions;
using Drawer3D.Model.Extensions;

namespace Drawer3D.Model
{
    public class FigureValidator
    {
        private readonly FigureSettings _figureSettings;

        private static readonly ResourceManager _resourceManager
            = new ResourceManager(typeof(Resources.FigureValidator));

        public FigureValidator(FigureSettings figureSettings)
        {
            _figureSettings = figureSettings ?? throw new ArgumentNullException();
        }

        public void ThrowFigureBuilt()
        {
            throw new FigureException("project", _resourceManager
                .GetFormattedString("FigureBuilt"));
        }

        public void ThrowAppNotConnected()
        {
            throw new FigureException("project", _resourceManager
                .GetFormattedString("AppNotConnected"));
        }

        public int GetMinLengthBetweenWalls(Vector vector)
        {
            return vector switch
            {
                Vector.X => _figureSettings.MinLengthBetweenWallsX,

                Vector.Y => _figureSettings.MinLengthBetweenWallsY,

                _ => throw new ArgumentOutOfRangeException(nameof(vector), vector, null)
            };
        }

        public int GetMaxLengthBetweenWalls(int size, Vector vector)
        {
            CheckSize(size, vector);
            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);
            return size - (minLengthBetweenWalls + _figureSettings.WallThickness * 3);
        }

        public int GetMaxCountWalls(int size, Vector vector)
        {
            CheckSize(size, vector);

            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);

            var countWalls =
                (size - _figureSettings.WallThickness * 2) /
                (double) (minLengthBetweenWalls + _figureSettings.WallThickness);

            return (int) countWalls - 1;
        }

        public void CheckSize(int size, Vector vector)
        {
            int minSize;
            int maxSize;

            switch (vector)
            {
                case Vector.X:
                    minSize = _figureSettings.SizeX.Min;
                    maxSize = _figureSettings.SizeX.Max;
                    break;

                case Vector.Y:
                    minSize = _figureSettings.SizeY.Min;
                    maxSize = _figureSettings.SizeY.Max;
                    break;

                case Vector.Z:
                    minSize = _figureSettings.SizeZ.Min;
                    maxSize = _figureSettings.SizeZ.Max;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(vector), vector, null);
            }

            if (size < minSize || size > maxSize)
            {
                var nameVector = vector.GetEnumDescription();
                throw new FigureException($"size{nameVector}"
                    , _resourceManager.GetFormattedString("SizeVector"
                        , nameVector
                        , minSize
                        , maxSize));
            }
        }

        public void CheckWalls(int size, Vector vector, Walls walls,
            int sizeVectorZ)
        {
            if (walls == null || walls.Points.IsNullOrEmpty())
            {
                return;
            }

            CheckSize(sizeVectorZ, Vector.Z);
            var maxHeight = sizeVectorZ - _figureSettings.WallThickness;
            var minHeight = _figureSettings.WallThickness;

            var vectorName = vector.GetEnumDescription();
            if (walls.Height < minHeight || walls.Height > maxHeight)
            {
                throw new FigureException($"heightWalls{vectorName}"
                    , _resourceManager.GetFormattedString("HeightWalls"
                        , vectorName
                        , minHeight
                        , maxHeight));
            }

            var maxCountWallsX = GetMaxCountWalls(size, vector);
            if (walls.Points.Count > maxCountWallsX)
            {
                throw new FigureException($"countWalls{vectorName}"
                    ,
                    _resourceManager.GetFormattedString("MaxCountWalls"
                        , vectorName
                        , maxCountWallsX));
            }

            int? lastPoint = null;
            var i = 1;
            foreach (var point in walls.Points)
            {
                CheckValidPoints(size, lastPoint, point, vector,
                    $"wall{vectorName}{i++}");

                lastPoint = point;
            }
        }

        private void CheckValidPoints(int size, int? lastPoint, int point,
            Vector vector, string errorKey)
        {
            CheckBorderPoint(size, point, vector, errorKey);

            if (!lastPoint.HasValue)
            {
                return;
            }

            CheckBorderPoint(size, lastPoint.Value, vector, errorKey);

            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector) +
                                        _figureSettings.WallThickness;

            if (point - lastPoint < minLengthBetweenWalls)
            {
                throw new FigureException(errorKey
                    , _resourceManager.GetFormattedString("PointsInterval"
                        , lastPoint
                        , point
                        , vector.GetEnumDescription()
                        , minLengthBetweenWalls));
            }
        }

        private void CheckBorderPoint(int size, int point, Vector vector, string errorKey)
        {
            var maxPoint = GetMaxLengthBetweenWalls(size, vector) +
                           _figureSettings.WallThickness;

            var minPoint = GetMinLengthBetweenWalls(vector) +
                           _figureSettings.WallThickness;

            if (point < minPoint || point > maxPoint)
            {
                throw new FigureException(errorKey
                    , _resourceManager.GetFormattedString("PointBorder"
                        , point
                        , vector.GetEnumDescription()
                        , minPoint
                        , maxPoint));
            }
        }
    }
}