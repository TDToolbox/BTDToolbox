using BTDToolbox.Classes;
using BTDToolbox.Classes.Spritesheets;
using BTDToolbox.Extra_Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static BTDToolbox.GeneralMethods;
using static BTDToolbox.ProjectConfig;

namespace BTDToolbox
{
    public partial class Main : Form
    {
        //Form variables
        public static string version = "Alpha 0.1.0";
        public static bool disableUpdates = false;
        private static Main toolbox;
        private static UpdateHandler update;
        string livePath = Environment.CurrentDirectory;


        //Project Variables
        
        Thread backupThread;
        public static bool exit = false;

        //Config variables
        ConfigFile cfgFile;
        bool existingUser = true;
        public string lastProject;
        public static string projName;
        public static string gameName;
        public static string BTD5_Dir;
        public static string BTDB_Dir;
        public static bool enableConsole;
        bool projNoGame = false;

        // Win32 Constants
        private const int SB_HORZ = 0;
        private const int SB_VERT = 1;
        private const int SB_CTL = 2;
        private const int SB_BOTH = 3;

        // Win32 Functions
        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);
        private const int WM_NCCALCSIZE = 0x83;

        /*//private const int SB_BOTH = 3;
        private const int WM_NCCALCSIZE = 0x83;
        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);*/



        //
        //Initialize window
        //
        public Main()
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
            this.DoubleBuffered = true;
            this.FormClosed += ExitHandling;
        }
        private void Startup()
        {
            cfgFile = DeserializeConfig();
            existingUser = cfgFile.ExistingUser;

            this.Size = new Size(cfgFile.Main_SizeX, cfgFile.Main_SizeY);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(cfgFile.Main_PosX, cfgFile.Main_PosY);
            this.Font = new Font("Microsoft Sans Serif", cfgFile.Main_FontSize);
            if (cfgFile.Main_Fullscreen)
                this.WindowState = FormWindowState.Maximized;

            enableConsole = cfgFile.EnableConsole;
            lastProject = cfgFile.LastProject;
            disableUpdates = cfgFile.disableUpdates;
        }
        private void FirstTimeUse()
        {
            var firstUser = new First_Time_Use();
            firstUser.MdiParent = this;
            firstUser.Show();
        }
        private void ExitHandling(object sender, EventArgs e)
        {
            exit = true;
            if (ConsoleHandler.validateConsole())
            {
                if (ConsoleHandler.console.Visible)
                    enableConsole = true;
                else
                    enableConsole = false;

                Serializer.SaveConfig(this, "main", cfgFile);
            }
        }
        private void Main_Load(object sender, EventArgs e)
        {
            var bg = new BGForm();
            bg.MdiParent = this;
            bg.MouseClick += Bg_MouseClick;
            bg.Show();


            if (File.Exists(Environment.CurrentDirectory + "\\BTDToolbox_Updater.exe"))
                File.Delete(Environment.CurrentDirectory + "\\BTDToolbox_Updater.exe");
            if (File.Exists(Environment.CurrentDirectory + "\\BTDToolbox_Updater.zip"))
                File.Delete(Environment.CurrentDirectory + "\\BTDToolbox_Updater.zip");
            if (File.Exists(Environment.CurrentDirectory + "\\Update"))
                File.Delete(Environment.CurrentDirectory + "\\Update");

            ConsoleHandler.console = new Console();
            ConsoleHandler.console.MdiParent = this;

            if (enableConsole == true)
                ConsoleHandler.console.Show();
            else
                ConsoleHandler.console.Hide();

            ConsoleHandler.appendLog("Program loaded!");
            if (programData.recentUpdate == true)
                ConsoleHandler.appendLog("BTD Toolbox has successfully updated.");

            if(!disableUpdates)
            {
                ConsoleHandler.announcement();
                var isUpdate = new UpdateHandler();
                isUpdate.HandleUpdates();
            }

            foreach (Control con in Controls)
                if (con is MdiClient)
                    mdiClient = con as MdiClient;
        }

        private void Bg_MouseClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void showUpdateChangelog()
        {
            if (programData.recentUpdate == true)
            {
                var changelog = new UpdateChangelog();
                changelog.MdiParent = this;
                changelog.Show();

                UpdateChangelog.recentUpdate = false;
                Serializer.SaveSmallSettings("updater", cfgFile);
            }
        }

        private void mainResize(object sender, EventArgs e)
        {

        }
        public static Main getInstance()
        {
            return toolbox;
        }
        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                CompileJet("launch");
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
                AddNewJet();
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                CompileJet("output");
            }
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Searching for existing projects...");
            OpenJetForm();

            if (JetProps.getForm(0) == null)
                ConsoleHandler.appendLog("No projects detected.");
            //taken from here
            if (existingUser == false)
            {
                FirstTimeUse();
            }
            showUpdateChangelog();
        }
        //
        //
        //Open or Create Projects
        //
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
        private void AddNewJet()
        {
            ZipForm.existingJetFile = "";
            string path = BrowseForFile("Browse for an existing .jet file", "jet", "Jet files (*.jet)|*.jet|All files (*.*)|*.*", "");
            if (path != null && path != "")
            {
                if (path.Contains(".jet"))
                {
                    gameName = DetermineJet_Game(path);
                    ZipForm.existingJetFile = path;
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
        //
        //Mdi Stuff
        //
        //Old WndProc (Mallis's)
        protected override void WndProc(ref Message m)
        {
            if (mdiClient != null)
            {
                try
                {
                    //ShowScrollBar(mdiClient.Handle, SB_BOTH, 0);// Hide the ScrollBars);
                    ShowScrollBar(m.HWnd, SB_BOTH, 0);// Hide the ScrollBars);
                }
                catch (Exception)
                {
                }
            }
            base.WndProc(ref m);
        }

        MdiClient mdiClient = null;
        private void Main_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        //
        //
        //UI Buttons
        //
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
        private void LaunchProgram_Click(object sender, EventArgs e)
        {
            CompileJet("launch");
        }
        private void ToggleConsole_Click(object sender, EventArgs e)
        {
            if (ConsoleHandler.validateConsole())
            {
                if (ConsoleHandler.console.Visible)
                    ConsoleHandler.console.Hide();
                else
                    ConsoleHandler.console.Show();
            }
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
        private void Debug_ThemedForm_Click(object sender, EventArgs e)
        {
            ThemedForm tft = new ThemedForm();
            tft.MdiParent = this;
            tft.Show();
        }
        private void OpenCredits_Click(object sender, EventArgs e)
        {
            CreditViewer cv = new CreditViewer();
            cv.MdiParent = this;
            cv.Show();
        }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompileJet("output");
        }
        private void Open_Existing_JetFile_Click(object sender, EventArgs e)
        {
            AddNewJet();
        }
        private void NewProject(string gameName)
        {
            if (projNoGame == false)
            {
                if (isGamePathValid(gameName) == false)
                {
                    ConsoleHandler.appendLog("Please browse for " + Get_EXE_Name(gameName));
                    browseForExe(gameName);
                    if (isGamePathValid(gameName) == false)
                    {
                        ConsoleHandler.appendLog("Theres been an error identifying your game");
                    }
                    else
                    {
                        if (!Validate_Backup(gameName))
                            CreateBackup(gameName);
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
            NewProject(gameName);
        }
        private void New_BTDB_Proj_Click(object sender, EventArgs e)
        {
            gameName = "BTDB";
            Serializer.SaveConfig(this, "game", cfgFile);
            NewProject(gameName);
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
            JsonEditorHandler.OpenFile(Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\TowerDefinitions\\DartMonkey.tower");
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
        

        private void BTDFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update = new UpdateHandler();
            update.HandleUpdates();
        }

        private void ReinstallBTDToolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update = new UpdateHandler();
            update.reinstall = true;
            update.HandleUpdates();
        }

        private void OpenBTDToolboxGithubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Opening BTD Toolbox Github page...");
            Process.Start("https://github.com/TDToolbox/BTDToolbox-2019");
        }

        private void TestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string startDir = "";
            if (gameName != "")
            {
                MessageBox.Show("Please select the sprite file you want to decompile");
                if (gameName == "BTD5")
                {
                    if(Serializer.Deserialize_Config().BTD5_Directory != "")
                    {
                        startDir = Serializer.Deserialize_Config().BTD5_Directory + "\\Assets\\Textures";
                    }
                }
                else
                {
                    if (Serializer.Deserialize_Config().BTDB_Directory != "")
                    {
                        startDir = Serializer.Deserialize_Config().BTDB_Directory + "\\Assets\\Textures";
                    }
                }
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = startDir;
            ofd.Title = "Browse for sprite sheet";
            ofd.Filter = "Image files (*.png, *.jpng, *.jpg, *.jpeg) | *.png; *.jpng; *.jpg; *.jpeg";
            ofd.Multiselect = true;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                if(ofd.FileName.EndsWith(".png") || ofd.FileName.EndsWith(".jpng") || ofd.FileName.EndsWith(".jpg") || ofd.FileName.EndsWith(".jpeg"))
                {
                    SpriteSheet_Handler handler = new SpriteSheet_Handler();
                    Thread thread = new Thread(delegate () { handler.Extract(ofd.FileName, "Cell"); });
                    thread.Start();
                    
                }
                else
                {
                    MessageBox.Show("You selected an invalid filetype. Please contact the TD Toolbox team if you think we should add this to the list");
                }
            }
            /*BattlesPassManager mgr = new BattlesPassManager();
            mgr.Show();*/
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var modMM = new ModLoader_Handling();
            modMM.Handle_ModLoader();
        }

        private void OpenSettings_Button_Click(object sender, EventArgs e)
        {
            var settings = new SettingsWindow();
            settings.MdiParent = this;
            settings.Show();
        }

        private void NKHook_Github_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Opening NKHook Github page...");
            Process.Start("https://github.com/DisabledMallis/NKHook5-Dep");
        }

        private void FlashReader_Click(object sender, EventArgs e)
        {
            var flashReader = new FlashReader();
            flashReader.MdiParent = this;
            flashReader.Show();
        }

        private void GetBTDBPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult diag = MessageBox.Show("You are trying to open the \"Get BTDB Password\" tool. It will take a couple of minutes to get the password. Do you wish to continue?", "Continue?", MessageBoxButtons.YesNo);
            if(diag == DialogResult.Yes)
            {
                var getPass = new CrackBTDB_Pass();
                getPass.Get_BTDB_Password();
            }
            else
            {
                ConsoleHandler.appendLog_CanRepeat("User cancelled password tool...");
            }
        }

        private void BTD5DirectoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (isGamePathValid("BTD5"))
            {
                ConsoleHandler.appendLog("Opening BTD5 Directory");
                Process.Start(DeserializeConfig().BTD5_Directory);
            }
            else
            {
                ConsoleHandler.appendLog("Could not find your BTD5 directory");
                browseForExe("BTD5");
                if(isGamePathValid("BTD5"))
                {
                    ConsoleHandler.appendLog("Opening BTD5 Directory");
                    Process.Start(DeserializeConfig().BTD5_Directory);
                }
                else
                {
                    ConsoleHandler.appendLog("Something went wrong...");
                }
            }
        }

        private void BTDBDirectoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (isGamePathValid("BTDB"))
            {
                ConsoleHandler.appendLog("Opening BTD Battles Directory");
                Process.Start(DeserializeConfig().BTDB_Directory);
            }
            else
            {
                ConsoleHandler.appendLog("Could not find your BTDB directory");
                browseForExe("BTDB");
                if (isGamePathValid("BTDB"))
                {
                    ConsoleHandler.appendLog("Opening BTDB Directory");
                    Process.Start(DeserializeConfig().BTDB_Directory);
                }
                else
                {
                    ConsoleHandler.appendLog("Something went wrong...");
                }
            }
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Opening BTD Toolbox Directory");
            Process.Start(Environment.CurrentDirectory);
        }

        private void ShowBTD5_Pass_Click(object sender, EventArgs e)
        {
            ConsoleHandler.force_appendLog("The password for BTD5.jet is:   Q%_{6#Px]]\n>> Make sure you don't copy it with spaces in it");
        }

        private void ShowLastBattlesPass_Click(object sender, EventArgs e)
        {
            if(Serializer.Deserialize_Config().battlesPass != null && Serializer.Deserialize_Config().battlesPass != "")
            {
                ConsoleHandler.force_appendLog("The last password you used for BTDB is:   " + Serializer.Deserialize_Config().battlesPass);
            }
            else
            {
                ConsoleHandler.force_appendLog("You don't have a battles password saved... Next time you make a new project, make sure to check \"Remember Password\"");
            }
        }

        private void EraseConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.ClearConsole();
        }

        private void EasyTowerEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var easyTower = new EasyTowerEditor();
            string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\TowerDefinitions\\DartMonkey.tower";
            easyTower.path = path;
            easyTower.Show();
        }

        private void BTD5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CreateBackup("BTD5");
        }

        private void BTDBattlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateBackup("BTDB");
        }

        private void RestoreBTD5LocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreGame_ToBackup_LOC("BTD5");
        }

        private void RestoreBTDBattlesLOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreGame_ToBackup_LOC("BTDB");
        }

        private void Reset_EXE_Click(object sender, EventArgs e)
        {
            ConsoleHandler.force_appendNotice("This will reset your game path...");
        }

        private void Reset_EXE_MouseHover(object sender, EventArgs e)
        {
            ConsoleHandler.force_appendNotice("This will reset your game path...");
        }

        private void ValidateBTD5_Click(object sender, EventArgs e)
        {
            Process.Start("steam://validate/306020");
        }

        private void ValidateBTDB_Click(object sender, EventArgs e)
        {
            Process.Start("steam://validate/444640");
        }

        private void FontForPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Opening download for BTD Font for PC");
            Process.Start("https://www.dropbox.com/s/k7y2utz42b5eg06/Oetztype.TTF?dl=1");
        }

        private void OnlineFontGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Opening Online BTD Font Generator...");
            Process.Start("https://fontmeme.com/bloons-td-battles-font/");
        }

        private void SpriteSheetDecompilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.appendLog("Opening Sprite Decompiler github link...");
            Process.Start("https://github.com/TheSubtleKiller/SpriteSheetRebuilder");
        }

        private void NewProject_From_Backup_Click(object sender, EventArgs e)
        {
            AddNewJet();
        }
        private void EZ_TowerEditor_Click(object sender, EventArgs e)
        {
            var ezTower = new EasyTowerEditor();
            string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\TowerDefinitions\\DartMonkey.tower";
            ezTower.path = path;
            ezTower.Show();
        }

        private void EZ_BloonEditor_Click(object sender, EventArgs e)
        {
            var ezBloon = new EZBloon_Editor();
            string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions\\Red.bloon";
            ezBloon.path = path;
            ezBloon.Show();
        }

        private void EZCard_Editor_Click(object sender, EventArgs e)
        {
            if (Serializer.Deserialize_Config().CurrentGame == "BTDB")
            {
                var ezCard = new EZCard_Editor();
                string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BattleCardDefinitions\\0.json";
                ezCard.path = path;
                ezCard.Show();
            }
            else
            {
                ConsoleHandler.force_appendNotice("This tool only works for BTD Battles projects. To use it, please open a BTDB project");
            }
        }

        private void BTDBPasswordManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BattlesPassManager mgr = new BattlesPassManager();
            mgr.Show();
        }
    }
}