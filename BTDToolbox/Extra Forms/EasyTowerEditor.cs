using BTDToolbox.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace BTDToolbox.Extra_Forms
{
    public partial class EasyTowerEditor : Form
    {
        public string json { get; set; }
        public string path { get; set; }

        Tower_Class.Artist artist;
        bool finishedLoading = false;
        string[] upgradenames = new string[] { };
        string[] upgradeIcons = new string[] { };
        string[] upgradeAvatars = new string[] { };
        string[] upgradePrices = new string[] { };
        string[] upgradeRanks = new string[] { };
        string[] upgradeXPs = new string[] { };
        
        string[] loc_upgradeNames = new string[] { };
        string[] loc_upgradeDescs = new string[] { };


        

        string loc_Path = "";
        string loc_towerName = "";
        string loc_towerDesc = "";
        public EasyTowerEditor()
        {
            InitializeComponent();
        }
        public void CreateTowerObject(string towerPath)
        {
            //var artist = QuickType.Artist.FromJson(tower);
            string json = File.ReadAllText(towerPath);
            artist = Tower_Class.Artist.FromJson(json);


            PopulateUI();
            /*artist.BaseCost = 100000;
            var x = Tower_Class.Serialize.ToJson(artist);
            File.WriteAllText(path, x);*/
        }
        private void PopulateUI()
        {
            ResetUI();
            TowerType_Label.Text = artist.TypeName;

            string towersPath = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\TowerDefinitions";
            var towerFiles = Directory.GetFiles(towersPath);

            foreach (string file in towerFiles)
            {
                string[] split = file.Split('\\');
                string filename = split[split.Length - 1].Replace("\\", "");
                AllTowerFiles_ComboBox.Items.Add(filename);
            }

            TowerName_TextBox.Text = artist.Name;
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

            upgradenames = CreateStringArray(upgradenames, artist.Upgrades);
            upgradeIcons = CreateStringArray(upgradeIcons, artist.UpgradeIcons);            
            upgradeAvatars = CreateStringArray(upgradeAvatars, artist.UpgradeAvatars);

            if (artist.UpgradePrices != null)
            {
                foreach (long[] a in artist.UpgradePrices)
                {
                    foreach (long b in a)
                    {
                        if (!upgradePrices.Contains(b.ToString()))
                        {
                            Array.Resize(ref upgradePrices, upgradePrices.Length + 1);
                            upgradePrices[upgradePrices.Length - 1] = b.ToString();
                        }
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
            
            ReadLoc();
            
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
        }
        private string[] CreateStringArray(string[] outputArray, string[][] deserializedArray)
        {
            if(deserializedArray != null)
            {
                foreach (string[] a in deserializedArray)
                {
                    foreach (string b in a)
                    {
                        if (!outputArray.Contains(b))
                        {
                            Array.Resize(ref outputArray, outputArray.Length + 1);
                            outputArray[outputArray.Length - 1] = b;
                        }
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
                UpgradeName_TextBox.Text = upgradenames[Upgrades_ListBox.SelectedIndex].ToString();
                UpgradePrice_TextBox.Text = upgradePrices[Upgrades_ListBox.SelectedIndex].ToString();
                UpgradeIcon_TextBox.Text = upgradeIcons[Upgrades_ListBox.SelectedIndex].ToString();
                UpgradeAvatar_TextBox.Text = upgradeAvatars[Upgrades_ListBox.SelectedIndex].ToString();

                RankToUnlockUpgrade_TextBox.Text = upgradeRanks[Upgrades_ListBox.SelectedIndex].ToString();
                XpToUnlockUpgrade_TextBox.Text = upgradeXPs[Upgrades_ListBox.SelectedIndex].ToString();

                //XpToUnlockUpgrade_TextBox.Text = upgradeXPs[Upgrades_ListBox.SelectedIndex].ToString();
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
            TargetingMode_ComboBox.SelectedItem = artist.TargetingMode;

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
        private void EasyTowerEditor_Load(object sender, EventArgs e)
        {
            string gameDir = "";
            if (Serializer.Deserialize_Config().CurrentGame == "BTD5")
            {
                gameDir = Serializer.Deserialize_Config().BTD5_Directory;
            }
            else
            {
                gameDir = Serializer.Deserialize_Config().BTDB_Directory;
            }
            loc_Path = gameDir + "\\Assets\\Loc\\English.xml";


            CreateTowerObject(path);
            try
            {
                string[] split = path.Split('\\');
                string filename = split[split.Length - 1].Replace("\\", "");
                AllTowerFiles_ComboBox.SelectedItem = filename;
            }
            catch
            {
                throw;
                //Environment.Exit(0);
            }
        }
        private void AllTowerFiles_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ResetUI();

            string towersPath = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\TowerDefinitions";
            CreateTowerObject(towersPath + "\\" + AllTowerFiles_ComboBox.SelectedItem);
            PopulateUI();
            this.Refresh();
        }
        private void AllTowerFiles_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateUI();
            this.Refresh();
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
                if(UpgradeName_TextBox.Focused)
                {
                    if (Upgrades_ListBox.Items.Count > 0)
                    {
                        var item = Upgrades_ListBox.SelectedIndex;
                        Upgrades_ListBox.Items.RemoveAt(item);
                        Upgrades_ListBox.Items.Insert(item, UpgradeName_TextBox.Text);
                        upgradenames[item] = UpgradeName_TextBox.Text;

                        Upgrades_ListBox.SelectedIndex = item;
                        UpgradeName_TextBox.SelectionStart = UpgradeName_TextBox.Text.Length;
                    }
                }
            }

        }
        private void EasyTowerEditor_Shown(object sender, EventArgs e)
        {
            finishedLoading = true;
        }
        private void Upgrades_ListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }
        private void ReadLoc()
        {
            string[] loc = File.ReadAllLines(loc_Path);
            string towerName = "LOC_" + TowerType_Label.Text + "_TOWER";
            string towerNamePlural = "LOC_" + TowerType_Label.Text + "_TOWER_PLURAL";
            string towerNameCAPS = "LOC_" + TowerType_Label.Text.ToUpper() + "_TOWER";
            string towerDesc = "LOC_TOWER_DESC_" + TowerType_Label.Text;

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
            foreach (string name in loc)
            {
                if ((name.Contains(towerName)) && (!name.Contains("PLURAL")) && (!name.Contains("CAPS")))
                {
                    string[] split = name.Split('>');
                    loc_towerName = split[split.Length - 2].Replace("</T", "");
                    TowerName_TextBox.Text = split[split.Length - 2].Replace("</T", "");
                }
                else if (name.Contains(towerDesc))
                {
                    string[] split = name.Split('>');
                    loc_towerDesc = split[split.Length - 2].Replace("</T", "");
                    TowerDesc_TextBox.Text = loc_towerDesc;
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
    }
}
