using System;
using System.Diagnostics;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Extensions;
using SolidWorks.Interop.sldworks;

namespace Drawer3D.Model
{
    public class Drawer
    {
        private readonly DrawerAppSettings _appSettings;

        private readonly FormValidator _formValidator;

        private readonly FormSettings _formSettings;

        private SldWorks _app;

        private IModelDoc2 _document;

        private const string TopAxisName = "Сверху";

        private const string SelectionAxisType = "PLANE";

        private int? _sizeX;

        private int? _sizeY;

        private bool _isFigureBuilt;

        public Drawer(DrawerAppSettings appSettings, FormSettings formSettings)
        {
            _appSettings = appSettings;
            _formValidator = new FormValidator(formSettings);
            _formSettings = formSettings;
        }

        public FormSettings FormSettings => (FormSettings) _formSettings.Clone();

        public void KillApp()
        {
            var processes = Process.GetProcessesByName(_appSettings.Name);
            foreach (var process in processes)
            {
                process.CloseMainWindow();
                process.Kill();
            }
        }

        public void ConnectToApp()
        {
            var appInstance = Activator.CreateInstance(
                Type.GetTypeFromCLSID(_appSettings.Guid));

            _app = (SldWorks) appInstance;
            _app.Visible = true;

            _app.NewPart();
            _document = _app.IActiveDoc2;

            _isFigureBuilt = false;
        }

        public void SaveToFile(string filePath)
        {
            CheckConnection();
            _document.SaveAs3(filePath, 0, 0);
        }

        public void CheckFigure(Figure figure)
        {
            if (figure == null)
            {
                throw new ArgumentNullException(nameof(figure));
            }

            _formValidator.CheckSize(figure.X, Vector.X);
            _formValidator.CheckSize(figure.Y, Vector.Y);
            _formValidator.CheckSize(figure.Z, Vector.Z);
            _formValidator.CheckWalls(figure.X, Vector.X, figure.WallsX, figure.Z);
            _formValidator.CheckWalls(figure.Y, Vector.Y, figure.WallsY, figure.Z);
        }

        public void BuildFigure(Figure figure)
        {
            if (figure == null)
            {
                throw new ArgumentNullException(nameof(figure));
            }

            if (_isFigureBuilt)
            {
                _formValidator.ThrowFigureBuilt();
            }

            CheckConnection();
            CheckFigure(figure);

            CreateBase(figure.X, figure.Y, figure.Z);
            CreateWalls(figure.WallsX, Vector.X);
            CreateWalls(figure.WallsY, Vector.Y);
            _isFigureBuilt = true;
        }

        private void CreateBase(int x, int y, int z)
        {
            SelectTopAxis();

            ToggleSketchMode();
            CreateRectangleOnSketch(0, 0, 0, x, y, 0);
            ClearSelection();

            ToggleSketchMode();
            ExtrudeSketch(_formSettings.WallThickness);

            SelectByPoint(x / (double) 2, y / (double) 2, _formSettings.WallThickness);

            ToggleSketchMode();
            CreateRectangleOnSketch(0, 0, 0, x, y, 0);
            ClearSelection();

            CreateRectangleOnSketch(_formSettings.WallThickness
                , _formSettings.WallThickness
                , 0
                , x - _formSettings.WallThickness
                , y - _formSettings.WallThickness
                , 0);

            ClearSelection();

            ToggleSketchMode();
            ExtrudeSketch(z - _formSettings.WallThickness);

            _sizeX = x;
            _sizeY = y;
        }

        private void CreateWalls(Walls walls, Vector vector)
        {
            if (walls == null || walls.Points.IsNullOrEmpty())
            {
                return;
            }

            var y1 = _formSettings.WallThickness;
            var z = _formSettings.WallThickness;

            var y2 = vector switch
            {
                Vector.X => _sizeY.Value - _formSettings.WallThickness,

                Vector.Y => _sizeX.Value - _formSettings.WallThickness,

                _ => throw new ArgumentOutOfRangeException(nameof(vector), vector, null)
            };

            SelectByPoint(_formSettings.WallThickness + 1
                , _formSettings.WallThickness + 1
                , _formSettings.WallThickness);

            ToggleSketchMode();

            foreach (var point in walls.Points)
            {
                var x1 = point;
                var x2 = point + _formSettings.WallThickness;

                switch (vector)
                {
                    case Vector.X:
                        CreateRectangleOnSketch(x1, y1, z, x2, y2, z);
                        break;
                    case Vector.Y:
                        CreateRectangleOnSketch(y1, x1, z, y2 + 0.00001, x2, z);
                        break;
                }

                ClearSelection();
            }

            ExtrudeSketch(walls.Height);
        }

        private void CheckConnection()
        {
            if (_app == null || _document == null)
            {
                _formValidator.ThrowAppNotConnected();
            }
        }

        private void ExtrudeSketch(double height)
        {
            var convertedHeight = height.ToMilli();

            _document.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0,
                convertedHeight, convertedHeight, false, false,
                false, false, 0, 0, false, false, false,
                false, true, true, true, 0, 0, false);

            _document.ISelectionManager.EnableContourSelection = false;
            ClearSelection();
        }

        private void SelectByPoint(double pointX, double pointY, double pointZ)
        {
            _document.Extension.SelectByRay(pointX.ToMilli(),
                pointZ.ToMilli(),
                -pointY.ToMilli(),
                1, 1, 1, 1, 2, false, 0, 0);
        }

        private void SelectTopAxis()
        {
            _document.Extension.SelectByID2(TopAxisName, SelectionAxisType,
                0, 0, 0, false, 0, null, 0);
        }

        private void ToggleSketchMode()
        {
            _document.SketchManager.InsertSketch(true);
        }

        private void CreateRectangleOnSketch(double x1, double y1, double z1,
            double x2, double y2, double z2)
        {
            _document.SketchManager.CreateCornerRectangle(x1.ToMilli(), y1.ToMilli(),
                z1.ToMilli(), x2.ToMilli(),
                y2.ToMilli(), z2.ToMilli());
        }

        private void ClearSelection()
        {
            _document.ClearSelection2(true);
        }
    }
}