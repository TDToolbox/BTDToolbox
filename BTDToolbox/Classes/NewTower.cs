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
using System.Windows;

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
        public bool CreateSpecialtyBuilding { get; set; }
        public string TowerName { get; set; }
        public string BaseTowerFile { get; set; }
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
            AddToTowerSelctionMenu();
        }

        private string CheckJSONFromFile(string path)
        {
            if (!File.Exists(path))
            {
                ConsoleHandler.appendLog_CanRepeat("Unable to make jsonObject because it doesnt exist at:  " + path);
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
                ConsoleHandler.force_appendLog("Unable to find the Tower Selection file!");
                return;
            }

            string text = CheckJSONFromFile(towerSelFilePath);
            if(!Guard.IsStringValid(text))
            {
                FileInfo file = new FileInfo(towerSelFilePath);
                ConsoleHandler.force_appendLog("The Tower Select File  " + file.Name + "  has has invalid text/JSON. Unable to add tower to TowerSelectionMenu");
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
                    ConsoleHandler.appendLog("Unable to find chosen base tower in tower selection menu files. Using blank values instead.");
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
            string output_Cfg = JsonConvert.SerializeObject(text, Formatting.Indented);

            StreamWriter serialize = new StreamWriter(TowerSelectFiles[TowerSelPos], false);
            serialize.Write(text);
            serialize.Close();

            ConsoleHandler.appendLog("Success");
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
    }
}

