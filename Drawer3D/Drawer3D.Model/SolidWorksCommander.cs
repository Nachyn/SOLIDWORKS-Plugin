using System;
using System.Diagnostics;
using Drawer3D.Model.Extensions;
using Drawer3D.Model.Interfaces;
using SolidWorks.Interop.sldworks;

namespace Drawer3D.Model
{
    public class SolidWorksCommander : ISolidWorksCommander
    {
        private const string TopAxisName = "Сверху";

        private const string SelectionAxisType = "PLANE";

        private const string PartFigureName = "Бобышка-Вытянуть";

        private const string SelectionBodyFeature = "BODYFEATURE";

        private const string SketchName = "Эскиз";

        private const string SelectionSketch = "SKETCH";


        private readonly SolidWorksSettings _appSettings;

        private SldWorks _app;

        private IModelDoc2 _document;

        public int BuildedPartFiguresCount { get; private set; }

        public bool IsConnectedToApp => _app != null && _document != null;

        public SolidWorksCommander(SolidWorksSettings appSettings)
        {
            _appSettings = appSettings ?? throw new ArgumentNullException();
        }

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

        public void ConnectToApp()
        {
            var appInstance = Activator.CreateInstance(
                Type.GetTypeFromCLSID(_appSettings.Guid));

            _app = (SldWorks) appInstance;
            _app.Visible = true;

            _app.NewPart();
            _document = _app.IActiveDoc2;

            BuildedPartFiguresCount = 0;
        }

        public void SaveToFile(string filePath)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.SaveAs3(filePath, 0, 0);
        }

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

        public void ClearSelection()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.ClearSelection2(true);
        }

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

        public void ToggleSketchMode()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.SketchManager.InsertSketch(true);
        }

        public void SelectByPoint(double pointX, double pointY, double pointZ)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.Extension.SelectByRay(pointX.ToMilli(),
                pointZ.ToMilli(),
                -pointY.ToMilli(),
                1, 1, 1, 1, 2, false, 0, 0);
        }

        public void SelectTopAxis()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.Extension.SelectByID2(TopAxisName, SelectionAxisType,
                0, 0, 0, false, 0, null, 0);
        }


        public void SelectPartFigure(int numberPartFigure)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.Extension.SelectByID2($"{PartFigureName}{numberPartFigure}"
                , SelectionBodyFeature, 0, 0, 0, true, 0, null, 0);
        }

        public void SelectSketch(int numberSketch)
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.Extension.SelectByID2($"{SketchName}{numberSketch}"
                , SelectionSketch, 0, 0, 0, true, 0, null, 0);
        }

        public void DeleteSelections()
        {
            if (!IsConnectedToApp)
            {
                return;
            }

            _document.EditDelete();
        }
    }
}