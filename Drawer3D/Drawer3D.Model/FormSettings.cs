using System;

namespace Drawer3D.Model
{
    public class FormSettings : ICloneable
    {
        private int _minLengthBetweenWallsY = 20;

        private int _minLengthBetweenWallsX = 20;

        private int _wallThickness = 5;

        private SizeRange _sizeX = new SizeRange {Max = 400, Min = 200};

        private SizeRange _sizeY = new SizeRange {Max = 400, Min = 200};

        private SizeRange _sizeZ = new SizeRange {Max = 150, Min = 50};

        public SizeRange SizeX
        {
            get => _sizeX;
            set => _sizeX = value ?? throw new ArgumentNullException();
        }

        public SizeRange SizeY
        {
            get => _sizeY;
            set => _sizeY = value ?? throw new ArgumentNullException();
        }

        public SizeRange SizeZ
        {
            get => _sizeZ;
            set => _sizeZ = value ?? throw new ArgumentNullException();
        }

        public int WallThickness
        {
            get => _wallThickness;
            set
            {
                CheckPositiveArgumentArgument(value);
                _wallThickness = value;
            }
        }

        public int MinLengthBetweenWallsX
        {
            get => _minLengthBetweenWallsX;
            set
            {
                CheckPositiveArgumentArgument(value);
                _minLengthBetweenWallsX = value;
            }
        }

        public int MinLengthBetweenWallsY
        {
            get => _minLengthBetweenWallsY;
            set
            {
                CheckPositiveArgumentArgument(value);
                _minLengthBetweenWallsY = value;
            }
        }

        public object Clone()
        {
            return new FormSettings
            {
                MinLengthBetweenWallsX = MinLengthBetweenWallsX,
                MinLengthBetweenWallsY = MinLengthBetweenWallsY,
                SizeX = new SizeRange {Max = SizeX.Max, Min = SizeX.Min},
                SizeY = new SizeRange {Max = SizeY.Max, Min = SizeY.Min},
                SizeZ = new SizeRange {Max = SizeZ.Max, Min = SizeZ.Min},
                WallThickness = WallThickness
            };
        }

        private static void CheckPositiveArgumentArgument(int value)
        {
            if (value > 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException("Need more than zero");
        }
    }
}