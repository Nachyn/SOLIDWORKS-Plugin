using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Drawer3D.Model;
using Drawer3D.Model.Enums;
using Drawer3D.ViewWpf.Helpers;
using Drawer3D.ViewWpf.Properties;

namespace Drawer3D.ViewWpf.ViewModels
{
    public class PointVm : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly int _index;

        private int _value;

        private readonly Figure _figure;

        private readonly FigureValidator _figureValidator;

        private readonly Vector _vector;

        public event PropertyChangedEventHandler PropertyChanged;

        public PointVm(int index
            , Figure figure
            , FigureValidator figureValidator
            , Vector vector)
        {
            _index = index;
            _figure = figure;
            _figureValidator = figureValidator;
            _vector = vector;
        }

        public int Value
        {
            get => _value;

            set
            {
                switch (_vector)
                {
                    case Vector.X:
                        _figure.WallsX.Points[_index] = value;
                        break;

                    case Vector.Y:
                        _figure.WallsY.Points[_index] = value;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public string this[string columnName] =>
            ErrorInfoHelper.HandleErrorInfo(() =>
            {
                Walls walls;
                int size;

                switch (_vector)
                {
                    case Vector.X:
                        walls = _figure.WallsX;
                        size = _figure.X;
                        break;

                    case Vector.Y:
                        walls = _figure.WallsY;
                        size = _figure.Y;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _figureValidator.CheckWalls(size, _vector, walls, _figure.Z);
            });

        public string Error { get; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}