namespace BTDToolbox
{
    partial class JsonEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonEditor));
            this.JsonToolstrip = new System.Windows.Forms.ToolStrip();
            this.Edit_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.Undo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Redo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowFindMenu_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowReplaceMenu_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ChangeFontSize_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Replace_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.Replace_Button = new System.Windows.Forms.ToolStripDropDownButton();
            this.ReplaceButton_DropDown = new System.Windows.Forms.ToolStripMenuItem();
            this.ReplaceAllButton_DropDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Find_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.FindNext_Button = new System.Windows.Forms.ToolStripButton();
            this.Editor_TextBox = new System.Windows.Forms.RichTextBox();
            this.JsonToolstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // JsonToolstrip
            // 
            this.JsonToolstrip.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.JsonToolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Edit_DropDown,
            this.toolStripSeparator1,
            this.Replace_TextBox,
            this.Replace_Button,
            this.toolStripSeparator2,
            this.Find_TextBox,
            this.FindNext_Button});
            this.JsonToolstrip.Location = new System.Drawing.Point(0, 0);
            this.JsonToolstrip.Name = "JsonToolstrip";
            this.JsonToolstrip.Size = new System.Drawing.Size(1085, 25);
            this.JsonToolstrip.TabIndex = 1;
            this.JsonToolstrip.Text = "toolStrip1";
            // 
            // Edit_DropDown
            // 
            this.Edit_DropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Edit_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Edit_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Undo_Button,
            this.Redo_Button,
            this.ShowFindMenu_Button,
            this.ShowReplaceMenu_Button,
            this.toolStripSeparator3,
            this.ChangeFontSize_MenuItem});
            this.Edit_DropDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Edit_DropDown.ForeColor = System.Drawing.Color.White;
            this.Edit_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("Edit_DropDown.Image")));
            this.Edit_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_DropDown.Name = "Edit_DropDown";
            this.Edit_DropDown.Size = new System.Drawing.Size(40, 22);
            this.Edit_DropDown.Text = "Edit";
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
            this.ShowFindMenu_Button.Click += new System.EventHandler(this.ShowFindMenu_Button_Click);
            // 
            // ShowReplaceMenu_Button
            // 
            this.ShowReplaceMenu_Button.Name = "ShowReplaceMenu_Button";
            this.ShowReplaceMenu_Button.Size = new System.Drawing.Size(201, 22);
            this.ShowReplaceMenu_Button.Text = "Replace            (Ctrl + H)";
            this.ShowReplaceMenu_Button.Click += new System.EventHandler(this.ShowReplaceMenu_Button_Click);
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
            this.FontSize_TextBox.TextChanged += new System.EventHandler(this.FontSize_TextBox_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Replace_TextBox
            // 
            this.Replace_TextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Replace_TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Replace_TextBox.Margin = new System.Windows.Forms.Padding(1, 0, 25, 0);
            this.Replace_TextBox.Name = "Replace_TextBox";
            this.Replace_TextBox.Size = new System.Drawing.Size(200, 25);
            // 
            // Replace_Button
            // 
            this.Replace_Button.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Replace_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Replace_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Replace_Button.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReplaceButton_DropDown,
            this.ReplaceAllButton_DropDown});
            this.Replace_Button.ForeColor = System.Drawing.Color.White;
            this.Replace_Button.Image = ((System.Drawing.Image)(resources.GetObject("Replace_Button.Image")));
            this.Replace_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Replace_Button.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.Replace_Button.Name = "Replace_Button";
            this.Replace_Button.Size = new System.Drawing.Size(61, 22);
            this.Replace_Button.Text = "Replace";
            // 
            // ReplaceButton_DropDown
            // 
            this.ReplaceButton_DropDown.Name = "ReplaceButton_DropDown";
            this.ReplaceButton_DropDown.Size = new System.Drawing.Size(132, 22);
            this.ReplaceButton_DropDown.Text = "Replace";
            this.ReplaceButton_DropDown.Click += new System.EventHandler(this.ReplaceButton_DropDown_Click);
            // 
            // ReplaceAllButton_DropDown
            // 
            this.ReplaceAllButton_DropDown.Name = "ReplaceAllButton_DropDown";
            this.ReplaceAllButton_DropDown.Size = new System.Drawing.Size(132, 22);
            this.ReplaceAllButton_DropDown.Text = "Replace All";
            this.ReplaceAllButton_DropDown.Click += new System.EventHandler(this.ReplaceAllButton_DropDown_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.BackColor = System.Drawing.Color.Black;
            this.toolStripSeparator2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(10, 0, 30, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // Find_TextBox
            // 
            this.Find_TextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Find_TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Find_TextBox.Margin = new System.Windows.Forms.Padding(1, 0, 25, 0);
            this.Find_TextBox.Name = "Find_TextBox";
            this.Find_TextBox.Size = new System.Drawing.Size(200, 25);
            this.Find_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Find_TextBox_KeyDown);
            // 
            // FindNext_Button
            // 
            this.FindNext_Button.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.FindNext_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.FindNext_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FindNext_Button.ForeColor = System.Drawing.Color.White;
            this.FindNext_Button.Image = ((System.Drawing.Image)(resources.GetObject("FindNext_Button.Image")));
            this.FindNext_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FindNext_Button.Margin = new System.Windows.Forms.Padding(5, 1, 5, 2);
            this.FindNext_Button.Name = "FindNext_Button";
            this.FindNext_Button.Size = new System.Drawing.Size(62, 22);
            this.FindNext_Button.Text = "Find Next";
            this.FindNext_Button.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // Editor_TextBox
            // 
            this.Editor_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Editor_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.Editor_TextBox.ForeColor = System.Drawing.Color.White;
            this.Editor_TextBox.Location = new System.Drawing.Point(0, 28);
            this.Editor_TextBox.Name = "Editor_TextBox";
            this.Editor_TextBox.Size = new System.Drawing.Size(1085, 573);
            this.Editor_TextBox.TabIndex = 2;
            this.Editor_TextBox.Text = "";
            this.Editor_TextBox.TextChanged += new System.EventHandler(this.Editor_TextBox_TextChanged);
            this.Editor_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_TextBox_KeyDown);
            // 
            // JsonEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1085, 603);
            this.Controls.Add(this.Editor_TextBox);
            this.Controls.Add(this.JsonToolstrip);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "JsonEditor";
            this.Text = "JsonEditor";
            this.Load += new System.EventHandler(this.JsonEditor_Load);
            this.JsonToolstrip.ResumeLayout(false);
            this.JsonToolstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip JsonToolstrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton Edit_DropDown;
        private System.Windows.Forms.ToolStripMenuItem Undo_Button;
        private System.Windows.Forms.ToolStripMenuItem Redo_Button;
        private System.Windows.Forms.ToolStripMenuItem ShowFindMenu_Button;
        private System.Windows.Forms.ToolStripMenuItem ShowReplaceMenu_Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ChangeFontSize_MenuItem;
        private System.Windows.Forms.ToolStripTextBox FontSize_TextBox;
        private System.Windows.Forms.RichTextBox Editor_TextBox;
        private System.Windows.Forms.ToolStripTextBox Find_TextBox;
        private System.Windows.Forms.ToolStripButton FindNext_Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripTextBox Replace_TextBox;
        private System.Windows.Forms.ToolStripDropDownButton Replace_Button;
        private System.Windows.Forms.ToolStripMenuItem ReplaceButton_DropDown;
        private System.Windows.Forms.ToolStripMenuItem ReplaceAllButton_DropDown;
    }
}