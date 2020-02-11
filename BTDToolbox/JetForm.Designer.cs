namespace BTDToolbox
{
    partial class JetForm : ThemedForm
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
        private new void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JetForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.fileViewContainer = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Open_Proj_Dir = new System.Windows.Forms.ToolStripMenuItem();
            this.Save_ToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ValidateAllFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.retToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeBackupOfProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revertToBackupToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.revertToBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.RenameProject_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteProject_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.Find_Toolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.findPanel = new System.Windows.Forms.Panel();
            this.nextSearchResultButton = new System.Windows.Forms.Button();
            this.instanceCountLabel = new System.Windows.Forms.Label();
            this.findBox = new System.Windows.Forms.TextBox();
            this.findLabel = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.lastSelectedLabel = new System.Windows.Forms.Label();
            this.goUpButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.name_column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileViewContainer)).BeginInit();
            this.fileViewContainer.Panel1.SuspendLayout();
            this.fileViewContainer.Panel2.SuspendLayout();
            this.fileViewContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.findPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleSeperator
            // 
            // 
            // TitleLabel
            // 
            this.TitleLabel.Size = new System.Drawing.Size(67, 16);
            this.TitleLabel.Text = "JetViewer";
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.fileViewContainer);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Indigo;
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Purple;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ico3.ico");
            this.imageList1.Images.SetKeyName(1, "json-file_light.png");
            // 
            // fileViewContainer
            // 
            this.fileViewContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.fileViewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileViewContainer.Location = new System.Drawing.Point(0, 0);
            this.fileViewContainer.Name = "fileViewContainer";
            // 
            // fileViewContainer.Panel1
            // 
            this.fileViewContainer.Panel1.Controls.Add(this.toolStrip1);
            this.fileViewContainer.Panel1.Controls.Add(this.findPanel);
            this.fileViewContainer.Panel1.Controls.Add(this.treeView1);
            // 
            // fileViewContainer.Panel2
            // 
            this.fileViewContainer.Panel2.Controls.Add(this.lastSelectedLabel);
            this.fileViewContainer.Panel2.Controls.Add(this.goUpButton);
            this.fileViewContainer.Panel2.Controls.Add(this.saveButton);
            this.fileViewContainer.Panel2.Controls.Add(this.listView1);
            this.fileViewContainer.Size = new System.Drawing.Size(776, 406);
            this.fileViewContainer.SplitterDistance = 251;
            this.fileViewContainer.TabIndex = 2;
            this.fileViewContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_SplitterMoved);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(251, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.Save_ToolStrip,
            this.toolStripSeparator1,
            this.ValidateAllFiles,
            this.toolStripSeparator3,
            this.retToolStripMenuItem,
            this.revertToBackupToolStripMenuItem,
            this.toolStripSeparator2,
            this.RenameProject_Button,
            this.DeleteProject_Button});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(38, 22);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Open_Proj_Dir});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // Open_Proj_Dir
            // 
            this.Open_Proj_Dir.Name = "Open_Proj_Dir";
            this.Open_Proj_Dir.Size = new System.Drawing.Size(162, 22);
            this.Open_Proj_Dir.Text = "Project Directory";
            this.Open_Proj_Dir.Click += new System.EventHandler(this.Open_Proj_Dir_Click);
            // 
            // Save_ToolStrip
            // 
            this.Save_ToolStrip.Name = "Save_ToolStrip";
            this.Save_ToolStrip.Size = new System.Drawing.Size(158, 22);
            this.Save_ToolStrip.Text = "Save";
            this.Save_ToolStrip.Click += new System.EventHandler(this.Save_ToolStrip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(155, 6);
            // 
            // ValidateAllFiles
            // 
            this.ValidateAllFiles.Name = "ValidateAllFiles";
            this.ValidateAllFiles.Size = new System.Drawing.Size(158, 22);
            this.ValidateAllFiles.Text = "Validate All Files";
            this.ValidateAllFiles.Click += new System.EventHandler(this.ValidateAllFiles_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // retToolStripMenuItem
            // 
            this.retToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeBackupOfProjectToolStripMenuItem,
            this.revertToBackupToolStripMenuItem1});
            this.retToolStripMenuItem.Name = "retToolStripMenuItem";
            this.retToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.retToolStripMenuItem.Text = "Backup project";
            // 
            // makeBackupOfProjectToolStripMenuItem
            // 
            this.makeBackupOfProjectToolStripMenuItem.Name = "makeBackupOfProjectToolStripMenuItem";
            this.makeBackupOfProjectToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.makeBackupOfProjectToolStripMenuItem.Text = "Make backup of project";
            // 
            // revertToBackupToolStripMenuItem1
            // 
            this.revertToBackupToolStripMenuItem1.Name = "revertToBackupToolStripMenuItem1";
            this.revertToBackupToolStripMenuItem1.Size = new System.Drawing.Size(199, 22);
            this.revertToBackupToolStripMenuItem1.Text = "Revert to backup";
            // 
            // revertToBackupToolStripMenuItem
            // 
            this.revertToBackupToolStripMenuItem.Name = "revertToBackupToolStripMenuItem";
            this.revertToBackupToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.revertToBackupToolStripMenuItem.Text = "Remake Project";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // RenameProject_Button
            // 
            this.RenameProject_Button.Name = "RenameProject_Button";
            this.RenameProject_Button.Size = new System.Drawing.Size(158, 22);
            this.RenameProject_Button.Text = "Rename Project";
            this.RenameProject_Button.Click += new System.EventHandler(this.RenameProject_Button_Click);
            // 
            // DeleteProject_Button
            // 
            this.DeleteProject_Button.Name = "DeleteProject_Button";
            this.DeleteProject_Button.Size = new System.Drawing.Size(158, 22);
            this.DeleteProject_Button.Text = "Delete Project";
            this.DeleteProject_Button.Click += new System.EventHandler(this.DeleteProject_Button_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Find_Toolstrip});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(40, 22);
            this.toolStripDropDownButton2.Text = "Edit";
            // 
            // Find_Toolstrip
            // 
            this.Find_Toolstrip.Name = "Find_Toolstrip";
            this.Find_Toolstrip.Size = new System.Drawing.Size(97, 22);
            this.Find_Toolstrip.Text = "Find";
            this.Find_Toolstrip.Click += new System.EventHandler(this.Find_Toolstrip_Click);
            // 
            // findPanel
            // 
            this.findPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.findPanel.Controls.Add(this.nextSearchResultButton);
            this.findPanel.Controls.Add(this.instanceCountLabel);
            this.findPanel.Controls.Add(this.findBox);
            this.findPanel.Controls.Add(this.findLabel);
            this.findPanel.Location = new System.Drawing.Point(0, 349);
            this.findPanel.Name = "findPanel";
            this.findPanel.Size = new System.Drawing.Size(269, 58);
            this.findPanel.TabIndex = 1;
            this.findPanel.Visible = false;
            // 
            // nextSearchResultButton
            // 
            this.nextSearchResultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextSearchResultButton.Location = new System.Drawing.Point(3, 29);
            this.nextSearchResultButton.Name = "nextSearchResultButton";
            this.nextSearchResultButton.Size = new System.Drawing.Size(176, 23);
            this.nextSearchResultButton.TabIndex = 3;
            this.nextSearchResultButton.Text = "Next Result";
            this.nextSearchResultButton.UseVisualStyleBackColor = true;
            this.nextSearchResultButton.Click += new System.EventHandler(this.NextSearchResultButton_Click);
            // 
            // instanceCountLabel
            // 
            this.instanceCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instanceCountLabel.AutoSize = true;
            this.instanceCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instanceCountLabel.ForeColor = System.Drawing.Color.White;
            this.instanceCountLabel.Location = new System.Drawing.Point(185, 32);
            this.instanceCountLabel.Name = "instanceCountLabel";
            this.instanceCountLabel.Size = new System.Drawing.Size(78, 16);
            this.instanceCountLabel.TabIndex = 2;
            this.instanceCountLabel.Text = "Instances: 0";
            // 
            // findBox
            // 
            this.findBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.findBox.Location = new System.Drawing.Point(47, 3);
            this.findBox.Name = "findBox";
            this.findBox.Size = new System.Drawing.Size(219, 20);
            this.findBox.TabIndex = 1;
            this.findBox.TextChanged += new System.EventHandler(this.searchbox_textChanged);
            // 
            // findLabel
            // 
            this.findLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.findLabel.AutoSize = true;
            this.findLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findLabel.ForeColor = System.Drawing.Color.White;
            this.findLabel.Location = new System.Drawing.Point(3, 3);
            this.findLabel.Name = "findLabel";
            this.findLabel.Size = new System.Drawing.Size(44, 20);
            this.findLabel.TabIndex = 0;
            this.findLabel.Text = "Find:";
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ForeColor = System.Drawing.Color.White;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.ItemHeight = 24;
            this.treeView1.Location = new System.Drawing.Point(0, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(251, 380);
            this.treeView1.TabIndex = 0;
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_CheckHotkey);
            // 
            // lastSelectedLabel
            // 
            this.lastSelectedLabel.AutoSize = true;
            this.lastSelectedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastSelectedLabel.ForeColor = System.Drawing.Color.White;
            this.lastSelectedLabel.Location = new System.Drawing.Point(79, 3);
            this.lastSelectedLabel.Name = "lastSelectedLabel";
            this.lastSelectedLabel.Size = new System.Drawing.Size(110, 20);
            this.lastSelectedLabel.TabIndex = 3;
            this.lastSelectedLabel.Text = "Last Selection";
            // 
            // goUpButton
            // 
            this.goUpButton.Location = new System.Drawing.Point(2, 0);
            this.goUpButton.Name = "goUpButton";
            this.goUpButton.Size = new System.Drawing.Size(75, 25);
            this.goUpButton.TabIndex = 2;
            this.goUpButton.Text = "Up";
            this.goUpButton.UseVisualStyleBackColor = true;
            this.goUpButton.Click += new System.EventHandler(this.goUpButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.saveButton.Font = new System.Drawing.Font("Microsoft YaHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(3, 380);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(499, 27);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save .jet";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click_1);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name_column});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.Color.White;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(2, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(519, 377);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // name_column
            // 
            this.name_column.Text = "Name";
            this.name_column.Width = 517;
            // 
            // JetForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "JetViewer";
            this.Activated += new System.EventHandler(this.JetForm_Activated);
            this.Load += new System.EventHandler(this.JetForm_Load);
            this.Shown += new System.EventHandler(this.JetForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JetForm_KeyDown);
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.fileViewContainer.Panel1.ResumeLayout(false);
            this.fileViewContainer.Panel1.PerformLayout();
            this.fileViewContainer.Panel2.ResumeLayout(false);
            this.fileViewContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileViewContainer)).EndInit();
            this.fileViewContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.findPanel.ResumeLayout(false);
            this.findPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer fileViewContainer;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button goUpButton;
        private System.Windows.Forms.ColumnHeader name_column;
        private System.Windows.Forms.Panel findPanel;
        private System.Windows.Forms.TextBox findBox;
        private System.Windows.Forms.Label findLabel;
        private System.Windows.Forms.Label instanceCountLabel;
        private System.Windows.Forms.Button nextSearchResultButton;
        private System.Windows.Forms.Label lastSelectedLabel;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Open_Proj_Dir;
        private System.Windows.Forms.ToolStripMenuItem Save_ToolStrip;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem retToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeBackupOfProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem revertToBackupToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem revertToBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Find_Toolstrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem RenameProject_Button;
        private System.Windows.Forms.ToolStripMenuItem DeleteProject_Button;
        private System.Windows.Forms.ToolStripMenuItem ValidateAllFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}