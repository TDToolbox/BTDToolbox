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
            this.findPanel = new System.Windows.Forms.Panel();
            this.nextSearchResultButton = new System.Windows.Forms.Button();
            this.instanceCountLabel = new System.Windows.Forms.Label();
            this.findBox = new System.Windows.Forms.TextBox();
            this.findLabel = new System.Windows.Forms.Label();
            this.lastSelectedLabel = new System.Windows.Forms.Label();
            this.goUpButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.name_column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            this.contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileViewContainer)).BeginInit();
            this.fileViewContainer.Panel1.SuspendLayout();
            this.fileViewContainer.Panel2.SuspendLayout();
            this.fileViewContainer.SuspendLayout();
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
            this.goUpButton.Location = new System.Drawing.Point(2, 3);
            this.goUpButton.Name = "goUpButton";
            this.goUpButton.Size = new System.Drawing.Size(75, 23);
            this.goUpButton.TabIndex = 2;
            this.goUpButton.Text = "Up";
            this.goUpButton.UseVisualStyleBackColor = true;
            this.goUpButton.Click += new System.EventHandler(this.goUpButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(0, 383);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(503, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save .jet";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click_1);
            this.saveButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.saveButton_Click);
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
            this.listView1.Size = new System.Drawing.Size(503, 383);
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
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(251, 407);
            this.treeView1.TabIndex = 0;
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeView_CheckHotkey);
            // 
            // JetForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JetForm";
            this.Text = "JetViewer";
            this.Activated += new System.EventHandler(this.JetForm_Activated);
            this.Load += new System.EventHandler(this.JetForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JetForm_KeyDown);
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.fileViewContainer.Panel1.ResumeLayout(false);
            this.fileViewContainer.Panel2.ResumeLayout(false);
            this.fileViewContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileViewContainer)).EndInit();
            this.fileViewContainer.ResumeLayout(false);
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
    }
}