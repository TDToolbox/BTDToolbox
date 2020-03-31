namespace BTDToolbox.Extra_Forms
{
    partial class EZ_Base
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
            this.Type_Label = new System.Windows.Forms.Label();
            this.Save_Button = new System.Windows.Forms.Button();
            this.Files_ComboBox = new System.Windows.Forms.ComboBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.OpenFile_Button = new System.Windows.Forms.Button();
            this.Game_Label = new System.Windows.Forms.Label();
            this.SwitchPanel_Button = new System.Windows.Forms.Button();
            this.GoToFile_Button = new System.Windows.Forms.Button();
            this.GoToFile_TB = new System.Windows.Forms.RichTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PrevCard_Button = new System.Windows.Forms.Button();
            this.NextCard_Button = new System.Windows.Forms.Button();
            this.OpenFile_Panel = new System.Windows.Forms.Panel();
            this.OpenText_Button = new System.Windows.Forms.Button();
            this.OpenNewEZBase_Button = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.OpenFile_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Type_Label
            // 
            this.Type_Label.AutoSize = true;
            this.Type_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Type_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Type_Label.ForeColor = System.Drawing.Color.White;
            this.Type_Label.Location = new System.Drawing.Point(14, 18);
            this.Type_Label.Name = "Type_Label";
            this.Type_Label.Size = new System.Drawing.Size(76, 35);
            this.Type_Label.TabIndex = 3;
            this.Type_Label.Text = "Type";
            // 
            // Save_Button
            // 
            this.Save_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Save_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.Save_Button.ForeColor = System.Drawing.Color.White;
            this.Save_Button.Location = new System.Drawing.Point(890, 484);
            this.Save_Button.Name = "Save_Button";
            this.Save_Button.Size = new System.Drawing.Size(164, 58);
            this.Save_Button.TabIndex = 47;
            this.Save_Button.Text = "Save";
            this.Save_Button.UseVisualStyleBackColor = false;
            this.Save_Button.Click += new System.EventHandler(this.Save_Button_Click);
            // 
            // Files_ComboBox
            // 
            this.Files_ComboBox.DropDownHeight = 500;
            this.Files_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Files_ComboBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Files_ComboBox.FormattingEnabled = true;
            this.Files_ComboBox.IntegralHeight = false;
            this.Files_ComboBox.ItemHeight = 19;
            this.Files_ComboBox.Location = new System.Drawing.Point(24, 56);
            this.Files_ComboBox.Name = "Files_ComboBox";
            this.Files_ComboBox.Size = new System.Drawing.Size(398, 27);
            this.Files_ComboBox.TabIndex = 48;
            this.Files_ComboBox.SelectedValueChanged += new System.EventHandler(this.Files_ComboBox_SelectedValueChanged);
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Panel1.Controls.Add(this.OpenFile_Button);
            this.Panel1.Controls.Add(this.Game_Label);
            this.Panel1.Location = new System.Drawing.Point(2, 50);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(1071, 413);
            this.Panel1.TabIndex = 49;
            // 
            // OpenFile_Button
            // 
            this.OpenFile_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.OpenFile_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenFile_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.OpenFile_Button.ForeColor = System.Drawing.Color.White;
            this.OpenFile_Button.Location = new System.Drawing.Point(426, 1);
            this.OpenFile_Button.Name = "OpenFile_Button";
            this.OpenFile_Button.Size = new System.Drawing.Size(141, 34);
            this.OpenFile_Button.TabIndex = 118;
            this.OpenFile_Button.Text = "Open";
            this.OpenFile_Button.UseVisualStyleBackColor = false;
            // 
            // Game_Label
            // 
            this.Game_Label.AutoSize = true;
            this.Game_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Game_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Game_Label.ForeColor = System.Drawing.Color.White;
            this.Game_Label.Location = new System.Drawing.Point(929, 4);
            this.Game_Label.Name = "Game_Label";
            this.Game_Label.Size = new System.Drawing.Size(90, 35);
            this.Game_Label.TabIndex = 117;
            this.Game_Label.Text = "Game";
            // 
            // SwitchPanel_Button
            // 
            this.SwitchPanel_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.SwitchPanel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SwitchPanel_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.SwitchPanel_Button.ForeColor = System.Drawing.Color.White;
            this.SwitchPanel_Button.Location = new System.Drawing.Point(720, 484);
            this.SwitchPanel_Button.Name = "SwitchPanel_Button";
            this.SwitchPanel_Button.Size = new System.Drawing.Size(164, 58);
            this.SwitchPanel_Button.TabIndex = 51;
            this.SwitchPanel_Button.Text = "Page 2";
            this.SwitchPanel_Button.UseVisualStyleBackColor = false;
            this.SwitchPanel_Button.Click += new System.EventHandler(this.SwitchPanel_Click);
            // 
            // GoToFile_Button
            // 
            this.GoToFile_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.GoToFile_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GoToFile_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.GoToFile_Button.ForeColor = System.Drawing.Color.White;
            this.GoToFile_Button.Location = new System.Drawing.Point(937, 12);
            this.GoToFile_Button.Name = "GoToFile_Button";
            this.GoToFile_Button.Size = new System.Drawing.Size(117, 32);
            this.GoToFile_Button.TabIndex = 119;
            this.GoToFile_Button.Text = "GoTo File";
            this.GoToFile_Button.UseVisualStyleBackColor = false;
            // 
            // GoToFile_TB
            // 
            this.GoToFile_TB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.GoToFile_TB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GoToFile_TB.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GoToFile_TB.ForeColor = System.Drawing.Color.White;
            this.GoToFile_TB.Location = new System.Drawing.Point(705, 17);
            this.GoToFile_TB.Multiline = false;
            this.GoToFile_TB.Name = "GoToFile_TB";
            this.GoToFile_TB.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.GoToFile_TB.Size = new System.Drawing.Size(226, 24);
            this.GoToFile_TB.TabIndex = 118;
            this.GoToFile_TB.Text = "";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pictureBox1.Location = new System.Drawing.Point(-41, -26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1225, 74);
            this.pictureBox1.TabIndex = 125;
            this.pictureBox1.TabStop = false;
            // 
            // PrevCard_Button
            // 
            this.PrevCard_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.PrevCard_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrevCard_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrevCard_Button.ForeColor = System.Drawing.Color.White;
            this.PrevCard_Button.Location = new System.Drawing.Point(306, 26);
            this.PrevCard_Button.Name = "PrevCard_Button";
            this.PrevCard_Button.Size = new System.Drawing.Size(55, 28);
            this.PrevCard_Button.TabIndex = 127;
            this.PrevCard_Button.Text = "<<";
            this.PrevCard_Button.UseVisualStyleBackColor = false;
            this.PrevCard_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PrevCard_Button_MouseDown);
            this.PrevCard_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PrevCard_Button_MouseUp);
            // 
            // NextCard_Button
            // 
            this.NextCard_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.NextCard_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextCard_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NextCard_Button.ForeColor = System.Drawing.Color.White;
            this.NextCard_Button.Location = new System.Drawing.Point(367, 26);
            this.NextCard_Button.Name = "NextCard_Button";
            this.NextCard_Button.Size = new System.Drawing.Size(55, 28);
            this.NextCard_Button.TabIndex = 126;
            this.NextCard_Button.Text = ">>";
            this.NextCard_Button.UseVisualStyleBackColor = false;
            this.NextCard_Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NextCard_Button_MouseDown);
            this.NextCard_Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NextCard_Button_MouseUp);
            // 
            // OpenFile_Panel
            // 
            this.OpenFile_Panel.Controls.Add(this.OpenText_Button);
            this.OpenFile_Panel.Controls.Add(this.OpenNewEZBase_Button);
            this.OpenFile_Panel.Location = new System.Drawing.Point(565, 37);
            this.OpenFile_Panel.Name = "OpenFile_Panel";
            this.OpenFile_Panel.Size = new System.Drawing.Size(168, 70);
            this.OpenFile_Panel.TabIndex = 119;
            this.OpenFile_Panel.Visible = false;
            // 
            // OpenText_Button
            // 
            this.OpenText_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.OpenText_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenText_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.OpenText_Button.ForeColor = System.Drawing.Color.White;
            this.OpenText_Button.Location = new System.Drawing.Point(3, 0);
            this.OpenText_Button.Name = "OpenText_Button";
            this.OpenText_Button.Size = new System.Drawing.Size(164, 34);
            this.OpenText_Button.TabIndex = 77;
            this.OpenText_Button.Text = "Open in Text";
            this.OpenText_Button.UseVisualStyleBackColor = false;
            this.OpenText_Button.Click += new System.EventHandler(this.OpenText_Button_Click);
            // 
            // OpenNewEZBase_Button
            // 
            this.OpenNewEZBase_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.OpenNewEZBase_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenNewEZBase_Button.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F);
            this.OpenNewEZBase_Button.ForeColor = System.Drawing.Color.White;
            this.OpenNewEZBase_Button.Location = new System.Drawing.Point(3, 35);
            this.OpenNewEZBase_Button.Name = "OpenNewEZBase_Button";
            this.OpenNewEZBase_Button.Size = new System.Drawing.Size(164, 35);
            this.OpenNewEZBase_Button.TabIndex = 76;
            this.OpenNewEZBase_Button.Text = "Open in EZ Base";
            this.OpenNewEZBase_Button.UseVisualStyleBackColor = false;
            this.OpenNewEZBase_Button.Click += new System.EventHandler(this.OpenNewEZBase_Button_Click);
            // 
            // EZ_Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1066, 554);
            this.Controls.Add(this.OpenFile_Panel);
            this.Controls.Add(this.PrevCard_Button);
            this.Controls.Add(this.NextCard_Button);
            this.Controls.Add(this.GoToFile_Button);
            this.Controls.Add(this.GoToFile_TB);
            this.Controls.Add(this.SwitchPanel_Button);
            this.Controls.Add(this.Files_ComboBox);
            this.Controls.Add(this.Save_Button);
            this.Controls.Add(this.Type_Label);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "EZ_Base";
            this.Text = "EZ Base";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EZ_Base_FormClosed);
            this.Shown += new System.EventHandler(this.EZ_Base_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EZ_Base_KeyDown);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.OpenFile_Panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Type_Label;
        private System.Windows.Forms.Button Save_Button;
        private System.Windows.Forms.ComboBox Files_ComboBox;
        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Button SwitchPanel_Button;
        private System.Windows.Forms.Label Game_Label;
        private System.Windows.Forms.Button GoToFile_Button;
        private System.Windows.Forms.RichTextBox GoToFile_TB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button PrevCard_Button;
        private System.Windows.Forms.Button NextCard_Button;
        private System.Windows.Forms.Button OpenFile_Button;
        private System.Windows.Forms.Panel OpenFile_Panel;
        private System.Windows.Forms.Button OpenText_Button;
        private System.Windows.Forms.Button OpenNewEZBase_Button;
    }
}