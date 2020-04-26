using System;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Extensions;
using Drawer3D.Model.Interfaces;

namespace Drawer3D.Model
{
    /// <summary>
    ///     Построитель-рисовальщик фигуры
    /// </summary>
    public class Drawer
    {
        /// <summary>
        ///     API Команды к программе SOLIDWORKS
        /// </summary>
        private readonly ISolidWorksCommander _commander;

        /// <summary>
        ///     Настройки фигуры
        /// </summary>
        private readonly FigureSettings _figureSettings;

        /// <summary>
        ///     Валидатор фигуры
        /// </summary>
        private readonly FigureValidator _figureValidator;

        /// <summary>
        ///     Высота фигуры
        /// </summary>
        private int? _sizeX;

        /// <summary>
        ///     Ширина фигуры
        /// </summary>
        private int? _sizeY;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="figureSettings">Настройки фигуры</param>
        /// <param name="commander">API Команды к программе SOLIDWORKS</param>
        public Drawer(FigureSettings figureSettings
            , ISolidWorksCommander commander)
        {
            _figureValidator =
                new FigureValidator(figureSettings ?? throw new ArgumentNullException());

            _figureSettings = figureSettings;
            _commander = commander ?? throw new ArgumentNullException();
        }

        /// <summary>
        ///     Настройки фигуры
        /// </summary>
        public FigureSettings FigureSettings => (FigureSettings) _figureSettings.Clone();

        /// <summary>
        ///     Проверить фигуру
        /// </summary>
        /// <param name="figure">Параметры фигуры</param>
        public void CheckFigure(Figure figure)
        {
            if (figure == null)
            {
                throw new ArgumentNullException(nameof(figure));
            }

            _figureValidator.CheckSize(figure.X, Vector.X);
            _figureValidator.CheckSize(figure.Y, Vector.Y);
            _figureValidator.CheckSize(figure.Z, Vector.Z);
            _figureValidator.CheckWalls(figure.X, Vector.X, figure.WallsX, figure.Z);
            _figureValidator.CheckWalls(figure.Y, Vector.Y, figure.WallsY, figure.Z);
        }

        /// <summary>
        ///     Построить фигуру
        /// </summary>
        /// <param name="figure">Параметры фигуры</param>
        public void BuildFigure(Figure figure)
        {
            if (figure == null)
            {
                throw new ArgumentNullException(nameof(figure));
            }

            CheckConnection();
            if (_commander.BuildedPartFiguresCount > 0)
            {
                DeleteFigure();
            }

            CheckFigure(figure);

            CreateBase(figure.X, figure.Y, figure.Z);
            CreateWalls(figure.WallsX, Vector.X);
            CreateWalls(figure.WallsY, Vector.Y);
        }

        /// <summary>
        ///     Подключиться к программе SOLIDWORKS
        /// </summary>
        public void ConnectToApp()
        {
            _commander.ConnectToApp();
        }

        /// <summary>
        ///     Сохранить проект
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public void SaveToFile(string filePath)
        {
            CheckConnection();
            _commander.SaveToFile(filePath);
        }

        /// <summary>
        ///     Удалить фигуру
        /// </summary>
        private void DeleteFigure()
        {
            _commander.ClearSelection();
            for (var i = _commander.BuildedPartFiguresCount - 3;
                i <= _commander.BuildedPartFiguresCount;
                i++)
            {
                _commander.SelectPartFigure(i);
            }

            _commander.DeleteSelections();

            _commander.ClearSelection();
            for (var i = _commander.BuildedPartFiguresCount - 3;
                i <= _commander.BuildedPartFiguresCount;
                i++)
            {
                _commander.SelectSketch(i);
            }

            _commander.DeleteSelections();
        }

        /// <summary>
        ///     Построить основание фигуры
        /// </summary>
        /// <param name="x">Длина</param>
        /// <param name="y">Ширина</param>
        /// <param name="z">Высота</param>
        private void CreateBase(int x, int y, int z)
        {
            _commander.SelectTopAxis();

            _commander.ToggleSketchMode();
            _commander.CreateRectangleOnSketch(0, 0, 0, x, y, 0);
            _commander.ClearSelection();

            _commander.ToggleSketchMode();
            _commander.ExtrudeSketch(_figureSettings.WallThickness);

            _commander.SelectByPoint(x / (double) 2, y / (double) 2,
                _figureSettings.WallThickness);
            _commander.ToggleSketchMode();

            _commander.Zoom(-10, -10, 0, 15, 15, 0);

            _commander.CreateRectangleOnSketch(0, 0, 0, x, y, 0);
            _commander.ClearSelection();

            _commander.CreateRectangleOnSketch(_figureSettings.WallThickness
                , _figureSettings.WallThickness
                , 0
                , x - _figureSettings.WallThickness
                , y - _figureSettings.WallThickness
                , 0);
            _commander.ClearSelection();

            _commander.ToggleSketchMode();
            _commander.ExtrudeSketch(z - _figureSettings.WallThickness);
            _commander.ZoomToFit();
            _sizeX = x;
            _sizeY = y;
        }

        /// <summary>
        ///     Построить стены вдоль вектора
        /// </summary>
        /// <param name="walls">Стены</param>
        /// <param name="vector">Вектор</param>
        private void CreateWalls(Walls walls, Vector vector)
        {
            if (walls == null || walls.Points.IsNullOrEmpty())
            {
                return;
            }

            var y1 = _figureSettings.WallThickness;
            var z = _figureSettings.WallThickness;

            var y2 = vector switch
            {
                Vector.X => _sizeY.Value - _figureSettings.WallThickness,

                Vector.Y => _sizeX.Value - _figureSettings.WallThickness,

                _ => throw new ArgumentOutOfRangeException(nameof(vector), vector, null)
            };

            _commander.SelectByPoint(_figureSettings.WallThickness * 2
                , _figureSettings.WallThickness + 2
                , _figureSettings.WallThickness);

            _commander.ToggleSketchMode();

            foreach (var point in walls.Points)
            {
                var x1 = point;
                var x2 = point + _figureSettings.WallThickness;

                switch (vector)
                {
                    case Vector.X:
                        _commander.CreateRectangleOnSketch(x1, y1, z, x2, y2, z);
                        break;
                    case Vector.Y:
                        _commander.CreateRectangleOnSketch(y1, x1, z, y2 + 0.00001, x2,
                            z);

                        break;
                }

                _commander.ClearSelection();
            }

            _commander.ExtrudeSketch(walls.Height);
        }

        /// <summary>
        ///     Проверить подключение к SOLIDWORKS
        /// </summary>
        private void CheckConnection()
        {
            if (!_commander.IsConnectedToApp)
            {
                _figureValidator.ThrowAppNotConnected();
            }
        }
    }
}