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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.goUpButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.name_column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type_column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time_modified_column = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).BeginInit();
            this.titleSeperator.Panel1.SuspendLayout();
            this.titleSeperator.Panel2.SuspendLayout();
            this.titleSeperator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileViewContainer)).BeginInit();
            this.fileViewContainer.Panel1.SuspendLayout();
            this.fileViewContainer.Panel2.SuspendLayout();
            this.fileViewContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.Size = new System.Drawing.Size(67, 16);
            this.TitleLabel.Text = "JetViewer";
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
            this.imageList1.Images.SetKeyName(1, "jeteditor.ico");
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
            this.fileViewContainer.Panel1.Controls.Add(this.treeView1);
            // 
            // fileViewContainer.Panel2
            // 
            this.fileViewContainer.Panel2.Controls.Add(this.goUpButton);
            this.fileViewContainer.Panel2.Controls.Add(this.saveButton);
            this.fileViewContainer.Panel2.Controls.Add(this.listView1);
            this.fileViewContainer.Size = new System.Drawing.Size(775, 605);
            this.fileViewContainer.SplitterDistance = 253;
            this.fileViewContainer.TabIndex = 2;
            this.fileViewContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer_SplitterMoved);
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
            this.treeView1.Size = new System.Drawing.Size(253, 606);
            this.treeView1.TabIndex = 0;
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
            this.saveButton.Location = new System.Drawing.Point(0, 582);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(518, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save .jet";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name_column,
            this.type_column,
            this.time_modified_column});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.Color.White;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(2, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(518, 582);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // name_column
            // 
            this.name_column.Text = "Name";
            this.name_column.Width = 384;
            // 
            // type_column
            // 
            this.type_column.Text = "Type";
            this.type_column.Width = 63;
            // 
            // time_modified_column
            // 
            this.time_modified_column.Text = "Last Modified";
            this.time_modified_column.Width = 80;
            // 
            // JetForm
            // 
            this.contentPanel.Controls.Add(this.fileViewContainer);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JetForm";
            this.Text = "JetViewer";
            this.Load += new System.EventHandler(this.JetForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JetForm_KeyDown);
            this.Controls.SetChildIndex(this.titleSeperator, 0);
            this.titleSeperator.Panel1.ResumeLayout(false);
            this.titleSeperator.Panel1.PerformLayout();
            this.titleSeperator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.titleSeperator)).EndInit();
            this.titleSeperator.ResumeLayout(false);
            this.fileViewContainer.Panel1.ResumeLayout(false);
            this.fileViewContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileViewContainer)).EndInit();
            this.fileViewContainer.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer fileViewContainer;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader name_column;
        private System.Windows.Forms.ColumnHeader type_column;
        private System.Windows.Forms.ColumnHeader time_modified_column;
        private System.Windows.Forms.Button goUpButton;
    }
}