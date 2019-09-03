using Ionic.Zip;
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
        private TD_Toolbox_Window Form;
        private string tempName;

        public JetForm(String filePath, TD_Toolbox_Window Form)
        {
            InitializeComponent();
            this.FormClosing += this.JetForm_Closed;
            this.filePath = filePath;
            this.Form = Form;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void JetForm_Load(object sender, EventArgs e)
        {
            refreshWindow();
        }

        public void refreshWindow()
        {
            Random rand = new Random();
            ZipFile archive = new ZipFile(filePath);
            archive.Password = "Q%_{6#Px]]";
            ConsoleHandler.appendLog("Creating temp files...");
            if (MessageBox.Show("Click 'Ok' to create temp files, this can take up to 2 minutes.", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string livePath = Environment.CurrentDirectory;
                tempName = (livePath + "\\temp_" + rand.Next());
                this.Text = tempName;
                archive.ExtractAll(tempName);
                ConsoleHandler.appendLog("Temp files created at: " + tempName);
                PopulateTreeView();
                return;
            }
            ConsoleHandler.appendLog("Temp files creation canceled");
        }

        private void JetForm_Closed(object sender, EventArgs e)
        {
            try
            {
                ConsoleHandler.appendLog("Deleting temp files...");
                DirectoryInfo temp = new DirectoryInfo(tempName);
                foreach (FileInfo file in temp.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in temp.GetDirectories())
                {
                    dir.Delete(true);
                }
                temp.Delete();
                ConsoleHandler.appendLog("Deleting temp files deleted!");
            } catch (Exception) { }
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection selected = listView1.SelectedItems;
            if(selected.Count == 1)
            {
                ConsoleHandler.appendLog(this.Text + "\\" + selected[0].Text);
            }
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
            if (Selected.Count == 1)
            {
                JsonEditor JsonWindow = new JsonEditor(this.Text + "\\" + Selected[0].Text);
                JsonWindow.MdiParent = Form;
                JsonWindow.Show();
            }
        }
    }
}
