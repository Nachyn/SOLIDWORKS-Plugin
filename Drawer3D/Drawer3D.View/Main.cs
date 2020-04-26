using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Drawer3D.Model;
using Drawer3D.Model.Enums;
using Drawer3D.Model.Exceptions;
using Drawer3D.View.Properties;
using Newtonsoft.Json;

namespace Drawer3D.View
{
    /// <summary>
    ///     Главная форма
    /// </summary>
    public partial class Main : Form
    {
        /// <summary>
        ///     Отступ между текстбоксами для стен
        /// </summary>
        private readonly int _marginWall = 26;

        /// <summary>
        ///     Относительный путь к JSON настройкам программы SOLIDWORKS
        /// </summary>
        private readonly string _solidWorksSettingsPath = "SolidWorksSettings.json";

        /// <summary>
        ///     Построитель-рисовальщик фигуры
        /// </summary>
        private Drawer _drawer;

        /// <summary>
        ///     Расположение последнего текстбокса для стен вдоль вектора X
        /// </summary>
        private Point _locationLastWallX;

        /// <summary>
        ///     Расположение последнего текстбокса для стен вдоль вектора Y
        /// </summary>
        private Point _locationLastWallY;

        /// <summary>
        ///     Размер текстбокса для стен
        /// </summary>
        private Size _sizeWall;

        /// <summary>
        ///     Текстбоксы для стен вдоль вектора X
        /// </summary>
        private List<TextBox> _wallsX;

        /// <summary>
        ///     Текстбоксы для стен вдоль вектора Y
        /// </summary>
        private List<TextBox> _wallsY;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public Main()
        {
            InitializeComponent();
            InitializeDrawer();
            InitializeWalls();
        }

        /// <summary>
        ///     Инициализировать текстбоксы для стены вдоль векторов
        /// </summary>
        private void InitializeWalls()
        {
            _sizeWall = _textBoxHeightWallsX.Size;
            _wallsX = new List<TextBox>();
            _locationLastWallX = _textBoxHeightWallsX.Location;
            _locationLastWallX.Y += _marginWall;
            _wallsY = new List<TextBox>();
            _locationLastWallY = _textBoxHeightWallsY.Location;
            _locationLastWallY.Y += _marginWall;
        }

        /// <summary>
        ///     Инициализировать построитель-рисовальщик фигуры
        /// </summary>
        private void InitializeDrawer()
        {
            _drawer = new Drawer(new FigureSettings(),
                new SolidWorksCommander(GetSolidWorksSettings()));
        }

        /// <summary>
        ///     Получить настройки для программы SOLIDWORKS
        /// </summary>
        /// <returns>Настройки для программы SOLIDWORKS</returns>
        private SolidWorksSettings GetSolidWorksSettings()
        {
            var jsonString = File.ReadAllText(_solidWorksSettingsPath);
            jsonString = jsonString.Replace("\"[", "[");
            jsonString = jsonString.Replace("]\"", "]");
            return JsonConvert.DeserializeObject<SolidWorksSettings>(jsonString);
        }

        private void TextBoxes_TextChanged(object sender, EventArgs e)
        {
            var textBoxes = new List<TextBox>
            {
                _textBoxBaseX,
                _textBoxBaseY,
                _textBoxBaseZ,
                _textBoxHeightWallsX,
                _textBoxHeightWallsY
            };

            textBoxes.AddRange(_wallsX);
            textBoxes.AddRange(_wallsY);

            _buttonBuild.Enabled = CheckTextBoxesOnInteger(textBoxes);

            if (sender is TextBox textBox)
            {
                textBox.Text = textBox.Text.Trim();
            }

            if (_buttonBuild.Enabled)
            {
                try
                {
                    _drawer.CheckFigure(GetFigure());
                }
                catch (FigureException exception)
                {
                    HandleFigureException(exception);
                }
            }
        }

        /// <summary>
        ///     Проверить текстбоксы на целочисленный тип
        /// </summary>
        /// <param name="textBoxes"></param>
        /// <returns>Все ли текстбоксы с целочисленным типом</returns>
        private bool CheckTextBoxesOnInteger(List<TextBox> textBoxes)
        {
            var isValid = true;

            _errorProvider.Clear();
            foreach (var textBox in textBoxes.Where(textBox =>
                !int.TryParse(textBox.Text, out _)))
            {
                if ((textBox == _textBoxHeightWallsX ||
                     textBox == _textBoxHeightWallsY)
                    && !textBox.Enabled)
                {
                    continue;
                }

                isValid = false;
                _errorProvider.SetError(textBox, "Не является числом.");
            }

            return isValid;
        }

        private void ButtonBuildWalls_Click(object sender, EventArgs e)
        {
            try
            {
                _drawer.BuildFigure(GetFigure());
            }
            catch (FigureException exception)
            {
                HandleFigureException(exception);
            }
            catch (ExternalException)
            {
                var info = Resources.ResourceManager.GetString("ProgramClosed");

                MessageBox.Show(info,
                    string.Empty,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        ///     Получить из формы пользовательские параметры для фигуры
        /// </summary>
        /// <returns>Пользовательские параметры для фигуры</returns>
        private Figure GetFigure()
        {
            Walls wallsX = null;
            if (_textBoxHeightWallsX.Enabled)
            {
                wallsX = new Walls
                {
                    Height = int.Parse(_textBoxHeightWallsX.Text),
                    Points = _wallsX.Select(tb => int.Parse(tb.Text)).ToList()
                };
            }

            Walls wallsY = null;
            if (_textBoxHeightWallsY.Enabled)
            {
                wallsY = new Walls
                {
                    Height = int.Parse(_textBoxHeightWallsY.Text),
                    Points = _wallsY.Select(tb => int.Parse(tb.Text)).ToList()
                };
            }

            return new Figure
            {
                X = int.Parse(_textBoxBaseX.Text),
                Y = int.Parse(_textBoxBaseY.Text),
                Z = int.Parse(_textBoxBaseZ.Text),
                WallsX = wallsX,
                WallsY = wallsY
            };
        }

        private void ButtonAddWallX_Click(object sender, EventArgs e)
        {
            AddWall(Vector.X);
            _textBoxHeightWallsX.Enabled = true;
            TextBoxes_TextChanged(null, null);
        }

        private void ButtonRemoveWallX_Click(object sender, EventArgs e)
        {
            RemoveWall(Vector.X);
            if (!_wallsX.Any())
            {
                _textBoxHeightWallsX.Enabled = false;
                _textBoxHeightWallsX.Text = string.Empty;
            }

            TextBoxes_TextChanged(null, null);
        }

        private void ButtonAddWallY_Click(object sender, EventArgs e)
        {
            AddWall(Vector.Y);
            _textBoxHeightWallsY.Enabled = true;
            TextBoxes_TextChanged(null, null);
        }

        private void ButtonRemoveWallY_Click(object sender, EventArgs e)
        {
            RemoveWall(Vector.Y);
            if (!_wallsY.Any())
            {
                _textBoxHeightWallsY.Enabled = false;
                _textBoxHeightWallsY.Text = string.Empty;
            }

            TextBoxes_TextChanged(null, null);
        }

        /// <summary>
        ///     Добавить текстбокс для стен
        /// </summary>
        /// <param name="vector">Вектор</param>
        private void AddWall(Vector vector)
        {
            Point location;
            List<TextBox> walls;
            switch (vector)
            {
                case Vector.X:
                    _locationLastWallX.Y += _marginWall;
                    location = _locationLastWallX;
                    walls = _wallsX;
                    break;

                case Vector.Y:
                    _locationLastWallY.Y += _marginWall;
                    location = _locationLastWallY;
                    walls = _wallsY;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(vector)
                        , vector, null);
            }

            var textBox = new TextBox {Location = location, Size = _sizeWall};

            textBox.TextChanged += TextBoxes_TextChanged;

            _groupBoxWalls.Controls.Add(textBox);
            walls.Add(textBox);
            MoveControls();
        }

        /// <summary>
        ///     Удалить текстбокс для стен
        /// </summary>
        /// <param name="vector">Вектор</param>
        private void RemoveWall(Vector vector)
        {
            var walls = vector switch
            {
                Vector.X => _wallsX,

                Vector.Y => _wallsY,

                _ => throw new ArgumentOutOfRangeException(nameof(vector), vector, null)
            };

            if (!walls.Any())
            {
                return;
            }

            using var removedTextBox = walls.Last();
            walls.Remove(removedTextBox);
            _groupBoxWalls.Controls.Remove(removedTextBox);

            switch (vector)
            {
                case Vector.X:
                    _locationLastWallX.Y -= _marginWall;
                    break;

                case Vector.Y:
                    _locationLastWallY.Y -= _marginWall;
                    break;
            }

            MoveControls();
        }

        /// <summary>
        ///     Обновить расположение элементов управления формы
        /// </summary>
        private void MoveControls()
        {
            var walls = new List<TextBox>();
            walls.AddRange(_wallsX);
            walls.AddRange(_wallsY);

            var maxWallLocationY = walls.Any() ? walls.Max(tb => tb.Location.Y) : 0;

            _groupBoxWalls.Size = !walls.Any()
                ? new Size(_groupBoxWalls.Size.Width, 100)
                : new Size(_groupBoxWalls.Size.Width,
                    maxWallLocationY + _marginWall);

            var fullGroupBoxHeight =
                _groupBoxWalls.Location.Y + _groupBoxWalls.Size.Height;

            Size = new Size(Size.Width, fullGroupBoxHeight + _marginWall * 3 - 5);

            _buttonBuild.Location = new Point(_buttonBuild.Location.X,
                fullGroupBoxHeight + 5);
        }

        /// <summary>
        ///     Обработать исключения типа FigureException
        /// </summary>
        /// <param name="exception">Исключение</param>
        private void HandleFigureException(FigureException exception)
        {
            _errorProvider.Clear();
            Control control;
            switch (exception.FigureError.Key)
            {
                case "sizeX":
                    control = _textBoxBaseX;
                    break;
                case "sizeY":
                    control = _textBoxBaseY;
                    break;
                case "sizeZ":
                    control = _textBoxBaseZ;
                    break;
                case "heightWallsX":
                    control = _textBoxHeightWallsX;
                    break;
                case "heightWallsY":
                    control = _textBoxHeightWallsY;
                    break;
                case {} key when key.StartsWith("wallX"):
                {
                    var number = int.Parse(key.Substring(5));
                    control = _wallsX[number - 1];
                    break;
                }
                case {} key when key.StartsWith("wallY"):
                {
                    var number = int.Parse(key.Substring(5));
                    control = _wallsY[number - 1];
                    break;
                }
                default:
                    MessageBox.Show(exception.FigureError.Message,
                        string.Empty,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    return;
            }

            _errorProvider.SetError(control, exception.FigureError.Message);
        }

        private void MenuProjectNew_Click(object sender, EventArgs e)
        {
            try
            {
                _drawer.ConnectToApp();
            }
            catch (ExternalException exception)
            {
                var info = Resources.ResourceManager.GetString("ProgramNotFound");

                MessageBox.Show(string.Format(info, exception.Message),
                    string.Empty,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            _menuProjectSaveAs.Enabled = true;
        }

        private void MenuProjectSaveAs_Click(object sender, EventArgs e)
        {
            var result = _saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _drawer.SaveToFile(_saveFileDialog.FileName);
            }
        }

        private void MenuParamsSetDefault_Click(object sender, EventArgs e)
        {
            MenuParamsClear_Click(null, null);

            _textBoxBaseX.Text = "200";
            _textBoxBaseY.Text = "400";
            _textBoxBaseZ.Text = "50";

            for (var i = 1; i <= 6; i++)
            {
                ButtonAddWallX_Click(null, null);
                _wallsX[i - 1].Text = $"{i * 25}";
                ButtonAddWallY_Click(null, null);
                _wallsY[i - 1].Text = $"{i * 25}";
            }

            _textBoxHeightWallsX.Text = "45";
            _textBoxHeightWallsY.Text = "45";
        }

        private void MenuParamsClear_Click(object sender, EventArgs e)
        {
            var countWallX = _wallsX.Count;
            for (var i = 0; i < countWallX; i++)
            {
                ButtonRemoveWallX_Click(null, null);
            }

            var countWallY = _wallsY.Count;
            for (var i = 0; i < countWallY; i++)
            {
                ButtonRemoveWallY_Click(null, null);
            }

            _textBoxBaseX.Text = _textBoxBaseY.Text = _textBoxBaseZ.Text = string.Empty;
        }
    }
}