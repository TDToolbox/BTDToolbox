
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfig;
using static BTDToolbox.GeneralMethods;
using System.Diagnostics;

namespace BTDToolbox
{
    public partial class TD_Toolbox_Window : Form
    {
        //Form variables
        string livePath = Environment.CurrentDirectory;
        public string projectDirPath;
        private static TD_Toolbox_Window toolbox;

        string version = "Alpha 0.0.3";

        //Project Variables
        public DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
        Thread backupThread;

        //Config variables
        ConfigFile cfgFile;
        public static string gameName;
        bool firstUse = true;
        public bool loadLastProject;
        public static string file;
        public int mainFormFontSize;
        public static bool enableConsole;
        public static string projName;
        private Bitmap bgImg;
        public string lastProject;
        float fontSize;
        public static string BTD5_Dir;
        public static string BTDB_Dir;

        //Scroll bar variables
        private const int SB_BOTH = 3;
        private const int WM_NCCALCSIZE = 0x83;
        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

        //
        //Initialize window
        //
        public TD_Toolbox_Window()
        {
            InitializeComponent();
            toolbox = this;
            Startup();

            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(15, 15, 15);
            this.BackgroundImageLayout = ImageLayout.Center;
            this.Resize += mainResize;
            this.KeyPreview = true;

            this.versionTag.BackColor = Color.FromArgb(15, 15, 15);
            this.versionTag.Text = version;

            Random rand = new Random();
            switch (rand.Next(0, 2))
            {
                case 0:
                    bgImg = Properties.Resources.Logo1;
                    break;
                case 1:
                    bgImg = Properties.Resources.Logo2;
                    break;
            }

            this.BackgroundImage = bgImg;
            this.FormClosed += ExitHandling;
        }
        private void Startup()
        {
            cfgFile = DeserializeConfig();
            bool existingUser = cfgFile.ExistingUser;
            if (existingUser == false)
            {
                backupThread = new Thread(FirstTimeUse);
                backupThread.Start();
            }

            this.Size = new Size(cfgFile.Main_SizeX, cfgFile.Main_SizeY);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(cfgFile.Main_PosX, cfgFile.Main_PosY);
            this.Font = new Font("Microsoft Sans Serif", cfgFile.Main_FontSize);
            if (cfgFile.Main_Fullscreen)
                this.WindowState = FormWindowState.Maximized;

            enableConsole = cfgFile.EnableConsole;
            lastProject = cfgFile.LastProject;

            if (lastProject != null)
            {

            }
        }
        private void TD_Toolbox_Window_Load(object sender, EventArgs e)
        {

            ConsoleHandler.console = new NewConsole();
            ConsoleHandler.console.MdiParent = this;

            if (enableConsole == true)
                ConsoleHandler.console.Show();
            else
                ConsoleHandler.console.Hide();

            ConsoleHandler.appendLog("Program loaded!");
            ConsoleHandler.appendLog("Searching for existing projects...");

            OpenJetForm();

            foreach (Control con in Controls)
                if (con is MdiClient)
                    mdiClient = con as MdiClient;
        }
        private void FirstTimeUse()
        {
            MessageBox.Show("Welcome to BTD Toolbox! This is a place holder. IF you are using Toolbox version 0.0.4 or higher and seeing this message, please contact staff, cuz we need to add a welcome screen already :)");
        }
        public static TD_Toolbox_Window getInstance()
        {
            return toolbox;
        }
        public void OpenJetForm()
        {
            if (lastProject != "" && lastProject != null)
            {
                DirectoryInfo dinfo = new DirectoryInfo(lastProject);
                string[] split = dinfo.ToString().Split('\\');
                string name = split[split.Length - 1];
                //projName = name;

                if (dinfo.Exists)
                {
                    foreach (JetForm o in JetProps.get())
                    {
                        if (o.projName == projName)
                        {
                            MessageBox.Show("The project is already opened..");
                            return;
                        }
                    }
                    JetForm jf = new JetForm(dinfo, this, name);
                    jf.MdiParent = this;
                    jf.Show();
                    projName = dinfo.ToString();
                }
            }
        }
        private void TD_Toolbox_Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                compileJet("launch");
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
                AddNewJet();
            }
        }
        private void mainResize(object sender, EventArgs e)
        {
            this.BackgroundImage = bgImg;
            this.BackgroundImageLayout = ImageLayout.Center;
        }
        private void Debug_ThemedForm_Click(object sender, EventArgs e)
        {
            ThemedForm tft = new ThemedForm();
            tft.MdiParent = this;
            tft.Show();
        }
        //
        //Open or Create Projects
        //
        private void AddNewJet()
        {
            string path = BrowseForFile("Browse for an existing .jet file", "jet", "Jet files (*.jet)|*.jet|All files (*.*)|*.*", "");
            if(path != null && path != "")
            {
                if (path.Contains(".jet"))
                {
                    gameName = DetermineJet_Game(path);
                    Serializer.SaveConfig(this, "game", programData);
                    var getName = new SetProjectName();
                    getName.Show();
                }
            }
        }
        private void OpenExistingProject_Click(object sender, EventArgs e)
        {
            BrowseForExistingProjects();
        }
        private void BrowseForExistingProjects()    //check this if having issues with last project
        {
            string path = BrowseForDirectory("Browse for an existing project", livePath);
            if (path != null && path != "")
            {
                string[] split = path.Split('\\');
                string name = split[split.Length - 1];
                if (!name.StartsWith("proj_"))
                {
                    ConsoleHandler.appendLog("Invalid project... Please browse for a folder that starts with    proj_");
                    MessageBox.Show("Error!! Not a valid project file. Please try again...");
                }
                else
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    projName = path;
                    if (path.Contains("BTD5"))
                        gameName = "BTD5";
                    else if (path.Contains("BTDB"))
                        gameName = "BTDB";

                    Serializer.SaveConfig(this, "game", cfgFile);
                    JetForm jf = new JetForm(dirInfo, this, name);
                    jf.MdiParent = this;
                    jf.Show();
                    Serializer.SaveConfig(this, "main", cfgFile);   //try changing this if having issues with last project
                }
            }
        }
        //
        //UI Buttons
        //
        private void compileJet(string switchCase)
        {
            if (JsonEditor.jsonError != true)
            {
                if (switchCase.Contains("BTDB"))
                {
                    ExtractingJet_Window.currentProject = projName;
                }
                if (switchCase == "launch")
                {
                    if (JetProps.get().Count == 1)
                    {
                        ExtractingJet_Window.launchProgram = true;
                        if (cfgFile.CurrentGame == "BTDB")
                            ExtractingJet_Window.switchCase = "compile BTDB";
                        else
                            ExtractingJet_Window.switchCase = "compile";
                        var compile = new ExtractingJet_Window();
                    }
                    else
                    {
                        if (JetProps.get().Count < 1)
                        {
                            MessageBox.Show("You have no .jets or projects open, you need one to launch.");
                        }
                        else
                        {
                            MessageBox.Show("You have multiple .jets or projects open, only one can be launched.");
                        }
                    }
                }
                else
                {
                    ExtractingJet_Window.launchProgram = false;
                    ExtractingJet_Window.switchCase = switchCase;
                    var compile = new ExtractingJet_Window();
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("ERROR!!! There is a JSON Error in this file!!!\n\nIf you leave the file now it will be corrupted and WILL break your mod. Do you still want to leave?", "ARE YOU SURE!!!!!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (switchCase.Contains("BTDB"))
                    {
                        ExtractingJet_Window.currentProject = projName;
                    }
                    if (switchCase == "launch")
                    {
                        if (JetProps.get().Count == 1)
                        {
                            ExtractingJet_Window.launchProgram = true;
                            if (cfgFile.CurrentGame == "BTDB")
                                ExtractingJet_Window.switchCase = "compile BTDB";
                            else
                                ExtractingJet_Window.switchCase = "compile";
                            var compile = new ExtractingJet_Window();
                        }
                        else
                        {
                            if (JetProps.get().Count < 1)
                            {
                                MessageBox.Show("You have no .jets or projects open, you need one to launch.");
                            }
                            else
                            {
                                MessageBox.Show("You have multiple .jets or projects open, only one can be launched.");
                            }
                        }
                    }
                    else
                    {
                        ExtractingJet_Window.launchProgram = false;
                        ExtractingJet_Window.switchCase = switchCase;
                        var compile = new ExtractingJet_Window();
                    }
                }
            }

        }
        private void LaunchProgram_Click(object sender, EventArgs e)
        {
            compileJet("launch");
        }
        private void ToggleConsole_Click(object sender, EventArgs e)
        {
            if (ConsoleHandler.console.Visible)
                ConsoleHandler.console.Hide();
            else
                ConsoleHandler.console.Show();
        }
        private void Find_Button_Click(object sender, EventArgs e)
        {
            FindWindow findForm = new FindWindow();
            findForm.Show();
            findForm.Text = "Find";
            findForm.replace = false;
            findForm.find = true;
        }
        private void Replace_Button_Click(object sender, EventArgs e)
        {
            FindWindow findForm = new FindWindow();
            findForm.Show();
            findForm.Text = "Replace";
            findForm.replace = true;
            findForm.find = false;
        }
        private void OpenCredits_Click(object sender, EventArgs e)
        {
            CreditViewer cv = new CreditViewer();
            cv.MdiParent = this;
            cv.Show();
        }
        //
        //Config stuff
        //
        private void ExitHandling(object sender, EventArgs e)
        {
            if (ConsoleHandler.console.Visible)
                enableConsole = true;
            else
                enableConsole = false;

            Serializer.SaveConfig(this, "main", cfgFile);
        }
        //
        //Mdi Stuff
        //
        protected override void WndProc(ref Message m)
        {
            if (mdiClient != null)
            {
                try
                {
                    ShowScrollBar(mdiClient.Handle, SB_BOTH, 0 /*Hide the ScrollBars*/);
                }
                catch (Exception)
                {
                }
            }
            base.WndProc(ref m);
        }
        MdiClient mdiClient = null;

        private void OpenJetExplorer_Click(object sender, EventArgs e)
        {
            try
            {
                OpenJetForm();
            }
            catch (Exception ex)
            {
                ConsoleHandler.appendLog(ex.StackTrace);
            }
        }

        private void TD_Toolbox_Window_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cfgFile.CurrentGame != "BTDB")
                compileJet("output");
            else
                compileJet("output BTDB");
        }

        private void NewProject_From_Backup_Click(object sender, EventArgs e)
        {

            //compileJet("decompile backup");
        }

        private void RemakeBackupjetToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Open_Existing_JetFile_Click(object sender, EventArgs e)
        {
            AddNewJet();
        }
        private void NewProject()
        {
            if (isGamePathValid(gameName) == false)
            {
                browseForExe(gameName);
                if (isGamePathValid(gameName) == false)
                {
                    ConsoleHandler.appendLog("Theres been an error identifying your game");
                }
                else
                {
                    Serializer.SaveConfig(this, "directories", cfgFile);
                    var setProjName = new SetProjectName();
                    setProjName.Show();
                }
            }
            else
            {
                var setProjName = new SetProjectName();
                setProjName.Show();
            }
        }
        private void New_BTD5_Proj_Click(object sender, EventArgs e)
        {
            gameName = "BTD5";
            Serializer.SaveConfig(this, "game", cfgFile);
            NewProject();
        }

        private void New_BTDB_Proj_Click(object sender, EventArgs e)
        {
            gameName = "BTDB";
            Serializer.SaveConfig(this, "game", cfgFile);
            NewProject();
        }

        private void Replace_BTDB_Backup_Click(object sender, EventArgs e)
        {
            CreateBackup("BTDB");
        }

        private void Replace_BTD5_Backup_Click(object sender, EventArgs e)
        {
            CreateBackup("BTD5");
        }

        private void TestForm_Click(object sender, EventArgs e)
        {
            var editor = new JsonEditor(lastProject + "\\Assets\\JSON\\TowerDefinitions\\DartMonkey.tower");
            editor.Show();
        }
        private void BTD5DirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isGamePathValid("BTD5"))
            {
                ConsoleHandler.appendLog("Opening BTD5 Directory");
                Process.Start(DeserializeConfig().BTD5_Directory);
            }
            else
                ConsoleHandler.appendLog("Could not find your BTD5 directory");
        }
        private void BTDBDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isGamePathValid("BTDB"))
            {
                ConsoleHandler.appendLog("Opening BTD Battles Directory");
                Process.Start(DeserializeConfig().BTDB_Directory);
            }
            else
                ConsoleHandler.appendLog("Could not find your BTDB directory");
        }
        private void ToolboxDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Opening BTD Toolbox Directory");
            Process.Start(Environment.CurrentDirectory);
        }
        private void ResetBTD5exeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string g = "BTD5";
            ConsoleHandler.appendLog("Please browse for BTD5-Win.exe");
            browseForExe(g);
            if (isGamePathValid(g) == true)
            {
                ConsoleHandler.appendLog("Success! Selected exe at: " + DeserializeConfig().BTD5_Directory);
            }
            else
                ConsoleHandler.appendLog("Invalid game directory selected.");
        }
        private void ResetBTDBattlesexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string g = "BTDB";
            ConsoleHandler.appendLog("Please browse for Battles-win.exe");
            browseForExe(g);
            if (isGamePathValid(g) == true)
            {
                ConsoleHandler.appendLog("Success! Selected exe at: " + DeserializeConfig().BTDB_Directory);
            }
            else
                ConsoleHandler.appendLog("Invalid game directory selected.");
        }
        private void ResetUserSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Resetting user settings...");
            string settingsPath = livePath + "\\settings.json";
            if (File.Exists(settingsPath))
            {
                File.Delete(settingsPath);
            }
            DeserializeConfig();
            ConsoleHandler.appendLog("User settings have been reset.");
        }

        private void Backup_BTD5_Click_1(object sender, EventArgs e)
        {
            RestoreGame_ToBackup("BTD5");
        }

        private void Backup_BTDB_Click_1(object sender, EventArgs e)
        {
            RestoreGame_ToBackup("BTDB");
        }
    }
}