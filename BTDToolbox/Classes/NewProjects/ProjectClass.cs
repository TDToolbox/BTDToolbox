using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox.Classes.NewProjects
{
    class ProjectClass
    {
        public class ProjectFile
        {
            public string ProjectName { get; set; }
            public string PathToProjectFiles { get; set; }
            public string PathToProjectClassFile { get; set; }
            public string GameName { get; set; }
            public string GamePath { get; set; }
            public string GameVersion { get; set; }
            public string JetPassword { get; set; }
            public string ExportPath { get; set; }
            public DateTime DateLastOpened { get; set; }
            public bool UseNKHook { get; set; }
            public List<string> JsonEditor_OpenedTabs { get; set; }
            public List<string> ModifiedFiles { get; set; }
        }
    }
}
