using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static BTDToolbox.GeneralMethods;
using static BTDToolbox.ProjectConfig;
using BTDToolbox.Classes.NewProjects;

namespace BTDToolbox
{
    public partial class ZipForm : Form
    {
        //Project Variables
        JetForm jf;

        //zip variables
        public static int totalFiles = 0;
        public int filesTransfered = 0;
        public static bool useLastPass = false;
        public static string rememberedPassword = "";
        public static string existingJetFile = "";
        public static string savedExportPath = "";

        //Config variables
        ConfigFile programData;
        string jetName = "";
        public string gameDir;
        public string gameName;
        public string steamJetPath;
        public static string BTD5_Dir;
        public static string BTDB_Dir;
        public static string BMC_Dir;


        //other variables
        public string projectName_Identifier = "";
        //public string fullProjName = "";
        public string jetFile_Game { get; set; }
        public string sourcePath { get; set; }
        public string destPath { get; set; }
        public string projName { get; set; }
        public string password { get; set; }
        public bool launch { get; set; }
        public bool isExporting { get; set; }

        //Threads
        Thread backgroundThread;

        public ZipForm()
        {
            InitializeComponent();
            StartUp();
            this.StartPosition = FormStartPosition.Manual;
            this.Left = 120;
            this.Top = 120;
        }
        private void StartUp()
        {
            programData = DeserializeConfig();
            //string std = DeserializeConfig().CurrentGame;

            gameName = CurrentProjectVariables.GameName;
            jetName = ReturnJetName(gameName);
            gameDir = CurrentProjectVariables.GamePath;
            if (gameName != "BTDB" && gameName != "" && gameName != null)
            {
                password = "Q%_{6#Px]]";
                CurrentProjectVariables.JetPassword = password;
                ProjectHandler.SaveProject();
            }
            steamJetPath = gameDir + "\\Assets\\" + jetName;
        }
        public void Extract()
        {
            bool rememberPass = Get_BTDB_Password.rememberPass;
            this.Text = "Extracting..";
            Filename_TB.Text = ReturnJetName(gameName);

            if (existingJetFile == "")
            {
                if (sourcePath == null || sourcePath == "")
                    sourcePath = Environment.CurrentDirectory + "\\Backups\\" + gameName + "_Original.jet";
            }
            else
            {
                sourcePath = existingJetFile;
            }

            if (File.Exists(sourcePath))
            {
                if(projName == "" || projName == null)
                {
                    projName = CurrentProjectVariables.ProjectName;
                }
                //check password
                if (File.Exists(sourcePath) && gameName == "BTDB")
                {
                    this.Hide();
                    bool badPass = Bad_JetPass(sourcePath, password);
                    
                    if (badPass == true)
                    {
                        DialogResult res = MessageBox.Show("You entered the wrong password. Would you like to try again?", "Wrong Password!", MessageBoxButtons.OKCancel);
                        if (res == DialogResult.OK)
                        {
                            var getpas = new Get_BTDB_Password();
                            getpas.projName = projName;
                            getpas.isExtracting = true;

                            if (rememberPass == true)
                                Get_BTDB_Password.rememberPass = true;
                            else
                                Get_BTDB_Password.rememberPass = false;
                            getpas.Show();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Show();
                        if(rememberedPassword != null && rememberedPassword != "")
                        {
                            password = rememberedPassword;
                            Serializer.SaveSmallSettings("battlesPass");
                        }
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
            else
            {
                ConsoleHandler.appendLog("ERROR!!! Failed to find file to extract");
                if(isGamePathValid(gameName))
                {
                    ConsoleHandler.appendLog("Generating new backup...");
                    CreateBackup(gameName);
                    ConsoleHandler.appendLog("Creating project from backup...");
                    backgroundThread = new Thread(Extract_OnThread);
                    backgroundThread.Start();
                }
                else
                {
                    ConsoleHandler.appendLog("ERROR!!! Failed to validate game path. Please browse for " + Get_EXE_Name(gameName));
                    browseForExe(gameName);
                    if(isGamePathValid(gameName))
                    {
                        this.Show();
                        backgroundThread = new Thread(Extract_OnThread);
                        backgroundThread.Start();
                    }
                    else
                    {
                        ConsoleHandler.appendLog("There was an issue... Please try again...");
                        this.Close();
                    }
                }
                
            }
        }
        private void Extract_OnThread()
        {
            destPath = CurrentProjectVariables.PathToProjectFiles;
            DirectoryInfo dinfo = new DirectoryInfo(destPath);
            if (!dinfo.Exists)
            {
                ConsoleHandler.appendLog("Creating project files for: " + projName);

                ZipFile archive = new ZipFile(sourcePath);
                archive.Password = password;
                totalFiles = archive.Count();
                filesTransfered = 0;
                archive.ExtractProgress += ZipExtractProgress;
                archive.ExtractAll(destPath);
                archive.Dispose();

                if (CurrentProjectVariables.GameName == "BMC")
                {
                    DialogResult diag = MessageBox.Show("Would you like to extract the Asset Bundles as well? " +
                        "They have to do with textures, and they are not necessary." +
                        "\nNote: Your project will take up more space", "Extract Asset Bundles as well?", MessageBoxButtons.YesNo);
                    if(diag == DialogResult.Yes)
                    {
                        Invoke((MethodInvoker)delegate {
                            Filename_TB.Text = "AssetBundles";
                        });
                        ExtractAssetBundleJet();
                    }
                }

                if (!Directory.Exists(Environment.CurrentDirectory + "\\Backups\\" + gameName + "_BackupProject"))
                {
                    string gamed = "";
                    if(gameName == "BTD5")
                        gamed = Serializer.Deserialize_Config().BTD5_Directory;
                    else if (gameName == "BTDB")
                        gamed = Serializer.Deserialize_Config().BTDB_Directory;
                    else if(gameName == "BMC")
                        gamed = Serializer.Deserialize_Config().BMC_Directory;

                    //they should have a backup jet of gamed not invalid. create backup proj
                    if (gamed != "" && gamed != null)
                    {
                        ConsoleHandler.force_appendNotice("Backup project not detected.... Creating one now..");
                        Invoke((MethodInvoker)delegate {
                            this.Focus();

                            destPath = Environment.CurrentDirectory + "\\Backups\\" + gameName + "_BackupProject";
                            archive = new ZipFile(sourcePath);
                            archive.Password = password;
                            totalFiles = archive.Count();
                            filesTransfered = 0;
                            archive.ExtractProgress += ZipExtractProgress;
                            archive.ExtractAll(destPath);
                            archive.Dispose();

                            DialogResult diag = MessageBox.Show("Would you like to extract the Asset Bundles as well? " +
                        "They have to do with textures, and they are not necessary." +
                        "\nNote: Your project will take up more space", "Extract Asset Bundles as well?", MessageBoxButtons.YesNo);

                            if (diag == DialogResult.Yes)
                            {
                                Invoke((MethodInvoker)delegate {
                                    Filename_TB.Text = "AssetBundles";
                                });
                                ExtractAssetBundleJet();
                            }
                        });
                    }
                    else
                    {
                        ConsoleHandler.force_appendNotice("Unable to find backup project or the game directory. Backup project WILL NOT be made, and you will NOT be able to use \"Restore to original\" until you browse for your game..");
                    }
                }
                ConsoleHandler.appendLog("Project files created at: " + CurrentProjectVariables.PathToProjectFiles);
                Invoke((MethodInvoker)delegate {
                    jf = new JetForm(dinfo, Main.getInstance(), dinfo.Name);
                    jf.MdiParent = Main.getInstance();
                    jf.Show();
                });
            }
            else
            {
                DialogResult varr = MessageBox.Show("A project with this name already exists. Do you want to replace it?", "", MessageBoxButtons.OKCancel);
                if (varr == DialogResult.OK)
                {
                    MessageBox.Show("Please close the Jet viewer for the old project...");
                    ConsoleHandler.appendLog("Deleting existing project....");
                    MessageBox.Show(dinfo.ToString());
                    DeleteDirectory(dinfo.ToString());
                    ConsoleHandler.appendLog("Project Deleted. Creating new project...");
                    Extract_OnThread();
                    return;
                }
                if (varr == DialogResult.Cancel)
                {
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
        public void Compile()
        {
            if (!IsGameRunning(gameName))
            {
                this.Text = "Compiling..";
                Filename_TB.Text = ReturnJetName(gameName);

                if (gameName == "BTDB")
                {

                    if (rememberedPassword != null && rememberedPassword != "")
                    {
                        password = rememberedPassword;
                        Serializer.SaveSmallSettings("battlesPass");
                        CurrentProjectVariables.JetPassword = password;
                        ProjectHandler.SaveProject();
                    }

                    if (password == null || password.Length <= 0)
                    {
                        var getpas = new Get_BTDB_Password();
                        getpas.launch = launch;
                        getpas.isExtracting = false;
                        getpas.projName = projName;
                        getpas.destPath = destPath;
                        getpas.Show();
                        this.Close();
                    }
                    else
                    {

                        backgroundThread = new Thread(Compile_OnThread);
                        backgroundThread.Start();
                    }
                }
                else
                {
                    backgroundThread = new Thread(Compile_OnThread);
                    backgroundThread.Start();
                }
            }
            else
            {
                ConsoleHandler.force_appendNotice("Game is currently running. Please close the game and try again...");
                this.Close();
            }
        }
        private void Compile_OnThread()
        {
            bool cont = true;
            if(launch)
            {
                if (gameDir == null || gameDir == "")
                {
                    cont = false;
                    this.Invoke(new Action(() => this.Close()));
                    ConsoleHandler.appendLog("There was an issue reading your game directory. Go to the \"Help\" tab at the top, browse for your game again, and then try again...");
                    backgroundThread.Abort();
                }
            }
            
            if (cont)
            {
                string dir = "";
                if (destPath == null || destPath == "")
                    dir = steamJetPath;
                else
                    dir = destPath;

                if (dir != "\\Assets\\" + jetName)
                {
                    if (DeserializeConfig().LastProject == null)
                        Serializer.SaveConfig(jf, "jet explorer");

                    DirectoryInfo projDir = new DirectoryInfo(CurrentProjectVariables.PathToProjectFiles);
                    if (Directory.Exists(projDir.ToString()))
                    {
                        ConsoleHandler.appendLog("Compiling jet...");
                        int numFiles = Directory.GetFiles((projDir.ToString()), "*", SearchOption.AllDirectories).Length;
                        int numFolders = Directory.GetDirectories(projDir.ToString(), "*", SearchOption.AllDirectories).Count();
                        totalFiles = numFiles + numFolders;
                        filesTransfered = 0;

                        ZipFile toExport = new ZipFile();
                        toExport.Password = password;
                        toExport.AddProgress += ZipCompileProgress;
                        toExport.AddDirectory(projDir.FullName);
                        toExport.Encryption = EncryptionAlgorithm.PkzipWeak;
                        toExport.Name = dir;
                        toExport.CompressionLevel = CompressionLevel.Level6;
                        toExport.Save();
                        toExport.Dispose();

                        if (CurrentProjectVariables.GameName == "BMC")
                        {
                            var d = new DirectoryInfo(CurrentProjectVariables.PathToProjectFiles).GetDirectories();
                            foreach(var a in d)
                            {
                                if (a.Name.Contains("AssetBundles"))
                                {
                                    Invoke((MethodInvoker)delegate { Filename_TB.Text = "AssetBundles"; });
                                    CompileAssetBundles();
                                    break;
                                }
                            }
                        }

                        ConsoleHandler.appendLog("Jet was successfully exported to: " + dir);

                        /*if (launch == true)
                            LaunchGame(gameName);*/
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
                    else
                    {
                        ConsoleHandler.appendLog("Output directory not found... Failed to compile...");
                    }
                }
                else
                {
                    ConsoleHandler.appendLog("Something went wrong with the export. The export path might have been invalid, or it might have been cancelled...");
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
            }
        }
        private void ExtractJet_Window_Load(object sender, EventArgs e)
        {
            CurrentFileProgress_Label.Hide();
            CurrentFileProgress.Hide();

            TotalProgress_ProgressBar.Location = new Point(TotalProgress_ProgressBar.Location.X, TotalProgress_ProgressBar.Location.Y - 50);
            TotalProgress_Label.Location = new Point(TotalProgress_Label.Location.X, TotalProgress_Label.Location.Y - 50);
            Filename_TB.Location = new Point(Filename_TB.Location.X, Filename_TB.Location.Y - 50);
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
        private void PrintError(string exception)
        {
            ConsoleHandler.appendLog("An error occured that may prevent the program from running properly.\r\nThe error is below: \r\n\r\n" + exception + "\r\n");
            this.Close();
        }

        public static void CreateAssetBundleJet(string exportPath)
        {
            string defaultLocation = Serializer.Deserialize_Config().BMC_Directory + "\\AssetBundles";
            ZipFile zip = new ZipFile();
            zip.AddDirectory(defaultLocation, "");
            zip.Save(exportPath);
            zip.Dispose();
        }
        public void ExtractAssetBundleJet()
        {
            string destPath = CurrentProjectVariables.PathToProjectFiles + "\\AssetBundles";
            string backupAssetBundle = Environment.CurrentDirectory + "\\Backups\\AssetBundles_Original";
            if (!Directory.Exists(backupAssetBundle))
            {
                ConsoleHandler.appendLog("Unable to find the Backup for BMC Asset Bundles. Reaquiring...");
                GeneralMethods.CreateBackup("BMC");
            }
            var jets = new DirectoryInfo(backupAssetBundle).GetFiles();

            foreach(var jet in jets)
            {
                
                if (jet.Name.EndsWith(".jet"))
                {
                    Invoke((MethodInvoker)delegate {
                        Filename_TB.Text = "Asset Bundle: " + jet.Name;
                    });
                    string dest = (destPath + "\\" + jet.Name).Replace("/", "\\");

                    ZipFile zip = new ZipFile(jet.FullName);
                    zip.Password = CurrentProjectVariables.JetPassword;
                    foreach (ZipEntry z in zip)
                    {
                        //string dest = (destPath + "\\" + jet.Name + "\\" + z.FileName).Replace("/", "\\");
                        if (z.FileName.Contains("JSON") && !dest.Contains(".tmp"))
                        {
                            z.ExtractWithPassword(dest, ExtractExistingFileAction.OverwriteSilently, CurrentProjectVariables.JetPassword);
                        }
                    }
                    zip.Dispose();
                }
            }
        }

        public void CompileAssetBundles()
        {
            string moddedBundles = CurrentProjectVariables.PathToProjectFiles + "\\AssetBundles";
            var dirs = new DirectoryInfo(moddedBundles).GetDirectories();

            foreach (var dir in dirs)
            {

                if (dir.Name.EndsWith(".jet"))
                {
                    Invoke((MethodInvoker)delegate {
                        Filename_TB.Text = "Asset Bundle: " + dir.Name;
                    });

                    string dest = (Environment.CurrentDirectory + "\\ModdedAssetBundles\\" + dir.Name).Replace("/", "\\");
                    //string dest = (DeserializeConfig().BMC_Directory + "\\AssetBundles\\" + dir.Name).Replace("/", "\\");

                    ZipFile bundle = new ZipFile(dest);
                    bundle.Password = CurrentProjectVariables.JetPassword;
                    bundle.AddDirectory(dir.FullName);

                    bundle.Encryption = EncryptionAlgorithm.PkzipWeak;
                    bundle.Name = dest;
                    bundle.CompressionLevel = CompressionLevel.Level6;
                    bundle.Save();
                    bundle.Dispose();
                }
            }
        }
        public void MergeAssetBundles()
        {

        }
        //
        //Zip stuff
        //
        /*private void HandleBTDBPass()
        {
            bool rememberPass = Get_BTDB_Password.rememberPass;
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

                        if (rememberPass == true)
                            Get_BTDB_Password.rememberPass = true;
                        else
                            Get_BTDB_Password.rememberPass = false;
                        getpas.Show();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Show();

                    password = rememberedPassword;
                    CurrentProjectVariables.JetPassword = password;

                    ProjectHandler.SaveProject();
                    Serializer.SaveSmallSettings("battlesPass");
                }
            }
        }

        public static void ExtractFile(string path)
        {
            Extractor.Extractor.ExtractFile(path);
        }*/
                }
            }