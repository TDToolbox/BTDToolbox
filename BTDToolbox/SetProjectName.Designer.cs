namespace BTDToolbox
{
    partial class SetProjectName
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
            this.ProjectName_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomName_RadioButton = new System.Windows.Forms.RadioButton();
            this.RandomName_RadioButton = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.CreateProject_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CustomLocation_Button = new System.Windows.Forms.Button();
            this.UseNKH_CB = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ProjectName_TextBox
            // 
            this.ProjectName_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectName_TextBox.Location = new System.Drawing.Point(152, 59);
            this.ProjectName_TextBox.MaxLength = 30;
            this.ProjectName_TextBox.Name = "ProjectName_TextBox";
            this.ProjectName_TextBox.Size = new System.Drawing.Size(239, 21);
            this.ProjectName_TextBox.TabIndex = 0;
            this.ProjectName_TextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(176, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Please enter a project name";
            // 
            // CustomName_RadioButton
            // 
            this.CustomName_RadioButton.AutoSize = true;
            this.CustomName_RadioButton.Checked = true;
            this.CustomName_RadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomName_RadioButton.ForeColor = System.Drawing.Color.White;
            this.CustomName_RadioButton.Location = new System.Drawing.Point(12, 26);
            this.CustomName_RadioButton.Name = "CustomName_RadioButton";
            this.CustomName_RadioButton.Size = new System.Drawing.Size(104, 19);
            this.CustomName_RadioButton.TabIndex = 5;
            this.CustomName_RadioButton.TabStop = true;
            this.CustomName_RadioButton.Text = "Custom Name";
            this.CustomName_RadioButton.UseVisualStyleBackColor = true;
            this.CustomName_RadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CustomName_RadioButton_MouseClick);
            // 
            // RandomName_RadioButton
            // 
            this.RandomName_RadioButton.AutoSize = true;
            this.RandomName_RadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RandomName_RadioButton.ForeColor = System.Drawing.Color.White;
            this.RandomName_RadioButton.Location = new System.Drawing.Point(12, 51);
            this.RandomName_RadioButton.Name = "RandomName_RadioButton";
            this.RandomName_RadioButton.Size = new System.Drawing.Size(110, 19);
            this.RandomName_RadioButton.TabIndex = 6;
            this.RandomName_RadioButton.Text = "Random Name";
            this.RandomName_RadioButton.UseVisualStyleBackColor = true;
            this.RandomName_RadioButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RandomName_RadioButton_MouseClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pictureBox1.Location = new System.Drawing.Point(140, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(288, 253);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // CreateProject_Button
            // 
            this.CreateProject_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateProject_Button.Location = new System.Drawing.Point(201, 106);
            this.CreateProject_Button.Name = "CreateProject_Button";
            this.CreateProject_Button.Size = new System.Drawing.Size(133, 23);
            this.CreateProject_Button.TabIndex = 4;
            this.CreateProject_Button.Text = "Create Project";
            this.CreateProject_Button.UseVisualStyleBackColor = true;
            this.CreateProject_Button.Click += new System.EventHandler(this.CreateProject_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(9, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Optional:";
            // 
            // CustomLocation_Button
            // 
            this.CustomLocation_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomLocation_Button.Location = new System.Drawing.Point(12, 144);
            this.CustomLocation_Button.Name = "CustomLocation_Button";
            this.CustomLocation_Button.Size = new System.Drawing.Size(110, 23);
            this.CustomLocation_Button.TabIndex = 10;
            this.CustomLocation_Button.Text = "Custom location";
            this.CustomLocation_Button.UseVisualStyleBackColor = true;
            this.CustomLocation_Button.Click += new System.EventHandler(this.CustomLocation_Button_Click);
            // 
            // UseNKH_CB
            // 
            this.UseNKH_CB.AutoSize = true;
            this.UseNKH_CB.ForeColor = System.Drawing.Color.White;
            this.UseNKH_CB.Location = new System.Drawing.Point(12, 121);
            this.UseNKH_CB.Name = "UseNKH_CB";
            this.UseNKH_CB.Size = new System.Drawing.Size(89, 17);
            this.UseNKH_CB.TabIndex = 11;
            this.UseNKH_CB.Text = "Use NKHook";
            this.UseNKH_CB.UseVisualStyleBackColor = true;
            this.UseNKH_CB.CheckedChanged += new System.EventHandler(this.UseNKH_CB_CheckedChanged);
            // 
            // SetProjectName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(405, 179);
            this.Controls.Add(this.UseNKH_CB);
            this.Controls.Add(this.CustomLocation_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RandomName_RadioButton);
            this.Controls.Add(this.CustomName_RadioButton);
            this.Controls.Add(this.CreateProject_Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProjectName_TextBox);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetProjectName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Name";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetProjectName_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ProjectName_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton CustomName_RadioButton;
        private System.Windows.Forms.RadioButton RandomName_RadioButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button CreateProject_Button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CustomLocation_Button;
        private System.Windows.Forms.CheckBox UseNKH_CB;
    }
}