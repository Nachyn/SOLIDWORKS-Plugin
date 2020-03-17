using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Drawer3D.ViewWpf
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<BindingExpression> _bindingPointsX;

        private readonly List<BindingExpression> _bindingPointsY;

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

        private static BindingExpression GetTextBinding(object sender)
        {
            return ((TextBox) sender).GetBindingExpression(TextBox.TextProperty);
        }
    }
}