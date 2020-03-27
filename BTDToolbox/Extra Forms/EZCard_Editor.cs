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
        PowerCard powerCard;
        EasyTowerEditor ezTower;
        EZBloon_Editor ezBloon;

        bool openStarterCard = false;
        bool openTower = false;
        bool openBloon = false;
        bool mouseHolding = false;
        int numStarterCards = 0;
        int autoscrollcount = 0;
        System.Threading.Thread thread;
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

                if (card.CardSet == "Power")
                {
                    HandlePowerCards();
                }
                else
                {
                    Hide_PowerCardStuff();
                }
                PopulateUI_Cards();
            }
            else
                ConsoleHandler.force_appendLog_CanRepeat("The file you are trying to load has invalid JSON, and as a result, can't be loaded...");
        }
        private void HandlePowerCards()
        {
            Tower_Bloon_Panel.Visible = false;
            TowerPanel.Visible = true;
            //SwitchPanel.Visible = false;
            Desc_label.Visible = true;
            Description_TB.Visible = true;
            label1.Visible = true;
            PlatformProduectID_TB.Visible = true;
            ProductID_Label.Visible = true;
            ProductID_TB.Visible = true;
            OldName_TB.Size = new Size(253, 24);
            OldName_Label.Location = new Point(316, 202);
            OldName_TB.Location = new Point(316, 223);
        }
        private void Hide_PowerCardStuff()
        {
            SwitchPanel.Text = "Page 2";
            SwitchPanel.Visible = true;
            Desc_label.Visible = false;
            Description_TB.Visible = false;
            label1.Visible = false;
            PlatformProduectID_TB.Visible = false;
            ProductID_Label.Visible = false;
            ProductID_TB.Visible = false;
            Power_Panel.Visible = false;
            OldName_TB.Size = new Size(527, 24);
            OldName_Label.Location = new Point(42, 251);
            OldName_TB.Location = new Point(42, 272);
        }
        private void PopulateUI_Power()
        {
            ResetUI();
            RefreshStartCards_LB();
            Tower_Bloon_Panel.Visible = false;
            TowerPanel.Visible = true;
            SwitchPanel.Visible = false;
            Desc_label.Visible = true;
            Description_TB.Visible = true;
            label1.Visible = true;
            PlatformProduectID_TB.Visible = true;
            ProductID_Label.Visible = true;
            ProductID_TB.Visible = true;
            OldName_TB.Size = new Size(213, 24);
            OldName_Label.Location = new Point(356, 202);
            OldName_TB.Location = new Point(356, 223);

            Card_Label.Text = "Card " + powerCard.Name;
            CardName_TB.Text = powerCard.Name;
            CardSet_TB.Text = powerCard.CardSet;
            CardSet_LB.SelectedItem = powerCard.CardSet;
            CardSprite_TB.Text = powerCard.CardSprite;
            CardSprites_LB.SelectedItem = powerCard.CardSprite;
            DiscardCost_TB.Text = powerCard.DiscardCost.ToString();
            OldName_TB.Text = powerCard.NameOld;
            ProductID_TB.Text = powerCard.ProductId;
            Description_TB.Text = powerCard.Description;
            PlatformProduectID_TB.Text = powerCard.PlatformProductId;
            UnlockMethod_LB.SelectedItem = powerCard.UnlockMethod;
            UnlockWinCount_TB.Text = powerCard.UnlockWin.Count.ToString();
            UnlockWinType_TB.Text = powerCard.UnlockWin.Type.ToString();
            SpotlightCount_TB.Text = powerCard.UnlockWin.SpotlightCount.ToString();
            
        }
        private void PopulateUI_Cards()
        {
            if (card != null)
            {
                ResetUI();
                RefreshStartCards_LB();

                //Panel1 stuff
                Card_Label.Text = "Card " + card.Name;
                CardName_TB.Text = card.Name;
                CardSet_TB.Text = card.CardSet;
                CardSet_LB.SelectedItem = card.CardSet;
                CardSprite_TB.Text = card.CardSprite;
                CardSprites_LB.SelectedItem = card.CardSprite;
                DiscardCost_TB.Text = card.DiscardCost.ToString();
                OldName_TB.Text = card.NameOld;

                StartingCard_CheckBox.Checked = card.StartingCard;
                Visible_CheckBox.Checked = card.Visible;

                int i = 0;
                foreach (var item in UnlockMethod_LB.Items)
                {
                    if (card.UnlockMethod.ToString().ToLower() == item.ToString().ToLower())
                    {
                        UnlockMethod_LB.SelectedIndex = i;
                        break;
                    }
                    i++;
                }
                if(card.UnlockWin!= null)
                {
                    UnlockWinCount_TB.Text = card.UnlockWin.Count.ToString();
                    UnlockWinType_TB.Text = card.UnlockWin.Type.ToString();
                    SpotlightCount_TB.Text = card.UnlockWin.SpotlightCount.ToString();
                }

                //Panel2 stuff
                //Tower stuff
                if(card.Tower != null)
                {
                    TowerType_TB.Text = card.Tower.TowerType;
                    TowerBGSprite_TB.Text = card.Tower.BackgroundSprite;
                    Upgrades1_TB.Text = card.Tower.Upgrades[0].ToString();
                    Upgrades2_TB.Text = card.Tower.Upgrades[1].ToString();
                    TowerCost_TB.Text = card.Tower.Cost.ToString();
                    if (card.Tower.Features != null)
                    {
                        foreach (var cardfeats in card.Tower.Features)
                        {
                            TowerFeatures_LB.SelectedItem = cardfeats;
                        }
                    }
                }             

                //Bloon stuff
                if(card.Bloon != null)
                {
                    BloonType_TB.Text = card.Bloon.BloonType;
                    UnlockRound_TB.Text = card.Bloon.UnlockRound.ToString();
                    BloonBGSprite_TB.Text = card.Bloon.BackgroundSprite;
                    BloonCost_TB.Text = card.Bloon.Cost.ToString();
                    IncomeChange_TB.Text = card.Bloon.IncomeChange.ToString();
                    NumBloons_TB.Text = card.Bloon.NumBloons.ToString();
                    Interval_TB.Text = card.Bloon.Interval.ToString();
                    foreach (var bloonfeats in card.Bloon.Features)
                    {
                        BloonFeatures_LB.SelectedItem = bloonfeats;
                    }
                }

                //Power stuff
                if(card.Power != null)
                {
                    PlatformProduectID_TB.Text = card.PlatformProductId;
                    ProductID_TB.Text = card.ProductId;
                    Description_TB.Text = card.Description;
                    PowerBGSprite_TB.Text = card.Power.BackgroundSprite;
                    PowerCost_TB.Text = card.Power.Cost.ToString();
                    PowerType_TB.Text = card.Power.Type;

                    if (card.Power.Features != null)
                    {
                        foreach (var powerfeats in card.Power.Features)
                        {
                            PowerFeatures_LB.SelectedItem = powerfeats;
                        }
                    }
                }
            }
        }
        private void SaveFile()
        {
            bool error = false;
            newCard = new Card();
            newCard = card;

            //Panel 1 stuff
            newCard.Name = CardName_TB.Text;
            newCard.CardSet = CardSet_TB.Text;
            newCard.CardSprite = CardSprite_TB.Text;
            newCard.NameOld = OldName_TB.Text;
            if(UnlockMethod_LB.SelectedItems.Count == 1)
                newCard.UnlockMethod = UnlockMethod_LB.SelectedItem.ToString();
            else
            {
                ConsoleHandler.force_appendNotice("You need to select at least one Unlock method!");
                error = true;
            }
            newCard.StartingCard = StartingCard_CheckBox.Checked;
            newCard.Visible = Visible_CheckBox.Checked;
            try { newCard.DiscardCost = Int64.Parse(DiscardCost_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your discard cost is not a valid number..."); error = true; }


            //Panel 2 stuff
            //Tower stuff
            newCard.Tower.TowerType = TowerType_TB.Text;
            newCard.Tower.BackgroundSprite = TowerBGSprite_TB.Text;
            TowerFeatures_LB.SelectedItems.CopyTo(newCard.Tower.Features, 0);

            try { newCard.Tower.Upgrades[0] = Int64.Parse(Upgrades1_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your left path upgrade is not a valid number..."); error = true; }
            try { newCard.Tower.Upgrades[1] = Int64.Parse(Upgrades2_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your right path upgrade is not a valid number..."); error = true; }
            try { newCard.Tower.Cost = Int64.Parse(TowerCost_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your tower cost is not a valid number..."); error = true; }

            //Bloon shit
            newCard.Bloon.BloonType = BloonType_TB.Text;
            newCard.Bloon.BackgroundSprite = BloonBGSprite_TB.Text;
            BloonFeatures_LB.SelectedItems.CopyTo(newCard.Bloon.Features, 0);

            try { newCard.Bloon.UnlockRound = Int64.Parse(UnlockRound_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your unlock round is not a valid number..."); error = true; }
            try { newCard.Bloon.Cost = Int64.Parse(BloonCost_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your bloon cost is not a valid number..."); error = true; }
            try { newCard.Bloon.IncomeChange = Int64.Parse(IncomeChange_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your income changed amount is not a valid number..."); error = true; }
            try { newCard.Bloon.Interval = Double.Parse(Interval_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your interval amount is not a valid number..."); error = true; }
            try { newCard.Bloon.NumBloons = Int64.Parse(NumBloons_TB.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your num bloons amount is not a valid number..."); error = true; }

            if (!error)
            {
                var saveFile = SerializeCard.ToJson(newCard);
                File.WriteAllText(path, saveFile);
            }

            RefreshStartCards_LB();
        }
        private int CountStartingCards()
        {
            numStarterCards = 0;
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
                            numStarterCards++;
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

            TotalStartingCards_Label.Text = "Total starting cards : " + numStarterCards;
            return numStarterCards;
        }
        private void ResetUI()
        {
            //Panel1 stuff
            Card_Label.Text = "";
            CardName_TB.Text = "";
            CardSet_TB.Text = "";
            CardSprite_TB.Text = "";
            DiscardCost_TB.Text = "";
            OldName_TB.Text = "";
            StartingCard_CheckBox.Checked = false;
            Visible_CheckBox.Checked = false;
            UnlockMethod_LB.SelectedIndex = -1;
            GoToCard_TB.Text = "";

            //Panel2 stuff
            //Tower stuff
            TowerType_TB.Text = "";
            TowerBGSprite_TB.Text = "";
            Upgrades1_TB.Text = "";
            Upgrades2_TB.Text = "";
            TowerCost_TB.Text = "";
            TowerFeatures_LB.SelectedIndex = -1;

            //Bloon stuff
            BloonType_TB.Text = "";
            UnlockRound_TB.Text = "";
            BloonBGSprite_TB.Text = "";
            BloonCost_TB.Text = "";
            IncomeChange_TB.Text = "";
            NumBloons_TB.Text = "";
            Interval_TB.Text = "";
            BloonFeatures_LB.SelectedIndex = -1;

            //Power stuff
            UnlockWinCount_TB.Text = "";
            UnlockWinType_TB.Text = "";
            SpotlightCount_TB.Text = "";
            PlatformProduectID_TB.Text = "";
            ProductID_TB.Text = "";
            Description_TB.Text = "";
            PowerCost_TB.Text = "";
            PowerType_TB.Text = "";
            PowerBGSprite_TB.Text = "";
            CardSet_LB.SelectedIndex = -1;
            CardSprites_LB.SelectedIndex = -1;
            PowerFeatures_LB.SelectedIndex = -1;
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
            if (CardSet_TB.Text == "Power")
            {
                if (SwitchPanel.Text == "Power")
                {
                    Power_Panel.Visible = true;
                    SwitchPanel.Text = "Hide";
                }
                else if (SwitchPanel.Text == "Hide")
                {
                    Power_Panel.Visible = false;
                    SwitchPanel.Text = "Power";
                }
            }
            else if (SwitchPanel.Text == "Page 2")
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
            if (e.KeyCode == Keys.Enter)
            {
                if (GoToCard_TB.Text.Length>0)
                {
                    GoToFile();
                    GoToCard_TB.Text = "";
                }
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
                else if (openBloon == true)
                {
                    if (card.Bloon.BloonType != null)
                    {
                        selectedCard = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions\\" + card.Bloon.BloonType + ".bloon";
                    }
                    else
                    {
                        ConsoleHandler.appendLog("This card doesn't have a specified bloon type");
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
            if(CountStartingCards() < 15)
            {
                ConsoleHandler.force_appendNotice("ERROR! You need at least 15 starter cards or the game will crash!");
                this.Focus();
            }
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
            DiscardCost_Label.MouseClick += Close_OpenPanel;
            CardName_Label.MouseClick += Close_OpenPanel;
            label3.MouseClick += Close_OpenPanel;
            CardSet_Label.MouseClick += Close_OpenPanel;
            CardSprite_Label.MouseClick += Close_OpenPanel;
            StartingCards_Label.MouseClick += Close_OpenPanel;
            OldName_Label.MouseClick += Close_OpenPanel;
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
            //SwitchPanel.MouseClick += Close_OpenPanel;
            OpenEZTower_Button.MouseClick += Close_OpenPanel;
            OpenEZBloon_Button.MouseClick += Close_OpenPanel;

            GoToCard_Button.MouseClick += Close_OpenPanel;
            GoToCard_TB.MouseClick += Close_OpenPanel;
            PlatformProduectID_TB.MouseClick += Close_OpenPanel;
            label1.MouseClick += Close_OpenPanel;
            ProductID_Label.MouseClick += Close_OpenPanel;
            ProductID_TB.MouseClick += Close_OpenPanel;
            CardSprites_LB.MouseClick += Close_OpenPanel;
            label10.MouseClick += Close_OpenPanel;
            CardSprites_Panel.MouseClick += Close_OpenPanel;
            Description_TB.MouseClick += Close_OpenPanel;
            Desc_label.MouseClick += Close_OpenPanel;
            CardSet_LB.MouseClick += Close_OpenPanel;
            label7.MouseClick += Close_OpenPanel;
            CardSets_Panel.MouseClick += Close_OpenPanel;
        }
        private void Close_OpenPanel(object sender, EventArgs e)
        {
            if (Open_Panel.Visible == true)
                Open_Panel.Visible = false;
            if (OpenTower_Panel.Visible == true)
                OpenTower_Panel.Visible = false;
            if (OpenBloon_Panel.Visible == true)
                OpenBloon_Panel.Visible = false;
            if (UnlockWin_Panel.Visible == true)
                UnlockWin_Panel.Visible = false;
            if (CardSprites_Panel.Visible == true)
                CardSprites_Panel.Visible = false;
            if (CardSets_Panel.Visible == true)
                CardSets_Panel.Visible = false;
            if (Power_Panel.Visible == true)
            {
                Power_Panel.Visible = false;
                SwitchPanel.Text = "Power";
            }
            
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
        private void OpenBloon_Button_Click(object sender, EventArgs e)
        {
            OpenBloon_Panel.Visible = !OpenBloon_Panel.Visible;
        }
        private void OpenBloonText_Button_Click(object sender, EventArgs e)
        {
            OpenBloon_Panel.Visible = false;
            openBloon = true;
            OpenInText();
            openBloon = false;
        }
        private void OpenEZBloon_Button_Click(object sender, EventArgs e)
        {
            OpenBloon_Panel.Visible = false;
            if (card.Bloon.BloonType != null)
            {
                if (ezBloon == null)
                {
                    ezBloon = new EZBloon_Editor();
                }
                string path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions\\" + card.Bloon.BloonType + ".bloon";
                ezBloon.path = path;
                ezBloon.Show();
                ezBloon.CreateBloonObject(path);
            }
            else
            {
                ConsoleHandler.appendLog("This card doesn't have a specified tower type");
            }
        }
        private void GoToCard_Button_Click(object sender, EventArgs e)
        {
            GoToFile();
        }
        private void GoToFile()
        {
            if (GoToCard_TB.Text.Length > 0)
            {
                if (CardFiles_ComboBox != null)
                {
                    string searchText = GoToCard_TB.Text.Trim().Replace(" ","").Replace("\n", "");
                    if (!searchText.Contains("Card"))
                        searchText = "Card " + GoToCard_TB.Text;

                    if (CardFiles_ComboBox.Items.Contains(searchText))
                    {
                        CardFiles_ComboBox.SelectedItem = searchText;
                    }
                    if (!CardFiles_ComboBox.Items.Contains(searchText))
                    {
                        if (CardFiles_ComboBox.Items.Contains(searchText.Replace(".json", "")))
                        {
                            CardFiles_ComboBox.SelectedItem = searchText;
                        }
                        else if (CardFiles_ComboBox.Items.Contains(searchText.Replace("Card ", "")))
                        {
                            CardFiles_ComboBox.SelectedItem = searchText;
                        }
                        else
                        {
                            ConsoleHandler.force_appendLog_CanRepeat("Could not find that card...");
                            this.Focus();
                        }
                    }
                    GoToCard_TB.Text = "";
                }
            }
            else
            {
                ConsoleHandler.force_appendNotice("You didn't enter a card name to search...");
                this.Focus();
            }
        }
        private void EZCard_Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (numStarterCards < 15)
            {
                MessageBox.Show("ERROR! You don't have at least 15 Starter Cards! The game will crash if you don't have at least 15 starter cards");
            }
        }
        private void UnlockMethod_LB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(UnlockMethod_LB.SelectedIndex == 2)
            {
                UnlockWin_Button.Visible = true;
            }
            else
            {
                UnlockWin_Button.Visible = false;
            }
        }
        private void UnlockWin_Button_Click(object sender, EventArgs e)
        {
            UnlockWin_Panel.Visible = !UnlockWin_Panel.Visible;
        }
        private void PrevCard_Button_MouseDown(object sender, MouseEventArgs e)
        {
            mouseHolding = true;
            thread = new System.Threading.Thread(delegate () { CardAutoScroll("back"); });
            thread.Start();
        }
        private void CardAutoScroll(string direction)
        {
            Invoke((MethodInvoker)delegate {
                if (direction == "back")
                {
                    if (CardFiles_ComboBox.SelectedIndex - 1 >= 0)
                    {
                        CardFiles_ComboBox.SelectedIndex = CardFiles_ComboBox.SelectedIndex - 1;
                    }
                }
                else
                {
                    if (CardFiles_ComboBox.SelectedIndex + 1 <= CardFiles_ComboBox.Items.Count - 1)
                    {
                        CardFiles_ComboBox.SelectedIndex = CardFiles_ComboBox.SelectedIndex + 1;
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
            if (mouseHolding == true)
            {
                autoscrollcount++;
                CardAutoScroll(direction);
            }
        }
        private void PrevCard_Button_MouseUp(object sender, MouseEventArgs e)
        {
            mouseHolding = false;
            autoscrollcount = 0;
            thread.Abort();
        }
        private void NextCard_Button_MouseDown(object sender, MouseEventArgs e)
        {
            mouseHolding = true;
            thread = new System.Threading.Thread(delegate () { CardAutoScroll("forward"); });
            thread.Start();
        }
        private void NextCard_Button_MouseUp(object sender, MouseEventArgs e)
        {
            mouseHolding = false;
            autoscrollcount = 0;
            thread.Abort();
        }
        private void CardSprites_Button_Click(object sender, EventArgs e)
        {
            CardSprites_Panel.Visible = !CardSprites_Panel.Visible;
        }
        private void CardSprites_LB_SelectedValueChanged(object sender, EventArgs e)
        {
            if(CardSprites_LB.SelectedItem != null)
            {
                CardSprite_TB.Text = CardSprites_LB.SelectedItem.ToString();
            }
        }
        private void CardSets_Button_Click(object sender, EventArgs e)
        {
            CardSets_Panel.Visible = !CardSets_Panel.Visible;
        }

        private void CardSet_LB_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CardSet_LB.SelectedItem != null)
            {
                CardSet_TB.Text = CardSet_LB.SelectedItem.ToString();
                if (CardSet_TB.Text == "Power")
                {
                    SwitchPanel.Text = "Power";
                    HandlePowerCards();
                }

                else
                {
                    Hide_PowerCardStuff();
                    SwitchPanel.Text = "Page 2";
                }
            }
        }

        private void Reset_Button_Click(object sender, EventArgs e)
        {
            ResetUI();
        }
    }
}
