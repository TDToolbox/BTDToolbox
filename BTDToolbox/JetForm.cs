using Ionic.Zip;
using System;
using System.IO;
using System.Windows.Forms;

namespace BTDToolbox
{
    public partial class JetForm : Form
    {
        private String filePath;
        private string tempName;

        public JetForm(String filePath)
        {
            InitializeComponent();
            this.filePath = filePath;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void JetForm_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            ZipFile archive = new ZipFile(filePath);
            archive.Password = "Q%_{6#Px]]";
            string livePath = Environment.CurrentDirectory;
            tempName = (livePath + "\\temp_" + rand.Next());
            this.Text = tempName;
            MessageBox.Show("Click 'Ok' to create temp files, this can take up to 30 seconds.");
            archive.ExtractAll(tempName);
            PopulateTreeView();
        }
        private void JetForm_Closed(object sender, EventArgs e)
        {
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
    }
}
