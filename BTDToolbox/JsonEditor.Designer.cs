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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Edit_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.Undo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Redo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowFindMenu_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowReplaceMenu_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ChangeFontSize_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.Find_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.FindNext_Button = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.Replace_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.Replace_Button = new System.Windows.Forms.ToolStripButton();
            this.ReplaceAll_Button = new System.Windows.Forms.ToolStripButton();
            this.Editor_TextBox = new System.Windows.Forms.RichTextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Edit_DropDown,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.Find_TextBox,
            this.FindNext_Button,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.Replace_TextBox,
            this.Replace_Button,
            this.ReplaceAll_Button});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1085, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Edit_DropDown
            // 
            this.Edit_DropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Edit_DropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Undo_Button,
            this.Redo_Button,
            this.ShowFindMenu_Button,
            this.ShowReplaceMenu_Button,
            this.toolStripSeparator3,
            this.ChangeFontSize_MenuItem});
            this.Edit_DropDown.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Edit_DropDown.Image = ((System.Drawing.Image)(resources.GetObject("Edit_DropDown.Image")));
            this.Edit_DropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_DropDown.Name = "Edit_DropDown";
            this.Edit_DropDown.Size = new System.Drawing.Size(40, 22);
            this.Edit_DropDown.Text = "Edit";
            // 
            // Undo_Button
            // 
            this.Undo_Button.Name = "Undo_Button";
            this.Undo_Button.Size = new System.Drawing.Size(200, 22);
            this.Undo_Button.Text = "Undo                (Ctrl + Z)";
            // 
            // Redo_Button
            // 
            this.Redo_Button.Name = "Redo_Button";
            this.Redo_Button.Size = new System.Drawing.Size(200, 22);
            this.Redo_Button.Text = "Redo                 (Ctrl + R)";
            // 
            // ShowFindMenu_Button
            // 
            this.ShowFindMenu_Button.Name = "ShowFindMenu_Button";
            this.ShowFindMenu_Button.Size = new System.Drawing.Size(200, 22);
            this.ShowFindMenu_Button.Text = "Find                  (Ctrl + F)";
            // 
            // ShowReplaceMenu_Button
            // 
            this.ShowReplaceMenu_Button.Name = "ShowReplaceMenu_Button";
            this.ShowReplaceMenu_Button.Size = new System.Drawing.Size(200, 22);
            this.ShowReplaceMenu_Button.Text = "Replace            (Ctrl + R)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(197, 6);
            // 
            // ChangeFontSize_MenuItem
            // 
            this.ChangeFontSize_MenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FontSize_TextBox});
            this.ChangeFontSize_MenuItem.Name = "ChangeFontSize_MenuItem";
            this.ChangeFontSize_MenuItem.Size = new System.Drawing.Size(200, 22);
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
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Find:  ";
            // 
            // Find_TextBox
            // 
            this.Find_TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Find_TextBox.Name = "Find_TextBox";
            this.Find_TextBox.Size = new System.Drawing.Size(200, 25);
            // 
            // FindNext_Button
            // 
            this.FindNext_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FindNext_Button.Image = ((System.Drawing.Image)(resources.GetObject("FindNext_Button.Image")));
            this.FindNext_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FindNext_Button.Name = "FindNext_Button";
            this.FindNext_Button.Size = new System.Drawing.Size(61, 22);
            this.FindNext_Button.Text = "Find Next";
            this.FindNext_Button.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(2, 0, 15, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(85, 22);
            this.toolStripLabel2.Text = "Replace With:  ";
            // 
            // Replace_TextBox
            // 
            this.Replace_TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Replace_TextBox.Name = "Replace_TextBox";
            this.Replace_TextBox.Size = new System.Drawing.Size(200, 25);
            // 
            // Replace_Button
            // 
            this.Replace_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Replace_Button.Image = ((System.Drawing.Image)(resources.GetObject("Replace_Button.Image")));
            this.Replace_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Replace_Button.Name = "Replace_Button";
            this.Replace_Button.Size = new System.Drawing.Size(52, 22);
            this.Replace_Button.Text = "Replace";
            this.Replace_Button.Click += new System.EventHandler(this.ReplaceButton_Click);
            // 
            // ReplaceAll_Button
            // 
            this.ReplaceAll_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ReplaceAll_Button.Image = ((System.Drawing.Image)(resources.GetObject("ReplaceAll_Button.Image")));
            this.ReplaceAll_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReplaceAll_Button.Margin = new System.Windows.Forms.Padding(10, 1, 0, 2);
            this.ReplaceAll_Button.Name = "ReplaceAll_Button";
            this.ReplaceAll_Button.Size = new System.Drawing.Size(69, 22);
            this.ReplaceAll_Button.Text = "Replace All";
            this.ReplaceAll_Button.Click += new System.EventHandler(this.ReplaceAllButton_Click);
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
            // 
            // JsonEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1085, 603);
            this.Controls.Add(this.Editor_TextBox);
            this.Controls.Add(this.toolStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "JsonEditor";
            this.Text = "JsonEditor";
            this.Load += new System.EventHandler(this.JsonEditor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
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
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox Find_TextBox;
        private System.Windows.Forms.ToolStripButton FindNext_Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox Replace_TextBox;
        private System.Windows.Forms.ToolStripButton ReplaceAll_Button;
        private System.Windows.Forms.ToolStripButton Replace_Button;
    }
}