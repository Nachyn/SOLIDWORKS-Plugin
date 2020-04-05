namespace Drawer3D.Model
{
    /// <summary>
    ///     Пользовательские параметры фигуры
    /// </summary>
    public class Figure
    {
        /// <summary>
        ///     Длина
        /// </summary>
        public int X { get; set; }

        /// <summary>
        ///     Ширина
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        ///     Высота
        /// </summary>
        public int Z { get; set; }

        /// <summary>
        ///     Стены вдоль вектора X
        /// </summary>
        public Walls WallsX { get; set; }

        /// <summary>
        ///     Стены вдоль вектора Y
        /// </summary>
        public Walls WallsY { get; set; }
    }
}