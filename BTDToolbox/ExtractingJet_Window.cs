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
using static BTDToolbox.ProjectConfigs;

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
        string exportPath = "";
        public static int totalFiles = 0;        
        public int filesTransfered = 0;
        public static bool launchProgram = false;

        //Config variables
        LaunchSettings_Config launchSettings_Config;
        string configOutput = "";
        public string gameDir;
        public string exePath;
        public string steamJetPath;

        //Threads
        Thread thread_RestoreGame;
        Thread compThread;
        Thread thread_DecompileJet;

        public ExtractingJet_Window()
        {
            InitializeComponent();
            ReadConfig();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 120;
            this.Top = 120;

            if (is_GamePath_Valid() == false)
            {
                ConsoleHandler.appendLog("Error identifying Game Directory or Backups. Please browse for your EXE again...\r\n");
                browseForExe();
                Validate_Backup();
            }

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
            if (gameDir == null || gameDir == "")
            {
                MessageBox.Show("No launch dir defined or is wrong. Please select BTD5-Win.exe or Battles-Win.exe so the program can export your project...");
                ConsoleHandler.appendLog("Launch Directory not detected or is invalid...");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void browseForExe()
        {
            OpenFileDialog fileDiag = new OpenFileDialog();
            fileDiag.Title = "Open game exe";
            fileDiag.DefaultExt = "exe";
            fileDiag.Filter = "Exe files (*.exe)|*.exe|All files (*.*)|*.*";
            fileDiag.Multiselect = false;
            if (fileDiag.ShowDialog() == DialogResult.OK)
            {
                gameDir = fileDiag.FileName.Replace("\\BTD5-Win.exe", "");
                exePath = gameDir + "\\BTD5-Win.exe";
                steamJetPath = gameDir + "\\Assets\\BTD5.jet";

                SerializeConfig();
                ConsoleHandler.appendLog("Launch settings saved in \\config\\btd5_launch_settings.json");
            }
        }
        private void SerializeConfig()
        {
            try
            {
                launchSettings_Config = new LaunchSettings_Config(gameDir);
                configOutput = JsonConvert.SerializeObject(launchSettings_Config);

                StreamWriter writeMainForm = new StreamWriter(livePath + "\\config\\btd5_launch_settings.json", false);
                writeMainForm.Write(configOutput);
                writeMainForm.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("There were problems with saving configurations. Things may not act normally.");
            }
        }
        private void ReadConfig()
        {
            try
            {
                string json = File.ReadAllText(livePath + "\\config\\btd5_launch_settings.json");
                LaunchSettings_Config deserialized_LaunchSettings = JsonConvert.DeserializeObject<LaunchSettings_Config>(json);

                gameDir = deserialized_LaunchSettings.GameDir;
                exePath = gameDir + "BTD5-Win.exe";
                steamJetPath = gameDir + "\\Assets\\BTD5.jet";
            }
            catch (FileNotFoundException)
            {
                SerializeConfig();
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(livePath + "\\config");
            }
        }
        private void Clear_Backup()
        {
            ConsoleHandler.appendLog("Clearing Backup .jet");
            File.Delete(livePath + "\\Backups\\Original.jet");
            Validate_Backup();
        }
        private void Validate_Backup()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\Original.jet"))
            {
                ConsoleHandler.appendLog("Jet backup not found, creating one...");
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups");
                File.Copy(steamJetPath, Environment.CurrentDirectory + "\\Backups\\Original.jet");
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
            exportPath = steamJetPath;
            compThread = new Thread(CompileThread);
            compThread.Start();
        } 
        private void CompileThread()
        {
            /*try
            {*/
                ConsoleHandler.appendLog("Compiling jet...");
                DirectoryInfo projDir = new DirectoryInfo(livePath + "\\" + currentProject);

                int numFiles = Directory.GetFiles((projDir.ToString()), "*", SearchOption.AllDirectories).Length;
                int numFolders = Directory.GetDirectories(projDir.ToString(), "*", SearchOption.AllDirectories).Count();
                totalFiles = numFiles + numFolders;


                filesTransfered = 0;
                ZipFile toExport = new ZipFile();
                toExport.Password = "Q%_{6#Px]]";
                toExport.AddProgress += ZipCompileProgress;
                toExport.AddDirectory(projDir.FullName);
                toExport.Encryption = EncryptionAlgorithm.PkzipWeak;
                toExport.Name = exportPath;
                toExport.CompressionLevel = CompressionLevel.Level6;

                toExport.Save();
                toExport.Dispose();

                if(launchProgram == true)
                {
                    Process.Start(gameDir + "//BTD5-Win.exe");
                    ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.\r\n");
                }
                this.Invoke(new Action(() => this.Close()));
                compThread.Abort();
            /*}
            catch(Exception)
            {
                ConsoleHandler.appendLog("Process was cancelled by the user, or there was an error");
            }*/
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
                compThread = new Thread(CompileThread);
                compThread.Start();
            }
        }


        //
        //Decompile functions
        //
        private void Decompile_NEW2()
        {
            Random rand = new Random();
            sourceJet = livePath + "\\Backups\\Original.jet";

            if (hasCustomProjectName)
            {
                projectName = (livePath + "\\proj_" + customName);
            }
            else
            {
                int randName = rand.Next(1, 99999999);
                projectName = (livePath + "\\proj_" + randName);
            }
            projectDest = projectName;

            thread_DecompileJet = new Thread(DecompileThread);
            thread_DecompileJet.Start();

            DirectoryInfo dinfo = new DirectoryInfo(projectName);
            jf = new JetForm(dinfo, TD_Toolbox_Window.getInstance(), dinfo.Name);
            jf.MdiParent = TD_Toolbox_Window.getInstance();
        }

        private void DecompileThread()
        {
            ZipFile archive = new ZipFile(sourceJet);
            archive.Password = "Q%_{6#Px]]";
            ConsoleHandler.appendLog("Creating project files...");

            //Extract and count progress
            DirectoryInfo dinfo = new DirectoryInfo(projectName);

            this.Show();
            this.Refresh();
            try
            {
                using (ZipFile zip = ZipFile.Read(sourceJet))
                {
                    totalFiles = archive.Count();
                    filesTransfered = 0;
                    archive.ExtractProgress += ZipExtractProgress;
                    archive.ExtractAll(projectDest);
                }
                ConsoleHandler.appendLog("Project files created at: " + projectName);
                jf.Invoke(new Action(() => jf.Show()));
            }
            catch (Exception)
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
                    var reopenSetProjectName = new SetProjectName();
                    reopenSetProjectName.Show();
                    this.Invoke(new Action(() => this.Close()));
                    thread_DecompileJet.Abort();
                }
            }

            this.Invoke(new Action(() => this.Close()));
            thread_DecompileJet.Abort();
        }
        //
        //Restore Backup
        //
        private void restoreGame()
        {
            thread_RestoreGame = new Thread(restoreGame_Thread);
            thread_RestoreGame.Start();
        }
        private void restoreGame_Thread()
        {
            ConsoleHandler.appendLog("Restoring backup .jet");
            File.Delete(steamJetPath);
            File.Copy(Environment.CurrentDirectory + "\\Backups\\Original.jet", steamJetPath);
            ConsoleHandler.appendLog("Backup restored\r\n");
            //this.Invoke(new Action(() => this.Close()));
            thread_RestoreGame.Abort();
        }
    }
}