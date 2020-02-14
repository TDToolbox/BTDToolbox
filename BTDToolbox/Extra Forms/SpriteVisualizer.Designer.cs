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
            this.label1 = new System.Windows.Forms.Label();
            this.Visualizer_PictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Bg_White_Button = new System.Windows.Forms.Button();
            this.Bg_Black_Button = new System.Windows.Forms.Button();
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
            this.Visualizer_PictureBox.Location = new System.Drawing.Point(727, 65);
            this.Visualizer_PictureBox.Name = "Visualizer_PictureBox";
            this.Visualizer_PictureBox.Size = new System.Drawing.Size(540, 570);
            this.Visualizer_PictureBox.TabIndex = 1;
            this.Visualizer_PictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(933, 644);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Background color:";
            // 
            // Bg_White_Button
            // 
            this.Bg_White_Button.BackColor = System.Drawing.Color.White;
            this.Bg_White_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bg_White_Button.Location = new System.Drawing.Point(923, 667);
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
            this.Bg_Black_Button.Location = new System.Drawing.Point(1004, 667);
            this.Bg_Black_Button.Name = "Bg_Black_Button";
            this.Bg_Black_Button.Size = new System.Drawing.Size(75, 23);
            this.Bg_Black_Button.TabIndex = 4;
            this.Bg_Black_Button.UseVisualStyleBackColor = false;
            this.Bg_Black_Button.Click += new System.EventHandler(this.Bg_Black_Button_Click);
            // 
            // SpriteVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(1333, 727);
            this.Controls.Add(this.Bg_Black_Button);
            this.Controls.Add(this.Bg_White_Button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Visualizer_PictureBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.Name = "SpriteVisualizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpriteVisualizer";
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
    }
}