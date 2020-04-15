namespace BTDToolbox
{
    partial class JsonEditor_Instance
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonEditor_Instance));
            this.Editor_TextBox = new System.Windows.Forms.RichTextBox();
            this.tB_line = new System.Windows.Forms.RichTextBox();
            this.JsonToolstrip = new System.Windows.Forms.ToolStrip();
            this.File_DropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.Undo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Redo_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowFindMenu_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowReplaceMenu_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.FindSubtask_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.ReformatJSON_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.EZCard_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.EZBoon_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.EZTowerEditor_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ChangeFontSize_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FontSize_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.viewOriginalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInFileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestoreToOriginal_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.Open_Button = new System.Windows.Forms.ToolStripDropDownButton();
            this.Weapons_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.TowerFile_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.UpgradeFIle_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.specialtyBuildingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TowerSpriteUpgradeDef_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lintPanel = new System.Windows.Forms.Panel();
            this.CloseFile_Button = new System.Windows.Forms.Button();
            this.JsonError_Label = new System.Windows.Forms.Label();
            this.Find_Panel = new System.Windows.Forms.Panel();
            this.SearchOptions_Button = new System.Windows.Forms.Button();
            this.Find_Button = new System.Windows.Forms.Button();
            this.Find_TB = new System.Windows.Forms.RichTextBox();
            this.Replace_Button = new System.Windows.Forms.Button();
            this.Replace_TB = new System.Windows.Forms.RichTextBox();
            this.SearchOptions_Panel = new System.Windows.Forms.Panel();
            this.Option1_CB = new System.Windows.Forms.CheckBox();
            this.NoItem_CM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findSubtaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getCurrentSubtaskNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ItemHighlighted_CM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findSubtaskToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.getThisSubtaskNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Encrypt_Button = new System.Windows.Forms.Button();
            this.disableAutoformattinhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.JsonToolstrip.SuspendLayout();
            this.Find_Panel.SuspendLayout();
            this.SearchOptions_Panel.SuspendLayout();
            this.NoItem_CM.SuspendLayout();
            this.ItemHighlighted_CM.SuspendLayout();
            this.SuspendLayout();
            // 
            // Editor_TextBox
            // 
            this.Editor_TextBox.AcceptsTab = true;
            this.Editor_TextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Editor_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Editor_TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Editor_TextBox.ForeColor = System.Drawing.Color.White;
            this.Editor_TextBox.Location = new System.Drawing.Point(32, 24);
            this.Editor_TextBox.Name = "Editor_TextBox";
            this.Editor_TextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.Editor_TextBox.Size = new System.Drawing.Size(719, 313);
            this.Editor_TextBox.TabIndex = 26;
            this.Editor_TextBox.Text = "";
            this.Editor_TextBox.SelectionChanged += new System.EventHandler(this.Editor_TextBox_SelectionChanged);
            this.Editor_TextBox.VScroll += new System.EventHandler(this.Editor_TextBox_VScroll);
            this.Editor_TextBox.FontChanged += new System.EventHandler(this.Editor_TextBox_FontChanged);
            this.Editor_TextBox.TextChanged += new System.EventHandler(this.Editor_TextBox_TextChanged);
            this.Editor_TextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_TextBox_KeyDown);
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
            this.tB_line.Size = new System.Drawing.Size(53, 313);
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
            this.Open_Button,
            this.toolStripSeparator1});
            this.JsonToolstrip.Location = new System.Drawing.Point(0, 0);
            this.JsonToolstrip.Name = "JsonToolstrip";
            this.JsonToolstrip.Padding = new System.Windows.Forms.Padding(0, 0, 1, 5);
            this.JsonToolstrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
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
            this.ReformatJSON_Button,
            this.disableAutoformattinhToolStripMenuItem,
            this.EZCard_Button,
            this.EZBoon_Button,
            this.EZTowerEditor_Button,
            this.toolStripSeparator3,
            this.ChangeFontSize_MenuItem,
            this.toolStripSeparator2,
            this.viewOriginalToolStripMenuItem,
            this.openInFileExplorerToolStripMenuItem,
            this.RestoreToOriginal_Button});
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
            this.Undo_Button.Visible = false;
            // 
            // Redo_Button
            // 
            this.Redo_Button.Name = "Redo_Button";
            this.Redo_Button.Size = new System.Drawing.Size(201, 22);
            this.Redo_Button.Text = "Redo                 (Ctrl + R)";
            this.Redo_Button.Visible = false;
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
            // FindSubtask_Button
            // 
            this.FindSubtask_Button.Name = "FindSubtask_Button";
            this.FindSubtask_Button.Size = new System.Drawing.Size(201, 22);
            this.FindSubtask_Button.Text = "Find Subtask";
            this.FindSubtask_Button.Click += new System.EventHandler(this.FindSubtask_Button_Click);
            // 
            // ReformatJSON_Button
            // 
            this.ReformatJSON_Button.Name = "ReformatJSON_Button";
            this.ReformatJSON_Button.Size = new System.Drawing.Size(201, 22);
            this.ReformatJSON_Button.Text = "Re-format JSON";
            this.ReformatJSON_Button.Click += new System.EventHandler(this.ReformatJSON_Button_Click);
            // 
            // EZCard_Button
            // 
            this.EZCard_Button.Name = "EZCard_Button";
            this.EZCard_Button.Size = new System.Drawing.Size(201, 22);
            this.EZCard_Button.Text = "Open in EZ Card tool";
            this.EZCard_Button.Visible = false;
            this.EZCard_Button.Click += new System.EventHandler(this.EZCard_Button_Click);
            // 
            // EZBoon_Button
            // 
            this.EZBoon_Button.Name = "EZBoon_Button";
            this.EZBoon_Button.Size = new System.Drawing.Size(201, 22);
            this.EZBoon_Button.Text = "Open in EZ Bloon tool";
            this.EZBoon_Button.Visible = false;
            this.EZBoon_Button.Click += new System.EventHandler(this.EZBoon_Button_Click);
            // 
            // EZTowerEditor_Button
            // 
            this.EZTowerEditor_Button.Name = "EZTowerEditor_Button";
            this.EZTowerEditor_Button.Size = new System.Drawing.Size(201, 22);
            this.EZTowerEditor_Button.Text = "Open in EZ Tower tool";
            this.EZTowerEditor_Button.Visible = false;
            this.EZTowerEditor_Button.Click += new System.EventHandler(this.EZTowerEditor_Button_Click);
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(198, 6);
            // 
            // viewOriginalToolStripMenuItem
            // 
            this.viewOriginalToolStripMenuItem.Name = "viewOriginalToolStripMenuItem";
            this.viewOriginalToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.viewOriginalToolStripMenuItem.Text = "View Original";
            this.viewOriginalToolStripMenuItem.Click += new System.EventHandler(this.ViewOriginalToolStripMenuItem_Click);
            // 
            // openInFileExplorerToolStripMenuItem
            // 
            this.openInFileExplorerToolStripMenuItem.Name = "openInFileExplorerToolStripMenuItem";
            this.openInFileExplorerToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.openInFileExplorerToolStripMenuItem.Text = "Open in File Explorer";
            this.openInFileExplorerToolStripMenuItem.Click += new System.EventHandler(this.OpenInFileExplorerToolStripMenuItem_Click);
            // 
            // RestoreToOriginal_Button
            // 
            this.RestoreToOriginal_Button.Name = "RestoreToOriginal_Button";
            this.RestoreToOriginal_Button.Size = new System.Drawing.Size(201, 22);
            this.RestoreToOriginal_Button.Text = "Restore File to Original";
            this.RestoreToOriginal_Button.Click += new System.EventHandler(this.RestoreToOriginal_Button_Click);
            // 
            // Open_Button
            // 
            this.Open_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Open_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Open_Button.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Weapons_Button,
            this.TowerFile_Button,
            this.UpgradeFIle_Button,
            this.specialtyBuildingToolStripMenuItem,
            this.TowerSpriteUpgradeDef_Button});
            this.Open_Button.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Open_Button.ForeColor = System.Drawing.Color.White;
            this.Open_Button.Image = ((System.Drawing.Image)(resources.GetObject("Open_Button.Image")));
            this.Open_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Open_Button.Name = "Open_Button";
            this.Open_Button.Size = new System.Drawing.Size(49, 20);
            this.Open_Button.Text = "Open";
            this.Open_Button.Visible = false;
            // 
            // Weapons_Button
            // 
            this.Weapons_Button.Name = "Weapons_Button";
            this.Weapons_Button.Size = new System.Drawing.Size(198, 22);
            this.Weapons_Button.Text = "Weapons";
            // 
            // TowerFile_Button
            // 
            this.TowerFile_Button.Name = "TowerFile_Button";
            this.TowerFile_Button.Size = new System.Drawing.Size(198, 22);
            this.TowerFile_Button.Text = ".tower file";
            this.TowerFile_Button.Click += new System.EventHandler(this.TowerFile_Button_Click);
            // 
            // UpgradeFIle_Button
            // 
            this.UpgradeFIle_Button.Name = "UpgradeFIle_Button";
            this.UpgradeFIle_Button.Size = new System.Drawing.Size(198, 22);
            this.UpgradeFIle_Button.Text = ".upgrade file";
            this.UpgradeFIle_Button.Click += new System.EventHandler(this.UpgradeFIle_Button_Click);
            // 
            // specialtyBuildingToolStripMenuItem
            // 
            this.specialtyBuildingToolStripMenuItem.Name = "specialtyBuildingToolStripMenuItem";
            this.specialtyBuildingToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.specialtyBuildingToolStripMenuItem.Text = "Specialty Building";
            this.specialtyBuildingToolStripMenuItem.Click += new System.EventHandler(this.SpecialtyBuildingToolStripMenuItem_Click);
            // 
            // TowerSpriteUpgradeDef_Button
            // 
            this.TowerSpriteUpgradeDef_Button.Name = "TowerSpriteUpgradeDef_Button";
            this.TowerSpriteUpgradeDef_Button.Size = new System.Drawing.Size(198, 22);
            this.TowerSpriteUpgradeDef_Button.Text = "TowerSpriteUpgradeDef";
            this.TowerSpriteUpgradeDef_Button.Click += new System.EventHandler(this.TowerSpriteUpgradeDef_Button_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
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
            this.lintPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LintPanel_MouseClick);
            // 
            // CloseFile_Button
            // 
            this.CloseFile_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseFile_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.CloseFile_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseFile_Button.Location = new System.Drawing.Point(527, 0);
            this.CloseFile_Button.Name = "CloseFile_Button";
            this.CloseFile_Button.Size = new System.Drawing.Size(36, 20);
            this.CloseFile_Button.TabIndex = 30;
            this.CloseFile_Button.Text = "X";
            this.CloseFile_Button.UseVisualStyleBackColor = false;
            this.CloseFile_Button.Click += new System.EventHandler(this.CloseFile_Button_Click);
            // 
            // JsonError_Label
            // 
            this.JsonError_Label.AutoSize = true;
            this.JsonError_Label.Font = new System.Drawing.Font("Microsoft YaHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JsonError_Label.ForeColor = System.Drawing.Color.Orange;
            this.JsonError_Label.Location = new System.Drawing.Point(176, 4);
            this.JsonError_Label.Name = "JsonError_Label";
            this.JsonError_Label.Size = new System.Drawing.Size(178, 20);
            this.JsonError_Label.TabIndex = 31;
            this.JsonError_Label.Text = "<<  Click to go to error";
            this.JsonError_Label.Visible = false;
            // 
            // Find_Panel
            // 
            this.Find_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Find_Panel.Controls.Add(this.SearchOptions_Button);
            this.Find_Panel.Controls.Add(this.Find_Button);
            this.Find_Panel.Controls.Add(this.Find_TB);
            this.Find_Panel.Controls.Add(this.Replace_Button);
            this.Find_Panel.Controls.Add(this.Replace_TB);
            this.Find_Panel.Location = new System.Drawing.Point(3, 213);
            this.Find_Panel.Name = "Find_Panel";
            this.Find_Panel.Size = new System.Drawing.Size(748, 124);
            this.Find_Panel.TabIndex = 32;
            this.Find_Panel.Visible = false;
            // 
            // SearchOptions_Button
            // 
            this.SearchOptions_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchOptions_Button.Font = new System.Drawing.Font("Candara Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchOptions_Button.Location = new System.Drawing.Point(270, 6);
            this.SearchOptions_Button.Name = "SearchOptions_Button";
            this.SearchOptions_Button.Size = new System.Drawing.Size(25, 21);
            this.SearchOptions_Button.TabIndex = 5;
            this.SearchOptions_Button.Text = "^";
            this.SearchOptions_Button.UseVisualStyleBackColor = true;
            this.SearchOptions_Button.Visible = false;
            this.SearchOptions_Button.Click += new System.EventHandler(this.FindOptions_Button_Click);
            // 
            // Find_Button
            // 
            this.Find_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Find_Button.Location = new System.Drawing.Point(30, 4);
            this.Find_Button.Name = "Find_Button";
            this.Find_Button.Size = new System.Drawing.Size(59, 24);
            this.Find_Button.TabIndex = 4;
            this.Find_Button.Text = "Find";
            this.Find_Button.UseVisualStyleBackColor = true;
            this.Find_Button.Visible = false;
            this.Find_Button.Click += new System.EventHandler(this.Find_Button_Click);
            // 
            // Find_TB
            // 
            this.Find_TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Find_TB.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Find_TB.Location = new System.Drawing.Point(90, 5);
            this.Find_TB.Multiline = false;
            this.Find_TB.Name = "Find_TB";
            this.Find_TB.Size = new System.Drawing.Size(181, 23);
            this.Find_TB.TabIndex = 3;
            this.Find_TB.Text = "";
            this.Find_TB.Visible = false;
            this.Find_TB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Find_TB_KeyDown);
            // 
            // Replace_Button
            // 
            this.Replace_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Replace_Button.Location = new System.Drawing.Point(301, 4);
            this.Replace_Button.Name = "Replace_Button";
            this.Replace_Button.Size = new System.Drawing.Size(59, 24);
            this.Replace_Button.TabIndex = 2;
            this.Replace_Button.Text = "Replace";
            this.Replace_Button.UseVisualStyleBackColor = true;
            this.Replace_Button.Visible = false;
            this.Replace_Button.Click += new System.EventHandler(this.Replace_Button_Click);
            // 
            // Replace_TB
            // 
            this.Replace_TB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Replace_TB.Font = new System.Drawing.Font("Microsoft YaHei", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Replace_TB.Location = new System.Drawing.Point(361, 5);
            this.Replace_TB.Multiline = false;
            this.Replace_TB.Name = "Replace_TB";
            this.Replace_TB.Size = new System.Drawing.Size(181, 23);
            this.Replace_TB.TabIndex = 0;
            this.Replace_TB.Text = "";
            this.Replace_TB.Visible = false;
            // 
            // SearchOptions_Panel
            // 
            this.SearchOptions_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchOptions_Panel.Controls.Add(this.Option1_CB);
            this.SearchOptions_Panel.Location = new System.Drawing.Point(473, 188);
            this.SearchOptions_Panel.Name = "SearchOptions_Panel";
            this.SearchOptions_Panel.Size = new System.Drawing.Size(104, 25);
            this.SearchOptions_Panel.TabIndex = 33;
            this.SearchOptions_Panel.Visible = false;
            // 
            // Option1_CB
            // 
            this.Option1_CB.AutoSize = true;
            this.Option1_CB.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Option1_CB.ForeColor = System.Drawing.Color.White;
            this.Option1_CB.Location = new System.Drawing.Point(15, 7);
            this.Option1_CB.Name = "Option1_CB";
            this.Option1_CB.Size = new System.Drawing.Size(89, 25);
            this.Option1_CB.TabIndex = 0;
            this.Option1_CB.Text = "Subtask";
            this.Option1_CB.UseVisualStyleBackColor = true;
            this.Option1_CB.CheckedChanged += new System.EventHandler(this.Option1_CB_CheckedChanged);
            // 
            // NoItem_CM
            // 
            this.NoItem_CM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.findSubtaskToolStripMenuItem,
            this.getCurrentSubtaskNumberToolStripMenuItem});
            this.NoItem_CM.Name = "NoItem_CM";
            this.NoItem_CM.Size = new System.Drawing.Size(203, 70);
            this.NoItem_CM.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.NoItem_CM_ItemClicked);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.testToolStripMenuItem.Text = "Paste";
            // 
            // findSubtaskToolStripMenuItem
            // 
            this.findSubtaskToolStripMenuItem.Name = "findSubtaskToolStripMenuItem";
            this.findSubtaskToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.findSubtaskToolStripMenuItem.Text = "Find Subtask";
            // 
            // getCurrentSubtaskNumberToolStripMenuItem
            // 
            this.getCurrentSubtaskNumberToolStripMenuItem.Name = "getCurrentSubtaskNumberToolStripMenuItem";
            this.getCurrentSubtaskNumberToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.getCurrentSubtaskNumberToolStripMenuItem.Text = "Get this subtask number";
            // 
            // ItemHighlighted_CM
            // 
            this.ItemHighlighted_CM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.findSubtaskToolStripMenuItem1,
            this.getThisSubtaskNumberToolStripMenuItem});
            this.ItemHighlighted_CM.Name = "ItemHighlighted_CM";
            this.ItemHighlighted_CM.Size = new System.Drawing.Size(203, 136);
            this.ItemHighlighted_CM.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ItemHighlighted_CM_ItemClicked);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.findToolStripMenuItem.Text = "Find";
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.replaceToolStripMenuItem.Text = "Replace";
            // 
            // findSubtaskToolStripMenuItem1
            // 
            this.findSubtaskToolStripMenuItem1.Name = "findSubtaskToolStripMenuItem1";
            this.findSubtaskToolStripMenuItem1.Size = new System.Drawing.Size(202, 22);
            this.findSubtaskToolStripMenuItem1.Text = "Find Subtask";
            // 
            // getThisSubtaskNumberToolStripMenuItem
            // 
            this.getThisSubtaskNumberToolStripMenuItem.Name = "getThisSubtaskNumberToolStripMenuItem";
            this.getThisSubtaskNumberToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.getThisSubtaskNumberToolStripMenuItem.Text = "Get this subtask number";
            // 
            // Encrypt_Button
            // 
            this.Encrypt_Button.BackColor = System.Drawing.Color.PaleGreen;
            this.Encrypt_Button.Location = new System.Drawing.Point(176, 0);
            this.Encrypt_Button.Name = "Encrypt_Button";
            this.Encrypt_Button.Size = new System.Drawing.Size(178, 20);
            this.Encrypt_Button.TabIndex = 34;
            this.Encrypt_Button.Text = "Encrypt and send to game";
            this.Encrypt_Button.UseVisualStyleBackColor = false;
            this.Encrypt_Button.Visible = false;
            this.Encrypt_Button.Click += new System.EventHandler(this.Encrypt_Button_Click);
            // 
            // disableAutoformattinhToolStripMenuItem
            // 
            this.disableAutoformattinhToolStripMenuItem.Name = "disableAutoformattinhToolStripMenuItem";
            this.disableAutoformattinhToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.disableAutoformattinhToolStripMenuItem.Text = "Disable auto-formatting";
            this.disableAutoformattinhToolStripMenuItem.Click += new System.EventHandler(this.DisableAutoformattinhToolStripMenuItem_Click);
            // 
            // JsonEditor_Instance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.Encrypt_Button);
            this.Controls.Add(this.SearchOptions_Panel);
            this.Controls.Add(this.Find_Panel);
            this.Controls.Add(this.JsonError_Label);
            this.Controls.Add(this.CloseFile_Button);
            this.Controls.Add(this.lintPanel);
            this.Controls.Add(this.JsonToolstrip);
            this.Controls.Add(this.Editor_TextBox);
            this.Controls.Add(this.tB_line);
            this.Name = "JsonEditor_Instance";
            this.Size = new System.Drawing.Size(754, 337);
            this.Resize += new System.EventHandler(this.JsonEditor_UserControl_Resize);
            this.JsonToolstrip.ResumeLayout(false);
            this.JsonToolstrip.PerformLayout();
            this.Find_Panel.ResumeLayout(false);
            this.SearchOptions_Panel.ResumeLayout(false);
            this.SearchOptions_Panel.PerformLayout();
            this.NoItem_CM.ResumeLayout(false);
            this.ItemHighlighted_CM.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem EZTowerEditor_Button;
        private System.Windows.Forms.ToolStripMenuItem EZBoon_Button;
        private System.Windows.Forms.ToolStripDropDownButton Open_Button;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel lintPanel;
        public System.Windows.Forms.RichTextBox Editor_TextBox;
        public System.Windows.Forms.RichTextBox tB_line;
        private System.Windows.Forms.Button CloseFile_Button;
        private System.Windows.Forms.Label JsonError_Label;
        private System.Windows.Forms.Panel Find_Panel;
        private System.Windows.Forms.RichTextBox Replace_TB;
        private System.Windows.Forms.Button Replace_Button;
        private System.Windows.Forms.Button Find_Button;
        private System.Windows.Forms.RichTextBox Find_TB;
        private System.Windows.Forms.Button SearchOptions_Button;
        private System.Windows.Forms.Panel SearchOptions_Panel;
        private System.Windows.Forms.CheckBox Option1_CB;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem viewOriginalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInFileExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RestoreToOriginal_Button;
        private System.Windows.Forms.ToolStripMenuItem EZCard_Button;
        private System.Windows.Forms.ContextMenuStrip NoItem_CM;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ItemHighlighted_CM;
        private System.Windows.Forms.ToolStripMenuItem findSubtaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getCurrentSubtaskNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findSubtaskToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem getThisSubtaskNumberToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TowerFile_Button;
        private System.Windows.Forms.ToolStripMenuItem UpgradeFIle_Button;
        private System.Windows.Forms.ToolStripMenuItem Weapons_Button;
        private System.Windows.Forms.ToolStripMenuItem TowerSpriteUpgradeDef_Button;
        private System.Windows.Forms.ToolStripMenuItem specialtyBuildingToolStripMenuItem;
        private System.Windows.Forms.Button Encrypt_Button;
        private System.Windows.Forms.ToolStripMenuItem ReformatJSON_Button;
        private System.Windows.Forms.ToolStripMenuItem disableAutoformattinhToolStripMenuItem;
    }
}
