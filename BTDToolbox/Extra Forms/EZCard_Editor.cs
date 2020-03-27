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

namespace BTDToolbox.Extra_Forms
{
    public partial class EZCard_Editor : Form
    {
        public string json { get; set; }
        public string path { get; set; }

        Card card;
        Card newCard;
        EasyTowerEditor ezTower;
        bool openStarterCard = false;
        bool openTower = false;
        public static bool EZCard_Opened = false;
        public string[] startingCards_array = new string[0];
        string game = Serializer.Deserialize_Config().CurrentGame;

        public EZCard_Editor()
        {
            InitializeComponent();
            EZCard_Opened = true;
            
            Set_ClickEvents();
        }
        public void CreateCardObject(string cardPath)
        {
            path = cardPath;
            json = File.ReadAllText(cardPath);
            if (JSON_Reader.IsValidJson(json))
            {
                card = Card.FromJson(json);
                PopulateUI();
            }
            else
                ConsoleHandler.force_appendLog_CanRepeat("The file you are trying to load has invalid JSON, and as a result, can't be loaded...");
        }
        private void PopulateUI()
        {
            if (card != null)
            {
                ResetUI();
                RefreshStartCards_LB();
                Card_Label.Text = "Card " + card.Name;
            }
        }
        private void SaveFile()
        {
            bool error = false;
            newCard = new Card();
            newCard = card;
            

            /*try { newBloon.BaseSpeed = Double.Parse(BaseSpeed_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your base speed is not a valid number..."); error = true; }*/

            
            /*if (!error)
            {
                var saveFile = SerializeBloon.ToJson(newCard);
                File.WriteAllText(path, saveFile);
            }*/
        }
        private int CountStartingCards()
        {
            int numStartingCards = 0;
            string cardPath = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BattleCardDefinitions";
            var cardFiles = Directory.GetFiles(cardPath);
            string[] cards_length2 = new string[0];
            string[] cards_length3 = new string[0];

            foreach (var item in cardFiles)
            {
                if(!item.Contains("CacheList"))
                {
                    json = File.ReadAllText(item);
                    if (JSON_Reader.IsValidJson(json))
                    {
                        newCard = new Card();
                        newCard = Card.FromJson(json);
                        if (newCard.StartingCard)
                        {
                            numStartingCards++;
                            string[] split = item.Split('\\');
                            string filename = split[split.Length - 1].Replace("\\", "");

                            if (filename.Replace(".json", "").Length == 1)
                            {
                                Array.Resize(ref startingCards_array, startingCards_array.Length + 1);
                                startingCards_array[startingCards_array.Length - 1] = filename;
                            }
                            else if (filename.Replace(".json", "").Length == 2)
                            {
                                Array.Resize(ref cards_length2, cards_length2.Length + 1);
                                cards_length2[cards_length2.Length - 1] = filename;
                            }
                            else if (filename.Replace(".json", "").Length == 3)
                            {
                                Array.Resize(ref cards_length3, cards_length3.Length + 1);
                                cards_length3[cards_length3.Length - 1] = filename;
                            }
                        }
                    }
                    else
                    {
                        ConsoleHandler.force_appendLog_CanRepeat(item + "  has invalid JSON");
                    }
                }
            }
            //organize cards in numeric order
            int startingCards_arraySize = startingCards_array.Length;
            Array.Resize(ref startingCards_array, startingCards_array.Length + cards_length2.Length + cards_length3.Length);
            Array.Copy(cards_length2, 0, startingCards_array, startingCards_arraySize, cards_length2.Length);
            Array.Copy(cards_length3, 0, startingCards_array, startingCards_arraySize + cards_length2.Length, cards_length3.Length);

            TotalStartingCards_Label.Text = "Total starting cards : " + numStartingCards;
            return numStartingCards;
        }
        private void ResetUI()
        {
            Card_Label.Text = "";
        }
        //
        // Handling UI changes
        //
        private void EasyTowerEditor_Shown(object sender, EventArgs e)
        {
            if (game == "BTDB")
            {
                string gameDir = Serializer.Deserialize_Config().BTDB_Directory;
                if (gameDir != null && gameDir != "")
                {
                    Game_Label.Text = "BTDB";
                    string cardPath = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BattleCardDefinitions";
                    var cardFiles = Directory.GetFiles(cardPath);
                    string[] cards_length2 = new string[0];
                    string[] cards_length3 = new string[0];

                    foreach (string file in cardFiles)
                    {
                        string[] split = file.Split('\\');
                        string filename = split[split.Length - 1].Replace("\\", "");

                        if (filename.EndsWith(".json"))
                        {
                            if (filename.Replace(".json","").Length == 1)
                            {
                                if (!CardFiles_ComboBox.Items.Contains(filename.Replace(".json", "")))
                                    CardFiles_ComboBox.Items.Add("Card " + filename.Replace(".json", ""));
                            }
                            else if (filename.Replace(".json", "").Length == 2)
                            {
                                Array.Resize(ref cards_length2, cards_length2.Length + 1);
                                cards_length2[cards_length2.Length - 1] = "Card " + filename.Replace(".json", "");
                            }
                            else if (filename.Replace(".json", "").Length == 3)
                            {
                                Array.Resize(ref cards_length3, cards_length3.Length + 1);
                                cards_length3[cards_length3.Length - 1] = "Card " + filename.Replace(".json", "");
                            }
                        }
                    }
                    CardFiles_ComboBox.Items.AddRange(cards_length2);
                    CardFiles_ComboBox.Items.AddRange(cards_length3);
                    CardFiles_ComboBox.SelectedIndex = 0;
                }
                else
                {
                    ConsoleHandler.force_appendNotice("You're game directory has not been set! You need to set your game Dir before continuing. You can do this by clicking the \"Help\" tab at the top, then clicking on \"Browse for game\"");
                    this.Close();
                }
            }
            else
            {
                ConsoleHandler.force_appendNotice("Error! This program only works for BTD Battles. Please open a BTD Battles project and try again.");
                this.Close();
            }
        }
        private void CardFiles_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedCard = "";
            if (CardFiles_ComboBox.SelectedItem.ToString().StartsWith("Card "))
                selectedCard = CardFiles_ComboBox.SelectedItem.ToString().Replace("Card ", "") + ".json";
            else
                selectedCard = CardFiles_ComboBox.SelectedItem.ToString() + ".json";
            path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BattleCardDefinitions\\" + selectedCard;
            CreateCardObject(path);
            CountStartingCards();
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            SaveFile();
            ConsoleHandler.appendLog_CanRepeat("Saved " + CardFiles_ComboBox.SelectedItem.ToString());
        }
        private void SwitchPanel_Click(object sender, EventArgs e)
        {
            if (SwitchPanel.Text == "Page 2")
            {
                TowerPanel.Visible = false;
                Tower_Bloon_Panel.Visible = true;
                SwitchPanel.Text = "Page 1";
            }
            else if (SwitchPanel.Text == "Page 1")
            {
                TowerPanel.Visible = true;
                Tower_Bloon_Panel.Visible = false;
                SwitchPanel.Text = "Page 2";
            }
        }
        private void EZBloon_Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                SaveFile();
                ConsoleHandler.appendLog_CanRepeat("Saved " + CardFiles_ComboBox.SelectedItem.ToString());
                GeneralMethods.CompileJet("launch");
                ConsoleHandler.appendLog_CanRepeat("Launching " + game);
            }
        }

        private void OpenText_Button_Click(object sender, EventArgs e)
        {
            OpenInText();
        }
        private void OpenInText()
        {
            if (Serializer.Deserialize_Config().useExternalEditor == false)
            {
                string selectedCard = "";
                if (openStarterCard == true)
                {
                    selectedCard = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BattleCardDefinitions\\" + StartingCards_LB.SelectedItem.ToString();
                }
                else if (openTower == true)
                {
                    if(card.Tower.TowerType != null)
                    {
                        selectedCard = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\TowerDefinitions\\" + card.Tower.TowerType + ".tower";
                    }
                    else
                    {
                        ConsoleHandler.appendLog("This card doesn't have a specified tower type");
                    }
                }
                else
                {
                    selectedCard = path;
                }
                JsonEditor JsonWindow = new JsonEditor(selectedCard);
                JsonWindow.MdiParent = Main.getInstance();
                JsonWindow.Show();
                this.Focus();
            }
            else
            {
                string selectedFile = path;
                Process.Start(selectedFile);
            }
        }
        private void EZBloon_Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            EZCard_Opened = false;
        }

        private void SeeStartingCards_Button_Click(object sender, EventArgs e)
        {
            startingCards_array = new string[0];
            CountStartingCards();
            StartingCards_LB.Items.Clear();
            StartingCards_LB.Items.AddRange(startingCards_array);

            TowerPanel.Visible = false;
        }

        private void StartingCard_OpenText_Button_Click(object sender, EventArgs e)
        {
            if (StartingCards_LB.SelectedIndices.Count == 1)
            {
                openStarterCard = true;
                OpenInText();
                openStarterCard = false;
            }
            else if (StartingCards_LB.SelectedIndices.Count > 1)
            {
                ConsoleHandler.force_appendNotice("Please select only ONE file to continue");
                this.Focus();
            }
            else if (StartingCards_LB.SelectedIndices.Count == 0)
            {
                ConsoleHandler.force_appendNotice("You need to have at least one file selected to contiune...");
                this.Focus();
            }
            Open_Panel.Visible = false;
        }

        private void OpenInEZCard_Button_Click(object sender, EventArgs e)
        {
            if (StartingCards_LB.SelectedIndices.Count == 1)
            {
                CardFiles_ComboBox.SelectedItem = "Card " + StartingCards_LB.SelectedItem.ToString().Replace(".json","");
            }
            else if (StartingCards_LB.SelectedIndices.Count > 1)
            {
                ConsoleHandler.force_appendNotice("Please select only ONE file to continue");
                this.Focus();
            }
            else if (StartingCards_LB.SelectedIndices.Count == 0)
            {
                ConsoleHandler.force_appendNotice("You need to have at least one file selected to contiune...");
                this.Focus();
            }
            Open_Panel.Visible = false;
        }

        private void RefreshStartCards_LB()
        {
            startingCards_array = new string[0];
            CountStartingCards();
            StartingCards_LB.Items.Clear();
            StartingCards_LB.Items.AddRange(startingCards_array);
        }
        private void RefreshList_Button_Click(object sender, EventArgs e)
        {
            RefreshStartCards_LB();
        }
        private void Open_Button_Click(object sender, EventArgs e)
        {
            if(Open_Panel.Visible == false)
                Open_Panel.Visible = true;
            else
                Open_Panel.Visible = false;
        }
        private void Set_ClickEvents()
        {
            this.MouseClick += Close_OpenPanel;

            Card_Label.MouseClick += Close_OpenPanel;
            UnlockMethod_Label.MouseClick += Close_OpenPanel;
            TotalStartingCards_Label.MouseClick += Close_OpenPanel;
            label1.MouseClick += Close_OpenPanel;
            label2.MouseClick += Close_OpenPanel;
            label3.MouseClick += Close_OpenPanel;
            label4.MouseClick += Close_OpenPanel;
            label5.MouseClick += Close_OpenPanel;
            label6.MouseClick += Close_OpenPanel;
            label16.MouseClick += Close_OpenPanel;
            Game_Label.MouseClick += Close_OpenPanel;

            Tower_Bloon_Panel.MouseClick += Close_OpenPanel;
            label12.MouseClick += Close_OpenPanel;
            label13.MouseClick += Close_OpenPanel;
            label14.MouseClick += Close_OpenPanel;
            label15.MouseClick += Close_OpenPanel;
            label17.MouseClick += Close_OpenPanel;
            label18.MouseClick += Close_OpenPanel;
            label19.MouseClick += Close_OpenPanel;
            label20.MouseClick += Close_OpenPanel;
            label21.MouseClick += Close_OpenPanel;
            label22.MouseClick += Close_OpenPanel;
            label23.MouseClick += Close_OpenPanel;
            label24.MouseClick += Close_OpenPanel;
            label25.MouseClick += Close_OpenPanel;

            TowerType_TB.MouseClick += Close_OpenPanel;
            TowerBGSprite_TB.MouseClick += Close_OpenPanel;
            TowerFeatures_LB.MouseClick += Close_OpenPanel;
            Upgrades1_TB.MouseClick += Close_OpenPanel;
            Upgrades2_TB.MouseClick += Close_OpenPanel;
            TowerCost_TB.MouseClick += Close_OpenPanel;
            BloonType_TB.MouseClick += Close_OpenPanel;
            UnlockRound_TB.MouseClick += Close_OpenPanel;
            BloonBGSprite_TB.MouseClick += Close_OpenPanel;
            BloonFeatures_LB.MouseClick += Close_OpenPanel;
            BloonCost_TB.MouseClick += Close_OpenPanel;
            IncomeChange_TB.MouseClick += Close_OpenPanel;
            NumBloons_TB.MouseClick += Close_OpenPanel;
            Interval_TB.MouseClick += Close_OpenPanel;
            Seperator_PB.MouseClick += Close_OpenPanel;

            CardFiles_ComboBox.MouseClick += Close_OpenPanel;
            OpenText_Button.MouseClick += Close_OpenPanel;
            CardName_TB.MouseClick += Close_OpenPanel;
            TowerPanel.MouseClick += Close_OpenPanel;
            CardSet_TB.MouseClick += Close_OpenPanel;
            CardSprite_TB.MouseClick += Close_OpenPanel;
            DiscardCost_TB.MouseClick += Close_OpenPanel;
            OldName_TB.MouseClick += Close_OpenPanel;
            UnlockMethod_LB.MouseClick += Close_OpenPanel;
            StartingCard_CheckBox.MouseClick += Close_OpenPanel;
            Visible_CheckBox.MouseClick += Close_OpenPanel;
            StartingCards_LB.MouseClick += Close_OpenPanel;
            RefreshList_Button.MouseClick += Close_OpenPanel;
            Save_Button.MouseClick += Close_OpenPanel;
            SwitchPanel.MouseClick += Close_OpenPanel;
        }
        private void Close_OpenPanel(object sender, EventArgs e)
        {
            if (Open_Panel.Visible == true)
                Open_Panel.Visible = false;
            if (OpenTower_Panel.Visible == true)
                OpenTower_Panel.Visible = false;
        }

        private void OpenTower_Button_Click(object sender, EventArgs e)
        {
            OpenTower_Panel.Visible = !OpenTower_Panel.Visible;
        }

        private void OpenEZTower_Button_Click(object sender, EventArgs e)
        {
            OpenTower_Panel.Visible = false;
            if (card.Tower.TowerType != null)
            {
                if(ezTower == null)
                {
                     ezTower = new EasyTowerEditor();
                }
                string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\TowerDefinitions\\" + card.Tower.TowerType + ".tower";
                ezTower.path = path;
                ezTower.Show();
                ezTower.CreateTowerObject(path);
            }
            else
            {
                ConsoleHandler.appendLog("This card doesn't have a specified tower type");
            }
        }

        private void OpenTowerText_Button_Click(object sender, EventArgs e)
        {
            OpenTower_Panel.Visible = false;
            openTower = true;
            OpenInText();
            openTower = false;
        }
    }
}
