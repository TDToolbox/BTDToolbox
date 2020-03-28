namespace BTDToolbox
{
    partial class JsonEditor_UserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonEditor_UserControl));
            this.Editor_TextBox = new System.Windows.Forms.RichTextBox();
            this.tB_line = new System.Windows.Forms.RichTextBox();
            this.JsonToolstrip = new System.Windows.Forms.ToolStrip();
            this.File_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.Undo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Redo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowFindMenu_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowReplaceMenu_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.FindSubtask_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ChangeFontSize_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.EasyTowerEditor_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.EZBoon_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Help_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lintPanel = new System.Windows.Forms.Panel();
            this.JsonToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Editor_TextBox
            // 
            this.Editor_TextBox.AcceptsTab = true;
            this.Editor_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Editor_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Editor_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editor_TextBox.ForeColor = System.Drawing.Color.White;
            this.Editor_TextBox.Location = new System.Drawing.Point(32, 22);
            this.Editor_TextBox.Name = "Editor_TextBox";
            this.Editor_TextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.Editor_TextBox.Size = new System.Drawing.Size(719, 309);
            this.Editor_TextBox.TabIndex = 26;
            this.Editor_TextBox.Text = "";
            this.Editor_TextBox.SelectionChanged += new System.EventHandler(this.Editor_TextBox_SelectionChanged);
            this.Editor_TextBox.VScroll += new System.EventHandler(this.Editor_TextBox_VScroll);
            this.Editor_TextBox.FontChanged += new System.EventHandler(this.Editor_TextBox_FontChanged);
            this.Editor_TextBox.TextChanged += new System.EventHandler(this.Editor_TextBox_TextChanged);
            // 
            // tB_line
            // 
            this.tB_line.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tB_line.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.tB_line.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tB_line.Font = new System.Drawing.Font("Consolas", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tB_line.ForeColor = System.Drawing.Color.DarkGray;
            this.tB_line.Location = new System.Drawing.Point(0, 24);
            this.tB_line.Name = "tB_line";
            this.tB_line.ReadOnly = true;
            this.tB_line.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.tB_line.Size = new System.Drawing.Size(53, 309);
            this.tB_line.TabIndex = 27;
            this.tB_line.TabStop = false;
            this.tB_line.Text = "";
            this.tB_line.WordWrap = false;
            this.tB_line.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TB_line_MouseDown);
            // 
            // JsonToolstrip
            // 
            this.JsonToolstrip.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.JsonToolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.JsonToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_DropDown,
            this.Help_DropDown,
            this.toolStripSeparator1});
            this.JsonToolstrip.Location = new System.Drawing.Point(0, 0);
            this.JsonToolstrip.Name = "JsonToolstrip";
            this.JsonToolstrip.Padding = new System.Windows.Forms.Padding(0, 0, 1, 5);
            this.JsonToolstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.JsonToolstrip.Size = new System.Drawing.Size(754, 28);
            this.JsonToolstrip.TabIndex = 28;
            this.JsonToolstrip.Text = "toolStrip1";
            // 
            // File_DropDown
            // 
            this.File_DropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.File_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.File_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Undo_Button,
            this.Redo_Button,
            this.ShowFindMenu_Button,
            this.ShowReplaceMenu_Button,
            this.FindSubtask_Button,
            this.toolStripSeparator3,
            this.ChangeFontSize_MenuItem,
            this.EasyTowerEditor_Button,
            this.EZBoon_Button});
            this.File_DropDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.File_DropDown.ForeColor = System.Drawing.Color.White;
            this.File_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("File_DropDown.Image")));
            this.File_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.File_DropDown.Name = "File_DropDown";
            this.File_DropDown.Size = new System.Drawing.Size(38, 20);
            this.File_DropDown.Text = "File";
            // 
            // Undo_Button
            // 
            this.Undo_Button.Name = "Undo_Button";
            this.Undo_Button.Size = new System.Drawing.Size(201, 22);
            this.Undo_Button.Text = "Undo                (Ctrl + Z)";
            // 
            // Redo_Button
            // 
            this.Redo_Button.Name = "Redo_Button";
            this.Redo_Button.Size = new System.Drawing.Size(201, 22);
            this.Redo_Button.Text = "Redo                 (Ctrl + R)";
            // 
            // ShowFindMenu_Button
            // 
            this.ShowFindMenu_Button.Name = "ShowFindMenu_Button";
            this.ShowFindMenu_Button.Size = new System.Drawing.Size(201, 22);
            this.ShowFindMenu_Button.Text = "Find                  (Ctrl + F)";
            // 
            // ShowReplaceMenu_Button
            // 
            this.ShowReplaceMenu_Button.Name = "ShowReplaceMenu_Button";
            this.ShowReplaceMenu_Button.Size = new System.Drawing.Size(201, 22);
            this.ShowReplaceMenu_Button.Text = "Replace            (Ctrl + H)";
            // 
            // FindSubtask_Button
            // 
            this.FindSubtask_Button.Name = "FindSubtask_Button";
            this.FindSubtask_Button.Size = new System.Drawing.Size(201, 22);
            this.FindSubtask_Button.Text = "Find Subtask";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(198, 6);
            // 
            // ChangeFontSize_MenuItem
            // 
            this.ChangeFontSize_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontSize_TextBox});
            this.ChangeFontSize_MenuItem.Name = "ChangeFontSize_MenuItem";
            this.ChangeFontSize_MenuItem.Size = new System.Drawing.Size(201, 22);
            this.ChangeFontSize_MenuItem.Text = "Change Font Size";
            // 
            // FontSize_TextBox
            // 
            this.FontSize_TextBox.Name = "FontSize_TextBox";
            this.FontSize_TextBox.Size = new System.Drawing.Size(100, 23);
            this.FontSize_TextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EasyTowerEditor_Button
            // 
            this.EasyTowerEditor_Button.Name = "EasyTowerEditor_Button";
            this.EasyTowerEditor_Button.Size = new System.Drawing.Size(201, 22);
            this.EasyTowerEditor_Button.Text = "EZ Tower tool";
            this.EasyTowerEditor_Button.Visible = false;
            // 
            // EZBoon_Button
            // 
            this.EZBoon_Button.Name = "EZBoon_Button";
            this.EZBoon_Button.Size = new System.Drawing.Size(201, 22);
            this.EZBoon_Button.Text = "EZ Bloon tool";
            // 
            // Help_DropDown
            // 
            this.Help_DropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Help_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Help_DropDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Help_DropDown.ForeColor = System.Drawing.Color.White;
            this.Help_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("Help_DropDown.Image")));
            this.Help_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Help_DropDown.Name = "Help_DropDown";
            this.Help_DropDown.Size = new System.Drawing.Size(45, 22);
            this.Help_DropDown.Text = "Help";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lintPanel
            // 
            this.lintPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lintPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lintPanel.ForeColor = System.Drawing.Color.Black;
            this.lintPanel.Location = new System.Drawing.Point(110, 0);
            this.lintPanel.Name = "lintPanel";
            this.lintPanel.Size = new System.Drawing.Size(60, 20);
            this.lintPanel.TabIndex = 29;
            // 
            // JsonEditor_UserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.lintPanel);
            this.Controls.Add(this.JsonToolstrip);
            this.Controls.Add(this.Editor_TextBox);
            this.Controls.Add(this.tB_line);
            this.Name = "JsonEditor_UserControl";
            this.Size = new System.Drawing.Size(754, 337);
            this.Resize += new System.EventHandler(this.JsonEditor_UserControl_Resize);
            this.JsonToolstrip.ResumeLayout(false);
            this.JsonToolstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip JsonToolstrip;
        private System.Windows.Forms.ToolStripDropDownButton File_DropDown;
        private System.Windows.Forms.ToolStripMenuItem Undo_Button;
        private System.Windows.Forms.ToolStripMenuItem Redo_Button;
        private System.Windows.Forms.ToolStripMenuItem ShowFindMenu_Button;
        private System.Windows.Forms.ToolStripMenuItem ShowReplaceMenu_Button;
        private System.Windows.Forms.ToolStripMenuItem FindSubtask_Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ChangeFontSize_MenuItem;
        private System.Windows.Forms.ToolStripTextBox FontSize_TextBox;
        private System.Windows.Forms.ToolStripMenuItem EasyTowerEditor_Button;
        private System.Windows.Forms.ToolStripMenuItem EZBoon_Button;
        private System.Windows.Forms.ToolStripDropDownButton Help_DropDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel lintPanel;
        public System.Windows.Forms.RichTextBox Editor_TextBox;
        public System.Windows.Forms.RichTextBox tB_line;
    }
}
