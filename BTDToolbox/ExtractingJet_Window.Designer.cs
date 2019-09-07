namespace BTDToolbox
{
    partial class ExtractingJet_Window
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.CurrentFileProgress = new System.Windows.Forms.ProgressBar();
            this.TotalProgress_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.CurrentFileProgress_Label = new System.Windows.Forms.Label();
            this.TotalProgress_Label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Current File:";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(12, 173);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(472, 43);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // CurrentFileProgress
            // 
            this.CurrentFileProgress.Location = new System.Drawing.Point(31, 39);
            this.CurrentFileProgress.Name = "CurrentFileProgress";
            this.CurrentFileProgress.Size = new System.Drawing.Size(435, 27);
            this.CurrentFileProgress.TabIndex = 0;
            // 
            // TotalProgress_ProgressBar
            // 
            this.TotalProgress_ProgressBar.Location = new System.Drawing.Point(31, 95);
            this.TotalProgress_ProgressBar.Name = "TotalProgress_ProgressBar";
            this.TotalProgress_ProgressBar.Size = new System.Drawing.Size(435, 27);
            this.TotalProgress_ProgressBar.TabIndex = 0;
            // 
            // CurrentFileProgress_Label
            // 
            this.CurrentFileProgress_Label.AutoSize = true;
            this.CurrentFileProgress_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.CurrentFileProgress_Label.ForeColor = System.Drawing.Color.White;
            this.CurrentFileProgress_Label.Location = new System.Drawing.Point(28, 21);
            this.CurrentFileProgress_Label.Name = "CurrentFileProgress_Label";
            this.CurrentFileProgress_Label.Size = new System.Drawing.Size(125, 15);
            this.CurrentFileProgress_Label.TabIndex = 3;
            this.CurrentFileProgress_Label.Text = "Current File Progress:";
            // 
            // TotalProgress_Label
            // 
            this.TotalProgress_Label.AutoSize = true;
            this.TotalProgress_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.TotalProgress_Label.ForeColor = System.Drawing.Color.White;
            this.TotalProgress_Label.Location = new System.Drawing.Point(28, 77);
            this.TotalProgress_Label.Name = "TotalProgress_Label";
            this.TotalProgress_Label.Size = new System.Drawing.Size(86, 15);
            this.TotalProgress_Label.TabIndex = 3;
            this.TotalProgress_Label.Text = "Total Progress";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pictureBox1.Location = new System.Drawing.Point(-3, 139);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(503, 118);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // ExtractingJet_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(496, 228);
            this.Controls.Add(this.TotalProgress_Label);
            this.Controls.Add(this.CurrentFileProgress_Label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.TotalProgress_ProgressBar);
            this.Controls.Add(this.CurrentFileProgress);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ExtractingJet_Window";
            this.Load += new System.EventHandler(this.ExtractJet_Window_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ProgressBar CurrentFileProgress;
        private System.Windows.Forms.ProgressBar TotalProgress_ProgressBar;
        private System.Windows.Forms.Label CurrentFileProgress_Label;
        private System.Windows.Forms.Label TotalProgress_Label;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}