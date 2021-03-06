﻿using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static BTDToolbox.GeneralMethods;
using BTDToolbox.Classes;
using BTDToolbox.Extra_Forms;
using BTDToolbox.Classes.NewProjects;

namespace BTDToolbox
{
    public partial class JetForm : ThemedForm
    {
        public DirectoryInfo dirInfo;
        private string tempName;
        public string projName;
        private ContextMenuStrip treeMenu;

        //Config values
        public static float jetExplorer_FontSize;
        public static int jetExplorer_SplitterWidth;

        public JetForm(DirectoryInfo dirInfo, string projName)
        {
            InitializeComponent();

            this.MdiParent = Main.getInstance();
            this.dirInfo = dirInfo;
            this.projName = projName;
            LoadProjectFile();
            StartUp();


            Serializer.cfg.LastProject = dirInfo.FullName;
            Serializer.SaveSettings();


            if (!Guard.IsStringValid(CurrentProjectVariables.GamePath))
                Main.getInstance().Launch_Program_ToolStrip.Visible = false;
            else
                Main.getInstance().Launch_Program_ToolStrip.Visible = true;


            this.TitleLabel.Text = "JetViewer:    |    " + projName;
            ConsoleHandler.append("Game: " + CurrentProjectVariables.GameName);
            ConsoleHandler.append("Loading Project: " + projName);


            if (CurrentProjectVariables.JsonEditor_OpenedTabs != null && CurrentProjectVariables.JsonEditor_OpenedTabs.Count > 0)
                ASkReopenPreviousFiles();

            initTreeMenu();
            IsEZEditorOpen();
        }

        private void IsEZEditorOpen()
        {
            if (EZBloon_Editor.EZBloon_Opened == true)
                ConsoleHandler.force_append_Notice("The EZ Bloon tool is currently opened for a different project. Please close it to avoid errors...");

            if (EasyTowerEditor.EZTower_Opened == true)
                ConsoleHandler.force_append_Notice("The EZ Tower tool is currently opened for a different project. Please close it to avoid errors...");

            if (EZCard_Editor.EZCard_Opened == true)
                ConsoleHandler.force_append_Notice("The EZ Card tool is currently opened for a different project. Please close it to avoid errors...");
        }

        private void ASkReopenPreviousFiles()
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

        private void StartUp()
        {
            //config stuff
            this.Size = new Size(Serializer.cfg.JetExplorer_SizeX, Serializer.cfg.JetExplorer_SizeY);
            this.Location = new Point(Serializer.cfg.JetExplorer_PosX, Serializer.cfg.JetExplorer_PosY);

            this.Font = new Font("Microsoft Sans Serif", Serializer.cfg.JetExplorer_FontSize);
            jetExplorer_SplitterWidth = Serializer.cfg.JetExplorer_SplitterWidth;
            fileViewContainer.SplitterDistance = jetExplorer_SplitterWidth;
            
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
            if (files.Count() <= 0)
            {
                ConsoleHandler.append_Force_CanRepeat("Unable to find a .toolbox file in your project directory");
                return;
            }
            else if (files.Count() > 1)
            {
                ConsoleHandler.append_Force_CanRepeat("You have more than one .toolbox file in your project directory. Unable to read project data");
                return;
            }    
            
            ProjectHandler.ReadProject(files[0].FullName);
        }
        private void initTreeMenu()
        {
            treeMenu = new ContextMenuStrip();
            treeMenu.Items.Add("Open in File Explorer");
            treeMenu.ItemClicked += treeContextClicked;
        }
        private void HandleNKH()
        {
            Main.getInstance().PopulateNKHMewnu();
            if(CurrentProjectVariables.GameName == "BTD5")
            {
                showNKHMessage();
            }
        }
        private void showNKHMessage()
        {
            if (Serializer.cfg.nkhookMsgShown == false)
            {
                NKHook_Message msg = new NKHook_Message();
                msg.Show();
            }
        }

        private void JetForm_Load(object sender, EventArgs e)
        {
            openDirWindow();
            JetProps.increment(this);
        }
        private void JetForm_Shown(object sender, EventArgs e)
        {
            Serializer.SaveSettings();

            if (NKHook.CanUseNKH())
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
            
            Serializer.SaveSettings();
            ConsoleHandler.append("Closing JetViewer for " + projName);
            JetProps.decrement(this);

            
            if (JsonEditorHandler.jeditor == null) return;

            if (JsonEditorHandler.jeditor.userControls == null) return;

            var removeUserControls = new List<JsonEditor_Instance>();
            foreach (var tab in JsonEditorHandler.jeditor.userControls)
            {
                if (tab.path.Contains(projName.Replace("\\", "")))
                    removeUserControls.Add(tab);
            }

            if (removeUserControls.Count == 0)
                return;

            ConsoleHandler.append("This project still has files opened. Closing opened files...");
            foreach (var tab in removeUserControls)
            {
                ConsoleHandler.append("Closing:  " + tab.path.Replace(dirInfo.FullName, ""));
                JsonEditorHandler.CloseFile(tab.path, true);
            }
        }

        public void PopulateTreeView(string expandedpath)
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
                ConsoleHandler.append("You need to select at least one file to open");
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
                    ConsoleHandler.append_CanRepeat(info.FullName + "  was added to the project");
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
                        ConsoleHandler.append("The file names are too similar!");
                        return;
                    }

                    ConsoleHandler.append_CanRepeat("Renaming:  " + select + "  to:  " + newName);
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
                    ConsoleHandler.append("You didn't enter a name");
                }
            }
        }
        private void delete()
        {
            ConsoleHandler.append_CanRepeat("Are you sure you want to delete the selected item(s)?");
            if (MessageBox.Show("Are you sure you want to delete the selected item(s)?", "Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ListView.SelectedListViewItemCollection Selected = listView1.SelectedItems;
                foreach (ListViewItem item in Selected)
                {
                    ConsoleHandler.append_CanRepeat("Deleting:  " + item);
                    if (item.Text.Contains("."))
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
                ConsoleHandler.append_CanRepeat(toCopy.Replace(CurrentProjectVariables.PathToProjectFiles,"") + " was copied and added to the clipboard");
            }
            Clipboard.SetFileDropList(pathCollection);
        }
        private void paste()
        {
            string targetDir = this.Text;
            StringCollection files = Clipboard.GetFileDropList();
            
            foreach (string name in files)
            {
                ConsoleHandler.append_CanRepeat("Pasting  " + name.Replace(CurrentProjectVariables.PathToProjectFiles, "") + "  to  " + targetDir.Replace(CurrentProjectVariables.PathToProjectFiles, ""));
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
            FileInfo f = new FileInfo(filepath);
            string backupPath = Environment.CurrentDirectory + "\\Backups\\" + CurrentProjectVariables.GameName + "_BackupProject\\" + f.FullName.Replace(CurrentProjectVariables.PathToProjectFiles.Replace("\\\\", "\\"), "");
            if (!File.Exists(backupPath))
            {
                ConsoleHandler.append_CanRepeat("Could not find " + filename + " in the backup, failed to restore file.");
                return;
            }

            if (File.Exists(filepath))
                File.Delete(filepath);

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
            ConsoleHandler.append_CanRepeat(filename + "has been restored");
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
                        string sourcefile = Environment.CurrentDirectory + "\\Backups\\" + CurrentProjectVariables.GameName + "_BackupProject\\" + filepath.Replace(Environment.CurrentDirectory, "").Replace("\\" + projName + "\\", "") + "\\" + filename;

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
            string path = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\BloonDefinitions\\" + filename;
            ezBloon.path = path;
            ezBloon.Show();
        }
        private void Open_EZTower()
        {
            string filename = listView1.SelectedItems[0].Text;
            var ezTower = new EasyTowerEditor();
            string path = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\" + filename;
            ezTower.path = path;
            ezTower.Show();
        }
        private void Open_EZCard()
        {
            string filename = listView1.SelectedItems[0].ToString().Replace("ListViewItem: {", "").Replace("}", "");
            var ezCard = new EZCard_Editor();
            string path = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\BattleCardDefinitions\\" + filename;
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
            Serializer.SaveSettings();
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
            this.TitleLabel.Text = "JetViewer:    |    " + CurrentProjectVariables.ProjectName;
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
            ConsoleHandler.append("Opening project directory...");
            Process.Start(Serializer.cfg.LastProject);
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
            new SetProjectName()
            {
                isRenaming = true,
                jetf = this
            };
        }
        public void RenameProject(string newProjName, string newProjPath)    //Musts get name from SetProjectName Form first. It will call this func
        {
            string oldName = CurrentProjectVariables.PathToProjectFiles;
            if(!Guard.IsStringValid(oldName) || !File.Exists(CurrentProjectVariables.PathToProjectClassFile + "\\" + CurrentProjectVariables.ProjectName + ".toolbox"))
            {
                ConsoleHandler.append("Unable to rename project. The .toolbox file for the original project wasn't found");
                return;
            }
            if (Directory.Exists(newProjPath))
            {
                ConsoleHandler.force_append_Notice_CanRepeat("Another project already has that name. Pick a new name and try again");
                return;
            }

            ConsoleHandler.append("Renaming project...");
            CopyDirectory(oldName, newProjPath + "\\" + newProjName);
            DirectoryInfo dinfo = new DirectoryInfo(newProjPath);

            if(!Directory.Exists(newProjPath + "\\" + newProjName))
            {
                ConsoleHandler.append("Rename project failed. failed to copy files to their new location");
                return;
            }

            bool success = false;
            foreach (JetForm o in JetProps.get())
            {
                if (o.dirInfo.FullName.Trim() == oldName.Trim())
                {
                    o.Close();
                    success = true;
                    break;
                }
            }

            if (!success)
                ConsoleHandler.append("Failed to close original project. Please manually close it");
            
            if(!File.Exists(CurrentProjectVariables.PathToProjectClassFile + "\\" + CurrentProjectVariables.ProjectName + ".toolbox"))
            {
                ConsoleHandler.append("Original .toolbox file doesn't exist");
                return;
            }

            
            File.Copy(CurrentProjectVariables.PathToProjectClassFile + "\\" + CurrentProjectVariables.ProjectName + ".toolbox", newProjPath + "\\" +newProjName +".toolbox");
            Directory.Delete(CurrentProjectVariables.PathToProjectClassFile, true);

            CurrentProjectVariables.PathToProjectClassFile = newProjPath;
            CurrentProjectVariables.ProjectName = newProjName;
            CurrentProjectVariables.PathToProjectFiles = newProjPath + "\\" + newProjName;
            ProjectHandler.SaveProject();

            JetForm jf = new JetForm(dinfo, newProjName);
            jf.Show();
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
                        DeleteDirectory(CurrentProjectVariables.PathToProjectFiles);
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
                ConsoleHandler.append("Opening folder in File Explorer..");
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
                    ConsoleHandler.append_CanRepeat("restore cancelled...");
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
                    ConsoleHandler.append_CanRepeat("restore cancelled...");
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
                    ConsoleHandler.append_Force("Something went wrong and the saved password for this project is empty...");
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
                        ConsoleHandler.append_Force("Game name for project is invalid!");
                        ConsoleHandler.force_append_Notice("Please enter a password so the jet file can be read");

                        var getPasss = new Get_BTDB_Password();
                        getPasss.Show();
                    }
                }
            }
            else
            {
                ConsoleHandler.append_Force("Unable to find project file...");
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

                    ConsoleHandler.append_Force("Searching for modified files");
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
                                ConsoleHandler.append_CanRepeat(ex.Message);
                                CurrentProjectVariables.ModifiedFiles.Add(file.FullName);

                            }
                        }

                    }
                    ProjectHandler.SaveProject();
                    ConsoleHandler.append_Force("Finished verifying files");


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
                            ConsoleHandler.append_Force_CanRepeat(a.Replace(CurrentProjectVariables.PathToProjectFiles + "\\", ""));
                            if (open == true)
                            {
                                JsonEditorHandler.OpenFile(a);
                            }
                        }
                    }
                    else
                        ConsoleHandler.append_Force("No modified files found..");
                }
                else
                    ConsoleHandler.append_Force("Backup not detected... Unable to continue...");
            }
            else
                ConsoleHandler.append("User cancelled operation...");
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
