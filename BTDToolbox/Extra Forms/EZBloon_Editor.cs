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
    public partial class EZBloon_Editor : Form
    {
        public string json { get; set; }
        public string path { get; set; }

        Bloon bloon;
        Bloon newBloon;
        
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
                path = bloonPath;
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
                ResetUI();
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
                
                //StatusImmunity checked listbox
                StatusImmunity_CheckedListBox = Handle_CheckedListbox(StatusImmunity_CheckedListBox, bloon.StatusImmunity);
                //DamageImmunity checked listbox
                DamageImmunity_CheckedListBox = Handle_CheckedListbox(DamageImmunity_CheckedListBox, bloon.DamageImmunity);
                //BloonAbility checked listbox
                BloonAbility_CheckedListBox = Handle_CheckedListbox(BloonAbility_CheckedListBox, bloon.BloonAbility);
                //ApplyStatus checked listbox
                ApplyStatus_CheckedListBox = Handle_CheckedListbox(ApplyStatus_CheckedListBox, bloon.ApplyStatus);

                //Can go underground checkbox
                HandleCheckBox(canGoUnderground, CanGoUnderground_CheckBox, bloon.CanGoUnderground);
                //Rotate To Path checkbox
                HandleCheckBox(canRotateToPath, RotateToPathDirection_Checkbox, bloon.RotateToPathDirection);
                //Draw layer textbox
                HandleTextBox(canDrawLayer, DrawLayer_TextBox, DrawLayer_Label, bloon.DrawLayer);
                //Hit addon textbox
                HandleTextBox(canHitAddon, HitAddon_TextBox, HitAddon_Label, bloon.HitAddon.ToString());
            }        
        }
        private CheckBox HandleCheckBox(bool allowCheckbox, CheckBox checkbox, bool bloonValue)
        {
            if (allowCheckbox)
            {
                checkbox.Visible = true;
                if (bloonValue == true)
                    checkbox.Checked = true;
                else
                    checkbox.Checked = false;
            }
            else
            {
                if (!advancedView)
                    checkbox.Visible = false;
            }
            return checkbox;
        }
        private object HandleTextBox(bool allowCheckbox, RichTextBox rtb, Label label, string bloonValue)
        {
            if (allowCheckbox)
            {
                rtb.Visible = true;
                label.Visible = true;
                rtb.Text = bloonValue;
            }
            else
            {
                if (!advancedView)
                {
                    rtb.Visible = false;
                    label.Visible = false;
                }
            }
            return rtb;
        }
        private void SaveFile()
        {
            newBloon = new Bloon();
            newBloon = bloon;
            newBloon.SpriteFile = SpriteFile_TextBox.Text;
            

            if (InitialHealth_TextBox.Text.Length > 0)
            {
                long initialHealth = 0;
                try
                {
                    initialHealth = Int64.Parse(InitialHealth_TextBox.Text);
                    if (initialHealth <= 0)
                    {
                        ConsoleHandler.force_appendNotice("Inital Health must be at least 1, automatically setting it to 1");
                        initialHealth = 1;
                    }
                    newBloon.InitialHealth = initialHealth;
                }
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Your initial health is not a valid number..."); }
            }

            try { newBloon.BaseSpeed = Int64.Parse(BaseSpeed_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your base speed is not a valid number..."); }

            try { newBloon.SpeedMultiplier = Int64.Parse(SpeedMultiplier_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your speed multiplier is not a valid number..."); }

            try { newBloon.Rbe = Int64.Parse(RBE_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your RBE is not a valid number..."); }

            if(Radius_TextBox.Text.Length > 0)
            {
                try { newBloon.Radius = Int64.Parse(Radius_TextBox.Text); }
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Your radius is not a valid number..."); }
            }

            if (SpeedMultiplier_TextBox.Text.Length > 0)
            {
                try { newBloon.Scale = Int64.Parse(SpeedMultiplier_TextBox.Text); }
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Your speed multiplier is not a valid number..."); }
            }

            if (ChildEffectScale_TextBox.Text.Length > 0)
            {
                try { newBloon.ChildEffectScale = Int64.Parse(ChildEffectScale_TextBox.Text); }
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Your child effect scale is not a valid number..."); }
            }

            if (HitAddon_TextBox.Visible == true)
            {
                if (HitAddon_TextBox.Text.Length > 0)
                {
                    try { newBloon.HitAddon = Int64.Parse(HitAddon_TextBox.Text); }
                    catch (FormatException e) { ConsoleHandler.force_appendNotice("Your hit addon is not a valid number..."); }
                }
            }            

            if (DrawLayer_TextBox.Visible == true)
            {
                if (DrawLayer_TextBox.Text.Length > 0)
                    newBloon.DrawLayer = DrawLayer_TextBox.Text;
            }
            
            if(CanGoUnderground_CheckBox.Visible == true)
                newBloon.CanGoUnderground = CanGoUnderground_CheckBox.Checked;

            if (RotateToPathDirection_Checkbox.Visible == true)
                newBloon.RotateToPathDirection = RotateToPathDirection_Checkbox.Checked;

            newBloon.StatusImmunity = CreateStringArray_FromCheckedLB(StatusImmunity_CheckedListBox);
            newBloon.DamageImmunity = CreateStringArray_FromCheckedLB(DamageImmunity_CheckedListBox);
            newBloon.BloonAbility = CreateStringArray_FromCheckedLB(BloonAbility_CheckedListBox);
            newBloon.ApplyStatus = CreateStringArray_FromCheckedLB(ApplyStatus_CheckedListBox);


            var saveFile = SerializeBloon.ToJson(newBloon);
            File.WriteAllText(path, saveFile);
        }
        private string[] CreateStringArray_FromCheckedLB(CheckedListBox listbox)
        {
            string[] output = new string[0];
            foreach(var item in listbox.CheckedItems)
            {
                Array.Resize(ref output, output.Length + 1);
                output[output.Length - 1] = item.ToString();
            }
            return output;
        }
        private void PopulateChildBloons()
        {
            if (bloon != null)
            {
                CheckedListBox newList = new CheckedListBox();
                string bloonDir = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions";
                var bloonFiles = Directory.GetFiles(bloonDir);

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

                label2.Text = "Bloon type:";
                BloonName_TextBox.ReadOnly = false;
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

                    label2.Text = "Bloon type (cant edit):";
                    BloonName_TextBox.ReadOnly = true;
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
    }
}
