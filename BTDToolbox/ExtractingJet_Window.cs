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
        public static string projectName = "";
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

        //Config variables
        ConfigFile programData;
        public string gameDir;
        public static string BTD5_Dir;
        public static string BTDB_Dir;
        public static string BackupGame = "";
        public string gameName;
        public string exePath;
        public string steamJetPath;

        //Threads
        Thread backgroundThread;

        public ExtractingJet_Window()
        {
            InitializeComponent();
            StartUp();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 120;
            this.Top = 120;

            if (is_GamePath_Valid() == false)
            {
                ConsoleHandler.appendLog("Error identifying Game Directory or Backups. Please browse for your EXE again...\r\n");
                browseForExe();
                if (is_GamePath_Valid() == false)
                {
                    MessageBox.Show("The EXE was not found... Please try again.");
                    this.Close();
                }
                else
                {
                    Validate_Backup();
                    SwitchCase();
                }   
            }
            else
            {
                Validate_Backup();
                SwitchCase();
            }
        }
        private void StartUp()
        {
            Deserialize_Config();
            BTD5_Dir = programData.BTD5_Directory;
            BTDB_Dir = programData.BTDB_Directory;

            if (programData.CurrentGame == "BTD5")
            {
                gameDir = BTD5_Dir;
                gameName = "BTD5";
                steamJetPath = gameDir + "\\Assets\\btd5.jet";
                exePath = BTD5_Dir + "\\BTD5-Win.exe";
            }
            else if (programData.CurrentGame == "BTDB")
            {
                gameDir = BTDB_Dir;
                gameName = "BTDB";
                steamJetPath = gameDir + "\\Assets\\data.jet";
                exePath = BTDB_Dir + "\\Battles-Win.exe";
            }
        }
        private void SwitchCase()
        {
            switch (switchCase)
            {
                case "output":
                    this.Text = "Compiling....";
                    OutputJet();
                    break;
                case "output BTDB":
                    this.Text = "Compiling....";
                    ExportBTDB();
                    break;
                case "compile":
                    this.Text = "Compiling....";
                    compile_and_overwrite_Jet();
                    break;
                case "compile BTDB":
                    this.Text = "Compiling....";
                    ExportBTDB();
                    break;
                case "launch":
                    this.Text = "Compiling....";
                    launchProgram = true;
                    compile_and_overwrite_Jet();
                    break;
                case "decompile":
                    this.Text = "Decompiling....";
                    Decompile_NEW2();
                    break;
                case "decompile backup":
                    this.Text = "Decompiling....";
                    SetProjectName.gameName = gameName;
                    var newProject = new SetProjectName();
                    newProject.Show();
                    break;
                case "backup":
                    this.Text = "Restoring backup....";
                    restoreGame();
                    break;
                case "clean backup":
                    this.Text = "replacing backup....";
                    Clear_Backup();
                    break;
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
                TotalProgress_ProgressBar.Invoke(new Action(() => TotalProgress_ProgressBar.Value = 100 * filesTransfered / totalFiles));
            }

            filesTransfered++;
        }
        private void ZipCompileProgress(object sender, AddProgressEventArgs e)
        {
            if (e.EventType != ZipProgressEventType.Adding_AfterAddEntry)
                return;
            if (filesTransfered >= (totalFiles/100))
            {
                TotalProgress_ProgressBar.Invoke(new Action(() => TotalProgress_ProgressBar.Value = 100 * filesTransfered / totalFiles));
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
            ConsoleHandler.appendLog("Clearing Backup .jet");
            File.Delete(livePath + "\\Backups\\" + programData.CurrentGame + "_Original.jet");
            Validate_Backup();
        }
        private void Validate_Backup()
        {
            string backupName = programData.CurrentGame + "_Original.jet";
            if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\" + backupName))
            {
                ConsoleHandler.appendLog("Jet backup not found, creating one...");
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups");
                File.Copy(steamJetPath, Environment.CurrentDirectory + "\\Backups\\" + backupName);
                ConsoleHandler.appendLog("Backup done");
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


            if (launchProgram == true)
            {
                Process.Start(exePath);
                ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.\r\n");
            }
            this.Invoke(new Action(() => this.Close()));
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
            programData.CurrentGame = "BTDB";
            programData.LastProject = currentProject;
            Get_BTDB_Password.compileOperation = switchCase;
            Get_BTDB_Password.projectName = currentProject;
            Get_BTDB_Password.setPassword = true;
            var setPass = new Get_BTDB_Password();
            
            setPass.Show();
            this.Close();
        }

        //
        //Decompile functions
        //
        private void Decompile_NEW2()
        {
            Random rand = new Random();
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
            jf.MdiParent = TD_Toolbox_Window.getInstance();
        }

        private void DecompileThread()
        {
            ZipFile archive = new ZipFile(sourceJet);
            ConsoleHandler.appendLog("Creating project files...");

            if (gameName != "BTDB")
            {
                jetPassword = "Q%_{6#Px]]";
            }

            archive.Password = jetPassword;
            //Extract and count progress
            DirectoryInfo dinfo = new DirectoryInfo(projectName);

            this.Show();
            this.Refresh();

            bool badPass = false;
            if (!File.Exists(projectDest))
            {
                using (ZipFile zip = ZipFile.Read(sourceJet))
                {
                    totalFiles = archive.Count();
                    filesTransfered = 0;
                    archive.ExtractProgress += ZipExtractProgress;

                    try
                    {
                        archive.ExtractAll(projectDest);
                    }
                    //catch (BadPasswordException)
                    catch (Exception)
                    {
                        badPass = true;
                        MessageBox.Show("You entered an invalid password. Check console for more details.");
                        ConsoleHandler.appendLog("You entered an invalid password. You need to enter the CORRECT password for the version of BTD Battles that you are trying to mod");
                    }
                }
                if (badPass != true)
                {
                    ConsoleHandler.appendLog("Project files created at: " + projectName);
                    jf.Invoke(new Action(() => jf.Show()));
                }
            }
            else
            {
                this.Hide();

                DialogResult varr = MessageBox.Show("A project with this name already exists. Do you want to replace it, or choose a different project name?", "", MessageBoxButtons.OKCancel);
                if (varr == DialogResult.OK)
                {
                    MessageBox.Show("Overwriting existing project... Please close the current Jet Explorer window.");
                    Directory.Delete(projectName, true);
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

            /*this.Invoke(new Action(() => this.Close()));
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
    }
}