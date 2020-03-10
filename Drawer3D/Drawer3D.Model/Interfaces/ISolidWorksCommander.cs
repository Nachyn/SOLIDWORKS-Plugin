namespace Drawer3D.Model.Interfaces
{
    public interface ISolidWorksCommander
    {
        int BuildedPartFiguresCount { get; }

        bool IsConnectedToApp { get; }

        void KillApp();

        void ConnectToApp();

        void SaveToFile(string filePath);

        void ExtrudeSketch(double height);

        void ClearSelection();

        void CreateRectangleOnSketch(double x1, double y1, double z1,
            double x2, double y2, double z2);

        void ToggleSketchMode();

        void SelectByPoint(double pointX, double pointY, double pointZ);

        void SelectTopAxis();

        void SelectPartFigure(int numberPartFigure);

        void SelectSketch(int numberSketch);

        void DeleteSelections();
    }
}