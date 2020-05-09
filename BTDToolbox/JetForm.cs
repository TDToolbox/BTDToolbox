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
using BTDToolbox.Classes;
using BTDToolbox.Extra_Forms;
using BTDToolbox.Classes.NewProjects;
using BTDToolbox.Properties;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace BTDToolbox
{
    public partial class JetForm : ThemedForm
    {
        string livePath = Environment.CurrentDirectory;
        private DirectoryInfo dirInfo;
        private Main Form;
        private string tempName;
        public string projName;
        private ContextMenuStrip treeMenu;

        //Config values
        ConfigFile programData;
        public static float jetExplorer_FontSize;
        public static int jetExplorer_SplitterWidth;
        public static string lastProject;
        public static bool useExternalEditor;

        public JetForm(DirectoryInfo dirInfo, Main Form, string projName)
        {
            InitializeComponent();
            programData = DeserializeConfig();
            StartUp();

            goUpButton.Font = new Font("Microsoft Sans Serif", 9);
            this.dirInfo = dirInfo;
            this.Form = Form;
            this.projName = projName;
            Main.projName = dirInfo.FullName;
            this.DoubleBuffered = true;
            string gamedir = "";

            //MessageBox.Show(dirInfo.FullName);

            initTreeMenu();


            if (projName.Contains("BTD5"))
            {
                Main.gameName = "BTD5";
                gamedir = Serializer.Deserialize_Config().BTD5_Directory;
            }
            else if (projName.Contains("BTDB"))
            {
                Main.gameName = "BTDB";
                gamedir = Serializer.Deserialize_Config().BTDB_Directory;
            }
            else if (projName.Contains("BMC"))
            {
                Main.gameName = "BMC";
                gamedir = Serializer.Deserialize_Config().BMC_Directory;
            }

            if (gamedir == "" || gamedir == null)
                Main.getInstance().Launch_Program_ToolStrip.Visible = false;
            else
                Main.getInstance().Launch_Program_ToolStrip.Visible = true;

            ConsoleHandler.appendLog("Game: " + CurrentProjectVariables.GameName);
            ConsoleHandler.appendLog("Loading Project: " + projName.ToString());


            LoadProjectFile();
            Serializer.SaveConfig(this, "game");
            Serializer.SaveConfig(this, "jet explorer");

            if (EZBloon_Editor.EZBloon_Opened == true)
                ConsoleHandler.force_appendNotice("The EZ Bloon tool is currently opened for a different project. Please close it to avoid errors...");

            if (EasyTowerEditor.EZTower_Opened == true)
                ConsoleHandler.force_appendNotice("The EZ Tower tool is currently opened for a different project. Please close it to avoid errors...");

            if (EZCard_Editor.EZCard_Opened == true)
                ConsoleHandler.force_appendNotice("The EZ Card tool is currently opened for a different project. Please close it to avoid errors...");


            if (CurrentProjectVariables.JsonEditor_OpenedTabs != null && CurrentProjectVariables.JsonEditor_OpenedTabs.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to re-open your previous files?", "Reopen previous files?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    foreach (string tab in CurrentProjectVariables.JsonEditor_OpenedTabs)
                        JsonEditorHandler.OpenFile(tab);
                }
                else
                {
                    CurrentProjectVariables.JsonEditor_OpenedTabs = new List<string>();
                    ProjectHandler.SaveProject();
                }
            }
        }
        private void StartUp()
        {
            //config stuff
            this.Size = new Size(programData.JetExplorer_SizeX, programData.JetExplorer_SizeY);
            this.Location = new Point(programData.JetExplorer_PosX, programData.JetExplorer_PosY);

            this.Font = new Font("Microsoft Sans Serif", programData.JetExplorer_FontSize);
            lastProject = programData.LastProject;
            jetExplorer_SplitterWidth = programData.JetExplorer_SplitterWidth;
            fileViewContainer.SplitterDistance = jetExplorer_SplitterWidth;
            useExternalEditor = programData.useExternalEditor;
            
            //other setup
            this.KeyPreview = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            listView1.DoubleClick += ListView1_DoubleClicked;
            listView1.MouseUp += ListView1_RightClicked;
            treeView1.MouseUp += TreeView_RightClicked;
            this.treeView1.AfterSelect += treeView1_AfterSelect;
            this.FormClosed += exitHandling;
            this.FormClosing += this.JetForm_Closed;
        }
        private void LoadProjectFile()
        {
            string projClassFolder = "";
            string[] split = dirInfo.FullName.Split('\\');
            if (split[split.Length - 1] == split[split.Length - 2])
                projClassFolder = dirInfo.FullName.Remove(dirInfo.FullName.LastIndexOf('\\'), dirInfo.FullName.Length - dirInfo.FullName.LastIndexOf('\\'));
            else
                projClassFolder = dirInfo.FullName;


            var files = new DirectoryInfo(projClassFolder).GetFiles("*.toolbox");
            if (files.Count() == 1)
            {
                ProjectHandler.ReadProject(files[0].FullName);
            }
            else
            {
                ConsoleHandler.force_appendLog_CanRepeat("Something went wrong. Your project file wasnt found or you had more than one. ");
            }
            /*var dirs = new DirectoryInfo(Environment.CurrentDirectory + "\\Projects").GetDirectories();
            foreach (var dir in dirs)
            {
                if (dir.Name == projName)
                {
                    lastProject = dir.FullName;
                    ProjectHandler.ReadProject(dir.FullName + "\\" + projName + ".toolbox");

                    //Main.projName = dir.FullName + "\\" + projName;
                    Serializer.SaveConfig(this, "jet explorer");
                }
            }*/
        }
        private void initTreeMenu()
        {
            treeMenu = new ContextMenuStrip();
            treeMenu.Items.Add("Open in File Explorer");
            treeMenu.ItemClicked += treeContextClicked;
        }
        private void HandleNKH()
        {
            Main.getInstance().Launch_Program_ToolStrip.DropDownItems.Clear();

            if (CurrentProjectVariables.GameName == "BTD5")
            {
                if (NKHook.DoesNkhExist())
                {
                    Main.getInstance().Launch_Program_ToolStrip.DropDownItems.Add("With NKHook");
                    Main.getInstance().Launch_Program_ToolStrip.DropDownItems.Add("Without NKHook");
                }
            }
        }
        private void JetForm_Load(object sender, EventArgs e)
        {
            openDirWindow();
            JetProps.increment(this);
        }
        private void JetForm_Shown(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "jet explorer");
            HandleNKH();
        }
        public void openDirWindow()
        {
            tempName = dirInfo.FullName;
            this.Text = tempName;
            PopulateTreeView(null);
            return;
        }

        private void JetForm_Closed(object sender, EventArgs e)
        {
            JetProps.decrement(this);
            Serializer.SaveConfig(this, "jet explorer");
            Serializer.SaveSmallSettings("external editor");
        }

        private void PopulateTreeView(string expandedpath)
        {
            TreeNode rootNode;
            
            DirectoryInfo info = new DirectoryInfo(tempName);
            var dirs = info.GetDirectories();
            bool found = false;

            foreach(var dir in dirs)
            {
                if (dir.Name == projName)
                {
                    found = true;
                    break;
                }
            }
            if(found)
                info = new DirectoryInfo(tempName + "\\" + projName);

            if (info.Exists)
            {
                rootNode = new TreeNode(info.Name);
                if(expandedpath != null)
                {
                    if (expandedpath.Contains(info.Name))
                    {
                        rootNode.Expand();
                    }
                }
                rootNode.Tag = info;
                GetDirectories(info.GetDirectories(), rootNode, expandedpath);
                treeView1.Nodes.Add(rootNode);
            }

        }
        private void PopulateListView(TreeNode selectedTreeNode)
        {
            TreeNode newSelected = selectedTreeNode;
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
        private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo, string expandedPath)
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
                    GetDirectories(subSubDirs, aNode, expandedPath);
                }
                if (expandedPath != null)
                {
                    if (expandedPath.Contains(subDir.Name))
                    {
                        int index = expandedPath.IndexOf(subDir.Name);
                        if (index + subDir.Name.Length + 1 > expandedPath.Length)
                        {
                            PopulateListView(aNode);
                        }
                        aNode.Expand();
                    }
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            PopulateListView(e.Node);
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
        private void OpenFile(bool openFromZip)
        {
            if(listView1.SelectedItems.Count <= 0)
                ConsoleHandler.appendLog("You need to select at least one file to open");
            else
            {
                int i = 0;
                foreach (ListViewItem a in listView1.SelectedItems)
                {
                    var Selected = listView1.SelectedItems[i];
                    if (!Selected.Text.Contains("."))
                    {
                        foreach (TreeNode node in treeView1.SelectedNode.Nodes)
                        {
                            if (node.Text == Selected.Text)
                            {
                                node.Expand();
                                treeView1.SelectedNode = node;
                                break;
                            }
                        }
                        break;
                    }
                    else
                    {
                        if(!openFromZip)
                            JsonEditorHandler.OpenFile(this.Text + "\\" + Selected.Text);
                        else
                        {
                            string selected = (treeView1.SelectedNode.FullPath + "\\" + a.Text).Replace(CurrentProjectVariables.ProjectName + "\\", "");
                            JsonEditorHandler.OpenFileFromZip(selected);
                        }
                    }
                    i++;
                }
            }
        }
        private void ListView1_DoubleClicked(object sender, EventArgs e)
        {
            OpenFile(false);
        }

        //Context caller
        private void ListView1_RightClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(listView1.SelectedItems.Count <= 0)
                    NoneSelected_CM.Show(listView1, e.Location);
                else if (listView1.SelectedItems.Count == 1)
                {
                    OneSelected_CM.Show(listView1, e.Location);
                    string filename = listView1.SelectedItems[0].Text;

                    ezTower_CMButton.Visible = false;
                    ezBloon_CMButton.Visible = false;
                    ezCard_CMButton.Visible = false;
                    ezTools_Seperator.Visible = true;

                    if (filename.EndsWith(".tower"))
                        ezTower_CMButton.Visible = true;
                    else if (filename.EndsWith(".bloon"))
                        ezBloon_CMButton.Visible = true;
                    else if ((this.Text).Contains("BattleCardDefinitions"))
                        ezCard_CMButton.Visible = true;
                    else
                        ezTools_Seperator.Visible = false;
                }
                else
                    MultiSelected_CM.Show(listView1, e.Location);
            }
        }
        private void TreeView_RightClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                treeMenu.Show(treeView1, e.Location);
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

                if(newName.Length > 0)
                {
                    string source = currentPath + "\\" + select;
                    string dest = currentPath + "\\" + newName;

                    if(source.ToLower() == dest.ToLower())
                    {
                        ConsoleHandler.appendLog("The file names are too similar!");
                        return;
                    }

                    // get the file attributes for file or directory
                    FileAttributes attr = File.GetAttributes(source);

                    //detect whether its a directory or file
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                        Directory.Move(source, dest);
                    else
                        File.Move(source, dest);

                    Selected[0].Text = newName;
                }
                else
                {
                    ConsoleHandler.appendLog("You didn't enter a name");
                }
            }
        }
        private void delete()
        {
            if (MessageBox.Show("Are you sure you want to delete the selected item(s)?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
                foreach (ListViewItem item in Selected)
                {
                    if(item.Text.Contains("."))
                    {
                        string currentPath = this.Text;
                        string toDelete = currentPath + "\\" + item.Text;

                        File.Delete(toDelete);

                        item.Remove();
                    }
                    else
                    {
                        GeneralMethods.DeleteDirectory(this.Text + "\\" + item.Text);
                        string path = this.Text;
                        treeView1.Nodes.Clear();
                        listView1.Items.Clear();
                        PopulateTreeView(path);
                    }
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
                if (name.Contains("."))
                {
                    FileInfo info = new FileInfo(name);
                    string dest = targetDir + "\\" + info.Name;
                    if (File.Exists(targetDir + "\\" + info.Name))
                    {
                        int i = 1;
                        string[] split = dest.Split('.');
                        string noExtention = dest.Replace("." + split[split.Length - 1], "");
                        string copyName = noExtention + "_Copy ";

                        while (File.Exists(dest))
                        {
                            dest = copyName + i + "." + split[split.Length - 1];
                            i++;
                        }
                    }
                    File.Copy(name, dest);

                    string[] filename = dest.Split('\\');
                    ListViewItem item = new ListViewItem(filename[filename.Length - 1], 1);
                    ListViewItem.ListViewSubItem[] subItems = new ListViewItem.ListViewSubItem[]
                        {
                        new ListViewItem.ListViewSubItem(item, "File"),
                        new ListViewItem.ListViewSubItem(item, "null")
                        };
                    item.SubItems.AddRange(subItems);
                    listView1.Items.Add(item);
                }
                else
                {
                    DirectoryInfo info = new DirectoryInfo(name);
                    string dest = targetDir + "\\" + info.Name;
                    if (Directory.Exists(targetDir + "\\" + info.Name))
                    {
                        int i = 1;
                        
                        string copyName = "_Copy ";
                        string baseDest = dest;
                        while (Directory.Exists(dest))
                        {
                            dest = baseDest + copyName + i;
                            i++;
                        }
                    }
                    if (!Directory.Exists(dest))
                    {
                        Directory.CreateDirectory(dest);

                        string[] filename = dest.Split('\\');
                        ListViewItem item = new ListViewItem(filename[filename.Length - 1], 1);
                        listView1.Items.Add(item);
                    }

                    foreach (string dirPath in Directory.GetDirectories(name, "*",SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(name, dest));

                    //Copy all the files & Replaces any files with the same name
                    foreach (string newPath in Directory.GetFiles(name, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(name, dest), true);
                    }

                    string path = this.Text;
                    treeView1.Nodes.Clear();
                    listView1.Items.Clear();
                    PopulateTreeView(path);
                }
            }
        }
        private void selectAll()
        {
            if (listView1.Focused)
            {
                int i = 0;
                foreach (var a in listView1.Items)
                {
                    listView1.Items[i].Selected = true;
                    i++;
                }
            }
        }
        private void openInFileExplorer()
        {
            if (!listView1.SelectedItems[0].Text.Contains("."))
                Process.Start(this.Text + "\\" + listView1.SelectedItems[0].Text);
            else
                Process.Start(this.Text);
        }
        private void restoreSingleFile(string filepath, string filename)
        {
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            string backupPath = Environment.CurrentDirectory + "\\Backups\\" + CurrentProjectVariables.GameName + "_BackupProject\\" + filepath.Replace(CurrentProjectVariables.PathToProjectFiles, "").Replace("\\" + projName + "\\", "");

            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, filepath);

                if (JsonEditorHandler.jeditor == null)
                {
                    JsonEditorHandler.OpenFile(filepath);
                }
                else if (JsonEditorHandler.jeditor.tabFilePaths.Contains(filepath))
                {
                    JsonEditorHandler.CloseFile(filepath);
                    JsonEditorHandler.OpenFile(filepath);
                }
                ConsoleHandler.appendLog_CanRepeat(filename + "has been restored");
            }
            else
            {
                ConsoleHandler.appendLog_CanRepeat("Could not find " + filename + " in the backup, failed to restore file.");
            }
            
        }
        private void restoreOriginal()
        {
            int i = 0;
            foreach(var x in listView1.SelectedItems)
            {
                string filepath = this.Text + "\\" + listView1.SelectedItems[i].Text;
                if (listView1.SelectedItems[i].Text.Contains("."))
                {
                    restoreSingleFile(filepath, listView1.SelectedItems[i].Text);
                }
                else
                {
                    foreach (var a in Directory.GetFiles(filepath))
                    {
                        string[] split = a.Split('\\');
                        string filename = split[split.Length - 1];
                        string sourcefile = Environment.CurrentDirectory + "\\Backups\\" + Main.gameName + "_BackupProject\\" + filepath.Replace(Environment.CurrentDirectory, "").Replace("\\" + projName + "\\", "") + "\\" + filename;

                        restoreSingleFile(a, filename);
                    }
                }
                i++;
            }
        }
        private void Open_EZBloon()
        {
            string filename = listView1.SelectedItems[0].ToString().Replace("ListViewItem: {", "").Replace("}", "");
            var ezBloon = new EZBloon_Editor();
            string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions\\" + filename;
            ezBloon.path = path;
            ezBloon.Show();
        }
        private void Open_EZTower()
        {
            string filename = listView1.SelectedItems[0].Text;
            var ezTower = new EasyTowerEditor();
            string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\TowerDefinitions\\" + filename;
            ezTower.path = path;
            ezTower.Show();
        }
        private void Open_EZCard()
        {
            string filename = listView1.SelectedItems[0].ToString().Replace("ListViewItem: {", "").Replace("}", "");
            var ezCard = new EZCard_Editor();
            string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BattleCardDefinitions\\" + filename;
            ezCard.path = path;
            ezCard.Show();
        }
        private void ViewOriginal()
        {
            int i = 0;
            foreach (var a in listView1.SelectedItems)
            {
                JsonEditorHandler.OpenOriginalFile(this.Text + "\\" + listView1.SelectedItems[i].Text);
                i++;
            }
        }
        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            jetExplorer_SplitterWidth = fileViewContainer.SplitterDistance;
        }
        
        private void exitHandling(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "jet explorer");
        }

        private void JetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveJet();
            }
            if (e.KeyCode == Keys.Delete)
            {
                ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
                if (Selected.Count > 0)
                {
                    delete();
                }
            }
            if (e.KeyCode == Keys.F2)
            {
                ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
                if (Selected.Count == 1)
                {
                    rename();
                }
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
                if (Selected.Count > 0)
                {
                    copy();
                }
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                paste();
            }
            if (e.Control && e.KeyCode == Keys.F)
            {
                if (findPanel.Visible)
                {
                    findPanel.Hide();
                }
                else
                {
                    findPanel.Visible = true;
                    findBox.Select();
                }
            }
            if (e.Control && e.KeyCode == Keys.A)
            {
                selectAll();
            }
        }
        private void JetForm_Activated(object sender, EventArgs e)
        {
            LoadProjectFile();
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
            CompileJet("output");
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

        private void RenameProject_Button_Click(object sender, EventArgs e)
        {
            var setName = new SetProjectName();
            setName.isRenaming = true;
            setName.Show();
            setName.jetf = this;
        }
        public void RenameProject(string newProjName)    //Musts get name from SetProjectName Form first. It will call this func
        {
            if (!File.Exists(newProjName))
            {
                string oldName = projName;

                CopyDirectory(oldName, newProjName);
                DirectoryInfo dinfo = new DirectoryInfo(newProjName);

                JetForm jf = new JetForm(dinfo, Main.getInstance(), newProjName);
                jf.MdiParent = Main.getInstance();
                jf.Show();

                try
                {
                    foreach (JetForm o in JetProps.get())
                    {
                        if (o.projName == oldName)
                        {
                            o.Close();
                            DeleteDirectory(oldName);
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void DeleteProject_Button_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (JetForm o in JetProps.get())
                {
                    if (o.projName == CurrentProjectVariables.ProjectName)
                    {
                        o.Close();
                        DeleteDirectory(Environment.CurrentDirectory + "\\Projects\\" + CurrentProjectVariables.ProjectName);
                    }
                }
            }
            catch
            {

            }
            
        }

        private void ValidateAllFiles_Click(object sender, EventArgs e)
        {
            Thread bg = new Thread(JSON_Reader.ValidateAllJsonFiles);
            bg.Start();
        }


        //Context caller
        private void treeContextClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Open in File Explorer")
            {
                ConsoleHandler.appendLog("Opening folder in File Explorer..");
                Process.Start(this.Text);
            }
        }
        private void NoneSelected_CM_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Add")
                add();
            else if (e.ClickedItem.Text == "Paste")
                paste();
            else if (e.ClickedItem.Text == "Select all")
                selectAll();
        }
        private void OneSelected_CM_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Open File")
                OpenFile(false);
            else if (e.ClickedItem.Text == "Rename")
                rename();
            else if (e.ClickedItem.Text == "Delete")
                delete();
            else if (e.ClickedItem.Text == "Copy")
                copy();
            else if (e.ClickedItem.Text == "Open with EZ Bloon")
                Open_EZBloon();
            else if (e.ClickedItem.Text == "Open with EZ Tower")
                Open_EZTower();
            else if (e.ClickedItem.Text == "Open with EZ Card")
                Open_EZCard();
            else if (e.ClickedItem.Text == "Open in File Explorer")
                openInFileExplorer();
            else if (e.ClickedItem.Text == "View original")
                ViewOriginal();
            else if (e.ClickedItem.Text == "Restore to original")
            {
                DialogResult diag = MessageBox.Show("Are you sure you want to restore this file to the original unmodded version?", "Restore to original?", MessageBoxButtons.YesNo);
                if (diag == DialogResult.Yes)
                    restoreOriginal();
                else
                    ConsoleHandler.appendLog_CanRepeat("restore cancelled...");
            }
        }
        private void MultiSelected_CM_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Open Files")
                OpenFile(false);
            if (e.ClickedItem.Text == "Delete")
                delete();
            if (e.ClickedItem.Text == "Copy")
                copy();
            if (e.ClickedItem.Text == "View original")
                ViewOriginal();
            if (e.ClickedItem.Text == "Restore to original")
            {
                DialogResult diag = MessageBox.Show("Are you sure you want to restore this file to the original unmodded version?", "Restore to original?", MessageBoxButtons.YesNo);
                if (diag == DialogResult.Yes)
                    restoreOriginal();
                else
                    ConsoleHandler.appendLog_CanRepeat("restore cancelled...");
            }
        }


        //
        //Zip stuff
        //
        public void PopulateListview(TreeNode selectedTreeNode)
        {
            string jetpath = CurrentProjectVariables.PathToProjectClassFile + "\\" + CurrentProjectVariables.ProjectName + ".jet";
            string pass = CurrentProjectVariables.JetPassword;
            string selectedPath = selectedTreeNode.FullPath.Replace(CurrentProjectVariables.ProjectName + "\\", "").Replace("/", "\\");
            string[] split1 = selectedPath.Split('\\');
            List<string> files = new List<string>();

            ZipFile jet = new ZipFile(jetpath);
            jet.Password = pass;

            listView1.Items.Clear();
            foreach (var z in jet)
            {
                string name = z.FileName.Replace("/", "\\");
                if (name.EndsWith("\\"))
                    name = name.Remove(name.Length - 1);
                files.Add(name);
            }

            List<string> folderz = new List<string>();
            List<string> filez = new List<string>();
            foreach (string a in files)
            {
                if (a.Contains(selectedPath + "\\"))
                {
                    string[] split2 = a.Split('\\');
                    if (split2[split2.Length - 2] == split1[split1.Length - 1])
                    {
                        if (split2[split2.Length - 1].Contains("."))
                            filez.Add(split2[split2.Length - 1]);
                        else
                            folderz.Add(split2[split2.Length - 1]);
                    }
                }
            }

            foreach (string fold in folderz)
                listView1.Items.Add(fold, 0);
            foreach (string fil in filez)
                listView1.Items.Add(fil, 1);

        }
        public void PopulateTreeview()
        {
            string jetpath = CurrentProjectVariables.PathToProjectClassFile + "\\" + CurrentProjectVariables.ProjectName + ".jet";
            if (File.Exists(jetpath))
            {
                string pass = CurrentProjectVariables.JetPassword;

                if (pass != "" && pass != null)
                {
                    ZipFile jet = new ZipFile(jetpath);
                    jet.Password = pass;

                    string temp = CurrentProjectVariables.PathToProjectClassFile;
                    List<string> folders = new List<string>();
                    foreach (ZipEntry z in jet)
                    {
                        if (z.IsDirectory)
                        {
                            folders.Add(z.FileName);
                            z.ExtractWithPassword(temp, ExtractExistingFileAction.OverwriteSilently, CurrentProjectVariables.JetPassword);
                        }
                    }
                    ListDirectory(treeView1, CurrentProjectVariables.PathToProjectClassFile);

                    DirectoryInfo dir = new DirectoryInfo(temp);
                    foreach (var folder in dir.GetDirectories())
                    {
                        foreach (string f in folders)
                        {
                            try { folder.Delete(true); }
                            catch { }
                            break;
                        }

                    }
                }
                else
                {
                    ConsoleHandler.force_appendLog("Something went wrong and the saved password for this project is empty...");
                    if (CurrentProjectVariables.GameName != "" && CurrentProjectVariables.GameName != null)
                    {
                        if (CurrentProjectVariables.GameName == "BTD5" || CurrentProjectVariables.GameName == "BMC")
                        {
                            pass = "Q%_{6#Px]]";
                            CurrentProjectVariables.JetPassword = "Q%_{6#Px]]";
                            ProjectHandler.SaveProject();
                        }
                        else
                        {
                            var getPasss = new Get_BTDB_Password();
                            getPasss.Show();
                        }
                    }
                    else
                    {
                        ConsoleHandler.force_appendLog("Game name for project is invalid!");
                        ConsoleHandler.force_appendNotice("Please enter a password so the jet file can be read");

                        var getPasss = new Get_BTDB_Password();
                        getPasss.Show();
                    }
                }
            }
            else
            {
                ConsoleHandler.force_appendLog("Unable to find project file...");
            }
        }
        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }
        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Name != CurrentProjectVariables.ProjectName + ".jet" && file.Name != CurrentProjectVariables.ProjectName + ".toolbox")
                {
                    directoryNode.Nodes.Add(new TreeNode(file.Name));
                }
            }
            return directoryNode;
        }

        private void FindAllModifiedFles_Button_Click(object sender, EventArgs e)
        {
            FindModFiles();
        }
        private void FindModFiles()
        {
            DialogResult diag = MessageBox.Show("It will take up to 10 seconds to find all of the modified files" +
                   " in the project. Do you wish to continue?", "Do you wish to continue?", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                string backupfile = Environment.CurrentDirectory + "\\Backups\\" + CurrentProjectVariables.GameName + "_Original.jet";
                if (File.Exists(backupfile))
                {
                    ZipFile backup = new ZipFile(backupfile);
                    backup.Password = CurrentProjectVariables.JetPassword;
                    var files = new DirectoryInfo(CurrentProjectVariables.PathToProjectFiles).GetFiles("*", SearchOption.AllDirectories);

                    ConsoleHandler.force_appendLog("Searching for modified files");
                    foreach (var file in files)
                    {
                        string modText = RemoveWhitespace(File.ReadAllText(file.FullName));

                        string pathInZip = file.FullName.Replace(CurrentProjectVariables.PathToProjectFiles + "\\", "");
                        string originalText = RemoveWhitespace(ProjectHandler.ReadTextFromZipFile(backup, pathInZip));
                        try
                        {
                            if (modText == originalText)
                            {
                                if (CurrentProjectVariables.ModifiedFiles.Contains(file.FullName))
                                {
                                    CurrentProjectVariables.ModifiedFiles.Remove(file.FullName);
                                }
                            }
                            else
                            {
                                if (!CurrentProjectVariables.ModifiedFiles.Contains(file.FullName))
                                {
                                    CurrentProjectVariables.ModifiedFiles.Add(file.FullName);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            //It broke for some reason, probably something modded here...
                            if (!CurrentProjectVariables.ModifiedFiles.Contains(file.FullName))
                            {
                                ConsoleHandler.appendLog_CanRepeat(ex.Message);
                                CurrentProjectVariables.ModifiedFiles.Add(file.FullName);

                            }
                        }

                    }
                    ProjectHandler.SaveProject();
                    ConsoleHandler.force_appendLog("Finished verifying files");


                    if (CurrentProjectVariables.ModifiedFiles.Count > 0)
                    {
                        bool open = false;
                        DialogResult diaga = MessageBox.Show("Modified files were found. Would you like toolbox to open them all? If you press no, " +
                            "the console will still display the names of all the modified files. \nYou can also find them " +
                            "under \"View modified files\" in the Jet Viewer's \"File\" tab", "Open all modified files?", MessageBoxButtons.YesNo);
                        if (diaga == DialogResult.Yes)
                        {
                            open = true;
                        }

                        foreach (var a in CurrentProjectVariables.ModifiedFiles)
                        {
                            ConsoleHandler.force_appendLog_CanRepeat(a.Replace(CurrentProjectVariables.PathToProjectFiles + "\\", ""));
                            if (open == true)
                            {
                                JsonEditorHandler.OpenFile(a);
                            }
                        }
                    }
                    else
                        ConsoleHandler.force_appendLog("No modified files found..");
                }
                else
                    ConsoleHandler.force_appendLog("Backup not detected... Unable to continue...");
            }
            else
                ConsoleHandler.appendLog("User cancelled operation...");
        }
        private void ViewModifiedFiles_Button_MouseHover(object sender, EventArgs e)
        {
            PopulateModFiles();
        }
        private void PopulateModFiles()
        { 
            ViewModifiedFiles_Button.DropDownItems.Clear();
            ViewModifiedFiles_Button.DropDownItems.Add("Find all modified files");
            ViewModifiedFiles_Button.DropDownItems.Add(new ToolStripSeparator());


            foreach (var a in CurrentProjectVariables.ModifiedFiles)
            {
                ViewModifiedFiles_Button.DropDownItems.Add(a.Replace(CurrentProjectVariables.PathToProjectFiles + "\\",""));
            }
        }
        private void ViewModifiedFiles_Button_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Text == "Find all modified files")
            {
                FindModFiles();
            }
            else if (e.ClickedItem.Text.Length > 0)
            {
                JsonEditorHandler.OpenFile(CurrentProjectVariables.PathToProjectFiles + "\\" + e.ClickedItem.Text);
            }
        }
        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }
        private void ViewModifiedFiles_Button_Click(object sender, EventArgs e)
        {
            PopulateModFiles();
        }
    }
}
