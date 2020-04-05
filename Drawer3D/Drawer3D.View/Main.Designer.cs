namespace Drawer3D.View
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._buttonBuild = new System.Windows.Forms.Button();
            this._groupBoxBase = new System.Windows.Forms.GroupBox();
            this._textBoxBaseZ = new System.Windows.Forms.TextBox();
            this._textBoxBaseY = new System.Windows.Forms.TextBox();
            this._textBoxBaseX = new System.Windows.Forms.TextBox();
            this._labelBaseZ = new System.Windows.Forms.Label();
            this._labelBaseY = new System.Windows.Forms.Label();
            this._labelBaseX = new System.Windows.Forms.Label();
            this._groupBoxWalls = new System.Windows.Forms.GroupBox();
            this._labelWalls = new System.Windows.Forms.Label();
            this._buttonAddWallY = new System.Windows.Forms.Button();
            this._buttonRemoveWallY = new System.Windows.Forms.Button();
            this._textBoxHeightWallsY = new System.Windows.Forms.TextBox();
            this._labelHeightWalls = new System.Windows.Forms.Label();
            this._textBoxHeightWallsX = new System.Windows.Forms.TextBox();
            this._buttonAddWallX = new System.Windows.Forms.Button();
            this._buttonRemoveWallX = new System.Windows.Forms.Button();
            this._labelWallsY = new System.Windows.Forms.Label();
            this._labelWallsX = new System.Windows.Forms.Label();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this._menu = new System.Windows.Forms.ToolStripMenuItem();
            this._menuProjectNew = new System.Windows.Forms.ToolStripMenuItem();
            this._menuProjectSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this._menuParams = new System.Windows.Forms.ToolStripMenuItem();
            this._menuParamsClear = new System.Windows.Forms.ToolStripMenuItem();
            this._menuParamsSetDefault = new System.Windows.Forms.ToolStripMenuItem();
            this._errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._groupBoxBase.SuspendLayout();
            this._groupBoxWalls.SuspendLayout();
            this._menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // _buttonBuild
            // 
            this._buttonBuild.Enabled = false;
            this._buttonBuild.Location = new System.Drawing.Point(166, 245);
            this._buttonBuild.Name = "_buttonBuild";
            this._buttonBuild.Size = new System.Drawing.Size(73, 23);
            this._buttonBuild.TabIndex = 2;
            this._buttonBuild.Text = "Построить";
            this._buttonBuild.UseVisualStyleBackColor = true;
            this._buttonBuild.Click += new System.EventHandler(this.ButtonBuildWalls_Click);
            // 
            // _groupBoxBase
            // 
            this._groupBoxBase.Controls.Add(this._textBoxBaseZ);
            this._groupBoxBase.Controls.Add(this._textBoxBaseY);
            this._groupBoxBase.Controls.Add(this._textBoxBaseX);
            this._groupBoxBase.Controls.Add(this._labelBaseZ);
            this._groupBoxBase.Controls.Add(this._labelBaseY);
            this._groupBoxBase.Controls.Add(this._labelBaseX);
            this._groupBoxBase.Location = new System.Drawing.Point(12, 27);
            this._groupBoxBase.Name = "_groupBoxBase";
            this._groupBoxBase.Size = new System.Drawing.Size(247, 106);
            this._groupBoxBase.TabIndex = 3;
            this._groupBoxBase.TabStop = false;
            this._groupBoxBase.Text = "Основание ( мм )";
            // 
            // _textBoxBaseZ
            // 
            this._textBoxBaseZ.Location = new System.Drawing.Point(58, 70);
            this._textBoxBaseZ.Name = "_textBoxBaseZ";
            this._textBoxBaseZ.Size = new System.Drawing.Size(73, 20);
            this._textBoxBaseZ.TabIndex = 5;
            this._textBoxBaseZ.TextChanged += new System.EventHandler(this.TextBoxes_TextChanged);
            // 
            // _textBoxBaseY
            // 
            this._textBoxBaseY.Location = new System.Drawing.Point(58, 44);
            this._textBoxBaseY.Name = "_textBoxBaseY";
            this._textBoxBaseY.Size = new System.Drawing.Size(73, 20);
            this._textBoxBaseY.TabIndex = 4;
            this._textBoxBaseY.TextChanged += new System.EventHandler(this.TextBoxes_TextChanged);
            // 
            // _textBoxBaseX
            // 
            this._textBoxBaseX.Location = new System.Drawing.Point(58, 18);
            this._textBoxBaseX.Name = "_textBoxBaseX";
            this._textBoxBaseX.Size = new System.Drawing.Size(73, 20);
            this._textBoxBaseX.TabIndex = 3;
            this._textBoxBaseX.TextChanged += new System.EventHandler(this.TextBoxes_TextChanged);
            // 
            // _labelBaseZ
            // 
            this._labelBaseZ.AutoSize = true;
            this._labelBaseZ.Location = new System.Drawing.Point(6, 73);
            this._labelBaseZ.Name = "_labelBaseZ";
            this._labelBaseZ.Size = new System.Drawing.Size(45, 13);
            this._labelBaseZ.TabIndex = 2;
            this._labelBaseZ.Text = "Высота";
            // 
            // _labelBaseY
            // 
            this._labelBaseY.AutoSize = true;
            this._labelBaseY.Location = new System.Drawing.Point(6, 47);
            this._labelBaseY.Name = "_labelBaseY";
            this._labelBaseY.Size = new System.Drawing.Size(46, 13);
            this._labelBaseY.TabIndex = 1;
            this._labelBaseY.Text = "Ширина";
            // 
            // _labelBaseX
            // 
            this._labelBaseX.AutoSize = true;
            this._labelBaseX.Location = new System.Drawing.Point(6, 21);
            this._labelBaseX.Name = "_labelBaseX";
            this._labelBaseX.Size = new System.Drawing.Size(40, 13);
            this._labelBaseX.TabIndex = 0;
            this._labelBaseX.Text = "Длина";
            // 
            // _groupBoxWalls
            // 
            this._groupBoxWalls.Controls.Add(this._labelWalls);
            this._groupBoxWalls.Controls.Add(this._buttonAddWallY);
            this._groupBoxWalls.Controls.Add(this._buttonRemoveWallY);
            this._groupBoxWalls.Controls.Add(this._textBoxHeightWallsY);
            this._groupBoxWalls.Controls.Add(this._labelHeightWalls);
            this._groupBoxWalls.Controls.Add(this._textBoxHeightWallsX);
            this._groupBoxWalls.Controls.Add(this._buttonAddWallX);
            this._groupBoxWalls.Controls.Add(this._buttonRemoveWallX);
            this._groupBoxWalls.Controls.Add(this._labelWallsY);
            this._groupBoxWalls.Controls.Add(this._labelWallsX);
            this._groupBoxWalls.Location = new System.Drawing.Point(12, 139);
            this._groupBoxWalls.Name = "_groupBoxWalls";
            this._groupBoxWalls.Size = new System.Drawing.Size(247, 100);
            this._groupBoxWalls.TabIndex = 4;
            this._groupBoxWalls.TabStop = false;
            this._groupBoxWalls.Text = "Сетка ( мм )";
            // 
            // _labelWalls
            // 
            this._labelWalls.AutoSize = true;
            this._labelWalls.Location = new System.Drawing.Point(6, 67);
            this._labelWalls.Name = "_labelWalls";
            this._labelWalls.Size = new System.Drawing.Size(39, 13);
            this._labelWalls.TabIndex = 12;
            this._labelWalls.Text = "Стены";
            // 
            // _buttonAddWallY
            // 
            this._buttonAddWallY.Location = new System.Drawing.Point(154, 63);
            this._buttonAddWallY.Name = "_buttonAddWallY";
            this._buttonAddWallY.Size = new System.Drawing.Size(35, 23);
            this._buttonAddWallY.TabIndex = 10;
            this._buttonAddWallY.Text = "+";
            this._buttonAddWallY.UseVisualStyleBackColor = true;
            this._buttonAddWallY.Click += new System.EventHandler(this.ButtonAddWallY_Click);
            // 
            // _buttonRemoveWallY
            // 
            this._buttonRemoveWallY.Location = new System.Drawing.Point(192, 63);
            this._buttonRemoveWallY.Name = "_buttonRemoveWallY";
            this._buttonRemoveWallY.Size = new System.Drawing.Size(35, 23);
            this._buttonRemoveWallY.TabIndex = 9;
            this._buttonRemoveWallY.Text = "-";
            this._buttonRemoveWallY.UseVisualStyleBackColor = true;
            this._buttonRemoveWallY.Click += new System.EventHandler(this.ButtonRemoveWallY_Click);
            // 
            // _textBoxHeightWallsY
            // 
            this._textBoxHeightWallsY.Enabled = false;
            this._textBoxHeightWallsY.Location = new System.Drawing.Point(154, 37);
            this._textBoxHeightWallsY.Name = "_textBoxHeightWallsY";
            this._textBoxHeightWallsY.Size = new System.Drawing.Size(73, 20);
            this._textBoxHeightWallsY.TabIndex = 7;
            this._textBoxHeightWallsY.TextChanged += new System.EventHandler(this.TextBoxes_TextChanged);
            // 
            // _labelHeightWalls
            // 
            this._labelHeightWalls.AutoSize = true;
            this._labelHeightWalls.Location = new System.Drawing.Point(6, 40);
            this._labelHeightWalls.Name = "_labelHeightWalls";
            this._labelHeightWalls.Size = new System.Drawing.Size(45, 13);
            this._labelHeightWalls.TabIndex = 6;
            this._labelHeightWalls.Text = "Высота";
            // 
            // _textBoxHeightWallsX
            // 
            this._textBoxHeightWallsX.Enabled = false;
            this._textBoxHeightWallsX.Location = new System.Drawing.Point(58, 37);
            this._textBoxHeightWallsX.Name = "_textBoxHeightWallsX";
            this._textBoxHeightWallsX.Size = new System.Drawing.Size(73, 20);
            this._textBoxHeightWallsX.TabIndex = 5;
            this._textBoxHeightWallsX.TextChanged += new System.EventHandler(this.TextBoxes_TextChanged);
            // 
            // _buttonAddWallX
            // 
            this._buttonAddWallX.Location = new System.Drawing.Point(58, 63);
            this._buttonAddWallX.Name = "_buttonAddWallX";
            this._buttonAddWallX.Size = new System.Drawing.Size(35, 23);
            this._buttonAddWallX.TabIndex = 4;
            this._buttonAddWallX.Text = "+";
            this._buttonAddWallX.UseVisualStyleBackColor = true;
            this._buttonAddWallX.Click += new System.EventHandler(this.ButtonAddWallX_Click);
            // 
            // _buttonRemoveWallX
            // 
            this._buttonRemoveWallX.Location = new System.Drawing.Point(96, 63);
            this._buttonRemoveWallX.Name = "_buttonRemoveWallX";
            this._buttonRemoveWallX.Size = new System.Drawing.Size(35, 23);
            this._buttonRemoveWallX.TabIndex = 3;
            this._buttonRemoveWallX.Text = "-";
            this._buttonRemoveWallX.UseVisualStyleBackColor = true;
            this._buttonRemoveWallX.Click += new System.EventHandler(this.ButtonRemoveWallX_Click);
            // 
            // _labelWallsY
            // 
            this._labelWallsY.AutoSize = true;
            this._labelWallsY.Location = new System.Drawing.Point(151, 16);
            this._labelWallsY.Name = "_labelWallsY";
            this._labelWallsY.Size = new System.Drawing.Size(81, 13);
            this._labelWallsY.TabIndex = 1;
            this._labelWallsY.Text = "Вдоль ширины";
            // 
            // _labelWallsX
            // 
            this._labelWallsX.AutoSize = true;
            this._labelWallsX.Location = new System.Drawing.Point(55, 16);
            this._labelWallsX.Name = "_labelWallsX";
            this._labelWallsX.Size = new System.Drawing.Size(73, 13);
            this._labelWallsX.TabIndex = 0;
            this._labelWallsX.Text = "Вдоль длины";
            // 
            // _saveFileDialog
            // 
            this._saveFileDialog.Filter = "SLDPRT|*.SLDPRT";
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menu,
            this._menuParams});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(271, 24);
            this._menuStrip.TabIndex = 7;
            this._menuStrip.Text = "menuStrip1";
            // 
            // _menu
            // 
            this._menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuProjectNew,
            this._menuProjectSaveAs});
            this._menu.Name = "_menu";
            this._menu.Size = new System.Drawing.Size(59, 20);
            this._menu.Text = "Проект";
            // 
            // _menuProjectNew
            // 
            this._menuProjectNew.Name = "_menuProjectNew";
            this._menuProjectNew.Size = new System.Drawing.Size(163, 22);
            this._menuProjectNew.Text = "Новый";
            this._menuProjectNew.Click += new System.EventHandler(this.MenuProjectNew_Click);
            // 
            // _menuProjectSaveAs
            // 
            this._menuProjectSaveAs.Enabled = false;
            this._menuProjectSaveAs.Name = "_menuProjectSaveAs";
            this._menuProjectSaveAs.Size = new System.Drawing.Size(163, 22);
            this._menuProjectSaveAs.Text = "Сохранить как...";
            this._menuProjectSaveAs.Click += new System.EventHandler(this.MenuProjectSaveAs_Click);
            // 
            // _menuParams
            // 
            this._menuParams.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuParamsClear,
            this._menuParamsSetDefault});
            this._menuParams.Name = "_menuParams";
            this._menuParams.Size = new System.Drawing.Size(83, 20);
            this._menuParams.Text = "Параметры";
            // 
            // _menuParamsClear
            // 
            this._menuParamsClear.Name = "_menuParamsClear";
            this._menuParamsClear.Size = new System.Drawing.Size(196, 22);
            this._menuParamsClear.Text = "Очистить";
            this._menuParamsClear.Click += new System.EventHandler(this.MenuParamsClear_Click);
            // 
            // _menuParamsSetDefault
            // 
            this._menuParamsSetDefault.Name = "_menuParamsSetDefault";
            this._menuParamsSetDefault.Size = new System.Drawing.Size(196, 22);
            this._menuParamsSetDefault.Text = "Задать по умолчанию";
            this._menuParamsSetDefault.Click += new System.EventHandler(this.MenuParamsSetDefault_Click);
            // 
            // _errorProvider
            // 
            this._errorProvider.BlinkRate = 0;
            this._errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this._errorProvider.ContainerControl = this;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 274);
            this.Controls.Add(this._groupBoxWalls);
            this.Controls.Add(this._groupBoxBase);
            this.Controls.Add(this._buttonBuild);
            this.Controls.Add(this._menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this._menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Drawer3D";
            this.TopMost = true;
            this._groupBoxBase.ResumeLayout(false);
            this._groupBoxBase.PerformLayout();
            this._groupBoxWalls.ResumeLayout(false);
            this._groupBoxWalls.PerformLayout();
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button _buttonBuild;
        private System.Windows.Forms.GroupBox _groupBoxBase;
        private System.Windows.Forms.Label _labelBaseZ;
        private System.Windows.Forms.Label _labelBaseY;
        private System.Windows.Forms.Label _labelBaseX;
        private System.Windows.Forms.TextBox _textBoxBaseZ;
        private System.Windows.Forms.TextBox _textBoxBaseY;
        private System.Windows.Forms.TextBox _textBoxBaseX;
        private System.Windows.Forms.GroupBox _groupBoxWalls;
        private System.Windows.Forms.Label _labelWallsX;
        private System.Windows.Forms.Label _labelWallsY;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
        private System.Windows.Forms.Button _buttonAddWallX;
        private System.Windows.Forms.Button _buttonRemoveWallX;
        private System.Windows.Forms.Label _labelHeightWalls;
        private System.Windows.Forms.TextBox _textBoxHeightWallsX;
        private System.Windows.Forms.TextBox _textBoxHeightWallsY;
        private System.Windows.Forms.Button _buttonAddWallY;
        private System.Windows.Forms.Button _buttonRemoveWallY;
        private System.Windows.Forms.Label _labelWalls;
        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.ToolStripMenuItem _menu;
        private System.Windows.Forms.ToolStripMenuItem _menuProjectNew;
        private System.Windows.Forms.ToolStripMenuItem _menuProjectSaveAs;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.ToolStripMenuItem _menuParams;
        private System.Windows.Forms.ToolStripMenuItem _menuParamsClear;
        private System.Windows.Forms.ToolStripMenuItem _menuParamsSetDefault;
    }
}