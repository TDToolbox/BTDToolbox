using Ionic.Zip;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace BTDToolbox.Classes.NewProjects
{
    class ProjectHandler
    {
        public static ProjectClass.ProjectFile project;
        public static void CreateProject()
        {
            project = new ProjectClass.ProjectFile();
            CurrentProjectVariables.ResetProjectVariables();          
        }
        public static bool IsProjectVald(string projFile)
        {
            string json = File.ReadAllText(projFile);
            bool isValid = JSON_Reader.IsValidJson(json);
            if (!isValid)
            {
                json = json.Replace("\\\\", "\\").Replace("\\", "\\\\");
                isValid = JSON_Reader.IsValidJson(json);
                if (!isValid)
                {
                    if (JetProps.get().Count() > 0)
                    {
                        MessageBox.Show("Project File has invalid JSON. Please contact BTD Toolbox devs for assistance. Click \"Help\" at the top, then click \"Contact Us\"");
                        try { JetProps.getForm(JetProps.get().Count() - 1).Close(); }
                        catch (System.InvalidOperationException) { }
                    }

                    return false;
                }
            }
            return true;
        }
        public static ProjectClass.ProjectFile ReadProject(string projFile)
        {
            if (project != null)
                CurrentProjectVariables.ResetProjectVariables();

            if (project == null)
                project = new ProjectClass.ProjectFile();

            if (!File.Exists(projFile))
            {
                ConsoleHandler.append("Something went wrong when trying to read the" +
                    "project. Project not found...");
                return null;

                //Create new project
                //CreateProject();  //Commented out until im sure this is what needs to happen next
            }

            if (!IsProjectVald(projFile))
                return null;

            string json = File.ReadAllText(projFile);
            project = JsonConvert.DeserializeObject<ProjectClass.ProjectFile>(json);

            CurrentProjectVariables.ProjectName = project.ProjectName.Replace("\\", "");
            CurrentProjectVariables.PathToProjectFiles = project.PathToProjectFiles.Replace("\\\\", "\\");
            CurrentProjectVariables.PathToProjectClassFile = project.PathToProjectClassFile.Replace("\\\\", "\\");
            CurrentProjectVariables.GameName = project.GameName;
            CurrentProjectVariables.GamePath = project.GamePath.Replace("\\\\", "\\");
            CurrentProjectVariables.GameVersion = project.GameVersion;
            CurrentProjectVariables.JetPassword = project.JetPassword;
            CurrentProjectVariables.ExportPath = project.ExportPath;
            CurrentProjectVariables.DateLastOpened = project.DateLastOpened;
            CurrentProjectVariables.JsonEditor_OpenedTabs = project.JsonEditor_OpenedTabs;
            CurrentProjectVariables.ModifiedFiles = project.ModifiedFiles;

            CurrentProjectVariables.UseNKHook = false;
            CurrentProjectVariables.DontAskAboutNKH = false;
            if (CurrentProjectVariables.GameName == "BTD5")
            {
                if (NKHook.DoesNkhExist())
                {
                    CurrentProjectVariables.UseNKHook = project.UseNKHook;
                    CurrentProjectVariables.DontAskAboutNKH = project.DontAskAboutNKH;
                }
            }

            return project;
        }
        public static void SaveProject()
        {
            if (JetProps.get().Count <= 0)
                return;

            if (project == null)
                project = new ProjectClass.ProjectFile();

            project.ProjectName = CurrentProjectVariables.ProjectName;
            project.PathToProjectFiles = CurrentProjectVariables.PathToProjectFiles;
            project.PathToProjectClassFile = CurrentProjectVariables.PathToProjectClassFile;
            project.GameName = CurrentProjectVariables.GameName;
            project.GamePath = CurrentProjectVariables.GamePath;
            project.GameVersion = CurrentProjectVariables.GameVersion;
            project.JetPassword = CurrentProjectVariables.JetPassword;
            project.ExportPath = CurrentProjectVariables.ExportPath;
            project.DateLastOpened = CurrentProjectVariables.DateLastOpened;
            project.JsonEditor_OpenedTabs = CurrentProjectVariables.JsonEditor_OpenedTabs;
            project.ModifiedFiles = CurrentProjectVariables.ModifiedFiles;
            
            
            project.UseNKHook = false;
            project.DontAskAboutNKH = false;
            project.UseNKHook = CurrentProjectVariables.UseNKHook;
            project.DontAskAboutNKH = CurrentProjectVariables.DontAskAboutNKH;


            string output_Cfg = JsonConvert.SerializeObject(project, Formatting.Indented);

            if (JetProps.get().Count <= 0)
                return;

            if(!Guard.IsStringValid(project.PathToProjectClassFile) || !Directory.Exists(project.PathToProjectClassFile))
            {
                ConsoleHandler.force_append_Notice_CanRepeat("WARNING!!!!! Unable to find the .toolbox file for this project! " +
                    "Failed to save project data. This can easily make toolbox not work properly. Please contact Toolbox" +
                    " Devs if this continues");
                return;
            }

            StreamWriter serialize = new StreamWriter(project.PathToProjectClassFile + "\\" + project.ProjectName + ".toolbox", false);
            serialize.Write(output_Cfg);
            serialize.Close();
        }
        public static void UpdateFileInZip(ZipFile zip, string pathToFile, string newText)
        {
            zip.Password = CurrentProjectVariables.JetPassword;
            zip.UpdateEntry(pathToFile, newText);

            if (!CurrentProjectVariables.ModifiedFiles.Contains(pathToFile))
            {
                CurrentProjectVariables.ModifiedFiles.Add(pathToFile);
                SaveProject();
            }
        }
        public static void SaveZipFile(ZipFile zip)
        {
            try
            {
                zip.Password = CurrentProjectVariables.JetPassword;
                zip.Save();
                zip.Dispose();
            }
            catch (Exception e) { ConsoleHandler.append_CanRepeat("SaveZipFile Exception: " + e.Message); }
        }
        public static string ReadTextFromZipFile(ZipFile zip, string fileInZip)
        {
            string returnText = "";
            if (zip.ContainsEntry(fileInZip))
            {
                try
                {
                    foreach (var entry in zip)
                    {
                        if (entry.FileName.Replace("/","\\").Contains(fileInZip))
                        {
                            entry.Password = CurrentProjectVariables.JetPassword;

                            Stream s = entry.OpenReader(CurrentProjectVariables.JetPassword);
                            StreamReader sr = new StreamReader(s);
                            returnText = sr.ReadToEnd();
                        }
                    }
                }
                catch (Exception e)
                {
                    ConsoleHandler.append_CanRepeat("ReadTextFromZipFile Exception: " + e.Message);
                }
            }
            else
            {
                //ConsoleHandler.append_Force_CanRepeat("Unable to check if  " + fileInZip.Replace("\\\\","\\").Replace(CurrentProjectVariables.PathToProjectFiles,"") + "  is modified because it wasnt found in the backup, and therefore has nothing to compare it too.");
                //ConsoleHandler.append_Force_CanRepeat("Unable to find   " + fileInZip + "   in the Jet... Failed to read file..");
            }
            return returnText;
        }
        public static bool ConfirmModifiedFiles(ZipFile moddedZip, ZipFile backupZip, string fileInZip)
        {
            string modText = Regex.Replace(ReadTextFromZipFile(moddedZip, fileInZip), @"^\s*$(\n|\r|\r\n)", "", RegexOptions.Multiline).ToLower().Trim().Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\r\n", "");
            string originalText = Regex.Replace(ReadTextFromZipFile(backupZip, fileInZip), @"^\s*$(\n|\r|\r\n)", "", RegexOptions.Multiline).ToLower().Trim().Replace(" ", "").Replace("\n", "").Replace("\r", "").Replace("\r\n", "");

            if (modText == originalText)
            {
                if (CurrentProjectVariables.ModifiedFiles.Contains(fileInZip))
                {
                    CurrentProjectVariables.ModifiedFiles.Remove(fileInZip);
                    SaveProject();
                }
                return false;
            }
            else
            {
                if (!CurrentProjectVariables.ModifiedFiles.Contains(fileInZip))
                {
                    CurrentProjectVariables.ModifiedFiles.Add(fileInZip);
                    SaveProject();
                }
                return true;
            }
        }
    }
}
