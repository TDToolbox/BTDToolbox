using Ionic.Zip;
using Ionic.Zlib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfig;
using static BTDToolbox.GeneralMethods;

namespace BTDToolbox
{
    public partial class ExtractingJet_Window : Form
    {
        //Project Variables
        public string livePath = Environment.CurrentDirectory;
        public static string switchCase = "";

        //Project name variables
        public static bool hasCustomProjectName = false;
        public static string currentProject = "";
        
        public static string customName = "";
        string projectDest = "";
        string sourceJet = "";
        JetForm jf;

        //zip variables
        public static string jetPassword;
        string exportPath = "";
        public static int totalFiles = 0;        
        public int filesTransfered = 0;
        public static bool launchProgram = false;
        bool cancelProgress;

        //Config variables
        ConfigFile programData;
        public string gameDir;
        public static string BTD5_Dir;
        public static string BTDB_Dir;
        public static string BackupGame = "";
        public string gameName;
        public string exePath;
        public string steamJetPath;





        //refactoring variables:
        //public string jetFile_Game = "";
        public string projectName_Identifier = "";
        public string fullProjName = "";
        public string jetFile_Game { get; set; }
        public string sourcePath { get; set; }
        public string destPath { get; set; }
        public string projName { get; set; }
        public string password { get; set; }

        //Threads
        Thread backgroundThread;

        public ExtractingJet_Window()
        {
            InitializeComponent();
            StartUp();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 120;
            this.Top = 120;
        }
        private void StartUp()
        {
            Deserialize_Config();
            string std = DeserializeConfig().CurrentGame;
            string exeName = "";
            string jetName = "";

            if (std == "BTDB")
            {
                gameDir = DeserializeConfig().BTDB_Directory;
                exeName = "Battles-Win.exe";
                jetName = "data.jet";
                
            }
            else
            {
                gameDir = DeserializeConfig().BTD5_Directory;
                exeName = "BTD5-Win.exe";
                jetName = "BTD5.jet";
                password = "Q%_{6#Px]]";
            }
            gameName = std;
            exePath = gameDir + "\\" + exeName;
            steamJetPath = gameDir + "\\Assets\\" + jetName;
            projectName_Identifier = "\\proj_" + gameName + "_";
            ValidateEXE(gameName);
        }
        public void Extract()
        {
            if (sourcePath == null || sourcePath == "")
                sourcePath = Environment.CurrentDirectory + "\\Backups\\" + gameName + "_Original.jet";

            Random rand = new Random();

            if (projName == null || projName == "")
            {
                int randName = rand.Next(1, 99999999);
                projName = randName.ToString();
            }
            fullProjName = projectName_Identifier + projName;

            //check password
            if (File.Exists(sourcePath) && gameName == "BTDB")
            {
                this.Hide();
                bool passRes = Bad_JetPass(sourcePath, password);
                if (passRes == true)
                {
                    DialogResult res = MessageBox.Show("You entered the wrong password. Would you like to try again?", "Wrong Password!", MessageBoxButtons.OKCancel);
                    if (res == DialogResult.OK)
                    {
                        var getpas = new Get_BTDB_Password();
                        getpas.projName = projName;
                        getpas.isExtracting = true;
                        getpas.Show();
                    }
                    else
                        this.Close();
                }
                else
                {
                    this.Show();
                    backgroundThread = new Thread(Extract_OnThread);
                    backgroundThread.Start();
                }
            }
            else
            {
                backgroundThread = new Thread(Extract_OnThread);
                backgroundThread.Start();
            }
        }
        private void Extract_OnThread()
        {
            destPath = Environment.CurrentDirectory + "\\" + fullProjName;
            DirectoryInfo dinfo = new DirectoryInfo(destPath);
            if (!dinfo.Exists)
            {
                if (File.Exists(sourcePath))
                {
                    ConsoleHandler.appendLog("Creating project files...");

                    ZipFile archive = new ZipFile(sourcePath);
                    archive.Password = password;
                    totalFiles = archive.Count();
                    filesTransfered = 0;
                    archive.ExtractProgress += ZipExtractProgress;
                    archive.ExtractAll(destPath);

                    ConsoleHandler.appendLog("Project files created at: " + fullProjName);
                    Invoke((MethodInvoker)delegate {
                        jf = new JetForm(dinfo, TD_Toolbox_Window.getInstance(), dinfo.Name);
                        jf.MdiParent = TD_Toolbox_Window.getInstance();
                        jf.Show();
                    });
                }
                else
                    ConsoleHandler.appendLog("Failed to find file to extract");
            }
            else
            {
                 DialogResult varr = MessageBox.Show("A project with this name already exists. Do you want to replace it?", "", MessageBoxButtons.OKCancel);
                if (varr == DialogResult.OK)
                {
                    MessageBox.Show("Overwriting existing project... Please close the current Jet Explorer window.");
                    ConsoleHandler.appendLog("Deleting existing project....");
                    DeleteDirectory(dinfo.ToString());
                    ConsoleHandler.appendLog("Project Deleted. Creating new project...");
                    Extract_OnThread();
                }
                if (varr == DialogResult.Cancel)
                {
                    this.Invoke(new Action(() => this.Close()));
                    backgroundThread.Abort();
                }
            }
            this.Invoke(new Action(() => this.Close()));
            backgroundThread.Abort();
        }

        private void SwitchCase()
        {
            

            switch (switchCase)
            {
                case "output":
                    this.Text = "Compiling....";
                    OutputJet();
                    break;
                case "compile":
                    this.Text = "Compiling....";
                    compile_and_overwrite_Jet();
                    break;
                case "decompile":
                    this.Text = "Decompiling....";
                    Decompile_NEW2();
                    break;
                /*case "decompile backup":
                    this.Text = "Decompiling....";
                    SetProjectName.gameName = gameName;
                    var newProject = new SetProjectName();
                    newProject.Show();
                    break;
                case "compile BTDB":
                    this.Text = "Compiling....";
                    ExportBTDB();
                    break;  
                case "output BTDB":
                    this.Text = "Compiling....";
                    ExportBTDB();
                    break;
                case "backup":
                    this.Text = "Restoring backup....";
                    restoreGame();
                    break;
                case "clean backup":
                    this.Text = "replacing backup....";
                    Clear_Backup();
                    break;
                case "launch":
                    this.Text = "Compiling....";
                    launchProgram = true;
                    compile_and_overwrite_Jet();
                    break;*/
                default:
                    MessageBox.Show("You did not enter a valid operation! There is an issue with the code");
                    break;
            }
        }
        private void ExtractJet_Window_Load(object sender, EventArgs e)
        {
            CurrentFileProgress_Label.Hide();
            CurrentFileProgress.Hide();

            TotalProgress_ProgressBar.Location = new Point(TotalProgress_ProgressBar.Location.X, TotalProgress_ProgressBar.Location.Y - 50);
            TotalProgress_Label.Location = new Point(TotalProgress_Label.Location.X, TotalProgress_Label.Location.Y - 50);
            richTextBox1.Location = new Point(richTextBox1.Location.X, richTextBox1.Location.Y - 50);
            label1.Location = new Point(label1.Location.X, label1.Location.Y - 50);
            this.Size = new Size(this.Size.Width, this.Size.Height - 50);
            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 50);
        }
        private void ZipExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.EventType != ZipProgressEventType.Extracting_BeforeExtractEntry)
                return;
            if (filesTransfered >= (totalFiles / 100))
            {
                try
                {
                    TotalProgress_ProgressBar.Invoke(new Action(() => TotalProgress_ProgressBar.Value = 100 * filesTransfered / totalFiles));
                }
                catch (Exception ex)
                {
                    PrintError(ex.Message);
                }
            }

            filesTransfered++;
        }
        private void ZipCompileProgress(object sender, AddProgressEventArgs e)
        {
            
            if (e.EventType != ZipProgressEventType.Adding_AfterAddEntry)
                return;
            if (filesTransfered >= (totalFiles/100))
            {
                try
                {
                    TotalProgress_ProgressBar.Invoke(new Action(() => TotalProgress_ProgressBar.Value = 100 * filesTransfered / totalFiles));
                }
                catch(Exception ex)
                {
                    PrintError(ex.Message);
                }
            }
            filesTransfered++;
        }

        //
        //Class wide functions
        //
        private bool is_GamePath_Valid()
        {
            if (programData.CurrentGame == "BTD5")
            {
                if (BTD5_Dir == null || BTD5_Dir == "")
                {
                    ConsoleHandler.appendLog("Launch Directory not detected or is invalid...");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (programData.CurrentGame == "BTDB")
            {
                if (BTDB_Dir == null || BTDB_Dir == "")
                {
                    ConsoleHandler.appendLog("Launch Directory not detected or is invalid...");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        private void browseForExe()
        {
            string game = "";

            if (programData.CurrentGame == "BTD5")
            {
                game = "BTD5-Win.exe";
            }
            else if (programData.CurrentGame == "BTDB")
            {
                game = "Battles-Win.exe";
            }

            MessageBox.Show("Please browse for " + game);

            OpenFileDialog fileDiag = new OpenFileDialog();
            fileDiag.Title = "Open game exe";
            fileDiag.DefaultExt = "exe";
            fileDiag.Filter = "Exe files (*.exe)|*.exe|All files (*.*)|*.*";
            fileDiag.Multiselect = false;

            //
            //I can clean this up by using game in the parts below
            //
            if (fileDiag.ShowDialog() == DialogResult.OK)
            {
                if (programData.CurrentGame == "BTD5")
                {
                    if (fileDiag.FileName.Contains("BTD5-Win.exe"))
                    {
                        BTD5_Dir = fileDiag.FileName.Replace("\\BTD5-Win.exe", "");
                        gameDir = fileDiag.FileName.Replace("\\BTD5-Win.exe", "");
                        exePath = gameDir + "\\BTD5-Win.exe";
                        steamJetPath = gameDir + "\\Assets\\BTD5.jet";
                    }
                    else
                    {
                        MessageBox.Show("You didn't select BTD5-Win.exe\r\nYou need to select the proper exe.\r\nIf you are trying to mod a different game, please try creating a project for that game instead");
                        ConsoleHandler.appendLog("Invalid game selected... Please try again, or try making a project for a different game.");
                    }
                }
                else if (programData.CurrentGame == "BTDB")
                {
                    if (fileDiag.FileName.Contains("Battles-Win.exe"))
                    {
                        BTDB_Dir = fileDiag.FileName.Replace("\\Battles-Win.exe", "");
                        gameDir = fileDiag.FileName.Replace("\\Battles-Win.exe", "");
                        exePath = gameDir + "\\Battles-Win.exe";
                        steamJetPath = gameDir + "\\Assets\\data.jet";
                    }
                    else
                    {
                        MessageBox.Show("You didn't select Battles-Win.exe\r\nYou need to select the proper exe.\r\nIf you are trying to mod a different game, please try creating a project for that game instead");
                        ConsoleHandler.appendLog("Invalid game selected... Please try again, or try making a project for a different game.");
                    }
                }

                Serializer.SaveConfig(this, "directories", programData);
                ConsoleHandler.appendLog("Launch settings saved in settings.json");
            }
        }
        private void Deserialize_Config()
        {
            programData = Serializer.Deserialize_Config();
        }
        private void Clear_Backup()
        {
            ConsoleHandler.appendLog("Clearing Backup .jet for " + BackupGame);
            File.Delete(livePath + "\\Backups\\" + BackupGame + "_Original.jet");
            Validate_Backup();
        }
        private void Validate_Backup()
        {
            ConsoleHandler.appendLog("Attempting to validate backup...");
            string backupName = programData.CurrentGame + "_Original.jet";
            if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\" + backupName))
            {
                ConsoleHandler.appendLog("Jet backup not found for" + programData.CurrentGame + ", creating one...");
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups");
                File.Copy(steamJetPath, Environment.CurrentDirectory + "\\Backups\\" + backupName);
                ConsoleHandler.appendLog("Backup created");
            }
            else
            {
                ConsoleHandler.appendLog("Backup successfully validated.");
            }
        }


        //
        //Compile functions
        //
        private void compile_and_overwrite_Jet()
        {
            this.Show();
            Validate_Backup();
            if (programData.CurrentGame == "BTD5")
            {
                exportPath = BTD5_Dir + "\\Assets\\BTD5.jet";
            }
            else if (programData.CurrentGame == "BTDB")
            {
                exportPath = BTDB_Dir + "\\Assets\\data.jet";
            }
            backgroundThread = new Thread(CompileThread);
            backgroundThread.Start();
        } 
        private void CompileThread()
        {
            ConsoleHandler.appendLog("Compiling jet...");
            DirectoryInfo projDir = new DirectoryInfo(programData.LastProject);
            int numFiles = Directory.GetFiles((projDir.ToString()), "*", SearchOption.AllDirectories).Length;
            int numFolders = Directory.GetDirectories(projDir.ToString(), "*", SearchOption.AllDirectories).Count();
            totalFiles = numFiles + numFolders;


            filesTransfered = 0;
            ZipFile toExport = new ZipFile();
            if (programData.CurrentGame != "BTDB")
            {
                jetPassword = "Q%_{6#Px]]";
            }

            toExport.Password = jetPassword;
            toExport.AddProgress += ZipCompileProgress;
            toExport.AddDirectory(projDir.FullName);
            toExport.Encryption = EncryptionAlgorithm.PkzipWeak;
            toExport.Name = exportPath;
            toExport.CompressionLevel = CompressionLevel.Level6;
            toExport.Save();
            toExport.Dispose();

            if (cancelProgress == true)
            {
                toExport.Dispose();
            }
            else
            {
                if (launchProgram == true)
                {
                    Process.Start(exePath);
                    ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.\r\n");
                }
            }
            try
            {
                this.Invoke(new Action(() => this.Close()));
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
            backgroundThread.Abort();

        }
        private void OutputJet()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Export .jet";
            sfd.DefaultExt = "jet";
            sfd.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                exportPath = sfd.FileName;
                backgroundThread = new Thread(CompileThread);

                backgroundThread.Start();
            }
        }
        private void ExportBTDB()
        {
            TD_Toolbox_Window.gameName = "BTDB";
            Get_BTDB_Password.compileOperation = switchCase;
            Get_BTDB_Password.projectName = currentProject;
            Get_BTDB_Password.setPassword = true;
            var setPass = new Get_BTDB_Password();

            //Serializer.SaveConfig(this, "jet explorer", programData);
            if (jetPassword != null)
            {
                setPass.Close();
                if (switchCase.Contains("output"))
                    OutputJet();
                else if (switchCase.Contains("compile"))
                    compile_and_overwrite_Jet();
            }
            else
                setPass.Show();
            //this.Close();
        }

        //
        //Decompile functions
        //
        private void Decompile_NEW2()
        {
            /*Random rand = new Random();
            sourceJet = livePath + "\\Backups\\" + gameName +"_Original.jet";
            projectName = (livePath + "\\proj_" + gameName + "_");

            if (hasCustomProjectName)
            {
                projectName = projectName + customName;
            }
            else
            {
                int randName = rand.Next(1, 99999999);
                projectName = projectName + randName;
            }
            TD_Toolbox_Window.projName = projectName;
            projectDest = projectName;

            backgroundThread = new Thread(DecompileThread);
            backgroundThread.Start();

            DirectoryInfo dinfo = new DirectoryInfo(projectName);
            jf = new JetForm(dinfo, TD_Toolbox_Window.getInstance(), dinfo.Name);
            jf.MdiParent = TD_Toolbox_Window.getInstance();*/
        }

        private void DecompileThread()
        {
            /*//this.Show();
            ZipFile archive = new ZipFile(sourceJet);
            //ConsoleHandler.appendLog("Creating project files...");

            if (gameName != "BTDB")
            {
                jetPassword = "Q%_{6#Px]]";
            }

            archive.Password = jetPassword;
            //Extract and count progress
            DirectoryInfo dinfo = new DirectoryInfo(projectName);

            bool badPass = false;
            if (!dinfo.Exists)
            {
                using (ZipFile zip = ZipFile.Read(sourceJet))
                {
                    totalFiles = archive.Count();
                    filesTransfered = 0;
                    archive.ExtractProgress += ZipExtractProgress;
                    
                    try
                    {
                        if (badPass != true)
                        {
                            archive.ExtractAll(projectDest);
                            ExtractingJet_Window.customName = "";
                        }
                        else
                        {
                            MessageBox.Show("Badd Password error. Contact staff if you are seeing this");
                        }
                    }
                    catch (BadPasswordException)
                    {
                        DeleteDirectory(dinfo.ToString());
                        badPass = true;
                        MessageBox.Show("You entered an invalid password. Check console for more details.");
                        ConsoleHandler.appendLog("You entered an invalid password. You need to enter the CORRECT password for the version of BTD Battles that you are trying to mod");                        
                    }
                    if (badPass != true)
                    {
                        ConsoleHandler.appendLog("Project files created at: " + projectName);
                        jf.Invoke(new Action(() => jf.Show()));
                    }
                }
            }
            else
            {
                //this.Hide();
                
                DialogResult varr = MessageBox.Show("A project with this name already exists. Do you want to replace it, or choose a different project name?", "", MessageBoxButtons.OKCancel);
                if (varr == DialogResult.OK)
                {
                    MessageBox.Show("Overwriting existing project... Please close the current Jet Explorer window.");
                    ConsoleHandler.appendLog("Deleting existing project....");
                    DeleteDirectory(projectName);
                    ConsoleHandler.appendLog("Project Deleted. Creating new project...");
                    DecompileThread();
                }
                if (varr == DialogResult.Cancel)
                {
                    SetProjectName.gameName = gameName;
                    var reopenSetProjectName = new SetProjectName();
                    reopenSetProjectName.Show();
                    this.Invoke(new Action(() => this.Close()));
                    backgroundThread.Abort();
                }
            }
            this.Invoke(new Action(() => this.Close()));
            backgroundThread.Abort();*/
        }
        //
        //Restore Backup
        //
        private void restoreGame()
        {
            backgroundThread = new Thread(restoreGame_Thread);
            backgroundThread.Start();
        }
        private void restoreGame_Thread()
        {
            string tempString = "";
            if (BackupGame == "BTD5")
            {
                tempString = steamJetPath;
                steamJetPath = BTD5_Dir + "\\Assets\\BTD5.jet";
            }
            else if (BackupGame == "BTDB")
            {
                tempString = steamJetPath;
                steamJetPath = BTDB_Dir + "\\Assets\\data.jet";
            }
            ConsoleHandler.appendLog("Restoring backup .jet");
            File.Delete(steamJetPath);
            File.Copy(Environment.CurrentDirectory + "\\Backups\\"+ BackupGame + "_Original.jet", steamJetPath);
            ConsoleHandler.appendLog("Backup restored\r\n");
            steamJetPath = tempString;
            //this.Invoke(new Action(() => this.Close()));
            backgroundThread.Abort();
        }
        private void PrintError(string exception)
        {
            ConsoleHandler.appendLog("An error occured that may prevent the program from running properly.\r\nThe error is below: \r\n\r\n" + exception + "\r\n");
            this.Close();
        }
        private void ExtractingJet_Window_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancelProgress = true;
        }
        public static void DeleteDirectory(string path)
        {
            var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };

            foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
            {
                info.Attributes = FileAttributes.Normal;
            }

            directory.Delete(true);
        }
    }
}