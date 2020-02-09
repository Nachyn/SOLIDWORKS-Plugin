using System;
using System.Collections.Generic;
using System.Diagnostics;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Extensions;
using SolidWorks.Interop.sldworks;

namespace Drawer3D.Model
{
    public class Drawer
    {
        private readonly DrawerAppSettings _appSettings;

        private SldWorks _app;

        private IModelDoc2 _document;

        private const string TopAxisName = "Сверху";

        private const string SelectionAxisType = "PLANE";

        private int? _x;

        private int? _y;

        private int? _z;

        private bool _isGridBuilt;

        public Drawer(DrawerAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

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

            _isGridBuilt = false;
            _x = _y = _z = null;
        }

        public void SaveToFile(string filePath)
        {
            CheckConnection();
            _document.SaveAs3(filePath, 0, 0);
        }

        public void BuildBase(int x, int y, int z)
        {
            CheckConnection();

            if (_x.HasValue || _y.HasValue || _z.HasValue)
            {
                FormValidator.ThrowBaseBuilt();
            }

            FormValidator.CheckSize(x, Vector.X);
            FormValidator.CheckSize(y, Vector.Y);
            FormValidator.CheckSize(z, Vector.Z);

            SelectTopAxis();

            ToggleSketchMode();
            CreateRectangleOnSketch(0, 0, 0, x, y, 0);
            ClearSelection();

            ToggleSketchMode();
            ExtrudeSketch(FormValidator.WallThickness);

            SelectByPoint(x / (double) 2, y / (double) 2, FormValidator.WallThickness);

            ToggleSketchMode();
            CreateRectangleOnSketch(0, 0, 0, x, y, 0);
            ClearSelection();

            CreateRectangleOnSketch(FormValidator.WallThickness
                , FormValidator.WallThickness
                , 0
                , x - FormValidator.WallThickness
                , y - FormValidator.WallThickness
                , 0);

            ClearSelection();

            ToggleSketchMode();
            ExtrudeSketch(z - FormValidator.WallThickness);

            _x = x;
            _y = y;
            _z = z;
        }

        public void BuildGrid(List<int> pointsX, List<int> pointsY)
        {
            CheckConnection();

            if (_isGridBuilt)
            {
                FormValidator.ThrowGridBuilt();
            }

            if (pointsX.IsNullOrEmpty() && pointsY.IsNullOrEmpty())
            {
                FormValidator.ThrowGridEmptyPoints();
            }

            if (!_x.HasValue || !_y.HasValue || !_z.HasValue)
            {
                FormValidator.ThrowBaseNotBuilt();
            }

            FormValidator.CheckWallPoints(_x.Value, pointsX, Vector.X);
            FormValidator.CheckWallPoints(_y.Value, pointsY, Vector.Y);

            CreateWalls(pointsX, Vector.X);
            CreateWalls(pointsY, Vector.Y);

            _isGridBuilt = true;
        }

        private void CreateWalls(List<int> points, Vector vector)
        {
            if (points.IsNullOrEmpty())
            {
                return;
            }

            var y1 = FormValidator.WallThickness;
            var z = FormValidator.WallThickness;

            var y2 = vector switch
            {
                Vector.X => _y.Value - FormValidator.WallThickness,

                Vector.Y => _x.Value - FormValidator.WallThickness,

                _ => throw new ArgumentOutOfRangeException(nameof(vector), vector, null)
            };

            SelectByPoint(FormValidator.WallThickness + 1
                , FormValidator.WallThickness + 1
                , FormValidator.WallThickness);

            ToggleSketchMode();

            foreach (var point in points)
            {
                var x1 = point;
                var x2 = point + FormValidator.WallThickness;

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

            ExtrudeSketch(_z.Value - FormValidator.WallThickness);
        }

        private void CheckConnection()
        {
            if (_app == null || _document == null)
            {
                FormValidator.ThrowAppNotConnected();
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