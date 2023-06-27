namespace Практичне_завдання_23._06._23
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.OlympicsDataGridView = new System.Windows.Forms.DataGridView();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.OptionsComboBox = new System.Windows.Forms.ComboBox();
            this.EditorModeButton = new System.Windows.Forms.Button();
            this.EditorModeExitButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RequestsComboBox = new System.Windows.Forms.ComboBox();
            this.UserDataTextBox = new System.Windows.Forms.TextBox();
            this.UserData1TextBox = new System.Windows.Forms.TextBox();
            this.ExecuteRequestButton = new System.Windows.Forms.Button();
            this.RequestsGroupBox = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.OlympicsDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.RequestsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OlympicsDataGridView
            // 
            this.OlympicsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OlympicsDataGridView.Location = new System.Drawing.Point(12, 12);
            this.OlympicsDataGridView.MultiSelect = false;
            this.OlympicsDataGridView.Name = "OlympicsDataGridView";
            this.OlympicsDataGridView.ReadOnly = true;
            this.OlympicsDataGridView.Size = new System.Drawing.Size(992, 287);
            this.OlympicsDataGridView.TabIndex = 0;
            // 
            // ChangeButton
            // 
            this.ChangeButton.Enabled = false;
            this.ChangeButton.Location = new System.Drawing.Point(6, 47);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(75, 37);
            this.ChangeButton.TabIndex = 1;
            this.ChangeButton.Text = "Change Information";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Visible = false;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // OptionsComboBox
            // 
            this.OptionsComboBox.AllowDrop = true;
            this.OptionsComboBox.FormattingEnabled = true;
            this.OptionsComboBox.Items.AddRange(new object[] {
            "Athletes",
            "Kind of Sports",
            "Olympics Info",
            "Kind of Sports and Athletes",
            "Olympics Overall Information"});
            this.OptionsComboBox.Location = new System.Drawing.Point(6, 19);
            this.OptionsComboBox.Name = "OptionsComboBox";
            this.OptionsComboBox.Size = new System.Drawing.Size(156, 21);
            this.OptionsComboBox.TabIndex = 2;
            this.OptionsComboBox.Visible = false;
            this.OptionsComboBox.SelectedIndexChanged += new System.EventHandler(this.OptionsComboBox_SelectedIndexChanged);
            // 
            // EditorModeButton
            // 
            this.EditorModeButton.Location = new System.Drawing.Point(6, 90);
            this.EditorModeButton.Name = "EditorModeButton";
            this.EditorModeButton.Size = new System.Drawing.Size(75, 37);
            this.EditorModeButton.TabIndex = 3;
            this.EditorModeButton.Text = "Enter Editor Mode";
            this.EditorModeButton.UseVisualStyleBackColor = true;
            this.EditorModeButton.Click += new System.EventHandler(this.EditorModeButton_Click);
            // 
            // EditorModeExitButton
            // 
            this.EditorModeExitButton.Enabled = false;
            this.EditorModeExitButton.Location = new System.Drawing.Point(87, 90);
            this.EditorModeExitButton.Name = "EditorModeExitButton";
            this.EditorModeExitButton.Size = new System.Drawing.Size(75, 37);
            this.EditorModeExitButton.TabIndex = 4;
            this.EditorModeExitButton.Text = "Exit Editor Mode";
            this.EditorModeExitButton.UseVisualStyleBackColor = true;
            this.EditorModeExitButton.Click += new System.EventHandler(this.EditorModeExitButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.OptionsComboBox);
            this.groupBox1.Controls.Add(this.EditorModeExitButton);
            this.groupBox1.Controls.Add(this.ChangeButton);
            this.groupBox1.Controls.Add(this.EditorModeButton);
            this.groupBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.groupBox1.Location = new System.Drawing.Point(835, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 133);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Editor Mode";
            // 
            // RequestsComboBox
            // 
            this.RequestsComboBox.FormattingEnabled = true;
            this.RequestsComboBox.Items.AddRange(new object[] {
            "Table of medal standings by country, for the entire history of the Olympiads",
            "Medalists in various sports, throughout the history of the Olympics",
            "The country with the most gold medals in the history of the Olympics",
            "The name of the country that most often hosted the Olympics",
            "Medal table by country, specific Olympiad",
            "Medalists in various sports, a specific Olympics",
            "Country with the most gold medals, specific Olympiad",
            "The athlete who has won the most gold medals in a particular sport",
            "The composition of the Olympic team of athletes, a specific country",
            "Statistics of the performance of a particular country in the history of the Olymp" +
                "iads",
            "Statistics of the performance of a particular country at the Olympiad"});
            this.RequestsComboBox.Location = new System.Drawing.Point(6, 19);
            this.RequestsComboBox.Name = "RequestsComboBox";
            this.RequestsComboBox.Size = new System.Drawing.Size(428, 21);
            this.RequestsComboBox.TabIndex = 6;
            this.RequestsComboBox.Text = "Choose Request!";
            this.RequestsComboBox.SelectedIndexChanged += new System.EventHandler(this.RequestsComboBox_SelectedIndexChanged);
            // 
            // UserDataTextBox
            // 
            this.UserDataTextBox.Enabled = false;
            this.UserDataTextBox.Location = new System.Drawing.Point(83, 56);
            this.UserDataTextBox.Name = "UserDataTextBox";
            this.UserDataTextBox.Size = new System.Drawing.Size(169, 20);
            this.UserDataTextBox.TabIndex = 7;
            this.UserDataTextBox.Enter += new System.EventHandler(this.UserDataTextBox_Enter);
            // 
            // UserData1TextBox
            // 
            this.UserData1TextBox.Enabled = false;
            this.UserData1TextBox.Location = new System.Drawing.Point(83, 82);
            this.UserData1TextBox.Name = "UserData1TextBox";
            this.UserData1TextBox.Size = new System.Drawing.Size(169, 20);
            this.UserData1TextBox.TabIndex = 8;
            this.UserData1TextBox.Enter += new System.EventHandler(this.UserData1TextBox_Enter);
            // 
            // ExecuteRequestButton
            // 
            this.ExecuteRequestButton.Enabled = false;
            this.ExecuteRequestButton.Location = new System.Drawing.Point(258, 56);
            this.ExecuteRequestButton.Name = "ExecuteRequestButton";
            this.ExecuteRequestButton.Size = new System.Drawing.Size(75, 46);
            this.ExecuteRequestButton.TabIndex = 5;
            this.ExecuteRequestButton.Text = "Execute Request";
            this.ExecuteRequestButton.UseVisualStyleBackColor = true;
            this.ExecuteRequestButton.Click += new System.EventHandler(this.ExecuteRequestButton_Click);
            // 
            // RequestsGroupBox
            // 
            this.RequestsGroupBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.RequestsGroupBox.Controls.Add(this.RequestsComboBox);
            this.RequestsGroupBox.Controls.Add(this.ExecuteRequestButton);
            this.RequestsGroupBox.Controls.Add(this.UserDataTextBox);
            this.RequestsGroupBox.Controls.Add(this.UserData1TextBox);
            this.RequestsGroupBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RequestsGroupBox.Location = new System.Drawing.Point(12, 305);
            this.RequestsGroupBox.Name = "RequestsGroupBox";
            this.RequestsGroupBox.Size = new System.Drawing.Size(440, 133);
            this.RequestsGroupBox.TabIndex = 9;
            this.RequestsGroupBox.TabStop = false;
            this.RequestsGroupBox.Text = "Requests to Data Base";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 450);
            this.Controls.Add(this.RequestsGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OlympicsDataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Olympics";
            ((System.ComponentModel.ISupportInitialize)(this.OlympicsDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.RequestsGroupBox.ResumeLayout(false);
            this.RequestsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView OlympicsDataGridView;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.ComboBox OptionsComboBox;
        private System.Windows.Forms.Button EditorModeButton;
        private System.Windows.Forms.Button EditorModeExitButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox RequestsComboBox;
        private System.Windows.Forms.TextBox UserDataTextBox;
        private System.Windows.Forms.TextBox UserData1TextBox;
        private System.Windows.Forms.Button ExecuteRequestButton;
        private System.Windows.Forms.GroupBox RequestsGroupBox;
    }
}

