using Ionic.Zip;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
            if(File.Exists(fileName))
            {
                ConsoleHandler.appendLog("Deleting file...");
                File.Delete(fileName);
                ConsoleHandler.appendLog("File deleted.");
            }
            else
            {
                ConsoleHandler.appendLog("Error! Unable to delete file");
            }
        }
        public static void DeleteDirectory(string path)
        {
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
                ConsoleHandler.appendLog("Directory not found. Unable to delete directory");
            }
        }
        public static void CopyFile(string originalLocation, string newLocation)
        {
            if(File.Exists(originalLocation))
            {
                if(!File.Exists(newLocation))
                {
                    ConsoleHandler.appendLog("Copying file...");
                    File.Copy(originalLocation, newLocation);
                    ConsoleHandler.appendLog("File Copied!");
                    File.Delete(originalLocation);
                }
                else
                {
                    ConsoleHandler.appendLog("A file with that name already exists...");
                    File.Delete(newLocation);
                    ConsoleHandler.appendLog("Existing file deleted.\nCopying file..");
                    File.Copy(originalLocation, newLocation);
                    ConsoleHandler.appendLog("File copied!");
                    File.Delete(originalLocation);
                }
            }
            else
            {
                ConsoleHandler.appendLog("Failed to copy file because it was not found.");
            }
        }
        public static void CopyDirectory(string originalLocation, string newLocation)
        {
            ConsoleHandler.appendLog("Copying directory...");
            foreach (string dirPath in Directory.GetDirectories(originalLocation, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(originalLocation, newLocation));

            foreach (string newPath in Directory.GetFiles(originalLocation, "*.*", SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(originalLocation, newLocation), true);
            ConsoleHandler.appendLog("Directory copied.");
        }
        public static ConfigFile DeserializeConfig()
        {
            //ConsoleHandler.appendLog("Reading config file..");
            programData = Serializer.Deserialize_Config();
            return programData;
        }
        public static void SerializeConfig(Form fro, string formName, ConfigFile progData)
        {
            ConsoleHandler.appendLog("Updating config file..");
            Serializer.SaveConfig(fro, formName, progData);
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
        public static bool isGamePathValid(string game)
        {
            string gameDir = "";
            if (game == "BTD5")
                gameDir = DeserializeConfig().BTD5_Directory;
            else if (game == "BTDB")
                gameDir = DeserializeConfig().BTDB_Directory;

            if (gameDir == null || gameDir == "")
            {
                ConsoleHandler.appendLog("Launch Directory not detected or is invalid...");
                return false;
            }
            else
            {
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
                if (game == "BTD5")
                {
                    exeName = "\\BTD5-Win.exe";
                    gameDir = DeserializeConfig().BTD5_Directory;
                }
                else if (game == "BTDB")
                {
                    exeName = "\\Battles-Win.exe";
                    gameDir = DeserializeConfig().BTDB_Directory;
                }
                exePath = gameDir + exeName;
                ConsoleHandler.appendLog("Launching " + game + "...");
                Process.Start(exePath);
                ConsoleHandler.appendLog("Steam is taking over for the rest of the launch.\r\n");
            }
            else
            {
                ConsoleHandler.appendLog("Unable to launch game... Game directory not detected...\r\n");
            }
            
        }
        public static void Validate_Backup(string gameName)
        {
            if (gameName != null && gameName != "")
            {
                string backupName = gameName + "_Original.jet";
                string backupDir = Environment.CurrentDirectory + "\\Backups";

                ConsoleHandler.appendLog("Attempting to validate backup...");
                if (Directory.Exists(backupDir))
                {
                    if (File.Exists(backupDir + "\\" + backupName))
                    {
                        ConsoleHandler.appendLog("Backup successfully validated.");
                    }
                    else
                    {
                        ConsoleHandler.appendLog("Jet backup not found for" + gameName);
                        CreateBackup(gameName);
                    }
                }
                else
                {
                    ConsoleHandler.appendLog("Backups directory does not exist. Creating directory...");
                    Directory.CreateDirectory(backupDir);
                    CreateBackup(gameName);
                }
            }
            else
            {
                ConsoleHandler.appendLog("There was an issue validating your backup. Reopen your project and try again. If the issue continutes, try reopening Toolbox.");
            }
        }
        public static string GetJetPath(string game)
        {
            string steamJetPath = "";
            string jetName = "";

            if (isGamePathValid(game) == true)
            {
                if (game == "BTD5")
                {
                    jetName = "BTD5.jet";
                    steamJetPath = DeserializeConfig().BTD5_Directory + jetName;
                }
                else if (game == "BTDB")
                {
                    jetName = "data.jet";
                    steamJetPath = DeserializeConfig().BTDB_Directory + jetName;   
                }
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
            if (!Directory.Exists(backupDir))
            {
                ConsoleHandler.appendLog("Backup directory does not exist. Creating directory...");
                Directory.CreateDirectory(backupDir);
            }

            string steamJetPath = GetJetPath(game);
            if (steamJetPath != null)
            {
                File.Copy(steamJetPath, backupDir + "\\");
                ConsoleHandler.appendLog("Backup created!");
            }
            else
            {
                ConsoleHandler.appendLog("Unable to create backup for " + game + ".");
            }
        }
        public static void RestoreGame_ToBackup(string game)
        {
            string gameDir = "";
            string jetName = "";
            string backupJetLoc = Environment.CurrentDirectory + "\\Backups\\" + game + "_Original.jet";

            if (isGamePathValid(game) == true)
            {
                if (game == "BTD5")
                {
                    jetName = "BTD5.jet";
                    gameDir = DeserializeConfig().BTD5_Directory;
                }
                else if (game == "BTDB")
                {
                    jetName = "data.jet";
                    gameDir = DeserializeConfig().BTDB_Directory;
                }
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

            try
            {
                archive.ExtractAll(tempDir);
            }
            catch (BadPasswordException)
            {
                badJetPass = true;
            }
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir,true);
            if (badJetPass)
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
            else
                game = "BTD5";

            return game;
        }
        public static void CompileJet(string switchCase)
        {

        }
        public static void ExitHandling(Form form)
        {
            
        }

        public static void browseForExe(string game)
        {
            string exeName = Get_EXE_Name(game);

            if (exeName != null && exeName != "")
            {
                string exePath = BrowseForFile("Open game exe", "exe", "Exe files (*.exe)|*.exe|All files (*.*)|*.*", "");

                if (exePath != null && exePath != "")
                {
                    string gameDir = exePath.Replace("\\" + exeName, "");
                    if (game == "BTD5")
                        TD_Toolbox_Window.BTD5_Dir = gameDir;
                    else if (game == "BTDB")
                        TD_Toolbox_Window.BTDB_Dir = gameDir;

                    Serializer.SaveConfig(TD_Toolbox_Window.getInstance(), "directories", programData);
                }
            }
        }
    }
}
