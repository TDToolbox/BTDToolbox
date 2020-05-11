using BTDToolbox.Classes.NewProjects;
using System.Drawing;

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
            this.ViewModifiedFiles_Button = new System.Windows.Forms.ToolStripMenuItem();
            this.FindAllModifiedFles_Button = new System.Windows.Forms.ToolStripMenuItem();
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
            this.OneSelected_CM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ezTools_Seperator = new System.Windows.Forms.ToolStripSeparator();
            this.ezTower_CMButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ezBloon_CMButton = new System.Windows.Forms.ToolStripMenuItem();
            this.ezCard_CMButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.viewOriginal_CMButton = new System.Windows.Forms.ToolStripMenuItem();
            this.RestoreToOriginal_CMButton = new System.Windows.Forms.ToolStripMenuItem();
            this.NoneSelected_CM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MultiSelected_CM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.viewOriginalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToOriginalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_RightCorner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_LeftCorner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fileViewContainer)).BeginInit();
            this.fileViewContainer.Panel1.SuspendLayout();
            this.fileViewContainer.Panel2.SuspendLayout();
            this.fileViewContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.findPanel.SuspendLayout();
            this.OneSelected_CM.SuspendLayout();
            this.NoneSelected_CM.SuspendLayout();
            this.MultiSelected_CM.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleSeperator
            // 
            // 
            // TitleLabel
            // 
            this.TitleLabel.Size = new System.Drawing.Size(97, 16);
            this.TitleLabel.Text = "JetViewer:    |    ";
            // 
            // contentPanel
            // 
            this.contentPanel.Controls.Add(this.fileViewContainer);
            // 
            // close_button
            // 
            this.close_button.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.close_button.FlatAppearance.BorderSize = 0;
            this.close_button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.close_button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            // 
            // TitleBar_RightCorner
            // 
            this.TitleBar_RightCorner.Location = new System.Drawing.Point(740, 0);
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
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
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
            this.ViewModifiedFiles_Button,
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
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
            this.Save_ToolStrip.Size = new System.Drawing.Size(176, 22);
            this.Save_ToolStrip.Text = "Save";
            this.Save_ToolStrip.Click += new System.EventHandler(this.Save_ToolStrip_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // ValidateAllFiles
            // 
            this.ValidateAllFiles.Name = "ValidateAllFiles";
            this.ValidateAllFiles.Size = new System.Drawing.Size(176, 22);
            this.ValidateAllFiles.Text = "Validate All Files";
            this.ValidateAllFiles.Click += new System.EventHandler(this.ValidateAllFiles_Click);
            // 
            // ViewModifiedFiles_Button
            // 
            this.ViewModifiedFiles_Button.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FindAllModifiedFles_Button});
            this.ViewModifiedFiles_Button.Name = "ViewModifiedFiles_Button";
            this.ViewModifiedFiles_Button.Size = new System.Drawing.Size(176, 22);
            this.ViewModifiedFiles_Button.Text = "View Modified Files";
            this.ViewModifiedFiles_Button.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ViewModifiedFiles_Button_DropDownItemClicked);
            this.ViewModifiedFiles_Button.Click += new System.EventHandler(this.ViewModifiedFiles_Button_Click);
            this.ViewModifiedFiles_Button.MouseHover += new System.EventHandler(this.ViewModifiedFiles_Button_MouseHover);
            // 
            // FindAllModifiedFles_Button
            // 
            this.FindAllModifiedFles_Button.Name = "FindAllModifiedFles_Button";
            this.FindAllModifiedFles_Button.Size = new System.Drawing.Size(187, 22);
            this.FindAllModifiedFles_Button.Text = "Find all modified files";
            this.FindAllModifiedFles_Button.Click += new System.EventHandler(this.FindAllModifiedFles_Button_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(173, 6);
            // 
            // retToolStripMenuItem
            // 
            this.retToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeBackupOfProjectToolStripMenuItem,
            this.revertToBackupToolStripMenuItem1});
            this.retToolStripMenuItem.Name = "retToolStripMenuItem";
            this.retToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.retToolStripMenuItem.Text = "Backup project";
            this.retToolStripMenuItem.Visible = false;
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
            this.revertToBackupToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.revertToBackupToolStripMenuItem.Text = "Remake Project";
            this.revertToBackupToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // RenameProject_Button
            // 
            this.RenameProject_Button.Name = "RenameProject_Button";
            this.RenameProject_Button.Size = new System.Drawing.Size(176, 22);
            this.RenameProject_Button.Text = "Rename Project";
            this.RenameProject_Button.Click += new System.EventHandler(this.RenameProject_Button_Click);
            // 
            // DeleteProject_Button
            // 
            this.DeleteProject_Button.Name = "DeleteProject_Button";
            this.DeleteProject_Button.Size = new System.Drawing.Size(176, 22);
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
            this.treeView1.Size = new System.Drawing.Size(251, 377);
            this.treeView1.TabIndex = 0;
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
            this.goUpButton.Font = new System.Drawing.Font("Microsoft New Tai Lue", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.goUpButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
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
            this.saveButton.Size = new System.Drawing.Size(515, 27);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save .jet";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click_1);
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
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
            this.listView1.Size = new System.Drawing.Size(519, 351);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // name_column
            // 
            this.name_column.Text = "Name";
            this.name_column.Width = 180;
            // 
            // OneSelected_CM
            // 
            this.OneSelected_CM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.toolStripMenuItem4,
            this.toolStripSeparator6,
            this.copyToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.ezTools_Seperator,
            this.ezTower_CMButton,
            this.ezBloon_CMButton,
            this.ezCard_CMButton,
            this.toolStripSeparator4,
            this.viewOriginal_CMButton,
            this.RestoreToOriginal_CMButton});
            this.OneSelected_CM.Name = "OneSelected_CM";
            this.OneSelected_CM.Size = new System.Drawing.Size(184, 242);
            this.OneSelected_CM.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OneSelected_CM_ItemClicked);
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(183, 22);
            this.toolStripMenuItem4.Text = "Open in File Explorer";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(180, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // ezTools_Seperator
            // 
            this.ezTools_Seperator.Name = "ezTools_Seperator";
            this.ezTools_Seperator.Size = new System.Drawing.Size(180, 6);
            // 
            // ezTower_CMButton
            // 
            this.ezTower_CMButton.Name = "ezTower_CMButton";
            this.ezTower_CMButton.Size = new System.Drawing.Size(183, 22);
            this.ezTower_CMButton.Text = "Open with EZ Tower";
            this.ezTower_CMButton.Visible = false;
            // 
            // ezBloon_CMButton
            // 
            this.ezBloon_CMButton.Name = "ezBloon_CMButton";
            this.ezBloon_CMButton.Size = new System.Drawing.Size(183, 22);
            this.ezBloon_CMButton.Text = "Open with EZ Bloon";
            this.ezBloon_CMButton.Visible = false;
            // 
            // ezCard_CMButton
            // 
            this.ezCard_CMButton.Name = "ezCard_CMButton";
            this.ezCard_CMButton.Size = new System.Drawing.Size(183, 22);
            this.ezCard_CMButton.Text = "Open with EZ Card";
            this.ezCard_CMButton.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(180, 6);
            // 
            // viewOriginal_CMButton
            // 
            this.viewOriginal_CMButton.Name = "viewOriginal_CMButton";
            this.viewOriginal_CMButton.Size = new System.Drawing.Size(183, 22);
            this.viewOriginal_CMButton.Text = "View original";
            // 
            // RestoreToOriginal_CMButton
            // 
            this.RestoreToOriginal_CMButton.Name = "RestoreToOriginal_CMButton";
            this.RestoreToOriginal_CMButton.Size = new System.Drawing.Size(183, 22);
            this.RestoreToOriginal_CMButton.Text = "Restore to original";
            // 
            // NoneSelected_CM
            // 
            this.NoneSelected_CM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.selectAllToolStripMenuItem});
            this.NoneSelected_CM.Name = "NoneSelected_CM";
            this.NoneSelected_CM.Size = new System.Drawing.Size(121, 70);
            this.NoneSelected_CM.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.NoneSelected_CM_ItemClicked);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            // 
            // MultiSelected_CM
            // 
            this.MultiSelected_CM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFilesToolStripMenuItem,
            this.copyToolStripMenuItem1,
            this.deleteToolStripMenuItem1,
            this.toolStripSeparator7,
            this.viewOriginalToolStripMenuItem1,
            this.restoreToOriginalToolStripMenuItem1});
            this.MultiSelected_CM.Name = "MultiSelected_CM";
            this.MultiSelected_CM.Size = new System.Drawing.Size(171, 120);
            this.MultiSelected_CM.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MultiSelected_CM_ItemClicked);
            // 
            // openFilesToolStripMenuItem
            // 
            this.openFilesToolStripMenuItem.Name = "openFilesToolStripMenuItem";
            this.openFilesToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.openFilesToolStripMenuItem.Text = "Open Files";
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.copyToolStripMenuItem1.Text = "Copy";
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            this.deleteToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.deleteToolStripMenuItem1.Text = "Delete";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(167, 6);
            // 
            // viewOriginalToolStripMenuItem1
            // 
            this.viewOriginalToolStripMenuItem1.Name = "viewOriginalToolStripMenuItem1";
            this.viewOriginalToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.viewOriginalToolStripMenuItem1.Text = "View original";
            // 
            // restoreToOriginalToolStripMenuItem1
            // 
            this.restoreToOriginalToolStripMenuItem1.Name = "restoreToOriginalToolStripMenuItem1";
            this.restoreToOriginalToolStripMenuItem1.Size = new System.Drawing.Size(170, 22);
            this.restoreToOriginalToolStripMenuItem1.Text = "Restore to original";
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
            this.Controls.SetChildIndex(this.titleSeperator, 0);
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_RightCorner)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TitleBar_LeftCorner)).EndInit();
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
            this.OneSelected_CM.ResumeLayout(false);
            this.NoneSelected_CM.ResumeLayout(false);
            this.MultiSelected_CM.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip OneSelected_CM;
        private System.Windows.Forms.ContextMenuStrip NoneSelected_CM;
        private System.Windows.Forms.ContextMenuStrip MultiSelected_CM;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ezTools_Seperator;
        private System.Windows.Forms.ToolStripMenuItem ezTower_CMButton;
        private System.Windows.Forms.ToolStripMenuItem ezBloon_CMButton;
        private System.Windows.Forms.ToolStripMenuItem ezCard_CMButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem viewOriginal_CMButton;
        private System.Windows.Forms.ToolStripMenuItem RestoreToOriginal_CMButton;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem viewOriginalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem restoreToOriginalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ViewModifiedFiles_Button;
        private System.Windows.Forms.ToolStripMenuItem FindAllModifiedFles_Button;
    }
}