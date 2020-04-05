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
    /// <summary>
    ///     Главная View-Model приложения
    /// </summary>
    public class ApplicationVm : INotifyPropertyChanged
    {
        /// <summary>
        ///     Построитель-рисовальщик
        /// </summary>
        private readonly Drawer _drawer;

        /// <summary>
        ///     Пользовательские параметры фигуры
        /// </summary>
        private readonly Figure _figure;

        /// <summary>
        ///     Создан ли проект (есть ли подключение к программе SOLIDWORKS)
        /// </summary>
        private bool _isProjectCreated;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public ApplicationVm()
        {
            var figureSettings = new FigureSettings();
            _drawer = new Drawer(figureSettings
                , new SolidWorksCommander(GetSolidWorksSettings()));

            Figure = new FigureVm(_figure = new Figure(), figureSettings);
        }

        /// <summary>
        ///     View-Model пользовательских параметров фигуры
        /// </summary>
        public FigureVm Figure { get; }

        /// <summary>
        ///     Создан ли проект (есть ли подключение к программе SOLIDWORKS)
        /// </summary>
        public bool IsProjectCreated
        {
            get => _isProjectCreated;
            private set
            {
                _isProjectCreated = value;
                OnPropertyChanged(nameof(IsProjectCreated));
            }
        }

        /// <summary>
        ///     Команда для создания нового проекта
        /// </summary>
        public RelayCommand CreateNewProject => new RelayCommand(obj =>
        {
            _drawer.ConnectToApp();
            IsProjectCreated = true;
        });

        /// <summary>
        ///     Команда для построения фигуры
        /// </summary>
        public RelayCommand BuildFigure => new RelayCommand(obj => { _drawer.BuildFigure(_figure); });

        /// <summary>
        ///     Команда для сохранения проекта
        /// </summary>
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

        /// <summary>
        ///     Событие извещает систему об изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Получить настройки для программы SOLIDWORKS
        /// </summary>
        /// <returns>Настройки для программы SOLIDWORKS</returns>
        private SolidWorksSettings GetSolidWorksSettings()
        {
            var name = Application.Current.Resources["SolidWorksName"].ToString();
            var guid = Application.Current.Resources["SolidWorksGuid"].ToString();
            return new SolidWorksSettings {Guid = new Guid(guid), Name = name};
        }

        /// <summary>
        ///     Извещает систему об изменении свойства
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}