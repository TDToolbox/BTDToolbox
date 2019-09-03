﻿using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class JetForm : Form
    {
        private String filePath;
        private DirectoryInfo dirInfo;
        private TD_Toolbox_Window Form;
        private string tempName;

        public JetForm(String filePath, TD_Toolbox_Window Form)
        {
            InitializeComponent();
            this.FormClosing += this.JetForm_Closed;
            this.filePath = filePath;
            this.Form = Form;
            //.OfType<ListView.>().FirstOrDefault().BackColor = Color.Black;
        }
        public JetForm(DirectoryInfo dirInfo, TD_Toolbox_Window Form)
        {
            InitializeComponent();
            this.FormClosing += this.JetForm_Closed;
            this.dirInfo = dirInfo;
            this.Form = Form;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void JetForm_Load(object sender, EventArgs e)
        {
            if (filePath != null)
            {
                openJetWindow();
            }
            else if (dirInfo != null)
            {
                openDirWindow();
            }
        }

        public void openDirWindow()
        {
            tempName = dirInfo.FullName;
            this.Text = tempName;
            PopulateTreeView();
            return;
        }

        public void openJetWindow()
        {
            Random rand = new Random();
            ZipFile archive = new ZipFile(filePath);
            archive.Password = "Q%_{6#Px]]";
            ConsoleHandler.appendLog("Creating project files...");
            if (MessageBox.Show("Click 'Ok' to create project files, this can take up to 2 minutes.", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string livePath = Environment.CurrentDirectory;
                tempName = (livePath + "\\proj_" + rand.Next());
                this.Text = tempName;
                archive.ExtractAll(tempName);
                ConsoleHandler.appendLog("Project files created at: " + tempName);
                PopulateTreeView();
                return;
            }
            ConsoleHandler.appendLog("Project files creation canceled");
        }

        private void JetForm_Closed(object sender, EventArgs e)
        {
            
        }

        private void PopulateTreeView()
        {
            TreeNode rootNode;

            DirectoryInfo info = new DirectoryInfo(tempName);
            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode);
                treeView1.Nodes.Add(rootNode);
            }
        }
        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            DirectoryInfo[] subSubDirs;
            foreach (DirectoryInfo subDir in subDirs)
            {
                aNode = new TreeNode(subDir.Name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetDirectories();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = (DirectoryInfo)newSelected.Tag;
            this.Text = nodeDirInfo.FullName;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                {
                    new ListViewItem.ListViewSubItem(item, "Directory"), new ListViewItem.ListViewSubItem(item, dir.LastAccessTime.ToShortDateString())
                };
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, "File"),
                        new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())
                    };

                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDiag = new SaveFileDialog();
            fileDiag.Title = "Save .jet";
            fileDiag.DefaultExt = "jet";
            fileDiag.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            if (fileDiag.ShowDialog() == DialogResult.OK)
            {
                ZipFile toExport = new ZipFile();
                toExport.Password = "Q%_{6#Px]]";
                toExport.AddDirectory(tempName);
                toExport.Encryption = EncryptionAlgorithm.PkzipWeak;
                toExport.Name = fileDiag.FileName;
                toExport.CompressionLevel = CompressionLevel.Level6;
                toExport.Save();
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
            if (Selected.Count == 1)
            {
                try
                {
                    JsonEditor JsonWindow = new JsonEditor(this.Text + "\\" + Selected[0].Text);
                    JsonWindow.MdiParent = Form;
                    JsonWindow.Show();
                } catch (Exception)
                {
                }
            }
        }
    }
}
