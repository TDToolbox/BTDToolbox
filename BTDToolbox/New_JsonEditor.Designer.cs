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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(New_JsonEditor));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lintPanel = new System.Windows.Forms.Panel();
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
            this.JsonToolstrip = new System.Windows.Forms.ToolStrip();
            this.FindReplace_Panel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.JsonToolstrip.SuspendLayout();
            this.FindReplace_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleSeperator
            // 
            // 
            // titleSeperator.Panel2
            // 
            this.titleSeperator.Panel2.Controls.Add(this.lintPanel);
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.JsonToolstrip);
            this.contentPanel.Controls.Add(this.tabControl1);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Indigo;
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(4, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(769, 366);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tabPage1.Controls.Add(this.FindReplace_Panel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(761, 340);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // lintPanel
            // 
            this.lintPanel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lintPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lintPanel.ForeColor = System.Drawing.Color.Black;
            this.lintPanel.Location = new System.Drawing.Point(123, 3);
            this.lintPanel.Name = "lintPanel";
            this.lintPanel.Size = new System.Drawing.Size(60, 24);
            this.lintPanel.TabIndex = 23;
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
            this.File_DropDown.Size = new System.Drawing.Size(38, 22);
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
            this.FontSize_TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
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
            // JsonToolstrip
            // 
            this.JsonToolstrip.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.JsonToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_DropDown,
            this.Help_DropDown,
            this.toolStripSeparator1});
            this.JsonToolstrip.Location = new System.Drawing.Point(0, 0);
            this.JsonToolstrip.Name = "JsonToolstrip";
            this.JsonToolstrip.Size = new System.Drawing.Size(776, 25);
            this.JsonToolstrip.TabIndex = 22;
            this.JsonToolstrip.Text = "toolStrip1";
            // 
            // FindReplace_Panel
            // 
            this.FindReplace_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FindReplace_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.FindReplace_Panel.Controls.Add(this.checkBox1);
            this.FindReplace_Panel.Controls.Add(this.richTextBox2);
            this.FindReplace_Panel.Controls.Add(this.button2);
            this.FindReplace_Panel.Controls.Add(this.richTextBox1);
            this.FindReplace_Panel.Controls.Add(this.button1);
            this.FindReplace_Panel.Location = new System.Drawing.Point(-4, 288);
            this.FindReplace_Panel.Name = "FindReplace_Panel";
            this.FindReplace_Panel.Size = new System.Drawing.Size(769, 56);
            this.FindReplace_Panel.TabIndex = 23;
            this.FindReplace_Panel.Visible = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(126, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Find Next";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(204, 2);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(229, 26);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Location = new System.Drawing.Point(530, 2);
            this.richTextBox2.Multiline = false;
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(229, 26);
            this.richTextBox2.TabIndex = 3;
            this.richTextBox2.Text = "";
            // 
            // button2
            // 
            this.button2.AllowDrop = true;
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(452, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 26);
            this.button2.TabIndex = 2;
            this.button2.Text = "Replace";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(661, 29);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 23);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Replace all?";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // New_JsonEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.KeyPreview = true;
            this.Name = "New_JsonEditor";
            this.Text = "New_JsonEditor";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.New_JsonEditor_KeyDown);
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.JsonToolstrip.ResumeLayout(false);
            this.JsonToolstrip.PerformLayout();
            this.FindReplace_Panel.ResumeLayout(false);
            this.FindReplace_Panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel lintPanel;
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
        private System.Windows.Forms.Panel FindReplace_Panel;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}