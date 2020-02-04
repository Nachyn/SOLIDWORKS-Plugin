using System;

namespace Drawer3D.Model
{
    public class FormSizeException : Exception
    {
        public FormSizeException(string message)
            : base(message)
        {
        }
    }
}