namespace BTDToolbox
{
    partial class CreditViewer : ThemedForm
    {
        public new void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_RightCorner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_LeftCorner)).BeginInit();
            this.SuspendLayout();
            // 
            // titleSeperator
            // 
            this.titleSeperator.Size = new System.Drawing.Size(401, 642);
            this.titleSeperator.SplitterDistance = 35;
            // 
            // TitleLabel
            // 
            this.TitleLabel.Size = new System.Drawing.Size(50, 16);
            this.TitleLabel.Text = "Credits";
            // 
            // contentPanel
            // 
            this.contentPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.contentPanel.Location = new System.Drawing.Point(3, 3);
            this.contentPanel.Size = new System.Drawing.Size(395, 597);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.close_button.Location = new System.Drawing.Point(337, 6);
            // 
            // Sizer
            // 
            this.Sizer.Location = new System.Drawing.Point(-4000, 2593);
            // 
            // TitleBar_RightCorner
            // 
            this.TitleBar_RightCorner.Location = new System.Drawing.Point(357, 0);
            // 
            // CreditViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(401, 642);
            this.Name = "CreditViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_RightCorner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_LeftCorner)).EndInit();
            this.ResumeLayout(false);

        }
    }
}