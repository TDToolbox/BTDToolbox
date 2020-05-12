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
using BTDToolbox.Classes.NewProjects;

namespace BTDToolbox
{
    public partial class Main : Form
    {
        //Form variables
        public static string version = "Toolbox 0.1.4";
        public static bool canUseNKH = false;

        //public static string projectFilePath = "";
        private static Main toolbox;
        public static BGForm bg;
        private static UpdateHandler update;
        public static bool finishedLoading = false;

        //Project Variables
        public static bool exit = false;        
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

        //
        //Initialize window
        //
        public Main()
        {
            InitializeComponent();
            Serializer.LoadSettings();
            toolbox = this;

            DeveloperMode.ControlDeveloperMode();
            Startup();

            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(15, 15, 15);
            this.BackgroundImageLayout = ImageLayout.Center;
            this.Resize += mainResize;
            this.KeyPreview = true;

            this.versionTag.BackColor = Color.FromArgb(15, 15, 15);
            this.versionTag.Text = version;
            this.DoubleBuffered = true;
        }

        

        private void Startup()
        {
            this.Size = new Size(Serializer.cfg.Main_SizeX, Serializer.cfg.Main_SizeY);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Serializer.cfg.Main_PosX, Serializer.cfg.Main_PosY);
            this.Font = new Font("Microsoft Sans Serif", Serializer.cfg.Main_FontSize);
        }
        private void FirstTimeUse()
        {
            var firstUser = new First_Time_Use();
            firstUser.MdiParent = this;
            firstUser.Show();
        }
        private void Main_Load(object sender, EventArgs e)
        {
            bg = new BGForm();
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
            ConsoleHandler.console.CreateLogFile();

            if (Serializer.cfg.EnableConsole == true)
                ConsoleHandler.console.Show();
            else
                ConsoleHandler.console.Hide();

            ConsoleHandler.append("Program loaded!");
            if (Serializer.cfg.recentUpdate == true)
                ConsoleHandler.append("BTD Toolbox has successfully updated.");

            if(!Serializer.cfg.disableUpdates)
            {
                new Thread(() =>
                {
                    ConsoleHandler.announcement();
                    var isUpdate = new UpdateHandler();
                    isUpdate.HandleUpdates();
                }).Start();                
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
            if (Serializer.cfg.recentUpdate == false)
                return;

            UpdateChangelog getChangelog = new UpdateChangelog();
            getChangelog.GotChangelog += Update_GotChangelog;
            getChangelog.StartUpdateChangelog();
        }

        private void Update_GotChangelog(object source, MyEventArgs e)
        {
            Invoke((MethodInvoker)delegate {
                UpdateChangelog update = new UpdateChangelog(e.GetInfo())
                {
                    MdiParent = this
                };
            });            
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
                if (NKHook.CanUseNKH() && CurrentProjectVariables.UseNKHook == true)
                    CompileJet("launch nkh");
                else
                    CompileJet("launch");
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
                SelectGame select = new SelectGame();
                select.Show();
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                CompileJet("output");
            }
        }
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        private void Main_Shown(object sender, EventArgs e)
        {
            ConsoleHandler.append("Searching for existing projects...");
            OpenJetForm();

            if (JetProps.getForm(0) == null)
                ConsoleHandler.append("No projects detected.");
            //taken from here
            if (Serializer.cfg.ExistingUser == false)
                FirstTimeUse();

            showUpdateChangelog();
            finishedLoading = true;
        }
        //
        //
        //Open or Create Projects
        //
        public void OpenJetForm()
        {
            if (!Guard.IsStringValid(Serializer.cfg.LastProject))
                return;

            DirectoryInfo dinfo = new DirectoryInfo(Serializer.cfg.LastProject);

            if (!dinfo.Exists)
                return;

            foreach (JetForm o in JetProps.get())
            {
                if (o.projName == Serializer.cfg.LastProject)
                {
                    MessageBox.Show("The project is already opened..");
                    return;
                }
            }

            JetForm jf = new JetForm(dinfo, dinfo.Name);
            jf.MdiParent = this;
            jf.Show();
            Serializer.cfg.LastProject = dinfo.ToString();
        }
        private void AddNewJet()
        {
            ZipForm.existingJetFile = "";
            string path = BrowseForFile("Browse for an existing .jet file", "jet", "Jet files (*.jet)|*.jet|All files (*.*)|*.*", "");
            if (Guard.IsStringValid(path) && path.Contains(".jet"))
            {
                SelectGame select = new SelectGame(path);
                select.Show();
            }
        }
        private void OpenExistingProject_Click(object sender, EventArgs e)
        {
            BrowseForExistingProjects();
        }
        private void BrowseForExistingProjects()    //check this if having issues with last project
        {
            string path = BrowseForDirectory("Browse for an existing project", Environment.CurrentDirectory);
            if (path != null && path != "")
            {
                string[] split = path.Split('\\');
                string name = split[split.Length - 1];
                if (!name.StartsWith("proj_"))
                {
                    ConsoleHandler.append("Invalid project... Please browse for a folder that starts with    proj_");
                    MessageBox.Show("Error!! Not a valid project file. Please try again...");
                }
                else
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    Serializer.cfg.LastProject = path;
                    if (path.Contains("BTD5"))
                        Serializer.cfg.CurrentGame = "BTD5";
                    else if (path.Contains("BTDB"))
                        Serializer.cfg.CurrentGame = "BTDB";
                    else if (path.Contains("BMC"))
                        Serializer.cfg.CurrentGame = "BMC";

                    JetForm jf = new JetForm(dirInfo, name);
                    jf.MdiParent = this;
                    jf.Show();
                    Serializer.SaveSettings();
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
                if(JetProps.get().Count >0)
                {
                    foreach (var a in JetProps.get())
                    {
                        if (a.Visible)
                        {
                            ConsoleHandler.append("Hiding JetViewer");
                            a.Hide();
                        }
                        else
                        {
                            ConsoleHandler.append("Reopening JetViewer");
                            a.Show();
                        }
                    }
                }
                else
                {
                    OpenJetForm();
                }
                
            }
            catch (Exception ex)
            {
                ConsoleHandler.append(ex.StackTrace);
            }
        }
        private void LaunchProgram_Click(object sender, EventArgs e)
        {
            if(!NKHook.CanUseNKH())
                CompileJet("launch");
        }
        private void ToggleConsole_Click(object sender, EventArgs e)
        {
            if (ConsoleHandler.validateConsole())
            {
                if (ConsoleHandler.console.Visible)
                {
                    ConsoleHandler.append("Hiding console.");
                    ConsoleHandler.console.Hide();
                }
                else
                {
                    ConsoleHandler.append("Showing console.");
                    ConsoleHandler.console.Show();
                }
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
            if (projNoGame)
                return;

            if (isGamePathValid(gameName) == false)
            {
                string gameDir = GetGameDir(gameName);
                if (!Guard.IsStringValid(gameDir))
                {
                    ConsoleHandler.append("The game directory wasn't found. Cancelling project creation.");
                    return;
                }

                CurrentProjectVariables.GameName = gameName;
                CurrentProjectVariables.GamePath = gameDir;
                
                if(Guard.IsStringValid(CurrentProjectVariables.PathToProjectClassFile))
                    ProjectHandler.SaveProject();

                SaveGamePath(gameName, gameDir);
                Serializer.SaveSettings();

                if (!Validate_Backup(gameName))
                    CreateBackup(gameName);
            }

            var setProjName = new SetProjectName();
            setProjName.Show();
        }
        private void New_BTD5_Proj_Click(object sender, EventArgs e)
        {
            Serializer.cfg.CurrentGame = "BTD5";
            ProjectHandler.CreateProject();
            CurrentProjectVariables.GameName = "BTD5";
            CurrentProjectVariables.GamePath = Serializer.cfg.BTD5_Directory;
            NewProject(Serializer.cfg.CurrentGame);
        }
        private void New_BTDB_Proj_Click(object sender, EventArgs e)
        {
            Serializer.cfg.CurrentGame = "BTDB";
            ProjectHandler.CreateProject();
            CurrentProjectVariables.GameName = "BTDB";
            CurrentProjectVariables.GamePath = Serializer.cfg.BTDB_Directory;
            NewProject(Serializer.cfg.CurrentGame);
        }
        private void New_BMC_Proj_Button_Click(object sender, EventArgs e)
        {
            ProjectHandler.CreateProject();
            CurrentProjectVariables.GameName = "BMC";
            CurrentProjectVariables.GamePath = Serializer.cfg.BMC_Directory;

            NewProject("BMC");
        }
        private void Replace_BTDB_Backup_Click(object sender, EventArgs e)
        {
            CreateBackup("BTDB");
        }
        private void Replace_BTD5_Backup_Click(object sender, EventArgs e)
        {
            CreateBackup("BTD5");
        }
        private void Replace_BMC_Backup_Click(object sender, EventArgs e)
        {
            CreateBackup("BMC");
        }

        private void TestForm_Click(object sender, EventArgs e)
        {
            JsonEditorHandler.OpenFile(CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\DartMonkey.tower");
        }
        private void ResetBTD5exeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string g = "BTD5";
            ConsoleHandler.append("Please browse for BTD5-Win.exe");
            browseForExe(g);
            if (isGamePathValid(g) == true)
            {
                ConsoleHandler.append("Success! Selected exe at: " + Serializer.cfg.BTD5_Directory);
            }
            else
                ConsoleHandler.append("Invalid game directory selected.");
        }
        private void ResetBTDBattlesexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string g = "BTDB";
            ConsoleHandler.append("Please browse for Battles-win.exe");
            browseForExe(g);
            if (isGamePathValid(g) == true)
            {
                ConsoleHandler.append("Success! Selected exe at: " + Serializer.cfg.BTDB_Directory);
            }
            else
                ConsoleHandler.append("Invalid game directory selected.");
        }
        private void resetBMCexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string g = "BMC";
            ConsoleHandler.append("Please browse for MonkeyCity-Win.exe");
            browseForExe(g);
            if (isGamePathValid(g) == true)
            {
                ConsoleHandler.append("Success! Selected exe at: " + Serializer.cfg.BTDB_Directory);
            }
            else
                ConsoleHandler.append("Invalid game directory selected.");
        }

        private void ResetUserSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append("Resetting user settings...");
            if (File.Exists(Serializer.settingsPath))
                File.Delete(Serializer.settingsPath);

            ConsoleHandler.append("User settings have been reset.");
        }
        private void Backup_BTD5_Click_1(object sender, EventArgs e)
        {
            RestoreGame_ToBackup("BTD5");
        }
        private void Backup_BTDB_Click_1(object sender, EventArgs e)
        {
            RestoreGame_ToBackup("BTDB");
        }
        private void Restore_BMC_Click(object sender, EventArgs e)
        {
            RestoreGame_ToBackup("BMC");
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
            ConsoleHandler.append("Opening BTD Toolbox Github page...");
            Process.Start("https://github.com/TDToolbox/BTDToolbox-2019");
        }

        private void TestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!Tutorials.HasFreshTutList())
                Tutorials.GetTutorialsList();
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
            NKHook_Message msg = new NKHook_Message();
            msg.Show();
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
                ConsoleHandler.append_CanRepeat("User cancelled password tool...");
            }
        }


        private void BTD5DirectoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!isGamePathValid("BTD5"))
            {
                string gameD = GetGameDir("BTD5");
                if (!Guard.IsStringValid(gameD))
                    return;

                SaveGamePath("BTD5", gameD);
            }

            ConsoleHandler.append("Opening BTD5 Directory");
            Process.Start(Serializer.cfg.BTD5_Directory);
        }

        private void BTDBDirectoryToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!isGamePathValid("BTDB"))
            {
                string gameD = GetGameDir("BTDB");
                if (!Guard.IsStringValid(gameD))
                    return;

                SaveGamePath("BTDB", gameD);
            }

            ConsoleHandler.append("Opening BTDB Directory");
            Process.Start(Serializer.cfg.BTDB_Directory);
        }
        private void bMCDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isGamePathValid("BMC"))
            {
                string gameD = GetGameDir("BMC");
                if (!Guard.IsStringValid(gameD))
                    return;

                SaveGamePath("BMC", gameD);
            }

            ConsoleHandler.append("Opening BMC Directory");
            Process.Start(Serializer.cfg.BMC_Directory);
        }


        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append("Opening BTD Toolbox Directory");
            Process.Start(Environment.CurrentDirectory);
        }

        private void ShowBTD5_Pass_Click(object sender, EventArgs e)
        {
            string pass = "Q%_{6#Px]]";
            ConsoleHandler.append_Force("The password for BTD5.jet is:   Q%_{6#Px]]\n>> Make sure you don't copy it with spaces in it");

            DialogResult diag = MessageBox.Show("Would you like toolbox to copy it to your clipboard? It will overwrite whatever is currently copied?", "Copy to clipboard?", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                Clipboard.SetText(pass);
                ConsoleHandler.append("The password has been automatically copied to your clipboard so you can paste it easily.");
            }
            else
                ConsoleHandler.append("The password was not copied to clipboard... You can see it in the messages above");
        }
        private void ShowBMCPass_Button_Click(object sender, EventArgs e)
        {
            string pass = "Q%_{6#Px]]";
            ConsoleHandler.append_Force("The password for BMC's data.jet is:   Q%_{6#Px]]\n>> Make sure you don't copy it with spaces in it");

            DialogResult diag = MessageBox.Show("Would you like toolbox to copy it to your clipboard? It will overwrite whatever is currently copied?", "Copy to clipboard?", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                Clipboard.SetText(pass);
                ConsoleHandler.append("The password has been automatically copied to your clipboard so you can paste it easily.");
            }
            else
                ConsoleHandler.append("The password was not copied to clipboard... You can see it in the messages above");
        }
        private void ShowLastBattlesPass_Click(object sender, EventArgs e)
        {
            string pass = "";
            if (CurrentProjectVariables.GameName == "BTDB")
            {
                if (CurrentProjectVariables.JetPassword != "" && CurrentProjectVariables.JetPassword != null)
                    pass = CurrentProjectVariables.JetPassword;
            }
            else if(Serializer.cfg.battlesPass != null && Serializer.cfg.battlesPass != "")
                pass = Serializer.cfg.battlesPass;

            if(pass != null && pass !="")
            {
                ConsoleHandler.append_Force("The last password you used for BTDB is:   " + pass);
                DialogResult diag = MessageBox.Show("Would you like toolbox to copy it to your clipboard? It will overwrite whatever is currently copied?", "Copy to clipboard?", MessageBoxButtons.YesNo);
                if(diag == DialogResult.Yes)
                {
                    Clipboard.SetText(pass);
                    ConsoleHandler.append("The password has been automatically copied to your clipboard so you can paste it easily.");
                }
                else
                    ConsoleHandler.append("The password was not copied to clipboard... You can see it in the messages above");
            }
            else
            {
                ConsoleHandler.append_Force("You don't have a battles password saved... Next time you make a new project, make sure to check \"Remember Password\"");
            }
        }

        private void EraseConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.ClearConsole();
            ConsoleHandler.append("Console successfully cleared.");
        }

        private void EasyTowerEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var easyTower = new EasyTowerEditor();
            string path = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\DartMonkey.tower";
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
        private void forBMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateBackup("BMC");
        }

        private void RestoreBTD5LocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreGame_ToBackup_LOC("BTD5");
        }

        private void RestoreBTDBattlesLOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreGame_ToBackup_LOC("BTDB");
        }
        private void restoreBMCLocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreGame_ToBackup_LOC("BMC");
        }



        private void Reset_EXE_Click(object sender, EventArgs e)
        {
            ConsoleHandler.force_append_Notice("This will reset your game path...");
        }

        private void Reset_EXE_MouseHover(object sender, EventArgs e)
        {
            ConsoleHandler.force_append_Notice("This will reset your game path...");
        }
        private void ValidateBTD5_Click(object sender, EventArgs e)
        {
            SteamValidate("BTD5");
        }
        private void ValidateBTDB_Click(object sender, EventArgs e)
        {
            SteamValidate("BTDB");
        }
        private void ValidateBMC_Click(object sender, EventArgs e)
        {
            SteamValidate("BMC");
        }

        private void FontForPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append("Opening download for BTD Font for PC");
            Process.Start("https://drive.google.com/open?id=19CgzvVJWo1E7lP-YSzaezGW79grUalnY");
        }
        private void OnlineFontGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append("Opening Online BTD Font Generator...");
            Process.Start("https://fontmeme.com/bloons-td-battles-font/");
        }
        private void SpriteSheetDecompilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append("Opening Sprite Decompiler github link...");
            Process.Start("https://github.com/TheSubtleKiller/SpriteSheetRebuilder");
        }
        private void NewProject_From_Backup_Click(object sender, EventArgs e)
        {
            Serializer.SaveSettings();
            AddNewJet();
        }

        private void EZ_TowerEditor_Click(object sender, EventArgs e)
        {
            var ezTower = new EasyTowerEditor();
            string path = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\DartMonkey.tower";
            ezTower.path = path;
        }
        private void EZ_BloonEditor_Click(object sender, EventArgs e)
        {
            var ezBloon = new EZBloon_Editor();
            string path = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\BloonDefinitions\\Red.bloon";
            ezBloon.path = path;
        }
        private void EZCard_Editor_Click(object sender, EventArgs e)
        {
            var ezCard = new EZCard_Editor();
            string path = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\BattleCardDefinitions\\0.json";
            ezCard.path = path;
        }

        private void BTDBPasswordManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BattlesPassManager mgr = new BattlesPassManager();
            mgr.Show();
        }
        

        
        //Monkey Wrench stuff
        bool mwMessageShown = false;
        private void MonkeyWrenchToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            if(!mwMessageShown)
            {
                mwMessageShown = true;
                ConsoleHandler.append_Force("Monkey Wrench is a great tool made by Topper, that allows you to convert .jpng files into regular .png files, and back again.");
                ConsoleHandler.append_Force("Once it's opened, type     \'help\'     without quotes, to learn how to use the commands.");
            }
        }
        private void MonkeyWrenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConsoleHandler.append("Opening download for Monkey Wrench");
            Process.Start("https://drive.google.com/open?id=1lmQUXRRGHkuZIDqTzP2A3YhEDb5NfRuU");
        }


        //Save editor stuff
        bool seMessageShown = false;
        private void SaveEditorToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            if(!seMessageShown)
            {
                seMessageShown = true;
                ConsoleHandler.append_Force("This save editor was originally made by Vadmeme on github. They're allowing anyone to use it, so we added it to toolbox.");
                ConsoleHandler.append_Force("If you're interested, the link for it is here: https://github.com/Vadmeme/BTDSaveEditor");
            }
        }
        private void BTD5SaveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string save = Serializer.cfg.SavePathBTD5;
            if (save == null || save == "")
            {
                SaveEditor.TryFindSteam.FindSaveFiles();
                save = Serializer.cfg.SavePathBTD5;
                if (save == "" || save == null)
                {
                    return;
                }
            }
            SaveEditor.SaveEditor.DecryptSave("BTD5", "");

            string btd5Save = SaveEditor.SaveEditor.savemodDir + "\\BTD5_Profile.save";
            if (File.Exists(btd5Save))
                JsonEditorHandler.OpenFile(btd5Save);
            else
                ConsoleHandler.append("Decrypted save file not found");
        }
        private void BTDBSaveModToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Thanks for trying the save editor for BTDB. Unfortunately, we haven't been able to successfully edit the save for BTD Battles" +
                ", as it resets the save when you run it. This happens with BTD Battles Only, but as a result " +
                "you will only be able to read the save file, and not edit it. Hopefully we figure this out soon");
            string save = Serializer.cfg.SavePathBTDB;
            if (save == null || save == "")
            {
                SaveEditor.TryFindSteam.FindSaveFiles();
                save = Serializer.cfg.SavePathBTDB;
                if (save == "" || save == null)
                {
                    return;
                }
            }
            SaveEditor.SaveEditor.DecryptSave("BTDB", "");

            string btdbSave = SaveEditor.SaveEditor.savemodDir + "\\BTDB_Profile.save";
            if (File.Exists(btdbSave))
                JsonEditorHandler.OpenFile(btdbSave);
            else
                ConsoleHandler.append("Decrypted save file not found");
        }
        private void BrowseForSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string browsedSave = SaveEditor.TryFindSteam.BrowseForSave();
            SaveEditor.SaveEditor.DecryptSave("", browsedSave);

            string save = SaveEditor.SaveEditor.savemodDir + "\\UnknownGame_Profile.save";
            if (File.Exists(save))
                JsonEditorHandler.OpenFile(save);
            else
                ConsoleHandler.append("Decrypted save file not found");
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void PopulateNKHMewnu()
        {
            Launch_Program_ToolStrip.DropDownItems.Clear();

            if (CurrentProjectVariables.GameName == "BTD5")
            {
                Launch_Program_ToolStrip.DropDownItems.Add("With NKHook");
                Launch_Program_ToolStrip.DropDownItems.Add("Without NKHook");
                Launch_Program_ToolStrip.DropDownItems.Add(new ToolStripSeparator());
                Launch_Program_ToolStrip.DropDownItems.Add("NKHook Plugin Manager");
            }
        }

        private void Launch_Program_ToolStrip_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (!NKHook.CanUseNKH())
                return;

            if(e.ClickedItem.Text == "With NKHook")
            {
                if (NKHook.DoesNkhExist())
                {
                    if(CurrentProjectVariables.UseNKHook == false && CurrentProjectVariables.DontAskAboutNKH == false)
                    {
                        AlwaysUseNKH ask = new AlwaysUseNKH(true);
                        ask.Show();
                    }
                    else
                        CompileJet("launch nkh");
                }
                else
                {
                    ConsoleHandler.force_append_Notice("Unable to locate NKHook5-Injector.exe. Opening Get NKHook message... You need to launch without it until you download NKHook.");
                    NKHook_Message msg = new NKHook_Message();
                    msg.Show();
                }
            }
            if (e.ClickedItem.Text == "Without NKHook")
            {
                CompileJet("launch");
            }
            if (e.ClickedItem.Text == "NKHook Plugin Manager")
            {
                NKHPluginMgr mgr = new NKHPluginMgr();   
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Browser b = new Browser("https://google.com");
            b.MdiParent = this;
            b.Show();
        }

        private void nKHookSiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Browser b = new Browser("https://nkhook.pro");
            b.MdiParent = this;
            b.Show();
        }

        private void WebBrowser_Button_Click(object sender, EventArgs e)
        {
            Browser b = new Browser(this,"https://duckduckgo.com/");
        }

        private void Tutorials_Button_Click(object sender, EventArgs e)
        {
            if (!Tutorials.HasFreshTutList())
            {
                ConsoleHandler.append("Aquiring a fresh list of availible tutorials");
                Tutorials.GetTutorialsList();
            }
        }

        private void Tutorials_Button_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            Tutorials.OpenTutorial(e.ClickedItem.Text);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit = true;
            ConsoleHandler.append("Closing toolbox");
            ProjectHandler.SaveProject();

            Serializer.cfg.EnableConsole = ConsoleHandler.console.Visible;
            Serializer.SaveSettings();

            if (New_JsonEditor.isJsonError)
                MessageBox.Show("One or more of your files has a JSON error! If you dont fix it your mod wont work and it will crash your game");


            //Application.Exit();
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CreditViewer cv = new CreditViewer();
            cv.GotCredits += Cv_GotCredits;
            cv.StartCreditsView();
        }

        private void Cv_GotCredits(object source, CreditViewer.CreditsEventArgs e)
        {
            Invoke((MethodInvoker)delegate {
                CreditViewer cred = new CreditViewer(e.GetInfo())
                {
                    MdiParent = this
                };
            });
        }
        private void contactUsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discordapp.com/invite/jj5Q7mA");
        }
    }
}