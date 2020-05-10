using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTDToolbox.Classes;

namespace BTDToolbox.Extra_Forms
{
    public partial class BattlesPassManager : Form
    {
        string path = Environment.CurrentDirectory + "\\versions test.json";
        string json = "";
        public BattlesPassManager()
        {
            InitializeComponent();
            json = File.ReadAllText(path);
            Game_LB.SelectedIndex = 0;
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {/*
            if(JSON_Reader.IsValidJson(json))
            {
                var ver = BattlesPassClass.FromJson(json);
                ver.Versions = new VersionElement[10];
                for (int i = 0; i < 10;i++)
                {
                    ver.Versions[i].Version.VersionNumber = i.ToString();
                }
                foreach (var a in ver.Versions)
                {
                    ConsoleHandler.append_CanRepeat(a.Version.VersionNumber);
                }
            }
            else
            {
                ConsoleHandler.append_CanRepeat("Invalid json");
            }
            */
        }

        private void Game_LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Game_LB.SelectedIndex == 0)
            {
                Password_TB.Text = "EB9D43D2ECF10989";
            }
            else if (Game_LB.SelectedIndex == 1)
            {
                Password_TB.Text = "EDB03D8CEE046D18";
            }
            else if (Game_LB.SelectedIndex == 1)
            {
                Password_TB.Text = "913287551902D49F";
            }
            else if (Game_LB.SelectedIndex == 1)
            {
                Password_TB.Text = "3A0620B4AB746982";
            }
        }
    }
}
