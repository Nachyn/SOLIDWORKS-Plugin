using System;
using System.Windows.Forms;

namespace Drawer3D.View
{
    /// <summary>
    ///     Класс, содержащий точку входа
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///     Главная точка входа для приложения
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}