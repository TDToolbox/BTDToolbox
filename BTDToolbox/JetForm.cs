using Ionic.Zip;
using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfig;
using static System.Windows.Forms.ToolStripItem;
using static BTDToolbox.GeneralMethods;

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
        ConfigFile programData;
        public static float jetExplorer_FontSize;
        public static int jetExplorer_SplitterWidth;
        public static string lastProject;

        public JetForm(DirectoryInfo dirInfo, TD_Toolbox_Window Form, string projName)
        {
            InitializeComponent();
            StartUp();

            this.dirInfo = dirInfo;
            this.Form = Form;
            this.projName = projName;
            
            initMultiContextMenu();
            initSelContextMenu();
            initEmpContextMenu();
            
            if(projName.Contains("BTD5"))
            {
                TD_Toolbox_Window.gameName = "BTD5";
            }
            else if (projName.Contains("BTDB"))
            {
                TD_Toolbox_Window.gameName = "BTDB";
            }
            ConsoleHandler.appendLog("Game: " + TD_Toolbox_Window.gameName);
            ConsoleHandler.appendLog("Loading Project: " + projName.ToString());
            Serializer.SaveConfig(this, "game", programData);
            
            //Serializer.SaveConfig(this, "jet explorer", programData);
        }
        private void Deserialize_Config()
        {
            programData = DeserializeConfig();
            //programData = Serializer.Deserialize_Config();
        }
        private void StartUp()
        {
            //config stuff
            Deserialize_Config();
            this.Size = new Size(programData.JetExplorer_SizeX, programData.JetExplorer_SizeY);
            this.Location = new Point(programData.JetExplorer_PosX, programData.JetExplorer_PosY);

            this.Font = new Font("Microsoft Sans Serif", programData.JetExplorer_FontSize);
            lastProject = programData.LastProject;
            jetExplorer_SplitterWidth = programData.JetExplorer_SplitterWidth;
            fileViewContainer.SplitterDistance = jetExplorer_SplitterWidth;
            
            //other setup
            this.KeyPreview = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            listView1.DoubleClick += ListView1_DoubleClicked;
            listView1.MouseUp += ListView1_RightClicked;
            this.treeView1.AfterSelect += treeView1_AfterSelect;
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
            Serializer.SaveConfig(this, "jet explorer", programData);
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
                if (current.Text != projName)
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
                    JsonEditor JsonWindow = new JsonEditor(this.Text + "\\" + Selected[0].Text);
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
            jetExplorer_SplitterWidth = fileViewContainer.SplitterDistance;
        }
        
        private void exitHandling(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "jet explorer", programData);
        }

        private void JetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveJet();
            }
        }

        private void TreeView_CheckHotkey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (findPanel.Visible)
                {
                    findPanel.Visible = false;
                }
                else
                {
                    findPanel.Visible = true;
                    findBox.Select();
                }
            }
        }


        private void JetForm_Activated(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "jet explorer", programData);
        }


        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private string LastSearchText;
        private int LastNodeIndex = 0;
        private int selected;
        private void searchbox_textChanged(object sender, EventArgs e)
        {
            string searchText = findBox.Text;
            selected = 0;
            if (String.IsNullOrEmpty(searchText))
            {
                return;
            };
            if (LastSearchText != searchText)
            {
                //It's a new Search
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, treeView1.Nodes[0]);
            }
            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex++;
                this.treeView1.SelectedNode = selectedNode;
                this.treeView1.SelectedNode.Expand();
                this.treeView1.Select();
            }
            instanceCountLabel.Text = "Instances: " + CurrentNodeMatches.Count;
            findBox.Select();
        }
        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                }
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search 
                }
                StartNode = StartNode.NextNode;
            }
        }

        private void NextSearchResultButton_Click(object sender, EventArgs e)
        {
            try
            {
                selected++;
                treeView1.SelectedNode = CurrentNodeMatches[selected];
            }
            catch (ArgumentOutOfRangeException)
            {
                selected = 0;
                treeView1.SelectedNode = CurrentNodeMatches[selected];
            }
        }

        private void SaveButton_Click_1(object sender, EventArgs e)
        {
            saveJet();
        }
        private void saveJet()
        {
            /*if (JsonEditor.jsonError != true)
            {
                if (JetProps.get().Count == 1)
                {
                    ExtractingJet_Window.currentProject = projName;
                    ExtractingJet_Window.switchCase = "output";
                    if (programData.CurrentGame == "BTDB")
                        ExtractingJet_Window.switchCase = "output BTDB";
                    var compile = new ExtractingJet_Window();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("ERROR!!! There is a JSON Error in this file!!!\n\nIf you leave the file now it will be corrupted and WILL break your mod. Do you still want to leave?", "ARE YOU SURE!!!!!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (JetProps.get().Count == 1)
                    {
                        ExtractingJet_Window.currentProject = projName;
                        ExtractingJet_Window.switchCase = "output";
                        if (programData.CurrentGame == "BTDB")
                            ExtractingJet_Window.switchCase = "output BTDB";
                        var compile = new ExtractingJet_Window();
                    }
                }
            }*/
            
        }

        private void Open_Proj_Dir_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Opening project directory...");
            Process.Start(DeserializeConfig().LastProject);
        }

        private void Save_ToolStrip_Click(object sender, EventArgs e)
        {
            saveJet();
        }

        private void Find_Toolstrip_Click(object sender, EventArgs e)
        {
            if (findPanel.Visible)
            {
                findPanel.Visible = false;
            }
            else
            {
                findPanel.Visible = true;
                findBox.Select();
            }
        }
    }
}
