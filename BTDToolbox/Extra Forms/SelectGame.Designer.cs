namespace BTDToolbox.Extra_Forms
{
    partial class SelectGame
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
            this.SelectGame_Label = new System.Windows.Forms.Label();
            this.BTD5_Button = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BTDB_Button = new System.Windows.Forms.Button();
            this.BMC_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectGame_Label
            // 
            this.SelectGame_Label.AutoSize = true;
            this.SelectGame_Label.Location = new System.Drawing.Point(31, 9);
            this.SelectGame_Label.Name = "SelectGame_Label";
            this.SelectGame_Label.Size = new System.Drawing.Size(207, 18);
            this.SelectGame_Label.TabIndex = 0;
            this.SelectGame_Label.Text = "What Game is Your Mod For?";
            // 
            // BTD5_Button
            // 
            this.BTD5_Button.ForeColor = System.Drawing.Color.Black;
            this.BTD5_Button.Location = new System.Drawing.Point(17, 52);
            this.BTD5_Button.Name = "BTD5_Button";
            this.BTD5_Button.Size = new System.Drawing.Size(75, 25);
            this.BTD5_Button.TabIndex = 1;
            this.BTD5_Button.Text = "BTD5";
            this.BTD5_Button.UseVisualStyleBackColor = true;
            this.BTD5_Button.Click += new System.EventHandler(this.BTD5_Button_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.pictureBox1.Location = new System.Drawing.Point(-10, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(349, 120);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // BTDB_Button
            // 
            this.BTDB_Button.ForeColor = System.Drawing.Color.Black;
            this.BTDB_Button.Location = new System.Drawing.Point(98, 52);
            this.BTDB_Button.Name = "BTDB_Button";
            this.BTDB_Button.Size = new System.Drawing.Size(75, 25);
            this.BTDB_Button.TabIndex = 3;
            this.BTDB_Button.Text = "BTDB";
            this.BTDB_Button.UseVisualStyleBackColor = true;
            this.BTDB_Button.Click += new System.EventHandler(this.BTDB_Button_Click);
            // 
            // BMC_Button
            // 
            this.BMC_Button.ForeColor = System.Drawing.Color.Black;
            this.BMC_Button.Location = new System.Drawing.Point(179, 52);
            this.BMC_Button.Name = "BMC_Button";
            this.BMC_Button.Size = new System.Drawing.Size(75, 25);
            this.BMC_Button.TabIndex = 4;
            this.BMC_Button.Text = "BMC";
            this.BMC_Button.UseVisualStyleBackColor = true;
            this.BMC_Button.Click += new System.EventHandler(this.BMC_Button_Click);
            // 
            // SelectGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(267, 89);
            this.Controls.Add(this.BMC_Button);
            this.Controls.Add(this.BTDB_Button);
            this.Controls.Add(this.BTD5_Button);
            this.Controls.Add(this.SelectGame_Label);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "SelectGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectGame";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SelectGame_Label;
        private System.Windows.Forms.Button BTD5_Button;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BTDB_Button;
        private System.Windows.Forms.Button BMC_Button;
    }
}