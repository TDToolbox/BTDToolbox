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
        public static bool isOutput;
        public static bool isDecompiling;
        public string livePath = Environment.CurrentDirectory;
        public static string switchCase = "";
        LaunchSettings_Config launchSettings_Config;
        string configOutput;

        //Project name variables
        public static bool hasCustomProjectName;
        public static string projectName;
        public static string customName;
        public static int filesInJet;

        //Extraction variables
        public static string file;
        public static int totalFiles;
        public static int filesExtracted;
        public static bool isProjectCreated;
        

        //Compile variables
        public Thread compThread;
        public static bool launchProgram = false;
        public static string currentProject = "";
        public int filesTransfered = 0;
        public DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);
        public DirectoryInfo projDir = new DirectoryInfo(Environment.CurrentDirectory + "\\" + currentProject);

        //Config variables
        public string gameDir;
        public string exePath;
        public string steamJetPath;

        public ExtractingJet_Window()
        {
            InitializeComponent();
            ReadConfig();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 120;
            this.Top = 120;

            if (is_GamePath_Valid() == false)
            {
                browseForExe();
            }

            switch (switchCase)
            {
                case "output":
                    this.Text = "Compiling....";
                    Outputting();
                    break;
                case "compile":
                    this.Text = "Compiling....";
                    compileJet_V2();
                    break;
                case "decompile":
                    this.Text = "Decompiling....";
                    Decompiling_NEW();
                    break;
                case "launch":
                    this.Text = "Compiling....";
                    launchProgram = true;
                    compileJet_V2();
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

            //this.Show();
        }

        public DirectoryInfo decompile(string inputPath, DirectoryInfo targetFolder)
        {

            Random rand = new Random();
            ZipFile archive = new ZipFile(inputPath);
            archive.Password = "Q%_{6#Px]]";
            ConsoleHandler.appendLog("Creating project files...");

            if (hasCustomProjectName)
            {
                projectName = (livePath + "\\proj_" + customName);
            }
            else
            {
                int randName = rand.Next(10000000, 99999999);
                projectName = (livePath + "\\proj_" + randName);
            }
            //Extract and count progress
            DirectoryInfo dinfo = new DirectoryInfo(projectName);
            using (ZipFile zip = ZipFile.Read(inputPath))
            {
                totalFiles = archive.Count();
                filesExtracted = 0;
                archive.ExtractProgress += ZipExtractProgress;
                archive.ExtractAll(projectName);
                
                JetForm jf = new JetForm(dinfo, TD_Toolbox_Window.getInstance(), dinfo.Name);
                jf.MdiParent = TD_Toolbox_Window.getInstance();
                jf.Show();
            }
            ConsoleHandler.appendLog("Project files created at: " + projectName);
            this.Close();

            return dinfo;
        }

        private void ZipExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.TotalBytesToTransfer > 0)
            {
                label1.Refresh();
                CurrentFileProgress_Label.Refresh();
                richTextBox1.Text = e.CurrentEntry.FileName;
                richTextBox1.Refresh();
                CurrentFileProgress.Value = Convert.ToInt32(100 * e.BytesTransferred / e.TotalBytesToTransfer);
                e.BytesTransferred++;
            }
            if (e.EventType != ZipProgressEventType.Extracting_BeforeExtractEntry)
                return;
            filesExtracted++;
            TotalProgress_ProgressBar.Value = 100 * filesExtracted / totalFiles;
            this.Refresh();
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

        //Restore backup .jet
        public static void restoreGame()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\Original.jet"))
            {
                MessageBox.Show("No backup found that can be restored! Use steam to re-download the original .jet");
                return;
            }
            ConsoleHandler.appendLog("Restoring backup .jet");
            string gameJetPath = Settings.readGamePath() + "\\..\\Assets\\BTD5.jet";
            File.Delete(gameJetPath);
            File.Copy(Environment.CurrentDirectory + "\\Backups\\Original.jet", gameJetPath);
            ConsoleHandler.appendLog("Backup restored");
            MessageBox.Show("Backup .jet restored!");
        }









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
                //Settings.setGamePath(fileDiag.FileName);  //this has been replaced with a config files
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
        private void compileJet_V2()
        {
            this.Show();
            Validate_Backup();
            compThread = new Thread(CompileThread2);
            compThread.Start();
        }
        private void CompileThread2()
        {
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
            toExport.Name = steamJetPath;
            toExport.CompressionLevel = CompressionLevel.Level6;

            toExport.Save();
            toExport.Dispose();

            if(launchProgram == true)
            {
                Process.Start(gameDir + "//BTD5-Win.exe");
                //Process.Start(Settings.readGamePath());  //this has been replaced with a config files
                ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.");
            }
            this.Invoke(new Action(() => this.Close()));
            compThread.Abort();
        }
        public void compileJet_OLD()
        {
            //Just do a quick check to see if launchsettings.txt exists
            try
            {
                string path = File.ReadAllText(livePath + "\\launchsettings.txt");
                //MessageBox.Show(path);
                string gameJetPath = path + "\\..\\Assets\\BTD5.jet";

                DirectoryInfo projDir = new DirectoryInfo(Environment.CurrentDirectory + "\\" + currentProject);

                if (!File.Exists(Environment.CurrentDirectory + "\\Backups\\Original.jet"))
                {
                    ConsoleHandler.appendLog("Jet backup not found, creating one...");
                    Directory.CreateDirectory(Environment.CurrentDirectory + "\\Backups");
                    File.Copy(gameJetPath, Environment.CurrentDirectory + "\\Backups\\Original.jet");
                    ConsoleHandler.appendLog("Backup done");
                }
                ConsoleHandler.appendLog("Compiling jet...");

                //Thread comp = new Thread(CompileThread);
                //comp.Start();
                ConsoleHandler.appendLog("Jet compiled");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("No launch dir defined or is wrong. Please select BTD5.exe so the program can export your project...");
                ConsoleHandler.appendLog("No launch dir defined or is wrong.");
                OpenFileDialog fileDiag = new OpenFileDialog();
                fileDiag.Title = "Open game exe";
                fileDiag.DefaultExt = "exe";
                fileDiag.Filter = "Exe files (*.exe)|*.exe|All files (*.*)|*.*";
                fileDiag.Multiselect = false;
                if (fileDiag.ShowDialog() == DialogResult.OK)
                {
                    string file = fileDiag.FileName;
                    file = file.Replace("BTD5-Win.exe", "");
                    Settings.setGamePath(file);
                    string jet = file + "\\..\\Assets\\BTD5.jet";
                    ConsoleHandler.appendLog("Launch settings saved in launchSettings.txt");
                }
            }
            /*if (launchProgram)
            {

                if (isCompiling == false)
                {
                    Process.Start(Settings.readGamePath());
                    ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.");
                }
                else
                {
                    while (isCompiling == true)
                    {
                        //Thread.Sleep(1000);
                        this.Invoke(new Action(() => this.Refresh()));
                        richTextBox1.Invoke(new Action(() => richTextBox1.Refresh()));
                        TotalProgress_ProgressBar.Invoke(new Action(() => TotalProgress_ProgressBar.Refresh()));
                        if (isCompiling == false)
                        {
                            Process.Start(Settings.readGamePath());
                            ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.");
                        }
                    }
                }
            }*/
            //isCompiling = false;
        }
        private void Outputting()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Export .jet";
            sfd.DefaultExt = "jet";
            sfd.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                DirectoryInfo projDir = new DirectoryInfo(Environment.CurrentDirectory + "\\" + currentProject);
                ConsoleHandler.appendLog("Compiling jet...");
                //this.compile(projDir, sfd.FileName);
                ConsoleHandler.appendLog("Jet compiled");
            }
            isOutput = false;
            return;
        }

        private void Decompiling_NEW()
        {
            this.Text = "Extracting....";
            try
            {
                DirectoryInfo extract = this.decompile(file, dir);
            }
            catch
            {
                DialogResult varr = MessageBox.Show("A project with this name already exists. Do you want to replace it, or choose a different project name?", "", MessageBoxButtons.OKCancel);
                if (varr == DialogResult.OK)
                {
                    Directory.Delete(projectName, true);
                    DirectoryInfo extract = this.decompile(file, dir);
                }
                if (varr == DialogResult.Cancel)
                {
                    var reopenSetProjectName = new SetProjectName();
                    reopenSetProjectName.Show();
                    this.Close();
                }
            }
            isDecompiling = false;
        }

    }
}