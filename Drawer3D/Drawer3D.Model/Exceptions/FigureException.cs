using System;

namespace Drawer3D.Model.Exceptions
{
    public class FigureException : Exception
    {
        public FigureError FigureError { get; set; }

        public FigureException(string key, string message)
            : base(message)
        {
            FigureError = new FigureError {Key = key, Message = message};
        }

        public FigureException(FigureError figureError)
            : base(figureError.Message)
        {
            FigureError = figureError;
        }
    }
}