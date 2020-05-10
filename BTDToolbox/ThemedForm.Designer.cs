namespace BTDToolbox
{
    partial class ThemedForm
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
        public void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemedForm));
            this.titleSeperator = new System.Windows.Forms.SplitContainer();
            this.close_button = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.TitleBarCorner = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Sizer = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBarCorner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // titleSeperator
            // 
            this.titleSeperator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.titleSeperator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleSeperator.IsSplitterFixed = true;
            this.titleSeperator.Location = new System.Drawing.Point(0, 0);
            this.titleSeperator.Name = "titleSeperator";
            this.titleSeperator.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // titleSeperator.Panel1
            // 
            this.titleSeperator.Panel1.BackColor = System.Drawing.Color.Black;
            this.titleSeperator.Panel1.BackgroundImage = global::BTDToolbox.Properties.Resources.newTitle_2;
            this.titleSeperator.Panel1.Controls.Add(this.close_button);
            this.titleSeperator.Panel1.Controls.Add(this.pictureBox1);
            this.titleSeperator.Panel1.Controls.Add(this.TitleLabel);
            this.titleSeperator.Panel1.Controls.Add(this.TitleBarCorner);
            // 
            // titleSeperator.Panel2
            // 
            this.titleSeperator.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.titleSeperator.Panel2.Controls.Add(this.Sizer);
            this.titleSeperator.Panel2.Controls.Add(this.contentPanel);
            this.titleSeperator.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.titleSeperator.Size = new System.Drawing.Size(800, 450);
            this.titleSeperator.SplitterDistance = 25;
            this.titleSeperator.TabIndex = 0;
            // 
            // close_button
            // 
            this.close_button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.close_button.BackColor = System.Drawing.Color.Transparent;
            this.close_button.BackgroundImage = global::BTDToolbox.Properties.Resources.new_close_button_2;
            this.close_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.close_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_button.ForeColor = System.Drawing.Color.White;
            this.close_button.Location = new System.Drawing.Point(736, 0);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(52, 26);
            this.close_button.TabIndex = 2;
            this.close_button.Text = "X";
            this.close_button.UseVisualStyleBackColor = false;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            this.close_button.MouseCaptureChanged += new System.EventHandler(this.close_button_MouseCaptureChanged);
            this.close_button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.close_button_MouseDown);
            this.close_button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.close_button_MouseUp);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.ForeColor = System.Drawing.Color.White;
            this.TitleLabel.Location = new System.Drawing.Point(12, 5);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(34, 16);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Title";
            // 
            // TitleBarCorner
            // 
            this.TitleBarCorner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.TitleBarCorner.Image = global::BTDToolbox.Properties.Resources.new_title_corner_8_left;
            this.TitleBarCorner.Location = new System.Drawing.Point(0, 0);
            this.TitleBarCorner.Name = "TitleBarCorner";
            this.TitleBarCorner.Size = new System.Drawing.Size(31, 59);
            this.TitleBarCorner.TabIndex = 0;
            this.TitleBarCorner.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::BTDToolbox.Properties.Resources.new_title_corner_8_right;
            this.pictureBox1.Location = new System.Drawing.Point(740, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 228);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // Sizer
            // 
            this.Sizer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Sizer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Sizer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Sizer.BackgroundImage")));
            this.Sizer.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.Sizer.Location = new System.Drawing.Point(788, 409);
            this.Sizer.Name = "Sizer";
            this.Sizer.Size = new System.Drawing.Size(12, 12);
            this.Sizer.TabIndex = 1;
            // 
            // contentPanel
            // 
            this.contentPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.contentPanel.Location = new System.Drawing.Point(12, 3);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(776, 406);
            this.contentPanel.TabIndex = 0;
            // 
            // ThemedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.titleSeperator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ThemedForm";
            this.Text = "ThemedFormTemplate";
            this.Shown += new System.EventHandler(this.ThemedForm_Shown);
            this.Resize += new System.EventHandler(this.ThemedForm_Resize);
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TitleBarCorner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.SplitContainer titleSeperator;
        public System.Windows.Forms.Label TitleLabel;
        public System.Windows.Forms.Panel contentPanel;
        public System.Windows.Forms.Button close_button;
        public System.Windows.Forms.Panel Sizer;
        private System.Windows.Forms.PictureBox TitleBarCorner;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}