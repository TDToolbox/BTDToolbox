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

            upgradenames = CreateStringArray(upgradenames, artist.Upgrades);
            upgradeIcons = CreateStringArray(upgradeIcons, artist.UpgradeIcons);            
            upgradeAvatars = CreateStringArray(upgradeAvatars, artist.UpgradeAvatars);

            if(artist.UpgradePrices != null)
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
            /*PopulateUI();
            this.Refresh();*/
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
    }
}
