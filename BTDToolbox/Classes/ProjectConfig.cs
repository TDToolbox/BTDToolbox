using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTDToolbox
{
    public class ProjectConfig
    {
        public class ConfigFile
        {
            //splash screen
            public bool enableSplash { get; set; }
            public bool recentUpdate { get; set; }
            public bool useExternalEditor { get; set; }
            public string battlesPass { get; set; }
            public bool disableUpdates { get; set; }


            //Project wide variables
            public string BTD5_Directory { get; set; }
            public string BTDB_Directory { get; set; }
            public string LastProject { get; set; }
            public string CurrentGame { get; set; }
            public string ExportPath { get; set; }
            public bool ExistingUser { get; set; }


            //Main Form variables
            public int Main_SizeX { get; set; }
            public int Main_SizeY { get; set; }
            public int Main_PosX { get; set; }
            public int Main_PosY { get; set; }
            public float Main_FontSize { get; set; }
            public bool Main_Fullscreen { get; set; }


            //Console variables
            public int Console_SizeX { get; set; }
            public int Console_SizeY { get; set; }
            public int Console_PosX { get; set; }
            public int Console_PosY { get; set; }
            public float Console_FontSize { get; set; }
            public bool EnableConsole { get; set; }


            //Jet Explorer variables
            public int JetExplorer_SplitterWidth { get; set; }
            public float JetExplorer_FolderView_FontSize { get; set; }
            public int JetExplorer_SizeX { get; set; }
            public int JetExplorer_SizeY { get; set; }
            public int JetExplorer_PosX { get; set; }
            public int JetExplorer_PosY { get; set; }
            public float JetExplorer_FontSize { get; set; }


            //JSON Editor variables
            public int JSON_Editor_SizeX { get; set; }
            public int JSON_Editor_SizeY { get; set; }
            public int JSON_Editor_PosX { get; set; }
            public int JSON_Editor_PosY { get; set; }
            public float JSON_Editor_FontSize { get; set; }
            public string[] JsonEditor_OpenedTabs { get; set; }
        }
    }
}
