using System.Linq;
using System.Windows;
using Drawer3D.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace Drawer3D.ViewWpf.ViewModels
{
    /// <summary>
    ///     Главная View-Model приложения
    /// </summary>
    public class ApplicationVm : ViewModelBase
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
            private set => Set(() => IsProjectCreated, ref _isProjectCreated, value);
        }

        /// <summary>
        ///     Команда для создания нового проекта
        /// </summary>
        public RelayCommand CreateNewProject => new RelayCommand(() =>
        {
            _drawer.ConnectToApp();
            IsProjectCreated = true;
        });

        /// <summary>
        ///     Команда для построения фигуры
        /// </summary>
        public RelayCommand BuildFigure => new RelayCommand(() => _drawer.BuildFigure(_figure));

        /// <summary>
        ///     Команда для сохранения проекта
        /// </summary>
        public RelayCommand SaveProject => new RelayCommand(() =>
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
        ///     Получить настройки для программы SOLIDWORKS
        /// </summary>
        /// <returns>Настройки для программы SOLIDWORKS</returns>
        private SolidWorksSettings GetSolidWorksSettings()
        {
            var name = Application.Current.Resources["SolidWorksName"].ToString();
            var apiNumbers = Application.Current.Resources["SolidWorksApiNumbers"].ToString();

            return new SolidWorksSettings
            {
                Name = name, ApiNumbers = apiNumbers.Split(',')
                    .Select(int.Parse).ToList()
            };
        }
    }
}