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
            this.contentPanel.Size = new System.Drawing.Size(0, 770);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.Location = new System.Drawing.Point(-62, 10);
            // 
            // Sizer
            // 
            this.Sizer.Location = new System.Drawing.Point(-10, 773);
            // 
            // TitleBar_RightCorner
            // 
            this.TitleBar_RightCorner.Location = new System.Drawing.Point(-43, 12);
            // 
            // CreditViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(401, 642);
            this.Name = "CreditViewer";
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