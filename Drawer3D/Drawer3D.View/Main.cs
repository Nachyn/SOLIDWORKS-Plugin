using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Drawer3D.Model;
using Drawer3D.Model.Exceptions;
using Newtonsoft.Json;
using Timer = System.Threading.Timer;

namespace Drawer3D.View
{
    public partial class Main : Form
    {
        private readonly int _delayedTextChangedTimer = 500;

        private Timer _timerTextBoxesWalls;


        private readonly string _drawerAppSettingsPath = "DrawerAppSettings.json";

        private Drawer _drawer;


        private int _x;

        private int _y;

        private int _z;

        public Main()
        {
            InitializeComponent();
            InitializeDrawer();
        }

        private void InitializeDrawer()
        {
            _drawer = new Drawer(GetDrawerAppSettings());
        }

        private DrawerAppSettings GetDrawerAppSettings()
        {
            var settingsText = File.ReadAllText(_drawerAppSettingsPath);
            return JsonConvert.DeserializeObject<DrawerAppSettings>(settingsText);
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            _drawer.ConnectToApp();
            _buttonSavePart.Enabled = true;
        }

        private void ButtonBuildBase_Click(object sender, EventArgs e)
        {
            try
            {
                _drawer.BuildBase(_x, _y, _z);
            }
            catch (FormException exception)
            {
                HandleFormException(exception);
            }
        }

        private bool IsValidBaseData()
        {
            if (!int.TryParse(_textBoxBaseX.Text, out var x) ||
                !int.TryParse(_textBoxBaseY.Text, out var y) ||
                !int.TryParse(_textBoxBaseZ.Text, out var z))
            {
                return false;
            }

            _x = x;
            _y = y;
            _z = z;

            return true;
        }

        private void TextBoxesBase_TextChanged(object sender, EventArgs e)
        {
            _buttonBuildBase.Enabled = IsValidBaseData();
            if (sender is TextBox textBox)
            {
                textBox.Text = textBox.Text.Trim();
            }
        }


        private void ButtonBuildWalls_Click(object sender, EventArgs e)
        {
            var pointsX = _textBoxWallsX.Text.Split(' ')
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse).ToList();

            var pointsY = _textBoxWallsY.Text.Split(' ')
                .Where(s => int.TryParse(s, out _))
                .Select(int.Parse).ToList();

            try
            {
                _drawer.BuildGrid(pointsX, pointsY);
            }
            catch (FormException exception)
            {
                HandleFormException(exception);
            }
        }

        private void TextBoxesWalls_TextChanged(object sender, EventArgs e)
        {
            _timerTextBoxesWalls?.Dispose();
            _timerTextBoxesWalls = new Timer(sender =>
            {
                if (sender is TextBox textBox)
                {
                    textBox.Invoke(new Action(() =>
                    {
                        textBox.Text = string.Join(" ",
                            textBox.Text.Split(' ')
                                .Where(s => int.TryParse(s, out _)));

                        textBox.SelectionStart = textBox.Text.Length;
                    }));
                }

                _timerTextBoxesWalls?.Dispose();
                _timerTextBoxesWalls = null;
            }, sender, _delayedTextChangedTimer, int.MaxValue);
        }

        private void HandleFormException(FormException exception)
        {
            MessageBox.Show(exception.Message,
                string.Empty,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ButtonSavePart_Click(object sender, EventArgs e)
        {
            var result = _saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _drawer.SaveToFile(_saveFileDialog.FileName);
            }
        }
    }
}