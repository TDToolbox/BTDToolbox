using Ionic.Zip;
using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfigs;
using static System.Windows.Forms.ToolStripItem;

namespace BTDToolbox
{
    public partial class JetForm : Form
    {
    
        string livePath = Environment.CurrentDirectory;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        //for resizing
        int Mx;
        int My;
        int Sw;
        int Sh;
        bool mov;

        //Resize defaults
        int minWidth = 200;
        int minHeight = 100;

        private String filePath;
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
        int treeViewX;
        int treeViewY;
        int splitterDistance;
        float treeViewFontSize;
        JetExplorer jetExplorerConfig;

        public JetForm(String filePath, TD_Toolbox_Window Form, string projName)
        {
            InitializeComponent();
            splitContainer1.Panel1.MouseMove += ToolbarDrag;
            splitContainer1.Panel2.MouseMove += ToolbarDrag;
            splitContainer1.MouseMove += ToolbarDrag;
            
            this.FormClosing += this.JetForm_Closed;
            listView1.DoubleClick += ListView1_DoubleClicked;
            listView1.MouseUp += ListView1_RightClicked;

            Sizer.MouseDown += SizerMouseDown;
            Sizer.MouseMove += SizerMouseMove;
            Sizer.MouseUp += SizerMouseUp;

            this.filePath = filePath;
            this.Form = Form;
            this.projName = projName;
            ConsoleHandler.appendLog(projName);

            initMultiContextMenu();
            initSelContextMenu();
            initEmpContextMenu();
            
            this.treeView1.AfterSelect += treeView1_AfterSelect;

            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public JetForm(DirectoryInfo dirInfo, TD_Toolbox_Window Form, string projName)
        {
            InitializeComponent();
            splitContainer1.Panel1.MouseMove += ToolbarDrag;
            splitContainer1.Panel2.MouseMove += ToolbarDrag;
            splitContainer1.MouseMove += ToolbarDrag;
            
            this.FormClosing += this.JetForm_Closed;
            listView1.DoubleClick += ListView1_DoubleClicked;
            listView1.MouseUp += ListView1_RightClicked;

            Sizer.MouseDown += SizerMouseDown;
            Sizer.MouseMove += SizerMouseMove;
            Sizer.MouseUp += SizerMouseUp;

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
                splitContainer.SplitterDistance = splitterDistance;
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
            if (filePath != null)
            {
                openJetWindow();
            }
            else if (dirInfo != null)
            {
                openDirWindow();
            }
            JetProps.increment(this);
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
            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
            DirectoryInfo extract = JetCompiler.decompile(filePath, dir);
            this.tempName = extract.FullName;
            dirInfo = extract;
            this.projName = extract.Name;
            PopulateTreeView();
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
                if (!Selected[0].Text.Contains("."))
                {
                    try
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
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    foreach(JsonEditor je in JsonProps.jsonformlist)
                    {
                        if(je.Text==Selected[0].Text)
                        {
                            je.BringToFront();
                            return;
                        }
                    }
                    try
                    {
                        JsonEditor JsonWindow = new JsonEditor(this.Text + "\\" + Selected[0].Text);
                        JsonWindow.MdiParent = Form;
                        JsonWindow.Show();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private void SizerMouseDown(object sender, MouseEventArgs e)
        {
            mov = true;
            My = MousePosition.Y;
            Mx = MousePosition.X;
            Sw = Width;
            Sh = Height;
        }
        private void SizerMouseMove(object sender, MouseEventArgs e)
        {
            if (mov == true)
            {
                splitContainer1.SplitterDistance = 25;
                splitContainer1.Dock = DockStyle.Fill;
                Width = MousePosition.X - Mx + Sw;
                Height = MousePosition.Y - My + Sh;
            }
        }
        private void SizerMouseUp(object sender, MouseEventArgs e)
        {
            mov = false;
            if (Width < minWidth)
            {
                Width = minWidth;
            }
            if (Height < minHeight)
            {
                Height = minHeight;
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

        //Toolbar methods
        private void ToolbarDrag(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }
        private void close_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitterDistance = splitContainer.SplitterDistance;
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
