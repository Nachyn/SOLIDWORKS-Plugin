using System;

namespace Drawer3D.Model.Exceptions
{
    public class FormException : Exception
    {
        public FormError FormError { get; set; }

        public FormException(string key, string message)
            : base(message)
        {
            FormError = new FormError {Key = key, Message = message};
        }

        public FormException(FormError formError)
            : base(formError.Message)
        {
            FormError = formError;
        }
    }
}