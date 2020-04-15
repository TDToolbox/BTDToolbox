using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox.Classes.NewProjects
{
    class CurrentProjectVariables
    {
        public static string ProjectName = "";
        public static string PathToProjectFiles = "";
        public static string PathToProjectClassFile = "";
        public static string GameName = "";
        public static string GamePath = "";
        public static string GameVersion = "";
        public static string JetPassword = "";
        public static string ExportPath = "";
        public static DateTime DateLastOpened;
        public static List<string> JsonEditor_OpenedTabs;
        public static List<string> ModifiedFiles;

        public static void ResetProjectVariables()
        {
            ProjectName = "";
            PathToProjectFiles = "";
            PathToProjectClassFile = "";
            GameName = "";
            GamePath = "";
            GameVersion = "";
            JetPassword = "";
            ExportPath = "";
            DateLastOpened = DateTime.Now;
            JsonEditor_OpenedTabs = new List<String>();
            ModifiedFiles = new List<String>();
        }
    }
}
