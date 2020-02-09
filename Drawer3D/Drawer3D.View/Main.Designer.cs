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
            this._buttonCreateNewPart = new System.Windows.Forms.Button();
            this._buttonBuildBase = new System.Windows.Forms.Button();
            this._buttonBuildWalls = new System.Windows.Forms.Button();
            this._groupBoxBase = new System.Windows.Forms.GroupBox();
            this._textBoxBaseZ = new System.Windows.Forms.TextBox();
            this._textBoxBaseY = new System.Windows.Forms.TextBox();
            this._textBoxBaseX = new System.Windows.Forms.TextBox();
            this._labelBaseZ = new System.Windows.Forms.Label();
            this._labelBaseY = new System.Windows.Forms.Label();
            this._labelBaseX = new System.Windows.Forms.Label();
            this._groupBoxWalls = new System.Windows.Forms.GroupBox();
            this._textBoxWallsY = new System.Windows.Forms.TextBox();
            this._textBoxWallsX = new System.Windows.Forms.TextBox();
            this._labelWallsY = new System.Windows.Forms.Label();
            this._labelWallsX = new System.Windows.Forms.Label();
            this._buttonSavePart = new System.Windows.Forms.Button();
            this._groupBoxPart = new System.Windows.Forms.GroupBox();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._groupBoxBase.SuspendLayout();
            this._groupBoxWalls.SuspendLayout();
            this._groupBoxPart.SuspendLayout();
            this.SuspendLayout();
            // 
            // _buttonCreateNewPart
            // 
            this._buttonCreateNewPart.Location = new System.Drawing.Point(6, 16);
            this._buttonCreateNewPart.Name = "_buttonCreateNewPart";
            this._buttonCreateNewPart.Size = new System.Drawing.Size(135, 23);
            this._buttonCreateNewPart.TabIndex = 0;
            this._buttonCreateNewPart.Text = "Создать";
            this._buttonCreateNewPart.UseVisualStyleBackColor = true;
            this._buttonCreateNewPart.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // _buttonBuildBase
            // 
            this._buttonBuildBase.Enabled = false;
            this._buttonBuildBase.Location = new System.Drawing.Point(9, 96);
            this._buttonBuildBase.Name = "_buttonBuildBase";
            this._buttonBuildBase.Size = new System.Drawing.Size(135, 23);
            this._buttonBuildBase.TabIndex = 1;
            this._buttonBuildBase.Text = "Построить";
            this._buttonBuildBase.UseVisualStyleBackColor = true;
            this._buttonBuildBase.Click += new System.EventHandler(this.ButtonBuildBase_Click);
            // 
            // _buttonBuildWalls
            // 
            this._buttonBuildWalls.Location = new System.Drawing.Point(9, 70);
            this._buttonBuildWalls.Name = "_buttonBuildWalls";
            this._buttonBuildWalls.Size = new System.Drawing.Size(301, 23);
            this._buttonBuildWalls.TabIndex = 2;
            this._buttonBuildWalls.Text = "Построить";
            this._buttonBuildWalls.UseVisualStyleBackColor = true;
            this._buttonBuildWalls.Click += new System.EventHandler(this.ButtonBuildWalls_Click);
            // 
            // _groupBoxBase
            // 
            this._groupBoxBase.Controls.Add(this._textBoxBaseZ);
            this._groupBoxBase.Controls.Add(this._textBoxBaseY);
            this._groupBoxBase.Controls.Add(this._buttonBuildBase);
            this._groupBoxBase.Controls.Add(this._textBoxBaseX);
            this._groupBoxBase.Controls.Add(this._labelBaseZ);
            this._groupBoxBase.Controls.Add(this._labelBaseY);
            this._groupBoxBase.Controls.Add(this._labelBaseX);
            this._groupBoxBase.Location = new System.Drawing.Point(12, 12);
            this._groupBoxBase.Name = "_groupBoxBase";
            this._groupBoxBase.Size = new System.Drawing.Size(163, 133);
            this._groupBoxBase.TabIndex = 3;
            this._groupBoxBase.TabStop = false;
            this._groupBoxBase.Text = "Основание";
            // 
            // _textBoxBaseZ
            // 
            this._textBoxBaseZ.Location = new System.Drawing.Point(71, 70);
            this._textBoxBaseZ.Name = "_textBoxBaseZ";
            this._textBoxBaseZ.Size = new System.Drawing.Size(73, 20);
            this._textBoxBaseZ.TabIndex = 5;
            this._textBoxBaseZ.TextChanged += new System.EventHandler(this.TextBoxesBase_TextChanged);
            // 
            // _textBoxBaseY
            // 
            this._textBoxBaseY.Location = new System.Drawing.Point(71, 44);
            this._textBoxBaseY.Name = "_textBoxBaseY";
            this._textBoxBaseY.Size = new System.Drawing.Size(73, 20);
            this._textBoxBaseY.TabIndex = 4;
            this._textBoxBaseY.TextChanged += new System.EventHandler(this.TextBoxesBase_TextChanged);
            // 
            // _textBoxBaseX
            // 
            this._textBoxBaseX.Location = new System.Drawing.Point(71, 18);
            this._textBoxBaseX.Name = "_textBoxBaseX";
            this._textBoxBaseX.Size = new System.Drawing.Size(73, 20);
            this._textBoxBaseX.TabIndex = 3;
            this._textBoxBaseX.TextChanged += new System.EventHandler(this.TextBoxesBase_TextChanged);
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
            this._groupBoxWalls.Controls.Add(this._textBoxWallsY);
            this._groupBoxWalls.Controls.Add(this._textBoxWallsX);
            this._groupBoxWalls.Controls.Add(this._buttonBuildWalls);
            this._groupBoxWalls.Controls.Add(this._labelWallsY);
            this._groupBoxWalls.Controls.Add(this._labelWallsX);
            this._groupBoxWalls.Location = new System.Drawing.Point(12, 151);
            this._groupBoxWalls.Name = "_groupBoxWalls";
            this._groupBoxWalls.Size = new System.Drawing.Size(323, 105);
            this._groupBoxWalls.TabIndex = 4;
            this._groupBoxWalls.TabStop = false;
            this._groupBoxWalls.Text = "Стены";
            // 
            // _textBoxWallsY
            // 
            this._textBoxWallsY.Location = new System.Drawing.Point(104, 44);
            this._textBoxWallsY.Name = "_textBoxWallsY";
            this._textBoxWallsY.Size = new System.Drawing.Size(206, 20);
            this._textBoxWallsY.TabIndex = 3;
            this._textBoxWallsY.TextChanged += new System.EventHandler(this.TextBoxesWalls_TextChanged);
            // 
            // _textBoxWallsX
            // 
            this._textBoxWallsX.Location = new System.Drawing.Point(104, 18);
            this._textBoxWallsX.Name = "_textBoxWallsX";
            this._textBoxWallsX.Size = new System.Drawing.Size(206, 20);
            this._textBoxWallsX.TabIndex = 2;
            this._textBoxWallsX.TextChanged += new System.EventHandler(this.TextBoxesWalls_TextChanged);
            // 
            // _labelWallsY
            // 
            this._labelWallsY.AutoSize = true;
            this._labelWallsY.Location = new System.Drawing.Point(6, 47);
            this._labelWallsY.Name = "_labelWallsY";
            this._labelWallsY.Size = new System.Drawing.Size(81, 13);
            this._labelWallsY.TabIndex = 1;
            this._labelWallsY.Text = "Вдоль ширины";
            // 
            // _labelWallsX
            // 
            this._labelWallsX.AutoSize = true;
            this._labelWallsX.Location = new System.Drawing.Point(6, 21);
            this._labelWallsX.Name = "_labelWallsX";
            this._labelWallsX.Size = new System.Drawing.Size(73, 13);
            this._labelWallsX.TabIndex = 0;
            this._labelWallsX.Text = "Вдоль длины";
            // 
            // _buttonSavePart
            // 
            this._buttonSavePart.Enabled = false;
            this._buttonSavePart.Location = new System.Drawing.Point(6, 45);
            this._buttonSavePart.Name = "_buttonSavePart";
            this._buttonSavePart.Size = new System.Drawing.Size(135, 23);
            this._buttonSavePart.TabIndex = 5;
            this._buttonSavePart.Text = "Сохранить";
            this._buttonSavePart.UseVisualStyleBackColor = true;
            this._buttonSavePart.Click += new System.EventHandler(this.ButtonSavePart_Click);
            // 
            // _groupBoxPart
            // 
            this._groupBoxPart.Controls.Add(this._buttonCreateNewPart);
            this._groupBoxPart.Controls.Add(this._buttonSavePart);
            this._groupBoxPart.Location = new System.Drawing.Point(181, 12);
            this._groupBoxPart.Name = "_groupBoxPart";
            this._groupBoxPart.Size = new System.Drawing.Size(154, 133);
            this._groupBoxPart.TabIndex = 6;
            this._groupBoxPart.TabStop = false;
            this._groupBoxPart.Text = "Проект детали";
            // 
            // _saveFileDialog
            // 
            this._saveFileDialog.Filter = "SLDPRT|*.SLDPRT";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 268);
            this.Controls.Add(this._groupBoxPart);
            this.Controls.Add(this._groupBoxWalls);
            this.Controls.Add(this._groupBoxBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
            this._groupBoxPart.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _buttonCreateNewPart;
        private System.Windows.Forms.Button _buttonBuildBase;
        private System.Windows.Forms.Button _buttonBuildWalls;
        private System.Windows.Forms.GroupBox _groupBoxBase;
        private System.Windows.Forms.Label _labelBaseZ;
        private System.Windows.Forms.Label _labelBaseY;
        private System.Windows.Forms.Label _labelBaseX;
        private System.Windows.Forms.TextBox _textBoxBaseZ;
        private System.Windows.Forms.TextBox _textBoxBaseY;
        private System.Windows.Forms.TextBox _textBoxBaseX;
        private System.Windows.Forms.GroupBox _groupBoxWalls;
        private System.Windows.Forms.Label _labelWallsX;
        private System.Windows.Forms.TextBox _textBoxWallsY;
        private System.Windows.Forms.TextBox _textBoxWallsX;
        private System.Windows.Forms.Label _labelWallsY;
        private System.Windows.Forms.Button _buttonSavePart;
        private System.Windows.Forms.GroupBox _groupBoxPart;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
    }
}