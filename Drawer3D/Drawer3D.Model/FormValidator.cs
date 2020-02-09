using System;
using System.Collections.Generic;
using System.Resources;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Exceptions;
using Drawer3D.Model.Extensions;

namespace Drawer3D.Model
{
    public static class FormValidator
    {
        public const int MaxSizeX = 400;

        public const int MinSizeX = 200;


        public const int MaxSizeY = 400;

        public const int MinSizeY = 200;


        public const int MaxSizeZ = 150;

        public const int MinSizeZ = 50;


        public const int WallThickness = 5;


        public const int MinLengthBetweenWallsX = 20;

        public const int MinLengthBetweenWallsY = 20;

        private static readonly ResourceManager _resourceManager
            = new ResourceManager(typeof(Resources.FormValidator));

        public static void ThrowGridBuilt()
        {
            throw new FormException(_resourceManager
                .GetFormattedString("GridBuilt"));
        }

        public static void ThrowAppNotConnected()
        {
            throw new FormException(_resourceManager
                .GetFormattedString("AppNotConnected"));
        }

        public static void ThrowBaseNotBuilt()
        {
            throw new FormException(_resourceManager
                .GetFormattedString("BaseNotBuilt"));
        }

        public static void ThrowBaseBuilt()
        {
            throw new FormException(_resourceManager
                .GetFormattedString("BaseBuilt"));
        }

        public static int GetMinLengthBetweenWalls(Vector vector)
        {
            return vector switch
            {
                Vector.X => MinLengthBetweenWallsX,

                Vector.Y => MinLengthBetweenWallsY,

                _ => throw new ArgumentOutOfRangeException(nameof(vector), vector, null)
            };
        }

        public static int GetMaxLengthBetweenWalls(int size, Vector vector)
        {
            CheckSize(size, vector);
            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);
            return size - (minLengthBetweenWalls + WallThickness * 3);
        }

        public static int GetMaxCountWalls(int size, Vector vector)
        {
            CheckSize(size, vector);

            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);

            var countWalls =
                (size - WallThickness * 2) /
                (double) (minLengthBetweenWalls + WallThickness);

            return (int) countWalls - 1;
        }

        public static void CheckSize(int size, Vector vector)
        {
            int minSize;
            int maxSize;

            switch (vector)
            {
                case Vector.X:
                    minSize = MinSizeX;
                    maxSize = MaxSizeX;
                    break;

                case Vector.Y:
                    minSize = MinSizeY;
                    maxSize = MaxSizeY;
                    break;

                case Vector.Z:
                    minSize = MinSizeZ;
                    maxSize = MaxSizeZ;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(vector), vector, null);
            }

            if (size < minSize || size > maxSize)
            {
                throw new FormException(
                    _resourceManager.GetFormattedString("SizeVector"
                        , vector.GetEnumDescription()
                        , minSize
                        , maxSize));
            }
        }

        public static void CheckWallPoints(int size, List<int> points, Vector vector)
        {
            if (points.IsNullOrEmpty())
            {
                return;
            }

            var maxCountWallsX = GetMaxCountWalls(size, vector);
            if (points.Count > maxCountWallsX)
            {
                throw new FormException(
                    _resourceManager.GetFormattedString("MaxCountWalls"
                        , vector.GetEnumDescription()
                        , maxCountWallsX));
            }

            int? lastPoint = null;
            foreach (var point in points)
            {
                CheckValidPoints(size, lastPoint, point, vector);
                lastPoint = point;
            }
        }

        private static void CheckValidPoints(int size, int? lastPoint, int point,
            Vector vector)
        {
            CheckBorderPoint(size, point, vector);

            if (!lastPoint.HasValue)
            {
                return;
            }

            CheckBorderPoint(size, lastPoint.Value, vector);

            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);

            if (point - (lastPoint + WallThickness) < minLengthBetweenWalls)
            {
                throw new FormException(
                    _resourceManager.GetFormattedString("PointsInterval"
                        , lastPoint
                        , point
                        , vector.GetEnumDescription()
                        , minLengthBetweenWalls));
            }
        }

        private static void CheckBorderPoint(int size, int point, Vector vector)
        {
            var maxPoint = GetMaxLengthBetweenWalls(size, vector) + WallThickness;

            var minPoint = GetMinLengthBetweenWalls(vector) + WallThickness;

            if (point < minPoint || point > maxPoint)
            {
                throw new FormException(
                    _resourceManager.GetFormattedString("PointBorder"
                        , point
                        , vector.GetEnumDescription()
                        , minPoint
                        , maxPoint));
            }
        }
    }
}