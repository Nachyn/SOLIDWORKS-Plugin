using System;
using System.Diagnostics;
using SolidWorks.Interop.sldworks;

namespace Drawer3D.Model
{
    public class Drawer
    {
        private readonly DrawerAppSettings _appSettings;

        private SldWorks _app;

        private IModelDoc2 _document;

        public Drawer(DrawerAppSettings appSettings)
        {
            _appSettings = appSettings;
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
    }
}