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
    public partial class EZBloon_Editor : Form
    {
        public string json { get; set; }
        public string path { get; set; }

        Bloon bloon;
        Bloon newBloon;

        bool advancedView = false;
        bool forceAdvancedView = false;
        public static bool EZBloon_Opened = false;

        string game = Serializer.Deserialize_Config().CurrentGame;

        public EZBloon_Editor()
        {
            InitializeComponent();
            EZBloon_Opened = true;
        }
        public void CreateBloonObject(string bloonPath)
        {
            if (bloonPath.EndsWith(".bloon"))
            {
                path = bloonPath;
                json = File.ReadAllText(bloonPath);
                if (JSON_Reader.IsValidJson(json))
                {
                    bloon = Bloon.FromJson(json);
                    PopulateUI();
                }
                else
                    ConsoleHandler.force_appendLog_CanRepeat("The file you are trying to load has invalid JSON, and as a result, can't be loaded...");
            }
            else
                ConsoleHandler.appendLog_CanRepeat("Error! Thats not a valid bloon file");
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
                //ApplyStatus checked listbox
                ApplyStatus_CheckedListBox = Handle_CheckedListbox(ApplyStatus_CheckedListBox, bloon.ApplyStatus);
                //BloonAbility checked listbox
                if (game == "BTD5")
                    BloonAbility_CheckedListBox = Handle_CheckedListbox(BloonAbility_CheckedListBox, bloon.BloonAbility);
                else
                {
                    BloonAbility_CheckedListBox.Visible = false;
                    label9.Visible = false;
                }
                HandleAdvancedView();

                if (BloonFiles_ComboBox.SelectedItem != null)
                {
                    if (BloonType_Label.Text.Replace(" bloon","") + ".bloon" != BloonFiles_ComboBox.SelectedItem.ToString())
                    {
                        BloonFiles_ComboBox.SelectedItem = BloonType_Label.Text.Replace(" bloon", "") + ".bloon";
                    }
                }
            }
        }
        private void HandleAdvancedView()
        {
            if (forceAdvancedView == true || AdvancedView_Checkbox.Checked == true)
            {
                CanGoUnderground_CheckBox.Visible = true;
                CanGoUnderground_CheckBox.Checked = bloon.CanGoUnderground;

                RotateToPathDirection_Checkbox.Visible = true;
                RotateToPathDirection_Checkbox.Checked = bloon.RotateToPathDirection;

                DrawLayer_Label.Visible = true;
                DrawLayer_LB.Visible = true;
                DrawLayer_LB.SelectedItem = bloon.DrawLayer;

                HitAddon_Label.Visible = true;
                HitAddon_TextBox.Visible = true;
                HitAddon_TextBox.Text = bloon.HitAddon.ToString();
            }
            else
            {
                if(json.Contains("RotateToPathDirection"))
                {
                    if (bloon.RotateToPathDirection == true)
                    {
                        RotateToPathDirection_Checkbox.Visible = true;
                        RotateToPathDirection_Checkbox.Checked = true;
                    }
                    else
                    {
                        RotateToPathDirection_Checkbox.Visible = false;
                        RotateToPathDirection_Checkbox.Checked = false;
                    }
                }
                else
                {
                    bloon.RotateToPathDirection = false;
                    RotateToPathDirection_Checkbox.Visible = false;
                    RotateToPathDirection_Checkbox.Checked = false;
                }

                if (json.Contains("CanGoUnderground"))
                {
                    if (bloon.CanGoUnderground == true)
                    {
                        CanGoUnderground_CheckBox.Visible = false;
                        CanGoUnderground_CheckBox.Checked = true;
                    }
                    else
                    {
                        CanGoUnderground_CheckBox.Visible = true;
                        CanGoUnderground_CheckBox.Checked = false;
                    }
                }
                else
                {
                    bloon.CanGoUnderground = true;
                    CanGoUnderground_CheckBox.Visible = false;
                    CanGoUnderground_CheckBox.Checked = true;
                }

                if (json.Contains("HitAddon"))
                {
                    if (bloon.HitAddon > 0)
                    {
                        HitAddon_Label.Visible = true;
                        HitAddon_TextBox.Visible = true;
                        HitAddon_TextBox.Text = bloon.HitAddon.ToString();
                    }
                    else
                    {
                        HitAddon_Label.Visible = false;
                        HitAddon_TextBox.Visible = false;
                        HitAddon_TextBox.Text = "";
                    }
                }
                else
                {
                    bloon.HitAddon = 0;
                    HitAddon_Label.Visible = false;
                    HitAddon_TextBox.Visible = false;
                    HitAddon_TextBox.Text = "";
                }


                if (json.Contains("DrawLayer"))
                {
                    if (bloon.DrawLayer != null)
                    {
                        if(bloon.DrawLayer.Length > 0)
                        {
                            DrawLayer_Label.Visible = true;
                            DrawLayer_LB.Visible = true;
                            DrawLayer_LB.SelectedItem = bloon.DrawLayer;
                        }
                        else
                        {
                            DrawLayer_Label.Visible = false;
                            DrawLayer_LB.Visible = false;
                            DrawLayer_LB.SelectedItem = "";
                        }
                    }
                    else
                    {
                        DrawLayer_Label.Visible = false;
                        DrawLayer_LB.Visible = false;
                        DrawLayer_LB.SelectedItem = "";
                    }
                }
                else
                {
                    bloon.DrawLayer = "";
                    DrawLayer_Label.Visible = false;
                    DrawLayer_LB.Visible = false;
                    DrawLayer_LB.SelectedItem = "";
                }
            }
        }
        private void SaveFile()
        {
            bool error = false;
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
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Your initial health is not a valid number..."); error = true; }
            }

            try { newBloon.BaseSpeed = Double.Parse(BaseSpeed_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your base speed is not a valid number..."); error = true; }

            try { newBloon.SpeedMultiplier = Double.Parse(SpeedMultiplier_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your speed multiplier is not a valid number..."); error = true; }

            try { newBloon.Rbe = Int64.Parse(RBE_TextBox.Text); }
            catch (FormatException e) { ConsoleHandler.force_appendNotice("Your RBE is not a valid number..."); error = true; }

            if(Radius_TextBox.Text.Length > 0)
            {
                try { newBloon.Radius = Double.Parse(Radius_TextBox.Text); }
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Your radius is not a valid number..."); error = true; }
            }

            if (Scale_TextBox.Text.Length > 0)
            {
                try { newBloon.Scale = Double.Parse(Scale_TextBox.Text); }
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Your scale is not a valid number..."); error = true; }
            }

            if (ChildEffectScale_TextBox.Text.Length > 0)
            {
                try { newBloon.ChildEffectScale = Double.Parse(ChildEffectScale_TextBox.Text); }
                catch (FormatException e) { ConsoleHandler.force_appendNotice("Your child effect scale is not a valid number..."); error = true; }
            }

            if (HitAddon_TextBox.Visible == true)
            {
                if (HitAddon_TextBox.Text.Length > 0)
                {
                    try { newBloon.HitAddon = Double.Parse(HitAddon_TextBox.Text); }
                    catch (FormatException e) { ConsoleHandler.force_appendNotice("Your hit addon is not a valid number..."); error = true; }
                }
            }

            if (DrawLayer_LB.Visible == true)
            {
                if (DrawLayer_LB.Text.Length > 0)
                    newBloon.DrawLayer = DrawLayer_LB.SelectedItem.ToString();
            }

            if (!AdvancedView_Checkbox.Checked || forceAdvancedView == false)
            {
                newBloon.CanGoUnderground = true;
            }
            //if(CanGoUnderground_CheckBox.Visible == true)

            if (RotateToPathDirection_Checkbox.Visible == true)
                newBloon.RotateToPathDirection = RotateToPathDirection_Checkbox.Checked;

            //Save child bloons
            object[] children = new object[0];
            foreach (var item in CurrentChildBloon_ListBox.Items)
            {
                Array.Resize(ref children, children.Length + 1);
                string childName = item.ToString();

                if (!item.ToString().EndsWith(".bloon"))
                    childName = childName + ".bloon";

                children[children.Length-1] = childName;
            }
            newBloon.ChildBloons = children;

            newBloon.StatusImmunity = CreateStringArray_FromCheckedLB(StatusImmunity_CheckedListBox);
            newBloon.DamageImmunity = CreateStringArray_FromCheckedLB(DamageImmunity_CheckedListBox);
            if(game == "BTD5")
            {
                newBloon.BloonAbility = CreateStringArray_FromCheckedLB(BloonAbility_CheckedListBox);
                newBloon.ApplyStatus = CreateStringArray_FromCheckedLB(ApplyStatus_CheckedListBox);
            }

            if (!error)
            {
                var saveFile = SerializeBloon.ToJson(newBloon);
                File.WriteAllText(path, saveFile);
            }
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
            foreach (var cbloon in bloon.ChildBloons)
            {
                CurrentChildBloon_ListBox.Items.Add(cbloon.ToString().Replace(".bloon", ""));
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
            CurrentChildBloon_ListBox.Items.Clear();

            //reset advanced stuff
            CanGoUnderground_CheckBox.Checked = false;
            RotateToPathDirection_Checkbox.Checked = false;
            DrawLayer_LB.SelectedItem = "";
            HitAddon_TextBox.Text = "";

            //reset checkboxes
            resetCheckedListBox(StatusImmunity_CheckedListBox);
            resetCheckedListBox(DamageImmunity_CheckedListBox);

            //BTD5 stuff
            if (game == "BTD5")
            {
                resetCheckedListBox(BloonAbility_CheckedListBox);
                resetCheckedListBox(ApplyStatus_CheckedListBox);
            }
        }
        private CheckedListBox resetCheckedListBox(CheckedListBox checkedListBox)
        {
            string[] temp = new string[checkedListBox.Items.Count];
            checkedListBox.Items.CopyTo(temp, 0);
            checkedListBox.Items.Clear();

            foreach (var item in temp)
            {
                checkedListBox.Items.Add(item, false);
            }
            return checkedListBox;
        }
        //
        // Handling UI changes
        //
        private void EasyTowerEditor_Shown(object sender, EventArgs e)
        {
            EZBloon_Opened = true;

            if(Main.projName != "" && Main.projName != null)
            {
                string bloonPath = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions";
                var bloonFiles = Directory.GetFiles(bloonPath);
                foreach (string file in bloonFiles)
                {
                    string[] split = file.Split('\\');
                    string filename = split[split.Length - 1].Replace("\\", "");

                    if (filename.EndsWith(".bloon"))
                    {
                        if (!BloonFiles_ComboBox.Items.Contains(filename))
                            BloonFiles_ComboBox.Items.Add(filename);

                        if (!AvailibleChildBloons_LB.Items.Contains(filename.Replace(".bloon","")))
                            AvailibleChildBloons_LB.Items.Add(filename.Replace(".bloon", ""));

                        if (file == path)
                            BloonFiles_ComboBox.SelectedItem = BloonFiles_ComboBox.Items[BloonFiles_ComboBox.Items.Count - 1];
                    }
                }

                if(game == "BTD5")
                {
                    Game_Label.Text = "BTD5";
                    string abilityPath = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonAbilities";
                    var abilityFiles = Directory.GetFiles(abilityPath);
                    foreach (var file in abilityFiles)
                    {
                        string[] split = file.Split('\\');
                        string filename = split[split.Length - 1].Replace("\\", "");
                        if (!BloonAbility_CheckedListBox.Items.Contains(filename.Replace(".json","")))
                        {
                            BloonAbility_CheckedListBox.Items.Add(filename.Replace(".json", ""), false);
                        }
                    }
                    ApplyStatus_CheckedListBox.Show();
                    ApplyStatus_Label.Show();
                }
                else
                {
                    Game_Label.Text = "BTDB";
                    ApplyStatus_CheckedListBox.Hide();
                    ApplyStatus_Label.Hide();
                }
                CreateBloonObject(path);
            }
            else
            {
                ConsoleHandler.force_appendNotice("You need to have a project opened to use this tool...");
                this.Close();
            }
        }
        private void BloonFiles_ComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            path = Environment.CurrentDirectory + "\\" + Serializer.Deserialize_Config().LastProject + "\\Assets\\JSON\\BloonDefinitions\\" + BloonFiles_ComboBox.SelectedItem;
            CreateBloonObject(path);
        }
        private void Save_Button_Click(object sender, EventArgs e)
        {
            SaveFile();
            ConsoleHandler.appendLog_CanRepeat("Saved " + BloonFiles_ComboBox.SelectedItem.ToString());
        }
        private void SwitchPanel_Click(object sender, EventArgs e)
        {
            Panel3.Hide();
            ChildBloons_Button.Text = "Child bloons";
            if (SwitchPanel.Text == "Page 2")
            {
                Panel1.Visible = false;
                Panel2.Visible = true;
                SwitchPanel.Text = "Page 1";
            }
            else if (SwitchPanel.Text == "Page 1")
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
                DrawLayer_LB.Visible = true;
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
                    DrawLayer_LB.Visible = false;
                    DrawLayer_Label.Visible = false;
                    HitAddon_Label.Visible = false;
                    HitAddon_TextBox.Visible = false;

                    label2.Text = "Bloon type (cant edit):";
                    BloonName_TextBox.ReadOnly = true;
                }
            }
        }
        private void ChildBloons_Button_Click(object sender, EventArgs e)
        {
            if (Panel3.Visible == true)
            {
                Panel3.Hide();
                if (SwitchPanel.Text == "Page 2")
                    Panel1.Show();
                else if (SwitchPanel.Text == "Page 1")
                    Panel2.Show();

                ChildBloons_Button.Text = "Child Bloons";
            }
            else
            {
                Panel1.Hide();
                Panel2.Hide();
                Panel3.Show();

                if (SwitchPanel.Text == "Page 2")
                    ChildBloons_Button.Text = "Page 1";
                else if (SwitchPanel.Text == "Page 1")
                    ChildBloons_Button.Text = "Page 2";
            }   
        }
        private void AddChild_Button_Click(object sender, EventArgs e)
        {
            if (AvailibleChildBloons_LB.SelectedItem != null)
            {
                if (AvailibleChildBloons_LB.SelectedItem.ToString() != bloon.Type)
                {
                    CurrentChildBloon_ListBox.Items.Add(AvailibleChildBloons_LB.SelectedItem.ToString());
                }
                else
                {
                    ConsoleHandler.force_appendLog_CanRepeat("This tool does not allow adding the current selected bloon as a child bloon...");
                }
            }
            else
            {
                ConsoleHandler.appendLog_CanRepeat("You didn't select a bloon to add");
            }
        }
        private void RemoveChild_Button_Click(object sender, EventArgs e)
        {
            int selected = CurrentChildBloon_ListBox.SelectedIndex;
            int max = CurrentChildBloon_ListBox.Items.Count;
            if(CurrentChildBloon_ListBox.SelectedItem != null)
            {
                CurrentChildBloon_ListBox.Items.RemoveAt(CurrentChildBloon_ListBox.SelectedIndex);
                if (selected + 1 < max)
                    CurrentChildBloon_ListBox.SelectedIndex = selected;
                else
                    CurrentChildBloon_ListBox.SelectedIndex = selected - 1;
            }
            else
            {
                ConsoleHandler.appendLog_CanRepeat("You didn't select a bloon to remove");
            }
        }

        private void EZBloon_Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                SaveFile();
                ConsoleHandler.appendLog_CanRepeat("Saved " + BloonFiles_ComboBox.SelectedItem.ToString());
                GeneralMethods.CompileJet("launch");
                ConsoleHandler.appendLog_CanRepeat("Launching " + game);
            }
        }

        private void OpenText_Button_Click(object sender, EventArgs e)
        {
            JsonEditorHandler.OpenFile(path);
            this.Focus();
        }

        private void EZBloon_Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            EZBloon_Opened = false;
        }
    }
}
