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
        public static bool EZCard_Opened = false;
        public string[] startingCards_array = new string[0];
        string game = Serializer.Deserialize_Config().CurrentGame;

        public EZCard_Editor()
        {
            InitializeComponent();
            EZCard_Opened = true;
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
                Card_Label.Text = card.Name;
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
                SwitchPanel.Text = "Page 1";
            }
            else if (SwitchPanel.Text == "Page 1")
            {
                TowerPanel.Visible = true;
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
                if (StarterCards_Panel.Visible == true)
                {
                    selectedCard = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BattleCardDefinitions\\" + StartingCards_LB.SelectedItem.ToString();
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

            StarterCards_Panel.Visible = true;
            TowerPanel.Visible = false;
        }

        private void StartingCard_OpenText_Button_Click(object sender, EventArgs e)
        {
            if (StartingCards_LB.SelectedIndices.Count == 1)
            {
                OpenInText();
            }
            else if (StartingCards_LB.SelectedIndices.Count > 1)
            {
                ConsoleHandler.force_appendNotice("Please select only ONE file to continue");
            }
            else if (StartingCards_LB.SelectedIndices.Count == 0)
            {
                ConsoleHandler.force_appendNotice("You need to have at least one file selected to contiune...");
            }
        }

        private void OpenInEZCard_Button_Click(object sender, EventArgs e)
        {
            if (StartingCards_LB.SelectedIndices.Count == 1)
            {
                string selectedCard = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BattleCardDefinitions\\" + StartingCards_LB.SelectedItem.ToString();
                CreateCardObject(selectedCard);
            }
            else if (StartingCards_LB.SelectedIndices.Count > 1)
            {
                ConsoleHandler.force_appendNotice("Please select only ONE file to continue");
            }
            else if (StartingCards_LB.SelectedIndices.Count == 0)
            {
                ConsoleHandler.force_appendNotice("You need to have at least one file selected to contiune...");
            }
        }

        private void RefreshList_Button_Click(object sender, EventArgs e)
        {
            startingCards_array = new string[0];
            CountStartingCards();
            StartingCards_LB.Items.Clear();
            StartingCards_LB.Items.AddRange(startingCards_array);
        }
    }
}
