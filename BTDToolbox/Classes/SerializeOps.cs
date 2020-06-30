using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static BTDToolbox.Main;
using static BTDToolbox.JetForm;
using BTDToolbox.Classes;
using BTDToolbox.Properties;
using BTDToolbox.Classes.NewProjects;

namespace BTDToolbox
{
    
    public class Config
    {
        //Version info
        public string NKHookVersion { get; set; }
        public string TowerLoadNKPluginVersion { get; set; }

        //GameCfg
        public string BTD5_Directory { get; set; }
        public string BTDB_Directory { get; set; }
        public string BMC_Directory { get; set; }
        public string SavePathBTD5 { get; set; }
        public string SavePathBTDB { get; set; }


        //ProjectCfg
        public string battlesPass { get; set; }
        public string LastProject { get; set; }
        public string CurrentGame { get; set; }
        public string ExportPath { get; set; }
        


        //Main Form variables
        public int Main_SizeX { get; set; }
        public int Main_SizeY { get; set; }
        public int Main_PosX { get; set; }
        public int Main_PosY { get; set; }
        public float Main_FontSize { get; set; }


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


        //Switches
        public bool enableSplash { get; set; }
        public bool ExistingUser { get; set; }
        public bool recentUpdate { get; set; }
        public bool useExternalEditor { get; set; }
        public bool disableUpdates { get; set; }
        public bool autoFormatJSON { get; set; }
        public bool nkhookMsgShown { get; set; }
        public bool openTutorialsInToolbox { get; set; }
        public bool UseDeveloperMode { get; set; }

    }

    public class Serializer
    {
        public static Config cfg;
        private static Serializer serializer;
        public static string settingsPath = Environment.CurrentDirectory + "\\settings.json";

        public Serializer()
        {
            if (cfg == null)
                cfg = new Config();
        }

        public void CreateNewSettings()
        {
            //ProjectCfg
            cfg.LastProject = "";
            cfg.CurrentGame = "";
            cfg.ExportPath = "";
            cfg.battlesPass = "";

            //GameCfg
            cfg.BTD5_Directory = "";
            cfg.BTDB_Directory = "";
            cfg.BMC_Directory = "";
            cfg.SavePathBTD5 = "";
            cfg.SavePathBTDB = "";

            //MainCfg
            cfg.Main_SizeX = 1280;
            cfg.Main_SizeY = 720;
            cfg.Main_PosX = 0;
            cfg.Main_PosY = 0;
            cfg.Main_FontSize = 10;

            //ConsoleCfg
            cfg.Console_SizeX = 796;
            cfg.Console_SizeY = 197;
            cfg.Console_PosX = 454;
            cfg.Console_PosY = 448;
            cfg.Console_FontSize = 12;

            //JetViewerCfg
            cfg.JetExplorer_SplitterWidth = 240;
            cfg.JetExplorer_SizeX = 460;
            cfg.JetExplorer_SizeY = 605;
            cfg.JetExplorer_PosX = 0;
            cfg.JetExplorer_PosY = 0;
            cfg.JetExplorer_FontSize = 10;


            //JsonEditorCfg
            cfg.JSON_Editor_SizeX = 796;
            cfg.JSON_Editor_SizeY = 450;
            cfg.JSON_Editor_PosX = 462;
            cfg.JSON_Editor_PosY = 0;
            cfg.JSON_Editor_FontSize = 13;

            //Switches
            cfg.enableSplash = true;
            cfg.recentUpdate = false;
            cfg.useExternalEditor = false;
            cfg.disableUpdates = false;
            cfg.nkhookMsgShown = false;
            cfg.autoFormatJSON = false;
            cfg.openTutorialsInToolbox = true;
            cfg.EnableConsole = true;
            cfg.ExistingUser = false;
            cfg.UseDeveloperMode = false;
            SaveSettings();
        }

        public static Config LoadSettings()
        {
            if (serializer == null)
                serializer = new Serializer();

            if (!File.Exists(settingsPath))
            {
                ConsoleHandler.append("Settings file doesn't exist! Creating a new settings file.");
                serializer.CreateNewSettings();
            }

            if (!JSON_Reader.IsValidJson(File.ReadAllText(settingsPath)))
            {
                ConsoleHandler.append("Settings file has invalid JSON and therefore can't be loaded. Creating a new settings file.");
                serializer.CreateNewSettings();
            }

            string json = File.ReadAllText(Environment.CurrentDirectory + "\\settings.json");
            cfg = JsonConvert.DeserializeObject<Config>(json);
            return cfg;
        }

        public static void SaveSettings()
        {
            if (serializer == null)
                serializer = new Serializer();

            serializer.SaveFormData();
            string output_Cfg = JsonConvert.SerializeObject(cfg, Formatting.Indented);

            StreamWriter serialize = new StreamWriter(settingsPath, false);
            serialize.Write(output_Cfg);
            serialize.Close();
        }

        private void SaveFormData()
        {
            //Main
            if(getInstance() != null)
            {
                cfg.Main_SizeX = getInstance().Size.Width;
                cfg.Main_SizeY = getInstance().Size.Height;
                cfg.Main_PosX = getInstance().Location.X;
                cfg.Main_PosY = getInstance().Location.Y;
                cfg.Main_FontSize = getInstance().Font.Size;
            }

            //console
            if(ConsoleHandler.console != null)
            {
                cfg.Console_SizeX = ConsoleHandler.console.Size.Width;
                cfg.Console_SizeY = ConsoleHandler.console.Size.Height;
                cfg.Console_PosX = ConsoleHandler.console.Location.X;
                cfg.Console_PosY = ConsoleHandler.console.Location.Y;
                cfg.Console_FontSize = ConsoleHandler.console.Font.Size;
            }

            //JetViewer
            SaveLastJetViewer();

            //JsonEditor
            if(JsonEditorHandler.jeditor != null)
            {
                cfg.JSON_Editor_SizeX = JsonEditorHandler.jeditor.Size.Width;
                cfg.JSON_Editor_SizeY = JsonEditorHandler.jeditor.Size.Height;
                cfg.JSON_Editor_PosX = JsonEditorHandler.jeditor.Location.X;
                cfg.JSON_Editor_PosY = JsonEditorHandler.jeditor.Location.Y;
            }
        }

        private void SaveLastJetViewer()
        {
            var list = JetProps.get();
            if (list.Count <= 0)
                return;

            int i = 0;
            foreach (var jetprop in list)
            {
                if (jetprop.projName == CurrentProjectVariables.PathToProjectFiles)
                    break;
                i++;
            }

            i = i - 1;
            cfg.JetExplorer_SizeX = list[i].Size.Width;
            cfg.JetExplorer_SizeY = list[i].Size.Height;
            cfg.JetExplorer_PosX = list[i].Location.X;
            cfg.JetExplorer_PosY = list[i].Location.Y;
            cfg.JetExplorer_FontSize = list[i].Font.Size;
            cfg.JetExplorer_SplitterWidth = jetExplorer_SplitterWidth;
            cfg.JetExplorer_FolderView_FontSize = jetExplorer_FontSize;

            if (CurrentProjectVariables.PathToProjectFiles != null)
                cfg.LastProject = CurrentProjectVariables.PathToProjectFiles.Replace("\\\\", "\\");
        }
    }
}
