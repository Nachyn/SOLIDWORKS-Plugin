using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Drawer3D.ViewWpf
{
    /// <summary>
    ///     Логика взаимодействия с MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Связующее выражения для точек-стен вдоль вектора X
        /// </summary>
        private readonly List<BindingExpression> _bindingPointsX;

        /// <summary>
        ///     Связующее выражения для точек-стен вдоль вектора Y
        /// </summary>
        private readonly List<BindingExpression> _bindingPointsY;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _bindingPointsX = new List<BindingExpression>();
            _bindingPointsY = new List<BindingExpression>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetTextBinding(sender).UpdateSource();
        }

        private void TextBoxPointX_Loaded(object sender, RoutedEventArgs e)
        {
            _bindingPointsX.Add(GetTextBinding(sender));
        }

        private void TextBoxPointY_Loaded(object sender, RoutedEventArgs e)
        {
            _bindingPointsY.Add(GetTextBinding(sender));
        }

        private void TextBoxPointX_Unloaded(object sender, RoutedEventArgs e)
        {
            _bindingPointsX.Remove(GetTextBinding(sender));
        }

        private void TextBoxPointY_Unloaded(object sender, RoutedEventArgs e)
        {
            _bindingPointsY.Remove(GetTextBinding(sender));
        }

        private void TextBoxPointX_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_TextChanged(sender, e);
            _bindingPointsX.ForEach(b => b.UpdateSource());
        }

        private void TextBoxPointY_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_TextChanged(sender, e);
            _bindingPointsY.ForEach(b => b.UpdateSource());
        }

        /// <summary>
        ///     Получить связующее выражение
        /// </summary>
        /// <param name="sender">Отправитель</param>
        /// <returns>Связующее выражение</returns>
        private static BindingExpression GetTextBinding(object sender)
        {
            return ((TextBox) sender).GetBindingExpression(TextBox.TextProperty);
        }
    }
}