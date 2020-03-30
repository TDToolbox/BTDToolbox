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
                jeditor.userControls = new JsonEditor_Instance[0];
                jeditor.tabPages = new TabPage[0];
                jeditor.tabFilePaths = new string[0];
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

                int i = 0;
                bool isFileOpened = false;
                foreach (string tp in jeditor.tabFilePaths)
                {
                    if (path == tp)
                    {
                        isFileOpened = true;
                        jeditor.tabControl1.SelectedTab = jeditor.tabPages[i];
                    }
                    i++;
                }
                if (!isFileOpened)
                {
                    jeditor.NewTab(path);
                }
            }
        }
        public static void OpenOriginalFile(string path)
        {
            string[] split = path.Split('\\');
            string filename = split[split.Length - 1];
            string backupProj = Environment.CurrentDirectory + "\\Backups\\" + Main.gameName + "_BackupProject\\" + path.Replace(Environment.CurrentDirectory,"").Replace(Serializer.Deserialize_Config().LastProject + "\\", "");
            if (File.Exists(backupProj))
            {
                OpenFile(backupProj);
            }
            else
            {
                ConsoleHandler.appendLog_CanRepeat("Could not find file in backup project... Unable to view original file");
            }
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
