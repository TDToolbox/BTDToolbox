using BTDToolbox.Classes.JSON_Classes;
using BTDToolbox.Classes.NewProjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace BTDToolbox.Classes
{
    class NewTower
    {
        #region Properties

        public enum TowerSelectMenu_Pos
        {
            Left,
            Right,
            FixedLeft,
            FixedRight
        }
        public Dictionary<TowerSelectMenu_Pos, string> TowerSelectFiles;

        public bool UseBaseTower { get; set; }
        public bool CreateSpecialty { get; set; }
        public string TowerName { get; set; }
        public string BaseTowerFile { get; set; }
        public string BaseTowerName_NoExt { get; set; }
        public TowerSelectMenu_Pos TowerSelPos { get; set; }

        #endregion


        #region Constructors
        public NewTower()
        {
            string towerSelFolder = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\ScreenDefinitions\\TowerSelectionScreen";

            TowerSelectFiles = new Dictionary<TowerSelectMenu_Pos, string>();
            TowerSelectFiles.Add(TowerSelectMenu_Pos.FixedRight, towerSelFolder + "\\FixedTowerBottom.json");
            TowerSelectFiles.Add(TowerSelectMenu_Pos.FixedLeft, towerSelFolder + "\\FixedTowerTop.json");
            TowerSelectFiles.Add(TowerSelectMenu_Pos.Right, towerSelFolder + "\\TowerOrderBottom.json");
            TowerSelectFiles.Add(TowerSelectMenu_Pos.Left, towerSelFolder + "\\TowerOrderTop.json");
        }
        public NewTower(string newTowerName, string baseTowerPath)
        {

        }

        public NewTower(string newTowerName, string baseTowerPath, TowerSelectMenu_Pos towerSelectMenu_Pos) :this()
        {
            this.TowerName = newTowerName;
            this.BaseTowerFile = baseTowerPath;
            this.TowerSelPos = towerSelectMenu_Pos;

            if (Guard.IsStringValid(BaseTowerFile))
                this.UseBaseTower = true;
            else
                this.UseBaseTower = false;
        }
        #endregion

        public void DuplicateAllTowerFiles()
        {
            BaseTowerName_NoExt = BaseTowerFile.Replace(".tower", "");

            if (CreateSpecialty)
                AddSpecialty();

            //AddToTowerSelctionMenu();
        }

        private string CheckJSONFromFile(string path)
        {
            if (!File.Exists(path))
            {
                ConsoleHandler.append_CanRepeat("Unable to make jsonObject because it doesnt exist at:  " + path);
                return "";
            }

            string text = File.ReadAllText(path);
            if (JSON_Reader.IsValidJson(text))
            {
                return text;
            }
            return "";
        }


        #region TowerSelectionMenu Stuff
        public void AddToTowerSelctionMenu()
        {

            string towerSelFilePath = TowerSelectFiles[TowerSelPos];
            if (!File.Exists(towerSelFilePath))
            {
                ConsoleHandler.append_Force("Unable to find the Tower Selection file!");
                return;
            }

            string text = CheckJSONFromFile(towerSelFilePath);
            if(!Guard.IsStringValid(text))
            {
                FileInfo file = new FileInfo(towerSelFilePath);
                ConsoleHandler.append_Force("The Tower Select File  " + file.Name + "  has has invalid text/JSON. Unable to add tower to TowerSelectionMenu");
                return;
            }

            TowerSelectionMenu menu = TowerSelectionMenu.FromJson(text);
            TowerSelectItem newItem = new TowerSelectItem();
            bool foundBaseTower = false;
            if (UseBaseTower)
            {
                newItem = TowerSelMenu_DupBaseTower();
                if (!Guard.IsStringValid(newItem.ToString()))
                {
                    foundBaseTower = false;
                    ConsoleHandler.append("Unable to find chosen base tower in tower selection menu files. Using blank values instead.");
                }
                else
                    foundBaseTower = true;
            }
            
            if(!UseBaseTower || !foundBaseTower)
                newItem = TowerSelMenu_EmptyTower(newItem);

            if (newItem == null)
                return;
            if (menu == null)
                return;

            AddTowerToSelMenu(newItem, menu);

        }
        private void AddTowerToSelMenu(TowerSelectItem newItem, TowerSelectionMenu menu)
        {
            menu.Items.Add(newItem);
            string text = menu.ToJson();

            StreamWriter serialize = new StreamWriter(TowerSelectFiles[TowerSelPos], false);
            serialize.Write(text);
            serialize.Close();

            ConsoleHandler.append("Tower added to " + TowerSelectFiles[TowerSelPos].Replace(CurrentProjectVariables.PathToProjectFiles,""));
        }

        private TowerSelectItem TowerSelMenu_EmptyTower(TowerSelectItem newItem)
        {
            newItem.FactoryName = TowerName;
            newItem.Icon = "";
            newItem.KeyboardShortcut = "";

            if (TowerSelPos == TowerSelectMenu_Pos.FixedLeft || TowerSelPos == TowerSelectMenu_Pos.FixedRight)
                newItem.ObjectType = 1;
            else
                newItem.ObjectType = 0;

            return newItem;
        }
        private TowerSelectItem TowerSelMenu_DupBaseTower()
        {
            FileInfo baseTower = new FileInfo(BaseTowerFile);
            TowerSelectItem newItem = new TowerSelectItem();

            foreach(var a in TowerSelectFiles)
            {
                string tempText = CheckJSONFromFile(a.Value);

                if (Guard.IsStringValid(tempText))
                {
                    TowerSelectionMenu tempMenu = TowerSelectionMenu.FromJson(tempText);
                    if(tempMenu != null)
                    {
                        foreach (TowerSelectItem item in tempMenu.Items)
                        {
                            if (item.ToString() == baseTower.Name.Replace(".tower", ""))
                            {
                                newItem.FactoryName = TowerName;
                                newItem.Icon = item.Icon;
                                newItem.KeyboardShortcut = item.KeyboardShortcut;
                                newItem.ObjectType = item.ObjectType;
                                break;
                            }
                        }
                    }
                }
            }

            return newItem;
        }
        #endregion


        #region SpecialtyBuilding Stuff
        private void AddSpecialty()
        {
            string specialtyPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\SpecialtyDefinitions\\" + TowerName + ".json";
            if (File.Exists(specialtyPath))
                File.Delete(specialtyPath);

            if (UseBaseTower)
                MakeDupSpecialty(specialtyPath);
            else
                MakeNewSpecialty(specialtyPath);
        }

        private void MakeNewSpecialty(string savePath)
        {
            MessageBox.Show("21:   " + savePath);
            //MessageBox.Show("2:   " + CurrentProjectVariables.PathToProjectFiles);
            SpecialtyBuildingClass specialty = new SpecialtyBuildingClass();
            MessageBox.Show("22:   " + savePath);
            //specialty.ToJson();

            FileInfo f = new FileInfo(savePath);
            specialty.FileName = f.Name;
            specialty.Name = "";
            specialty.Icon = "";
            specialty.Building = "";

            long[] newPrices = { 0, 0, 0, 0 };
            specialty.Prices = newPrices;

            specialty.RelatedTower = TowerName;


            
            specialty.Tiers = new Tiers()
            {
                I = new I()
                {
                    Text = "",
                    TowerModifiers = new ITowerModifier[0]
                },
                Ii = new Ii()
                {
                    Text = "",
                    TowerModifiers = new IiTowerModifier[0]
                },
                Iii = new Iii()
                {
                    Text = "",
                    TowerModifiers = new IiiTowerModifier[0]
                },
                Iv = new Iv()
                {
                    Text = "",
                    TowerModifiers = new IvTowerModifier[0]
                },
                X = new I
                {
                    Text = "",
                    TowerModifiers = new ITowerModifier[0]
                }
            };
           

            SaveSpecialty(savePath, specialty);
        }
        private void MakeDupSpecialty(string savePath)
        {
            MessageBox.Show("12:   " + savePath);
            //MessageBox.Show("2:   " + CurrentProjectVariables.PathToProjectFiles);
            SpecialtyBuildingClass specialty = new SpecialtyBuildingClass();
            //specialty.ToJson();
            MessageBox.Show("11:   " + savePath);
            MessageBox.Show("BaseTowerName_NoExt:   " + BaseTowerName_NoExt);
            string baseSpecialPath = CurrentProjectVariables.PathToProjectFiles = "\\Assets\\JSON\\SpecialtyDefinitions\\" + BaseTowerName_NoExt + ".json";
            MessageBox.Show("baseSpecialPath  "+baseSpecialPath);
            if(!File.Exists(baseSpecialPath))
            {
                ConsoleHandler.append("The Base specialty building doesn't exist. Using a new one instead.");
                MakeNewSpecialty(savePath);
                return;
            }
            if (!JSON_Reader.IsValidJson(File.ReadAllText(baseSpecialPath)))
            {
                ConsoleHandler.append("The Base specialty building has invalid JSON. Using a new one instead.");
                MakeNewSpecialty(savePath);
                return;
            }

            SpecialtyBuildingClass baseSpecialty = SpecialtyBuildingClass.FromJson(baseSpecialPath);
            

            FileInfo f = new FileInfo(savePath);
            specialty.FileName = f.Name;
            specialty.Name = baseSpecialty.Name;
            specialty.Icon = baseSpecialty.Icon;
            specialty.Building = baseSpecialty.Building;

            specialty.Prices = baseSpecialty.Prices;
            specialty.RelatedTower = TowerName;

            specialty.Tiers = baseSpecialty.Tiers;
            SaveSpecialty(savePath, specialty);
        }
        
        private void SaveSpecialty(string path, SpecialtyBuildingClass specialty)
        {
            string text = specialty.ToJson();

            StreamWriter serialize = new StreamWriter(path, false);
            serialize.Write(text);
            serialize.Close();
        }
        #endregion
    }
}

