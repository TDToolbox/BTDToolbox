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
using Tower_Class;

namespace BTDToolbox.Extra_Forms
{
    public partial class EZBloon_Editor : Form
    {
        public string json { get; set; }
        public string path { get; set; }

        Bloon bloon;
        Tower_Class.Artist artist;
        bool finishedLoading = false;
        bool firstLoad = false;
        bool advancedView = false;
        bool forceAdvancedView = false;
        bool canGoUnderground = false;
        bool canRotateToPath = false;
        bool canDrawLayer = false;
        bool canHitAddon = false;

        string[] upgradenames = new string[] { };
        string[] upgradeIcons = new string[] { };
        string[] upgradeAvatars = new string[] { };
        string[] upgradePrices = new string[] { };
        string[] upgradeRanks = new string[] { };
        string[] upgradeXPs = new string[] { };
        
        string[] loc_upgradeNames = new string[] { };
        string[] loc_upgradeDescs = new string[] { };

        string game = Serializer.Deserialize_Config().CurrentGame;

        string[] loc_Text = new string[] { };
        string loc_Path = "";
        string loc_towerName = "";
        string loc_towerDesc = "";
        public EZBloon_Editor()
        {
            InitializeComponent();
        }
        public void CreateBloonObject(string bloonPath)
        {
            if (bloonPath.EndsWith(".bloon"))
            {
                string json = File.ReadAllText(bloonPath);
                if (JSON_Reader.IsValidJson(json))
                {
                    bloon = Bloon.FromJson(json);

                    canGoUnderground = false;
                    canRotateToPath = false;
                    canDrawLayer = false;
                    canHitAddon = false;

                    if (json.Contains("DrawLayer") || json.Contains("CanGoUnderground") || json.Contains("RotateToPathDirection") || json.Contains("HitAddon"))
                        forceAdvancedView = true;
                    else
                        forceAdvancedView = false;

                    if (json.Contains("CanGoUnderground"))
                        canGoUnderground = true;
                    if (json.Contains("RotateToPathDirection"))
                        canRotateToPath = true;
                    if (json.Contains("DrawLayer"))
                        canDrawLayer = true;
                    if (json.Contains("HitAddon"))
                        canHitAddon = true;

                    PopulateUI();
                }
                else
                {
                    ConsoleHandler.force_appendLog_CanRepeat("The file you are trying to load has invalid JSON, and as a result, can't be loaded...");
                }
            }
            else
            {
                ConsoleHandler.appendLog_CanRepeat("Error! Thats not a valid bloon file");
            }
        }
        private void PopulateUI()
        {
            if (bloon != null)
            {
                //ResetUI();
                BloonType_Label.Text = bloon.Type + " bloon";
                BloonName_TextBox.Text = bloon.Type;
                InitialHealth_TextBox.Text = bloon.InitialHealth.ToString();
                BaseSpeed_TextBox.Text = bloon.BaseSpeed.ToString();
                SpeedMultiplier_TextBox.Text = bloon.SpeedMultiplier.ToString();

                if (bloon.SpriteFile != null)
                    SpriteFile_TextBox.Text = bloon.SpriteFile.ToString();
                else
                    SpriteFile_TextBox.Text = "";

                RBE_TextBox.Text = bloon.Rbe.ToString();
                Radius_TextBox.Text = bloon.Radius.ToString();
                Scale_TextBox.Text = bloon.Scale.ToString();
                ChildEffectScale_TextBox.Text = bloon.ChildEffectScale.ToString();

                PopulateChildBloons();
                StatusImmunity_CheckedListBox = Handle_CheckedListbox(StatusImmunity_CheckedListBox, bloon.StatusImmunity);
                DamageImmunity_CheckedListBox = Handle_CheckedListbox(DamageImmunity_CheckedListBox, bloon.DamageImmunity);
                BloonAbility_CheckedListBox = Handle_CheckedListbox(BloonAbility_CheckedListBox, bloon.BloonAbility);



                //Can go underground checkbox
                if (canGoUnderground)
                {
                    CanGoUnderground_CheckBox.Visible = true;
                    if (bloon.CanGoUnderground == true)
                        CanGoUnderground_CheckBox.Checked = true;
                    else
                        CanGoUnderground_CheckBox.Checked = false;
                }
                else
                {
                    if (!advancedView)
                        CanGoUnderground_CheckBox.Visible = false;
                }

                //Rotate To Path checkbox
                if (canRotateToPath)
                {
                    RotateToPathDirection_Checkbox.Visible = true;
                    if (bloon.RotateToPathDirection == true)
                        RotateToPathDirection_Checkbox.Checked = true;
                    else
                        RotateToPathDirection_Checkbox.Checked = false;
                }
                else
                {
                    if (!advancedView)
                        RotateToPathDirection_Checkbox.Visible = false;
                }

                //Draw layer textbox
                if (canDrawLayer)
                {
                    DrawLayer_TextBox.Visible = true;
                    DrawLayer_Label.Visible = true;
                    DrawLayer_TextBox.Text = bloon.DrawLayer;
                }
                else
                {
                    if (!advancedView)
                    {
                        DrawLayer_TextBox.Visible = false;
                        DrawLayer_Label.Visible = false;
                    }                        
                }

                //Hit addon textbox
                if (canHitAddon)
                {
                    HitAddon_TextBox.Visible = true;
                    HitAddon_Label.Visible = true;
                    HitAddon_TextBox.Text = bloon.HitAddon.ToString();
                }
                else
                {
                    if (!advancedView)
                    {
                        HitAddon_TextBox.Visible = false;
                        HitAddon_Label.Visible = false;
                    }
                }
            }        
        }
        private void SaveFile()
        {
            try { artist.BaseCost = Int32.Parse(InitialHealth_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your base cost is not a valid number..."); }

            try { artist.RankToUnlock = Int32.Parse(BaseSpeed_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your Rank To Unlock is not a valid number..."); }

            /*artist.Icon = Icon_TextBox.Text;
            artist.TargetingMode = TargetingMode_ComboBox.SelectedItem.ToString();
            artist.CanTargetCamo = CanTargetCamo_CheckBox.Checked;
            artist.RotatesToTarget = RotateToTarget_CheckBox.Checked;
            artist.TargetsManually = TargetsManually_CheckBox.Checked;
            artist.TargetIsWeaponOrigin = TargetIsWeaponOrigin_CheckBox.Checked;
            artist.CanBePlacedInWater = CanBePlacedInWater_CheckBox.Checked;
            artist.CanBePlacedOnLand = CanBePlacedOnLand_CheckBox.Checked;
            artist.CanBePlacedOnPath = CanBePlacedOnPath_CheckBox.Checked;
            artist.UseRadiusPlacement = UsePlacementRadius_Checkbox.Checked;
*/
            

           

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
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Invalid long number detected in the right path..."); }

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
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Invalid long number detected in the left path..."); }
                
            }
            for (int i = 0; i < upgradeCount; i++)
            {
                Array.Resize(ref rightPath, rightPath.Length + 1);
                try { rightPath[rightPath.Length - 1] = Int32.Parse(inputArray[i]); }
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Invalid long number detected in the right path..."); }
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
        private void PopulateChildBloons()
        {
            if (bloon != null)
            {
                CheckedListBox newList = new CheckedListBox();
                path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions";
                var bloonFiles = Directory.GetFiles(path);

                foreach (string file in bloonFiles)
                {
                    string[] split = file.Split('\\');
                    string filename = split[split.Length - 1].Replace("\\", "");

                    if (filename.EndsWith(".bloon"))
                    {
                        if (!ChildBloons_CheckedListBox.Items.Contains(filename.Replace(".bloon","")))
                            ChildBloons_CheckedListBox.Items.Add(filename.Replace(".bloon", ""));
                    }
                }

                int i = 0;
                if (bloon.ChildBloons.Length > 0)
                {
                    foreach (var cbloon in bloon.ChildBloons)
                    {
                        foreach (object item in ChildBloons_CheckedListBox.Items)
                        {
                            if (cbloon.ToString().ToLower() == (item.ToString() + ".bloon").ToLower())
                            {
                                newList.Items.Add(item, true);
                            }
                            else
                            {
                                newList.Items.Add(item, CheckState.Unchecked);
                            }
                            i++;
                        }
                    }

                    i = 0;
                    ChildBloons_CheckedListBox.Items.Clear();
                    foreach(var item in newList.Items)
                    {
                        if(!ChildBloons_CheckedListBox.Items.Contains(item))
                            ChildBloons_CheckedListBox.Items.Add(item, newList.GetItemChecked(i));
                        i++;
                    }
                }
            }
        }
        private CheckedListBox Handle_CheckedListbox(CheckedListBox originalList, string[] values)
        {
            if (values != null)
            {
                CheckedListBox newList = new CheckedListBox();
                foreach (var item in originalList.Items)
                {
                    bool found = false;
                    foreach (var x in values)
                    {
                        if (x.ToLower() == item.ToString().ToLower())
                        {
                            newList.Items.Add(item, true);
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        newList.Items.Add(item, false);
                    }
                }
                int i = 0;
                originalList.Items.Clear();
                foreach (var x in newList.Items)
                {
                    originalList.Items.Add(x, newList.GetItemChecked(i));
                    i++;
                }
            }
            return originalList;
        }
        private CheckedListBox ResetListBox(CheckedListBox originalList)
        {
            if (originalList != null)
            {
                int i = 0;
                CheckedListBox list = new CheckedListBox();
                foreach (var item in originalList.Items)
                {
                    list.Items.Add(item, false);
                }
                originalList.Items.Clear();
                foreach (var item in list.Items)
                {
                    originalList.Items.Add(item, false);
                }
                return originalList;
            }
            return null;
        }
        private void ResetUI()
        {
            BloonType_Label.Text = "";
            BloonName_TextBox.Text = "";
            InitialHealth_TextBox.Text = "";
            BaseSpeed_TextBox.Text = "";
            SpeedMultiplier_TextBox.Text = "";
            SpriteFile_TextBox.Text = "";
            RBE_TextBox.Text = "";
            Radius_TextBox.Text = "";
            Scale_TextBox.Text = "";
            ChildEffectScale_TextBox.Text = "";

            /*ResetListBox(ChildBloons_CheckedListBox);
            ResetListBox(StatusImmunity_CheckedListBox);
            ResetListBox(DamageImmunity_CheckedListBox);
            
            if (bloon.BloonAbility != null)
            {
                if (bloon.BloonAbility != null)
                    ResetListBox(BloonAbility_CheckedListBox);
            }*/
            /*int i = 0;
            CheckedListBox list = new CheckedListBox();
            foreach (var item in ChildBloons_CheckedListBox.Items)
            {
                list.Items.Add(item, false);
            }
            ChildBloons_CheckedListBox.Items.Clear();
            foreach (var item in list.Items)
            {
                ChildBloons_CheckedListBox.Items.Add(item, false);
            }

            i = 0;
            list = new CheckedListBox();
            foreach (var item in StatusImmunity_CheckedListBox.Items)
            {
                list.Items.Add(item, false);
            }
            StatusImmunity_CheckedListBox.Items.Clear();
            foreach (var item in list.Items)
            {
                StatusImmunity_CheckedListBox.Items.Add(item, false);
            }


            i = 0;
            list = new CheckedListBox();
            foreach (var item in DamageImmunity_CheckedListBox.Items)
            {
                list.Items.Add(item, false);
            }
            DamageImmunity_CheckedListBox.Items.Clear();
            foreach (var item in list.Items)
            {
                DamageImmunity_CheckedListBox.Items.Add(item, false);
            }*/


            //reset advanced stuff
            CanGoUnderground_CheckBox.Checked = false;
            RotateToPathDirection_Checkbox.Checked = false;
            DrawLayer_TextBox.Text = "";
            HitAddon_TextBox.Text = "";
        }
        private void EasyTowerEditor_Shown(object sender, EventArgs e)
        {
            finishedLoading = true;
            firstLoad = true;
            string gameDir = "";
            if (game == "BTD5")
            {
                gameDir = Serializer.Deserialize_Config().BTD5_Directory;
                loc_Path = gameDir + "\\Assets\\Loc\\English.xml";
            }
            else
            {
                gameDir = Serializer.Deserialize_Config().BTDB_Directory;
            }

            if (gameDir != null && gameDir != "")
            {
                string bloonPath = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions";
                var bloonFiles = Directory.GetFiles(bloonPath);
                foreach (string file in bloonFiles)
                {
                    string[] split = file.Split('\\');
                    string filename = split[split.Length - 1].Replace("\\", "");
                    
                    if(filename.EndsWith(".bloon"))
                    {
                        if (!BloonFiles_ComboBox.Items.Contains(filename))
                            BloonFiles_ComboBox.Items.Add(filename);

                        if (file == path)
                        {
                            BloonFiles_ComboBox.SelectedItem = BloonFiles_ComboBox.Items[BloonFiles_ComboBox.Items.Count - 1];
                        }
                    }
                }
                CreateBloonObject(path);
            }
            else
            {
                ConsoleHandler.force_appendNotice("You're game directory has not been set! You need to set your game Dir before continuing. You can do this by clicking the \"Help\" tab at the top, then clicking on \"Browse for game\"");
                this.Close();
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

                path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions\\" + BloonFiles_ComboBox.SelectedItem;
                CreateBloonObject(path);
                this.Refresh();
            }

        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            SaveFile();
            ConsoleHandler.appendLog_CanRepeat("Saved " + BloonFiles_ComboBox.SelectedItem.ToString());
        }
        private void TowerName_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (BloonName_TextBox.Focused)
            {
                loc_towerName = BloonName_TextBox.Text;
            }
            
        }
        private void SwitchPanel_Click(object sender, EventArgs e)
        {
            if (Panel1.Visible && !Panel2.Visible)
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                SwitchPanel.Text = "Page 1";
            }
            else if (!Panel1.Visible && Panel2.Visible)
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                SwitchPanel.Text = "Page 2";
            }
        }

        private void AdvancedView_Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            advancedView = !advancedView;

            if(advancedView)
            {
                CanGoUnderground_CheckBox.Visible = true;
                RotateToPathDirection_Checkbox.Visible = true;
                DrawLayer_TextBox.Visible = true;
                DrawLayer_Label.Visible = true;
                HitAddon_Label.Visible = true;
                HitAddon_TextBox.Visible = true;
            }
            else
            {
                if (!forceAdvancedView)
                {
                    CanGoUnderground_CheckBox.Visible = false;
                    RotateToPathDirection_Checkbox.Visible = false;
                    DrawLayer_TextBox.Visible = false;
                    DrawLayer_Label.Visible = false;
                    HitAddon_Label.Visible = false;
                    HitAddon_TextBox.Visible = false;
                }
            }
        }

        private void ChildBloons_CheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChildBloons_CheckedListBox.SelectedItem.ToString() == bloon.Type)
            {
                //ChildBloons_CheckedListBox.SetItemCheckState(ChildBloons_CheckedListBox.SelectedIndex, CheckState.Unchecked);
                ChildBloons_CheckedListBox.SetItemCheckState(ChildBloons_CheckedListBox.SelectedIndex, CheckState.Indeterminate);
            }
        }

        private void ChildBloons_CheckedListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ChildBloons_CheckedListBox.SelectedItem.ToString() == bloon.Type)
            {
                //ChildBloons_CheckedListBox.SetItemCheckState(ChildBloons_CheckedListBox.SelectedIndex, CheckState.Unchecked);
                ChildBloons_CheckedListBox.SetItemCheckState(ChildBloons_CheckedListBox.SelectedIndex, CheckState.Indeterminate);
            }
        }

        private void ChildBloons_CheckedListBox_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
