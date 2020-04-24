using System;
using System.ComponentModel;
using Drawer3D.Model;
using Drawer3D.Model.Enums;
using Drawer3D.ViewWpf.Helpers;
using GalaSoft.MvvmLight;

namespace Drawer3D.ViewWpf.ViewModels
{
    /// <summary>
    ///     View-Model точки-стены вдоль вектора
    /// </summary>
    public class PointVm : ObservableObject, IDataErrorInfo
    {
        /// <summary>
        ///     Пользовательские параметры фигуры
        /// </summary>
        private readonly Figure _figure;

        /// <summary>
        ///     Валидатор настроек фигуры
        /// </summary>
        private readonly FigureValidator _figureValidator;

        /// <summary>
        ///     Индекс в коллекции
        /// </summary>
        private readonly int _index;

        /// <summary>
        ///     Текущий вектор
        /// </summary>
        private readonly Vector _vector;

        /// <summary>
        ///     Значение
        /// </summary>
        private int _value;

        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="figure">Пользовательские параметры фигуры</param>
        /// <param name="figureValidator">Валидатор настроек фигуры</param>
        /// <param name="vector">Текущий вектор</param>
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

        /// <summary>
        ///     Получает и задает значение
        /// </summary>
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

                Set(() => Value, ref _value, value);
            }
        }

        /// <summary>
        ///     Валидировать текущее свойство
        /// </summary>
        /// <param name="columnName">Текущее свойство</param>
        /// <returns>Строка с ошибой, пустая строка - ошибок нет</returns>
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

        /// <summary>
        ///     Текущая ошибка валидации
        /// </summary>
        public string Error => throw new NotImplementedException();
    }
}