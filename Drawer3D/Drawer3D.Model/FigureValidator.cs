using System;
using System.Resources;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Exceptions;
using Drawer3D.Model.Extensions;

namespace Drawer3D.Model
{
    /// <summary>
    ///     Валидатор фигуры
    /// </summary>
    public class FigureValidator
    {
        /// <summary>
        ///     Ресурсы
        /// </summary>
        private static readonly ResourceManager ResourceManager
            = new ResourceManager(typeof(Resources.FigureValidator));

        /// <summary>
        ///     Настройки фигуры
        /// </summary>
        private readonly FigureSettings _figureSettings;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="figureSettings">Настройки фигуры</param>
        public FigureValidator(FigureSettings figureSettings)
        {
            _figureSettings = figureSettings ?? throw new ArgumentNullException();
        }

        /// <summary>
        ///     Выбросить исключение "Нет подключения к программе SOLIDWORKS"
        /// </summary>
        public void ThrowAppNotConnected()
        {
            throw new FigureException("project", ResourceManager
                .GetFormattedString("AppNotConnected"));
        }

        /// <summary>
        ///     Рассчитать минимальную длину между стенками вектора vector
        /// </summary>
        /// <param name="vector">Вектор</param>
        /// <returns>Минимальная длина между стенками</returns>
        public int GetMinLengthBetweenWalls(Vector vector)
        {
            return vector switch
            {
                Vector.X => _figureSettings.MinLengthBetweenWallsX,
                Vector.Y => _figureSettings.MinLengthBetweenWallsY,
                _ => throw new ArgumentOutOfRangeException(nameof(vector), vector, null)
            };
        }

        /// <summary>
        ///     Рассчитать максимальную длину между стенками вектора vector
        /// </summary>
        /// <param name="size">Текущий размер (д/ш/в)</param>
        /// <param name="vector">Вектор</param>
        /// <returns>Максимальная длина между стенками</returns>
        public int GetMaxLengthBetweenWalls(int size, Vector vector)
        {
            CheckSize(size, vector);
            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);
            return size - (minLengthBetweenWalls + _figureSettings.WallThickness * 3);
        }

        /// <summary>
        ///     Рассчитать максимальное количество стенок по вектору vector
        /// </summary>
        /// <param name="size">Текущий размер (д/ш/в)</param>
        /// <param name="vector">Вектор</param>
        /// <returns>Максимальное количество стен</returns>
        public int GetMaxCountWalls(int size, Vector vector)
        {
            CheckSize(size, vector);

            var minLengthBetweenWalls = GetMinLengthBetweenWalls(vector);

            var countWalls =
                (size - _figureSettings.WallThickness * 2) /
                (double) (minLengthBetweenWalls + _figureSettings.WallThickness);

            return (int) countWalls - 1;
        }

        /// <summary>
        ///     Проверить размер (д/ш/в) по вектору vector
        /// </summary>
        /// <param name="size">Размер</param>
        /// <param name="vector">Вектор</param>
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
                    , ResourceManager.GetFormattedString("SizeVector"
                        , nameVector
                        , minSize
                        , maxSize));
            }
        }

        /// <summary>
        ///     Проверить высоту стен
        /// </summary>
        /// <param name="height">Высота стены</param>
        /// <param name="vector">Вектор</param>
        /// <param name="sizeVectorZ">Высота Z</param>
        public void CheckHeightWalls(int height, Vector vector
            , int sizeVectorZ)
        {
            CheckSize(sizeVectorZ, Vector.Z);
            var maxHeight = sizeVectorZ - _figureSettings.WallThickness;
            var minHeight = _figureSettings.WallThickness;

            var vectorName = vector.GetEnumDescription();
            if (height < minHeight || height > maxHeight)
            {
                throw new FigureException($"heightWalls{vectorName}"
                    , ResourceManager.GetFormattedString("HeightWalls"
                        , vectorName
                        , minHeight
                        , maxHeight));
            }
        }

        /// <summary>
        ///     Проверить стены
        /// </summary>
        /// <param name="size">Размер (д/ш/в)</param>
        /// <param name="vector">Вектор</param>
        /// <param name="walls">Стены</param>
        /// <param name="sizeVectorZ">Высота Z</param>
        public void CheckWalls(int size, Vector vector, Walls walls
            , int sizeVectorZ)
        {
            if (walls == null || walls.Points.IsNullOrEmpty())
            {
                return;
            }

            var vectorName = vector.GetEnumDescription();
            CheckHeightWalls(walls.Height, vector, sizeVectorZ);

            var maxCountWallsX = GetMaxCountWalls(size, vector);
            if (walls.Points.Count > maxCountWallsX)
            {
                throw new FigureException($"countWalls{vectorName}"
                    ,
                    ResourceManager.GetFormattedString("MaxCountWalls"
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

        /// <summary>
        ///     Проверить точки по которым строятся стены
        /// </summary>
        /// <param name="size">Размер (д/ш/в)</param>
        /// <param name="lastPoint">Последняя точка</param>
        /// <param name="point">Текущая точка</param>
        /// <param name="vector">Вектор</param>
        /// <param name="errorKey">Тип ошибки</param>
        private void CheckValidPoints(int size, int? lastPoint, int point
            , Vector vector, string errorKey)
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
                    , ResourceManager.GetFormattedString("PointsInterval"
                        , lastPoint
                        , point
                        , vector.GetEnumDescription()
                        , minLengthBetweenWalls));
            }
        }

        /// <summary>
        ///     Проверить границы фигуры
        /// </summary>
        /// <param name="size">Размер (д/ш/в)</param>
        /// <param name="point">Текущая точка</param>
        /// <param name="vector">Вектор</param>
        /// <param name="errorKey">Тип ошибки</param>
        private void CheckBorderPoint(int size, int point, Vector vector, string errorKey)
        {
            var maxPoint = GetMaxLengthBetweenWalls(size, vector) +
                           _figureSettings.WallThickness;

            var minPoint = GetMinLengthBetweenWalls(vector) +
                           _figureSettings.WallThickness;

            if (point < minPoint || point > maxPoint)
            {
                throw new FigureException(errorKey
                    , ResourceManager.GetFormattedString("PointBorder"
                        , point
                        , vector.GetEnumDescription()
                        , minPoint
                        , maxPoint));
            }
        }
    }
}