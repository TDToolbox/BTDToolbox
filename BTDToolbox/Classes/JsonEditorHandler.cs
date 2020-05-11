using BTDToolbox.Classes.NewProjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox.Classes
{
    class JsonEditorHandler
    {
        public static New_JsonEditor jeditor;

        public static void ValidateEditor()
        {
            if (jeditor == null)
            {
                jeditor = new New_JsonEditor();
                jeditor.tabPages = new List<TabPage>();
                jeditor.tabFilePaths = new List<string>();
                jeditor.userControls = new List<JsonEditor_Instance>();

                if (NewProjects.CurrentProjectVariables.JsonEditor_OpenedTabs == null)
                    NewProjects.CurrentProjectVariables.JsonEditor_OpenedTabs = new List<string>();
                jeditor.Show();
            }
            else if (jeditor.Visible == false)
            {
                jeditor.Show();
            }
        }
        public static void OpenFileFromZip(string path)
        {
            if (Serializer.cfg.useExternalEditor == false)
            {
                ValidateEditor();

                string pathCleaned = path.Replace("\\", "/");
                if (jeditor.tabFilePaths.Contains(pathCleaned))
                    jeditor.tabControl1.SelectedIndex = jeditor.tabFilePaths.IndexOf(pathCleaned);
                else
                    jeditor.NewTab(path, true);
            }
        }
        public static void OpenFile(string path)
        {
            if (File.Exists(path))
            {
                if (Serializer.cfg.useExternalEditor == false)
                {
                    ValidateEditor();
                    if (jeditor.tabFilePaths.Contains(path))
                        jeditor.tabControl1.SelectedIndex = jeditor.tabFilePaths.IndexOf(path);
                    else
                        jeditor.NewTab(path, false);
                }
            }
            else
            {
                ConsoleHandler.force_append_Notice("One or more files was not found...");
            }
        }
        public static void OpenOriginalFile(string path)
        {
            string[] split = path.Split('\\');
            string filename = split[split.Length - 1];
            string backupProj = "";
            
            if (!path.Contains("\\Backups\\" + CurrentProjectVariables.GameName + "_BackupProject"))
                backupProj = Environment.CurrentDirectory + "\\Backups\\" + CurrentProjectVariables.GameName + "_BackupProject" + path.Replace(CurrentProjectVariables.PathToProjectFiles.Replace("\\\\", "\\"), "");
            else
            {
                backupProj = path;
                if(jeditor.tabControl1.SelectedTab.Text == filename + New_JsonEditor.readOnlyName)
                    ConsoleHandler.append_Notice("You are already looking at the original " + filename.Replace(New_JsonEditor.readOnlyName, ""));
            }

            if (File.Exists(backupProj))
                OpenFile(backupProj);
            else
                ConsoleHandler.append_CanRepeat("Could not find file in backup project... Unable to view original file");
        }
        public static void CloseFile(string path)
        {
            jeditor.CloseTab(path);
        }
        public static bool AreJsonErrors()
        {
            if(jeditor != null)
            {
                int i = 0;
                bool isError = false;
                foreach (var u in jeditor.userControls)
                {
                    if (u.jsonError)
                    {
                        isError = true;
                        jeditor.tabControl1.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
                if (isError)
                    return true;
            }
            return false;
        }
    }
}
