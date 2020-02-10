using BTDToolbox.Classes;
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
            if (File.Exists(originalLocation))
            {
                if (!File.Exists(newLocation))
                {
                    ConsoleHandler.appendLog("Copying file...");
                    File.Copy(originalLocation, newLocation);
                    ConsoleHandler.appendLog("File Copied!");
                }
                else
                {
                    ConsoleHandler.appendLog("A file with that name already exists...");
                    DeleteFile(newLocation);
                    ConsoleHandler.appendLog("Replacing existing file..");
                    File.Copy(originalLocation, newLocation);
                    ConsoleHandler.appendLog("File copied!");
                }
            }
            else
            {
                ConsoleHandler.appendLog("Failed to copy file because it was not found at: \r\n" + originalLocation);
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
                ConsoleHandler.appendLog("Unable to detect launch directory for " + game);
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

                ConsoleHandler.appendLog("Attempting to validate backup...");
                if (Directory.Exists(backupDir))
                {
                    if (File.Exists(backupDir + "\\" + backupName))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
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
                    steamJetPath = DeserializeConfig().BTD5_Directory + "\\Assets\\" + jetName;
                }
                else if (game == "BTDB")
                {
                    jetName = "data.jet";
                    steamJetPath = DeserializeConfig().BTDB_Directory + "\\Assets\\" + jetName;
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
            string backupName = game + "_Original.jet";
            string fullBackupPath = backupDir + "\\" + backupName;

            if (!Directory.Exists(backupDir))
            {
                ConsoleHandler.appendLog("Backup directory does not exist. Creating directory...");
                Directory.CreateDirectory(backupDir);
            }

            string steamJetPath = GetJetPath(game);
            if (steamJetPath != null)
            {
                CopyFile(steamJetPath, fullBackupPath);
                if (File.Exists(fullBackupPath))
                    ConsoleHandler.appendLog("Backup created!");
                else
                    ConsoleHandler.appendLog("Backup failed...");
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
            else
                game = "BTD5";

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

            if (exeName != null && exeName != "")
            {
                MessageBox.Show("Please browse for " + exeName + ".\n\nMake sure that your game is UNMODDED, otherwise Toolbox will make a corrupt backup");
                ConsoleHandler.appendLog("Make sure that your game is UNMODDED, otherwise Toolbox will make a corrupt backup..");
                string exePath = BrowseForFile("Open game exe", "exe", "Exe files (*.exe)|*.exe|All files (*.*)|*.*", "");
                if (exePath != null && exePath != "")
                {
                    if (exePath.Contains(exeName))
                    {
                        string gameDir = exePath.Replace("\\" + exeName, "");
                        if (game == "BTD5")
                            Main.BTD5_Dir = gameDir;
                        else if (game == "BTDB")
                            Main.BTDB_Dir = gameDir;

                        Serializer.SaveConfig(Main.getInstance(), "directories", programData);
                    }
                    else
                    {
                        ConsoleHandler.appendLog("You selected an Invalid .exe. Please browse for the exe for your game.");
                    }
                }
            }
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
            if (JsonEditor.jsonError != true)
            {
                if (JetProps.get().Count == 1)
                {
                    string dest = "";
                    bool isOutputting = false;

                    var zip = new ZipForm();

                    if (switchCase.Contains("output"))
                    {
                        isOutputting = true;
                        ConsoleHandler.appendLog("Select where you want to export your jet file. Make sure to give it a name..");
                        dest = OutputJet();
                        zip.destPath = dest;
                    }
                    if (switchCase.Contains("launch"))
                    {
                        zip.launch = true;
                    }
                    if (isOutputting)
                    {
                        if (dest != null && dest != "")
                        {
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
