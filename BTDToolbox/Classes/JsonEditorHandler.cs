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
                jeditor.Show();
            }
            else if (jeditor.Visible == false)
            {
                jeditor.Show();
            }
        }
        public static void OpenFile(string path)
        {
            if (Serializer.Deserialize_Config().useExternalEditor == false)
            {
                ValidateEditor();
                if(jeditor.tabFilePaths.Contains(path))
                {
                    jeditor.tabControl1.SelectedIndex = jeditor.tabFilePaths.IndexOf(path);
                }
                else
                {
                    jeditor.NewTab(path);
                }
            }
        }
        public static void OpenOriginalFile(string path)
        {
            string[] split = path.Split('\\');
            string filename = split[split.Length - 1];
            string backupProj = "";
            if (!path.Contains("\\Backups\\" + Main.gameName + "_BackupProject\\"))
                backupProj = Environment.CurrentDirectory + "\\Backups\\" + Main.gameName + "_BackupProject\\" + path.Replace(Environment.CurrentDirectory, "").Replace(Serializer.Deserialize_Config().LastProject + "\\", "");
            else
            {
                backupProj = path;
                if(jeditor.tabControl1.SelectedTab.Text == filename + New_JsonEditor.readOnlyName)
                    ConsoleHandler.appendNotice("You are already looking at the original " + filename.Replace(New_JsonEditor.readOnlyName, ""));
            }
            
            if (File.Exists(backupProj))
                OpenFile(backupProj);
            else
                ConsoleHandler.appendLog_CanRepeat("Could not find file in backup project... Unable to view original file");
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
