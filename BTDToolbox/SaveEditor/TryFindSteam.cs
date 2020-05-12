using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTDToolbox.SaveEditor
{
    class TryFindSteam
    {

        public static List<string> drives = new List<string>() { "A:\\", "B:\\", "C:\\","D:\\", "E:\\", "F:\\",
            "G:\\", "H:\\", "I:\\","J:\\", "K:\\", "L:\\","M:\\", "N:\\", "O:\\","P:\\", "Q:\\", "R:\\",
            "S:\\", "T:\\", "U:\\","V:\\", "W:\\", "X:\\","Y:\\", "Z:\\"
        };
        public static List<string> paths = new List<string>() { "", "Program Files", "Program Files (x86)", "Games", "Programs"
        };
        public static string BrowseForSave()
        {
            MessageBox.Show("Please Browse for your save file. It will be in your steam folder. Here is an example of the path it should have:\n\nBTD5\nC:\\Program Files (x86)\\Steam\\userdata\\{USERID}\\306020\\local\\Data\\Docs\n\nBTDB:\nC:\\Program Files (x86)\\Steam\\userdata\\{USERID}\\444640\\local\\Data\\Docs\n\nAlso, your {USERID} is a set of numbers that stands for your user id. For example:  44206401");

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Browse";
            ofd.Filter = "Save files (*.save)|*.save|All files (*.*)|*.*";
            if (ofd.ShowDialog() != DialogResult.OK) return "";
            string file = ofd.FileName;

            string[] split = file.Split('\\');
            if (file.Contains("306020")) //this game is btd5
                Serializer.cfg.SavePathBTD5 = file.Replace(split[split.Length - 1], "");
            else if (file.Contains("444640"))   //this game is btdb
                Serializer.cfg.SavePathBTDB = file.Replace(split[split.Length - 1], "");

            Serializer.SaveSettings();
            return file;
        }
        public static void FindSaveFiles()
        {
            ConsoleHandler.append("Attempting to automatically find save folder...");
            string steam = CheckDirsForSteam("\\userdata");
            if (steam != "")
            {
                //Find btd5 save
                string btd5SavePath = FindSaveFolder(steam, "306020");
                if(btd5SavePath != "")
                {
                    Serializer.cfg.SavePathBTD5 = btd5SavePath + "\\local\\Data\\Docs";
                }

                string btdbSavePath = FindSaveFolder(steam, "444640");
                if (btdbSavePath != "")
                {
                    var files = Directory.GetFiles(btdbSavePath, "*", SearchOption.AllDirectories);
                    foreach (string f in files)
                    {
                        if (f.Contains("Profile.save"))
                        {
                            string[] split = f.Split('\\');
                            Serializer.cfg.SavePathBTDB = f.Replace(split[split.Length - 1], "");
                        }
                    }
                }
                Serializer.SaveSettings();
            }
            else
            {
                ConsoleHandler.append_Force_CanRepeat("Failed to automatically find the Steam folder");
                BrowseForSave();
            }
        }
        public static string FindSaveFolder(string path, string searchDir)
        {
            var dirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            foreach(string d in dirs)
            {
                if (d.Contains(searchDir))
                {
                    return d;
                }
            }
            return "";
        }
        public static string CheckDirsForSteam(string searchFolder)
        {
            string path = "";
            
            //Check if Steam is in the main drive
            foreach (string drive in drives)
            {
                foreach (string p in paths)
                {
                    path = drive + p + "\\Steam";
                    if (Directory.Exists(path))
                    {
                        if (Directory.Exists(path + searchFolder))
                        {
                            return path + searchFolder;
                        }
                    }
                }
                
            }
            return "";
        }
        private string checkDirForSteam(string path)
        {
            if(Directory.Exists(path))
            {
                var dir = Directory.GetFiles(path, "*");
                foreach (var a in dir)
                {
                    if (a.Contains("Steam"))
                        return a;
                }
            }
            return "";
        }
        private void HandleSteamPath()
        {

        }
    }
}
