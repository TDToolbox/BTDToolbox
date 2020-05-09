using BTDToolbox.Classes;
using BTDToolbox.Classes.NewProjects;
using Ionic.Zip;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfig;

namespace BTDToolbox
{
    class GeneralMethods
    {
        public static bool badJetPass = false;

        public static ConfigFile programData;
        
        public static void DeleteFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                ConsoleHandler.appendLog("Deleting file...");
                File.Delete(fileName);
                ConsoleHandler.appendLog("File deleted.");
            }
            else
            {
                ConsoleHandler.appendLog("Error! Unable to delete the file:\r\n" + fileName);
            }
        }
        public static void DeleteDirectory(string path)
        {
            if(path == Environment.CurrentDirectory)
            {
                ConsoleHandler.force_appendNotice("A Critical error occured... Attempted to delete BTD Toolbox folder...");
                return;
            }
            if (Directory.Exists(path))
            {
                ConsoleHandler.appendLog("Deleting directory..");
                var directory = new DirectoryInfo(path) { Attributes = FileAttributes.Normal };
                foreach (var info in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
                {
                    info.Attributes = FileAttributes.Normal;
                }
                directory.Delete(true);
                ConsoleHandler.appendLog("Directory deleted.");
            }
            else
            {
                ConsoleHandler.appendLog("Directory not found. Unable to delete directory at:\r\n" + path);
            }
        }
        public static void CopyFile(string originalLocation, string newLocation)
        {
            string[] split = originalLocation.Split('\\');
            string filename = split[split.Length - 1];

            if (File.Exists(originalLocation))
            {
                if (!File.Exists(newLocation))
                {
                    ConsoleHandler.appendLog("Copying "+ filename + "...");
                    File.Copy(originalLocation, newLocation);
                    ConsoleHandler.appendLog("Copied " + filename + "!");
                }
                else
                {
                    ConsoleHandler.appendLog("A file with that name already exists...");
                    DeleteFile(newLocation);
                    ConsoleHandler.appendLog("Replacing existing file..");
                    File.Copy(originalLocation, newLocation);
                    ConsoleHandler.appendLog("Copied " + filename + "!");
                }
            }
            else
            {
                ConsoleHandler.appendLog("Failed to copy " + filename + " because it was not found at: \r\n" + originalLocation);
            }
        }
        public static void CopyDirectory(string source, string destination)
        {
            string[] split = source.Split('\\');
            string dirname = split[split.Length - 1];

            ConsoleHandler.appendLog("Copying " + dirname + "...");
            foreach (string dirPath in Directory.GetDirectories(source, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(source, destination));

            foreach (string newPath in Directory.GetFiles(source, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(source, destination), true);
            ConsoleHandler.appendLog("Copied " + dirname + "!");
        }
        public static ConfigFile DeserializeConfig()
        {
            //ConsoleHandler.appendLog("Reading config file..");
            programData = Serializer.Deserialize_Config();
            return programData;
        }

        public static string BrowseForFile(string title, string defaultExt, string filter, string startDir)
        {
            OpenFileDialog fileDiag = new OpenFileDialog();
            fileDiag.Title = title;
            fileDiag.DefaultExt = defaultExt;
            fileDiag.Filter = filter;
            fileDiag.Multiselect = false;
            fileDiag.InitialDirectory = startDir;

            if (fileDiag.ShowDialog() == DialogResult.OK)
            {
                return fileDiag.FileName;
            }
            else
                return null;
        }
        public static string[] BrowseForFiles(string title, string defaultExt, string filter, string startDir)
        {
            OpenFileDialog fileDiag = new OpenFileDialog();
            fileDiag.Title = title;
            fileDiag.DefaultExt = defaultExt;
            fileDiag.Filter = filter;
            fileDiag.Multiselect = true;
            fileDiag.InitialDirectory = startDir;

            if (fileDiag.ShowDialog() == DialogResult.OK)
            {
                return fileDiag.FileNames;
            }
            else
                return null;
        }
        public static string BrowseForDirectory(string title, string startDir)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = title;
            dialog.Multiselect = false;
            dialog.InitialDirectory = startDir;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            else
                return null;
        }
        public static string Get_EXE_Name(string game)
        {
            string exeName = "";
            if (game != null && game != "")
            {
                if (game == "BTD5")
                    exeName = "BTD5-Win.exe";
                else if (game == "BTDB")
                    exeName = "Battles-Win.exe";
                else if (game == "BMC")
                    exeName = "MonkeyCity-Win.exe";

                return exeName;
            }
            else
                return null;
        }
        public static string GetCurrent_GameName()
        {
            string game = DeserializeConfig().CurrentGame;

            if (game == null || game == "")
            {
                ConsoleHandler.appendLog("Unable to get game name..");
                return null;
            }
            return game;
        }
        public static string ReturnGamePath(string game)
        {
            string gameDir = "";
            if (game == "BTD5")
                gameDir = DeserializeConfig().BTD5_Directory;
            else if (game == "BTDB")
                gameDir = DeserializeConfig().BTDB_Directory;
            else if (game == "BMC")
                gameDir = DeserializeConfig().BMC_Directory;

            return gameDir;
        }
        public static string ReturnJetName(string game)
        {
            string jetName = "";
            if (game == "BTD5")
                jetName = "BTD5.jet";
            else if (game == "BTDB")
                jetName = "data.jet";
            else if (game == "BMC")
                jetName = "data.jet";

            return jetName;
        }
        public static bool isGamePathValid(string game)
        {
            string gameDir = ReturnGamePath(game);

            if (gameDir == null || gameDir == "")
            {
                ConsoleHandler.appendLog("Unable to detect launch directory for " + game);
                return false;
            }
            else
            {
                if (Main.getInstance().Launch_Program_ToolStrip.Visible == false)
                    Main.getInstance().Launch_Program_ToolStrip.Visible = true;
                return true;                
            }
        }
        public static void LaunchGame(string game)
        {
            string exePath = "";
            string gameDir = "";
            string exeName = "";

            if (isGamePathValid(game) == true)
            {
                gameDir = CurrentProjectVariables.GamePath;
                exeName = Get_EXE_Name(game);
                
                exePath = gameDir + "\\" + exeName;
                ConsoleHandler.appendLog("Launching " + game + "...");
                Process.Start(exePath);
                ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.");
            }
            else
            {
                ConsoleHandler.appendLog("Unable to launch game... Game directory not detected...");
            }

        }
        public static bool Validate_Backup(string gameName)
        {
            if (gameName != null && gameName != "")
            {
                string backupName = gameName + "_Original.jet";
                string backupDir = Environment.CurrentDirectory + "\\Backups";
                string backupLocName = gameName + "_Original_LOC.xml";

                ConsoleHandler.appendLog("Validating backup...");
                if (Directory.Exists(backupDir))
                {
                    if (!File.Exists(backupDir + "\\" + backupName))
                    {
                        ConsoleHandler.appendLog("Failed to validate backup...");
                        return false;
                    }
                    if (!File.Exists(backupDir + "\\" + backupLocName))
                    {
                        ConsoleHandler.appendLog("Failed to validate backup loc...");
                        return false;
                    }
                    if(gameName == "BMC")
                    {
                        string backupAssetBundle = backupDir + "\\AssetBundles_Original";
                        if (!Directory.Exists(backupAssetBundle))
                        {
                            ConsoleHandler.appendLog("Failed to validate backup Asset Bundle for BMC...");
                            return false;
                        }
                    }
                    
                    ConsoleHandler.appendLog("Backup validated");
                    return true;
                }
                else
                {
                    ConsoleHandler.appendLog("Failed to validate one or more backup files...");
                    return false;
                }
            }
            else
            {
                ConsoleHandler.appendLog("Failed to validate one or more backup files...");
                return false;
            }
        }
        public static string GetJetPath(string game)
        {
            string steamJetPath = "";
            string jetName = "";
            string gameDir = "";
            if (isGamePathValid(game) == true)
            {
                jetName = ReturnJetName(game);
                gameDir = ReturnGamePath(game);
                steamJetPath = gameDir + "\\Assets\\" + jetName;
                
                return steamJetPath;
            }
            else
            {
                ConsoleHandler.appendLog("Unable to locate the original" + jetName + ". Are you sure it exists?");
                ConsoleHandler.appendLog("If this problem continues, double check that your " + jetName + " is spelled just like this:  " + jetName);
                return null;
            }
        }
        public static void CreateBackup(string game)
        {
            string backupDir = Environment.CurrentDirectory + "\\Backups";
            string backupName = game + "_Original.jet";
            string backupLocName = game + "_Original_LOC.xml";
            string fullBackupPath = backupDir + "\\" + backupName;

            MessageBox.Show("One or more of the backup files failed to be aquired... Please wait while they are reaquired...");
            ConsoleHandler.appendLog("Aquiring new backups..");
            if (!Directory.Exists(backupDir))
            {
                ConsoleHandler.appendLog("Backup directory does not exist. Creating directory...");
                Directory.CreateDirectory(backupDir);
            }

            string gameDir = ReturnGamePath(game);
            string steamJetPath = GetJetPath(game);

            if (steamJetPath != null)
            {
                CopyFile(gameDir + "\\Assets\\Loc\\English.xml", backupDir + "\\" + backupLocName);
                CopyFile(steamJetPath, fullBackupPath);    
                
                if (File.Exists(fullBackupPath))
                    ConsoleHandler.appendLog("Backup jet created!");
                else
                    ConsoleHandler.appendLog("Backup jet failed...");

                if (game == "BMC")
                {
                    string source = Serializer.Deserialize_Config().BMC_Directory + "\\AssetBundles";
                    string destination = backupDir + "\\AssetBundles_Original";
                    if (Directory.Exists(destination))
                        Directory.Delete(destination, true);

                    Directory.CreateDirectory(destination);
                    CopyDirectory(source, destination);
                }
            }
            else
            {
                ConsoleHandler.appendLog("Unable to create backup for " + game + ".");
            }
        }
        public static void SteamValidate(string game)
        {
            string url = "";
            if (game == "BTD5")
                url = "306020";
            else if (game == "BTDB")
                url = "444640";
            else if (game == "BMC")
                url = "1252780";

            Process.Start("steam://validate/" + url);
        }
        public static void BackupLOC(string game)
        {
            string backupDir = Environment.CurrentDirectory + "\\Backups";
            string backupLocName = game + "_Original_LOC.xml";

            if (!Directory.Exists(backupDir))
            {
                ConsoleHandler.appendLog("Backup directory does not exist. Creating directory...");
                Directory.CreateDirectory(backupDir);
            }

            string gameDir = ReturnGamePath(game);
            string steamJetPath = GetJetPath(game);

            if (steamJetPath != null)
                CopyFile(gameDir + "\\Assets\\Loc\\English.xml", backupDir + "\\" + backupLocName);
            else
                ConsoleHandler.appendLog("Unable to create backup for " + game + ".");
        }
        public static void RestoreGame_ToBackup(string game)
        {
            string gameDir = "";
            string jetName = "";
            string backupJetLoc = Environment.CurrentDirectory + "\\Backups\\" + game + "_Original.jet";

            if (isGamePathValid(game) == true)
            {
                gameDir = ReturnGamePath(game);
                jetName = ReturnJetName(game);
                string steamJetPath = gameDir + "\\Assets\\" + jetName;

                if (!File.Exists(backupJetLoc))
                {
                    ConsoleHandler.appendLog("Unable to restore " + jetName + ". Backup file doesn't exist..");
                }
                else
                {
                    ConsoleHandler.appendLog("Replacing " + jetName + " with the backup " + jetName);
                    if (File.Exists(steamJetPath))
                        File.Delete(steamJetPath);

                    File.Copy(Environment.CurrentDirectory + "\\Backups\\" + game + "_Original.jet", steamJetPath);
                    ConsoleHandler.appendLog(jetName + " successfully restored!");
                }
            }
            else
            {
                ConsoleHandler.appendLog("Unable to restore " + jetName + ". Game directory not detected..");
            }
        }
        public static void RestoreGame_ToBackup_LOC(string game)
        {
            string gameDir = "";
            string backupJetLoc = Environment.CurrentDirectory + "\\Backups\\" + game + "_Original_LOC.xml";
            string locName = "English.xml";

            if (isGamePathValid(game) == true)
            {
                gameDir = ReturnGamePath(game);
                string locPath = gameDir + "\\Assets\\Loc\\" + locName;

                if (!File.Exists(backupJetLoc))
                {
                    ConsoleHandler.appendLog("Unable to restore " + locName + ". Backup file doesn't exist..");
                }
                else
                {
                    ConsoleHandler.appendLog("Replacing " + locName + " with the backup " + locName);
                    if (File.Exists(locPath))
                        File.Delete(locPath);

                    File.Copy(backupJetLoc, locPath);
                    ConsoleHandler.appendLog(locName + " successfully restored!");
                }
            }
            else
            {
                ConsoleHandler.appendLog("Unable to restore " + locName + ". Game directory not detected..");
            }
        }
        public static bool IsJSON_Valid(string text)
        {
            try
            {
                JObject.Parse(text);
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
        public static bool Bad_JetPass(string path, string password)
        {
            string tempDir = Environment.CurrentDirectory + "\\temp";
            badJetPass = false;
            if (Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);

            ZipFile archive = new ZipFile(path);
            archive.Password = password;
            archive.ExtractProgress += ZipExtractProgress;

            int i = 0;
            try
            {
                foreach(ZipEntry e in archive)
                {
                    e.Extract(tempDir);
                    
                    if(i >= 5)
                        break;
                    i++;
                }
                //archive.ExtractAll(tempDir);
            }
            catch (BadPasswordException)
            {
                badJetPass = true;
            }
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);

            if (badJetPass == true)
                return true;
            else
                return false;
        }
        public static void ZipExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            if (e.EventType != ZipProgressEventType.Extracting_BeforeExtractEntry)
                return;
            if (e.EntriesExtracted > 1)
            {
                e.Cancel = true;
            }
        }
        public static string DetermineJet_Game(string jetPath)
        {
            string game = "";
            if (Bad_JetPass(jetPath, "Q%_{6#Px]]"))
                game = "BTDB";

            return game;
        }
        public static bool ValidateEXE(string game)
        {
            if (isGamePathValid(game) == false)
            {
                ConsoleHandler.appendLog("Error identifying Game Directory or Backups. Please browse for your EXE again...\r\n");
                browseForExe(game);
                if (isGamePathValid(game) == false)
                {
                    MessageBox.Show("The EXE was not found... Please try again.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
        public static void browseForExe(string game)
        {
            string exeName = Get_EXE_Name(game);

            if(!Guard.IsStringValid(exeName))
                return;


            MessageBox.Show("Please browse for " + exeName + ".\n\nMake sure that your game is UNMODDED, otherwise Toolbox will make a corrupt backup");
            ConsoleHandler.appendLog("Make sure that your game is UNMODDED, otherwise Toolbox will make a corrupt backup..");
            string exePath = BrowseForFile("Open game exe", "exe", "Exe files (*.exe)|*.exe|All files (*.*)|*.*", "");
            if (!Guard.IsStringValid(exePath))
            {
                ConsoleHandler.force_appendLog("Invalid EXE path!");
                return;
            }

            if (!exePath.Contains(exeName))
            {
                ConsoleHandler.appendLog("You selected an Invalid .exe. Please browse for the exe for your game.");
                return;
            }

            string gameDir = exePath.Replace("\\" + exeName, "");
            if (game == "BTD5")
                Main.BTD5_Dir = gameDir;
            else if (game == "BTDB")
                Main.BTDB_Dir = gameDir;
            else if (game == "BMC")
                Main.BMC_Dir = gameDir;

            Serializer.SaveConfig(Main.getInstance(), "directories");
        }
        public static string OutputJet()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Export .jet";
            sfd.DefaultExt = "jet";
            sfd.Filter = "Jet files (*.jet)|*.jet|All files (*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            else
                return null;
        }
        public static void CompileJet(string switchCase)
        {
            if (New_JsonEditor.isJsonError != true)
            {                
                if (JetProps.get().Count == 1)
                {
                    string dest = "";
                    bool isOutputting = false;
                    bool abort = false;

                    var zip = new ZipForm();

                    if (switchCase.Contains("output"))
                    {
                        isOutputting = true;
                        //string exPath = programData.ExportPath;
                        string exPath = CurrentProjectVariables.ExportPath;
                        if (exPath != "" && exPath != null)
                        {
                            DialogResult diag = MessageBox.Show("Do you want export to the same place as last time?", "Export to the same place?", MessageBoxButtons.YesNo);
                            if (diag == DialogResult.Yes)
                                dest = exPath;
                            else
                                exPath = "";
                        }
                        if (exPath == "" || exPath == null)
                        {
                            ConsoleHandler.appendLog("Select where you want to export your jet file. Make sure to give it a name..");
                            dest = OutputJet();

                            CurrentProjectVariables.ExportPath = dest;
                            ProjectHandler.SaveProject();
                            
                            //ZipForm.savedExportPath = dest;
                            //Serializer.SaveSmallSettings("export path");
                        }
                        zip.destPath = dest;
                    }


                    else if (switchCase.Contains("launch"))
                    {
                        if (switchCase.Contains("nkh"))
                            zip.launchNKH = true;

                        if (CurrentProjectVariables.GamePath != null && CurrentProjectVariables.GamePath != "")
                        {
                            zip.launch = true;
                            zip.destPath = CurrentProjectVariables.GamePath + "\\Assets\\" + ReturnJetName(CurrentProjectVariables.GameName);
                        }
                        else
                        {
                            ConsoleHandler.force_appendNotice("Unable to find your game directory, and therefore, unable to launch. Do you want to try browsing for your game?");
                            DialogResult diag = MessageBox.Show("Unable to find your game directory, and therefore, unable to launch. Do you want to try browsing for your game?", "Browse for game?", MessageBoxButtons.YesNoCancel);
                            if (diag == DialogResult.Yes)
                            {
                                browseForExe(CurrentProjectVariables.GameName);
                            }
                            if (diag == DialogResult.No)
                            {
                                DialogResult diag2 = MessageBox.Show("Do you want to just save your jet file instead?", "Save jet instead?", MessageBoxButtons.YesNo);
                                {
                                    if (diag2 == DialogResult.Yes)
                                    {
                                        isOutputting = true;
                                        string exPath = CurrentProjectVariables.ExportPath;
                                        if (exPath != "" && exPath != null)
                                        {
                                            DialogResult diagz = MessageBox.Show("Do you want export to the same place as last time?", "Export to the same place?", MessageBoxButtons.YesNo);
                                            if (diagz == DialogResult.Yes)
                                                dest = exPath;
                                            else
                                                exPath = "";
                                        }
                                        if (exPath == "" || exPath == null)
                                        {
                                            ConsoleHandler.appendLog("Select where you want to export your jet file. Make sure to give it a name..");
                                            dest = OutputJet();
                                            CurrentProjectVariables.ExportPath = dest;
                                            ProjectHandler.SaveProject();

                                            /*ZipForm.savedExportPath = dest;
                                            Serializer.SaveSmallSettings("export path");*/
                                        }
                                        zip.destPath = dest;
                                    }
                                }
                            }
                            else
                                abort = true;
                        }
                    }
                    if (!abort)
                    {
                        if (isOutputting)
                        {
                            if (dest != null && dest != "")
                            {
                                zip.destPath = dest;
                                zip.Show();
                                zip.Compile();
                            }
                            else
                            {
                                ConsoleHandler.appendLog("Export cancelled...");
                            }
                        }
                        else
                        {
                            zip.Show();
                            zip.Compile();
                        }
                    }
                }
                else
                {
                    if (JetProps.get().Count < 1)
                    {
                        MessageBox.Show("You have no .jets or projects open, you need one to launch.");
                        ConsoleHandler.appendLog("You need to open a project to continue...");
                    }
                    else
                    {
                        MessageBox.Show("You have multiple .jets or projects open, only one can be launched.");
                        ConsoleHandler.appendLog("You need to close projects to continue...");
                    }
                }
            }
        }
        public static void checkedForExit()
        {
            if (Main.exit)
            {
                try
                { Environment.Exit(0); }
                catch
                { }
            }
        }
        public static bool IsGameRunning(string game)
        {
            string exename =  Get_EXE_Name(game).Replace(".exe", "");
            
            Process[] pname = Process.GetProcessesByName(exename);
            if (pname.Length == 0)
                return false;
            else
                return true;
        }
        public static void TerminateGame(string game)
        {
            string exename = Get_EXE_Name(game).Replace(".exe", "");

            foreach (var process in Process.GetProcessesByName(exename))
            {
                process.Kill();
            }
        }
        public static Point GetCenterScreen()
        {
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            int x = resolution.Width / 2;
            int y = resolution.Height / 2;

            Point p = new Point(x, y);
            return p;
        }
        public static string GetOS_Type()   //x64 or x32?
        {
            bool is64Bit = Environment.Is64BitOperatingSystem;

            if (is64Bit == true)
                return "64";
            else
                return "32";
        }
        public static string WordUnderMouse(RichTextBox rch, int x, int y)
        {
            // Get the character's position.
            int pos = rch.GetCharIndexFromPosition(new Point(x, y));
            //if (pos >= 0) return "";

            // Find the start of the word.
            string txt = rch.Text;

            int start_pos;
            for (start_pos = pos; start_pos >= 0; start_pos--)
            {
                // Allow digits, letters, and underscores
                // as part of the word.
                char ch = txt[start_pos];
                if (!char.IsLetterOrDigit(ch) && !(ch == '_')) break;
            }
            start_pos++;

            // Find the end of the word.
            int end_pos;
            for (end_pos = pos; end_pos > txt.Length; end_pos++)
            {
                char ch = txt[end_pos];
                if (!char.IsLetterOrDigit(ch) && !(ch == '_')) break;
            }
            end_pos--;

            // Return the result.
            if (start_pos > end_pos) return "";
            return txt.Substring(start_pos, end_pos - start_pos + 1);
        }
        public static int CharIndex_UnderMouse(RichTextBox rch, int x, int y)
        {
            // Get the character's position.
            int pos = rch.GetCharIndexFromPosition(new Point(x, y));
            return pos;
        }
    }
}
