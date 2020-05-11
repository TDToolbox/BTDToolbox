using BTDToolbox.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;
using Tower_Class;

namespace BTDToolbox.Extra_Forms
{
    public partial class EZ_Base : Form
    {
        public string json { get; set; }
        public string path { get; set; }

        string gameDir = "";
        int autoscrollcount = 0;
        bool mouseDown = false;
        System.Threading.Thread thread;
        string jetDir = "";
        public static bool EZTBase_Opened = false;


        string game = Serializer.cfg.CurrentGame;

        public EZ_Base()
        {
            InitializeComponent();
            Set_ClickEvents();

            if (game == "BTD5")
            {
                Game_Label.Text = "BTD5";
                gameDir = Serializer.cfg.BTD5_Directory;
            }
            else
            {
                Game_Label.Text = "BTDB";
                gameDir = Serializer.cfg.BTDB_Directory;
            }

            if (gameDir == null || gameDir == "")
            {
                ConsoleHandler.force_append_Notice("You're game directory has not been set! You need to set your game Dir before continuing. You can do this by clicking the \"Help\" tab at the top, then clicking on \"Browse for game\"");
                this.Close();
            }
        }
        public void CreateJSONObject(string filepath)
        {
            string json = File.ReadAllText(filepath);
            if (JSON_Reader.IsValidJson(json))
            {
                ResetUI();
                //Populate UI
            }
            else
            {
                ConsoleHandler.append_Force_CanRepeat("The file you are trying to load has invalid JSON, and as a result, can't be loaded...");
            }

        }
        private void PopulateUI()
        {
            //if json object not invalid
            //{
                
            //}
        }
        private void ResetUI()
        {

        }
        private void SaveFile()
        {
            //bool error = false;

            /*try { newCard.DiscardCost = Int64.Parse(DiscardCost_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_append_Notice("Your discard cost is not a valid number..."); error = true; }*/

            /*if (!error)
            {
                var y = Tower_Class.Serialize.ToJson(artist);
                File.WriteAllText(path, y);
            }*/
        }
        
        private void CardAutoScroll(string direction)
        {
            Invoke((MethodInvoker)delegate {
                if (direction == "back")
                {
                    if (Files_ComboBox.SelectedIndex - 1 >= 0)
                    {
                        Files_ComboBox.SelectedIndex = Files_ComboBox.SelectedIndex - 1;
                    }
                }
                else
                {
                    if (Files_ComboBox.SelectedIndex + 1 <= Files_ComboBox.Items.Count - 1)
                    {
                        Files_ComboBox.SelectedIndex = Files_ComboBox.SelectedIndex + 1;
                    }
                }

            });
            int sleeptime = 125;
            if (autoscrollcount < 3)
                sleeptime = 125;
            else if (autoscrollcount > 3 && autoscrollcount < 8)
                sleeptime = 90;
            else if (autoscrollcount > 8 && autoscrollcount < 15)
                sleeptime = 75;
            else if (autoscrollcount > 15 && autoscrollcount < 22)
                sleeptime = 50;
            else if (autoscrollcount > 22)
                sleeptime = 30;

            System.Threading.Thread.Sleep(sleeptime);
            if (mouseDown == true)
            {
                autoscrollcount++;
                CardAutoScroll(direction);
            }
        }
        private void Set_ClickEvents()
        {
            this.MouseClick += Close_Panels;

            Type_Label.MouseClick += Close_Panels;
            Files_ComboBox.MouseClick += Close_Panels;
            OpenText_Button.MouseClick += Close_Panels;
            OpenNewEZBase_Button.MouseClick += Close_Panels;
            GoToFile_Button.MouseClick += Close_Panels;
            GoToFile_TB.MouseClick += Close_Panels;
            Save_Button.MouseClick += Close_Panels;
            Panel1.MouseClick += Close_Panels;
        }
        private void Close_Panels(object sender, EventArgs e)
        {
            OpenFile_Panel.Hide();
        }
        //
        // Handling UI changes
        //
        private void EZ_Base_Shown(object sender, EventArgs e)
        {
            EZTBase_Opened = true;

            string towersPath = Environment.CurrentDirectory + "\\" + Serializer.cfg.LastProject + "\\Assets\\JSON\\TowerDefinitions";
            var towerFiles = Directory.GetFiles(towersPath);
            foreach (string file in towerFiles)
            {
                string[] split = file.Split('\\');
                string filename = split[split.Length - 1].Replace("\\", "");

                if (!Files_ComboBox.Items.Contains(filename))
                    Files_ComboBox.Items.Add(filename);

                if (file == path)
                    Files_ComboBox.SelectedItem = Files_ComboBox.Items[Files_ComboBox.Items.Count - 1];
            }
        }
        private void Files_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            path = Environment.CurrentDirectory + "\\" + Serializer.cfg.LastProject + jetDir + "\\" + Files_ComboBox.SelectedItem;
            CreateJSONObject(path);
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            SaveFile();
            ConsoleHandler.append_CanRepeat("Saved " + Files_ComboBox.SelectedItem.ToString());
        }
        private void SwitchPanel_Click(object sender, EventArgs e)
        {
            if(Panel1.Visible)
            {
                Panel1.Visible = false;
                SwitchPanel_Button.Text = "Tower";
            }
            else if (!Panel1.Visible)
            {
                Panel1.Visible = true;
                SwitchPanel_Button.Text = "Upgrades";
            }
        }
        private void OpenNewEZBase_Button_Click(object sender, EventArgs e)
        {
            
        }
        private void OpenText_Button_Click(object sender, EventArgs e)
        {
            JsonEditorHandler.OpenFile(path);
            this.Focus();
        }
        private void EZ_Base_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                SaveFile();
                ConsoleHandler.append_CanRepeat("Saved " + Files_ComboBox.SelectedItem.ToString());
                GeneralMethods.CompileJet("launch");
                ConsoleHandler.append_CanRepeat("Launching " + game);
            }
        }
        private void EZ_Base_FormClosed(object sender, FormClosedEventArgs e)
        {
            EZTBase_Opened = false;
        }
        private void PrevCard_Button_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            thread = new System.Threading.Thread(delegate () { CardAutoScroll("back"); });
            thread.Start();
        }
        private void PrevCard_Button_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            autoscrollcount = 0;
            thread.Abort();
        }
        private void NextCard_Button_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            thread = new System.Threading.Thread(delegate () { CardAutoScroll("forward"); });
            thread.Start();
        }
        private void NextCard_Button_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            autoscrollcount = 0;
            thread.Abort();
        }
    }
}
