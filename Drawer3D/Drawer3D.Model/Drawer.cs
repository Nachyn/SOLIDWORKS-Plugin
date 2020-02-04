using System;
using System.Diagnostics;
using Drawer3D.Model.Extensions;
using SolidWorks.Interop.sldworks;

namespace Drawer3D.Model
{
    public class Drawer
    {
        private readonly DrawerAppSettings _appSettings;

        private readonly FormValidator _formValidator;

        private SldWorks _app;

        private IModelDoc2 _document;

        public Drawer(DrawerAppSettings appSettings
            , FormValidator formValidator)
        {
            _appSettings = appSettings;
            _formValidator = formValidator;
            ConnectToApp();
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

        private void ConnectToApp()
        {
            var appInstance = Activator.CreateInstance(
                Type.GetTypeFromCLSID(_appSettings.Guid));

            _app = (SldWorks) appInstance;
            _app.Visible = true;

            _app.NewPart();
            _document = _app.IActiveDoc2;
        }

        public void BuildBase(int x, int y, int z)
        {
            _formValidator.CheckRangeX(x);
            _formValidator.CheckRangeY(y);
            _formValidator.CheckRangeZ(z);

            _document.Extension.SelectByID2("Сверху", "PLANE", 0, 0, 0,
                false, 0, null, 0);

            _document.SketchManager.InsertSketch(true);
            _document.SketchManager.CreateCornerRectangle(0, 0, 0, x.ToMilli(),
                y.ToMilli(), 0);

            _document.ClearSelection2(true);

            _document.SketchManager.InsertSketch(true);
            ExtrudeSketch(FormValidator.WallThickness);

            SelectByRay(x / (double) 2, y / (double) 2, z);

            _document.SketchManager.InsertSketch(true);
            _document.SketchManager.CreateCornerRectangle(0, 0, 0, x.ToMilli(),
                y.ToMilli(), 0);

            _document.ClearSelection2(true);
            _document.SketchManager.CreateCornerRectangle(
                FormValidator.WallThickness.ToMilli(),
                FormValidator.WallThickness.ToMilli(), 0,
                (x - FormValidator.WallThickness).ToMilli(),
                (y - FormValidator.WallThickness).ToMilli(), 0);

            _document.ClearSelection2(true);
            _document.SketchManager.InsertSketch(true);
            ExtrudeSketch(z - FormValidator.WallThickness);
        }

        private void ExtrudeSketch(double height)
        {
            var convertedHeight = height.ToMilli();

            _document.FeatureManager.FeatureExtrusion2(true, false, false, 0, 0,
                convertedHeight, convertedHeight, false, false,
                false, false, 0, 0, false, false, false,
                false, true, true, true, 0, 0, false);

            _document.ISelectionManager.EnableContourSelection = false;
            _document.ClearSelection2(true);
        }

        private void SelectByRay(double x, double y, double z)
        {
            _document.Extension.SelectByRay(x.ToMilli(), z.ToMilli(), -y.ToMilli(),
                -0.4000360267793123259, -0.5150380749100240685, -0.7580942940502836125,
                0.0004417696109455605314, 2, false, 0, 0);
        }
    }
}