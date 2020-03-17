using System.Windows;
using System.Windows.Threading;
using Drawer3D.Model.Exceptions;

namespace Drawer3D.ViewWpf
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender
            , DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is FigureException figureException)
            {
                MessageBox.Show(figureException.FigureError.Message
                    , string.Empty
                    , MessageBoxButton.OK
                    , MessageBoxImage.Exclamation);

                e.Handled = true;
            }
        }
    }
}