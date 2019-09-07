using Ionic.Zip;
using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfigs;
using static System.Windows.Forms.ToolStripItem;

namespace BTDToolbox
{
    public partial class JetForm : ThemedForm
    {
        string livePath = Environment.CurrentDirectory;

        private DirectoryInfo dirInfo;
        private TD_Toolbox_Window Form;
        private string tempName;
        public string projName;
        private ContextMenuStrip selMenu;
        private ContextMenuStrip empMenu;
        private ContextMenuStrip multiSelMenu;

        //Config values
        public static float jetFormFontSize;
        string jetFormOutput;
        int splitterDistance;
        float treeViewFontSize;
        JetExplorer jetExplorerConfig;
        public static bool jetImportCancelled;

        public JetForm(DirectoryInfo dirInfo, TD_Toolbox_Window Form, string projName)
        {
            InitializeComponent();
            
            listView1.DoubleClick += ListView1_DoubleClicked;
            listView1.MouseUp += ListView1_RightClicked;

            this.dirInfo = dirInfo;
            this.Form = Form;
            this.projName = projName;
            ConsoleHandler.appendLog(projName);

            initMultiContextMenu();
            initSelContextMenu();
            initEmpContextMenu();

            try
            {
                string json = File.ReadAllText(livePath + "\\config\\jetForm.json");
                JetExplorer deserializedJetForm = JsonConvert.DeserializeObject<JetExplorer>(json);

                Size JetFormSize = new Size(deserializedJetForm.SizeX, deserializedJetForm.SizeY);
                this.Size = JetFormSize;

                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(deserializedJetForm.PosX, deserializedJetForm.PosY);

                Font jetFormFontSize = new Font("Microsoft Sans Serif", deserializedJetForm.FontSize);
                this.Font = jetFormFontSize;

                splitterDistance = deserializedJetForm.SplitterDistance;
                fileViewContainer.SplitterDistance = splitterDistance;
                treeViewFontSize = deserializedJetForm.TreeViewFontSize;
            } catch (System.IO.FileNotFoundException)
            {
                SerializeConfig();
            }
            catch (System.ArgumentException)
            {
                jetFormFontSize = 10;
            }

            this.treeView1.AfterSelect += treeView1_AfterSelect;

            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.FormClosed += exitHandling;
            this.FormClosing += this.JetForm_Closed;
        }
        
        private void initSelContextMenu()
        {
            selMenu = new ContextMenuStrip();
            selMenu.Items.Add("Rename");
            selMenu.Items.Add("Delete");
            selMenu.Items.Add("Copy");
            selMenu.Items.Add("Restore original");
            selMenu.ItemClicked += jsonContextClicked;
        }
        private void initMultiContextMenu()
        {
            multiSelMenu = new ContextMenuStrip();
            multiSelMenu.Items.Add("Delete");
            multiSelMenu.Items.Add("Copy");
            multiSelMenu.ItemClicked += multiJsonContextClicked;
        }
        private void initEmpContextMenu()
        {
            empMenu = new ContextMenuStrip();
            empMenu.Items.Add("Add");
            empMenu.Items.Add("Paste");
            empMenu.ItemClicked += listContextClicked;
        }

        private void JetForm_Load(object sender, EventArgs e)
        {
            openDirWindow();
            JetProps.increment(this);
        }

        public void openDirWindow()
        {
            tempName = dirInfo.FullName;
            this.Text = tempName;
            PopulateTreeView();
            return;
        }

        private void JetForm_Closed(object sender, EventArgs e)
        {
            JetProps.decrement(this);
            SerializeConfig();
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

            try
            {
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
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Project was desynced and can no longer be used");
                this.Close();
            }
        }

        private void goUpButton_Click(object sender, EventArgs e)
        {
            TreeNode current = treeView1.SelectedNode;
            try
            {
                if(current.Text != projName)
                {
                    treeView1.SelectedNode = current.Parent;
                }
            }
            catch (NullReferenceException)
            {
            }
        }
        private void ListView1_DoubleClicked(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
            if (Selected.Count == 1)
            {
                try
                {
                    string targetPath = this.Text + "\\" + Selected[0].Text;
                    foreach (JsonEditor jedit in JsonProps.get())
                    {
                        if (jedit.Path == targetPath)
                        {
                            jedit.BringToFront();
                            return;
                        }
                    }
                    JsonEditor JsonWindow = new JsonEditor(targetPath);
                    JsonWindow.MdiParent = Form;
                    JsonWindow.Show();
                }
                catch (Exception)
                {
                    try
                    {
                        if (!Selected[0].Text.Contains("."))
                        {
                            foreach (TreeNode node in treeView1.SelectedNode.Nodes)
                            {
                                if (node.Text == Selected[0].Text)
                                {
                                    node.Expand();
                                    treeView1.SelectedNode = node;
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDiag = new SaveFileDialog();
            fileDiag.Title = "Save .jet";
            fileDiag.DefaultExt = "jet";
            fileDiag.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            if (fileDiag.ShowDialog() == DialogResult.OK)
            {
                JetCompiler.compile(dirInfo, fileDiag.FileName);
            }
        }

        //Context caller
        private void ListView1_RightClicked(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
                    if (Selected.Count == 1)
                    {
                        selMenu.Show(listView1, e.Location);
                    }
                    else if (Selected.Count == 0 || Selected == null)
                    {

                        empMenu.Show(listView1, e.Location);
                    }
                    else if (Selected.Count > 1)
                    {
                        multiSelMenu.Show(listView1, e.Location);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        //Context caller
        private void jsonContextClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Rename")
            {
                try
                {
                    rename();
                }
                catch (Exception)
                {
                }
            }
            if (e.ClickedItem.Text == "Delete")
            {
                try
                {
                    delete();
                }
                catch (Exception)
                {
                }
            }
            if (e.ClickedItem.Text == "Copy")
            {
                try
                {
                    copy();
                }
                catch (Exception)
                {
                }
            }
        }
        private void listContextClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Add")
            {
                try
                {
                    add();
                }
                catch (Exception)
                {
                }
            }
            if (e.ClickedItem.Text == "Paste")
            {
                try
                {
                    paste();
                }
                catch (Exception)
                {
                }
            }
        }
        private void multiJsonContextClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Delete")
            {
                try
                {
                    delete();
                }
                catch (Exception)
                {
                }
            }
            if (e.ClickedItem.Text == "Copy")
            {
                try
                {
                    copy();
                }
                catch (Exception)
                {
                }
            }
        }

        //Context menu methods
        private void add()
        {
            string targetDir = this.Text;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = "Select items to add";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string name in ofd.FileNames)
                {
                    FileInfo info = new FileInfo(name);
                    File.Copy(name, targetDir + "\\" + info.Name);
                }
                foreach (string safeName in ofd.SafeFileNames)
                {
                    ListViewItem item = new ListViewItem(safeName, 1);
                    ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[]
                        {
                        new ListViewItem.ListViewSubItem(item, "File"),
                        new ListViewItem.ListViewSubItem(item, "null")
                        };
                    item.SubItems.AddRange(subItems);
                    listView1.Items.Add(item);
                }
            }
        }
        private void rename()
        {
            ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
            if (Selected.Count == 1)
            {
                string select = Selected[0].Text;

                int posX = Screen.PrimaryScreen.Bounds.X / 2;
                int posY = Screen.PrimaryScreen.Bounds.Y / 2;

                string currentPath = this.Text;
                string newName = Microsoft.VisualBasic.Interaction.InputBox("Input new file name", "Rename file", select, posX, posY);

                string source = currentPath + "\\" + select;
                string dest = currentPath + "\\" + newName;

                File.Move(source, dest);

                Selected[0].Text = newName;
            }
        }
        private void delete()
        {
            if (MessageBox.Show("Are you sure you want to delete the selected item(s)?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
                foreach (ListViewItem item in Selected)
                {
                    string currentPath = this.Text;
                    string toDelete = currentPath + "\\" + item.Text;

                    File.Delete(toDelete);

                    item.Remove();
                }
            }
        }
        private void copy()
        {
            ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
            StringCollection pathCollection = new StringCollection();
            foreach (ListViewItem item in Selected)
            {
                string currentPath = this.Text;
                string toCopy = currentPath + "\\" + item.Text;

                pathCollection.Add(toCopy);
            }
            Clipboard.SetFileDropList(pathCollection);
        }
        private void paste()
        {
            string targetDir = this.Text;
            StringCollection files = Clipboard.GetFileDropList();
            foreach (string name in files)
            {
                FileInfo info = new FileInfo(name);
                File.Copy(name, targetDir + "\\" + info.Name);

                ListViewItem item = new ListViewItem(info.Name, 1);
                ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[]
                    {
                        new ListViewItem.ListViewSubItem(item, "File"),
                        new ListViewItem.ListViewSubItem(item, "null")
                    };
                item.SubItems.AddRange(subItems);
                listView1.Items.Add(item);
            }
        }

        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitterDistance = fileViewContainer.SplitterDistance;
        }
        private void SerializeConfig()
        {
            jetExplorerConfig = new JetExplorer("Jet Form", this.Size.Width, this.Size.Height, this.Location.X, this.Location.Y, 10, splitterDistance, this.treeView1.Font.Size);
            jetFormOutput = JsonConvert.SerializeObject(jetExplorerConfig);

            StreamWriter writeConsoleForm = new StreamWriter(livePath + "\\config\\jetForm.json", false);
            writeConsoleForm.Write(jetFormOutput);
            writeConsoleForm.Close();
        }
        private void exitHandling(object sender, EventArgs e)
        {
            SerializeConfig();
        }

        private void JetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (JetProps.get().Count == 1)
                {
                    Launcher.launchGame(JetProps.getForm(0));
                }
                else if (JetProps.get().Count < 1)
                {
                    MessageBox.Show("You have no .jets or projects open, you need one to launch.");
                }
                else
                {
                    MessageBox.Show("You have multiple .jets or projects open, only one can be launched.");
                }
            }
        }

        private void SplitContainer1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (JetProps.get().Count == 1)
                {
                    Launcher.launchGame(JetProps.getForm(0));
                }
                else if (JetProps.get().Count < 1)
                {
                    MessageBox.Show("You have no .jets or projects open, you need one to launch.");
                }
                else
                {
                    MessageBox.Show("You have multiple .jets or projects open, only one can be launched.");
                }
            }
        }
    }
}
