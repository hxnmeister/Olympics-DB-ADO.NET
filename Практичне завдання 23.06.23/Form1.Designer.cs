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
            ((System.ComponentModel.ISupportInitialize)(this.OlympicsDataGridView)).BeginInit();
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
            this.ChangeButton.Location = new System.Drawing.Point(929, 332);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(75, 42);
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
            this.OptionsComboBox.Location = new System.Drawing.Point(834, 305);
            this.OptionsComboBox.Name = "OptionsComboBox";
            this.OptionsComboBox.Size = new System.Drawing.Size(170, 21);
            this.OptionsComboBox.TabIndex = 2;
            this.OptionsComboBox.Visible = false;
            this.OptionsComboBox.SelectedIndexChanged += new System.EventHandler(this.OptionsComboBox_SelectedIndexChanged);
            // 
            // EditorModeButton
            // 
            this.EditorModeButton.Location = new System.Drawing.Point(848, 396);
            this.EditorModeButton.Name = "EditorModeButton";
            this.EditorModeButton.Size = new System.Drawing.Size(75, 42);
            this.EditorModeButton.TabIndex = 3;
            this.EditorModeButton.Text = "Enter Editor Mode";
            this.EditorModeButton.UseVisualStyleBackColor = true;
            this.EditorModeButton.Click += new System.EventHandler(this.EditorModeButton_Click);
            // 
            // EditorModeExitButton
            // 
            this.EditorModeExitButton.Enabled = false;
            this.EditorModeExitButton.Location = new System.Drawing.Point(929, 396);
            this.EditorModeExitButton.Name = "EditorModeExitButton";
            this.EditorModeExitButton.Size = new System.Drawing.Size(75, 42);
            this.EditorModeExitButton.TabIndex = 4;
            this.EditorModeExitButton.Text = "Exit Editor Mode";
            this.EditorModeExitButton.UseVisualStyleBackColor = true;
            this.EditorModeExitButton.Click += new System.EventHandler(this.EditorModeExitButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 450);
            this.Controls.Add(this.EditorModeExitButton);
            this.Controls.Add(this.EditorModeButton);
            this.Controls.Add(this.OptionsComboBox);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.OlympicsDataGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.OlympicsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView OlympicsDataGridView;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.ComboBox OptionsComboBox;
        private System.Windows.Forms.Button EditorModeButton;
        private System.Windows.Forms.Button EditorModeExitButton;
    }
}

