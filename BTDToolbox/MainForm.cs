
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

namespace BTDToolbox
{
    public partial class TD_Toolbox_Window : Form
    {
        //Form variables
        string livePath = Environment.CurrentDirectory;
        public string projectDirPath;
        private static TD_Toolbox_Window toolbox;

        string version = "Alpha 0.0.2";

        //Project Variables
        public DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
        Thread backupThread;

        //Config variables
        ConfigFile programData;
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
            Deserialize_Config();
            bool existingUser = programData.ExistingUser;
            if (existingUser == false)
            {
                backupThread = new Thread(FirstTimeUse);
                backupThread.Start();
            }

            this.Size = new Size(programData.Main_SizeX, programData.Main_SizeY);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(programData.Main_PosX, programData.Main_PosY);
            this.Font = new Font("Microsoft Sans Serif", programData.Main_FontSize);
            if (programData.Main_Fullscreen)
                this.WindowState = FormWindowState.Maximized;

            enableConsole = programData.EnableConsole;
            projectDirPath = programData.LastProject_Path;
            lastProject = programData.LastProject;
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
            MessageBox.Show("Welcome to BTD Toolbox! This is a place holder. IF you are seeing this message, please contact staff");
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
                if(dinfo.Exists)
                {
                    foreach (JetForm o in JetProps.get())
                    {
                        if (o.projName == projName)
                        {
                            MessageBox.Show("The project is already opened..");
                            return;
                        }
                    }
                    JetForm jf = new JetForm(dinfo, this, projName);
                    jf.MdiParent = this;
                    jf.Show();
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
        public void ImportNewJet_Click(object sender, EventArgs e)
        {
            AddNewJet();
        }
        public static void AddNewJet()
        {
            OpenFileDialog fileDiag = new OpenFileDialog();

            fileDiag.Title = "Open .jet";
            fileDiag.DefaultExt = "jet";
            fileDiag.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            fileDiag.Multiselect = false;
            if (fileDiag.ShowDialog() == DialogResult.OK)
            {
                file = fileDiag.FileName;
                byte[] jetBytes = File.ReadAllBytes(file).Take(2).ToArray();
                string bytes = "" + (char)jetBytes[0] + (char)jetBytes[1];

                if (bytes == "PK")
                {
                    var setProjectName = new SetProjectName();
                    //ExtractingJet_Window.file = file;     //come back to this. Otherwise it will open twice
                    setProjectName.Show();
                }
                else
                {
                    MessageBox.Show("Error!! Not a valid .Jet File. Please try again...");
                }
            }
        }
        private void OpenExistingProject_Click(object sender, EventArgs e)
        {
            BrowseForExistingProjects();
        }
        private void BrowseForExistingProjects()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Browse for an existing project";
            dialog.Multiselect = false;
            dialog.InitialDirectory = livePath;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string selected = dialog.FileName;
                DirectoryInfo dirInfo = new DirectoryInfo(selected);
                string[] split = dialog.FileName.Split('\\');
                string name = split[split.Length - 1];
                if (!name.StartsWith("proj_"))
                {
                    MessageBox.Show("Error!! Not a valid project file. Please try again...");
                }
                else
                {
                    if (name.StartsWith("proj_BTD5"))
                    {
                        gameName = "BTD5";
                    }
                    else if (name.StartsWith("proj_BTDB"))
                    {
                        gameName = "BTDB";
                    }
                    projName = dialog.FileName;
                    Serializer.SaveConfig(this, "game", programData);
                    JetForm jf = new JetForm(dirInfo, this, name);
                    jf.MdiParent = this;
                    jf.Show();
                }
            }
        }
        //
        //UI Buttons
        //
        private void compileJet(string switchCase)
        {
            if (switchCase == "launch")
            {
                if (JetProps.get().Count == 1)
                {
                    
                    ExtractingJet_Window.switchCase = switchCase;
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
                ExtractingJet_Window.switchCase = switchCase;
                var compile = new ExtractingJet_Window();
            }
            
        }
        private void LaunchProgram_Click(object sender, EventArgs e)
        {
            compileJet("launch");
        }
        private void ToggleConsole_Click(object sender, EventArgs e)
        {
            if (ConsoleHandler.console.Visible)
            {
                ConsoleHandler.console.Hide();
                enableConsole = false;
            }
            else
            {
                enableConsole = true;
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
        private void OpenCredits_Click(object sender, EventArgs e)
        {
            CreditViewer cv = new CreditViewer();
            cv.MdiParent = this;
            cv.Show();
        }
        //
        //Config stuff
        //
        private void Deserialize_Config()
        {
            programData = Serializer.Deserialize_Config();
        }
        private void ExitHandling(object sender, EventArgs e)
        {
            if (ConsoleHandler.console.Visible)
            {
                enableConsole = true;
            }
            else
            {
                enableConsole = false;
            }
            Serializer.SaveConfig(this, "main", programData);
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
            } catch (Exception ex)
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
            compileJet("output");
        }

        private void NewProject_From_Backup_Click(object sender, EventArgs e)
        {

            //compileJet("decompile backup");
        }

        private void RemakeBackupjetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            compileJet("clean backup");
        }

        private void Open_Existing_JetFile_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Browse for an existing .jet file";
            ofd.DefaultExt = "jet";
            ofd.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ExtractingJet_Window.projectName = ofd.FileName;
                compileJet("decompile");
            }
        }

        private void New_BTD5_Proj_Click(object sender, EventArgs e)
        {
            gameName = "BTD5";
            programData.CurrentGame = gameName;
            Serializer.SaveConfig(this, "game", programData);
            compileJet("decompile backup");
        }

        private void New_BTDB_Proj_Click(object sender, EventArgs e)
        {
            gameName = "BTDB";
            programData.CurrentGame = gameName;
            Serializer.SaveConfig(this, "game", programData);
            compileJet("decompile backup");
        }

        private void Backup_BTD5_Click(object sender, EventArgs e)
        {
            ExtractingJet_Window.BackupGame = "BTD5";
            ExtractingJet_Window.switchCase = "backup";
            var compile = new ExtractingJet_Window();
        }

        private void Backup_BTDB_Click(object sender, EventArgs e)
        {
            ExtractingJet_Window.BackupGame = "BTDB";
            ExtractingJet_Window.switchCase = "backup";
            var compile = new ExtractingJet_Window();
        }
    }
}