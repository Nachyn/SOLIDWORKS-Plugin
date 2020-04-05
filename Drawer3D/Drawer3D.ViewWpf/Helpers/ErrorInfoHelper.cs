using System;
using Drawer3D.Model.Exceptions;

namespace Drawer3D.ViewWpf.Helpers
{
    /// <summary>
    ///     Класс помощник для ошибок
    /// </summary>
    public static class ErrorInfoHelper
    {
        /// <summary>
        ///     Обработать исключение FigureException и вернуть строку с ошибкой
        /// </summary>
        /// <param name="action">Действие</param>
        /// <returns>Строка с ошибой, пустая строка - ошибок нет</returns>
        public static string HandleErrorInfo(Action action)
        {
            try
            {
                action();
                return string.Empty;
            }
            catch (FigureException exception)
            {
                return exception.FigureError.Message;
            }
        }
    }
}