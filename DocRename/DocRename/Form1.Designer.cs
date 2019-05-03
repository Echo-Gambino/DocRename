namespace DocRename
{
    partial class docRenameForm
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
            this.input_textBox = new System.Windows.Forms.TextBox();
            this.input_button = new System.Windows.Forms.Button();
            this.version_label = new System.Windows.Forms.Label();
            this.project_label = new System.Windows.Forms.Label();
            this.name_label = new System.Windows.Forms.Label();
            this.date_label = new System.Windows.Forms.Label();
            this.output_button = new System.Windows.Forms.Button();
            this.preview_textBox = new System.Windows.Forms.TextBox();
            this.title_label = new System.Windows.Forms.Label();
            this.version_comboBox = new System.Windows.Forms.ComboBox();
            this.project_comboBox = new System.Windows.Forms.ComboBox();
            this.name_comboBox = new System.Windows.Forms.ComboBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.date_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.info_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // input_textBox
            // 
            this.input_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.input_textBox.Location = new System.Drawing.Point(12, 32);
            this.input_textBox.Name = "input_textBox";
            this.input_textBox.Size = new System.Drawing.Size(221, 22);
            this.input_textBox.TabIndex = 0;
            // 
            // input_button
            // 
            this.input_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.input_button.Location = new System.Drawing.Point(239, 31);
            this.input_button.Name = "input_button";
            this.input_button.Size = new System.Drawing.Size(92, 23);
            this.input_button.TabIndex = 1;
            this.input_button.Text = "Browse";
            this.input_button.UseVisualStyleBackColor = true;
            this.input_button.Click += new System.EventHandler(this.input_button_Click);
            // 
            // version_label
            // 
            this.version_label.AutoSize = true;
            this.version_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version_label.Location = new System.Drawing.Point(10, 115);
            this.version_label.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.version_label.MinimumSize = new System.Drawing.Size(60, 20);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(60, 20);
            this.version_label.TabIndex = 2;
            this.version_label.Text = "Version:";
            this.version_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // project_label
            // 
            this.project_label.AutoSize = true;
            this.project_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.project_label.Location = new System.Drawing.Point(10, 61);
            this.project_label.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.project_label.MinimumSize = new System.Drawing.Size(60, 20);
            this.project_label.Name = "project_label";
            this.project_label.Size = new System.Drawing.Size(60, 20);
            this.project_label.TabIndex = 3;
            this.project_label.Text = "Project:";
            this.project_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.name_label.Location = new System.Drawing.Point(10, 88);
            this.name_label.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.name_label.MinimumSize = new System.Drawing.Size(60, 20);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(60, 20);
            this.name_label.TabIndex = 4;
            this.name_label.Text = "Name:";
            this.name_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // date_label
            // 
            this.date_label.AutoSize = true;
            this.date_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_label.Location = new System.Drawing.Point(10, 142);
            this.date_label.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.date_label.MinimumSize = new System.Drawing.Size(60, 20);
            this.date_label.Name = "date_label";
            this.date_label.Size = new System.Drawing.Size(60, 20);
            this.date_label.TabIndex = 5;
            this.date_label.Text = "Date:";
            this.date_label.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // output_button
            // 
            this.output_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.output_button.Location = new System.Drawing.Point(239, 225);
            this.output_button.Name = "output_button";
            this.output_button.Size = new System.Drawing.Size(92, 23);
            this.output_button.TabIndex = 11;
            this.output_button.Text = "Rename";
            this.output_button.UseVisualStyleBackColor = true;
            this.output_button.Click += new System.EventHandler(this.output_button_Click);
            // 
            // preview_textBox
            // 
            this.preview_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preview_textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preview_textBox.Location = new System.Drawing.Point(12, 226);
            this.preview_textBox.Name = "preview_textBox";
            this.preview_textBox.Size = new System.Drawing.Size(221, 22);
            this.preview_textBox.TabIndex = 10;
            // 
            // title_label
            // 
            this.title_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.title_label.AutoSize = true;
            this.title_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title_label.Location = new System.Drawing.Point(116, 9);
            this.title_label.Name = "title_label";
            this.title_label.Size = new System.Drawing.Size(108, 20);
            this.title_label.TabIndex = 12;
            this.title_label.Text = "DocRename";
            this.title_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // version_comboBox
            // 
            this.version_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.version_comboBox.FormattingEnabled = true;
            this.version_comboBox.Location = new System.Drawing.Point(76, 114);
            this.version_comboBox.Name = "version_comboBox";
            this.version_comboBox.Size = new System.Drawing.Size(256, 21);
            this.version_comboBox.TabIndex = 13;
            this.version_comboBox.TextChanged += new System.EventHandler(this.version_comboBox_TextChanged);
            // 
            // project_comboBox
            // 
            this.project_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.project_comboBox.FormattingEnabled = true;
            this.project_comboBox.Location = new System.Drawing.Point(76, 60);
            this.project_comboBox.Name = "project_comboBox";
            this.project_comboBox.Size = new System.Drawing.Size(256, 21);
            this.project_comboBox.TabIndex = 14;
            this.project_comboBox.TextChanged += new System.EventHandler(this.project_comboBox_TextChanged);
            // 
            // name_comboBox
            // 
            this.name_comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.name_comboBox.FormattingEnabled = true;
            this.name_comboBox.Location = new System.Drawing.Point(76, 87);
            this.name_comboBox.Name = "name_comboBox";
            this.name_comboBox.Size = new System.Drawing.Size(256, 21);
            this.name_comboBox.TabIndex = 15;
            this.name_comboBox.TextChanged += new System.EventHandler(this.name_comboBox_TextChanged);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // date_dateTimePicker
            // 
            this.date_dateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.date_dateTimePicker.Location = new System.Drawing.Point(76, 142);
            this.date_dateTimePicker.Name = "date_dateTimePicker";
            this.date_dateTimePicker.Size = new System.Drawing.Size(257, 20);
            this.date_dateTimePicker.TabIndex = 17;
            this.date_dateTimePicker.ValueChanged += new System.EventHandler(this.date_dateTimePicker_ValueChanged);
            this.date_dateTimePicker.Enter += new System.EventHandler(this.date_dateTimePicker_ValueChanged);
            // 
            // info_label
            // 
            this.info_label.AutoSize = true;
            this.info_label.Location = new System.Drawing.Point(12, 171);
            this.info_label.Name = "info_label";
            this.info_label.Size = new System.Drawing.Size(52, 13);
            this.info_label.TabIndex = 18;
            this.info_label.Text = "info_label";
            // 
            // docRenameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 261);
            this.Controls.Add(this.info_label);
            this.Controls.Add(this.date_dateTimePicker);
            this.Controls.Add(this.name_comboBox);
            this.Controls.Add(this.project_comboBox);
            this.Controls.Add(this.version_comboBox);
            this.Controls.Add(this.title_label);
            this.Controls.Add(this.output_button);
            this.Controls.Add(this.preview_textBox);
            this.Controls.Add(this.date_label);
            this.Controls.Add(this.name_label);
            this.Controls.Add(this.project_label);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.input_button);
            this.Controls.Add(this.input_textBox);
            this.MaximumSize = new System.Drawing.Size(800, 300);
            this.MinimumSize = new System.Drawing.Size(360, 260);
            this.Name = "docRenameForm";
            this.Text = "DocRename";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input_textBox;
        private System.Windows.Forms.Button input_button;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.Label project_label;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Label date_label;
        private System.Windows.Forms.Button output_button;
        private System.Windows.Forms.TextBox preview_textBox;
        private System.Windows.Forms.Label title_label;
        private System.Windows.Forms.ComboBox version_comboBox;
        private System.Windows.Forms.ComboBox project_comboBox;
        private System.Windows.Forms.ComboBox name_comboBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DateTimePicker date_dateTimePicker;
        private System.Windows.Forms.Label info_label;
    }
}

