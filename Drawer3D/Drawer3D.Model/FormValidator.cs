using System;
using System.Resources;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Exceptions;
using Drawer3D.Model.Extensions;

namespace Drawer3D.Model
{
    public class FormValidator
    {
        private readonly FormSettings _formSettings;

        private static readonly ResourceManager _resourceManager
            = new ResourceManager(typeof(Resources.FormValidator));

        public FormValidator(FormSettings formSettings)
        {
            _formSettings = formSettings ?? throw new ArgumentNullException();
        }

        public void ThrowFigureBuilt()
        {
            throw new FormException("project", _resourceManager
                .GetFormattedString("FigureBuilt"));
        }

        public void ThrowAppNotConnected()
        {
            throw new FormException("project", _resourceManager
                .GetFormattedString("AppNotConnected"));
        }

        public int GetMinLengthBetweenWalls(Vector vector)
        {
            return vector switch
            {
                Vector.X => _formSettings.MinLengthBetweenWallsX,

                Vector.Y => _formSettings.MinLengthBetweenWallsY,

                _ => throw new ArgumentOutOfRangeException(nameof(vector), vector, null)
            };
        }

        public int GetMaxLengthBetweenWalls(int size, Vector vector)
        {
            CheckSize(size, vector);
            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);
            return size - (minLengthBetweenWalls + _formSettings.WallThickness * 3);
        }

        public int GetMaxCountWalls(int size, Vector vector)
        {
            CheckSize(size, vector);

            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);

            var countWalls =
                (size - _formSettings.WallThickness * 2) /
                (double) (minLengthBetweenWalls + _formSettings.WallThickness);

            return (int) countWalls - 1;
        }

        public void CheckSize(int size, Vector vector)
        {
            int minSize;
            int maxSize;

            switch (vector)
            {
                case Vector.X:
                    minSize = _formSettings.SizeX.Min;
                    maxSize = _formSettings.SizeX.Max;
                    break;

                case Vector.Y:
                    minSize = _formSettings.SizeY.Min;
                    maxSize = _formSettings.SizeY.Max;
                    break;

                case Vector.Z:
                    minSize = _formSettings.SizeZ.Min;
                    maxSize = _formSettings.SizeZ.Max;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(vector), vector, null);
            }

            if (size < minSize || size > maxSize)
            {
                var nameVector = vector.GetEnumDescription();
                throw new FormException($"size{nameVector}"
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
            var maxHeight = sizeVectorZ - _formSettings.WallThickness;
            var minHeight = _formSettings.WallThickness;

            var vectorName = vector.GetEnumDescription();
            if (walls.Height < minHeight || walls.Height > maxHeight)
            {
                throw new FormException($"heightWalls{vectorName}"
                    , _resourceManager.GetFormattedString("HeightWalls"
                        , vectorName
                        , minHeight
                        , maxHeight));
            }

            var maxCountWallsX = GetMaxCountWalls(size, vector);
            if (walls.Points.Count > maxCountWallsX)
            {
                throw new FormException($"countWalls{vectorName}"
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
                                        _formSettings.WallThickness;

            if (point - lastPoint < minLengthBetweenWalls)
            {
                throw new FormException(errorKey
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
                           _formSettings.WallThickness;

            var minPoint = GetMinLengthBetweenWalls(vector) +
                           _formSettings.WallThickness;

            if (point < minPoint || point > maxPoint)
            {
                throw new FormException(errorKey
                    , _resourceManager.GetFormattedString("PointBorder"
                        , point
                        , vector.GetEnumDescription()
                        , minPoint
                        , maxPoint));
            }
        }
    }
}