using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Drawer3D.Model;
using Drawer3D.ViewWpf.Annotations;
using Drawer3D.ViewWpf.Commands;
using Microsoft.Win32;

namespace Drawer3D.ViewWpf.ViewModels
{
    public class ApplicationVm : INotifyPropertyChanged
    {
        private readonly Drawer _drawer;

        private readonly Figure _figure;

        private bool _isProjectCreated;

        public FigureVm Figure { get; }

        public ApplicationVm()
        {
            var figureSettings = new FigureSettings();
            _drawer = new Drawer(figureSettings
                , new SolidWorksCommander(GetSolidWorksSettings()));

            Figure = new FigureVm(_figure = new Figure(), figureSettings);
        }

        public bool IsProjectCreated
        {
            get => _isProjectCreated;
            private set
            {
                _isProjectCreated = value;
                OnPropertyChanged(nameof(IsProjectCreated));
            }
        }

        public RelayCommand CreateNewProject => new RelayCommand(obj =>
        {
            _drawer.ConnectToApp();
            IsProjectCreated = true;
        });

        public RelayCommand BuildFigure => new RelayCommand(obj =>
        {
            _drawer.BuildFigure(_figure);
        });

        public RelayCommand SaveProject => new RelayCommand(obj =>
        {
            if (!IsProjectCreated)
            {
                return;
            }

            var saveDialog = new SaveFileDialog {Filter = "SLDPRT|*.SLDPRT"};
            if (saveDialog.ShowDialog() == true)
            {
                _drawer.SaveToFile(saveDialog.FileName);
            }
        });

        private SolidWorksSettings GetSolidWorksSettings()
        {
            var name = Application.Current.Resources["SolidWorksName"].ToString();
            var guid = Application.Current.Resources["SolidWorksGuid"].ToString();
            return new SolidWorksSettings {Guid = new Guid(guid), Name = name};
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}