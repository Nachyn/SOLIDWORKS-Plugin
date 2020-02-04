using System;
using System.Resources;
using Drawer3D.Model.Extensions;

namespace Drawer3D.Model
{
    public class FormValidator
    {
        public const int MaxX = 400;

        public const int MinX = 200;


        public const int MaxY = 400;

        public const int MinY = 200;


        public const int MaxZ = 50;

        public const int MinZ = 150;


        public const int WallThickness = 5;


        public const int MinLengthBetweenWallsX = 20;

        public const int MinLengthBetweenWallsY = 20;

        private readonly ResourceManager _resourceManager
            = new ResourceManager(typeof(Resources.FormValidator));

        public int MaxLengthBetweenWallsX(int x)
        {
            return x - (MinLengthBetweenWallsX + WallThickness * 3);
        }

        public int MaxLengthBetweenWallsY(int y)
        {
            return y - (MinLengthBetweenWallsY + WallThickness * 3);
        }


        public int GetMaxCountWallsX(int x)
        {
            return (int) Math.Floor((double)
                ((x - WallThickness * 2) /
                 (MaxLengthBetweenWallsX(x) + WallThickness)));
        }

        public int GetMaxCountWallsY(int y)
        {
            return (int) Math.Floor((double)
                ((y - WallThickness * 2) /
                 (MaxLengthBetweenWallsY(y) + WallThickness)));
        }

        public void CheckRangeX(int x)
        {
            if (x < MinX || x > MaxX)
            {
                throw new FormSizeException(
                    _resourceManager.GetFormattedString("RangeX", MinX, MaxX));
            }
        }

        public void CheckRangeY(int y)
        {
            if (y < MinY || y > MaxY)
            {
                throw new FormSizeException(
                    _resourceManager.GetFormattedString("RangeY", MinY, MaxY));
            }
        }

        public void CheckRangeZ(int z)
        {
            if (z < MinZ || z > MaxZ)
            {
                throw new FormSizeException(
                    _resourceManager.GetFormattedString("RangeZ", MinZ, MaxZ));
            }
        }
    }
}