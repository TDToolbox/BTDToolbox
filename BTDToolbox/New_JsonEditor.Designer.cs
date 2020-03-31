namespace BTDToolbox
{
    partial class New_JsonEditor
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInFileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleSeperator
            // 
            // 
            // TitleLabel
            // 
            this.TitleLabel.Size = new System.Drawing.Size(75, 16);
            this.TitleLabel.Text = "Json Editor";
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.contentPanel.Controls.Add(this.tabControl1);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Indigo;
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            this.close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(4, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(769, 397);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.TabControl1_DrawItem);
            this.tabControl1.SizeChanged += new System.EventHandler(this.TabControl1_SizeChanged);
            // 
            // ContextMenu
            // 
            this.ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.viewOriginalToolStripMenuItem,
            this.restoreToOriginalToolStripMenuItem,
            this.openInFileExplorerToolStripMenuItem});
            this.ContextMenu.Name = "ContextMenu";
            this.ContextMenu.Size = new System.Drawing.Size(184, 114);
            this.ContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ContextClicked);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // viewOriginalToolStripMenuItem
            // 
            this.viewOriginalToolStripMenuItem.Name = "viewOriginalToolStripMenuItem";
            this.viewOriginalToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.viewOriginalToolStripMenuItem.Text = "View original";
            // 
            // restoreToOriginalToolStripMenuItem
            // 
            this.restoreToOriginalToolStripMenuItem.Name = "restoreToOriginalToolStripMenuItem";
            this.restoreToOriginalToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.restoreToOriginalToolStripMenuItem.Text = "Restore to original";
            // 
            // openInFileExplorerToolStripMenuItem
            // 
            this.openInFileExplorerToolStripMenuItem.Name = "openInFileExplorerToolStripMenuItem";
            this.openInFileExplorerToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openInFileExplorerToolStripMenuItem.Text = "Open in File Explorer";
            // 
            // New_JsonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.KeyPreview = true;
            this.Name = "New_JsonEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "New_JsonEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.New_JsonEditor_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.New_JsonEditor_KeyDown);
            this.Controls.SetChildIndex(this.titleSeperator, 0);
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.ContextMenuStrip ContextMenu;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInFileExplorerToolStripMenuItem;
    }
}