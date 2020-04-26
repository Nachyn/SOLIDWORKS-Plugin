using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Drawer3D.Model.Extensions;
using Drawer3D.Model.Interfaces;
using SolidWorks.Interop.sldworks;

namespace Drawer3D.Model
{
    /// <summary>
    ///     API Команды к программе SOLIDWORKS
    /// </summary>
    public class SolidWorksCommander : ISolidWorksCommander
    {
        /// <summary>
        ///     Название верхней оси
        /// </summary>
        private const string TopAxisName = "Сверху";

        /// <summary>
        ///     Тип выделения для выбора оси
        /// </summary>
        private const string SelectionAxisType = "PLANE";

        /// <summary>
        ///     Название части фигуры
        /// </summary>
        private const string PartFigureName = "Бобышка-Вытянуть";

        /// <summary>
        ///     Тип выделения для тела объекта
        /// </summary>
        private const string SelectionBodyFeature = "BODYFEATURE";

        /// <summary>
        ///     Название эскиза
        /// </summary>
        private const string SketchName = "Эскиз";

        /// <summary>
        ///     Тип выделения для эскиза
        /// </summary>
        private const string SelectionSketch = "SKETCH";

        /// <summary>
        ///     Тип выделения с помощью точки
        /// </summary>
        private const string SelectionByPointsType = "FACE";


        /// <summary>
        ///     Настройки программы SOLIDWORKS
        /// </summary>
        private readonly SolidWorksSettings _appSettings;

        /// <summary>
        ///     Объект API программы SOLIDWORKS
        /// </summary>
        private SldWorks _app;

        /// <summary>
        ///     Объект API текущего проекта в программе SOLIDWORKS
        /// </summary>
        private IModelDoc2 _document;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="appSettings">Настройки программы SOLIDWORKS</param>
        public SolidWorksCommander(SolidWorksSettings appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException();
        }

        /// <summary>
        ///     Количество построенных частей фигуры
        /// </summary>
        public int BuildedPartFiguresCount { get; private set; }

        /// <summary>
        ///     Есть ли подключение
        /// </summary>
        public bool IsConnectedToApp => _app != null && _document != null;

        /// <summary>
        ///     Закрыть программу
        /// </summary>
        public void KillApp()
        {
            var processes = Process.GetProcessesByName(_appSettings.Name);
            foreach (var process in processes)
            {
                process.CloseMainWindow();
                process.Kill();
            }

            _document = null;
            _app = null;
        }

        /// <summary>
        ///     Подключиться к программе
        /// </summary>
        public void ConnectToApp()
        {
            Type solidWorksType = null;
            foreach (var number in _appSettings.ApiNumbers)
            {
                solidWorksType = Type.GetTypeFromProgID($"SldWorks.Application.{number}");
                if (solidWorksType != null)
                {
                    break;
                }
            }

            if (solidWorksType == null)
            {
                throw new ExternalException(string.Join(", ", _appSettings.ApiNumbers));
            }

            var appInstance = Activator.CreateInstance(solidWorksType);

            _app = (SldWorks) appInstance;
            _app.Visible = true;

            _app.NewPart();
            _document = _app.IActiveDoc2;

            BuildedPartFiguresCount = 0;
        }

        /// <summary>
        ///     Сохранить проект
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        public void SaveToFile(string filePath)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.SaveAs3(filePath, 0, 0);
        }

        /// <summary>
        ///     Вытянуть эскиз
        /// </summary>
        /// <param name="height">Высота</param>
        public void ExtrudeSketch(double height)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            var convertedHeight = height.ToMilli();

            _document.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0,
                convertedHeight, convertedHeight, false, false,
                false, false, 0, 0, false, false, false,
                false, true, true, true, 0, 0, false);

            _document.ISelectionManager.EnableContourSelection = false;
            ClearSelection();

            BuildedPartFiguresCount += 1;
        }

        /// <summary>
        ///     Очистить выделения
        /// </summary>
        public void ClearSelection()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.ClearSelection2(true);
        }

        /// <summary>
        ///     Создать прямоугольник на эскизе
        /// </summary>
        /// <param name="x1">1 координата по X</param>
        /// <param name="y1">1 координата по Y</param>
        /// <param name="z1">1 координата по Z</param>
        /// <param name="x2">2 координата по X</param>
        /// <param name="y2">2 координата по Y</param>
        /// <param name="z2">2 координата по Z</param>
        public void CreateRectangleOnSketch(double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.SketchManager.CreateCornerRectangle(x1.ToMilli(), y1.ToMilli(),
                z1.ToMilli(), x2.ToMilli(),
                y2.ToMilli(), z2.ToMilli());
        }

        /// <summary>
        ///     Перейти / Выйти из режима эскиза
        /// </summary>
        public void ToggleSketchMode()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.SketchManager.InsertSketch(true);
        }

        /// <summary>
        ///     Выбрать объект по точке
        /// </summary>
        /// <param name="pointX">Координата по X</param>
        /// <param name="pointY">Координата по Y</param>
        /// <param name="pointZ">Координата по Z</param>
        public void SelectByPoint(double pointX, double pointY, double pointZ)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.Extension.SelectByID2(string.Empty, SelectionByPointsType
                , pointX.ToMilli(), pointZ.ToMilli(), -pointY.ToMilli()
                , false, 0, null, 0);
        }

        /// <summary>
        ///     Выбрать верхнюю ось
        /// </summary>
        public void SelectTopAxis()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.Extension.SelectByID2(TopAxisName, SelectionAxisType,
                0, 0, 0, false, 0, null, 0);
        }

        /// <summary>
        ///     Выбрать часть фигуры
        /// </summary>
        /// <param name="numberPartFigure">Номер части</param>
        public void SelectPartFigure(int numberPartFigure)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.Extension.SelectByID2($"{PartFigureName}{numberPartFigure}"
                , SelectionBodyFeature, 0, 0, 0, true, 0, null, 0);
        }

        /// <summary>
        ///     Выбрать эскиз
        /// </summary>
        /// <param name="numberSketch">Номер эскиза</param>
        public void SelectSketch(int numberSketch)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.Extension.SelectByID2($"{SketchName}{numberSketch}"
                , SelectionSketch, 0, 0, 0, true, 0, null, 0);
        }

        /// <summary>
        ///     Удалить выбранные объекты
        /// </summary>
        public void DeleteSelections()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.EditDelete();
        }

        /// <summary>
        ///     Увеличить по координатам
        /// </summary>
        /// <param name="x1">1 координата по X</param>
        /// <param name="y1">1 координата по Y</param>
        /// <param name="z1">1 координата по Z</param>
        /// <param name="x2">2 координата по X</param>
        /// <param name="y2">2 координата по Y</param>
        /// <param name="z2">2 координата по Z</param>
        public void Zoom(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.ViewZoomTo2(x1.ToMilli(), y1.ToMilli(), z1.ToMilli()
                , x2.ToMilli(), y2.ToMilli(), z2.ToMilli());
        }

        /// <summary>
        ///     Увеличить фигуру в полный размер
        /// </summary>
        public void ZoomToFit()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.ViewZoomtofit2();
        }
    }
}