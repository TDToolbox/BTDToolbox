using BTDToolbox.Classes;
using BTDToolbox.Classes.JSON_Classes;
using BTDToolbox.Classes.NewProjects;
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
    public partial class EasyTowerEditor : Form
    {
        public string json { get; set; }
        public string path { get; set; }

        Tower_Class.Tower artist;
        bool finishedLoading = false;
        bool firstLoad = false;
        public static bool EZTower_Opened = false;
        string[] upgradenames = new string[] { };
        string[] upgradeIcons = new string[] { };
        string[] upgradeAvatars = new string[] { };
        string[] upgradePrices = new string[] { };
        string[] upgradeRanks = new string[] { };
        string[] upgradeXPs = new string[] { };
        
        string[] loc_upgradeNames = new string[] { };
        string[] loc_upgradeDescs = new string[] { };

        string game = "";
        string gameDir = "";
        string[] loc_Text = new string[] { };
        string loc_Path = "";
        string loc_towerName = "";
        string loc_towerDesc = "";

        //Open button stuff (god i need to fix this)
        string file = "";
        string towerSpriteUpgradeDef = ""; //if user set custom tower sprite upgrade def
        string towerName = ""; //if user set custom tower sprite upgrade def, used to keep track of actual tower
        string towerTypeName = "";
        string specialty = "";
        string filename = ""; //this is to get the TowerName.tower part
        int checkboxesChecked = 0; //number of SelectionMenuCheckboxes Checked
        bool useBaseTower;

        public EasyTowerEditor()
        {
            InitializeComponent();
            if (NKHook.CanUseNKH())
                NewTower_Button.Visible = true;

            if(!Guard.IsStringValid(CurrentProjectVariables.PathToProjectFiles))
            {
                ConsoleHandler.append("Can't use EZ Tower tool because you don't have a project opened");
                this.Close();
                return;
            }
            Weapons_Button.DropDownItemClicked += Weapons_Button_Click;

            SelectionMenu_Left_CB.CheckedChanged += SelectionMenu_ItemChecked;
            SelectionMenu_Right_CB.CheckedChanged += SelectionMenu_ItemChecked;
            SelectionMenu_FixedLeft_CB.CheckedChanged += SelectionMenu_ItemChecked;
            SelectionMenu_FixedRight_CB.CheckedChanged += SelectionMenu_ItemChecked;

            EZTower_Opened = true;
            if (game == "BTDB")
            {
                label2.Hide();
                TowerName_TextBox.Hide();
            }
            else
            {
                label2.Show();
                TowerName_TextBox.Show();

                if(game == "BTD5")
                {
                    if (NKHook.DoesNkhExist())
                        NewTower_Button.Visible = true;
                }
            }
            game = CurrentProjectVariables.GameName;
            gameDir = CurrentProjectVariables.GamePath;

            this.Show();
        }

        private void SelectionMenu_ItemChecked(object sender, EventArgs e)
        {
            checkboxesChecked = 0;

            if (SelectionMenu_Left_CB.Checked)
                checkboxesChecked++;

            if (SelectionMenu_Right_CB.Checked)
                checkboxesChecked++;

            if (SelectionMenu_FixedLeft_CB.Checked)
                checkboxesChecked++;

            if (SelectionMenu_FixedRight_CB.Checked)
                checkboxesChecked++;
        }

        public void CreateTowerObject(string towerPath)
        {
            string json = File.ReadAllText(towerPath);
            if (!JSON_Reader.IsValidJson(json))
                ConsoleHandler.append_Force_CanRepeat("The file you are trying to load has invalid JSON, and as a result, can't be loaded...");

            artist = Tower_Class.Tower.FromJson(json);
            PopulateUI();
            string[] split = towerPath.Split('\\');
            filename = split[split.Length - 1];
            PopulateToolbar();

            finishedLoading = true;
        }
        private void PopulateToolbar()
        {
            PopulateOpenButton();
        }
        private void PopulateUI()
        {
            if (artist != null)
            {
                ResetUI();
                TowerType_Label.Text = artist.TypeName;

                string name = "";
                if (!Guard.IsStringValid(artist.Name))
                    name = artist.TypeName;
                else
                    name = artist.Name;

                TowerName_TextBox.Text = name;

                BaseCost_TextBox.Text = artist.BaseCost.ToString();
                RankToUnlock_TextBox.Text = artist.RankToUnlock.ToString();
                Icon_TextBox.Text = artist.Icon;
                SpriteUpgradeDef_TextBox.Text = artist.SpriteUpgradeDefinition;

                if (artist.CanTargetCamo == null)
                    CanTargetCamo_CheckBox.Checked = false;
                else
                    CanTargetCamo_CheckBox.Checked = artist.CanTargetCamo.Value;

                if (artist.RotatesToTarget == null)
                    RotateToTarget_CheckBox.Checked = false;
                else
                    RotateToTarget_CheckBox.Checked = artist.RotatesToTarget.Value;

                if (artist.TargetsManually == null)
                    TargetsManually_CheckBox.Checked = false;
                else
                    TargetsManually_CheckBox.Checked = artist.TargetsManually.Value;

                if (artist.TargetIsWeaponOrigin == null)
                    TargetIsWeaponOrigin_CheckBox.Checked = false;
                else
                    TargetIsWeaponOrigin_CheckBox.Checked = artist.TargetIsWeaponOrigin.Value;

                if (artist.CanBePlacedInWater == null)
                    CanBePlacedInWater_CheckBox.Checked = false;
                else
                    CanBePlacedInWater_CheckBox.Checked = artist.CanBePlacedInWater.Value;

                if (artist.CanBePlacedOnLand == null)
                    CanBePlacedOnLand_CheckBox.Checked = false;
                else
                    CanBePlacedOnLand_CheckBox.Checked = artist.CanBePlacedOnLand.Value;

                if (artist.CanBePlacedOnPath == null)
                    CanBePlacedOnPath_CheckBox.Checked = false;
                else
                    CanBePlacedOnPath_CheckBox.Checked = artist.CanBePlacedOnPath.Value;

                if (artist.UseRadiusPlacement == null)
                    UsePlacementRadius_Checkbox.Checked = false;
                else
                    UsePlacementRadius_Checkbox.Checked = artist.UseRadiusPlacement.Value;

                PlacementH_TextBox.Text = artist.PlacementH.ToString();
                PlacementW_TextBox.Text = artist.PlacementW.ToString();
                PlacementRadius_TextBox.Text = artist.PlacementRadius.ToString();

                if (artist.TargetingMode == null)
                    TargetingMode_ComboBox.SelectedItem = TargetingMode_ComboBox.Items[0];
                else
                    TargetingMode_ComboBox.SelectedItem = artist.TargetingMode;


                //Upgrade stuff
                upgradenames = new string[] { };
                upgradeIcons = new string[] { };
                upgradeAvatars = new string[] { };
                upgradePrices = new string[] { };
                upgradeRanks = new string[] { };
                upgradeXPs = new string[] { };
                loc_upgradeNames = new string[] { };
                loc_upgradeDescs = new string[] { };

                upgradenames = CreateStringArray(upgradenames, artist.Upgrades);
                upgradeIcons = CreateStringArray(upgradeIcons, artist.UpgradeIcons);
                upgradeAvatars = CreateStringArray(upgradeAvatars, artist.UpgradeAvatars);

                if (artist.UpgradePrices != null)
                {
                    foreach (long[] a in artist.UpgradePrices)
                    {
                        foreach (long b in a)
                        {
                            Array.Resize(ref upgradePrices, upgradePrices.Length + 1);
                            upgradePrices[upgradePrices.Length - 1] = b.ToString();
                        }
                    }
                }
                else
                {
                    Upgrades_ListBox.Items.Clear();
                    Upgrades_ListBox.Refresh();
                }

                var g = artist.UpgradeGateway;
                if (g != null)
                {
                    foreach (var a in g)
                    {
                        foreach (var b in a)
                        {
                            Array.Resize(ref upgradeRanks, upgradeRanks.Length + 1);
                            upgradeRanks[upgradeRanks.Length - 1] = b.Rank.ToString();

                            Array.Resize(ref upgradeXPs, upgradeXPs.Length + 1);
                            upgradeXPs[upgradeXPs.Length - 1] = b.Xp.ToString();
                        }
                    }
                }

                if (game == "BTDB")
                {
                    if (artist.UpgradeDescriptions != null)
                    {
                        foreach (string[] a in artist.UpgradeDescriptions)
                        {
                            foreach (string b in a)
                            {
                                Array.Resize(ref loc_upgradeDescs, loc_upgradeDescs.Length + 1);
                                loc_upgradeDescs[loc_upgradeDescs.Length - 1] = b;
                            }
                        }
                    }

                    if (artist.Description != null)
                    {
                        loc_towerDesc = artist.Description;
                        TowerDesc_TextBox.Text = loc_towerDesc;
                    }
                }
                if(game == "BTD5" || game == "BMC")
                {
                    if(!File.Exists(loc_Path))
                    {
                        ConsoleHandler.append("LOC file dosn't exist! Couldn't find: " + loc_Path);
                    }
                    else
                    {
                        ReadLoc();

                        string gameDir = CurrentProjectVariables.GamePath;
                        if (!Guard.IsStringValid(gameDir))
                        {
                            ConsoleHandler.force_append_Notice("You haven't browsed for your " + game + " Game so you will not be able to edit the tower and upgrade descriptions");
                            this.Focus();
                        }
                    }
                   
                }

                if (upgradenames != null)
                {
                    for (int i = 0; i < upgradenames.Length; i++)
                    {
                        Upgrades_ListBox.Items.Add(upgradenames[i]);
                    }
                    if (Upgrades_ListBox.Items.Count != 0)
                    {
                        Upgrades_ListBox.SelectedIndex = 0;
                        PopulateUpgrades();
                    }
                }

                if (AllTowerFiles_ComboBox.SelectedItem != null)
                {
                    if (TowerType_Label.Text + ".tower" != AllTowerFiles_ComboBox.SelectedItem.ToString())
                    {
                        AllTowerFiles_ComboBox.SelectedItem = TowerType_Label.Text + ".tower";
                    }
                }
            }        
        }
        private void AskToGetNewLoc()
        {
            DialogResult diag = MessageBox.Show("Unable to find " + game + "'s LOC file. Do you want toolbox to make Steam validate " + game + " so it can reaquire it? If you have any mods applied to " + game + " steam, they will be lost. Do you want to continue?", "Continue?", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                if (game == "BTD5")
                    GeneralMethods.SteamValidate("BTD5");
                else
                    GeneralMethods.SteamValidate("BMC");
            }
            else
            {
                ConsoleHandler.force_append_Notice("Valdation cancelled. You will not be able to mod the tower or upgrade descriptions...");
                this.Focus();
            }
        }
        private void SaveFile()
        {
            try { artist.BaseCost = Int32.Parse(BaseCost_TextBox.Text); }
            catch (FormatException) { ConsoleHandler.force_append_Notice("Your base cost is not a valid number..."); }

            try { artist.RankToUnlock = Int32.Parse(RankToUnlock_TextBox.Text); }
            catch (FormatException) { ConsoleHandler.force_append_Notice("Your Rank To Unlock is not a valid number..."); }

            artist.Icon = Icon_TextBox.Text;
            artist.TargetingMode = TargetingMode_ComboBox.SelectedItem.ToString();
            artist.CanTargetCamo = CanTargetCamo_CheckBox.Checked;
            artist.RotatesToTarget = RotateToTarget_CheckBox.Checked;
            artist.TargetsManually = TargetsManually_CheckBox.Checked;
            artist.TargetIsWeaponOrigin = TargetIsWeaponOrigin_CheckBox.Checked;
            artist.CanBePlacedInWater = CanBePlacedInWater_CheckBox.Checked;
            artist.CanBePlacedOnLand = CanBePlacedOnLand_CheckBox.Checked;
            artist.CanBePlacedOnPath = CanBePlacedOnPath_CheckBox.Checked;
            artist.UseRadiusPlacement = UsePlacementRadius_Checkbox.Checked;

            if(UsePlacementRadius_Checkbox.Checked == false)
            {
                try { artist.PlacementH = Int32.Parse(PlacementH_TextBox.Text); }
                catch (FormatException) { ConsoleHandler.force_append_Notice("Your Placement Height is not a valid number..."); }

                try { artist.PlacementW = Int32.Parse(PlacementW_TextBox.Text); }
                catch (FormatException) { ConsoleHandler.force_append_Notice("Your Placement Width is not a valid number..."); }

                artist.PlacementRadius = 0;
            }
            else
            {
                try { artist.PlacementRadius = Int32.Parse(PlacementRadius_TextBox.Text); }
                catch (FormatException) { ConsoleHandler.force_append_Notice("Your Placement Radius is not a valid number..."); }

                artist.PlacementH = 0;
                artist.PlacementW = 0;
            }

            


            //Upgrade stuff
            artist.SpriteUpgradeDefinition = SpriteUpgradeDef_TextBox.Text;

            //
            //BUGS BE HERE IF ADDING NEW UPGRADE TIERS
            artist.Upgrades = CreateDoubleArray_String(upgradenames);
            artist.UpgradeIcons = CreateDoubleArray_String(upgradeIcons);
            artist.UpgradeAvatars = CreateDoubleArray_String(upgradeAvatars);
            artist.UpgradePrices = CreateDoubleArray_Long(upgradePrices);
            artist.UpgradeGateway = CreateDoubleArray_UpgradeGateway(CreateArray_Long(upgradeRanks), CreateArray_Long(upgradeXPs));


            var y = Tower_Class.Serialize.ToJson(artist);
            File.WriteAllText(path, y);
        }
        private Tower_Class.UpgradeGateway[][] CreateDoubleArray_UpgradeGateway(long[] rankArray, long[] XpArray)
        {
            int upgradeCount = upgradenames.Length / 2;
            var leftPath = new Tower_Class.UpgradeGateway[] { };
            var rightPath = new Tower_Class.UpgradeGateway[] { };
            

            for (int i = 0; i < upgradeCount; i++)
            {
                Array.Resize(ref leftPath, leftPath.Length + 1);

                var upgradeG = new Tower_Class.UpgradeGateway();
                upgradeG.Rank = rankArray[i];
                upgradeG.Xp = XpArray[i];
                leftPath[leftPath.Length - 1] = upgradeG;
            }
            for (int i = upgradeCount; i < upgradeCount * 2; i++)
            {
                Array.Resize(ref rightPath, rightPath.Length + 1);

                var upgradeG = new Tower_Class.UpgradeGateway();
                upgradeG.Rank = rankArray[i];
                upgradeG.Xp = XpArray[i];
                rightPath[rightPath.Length - 1] = upgradeG;
            }
            Tower_Class.UpgradeGateway[][] upgrades = new Tower_Class.UpgradeGateway[][] { leftPath, rightPath };
            return upgrades;
        }
        private string[][] CreateDoubleArray_String(string[] inputArray)
        {
            int upgradeCount = inputArray.Length / 2;
            
            string[] leftPath = new string[] { };
            string[] rightPath = new string[] { };

            for (int i = 0; i < upgradeCount; i++)
            {
                Array.Resize(ref leftPath, leftPath.Length + 1);
                leftPath[leftPath.Length - 1] = inputArray[i];
            }
            for (int i = upgradeCount; i < upgradeCount * 2; i++)
            {
                Array.Resize(ref rightPath, rightPath.Length + 1);
                rightPath[rightPath.Length - 1] = inputArray[i];
            }
            string[][] upgrades = new string[][] { leftPath, rightPath };

            return upgrades;
        }
        private long[] CreateArray_Long(string[] inputArray)
        {
            int upgradeCount = inputArray.Length;
            long[] array = new long[] { };

            for (int i = 0; i < upgradeCount; i++)
            {
                Array.Resize(ref array, array.Length + 1);
                try { array[array.Length - 1] = Int32.Parse(inputArray[i]); }
                catch (FormatException) { ConsoleHandler.force_append_Notice("Invalid long number detected in the right path..."); }

            }
            return array;
        }
        private long[][] CreateDoubleArray_Long(string[] inputArray)
        {
            int upgradeCount = inputArray.Length / 2;
            long[] leftPath = new long[] { };
            long[] rightPath = new long[] { };

            for (int i = 0; i < upgradeCount; i++)
            {
                Array.Resize(ref leftPath, leftPath.Length + 1);
                try { leftPath[leftPath.Length - 1] = Int32.Parse(inputArray[i]); }
                catch (FormatException) { ConsoleHandler.force_append_Notice("Invalid long number detected in the left path..."); }
                
            }
            for (int i = 0; i < upgradeCount; i++)
            {
                Array.Resize(ref rightPath, rightPath.Length + 1);
                try { rightPath[rightPath.Length - 1] = Int32.Parse(inputArray[i]); }
                catch (FormatException) { ConsoleHandler.force_append_Notice("Invalid long number detected in the right path..."); }
            }
            long[][] upgrades = new long[][] { leftPath, rightPath };

            return upgrades;
        }
        private string[] CreateStringArray(string[] outputArray, string[][] deserializedArray)
        {
            if(deserializedArray != null)
            {
                foreach (string[] a in deserializedArray)
                {
                    foreach (string b in a)
                    {
                        Array.Resize(ref outputArray, outputArray.Length + 1);
                        outputArray[outputArray.Length - 1] = b;
                    }
                }
                return outputArray;
            }
            else
            {
                return null;
            }
        }
        private void PopulateUpgrades()
        {
            //var item = Upgrades_ListBox.SelectedIndex;
            if (Upgrades_ListBox.Items.Count > 0)
            {
                if (upgradenames.Length - 1 >= Upgrades_ListBox.SelectedIndex)
                    UpgradeName_TextBox.Text = upgradenames[Upgrades_ListBox.SelectedIndex].ToString();

                if (upgradePrices.Length - 1 >= Upgrades_ListBox.SelectedIndex)
                    UpgradePrice_TextBox.Text = upgradePrices[Upgrades_ListBox.SelectedIndex].ToString();
                
                if (upgradeIcons.Length - 1 >= Upgrades_ListBox.SelectedIndex)
                    UpgradeIcon_TextBox.Text = upgradeIcons[Upgrades_ListBox.SelectedIndex].ToString();
                
                if (upgradeAvatars.Length - 1 >= Upgrades_ListBox.SelectedIndex)
                    UpgradeAvatar_TextBox.Text = upgradeAvatars[Upgrades_ListBox.SelectedIndex].ToString();

                if(upgradeRanks.Length-1 >= Upgrades_ListBox.SelectedIndex)
                    RankToUnlockUpgrade_TextBox.Text = upgradeRanks[Upgrades_ListBox.SelectedIndex].ToString();
                
                if (upgradeXPs.Length - 1 >= Upgrades_ListBox.SelectedIndex)
                    XpToUnlockUpgrade_TextBox.Text = upgradeXPs[Upgrades_ListBox.SelectedIndex].ToString();

                if (loc_upgradeDescs.Length - 1 >= Upgrades_ListBox.SelectedIndex)
                    UpgradeDesc_TextBox.Text = loc_upgradeDescs[Upgrades_ListBox.SelectedIndex].ToString();
            }
        }
        private void ResetUI()
        {
            TowerName_TextBox.Text = "";
            BaseCost_TextBox.Text = "";
            RankToUnlock_TextBox.Text = "";
            Icon_TextBox.Text = "";
            TowerDesc_TextBox.Text = "";
            CanTargetCamo_CheckBox.Checked = false;
            RotateToTarget_CheckBox.Checked = false;
            TargetsManually_CheckBox.Checked = false;
            TargetIsWeaponOrigin_CheckBox.Checked = false;
            CanBePlacedInWater_CheckBox.Checked = false;
            CanBePlacedOnLand_CheckBox.Checked = false;
            CanBePlacedOnPath_CheckBox.Checked = false;
            PlacementH_TextBox.Text = "";
            PlacementW_TextBox.Text = "";
            PlacementRadius_TextBox.Text = "";
            //TargetingMode_ComboBox.SelectedItem = artist.TargetingMode;
            TargetingMode_ComboBox.SelectedItem = TargetingMode_ComboBox.Items[0];

            //upgrade stuff
            Upgrades_ListBox.Items.Clear();
            SpriteUpgradeDef_TextBox.Text = "";
            UpgradeName_TextBox.Text = "";
            UpgradeDesc_TextBox.Text = "";
            UpgradePrice_TextBox.Text = "";
            XpToUnlockUpgrade_TextBox.Text = "";
            RankToUnlockUpgrade_TextBox.Text = "";
            UpgradeIcon_TextBox.Text = "";
            UpgradeAvatar_TextBox.Text = "";
        }     
        private void EasyTowerEditor_Shown(object sender, EventArgs e)
        {
            firstLoad = true;
            if (game == "BTD5")
            {
                loc_Path = Serializer.cfg.BTD5_Directory + "\\Assets\\Loc\\English.xml";
            }
            else if (game == "BMC")
            {
                loc_Path = CurrentProjectVariables.GamePath + "\\Assets\\Loc\\English.xml";
            }
            else
            {
                loc_Path = "";
            }
            if (!Guard.IsStringValid(Serializer.cfg.LastProject))
            {
                ConsoleHandler.force_append_Notice("You need to have a project opened to use this tool...");
                this.Close();
            }


            PopulateTowerList();
            CreateTowerObject(path);
        }

        private void PopulateTowerList()
        {
            string towersPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions";
            var towerFiles = Directory.GetFiles(towersPath);
            foreach (string file in towerFiles)
            {
                string[] split = file.Split('\\');
                string filename = split[split.Length - 1].Replace("\\", "");

                if (!AllTowerFiles_ComboBox.Items.Contains(filename))
                    AllTowerFiles_ComboBox.Items.Add(filename);

                if (file == path)
                    AllTowerFiles_ComboBox.SelectedItem = AllTowerFiles_ComboBox.Items[AllTowerFiles_ComboBox.Items.Count - 1];
            }
        }

        private void SaveLoc()
        {
            if (game == "BTD5" || game == "BMC")
            {
                if (!File.Exists(gameDir + "\\Assets\\Loc\\English.xml"))
                {
                    ConsoleHandler.append("Failed to find the game's English.xml file. Unable to save LOC data");
                    return;
                }
                if (!File.Exists(loc_Path))
                {
                    ConsoleHandler.append("Failed to find stored English.xml file. Unable to save LOC data");
                    return;
                }
                loc_Text = File.ReadAllLines(loc_Path);

                string towerName = "";
                string towerNamePlural = "";
                string towerNameCAPS = "";
                string towerDesc = "";

                if (game == "BMC")
                {
                    towerName = "LOC_MY_MONKEYS_" + TowerType_Label.Text + "_NAME";
                    towerNamePlural = "LOC_MY_MONKEYS_" + TowerType_Label.Text + "_NAME_PLURAL";
                    towerNameCAPS = "LOC_" + TowerType_Label.Text.ToUpper() + "_TOWER";
                    towerDesc = "LOC_MY_MONKEYS_" + TowerType_Label.Text + "_DESC";
                }
                else if (game == "BTD5")
                {
                    towerName = "LOC_" + TowerType_Label.Text + "_TOWER";
                    towerNamePlural = "LOC_" + TowerType_Label.Text + "_TOWER_PLURAL";
                    towerNameCAPS = "LOC_" + TowerType_Label.Text.ToUpper() + "_TOWER";
                    towerDesc = "LOC_TOWER_DESC_" + TowerType_Label.Text;
                }

                //Upgrade Names
                string towerUpgrade_A1 = "LOC_UPGRADE_A1_" + TowerType_Label.Text;
                string towerUpgrade_A2 = "LOC_UPGRADE_A2_" + TowerType_Label.Text;
                string towerUpgrade_A3 = "LOC_UPGRADE_A3_" + TowerType_Label.Text;
                string towerUpgrade_A4 = "LOC_UPGRADE_A4_" + TowerType_Label.Text;

                string towerUpgrade_B1 = "LOC_UPGRADE_B1_" + TowerType_Label.Text;
                string towerUpgrade_B2 = "LOC_UPGRADE_B2_" + TowerType_Label.Text;
                string towerUpgrade_B3 = "LOC_UPGRADE_B3_" + TowerType_Label.Text;
                string towerUpgrade_B4 = "LOC_UPGRADE_B4_" + TowerType_Label.Text;


                //Upgrade Descriptions
                string towerUpgradeDesc_A1 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_A1";
                string towerUpgradeDesc_A2 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_A2";
                string towerUpgradeDesc_A3 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_A3";
                string towerUpgradeDesc_A4 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_A4";

                string towerUpgradeDesc_B1 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_B1";
                string towerUpgradeDesc_B2 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_B2";
                string towerUpgradeDesc_B3 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_B3";
                string towerUpgradeDesc_B4 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_B4";


                //Get tower name
                int i = 0;
                foreach (string name in loc_Text)
                {
                    if ((name.Contains(towerName)) && (!name.Contains("PLURAL")) && (!name.Contains("CAPS")))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerName + "\" l=\"0\">" + loc_towerName + "</T>";
                    }
                    else if (name.Contains(towerDesc))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerDesc + "\" l=\"0\">" + loc_towerDesc + "</T>";
                    }
                    else if (name.Contains(towerUpgrade_A1))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgrade_A1 + "\" l=\"0\">" + loc_upgradeNames[0] + "</T>";
                    }
                    else if (name.Contains(towerUpgrade_A2))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgrade_A2 + "\" l=\"0\">" + loc_upgradeNames[1] + "</T>";
                    }
                    else if (name.Contains(towerUpgrade_A3))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgrade_A3 + "\" l=\"0\">" + loc_upgradeNames[2] + "</T>";
                    }
                    else if (name.Contains(towerUpgrade_A4))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgrade_A4 + "\" l=\"0\">" + loc_upgradeNames[3] + "</T>";
                    }
                    else if (name.Contains(towerUpgrade_B1))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgrade_B1 + "\" l=\"0\">" + loc_upgradeNames[4] + "</T>";
                    }
                    else if (name.Contains(towerUpgrade_B2))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgrade_B2 + "\" l=\"0\">" + loc_upgradeNames[5] + "</T>";
                    }
                    else if (name.Contains(towerUpgrade_B3))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgrade_B3 + "\" l=\"0\">" + loc_upgradeNames[6] + "</T>";
                    }
                    else if (name.Contains(towerUpgrade_B4))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgrade_B4 + "\" l=\"0\">" + loc_upgradeNames[7] + "</T>";
                    }


                    else if (name.Contains(towerUpgradeDesc_A1))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgradeDesc_A1 + "\" l=\"0\">" + loc_upgradeDescs[0] + "</T>";
                    }
                    else if (name.Contains(towerUpgradeDesc_A2))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgradeDesc_A2 + "\" l=\"0\">" + loc_upgradeDescs[1] + "</T>";
                    }
                    else if (name.Contains(towerUpgradeDesc_A3))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgradeDesc_A3 + "\" l=\"0\">" + loc_upgradeDescs[2] + "</T>";
                    }
                    else if (name.Contains(towerUpgradeDesc_A4))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgradeDesc_A4 + "\" l=\"0\">" + loc_upgradeDescs[3] + "</T>";
                    }


                    else if (name.Contains(towerUpgradeDesc_B1))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgradeDesc_B1 + "\" l=\"0\">" + loc_upgradeDescs[4] + "</T>";
                    }
                    else if (name.Contains(towerUpgradeDesc_B2))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgradeDesc_B2 + "\" l=\"0\">" + loc_upgradeDescs[5] + "</T>";
                    }
                    else if (name.Contains(towerUpgradeDesc_B3))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgradeDesc_B3 + "\" l=\"0\">" + loc_upgradeDescs[6] + "</T>";
                    }
                    else if (name.Contains(towerUpgradeDesc_B4))
                    {
                        loc_Text[i] = "\t<T id=\"" + towerUpgradeDesc_B4 + "\" l=\"0\">" + loc_upgradeDescs[7] + "</T>";
                    }
                    i++;
                }
                File.WriteAllLines(gameDir + "\\Assets\\Loc\\English.xml", loc_Text);               
            }
        }
        private void ReadLoc()
        {
            if(File.Exists(loc_Path))
            {
                loc_Text = File.ReadAllLines(loc_Path);

                string towerName = "";
                string towerNamePlural = "";
                string towerNameCAPS = "";
                string towerDesc = "";

                if (game == "BMC")
                {
                    towerName = "LOC_MY_MONKEYS_" + TowerType_Label.Text + "_NAME";
                    towerNamePlural = "LOC_MY_MONKEYS_" + TowerType_Label.Text + "_NAME_PLURAL";
                    towerNameCAPS = "LOC_" + TowerType_Label.Text.ToUpper() + "_TOWER";
                    towerDesc = "LOC_MY_MONKEYS_" + TowerType_Label.Text + "_DESC";
                }
                else if (game == "BTD5")
                {
                    towerName = "LOC_" + TowerType_Label.Text + "_TOWER";
                    towerNamePlural = "LOC_" + TowerType_Label.Text + "_TOWER_PLURAL";
                    towerNameCAPS = "LOC_" + TowerType_Label.Text.ToUpper() + "_TOWER";
                    towerDesc = "LOC_TOWER_DESC_" + TowerType_Label.Text;
                }
                

                //Upgrade Names
                string towerUpgrade_A1 = "LOC_UPGRADE_A1_" + TowerType_Label.Text;
                string towerUpgrade_A2 = "LOC_UPGRADE_A2_" + TowerType_Label.Text;
                string towerUpgrade_A3 = "LOC_UPGRADE_A3_" + TowerType_Label.Text;
                string towerUpgrade_A4 = "LOC_UPGRADE_A4_" + TowerType_Label.Text;

                string towerUpgrade_B1 = "LOC_UPGRADE_B1_" + TowerType_Label.Text;
                string towerUpgrade_B2 = "LOC_UPGRADE_B2_" + TowerType_Label.Text;
                string towerUpgrade_B3 = "LOC_UPGRADE_B3_" + TowerType_Label.Text;
                string towerUpgrade_B4 = "LOC_UPGRADE_B4_" + TowerType_Label.Text;


                //Upgrade Descriptions
                string towerUpgradeDesc_A1 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_A1";
                string towerUpgradeDesc_A2 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_A2";
                string towerUpgradeDesc_A3 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_A3";
                string towerUpgradeDesc_A4 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_A4";

                string towerUpgradeDesc_B1 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_B1";
                string towerUpgradeDesc_B2 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_B2";
                string towerUpgradeDesc_B3 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_B3";
                string towerUpgradeDesc_B4 = "LOC_" + TowerType_Label.Text + "_UPGRADE_DESC_B4";


                //Get tower name
                foreach (string name in loc_Text)
                {
                    if ((name.Contains(towerName)) && (!name.Contains("PLURAL")) && (!name.Contains("CAPS")))
                    {
                        string[] split = name.Split('>');
                        loc_towerName = split[split.Length - 2].Replace("</T", "");
                        TowerName_TextBox.Text = split[split.Length - 2].Replace("</T", "");
                    }
                    else if (name.Contains(towerDesc))
                    {
                        if (TowerDesc_TextBox.Text.Length == 0)
                        {
                            string[] split = name.Split('>');
                            loc_towerDesc = split[split.Length - 2].Replace("</T", "");
                            TowerDesc_TextBox.Text = loc_towerDesc;
                        }
                    }
                    else if (name.Contains(towerUpgrade_A1) || name.Contains(towerUpgrade_A2) || name.Contains(towerUpgrade_A3) || name.Contains(towerUpgrade_A4) || name.Contains(towerUpgrade_B1) || name.Contains(towerUpgrade_B2) || name.Contains(towerUpgrade_B3) || name.Contains(towerUpgrade_B4))
                    {
                        string[] split = name.Split('>');
                        string upgradeName = split[split.Length - 2].Replace("</T", "");

                        Array.Resize(ref loc_upgradeNames, loc_upgradeNames.Length + 1);
                        loc_upgradeNames[loc_upgradeNames.Length - 1] = upgradeName;
                    }
                    else if (name.Contains(towerUpgradeDesc_A1) || name.Contains(towerUpgradeDesc_A2) || name.Contains(towerUpgradeDesc_A3) || name.Contains(towerUpgradeDesc_A4) || name.Contains(towerUpgradeDesc_B1) || name.Contains(towerUpgradeDesc_B2) || name.Contains(towerUpgradeDesc_B3) || name.Contains(towerUpgradeDesc_B4))
                    {
                        
                        string[] split = name.Split('>');
                        string upgradeDesc = split[split.Length - 2].Replace("</T", "");

                        Array.Resize(ref loc_upgradeDescs, loc_upgradeDescs.Length + 1);
                        loc_upgradeDescs[loc_upgradeDescs.Length - 1] = upgradeDesc;
                    }
                }
            }
            else
            {
                    
            }
        }


        //
        // Handling UI changes
        //
        private void AllTowerFiles_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (firstLoad)
            {
                firstLoad = false;
            }
            else
            {
                ResetUI();

                path = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\" + AllTowerFiles_ComboBox.SelectedItem;
                
                CreateTowerObject(path);
                this.Refresh();
            }

        }
        private void Upgrades_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!UpgradeName_TextBox.Focused)
            {
                PopulateUpgrades();
            }
        }
        private void UpgradeName_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (finishedLoading)
            {
                if (UpgradeName_TextBox.Focused)
                {
                    if (Upgrades_ListBox.Items.Count > 0)
                    {
                        if (Upgrades_ListBox.SelectedIndex < 0)
                            return;

                        int item = Upgrades_ListBox.SelectedIndex;;
                        string text = UpgradeName_TextBox.Text;
                        Upgrades_ListBox.Items[item] = text;
                        upgradenames[item] = text;

                        if(CurrentProjectVariables.GameName != "BTDB")
                            loc_upgradeNames[item] = UpgradeName_TextBox.Text;

                        Upgrades_ListBox.SelectedIndex = item;
                        UpgradeName_TextBox.SelectionStart = UpgradeName_TextBox.Text.Length;
                    }
                }
            }

        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            SaveFile();
            if (File.Exists(loc_Path))
                SaveLoc();
            ConsoleHandler.append_CanRepeat("Saved " + AllTowerFiles_ComboBox.SelectedItem.ToString());
        }
        private void UpgradeIcon_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (UpgradeIcon_TextBox.Focused)
            {
                if (Upgrades_ListBox.Items.Count > 0)
                {
                    var item = Upgrades_ListBox.SelectedIndex;
                    upgradeIcons[item] = UpgradeIcon_TextBox.Text;
                }
            }
        }
        private void UpgradeAvatar_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (UpgradeAvatar_TextBox.Focused)
            {
                if (Upgrades_ListBox.Items.Count > 0)
                {
                    var item = Upgrades_ListBox.SelectedIndex;
                    upgradeAvatars[item] = UpgradeAvatar_TextBox.Text;
                }
            }
        }
        private void UpgradePrice_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (UpgradePrice_TextBox.Focused)
            {
                if (Upgrades_ListBox.Items.Count > 0)
                {
                    var item = Upgrades_ListBox.SelectedIndex;
                    upgradePrices[item] = UpgradePrice_TextBox.Text;
                }
            }
        }
        private void XpToUnlockUpgrade_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (XpToUnlockUpgrade_TextBox.Focused)
            {
                if (Upgrades_ListBox.Items.Count > 0)
                {
                    var item = Upgrades_ListBox.SelectedIndex;
                    upgradeXPs[item] = XpToUnlockUpgrade_TextBox.Text;
                }
            }
        }
        private void RankToUnlockUpgrade_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (RankToUnlockUpgrade_TextBox.Focused)
            {
                if (Upgrades_ListBox.Items.Count > 0)
                {
                    var item = Upgrades_ListBox.SelectedIndex;
                    upgradeRanks[item] = RankToUnlockUpgrade_TextBox.Text;
                }
            }
        }
        private void UpgradeDesc_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (UpgradeDesc_TextBox.Focused)
            {
                if (Upgrades_ListBox.Items.Count > 0)
                {
                    var item = Upgrades_ListBox.SelectedIndex;
                    loc_upgradeDescs[item] = UpgradeDesc_TextBox.Text;
                }
            }
        }
        private void TowerName_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (TowerName_TextBox.Focused)
            {
                loc_towerName = TowerName_TextBox.Text;
            }
            
        }
        private void TowerDesc_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (TowerDesc_TextBox.Focused)
            {
                loc_towerDesc = TowerDesc_TextBox.Text;
            }
        }
        private void SwitchPanel_Click(object sender, EventArgs e)
        {
            if(TowerPanel.Visible && !UpgradesPanel.Visible)
            {
                TowerPanel.Visible = false;
                UpgradesPanel.Visible = true;
                SwitchPanel.Text = "Tower";
            }
            else if (!TowerPanel.Visible && UpgradesPanel.Visible)
            {
                TowerPanel.Visible = true;
                UpgradesPanel.Visible = false;
                SwitchPanel.Text = "Upgrades";
            }
        }
        private void CanBePlacedOnPath_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(finishedLoading)
            {
                if (CanBePlacedOnPath_CheckBox.Checked)
                {
                    CanBePlacedOnLand_CheckBox.Checked = false;
                    CanBePlacedInWater_CheckBox.Checked = false;
                    ConsoleHandler.append_Notice("CanBePlacedOnPath  overrides CanBePlacedOnLand and CanBePlacedInWater");
                }
            }
            
        }
        private void CanBePlacedOnLand_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CanBePlacedOnLand_CheckBox.Checked)
            {
                CanBePlacedOnPath_CheckBox.Checked = false;
            }
        }
        private void CanBePlacedInWater_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CanBePlacedInWater_CheckBox.Checked)
            {
                CanBePlacedOnPath_CheckBox.Checked = false;
            }
        }
        private void UsePlacementRadius_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            if(finishedLoading == true)
            {
                if (UsePlacementRadius_Checkbox.Checked)
                {
                    label17.Hide();
                    label18.Hide();
                    label19.Show();
                    PlacementH_TextBox.Hide();
                    PlacementW_TextBox.Hide();
                    PlacementRadius_TextBox.Show();
                    PlacementH_TextBox.Text = "0";
                    PlacementW_TextBox.Text = "0";
                    ConsoleHandler.append_Notice("Using placement radius overrides PlcementH and PlacementW");
                }
                if (!UsePlacementRadius_Checkbox.Checked)
                {
                    label17.Show();
                    label18.Show();
                    label19.Hide();

                    PlacementRadius_TextBox.Text = "0";
                    PlacementRadius_TextBox.Hide();
                    PlacementH_TextBox.Show();
                    PlacementW_TextBox.Show();
                }
            }
        }

        private void OpenText_Button_Click(object sender, EventArgs e)
        {
            JsonEditorHandler.OpenFile(path);
            this.Focus();
        }

        private void EasyTowerEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                SaveFile();
                ConsoleHandler.append_CanRepeat("Saved " + AllTowerFiles_ComboBox.SelectedItem.ToString());
                GeneralMethods.CompileJet("launch");
                ConsoleHandler.append_CanRepeat("Launching " + game);
            }
        }

        private void EasyTowerEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            EZTower_Opened = false;
        }


        //
        //Handle open buttons
        //

        public string GetSpecialtyBuilding()
        {
            string specialtyBuilding = "";
            string projPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\";

            foreach (var x in Directory.GetFiles(projPath + "SpecialtyDefinitions"))
            {
                string json = File.ReadAllText(x);
                if (!JSON_Reader.IsValidJson(json))
                    continue;

                SpecialtyBuildingClass s = new SpecialtyBuildingClass();
                s = SpecialtyBuildingClass.FromJson(json);

                if (s != null && s.RelatedTower != null && s.RelatedTower == file)
                {
                    specialtyBuilding = x.Replace(projPath + "SpecialtyDefinitions\\", "");
                    towerTypeName = s.RelatedTower;
                    break;
                }
            }
            return specialtyBuilding;
        }
        private void PopulateOpenButton()
        {
            string projPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\";

            Weapons_Button.DropDownItems.Clear();
            filename = AllTowerFiles_ComboBox.SelectedItem.ToString();
            TowerFile_Button.Text = filename;
            file = filename.Replace(".tower", "");


            
            if (File.Exists(projPath + "UpgradeDefinitions\\" + file + ".upgrades"))
                UpgradeFile_Button.Text = file + ".upgrades";
            else
                UpgradeFile_Button.Visible = false;


            if(Directory.Exists(projPath + "SpecialtyDefinitions"))
            {
                specialty = GetSpecialtyBuilding();
                if (!specialty.Contains(".json"))
                    specialty = specialty + ".json";
                if (specialty != null && specialty != "")
                {
                    if (File.Exists(projPath + "SpecialtyDefinitions\\" + specialty))
                        specialtyBuildingToolStripMenuItem.Visible = true;
                    else
                        specialtyBuildingToolStripMenuItem.Visible = false;
                }
            }
            

            if (Directory.Exists(projPath + "WeaponDefinitions\\" + file))
            {
                string weaponDir = projPath + "WeaponDefinitions\\" + file;
                foreach (var x in Directory.GetFiles(weaponDir))
                {
                    string[] split = x.Split('\\');
                    Weapons_Button.DropDownItems.Add(split[split.Length - 1]);
                }
            }
            else
                Weapons_Button.Visible = false;


            //TowerSpriteUpgradeDef
            //Attempting to get the TowerSpriteUpgradeDef from tower file
            Tower_Class.Tower tower = new Tower_Class.Tower();
            string towerfile = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\" + file + ".tower";
            if (File.Exists(towerfile))
            {
                string json = File.ReadAllText(towerfile);
                if (JSON_Reader.IsValidJson(json))
                {
                    tower = Tower_Class.Tower.FromJson(json);
                    if (tower != null)
                    {
                        if (tower.SpriteUpgradeDefinition == null || tower.SpriteUpgradeDefinition == "")
                        {
                            TowerSpriteUpgradeDef_Button.Visible = false;
                        }
                        else
                            towerSpriteUpgradeDef = tower.SpriteUpgradeDefinition;
                    }
                }
                else
                {
                    if (File.Exists(projPath + "TowerSpriteUpgradeDefinitions\\" + file + ".json"))
                    {
                        ConsoleHandler.append_Force_CanRepeat("Tower file has invalid JSON, and therefore, unable to get current TowerSpriteDefinition file. Using default one instead...");
                    }
                    else
                    {
                        ConsoleHandler.append_Force_CanRepeat("Tower file has invalid JSON, and therefore, unable to get current TowerSpriteDefinition file. Additionally, the default one does not exist. Unable to open TowerSpriteUpgradeDef");
                        TowerSpriteUpgradeDef_Button.Visible = false;
                    }
                }
            }
            else
            {
                if (!File.Exists(projPath + "TowerSpriteUpgradeDefinitions\\" + file + ".json"))
                    TowerSpriteUpgradeDef_Button.Visible = false;
            }
        }
        private void TowerFile_Button_Click(object sender, EventArgs e)
        {
            string filepath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\" + file + ".tower";
            if (File.Exists(filepath))
                JsonEditorHandler.OpenFile(filepath);
            this.Focus();
        }
        private void UpgradeFile_Button_Click(object sender, EventArgs e)
        {
            string filepath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\UpgradeDefinitions\\" + file + ".upgrades";
            if (File.Exists(filepath))
                JsonEditorHandler.OpenFile(filepath);
            this.Focus();
        }
        private void Weapons_Button_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            string weaponfile = e.ClickedItem.Text.Replace(".tower", "").Replace(".upgrades", "").Replace(".weapon", "").Replace(".json", "");
            string foldername = "";
            if (towerName != "")
            {
                foldername = towerName.Replace(".tower", "").Replace(".upgrades", "").Replace(".weapon", "").Replace(".json", "");
            }
            else
                foldername = filename.Replace(".tower", "").Replace(".upgrades", "").Replace(".weapon", "").Replace(".json", "");
            string filepath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\WeaponDefinitions\\" + foldername + "\\" + weaponfile + ".weapon";

            if (File.Exists(filepath))
                JsonEditorHandler.OpenFile(filepath);
            this.Focus();
        }
        private void TowerSpriteUpgradeDef_Button_Click(object sender, EventArgs e)
        {
            if (towerSpriteUpgradeDef == "")
                towerSpriteUpgradeDef = file;
            string filepath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerSpriteUpgradeDefinitions\\" + towerSpriteUpgradeDef;
            if (!filepath.EndsWith(".json"))
                filepath = filepath + ".json";

            if (File.Exists(filepath))
                JsonEditorHandler.OpenFile(filepath);
            else
                ConsoleHandler.append("The TowerSpriteUpgradeDef  " + towerSpriteUpgradeDef + " was not found");
            this.Focus();
        }
        private void SpecialtyBuildingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\SpecialtyDefinitions\\" + specialty;
            if (File.Exists(filepath))
                JsonEditorHandler.OpenFile(filepath);
            this.Focus();
        }
        private void Open_Button_Click(object sender, EventArgs e)
        {
            //Open_Button.ForeColor = Color.Black;
        }

        private void NewTower_Button_Click(object sender, EventArgs e)
        {
            Open_Panel.Visible = !Open_Panel.Visible;
        }

        private void ChoseName_Button_Click(object sender, EventArgs e)
        {
            if(NewTowerName_TB.Text.Length != 4)
            {
                MessageBox.Show("To use new towers with NKHook, your new tower name MUST be 4 characters long");
            }
            else if(checkboxesChecked == 0)
            {
                MessageBox.Show("Please check which side of the Tower Buy Menu you want the tower to be on");
            }
            else if (checkboxesChecked > 1)
            {
                MessageBox.Show("Please check only one checkbox");
            }
            else
            {
                CreateNewTower();
            }
        }
        private void CreateNewTower()
        {
            NewTowerName_BGPanel.Hide();
            var pos = NewTower.TowerSelectMenu_Pos.Left;

            if (SelectionMenu_Right_CB.Checked)
                pos = NewTower.TowerSelectMenu_Pos.Right;

            if (SelectionMenu_FixedLeft_CB.Checked)
                pos = NewTower.TowerSelectMenu_Pos.FixedLeft;

            if (SelectionMenu_FixedRight_CB.Checked)
                pos = NewTower.TowerSelectMenu_Pos.FixedRight;

            string baseT = "";
            if (useBaseTower)
                baseT = path;

            
            NewTower newTower = new NewTower()
            {
                TowerName = NewTowerName_TB.Text,
                UseBaseTower = useBaseTower,
                BaseTowerFile = baseT,
                TowerSelPos = pos,
                CreateSpecialty = CreateSpecialty_CB.Checked
            };            

            newTower.DuplicateAllTowerFiles();

            if(!File.Exists(CurrentProjectVariables.PathToProjectFiles +"\\Assets\\JSON\\TowerDefinitions\\" + NewTowerName_TB.Text + ".tower"))
            {
                ConsoleHandler.append("Something went wrong with the tower creation. The new TowerDefinitions file " +
                    "wasn't found. You should double check to make sure its there");
                return;
            }
            

            PopulateTowerList();
            AllTowerFiles_ComboBox.SelectedItem = NewTowerName_TB.Text + ".tower";
        }
        private void NewEmptyTower_Button_Click(object sender, EventArgs e)
        {
            NewTowerName_BGPanel.Show();
            NewTowerName_BGPanel.BringToFront();
            useBaseTower = false;
            Open_Panel.Hide();
        }

        private void UseBaseTower_Buton_Click(object sender, EventArgs e)
        {
            NewTowerName_BGPanel.Show();
            NewTowerName_BGPanel.BringToFront();
            useBaseTower = true;
            Open_Panel.Hide();
        }

        private void CancelNewTower_Button_Click(object sender, EventArgs e)
        {
            NewTowerName_BGPanel.Hide();
            useBaseTower = false;
        }
    }
}
