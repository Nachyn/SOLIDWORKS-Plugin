using System;

namespace Drawer3D.Model.Exceptions
{
    public class FormSizeException : Exception
    {
        public FormSizeException(string message)
            : base(message)
        {
        }
    }
}