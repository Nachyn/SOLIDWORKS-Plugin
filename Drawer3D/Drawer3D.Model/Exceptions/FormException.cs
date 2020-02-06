using System;

namespace Drawer3D.Model.Exceptions
{
    public class FormException : Exception
    {
        public FormException(string message)
            : base(message)
        {
        }
    }
}