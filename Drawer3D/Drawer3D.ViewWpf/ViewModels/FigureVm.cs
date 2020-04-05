using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Drawer3D.Model;
using Drawer3D.Model.Enums;
using Drawer3D.ViewWpf.Annotations;
using Drawer3D.ViewWpf.Commands;
using Drawer3D.ViewWpf.Helpers;

namespace Drawer3D.ViewWpf.ViewModels
{
    /// <summary>
    ///     View-Model пользовательских параметров фигуры
    /// </summary>
    public class FigureVm : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        ///     Пользовательские параметры фигуры
        /// </summary>
        private readonly Figure _figure;

        /// <summary>
        ///     Допустимые настройки фигуры
        /// </summary>
        private readonly FigureSettings _figureSettings;

        /// <summary>
        ///     Валидатор настроек фигуры
        /// </summary>
        private readonly FigureValidator _figureValidator;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="figure">Пользовательские параметры фигуры</param>
        /// <param name="figureSettings">Допустимые настройки фигуры</param>
        public FigureVm(Figure figure
            , FigureSettings figureSettings)
        {
            _figure = figure ?? throw new ArgumentNullException();
            _figureSettings = figureSettings ?? throw new ArgumentNullException();
            _figureValidator = new FigureValidator(_figureSettings);

            if (_figure.WallsX == null)
            {
                _figure.WallsX = new Walls();
            }

            if (_figure.WallsY == null)
            {
                _figure.WallsY = new Walls();
            }

            WallPointsX = new ObservableCollection<PointVm>();
            WallPointsX.CollectionChanged += WallPointsX_CollectionChanged;

            WallPointsY = new ObservableCollection<PointVm>();
            WallPointsY.CollectionChanged += WallPointsY_CollectionChanged;
        }

        /// <summary>
        ///     Получить и задать длину
        /// </summary>
        public int X
        {
            get => _figure.X;
            set
            {
                _figure.X = value;
                OnPropertyChanged(nameof(X));
            }
        }

        /// <summary>
        ///     Получить и задать ширину
        /// </summary>
        public int Y
        {
            get => _figure.Y;
            set
            {
                _figure.Y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        /// <summary>
        ///     Получить и задать высоту
        /// </summary>
        public int Z
        {
            get => _figure.Z;
            set
            {
                _figure.Z = value;
                OnPropertyChanged(nameof(Z));
            }
        }

        /// <summary>
        ///     Получить и задать высоту стен вдоль вектора X
        /// </summary>
        public int HeightWallsX
        {
            get => _figure.WallsX.Height;
            set
            {
                _figure.WallsX.Height = value;
                OnPropertyChanged(nameof(HeightWallsX));
            }
        }

        /// <summary>
        ///     Получить и задать высоту стен вдоль вектора Y
        /// </summary>
        public int HeightWallsY
        {
            get => _figure.WallsY.Height;
            set
            {
                _figure.WallsY.Height = value;
                OnPropertyChanged(nameof(HeightWallsY));
            }
        }

        /// <summary>
        ///     Список View-Model точек-стен вдоль вектора X
        /// </summary>
        public ObservableCollection<PointVm> WallPointsX { get; }

        /// <summary>
        ///     Список View-Model точек-стен вдоль вектора Y
        /// </summary>
        public ObservableCollection<PointVm> WallPointsY { get; }

        /// <summary>
        ///     Команда для добавления стенки вдоль вектора X
        /// </summary>
        public RelayCommand AddWallPointX => new RelayCommand(obj =>
        {
            var pointVm = new PointVm(WallPointsX.Count
                , _figure
                , _figureValidator,
                Vector.X);

            WallPointsX.Add(pointVm);

            if (obj is int value)
            {
                pointVm.Value = value;
            }
        });

        /// <summary>
        ///     Команда для добавления стенки вдоль вектора Y
        /// </summary>
        public RelayCommand AddWallPointY => new RelayCommand(obj =>
        {
            var pointVm = new PointVm(WallPointsY.Count
                , _figure
                , _figureValidator,
                Vector.Y);

            WallPointsY.Add(pointVm);

            if (obj is int value)
            {
                pointVm.Value = value;
            }
        });

        /// <summary>
        ///     Команда для удаления последней стенки вдоль вектора X
        /// </summary>
        public RelayCommand RemoveLastWallPointX => new RelayCommand(obj =>
        {
            var lastPoint = WallPointsX.LastOrDefault();
            if (lastPoint != null)
            {
                WallPointsX.Remove(lastPoint);
            }
        });

        /// <summary>
        ///     Команда для удаления последней стенки вдоль вектора Y
        /// </summary>
        public RelayCommand RemoveLastWallPointY => new RelayCommand(obj =>
        {
            var lastPoint = WallPointsY.LastOrDefault();
            if (lastPoint != null)
            {
                WallPointsY.Remove(lastPoint);
            }
        });

        /// <summary>
        ///     Задать параметры фигуры по умолчанию
        /// </summary>
        public RelayCommand SetDefaultValues => new RelayCommand(obj =>
        {
            X = _figureSettings.SizeX.Min;
            Y = _figureSettings.SizeY.Min;
            Z = _figureSettings.SizeZ.Min;
            HeightWallsX = HeightWallsY = Z - _figureSettings.WallThickness;
            for (var i = 25; i <= 100; i += 25)
            {
                AddWallPointX.Execute(i);
                AddWallPointY.Execute(i);
            }
        });

        /// <summary>
        ///     Очистить все свойства (задать 0).
        /// </summary>
        public RelayCommand ClearValues => new RelayCommand(obj =>
        {
            X = Y = Z = HeightWallsX = HeightWallsY = 0;
        });

        /// <summary>
        ///     Валидировать текущее свойство
        /// </summary>
        /// <param name="columnName">Текущее свойство</param>
        /// <returns>Строка с ошибой, пустая строка - ошибок нет</returns>
        public string this[string columnName] =>
            ErrorInfoHelper.HandleErrorInfo(() =>
            {
                switch (columnName)
                {
                    case nameof(X):
                        _figureValidator.CheckSize(X, Vector.X);
                        break;

                    case nameof(Y):
                        _figureValidator.CheckSize(Y, Vector.Y);
                        break;

                    case nameof(Z):
                        _figureValidator.CheckSize(Z, Vector.Z);
                        break;

                    case nameof(HeightWallsX):
                        _figureValidator.CheckHeightWalls(HeightWallsX, Vector.X,
                            _figure.Z);

                        break;

                    case nameof(HeightWallsY):
                        _figureValidator.CheckHeightWalls(HeightWallsY, Vector.Y,
                            _figure.Z);

                        break;
                }
            });

        /// <summary>
        ///     Текущая ошибка валидации
        /// </summary>
        public string Error => throw new NotImplementedException();

        /// <summary>
        ///     Событие извещает систему об изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Извещает систему об изменении свойства
        /// </summary>
        /// <param name="propertyName">Имя свойства</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(Z))
            {
                CalculateWallsHeight();
            }

            switch (propertyName)
            {
                case nameof(HeightWallsX):
                    WallPointsX.Clear();
                    break;

                case nameof(HeightWallsY):
                    WallPointsY.Clear();
                    break;

                default:
                    WallPointsX.Clear();
                    WallPointsY.Clear();
                    break;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     Рассчитать высоту стен
        /// </summary>
        private void CalculateWallsHeight()
        {
            if (Z >= _figureSettings.SizeZ.Min)
            {
                HeightWallsY = HeightWallsX = Z - _figureSettings.WallThickness;
            }
        }

        private void WallPointsY_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            _figure.WallsY.Points = WallPointsY.Select(p => p.Value).ToList();
        }

        private void WallPointsX_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            _figure.WallsX.Points = WallPointsX.Select(p => p.Value).ToList();
        }
    }
}