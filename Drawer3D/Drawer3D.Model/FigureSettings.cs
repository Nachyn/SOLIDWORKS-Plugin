using System;

namespace Drawer3D.Model
{
    /// <summary>
    ///     Настройки фигуры
    /// </summary>
    public class FigureSettings : ICloneable
    {
        /// <summary>
        ///     Минимальная длина между стенок вдоль вектора X
        /// </summary>
        private int _minLengthBetweenWallsX = 20;

        /// <summary>
        ///     Минимальная длина между стенок вдоль вектора Y
        /// </summary>
        private int _minLengthBetweenWallsY = 20;

        /// <summary>
        ///     Диапазон значений для длины
        /// </summary>
        private SizeRange _sizeX = new SizeRange {Max = 400, Min = 200};

        /// <summary>
        ///     Диапазон значений для ширины
        /// </summary>
        private SizeRange _sizeY = new SizeRange {Max = 400, Min = 200};

        /// <summary>
        ///     Диапазон значений для высоты
        /// </summary>
        private SizeRange _sizeZ = new SizeRange {Max = 150, Min = 50};

        /// <summary>
        ///     Толщина стен
        /// </summary>
        private int _wallThickness = 5;

        /// <summary>
        ///     Диапазон значений для длины
        /// </summary>
        public SizeRange SizeX
        {
            get => _sizeX;
            set => _sizeX = value ?? throw new ArgumentNullException();
        }

        /// <summary>
        ///     Диапазон значений для ширины
        /// </summary>
        public SizeRange SizeY
        {
            get => _sizeY;
            set => _sizeY = value ?? throw new ArgumentNullException();
        }

        /// <summary>
        ///     Диапазон значений для высоты
        /// </summary>
        public SizeRange SizeZ
        {
            get => _sizeZ;
            set => _sizeZ = value ?? throw new ArgumentNullException();
        }

        /// <summary>
        ///     Толщина стен
        /// </summary>
        public int WallThickness
        {
            get => _wallThickness;
            set
            {
                CheckPositiveArgument(value);
                _wallThickness = value;
            }
        }

        /// <summary>
        ///     Минимальная длина между стенок вдоль вектора X
        /// </summary>
        public int MinLengthBetweenWallsX
        {
            get => _minLengthBetweenWallsX;
            set
            {
                CheckPositiveArgument(value);
                _minLengthBetweenWallsX = value;
            }
        }

        /// <summary>
        ///     Минимальная длина между стенок вдоль вектора Y
        /// </summary>
        public int MinLengthBetweenWallsY
        {
            get => _minLengthBetweenWallsY;
            set
            {
                CheckPositiveArgument(value);
                _minLengthBetweenWallsY = value;
            }
        }

        /// <summary>
        ///     Клонировать настройки
        /// </summary>
        /// <returns>Текущие настройки</returns>
        public object Clone()
        {
            return new FigureSettings
            {
                MinLengthBetweenWallsX = MinLengthBetweenWallsX,
                MinLengthBetweenWallsY = MinLengthBetweenWallsY,
                SizeX = new SizeRange {Max = SizeX.Max, Min = SizeX.Min},
                SizeY = new SizeRange {Max = SizeY.Max, Min = SizeY.Min},
                SizeZ = new SizeRange {Max = SizeZ.Max, Min = SizeZ.Min},
                WallThickness = WallThickness
            };
        }

        /// <summary>
        ///     Проверить на положительное значение
        /// </summary>
        /// <param name="value">Значение</param>
        private static void CheckPositiveArgument(int value)
        {
            if (value > 0)
            {
                return;
            }

            throw new ArgumentOutOfRangeException("Need more than zero");
        }
    }
}