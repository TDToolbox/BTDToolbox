namespace BTDToolbox.Extra_Forms
{
    partial class SpriteVisualizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteVisualizer));
            this.label1 = new System.Windows.Forms.Label();
            this.Visualizer_PictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Bg_White_Button = new System.Windows.Forms.Button();
            this.Bg_Black_Button = new System.Windows.Forms.Button();
            this.Folder_ComboBox = new System.Windows.Forms.ComboBox();
            this.File_ComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Sprites_ListBox = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Reload_Button = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Visualizer_PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sprite Visualizer";
            // 
            // Visualizer_PictureBox
            // 
            this.Visualizer_PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Visualizer_PictureBox.BackColor = System.Drawing.Color.White;
            this.Visualizer_PictureBox.Location = new System.Drawing.Point(887, 65);
            this.Visualizer_PictureBox.Name = "Visualizer_PictureBox";
            this.Visualizer_PictureBox.Size = new System.Drawing.Size(380, 376);
            this.Visualizer_PictureBox.TabIndex = 1;
            this.Visualizer_PictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1010, 440);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Background color:";
            // 
            // Bg_White_Button
            // 
            this.Bg_White_Button.BackColor = System.Drawing.Color.White;
            this.Bg_White_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bg_White_Button.Location = new System.Drawing.Point(1000, 463);
            this.Bg_White_Button.Name = "Bg_White_Button";
            this.Bg_White_Button.Size = new System.Drawing.Size(75, 23);
            this.Bg_White_Button.TabIndex = 3;
            this.Bg_White_Button.UseVisualStyleBackColor = false;
            this.Bg_White_Button.Click += new System.EventHandler(this.Bg_White_Button_Click);
            // 
            // Bg_Black_Button
            // 
            this.Bg_Black_Button.BackColor = System.Drawing.Color.Black;
            this.Bg_Black_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bg_Black_Button.ForeColor = System.Drawing.Color.Black;
            this.Bg_Black_Button.Location = new System.Drawing.Point(1081, 463);
            this.Bg_Black_Button.Name = "Bg_Black_Button";
            this.Bg_Black_Button.Size = new System.Drawing.Size(75, 23);
            this.Bg_Black_Button.TabIndex = 4;
            this.Bg_Black_Button.UseVisualStyleBackColor = false;
            this.Bg_Black_Button.Click += new System.EventHandler(this.Bg_Black_Button_Click);
            // 
            // Folder_ComboBox
            // 
            this.Folder_ComboBox.FormattingEnabled = true;
            this.Folder_ComboBox.Location = new System.Drawing.Point(20, 102);
            this.Folder_ComboBox.Name = "Folder_ComboBox";
            this.Folder_ComboBox.Size = new System.Drawing.Size(376, 29);
            this.Folder_ComboBox.TabIndex = 5;
            this.Folder_ComboBox.SelectedValueChanged += new System.EventHandler(this.Folder_ComboBox_SelectedValueChanged);
            // 
            // File_ComboBox
            // 
            this.File_ComboBox.FormattingEnabled = true;
            this.File_ComboBox.Location = new System.Drawing.Point(20, 155);
            this.File_ComboBox.Name = "File_ComboBox";
            this.File_ComboBox.Size = new System.Drawing.Size(376, 29);
            this.File_ComboBox.TabIndex = 6;
            this.File_ComboBox.SelectedValueChanged += new System.EventHandler(this.File_ComboBox_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Folder:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "File:";
            // 
            // Sprites_ListBox
            // 
            this.Sprites_ListBox.FormattingEnabled = true;
            this.Sprites_ListBox.ItemHeight = 21;
            this.Sprites_ListBox.Location = new System.Drawing.Point(20, 241);
            this.Sprites_ListBox.Name = "Sprites_ListBox";
            this.Sprites_ListBox.Size = new System.Drawing.Size(376, 319);
            this.Sprites_ListBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "Sprites";
            // 
            // Reload_Button
            // 
            this.Reload_Button.ForeColor = System.Drawing.Color.Black;
            this.Reload_Button.Location = new System.Drawing.Point(1166, 672);
            this.Reload_Button.Name = "Reload_Button";
            this.Reload_Button.Size = new System.Drawing.Size(155, 43);
            this.Reload_Button.TabIndex = 11;
            this.Reload_Button.Text = "Reload";
            this.Reload_Button.UseVisualStyleBackColor = true;
            this.Reload_Button.Click += new System.EventHandler(this.Reload_Button_Click);
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(290, 217);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 26);
            this.button1.TabIndex = 12;
            this.button1.Text = "Add New";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // SpriteVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1333, 727);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Reload_Button);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Sprites_ListBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.File_ComboBox);
            this.Controls.Add(this.Folder_ComboBox);
            this.Controls.Add(this.Bg_Black_Button);
            this.Controls.Add(this.Bg_White_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Visualizer_PictureBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.Name = "SpriteVisualizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sprite Visualizer";
            this.Load += new System.EventHandler(this.SpriteVisualizer_Load);
            this.Shown += new System.EventHandler(this.SpriteVisualizer_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Visualizer_PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Visualizer_PictureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Bg_White_Button;
        private System.Windows.Forms.Button Bg_Black_Button;
        private System.Windows.Forms.ComboBox Folder_ComboBox;
        private System.Windows.Forms.ComboBox File_ComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox Sprites_ListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Reload_Button;
        private System.Windows.Forms.Button button1;
    }
}