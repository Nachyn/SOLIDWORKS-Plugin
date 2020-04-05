using System;

namespace Drawer3D.Model.Exceptions
{
    /// <summary>
    ///     Исключение проектируемой фигуры
    /// </summary>
    public class FigureException : Exception
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        /// <param name="key">Тип ошибки</param>
        /// <param name="message">Описание ошибки</param>
        public FigureException(string key
            , string message) : base(message)
        {
            FigureError = new FigureError {Key = key, Message = message};
        }

        /// <summary>
        ///     Информация об исключении
        /// </summary>
        public FigureError FigureError { get; set; }
    }
}