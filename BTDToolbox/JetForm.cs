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

            if (projName == Serializer.Deserialize_Config().LastProject)
            {
                if (Serializer.Deserialize_Config().JsonEditor_OpenedTabs.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to re-open your previous files?", "Reopen previous files?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        foreach (string tab in Serializer.Deserialize_Config().JsonEditor_OpenedTabs)
                            JsonEditorHandler.OpenFile(tab);
                    }
                    else
                    {
                        programData.JsonEditor_OpenedTabs = new List<string>();
                        Serializer.SaveJSONEditor_Tabs();
                    }
                }
            }
            else
            {
                programData.JsonEditor_OpenedTabs =  new List<string>();
                Serializer.SaveJSONEditor_Tabs();
            }

            goUpButton.Font = new Font("Microsoft Sans Serif", 9);
            this.dirInfo = dirInfo;
            this.Form = Form;
            this.projName = projName;
            Main.projName = projName;
            this.DoubleBuffered = true;
            string gamedir = "";

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
            if (gamedir == "" || gamedir == null)
            {
                Main.getInstance().Launch_Program_ToolStrip.Visible = false;
            }
            else
            {
                Main.getInstance().Launch_Program_ToolStrip.Visible = true;
            }
            ConsoleHandler.appendLog("Game: " + Main.gameName);
            ConsoleHandler.appendLog("Loading Project: " + projName.ToString());
            

            

            Serializer.SaveConfig(this, "game");
            Serializer.SaveConfig(this, "jet explorer");

            if (EZBloon_Editor.EZBloon_Opened == true)
                ConsoleHandler.force_appendNotice("The EZ Bloon tool is currently opened for a different project. Please close it to avoid errors...");

            if (EasyTowerEditor.EZTower_Opened == true)
                ConsoleHandler.force_appendNotice("The EZ Tower tool is currently opened for a different project. Please close it to avoid errors...");

            if (EZCard_Editor.EZCard_Opened == true)
                ConsoleHandler.force_appendNotice("The EZ Card tool is currently opened for a different project. Please close it to avoid errors...");
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
                    var files = jet.EntryFileNames;
                    string firstDirPath = "";
                    //DirectoryInfo d;
                    List<string> zipFiles = new List<string>();
                    foreach (ZipEntry z in jet)
                    {
                        if (z.IsDirectory)
                        {
                            //lets only get the first dir
                            //firstDirPath = jetpath + "\\" + z.FileName;
                            zipFiles.Add(z.FileName);
                            treeView1.Nodes.Add(z.FileName);
                            /*d = new DirectoryInfo(z.FileName);
                            foreach (var f in d.GetFiles("*", SearchOption.AllDirectories))
                            {
                                ConsoleHandler.appendLog_CanRepeat(f.FullName);
                            }*/
                        }
                    }
                    ConsoleHandler.appendLog_CanRepeat("Done");
                    /*DirectoryInfo d = new DirectoryInfo(firstDirPath);
                    foreach (var f in d.GetFiles("*", SearchOption.AllDirectories))
                    {
                        ConsoleHandler.appendLog_CanRepeat(f.FullName);
                    }*/
                    //ListDirectory(treeView1, firstDirPath);
                    /*DirectoryInfo d = new DirectoryInfo(jet.);
                    ListDirectory(treeView1,);*/

                    /*foreach (var file in files)
                    {
                        //treeView1.Nodes.Add(file);
                        
                    }*/

                    //ReadJetProject();
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
        public void ReadJetProject()
        {
            
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
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            return directoryNode;
        }















        private void initTreeMenu()
        {
            treeMenu = new ContextMenuStrip();
            treeMenu.Items.Add("Open in File Explorer");
            treeMenu.ItemClicked += treeContextClicked;
        }
        private void JetForm_Load(object sender, EventArgs e)
        {
            openDirWindow();
            JetProps.increment(this);
        }
        private void JetForm_Shown(object sender, EventArgs e)
        {
            Serializer.SaveConfig(this, "jet explorer");
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
            Serializer.SaveConfig(this, "jet explorer");
            Serializer.SaveSmallSettings("external editor");
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
        private void OpenFile()
        {
            if(listView1.SelectedItems.Count <= 0)
                ConsoleHandler.appendLog("You need to select at least one file to open");
            else
            {
                int i = 0;
                foreach (var a in listView1.SelectedItems)
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
                        JsonEditorHandler.OpenFile(this.Text + "\\" + Selected.Text);
                    }
                    i++;
                }
            }
        }
        private void ListView1_DoubleClicked(object sender, EventArgs e)
        {
            OpenFile();
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
                        treeView1.Nodes.Clear();
                        listView1.Items.Clear();
                        PopulateTreeView();
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

                    treeView1.Nodes.Clear();
                    listView1.Items.Clear();
                    PopulateTreeView();
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
            File.Copy(Environment.CurrentDirectory + "\\Backups\\" + Main.gameName + "_BackupProject\\" + filepath.Replace(Environment.CurrentDirectory, "").Replace("\\" + projName + "\\", ""), filepath);

            if (JsonEditorHandler.jeditor.tabFilePaths.Contains(filepath))
            {
                JsonEditorHandler.CloseFile(filepath);
                JsonEditorHandler.OpenFile(filepath);
            }
            ConsoleHandler.appendLog_CanRepeat(filename + "has been restored");
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
            Serializer.SaveConfig(this, "jet explorer");
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
                    if (o.projName == this.projName)
                    {
                        o.Close();
                        DeleteDirectory(this.projName);
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
                OpenFile();
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
                OpenFile();
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
    }
}
