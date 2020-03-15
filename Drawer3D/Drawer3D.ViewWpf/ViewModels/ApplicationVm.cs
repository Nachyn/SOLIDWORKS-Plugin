using System;
using System.Windows;
using Drawer3D.Model;

namespace Drawer3D.ViewWpf.ViewModels
{
    public class ApplicationVm
    {
        private Drawer _drawer;

        public ApplicationVm()
        {
            InitializeDrawer();
        }

        private void InitializeDrawer()
        {
            _drawer = new Drawer(new FigureSettings(),
                new SolidWorksCommander(GetSolidWorksSettings()));
        }

        private SolidWorksSettings GetSolidWorksSettings()
        {
            var name = Application.Current.Resources["SolidWorksName"].ToString();
            var guid = Application.Current.Resources["SolidWorksGuid"].ToString();
            return new SolidWorksSettings {Guid = new Guid(guid), Name = name};
        }
    }
}