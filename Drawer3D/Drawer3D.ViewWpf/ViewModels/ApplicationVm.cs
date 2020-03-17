using System;
using System.Windows;
using Drawer3D.Model;

namespace Drawer3D.ViewWpf.ViewModels
{
    public class ApplicationVm
    {
        private Drawer _drawer;

        private Figure _figure;

        public FigureVm Figure { get; }

        public ApplicationVm()
        {
            var figureSettings = new FigureSettings();
            _drawer = new Drawer(figureSettings
                , new SolidWorksCommander(GetSolidWorksSettings()));

            Figure = new FigureVm(_figure = new Figure(), figureSettings);
        }

        private SolidWorksSettings GetSolidWorksSettings()
        {
            var name = Application.Current.Resources["SolidWorksName"].ToString();
            var guid = Application.Current.Resources["SolidWorksGuid"].ToString();
            return new SolidWorksSettings {Guid = new Guid(guid), Name = name};
        }
    }
}