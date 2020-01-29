using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static BTDToolbox.ProjectConfig;
using static BTDToolbox.TD_Toolbox_Window;
using static BTDToolbox.JetForm;
namespace BTDToolbox
{
    public class Serializer
    {
        public static void SaveConfig(Form frm, string formName, ConfigFile serialize_config)
        {
            var cfg = Serializer.Deserialize_Config();
            
            if (formName == "main")
            {
                // serialize_config.Main_Fullscreen = WindowState == FormWindowState.Maximized;
                cfg.Main_SizeX = frm.Size.Width;
                cfg.Main_SizeY = frm.Size.Height;
                cfg.Main_PosX = frm.Location.X;
                cfg.Main_PosY = frm.Location.Y;
                cfg.Main_FontSize = frm.Font.Size;
                cfg.EnableConsole = enableConsole;
                cfg.ExistingUser = true;
            }

            if (formName == "console")
            {
                cfg.Console_SizeX = frm.Size.Width;
                cfg.Console_SizeY = frm.Size.Height;
                cfg.Console_PosX = frm.Location.X;
                cfg.Console_PosY = frm.Location.Y;
                cfg.Console_FontSize = frm.Font.Size;
            }

            if (formName == "jet explorer")
            {
                cfg.JetExplorer_SizeX = frm.Size.Width;
                cfg.JetExplorer_SizeY = frm.Size.Height;
                cfg.JetExplorer_PosX = frm.Location.X;
                cfg.JetExplorer_PosY = frm.Location.Y;                
                cfg.JetExplorer_FontSize = frm.Font.Size;

                if (projName == null)
                    cfg.LastProject = cfg.LastProject;
                else
                    cfg.LastProject = projName;
            }

            if (formName == "json editor")
            {
                cfg.JSON_Editor_SizeX = frm.Size.Width;
                cfg.JSON_Editor_SizeY = frm.Size.Height;
                cfg.JSON_Editor_PosX = frm.Location.X;
                cfg.JSON_Editor_PosY = frm.Location.Y;
                cfg.JSON_Editor_FontSize = frm.Font.Size;
            }

            string output_Cfg = JsonConvert.SerializeObject(cfg, Formatting.Indented);

            StreamWriter serialize = new StreamWriter(Environment.CurrentDirectory + "\\settings.json", false);
            serialize.Write(output_Cfg);
            serialize.Close();
        }

        public static ConfigFile Deserialize_Config()
        {
            ConfigFile programData = new ConfigFile();
            if (File.Exists(Environment.CurrentDirectory + "\\settings.json"))
            {
                string json = File.ReadAllText(Environment.CurrentDirectory + "\\settings.json");
                programData = JsonConvert.DeserializeObject<ConfigFile>(json);
            }

            return programData;
        }


    }
}
