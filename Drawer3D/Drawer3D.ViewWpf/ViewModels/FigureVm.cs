using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Drawer3D.Model;
using Drawer3D.Model.Enums;
using Drawer3D.ViewWpf.Commands;
using Drawer3D.ViewWpf.Helpers;
using Drawer3D.ViewWpf.Properties;

namespace Drawer3D.ViewWpf.ViewModels
{
    public class FigureVm : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly Figure _figure;

        private readonly FigureValidator _figureValidator;

        public FigureVm(Figure figure
            , FigureSettings figureSettings)
        {
            _figure = figure ?? throw new ArgumentNullException();
            _figureValidator =
                new FigureValidator(figureSettings ?? throw new ArgumentNullException());

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

        public int X
        {
            get => _figure.X;
            set
            {
                _figure.X = value;
                OnPropertyChanged(nameof(X));
            }
        }

        public int Y
        {
            get => _figure.Y;
            set
            {
                _figure.Y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        public int Z
        {
            get => _figure.Z;
            set
            {
                _figure.Z = value;
                OnPropertyChanged(nameof(Z));
            }
        }

        public int HeightWallsX
        {
            get => _figure.WallsX.Height;
            set
            {
                _figure.WallsX.Height = value;
                OnPropertyChanged(nameof(HeightWallsX));
            }
        }

        public int HeightWallsY
        {
            get => _figure.WallsY.Height;
            set
            {
                _figure.WallsY.Height = value;
                OnPropertyChanged(nameof(HeightWallsY));
            }
        }

        public ObservableCollection<PointVm> WallPointsX { get; }

        public ObservableCollection<PointVm> WallPointsY { get; }

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

        public RelayCommand AddWallPointX => new RelayCommand(obj =>
        {
            WallPointsX.Add(new PointVm(WallPointsX.Count, _figure, _figureValidator,
                Vector.X));
        });

        public RelayCommand AddWallPointY => new RelayCommand(obj =>
        {
            WallPointsY.Add(new PointVm(WallPointsY.Count, _figure, _figureValidator,
                Vector.Y));
        });

        public RelayCommand RemoveLastWallPointX => new RelayCommand(obj =>
        {
            var lastPoint = WallPointsX.LastOrDefault();
            if (lastPoint != null)
            {
                WallPointsX.Remove(lastPoint);
            }
        });

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            ClearPoints();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ClearPoints()
        {
            WallPointsX.Clear();
            WallPointsY.Clear();
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