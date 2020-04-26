namespace Drawer3D.Model.Interfaces
{
    /// <summary>
    ///     API Команды к программе SOLIDWORKS
    /// </summary>
    public interface ISolidWorksCommander
    {
        /// <summary>
        ///     Количество построенных частей фигуры
        /// </summary>
        int BuildedPartFiguresCount { get; }

        /// <summary>
        ///     Есть ли подключение
        /// </summary>
        bool IsConnectedToApp { get; }

        /// <summary>
        ///     Закрыть программу
        /// </summary>
        void KillApp();

        /// <summary>
        ///     Подключиться к программе
        /// </summary>
        void ConnectToApp();

        /// <summary>
        ///     Сохранить проект
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        void SaveToFile(string filePath);

        /// <summary>
        ///     Вытянуть эскиз
        /// </summary>
        /// <param name="height">Высота</param>
        void ExtrudeSketch(double height);

        /// <summary>
        ///     Очистить выделения
        /// </summary>
        void ClearSelection();

        /// <summary>
        ///     Создать прямоугольник на эскизе
        /// </summary>
        /// <param name="x1">1 координата по X</param>
        /// <param name="y1">1 координата по Y</param>
        /// <param name="z1">1 координата по Z</param>
        /// <param name="x2">2 координата по X</param>
        /// <param name="y2">2 координата по Y</param>
        /// <param name="z2">2 координата по Z</param>
        void CreateRectangleOnSketch(double x1, double y1, double z1,
            double x2, double y2, double z2);

        /// <summary>
        ///     Перейти / Выйти из режима эскиза
        /// </summary>
        void ToggleSketchMode();

        /// <summary>
        ///     Выбрать объект по точке
        /// </summary>
        /// <param name="pointX">Координата по X</param>
        /// <param name="pointY">Координата по Y</param>
        /// <param name="pointZ">Координата по Z</param>
        void SelectByPoint(double pointX, double pointY, double pointZ);

        /// <summary>
        ///     Выбрать верхнюю ось
        /// </summary>
        void SelectTopAxis();

        /// <summary>
        ///     Выбрать часть фигуры
        /// </summary>
        /// <param name="numberPartFigure">Номер части</param>
        void SelectPartFigure(int numberPartFigure);

        /// <summary>
        ///     Выбрать эскиз
        /// </summary>
        /// <param name="numberSketch">Номер эскиза</param>
        void SelectSketch(int numberSketch);

        /// <summary>
        ///     Удалить выбранные объекты
        /// </summary>
        void DeleteSelections();

        /// <summary>
        ///     Увеличить по координатам
        /// </summary>
        /// <param name="x1">1 координата по X</param>
        /// <param name="y1">1 координата по Y</param>
        /// <param name="z1">1 координата по Z</param>
        /// <param name="x2">2 координата по X</param>
        /// <param name="y2">2 координата по Y</param>
        /// <param name="z2">2 координата по Z</param>
        void Zoom(double x1, double y1, double z1, double x2, double y2, double z2);

        /// <summary>
        ///     Увеличить фигуру в полный размер
        /// </summary>
        void ZoomToFit();
    }
}