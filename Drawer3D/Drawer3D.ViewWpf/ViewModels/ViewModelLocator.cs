using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Drawer3D.ViewWpf.ViewModels
{
    /// <summary>
    ///     Этот класс содержит статические ссылки на все модели представлений
    ///     в приложении и предоставляет точку входа для привязок.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        ///      Конструктор
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<ApplicationVm>();
        }

        /// <summary>
        ///     Главная View-Model
        /// </summary>
        public ApplicationVm Application => ServiceLocator.Current.GetInstance<ApplicationVm>();
    }
}