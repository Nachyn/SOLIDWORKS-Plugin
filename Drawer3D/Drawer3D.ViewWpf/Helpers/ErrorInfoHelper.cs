using System;
using Drawer3D.Model.Exceptions;

namespace Drawer3D.ViewWpf.Helpers
{
    public static class ErrorInfoHelper
    {
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