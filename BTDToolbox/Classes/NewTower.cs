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
using Tower_Class;

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
        string towerDataFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NKHook5\\Plugins\\NewTowersList.json";

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


        public void CreateTowerPluginData()
        {
            string json = "";
            TowerList list = new TowerList();
            ConsoleHandler.append("Adding " + TowerName + " to auto-load tower list");
            if (File.Exists(towerDataFile))
            {
                json = File.ReadAllText(towerDataFile);
                JavaScriptSerializer js = new JavaScriptSerializer();
                list = js.Deserialize<TowerList>(json);
            }

            if (list.List == null)
                list.List = new List<string>();


            if (list.List.Contains(TowerName))
                return;

            list.List.Add(TowerName);

            string output_Cfg = JsonConvert.SerializeObject(list, Formatting.Indented);
            StreamWriter serialize = new StreamWriter(towerDataFile, false);
            serialize.Write(output_Cfg);
            serialize.Close();
            ConsoleHandler.append("Finished writing " + TowerName + " to the auto-load tower list." +
                " this will auto-load your tower into BTD5 if you have the plugin.");
        }

        public void DuplicateAllTowerFiles()
        {
            if(Guard.IsStringValid(BaseTowerFile))
            {
                FileInfo f = new FileInfo(BaseTowerFile);
                BaseTowerName_NoExt = f.Name.Replace(".tower", "");
                this.UseBaseTower = true;
            }

            if (CreateSpecialty)
                AddSpecialty();

            AddTowerDef();
            AddUpgradesFile();
            AddWeapons();
            AddTowerSpriteDef();
            AddToTowerSelctionMenu();

            CreateTowerPluginData();
            ConsoleHandler.append("Finished creating new tower: " + TowerName);
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

            ConsoleHandler.append("Creating new tower entry in TowerSelectionMenu");
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
            ConsoleHandler.append("Creating empty TowerSelectMenu entry");
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
            ConsoleHandler.append("Duplicating base TowerSelectMenu entry");
            foreach (var a in TowerSelectFiles)
            {
                string tempText = CheckJSONFromFile(a.Value);

                if (!Guard.IsStringValid(tempText))
                    continue;

                TowerSelectionMenu tempMenu = TowerSelectionMenu.FromJson(tempText);
                if (tempMenu == null)
                    continue;

                foreach (TowerSelectItem item in tempMenu.Items)
                {
                    if (item.FactoryName == baseTower.Name.Replace(".tower", ""))
                    {
                        ConsoleHandler.append("Found base tower's TowerSelect entry in: " + a.Value.Replace(CurrentProjectVariables.PathToProjectFiles,""));
                        newItem.FactoryName = TowerName;
                        newItem.Icon = item.Icon;
                        newItem.KeyboardShortcut = item.KeyboardShortcut;
                        newItem.ObjectType = item.ObjectType;
                        break;
                    }
                }
            }

            return newItem;
        }
        #endregion


        #region SpecialtyBuilding Stuff
        private void AddSpecialty()
        {
            ConsoleHandler.append("Creating Specialty Building");
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
            ConsoleHandler.append("Creating new Specialty Building");
            SpecialtyBuildingClass specialty = new SpecialtyBuildingClass();

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
            ConsoleHandler.append("Duplicating base tower's specialty building");
            SpecialtyBuildingClass specialty = new SpecialtyBuildingClass();

            string baseSpecialName = GetSpecialtyBuilding();

            string baseSpecialPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\SpecialtyDefinitions\\" + baseSpecialName;
            
            if(!File.Exists(baseSpecialPath))
            {
                ConsoleHandler.append("Unable to find the base tower's specialty building by RelatedTower property" +
                    ". Creating a new one instead.");
                MakeNewSpecialty(savePath);
                return;
            }
            if (!JSON_Reader.IsValidJson(File.ReadAllText(baseSpecialPath)))
            {
                ConsoleHandler.append("The Base specialty building has invalid JSON. Using a new one instead.");
                MakeNewSpecialty(savePath);
                return;
            }

            SpecialtyBuildingClass baseSpecialty = new SpecialtyBuildingClass();
            baseSpecialty = SpecialtyBuildingClass.FromJson(baseSpecialPath);

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
        public string GetSpecialtyBuilding()
        {
            string specialtyBuilding = "";
            string projPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\";

            ConsoleHandler.append("Searching for base tower's specialty building...");
            foreach (var x in Directory.GetFiles(projPath + "SpecialtyDefinitions"))
            {
                string json = File.ReadAllText(x);
                if (!JSON_Reader.IsValidJson(json))
                    continue;

                SpecialtyBuildingClass s = new SpecialtyBuildingClass();
                s = SpecialtyBuildingClass.FromJson(json);

                if (s != null && s.RelatedTower != null && s.RelatedTower == BaseTowerName_NoExt)
                {  
                    specialtyBuilding = x.Replace(projPath + "SpecialtyDefinitions\\", "");
                    ConsoleHandler.append("Found base tower's specialty building called:  " + specialtyBuilding);
                    break;
                }
            }
            return specialtyBuilding;
        }
        private void SaveSpecialty(string path, SpecialtyBuildingClass specialty)
        {
            ConsoleHandler.append("Saving specialty building");
            string text = specialty.ToJson();

            StreamWriter serialize = new StreamWriter(path, false);
            serialize.Write(text);
            serialize.Close();
        }
        #endregion


        #region TowerDefinition Stuff
        private void AddTowerDef()
        {
            ConsoleHandler.append("Creating tower definition for new tower");
            string towerDefPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\" + TowerName + ".tower";
            if (File.Exists(towerDefPath))
                File.Delete(towerDefPath);

            if (UseBaseTower)
                MakeDupeTowerDef(towerDefPath);
            else
                MakeNewTowerDef(towerDefPath);
        }

        private void MakeNewTowerDef(string savePath)
        {
            ConsoleHandler.append("Creating new tower definition");
            Tower_Class.Tower tower = new Tower_Class.Tower()
            {
                AircraftList = { },
                BaseCost = 0,
                CanBePlacedInWater = false,
                CanBePlacedOnLand = true,
                CanBePlacedOnPath = false,
                DefaultWeapons = { },
                Icon = "",
                PlacementH = 0,
                PlacementW = 0,
                PlacementRadius = 0,
                RankToUnlock = 1,
                RotatesToTarget = false,
                TargetIsWeaponOrigin = false,
                TargetingMode = "First",
                TypeName = TowerName,
                UseRadiusPlacement = true,
                SpriteUpgradeDefinition = TowerName + ".json",

                Upgrades = CreateDoubleArray_String(new string[8] { "", "", "", "", "", "", "", "" }),
                UpgradeIcons = CreateDoubleArray_String(new string[8] { "", "", "", "", "", "", "", "" }),
                UpgradeAvatars = CreateDoubleArray_String(new string[8] { "", "", "", "", "", "", "", "" }),

                UpgradeGateway = CreateDoubleArray_UpgradeGateway(new long[8] { 0, 0, 0, 0, 0, 0, 0, 0 }, new long[8] { 0, 0, 0, 0, 0, 0, 0, 0 }),
                UpgradePrices = CreateDoubleArray_Long(new long[8] { 0,0,0,0,0,0,0,0})
            };

            SaveTowerDef(savePath, tower);
        }
        private void MakeDupeTowerDef(string savePath)
        {
            ConsoleHandler.append("Duplicating base tower's TowerDefinition");
            string baseTowerDefPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerDefinitions\\" + BaseTowerName_NoExt + ".tower";

            if (!File.Exists(baseTowerDefPath))
            {
                ConsoleHandler.append("The Base Tower's TowerDefinition wasn't found. Creating a new one instead.");
                MakeNewTowerDef(savePath);
                return;
            }
            if (!JSON_Reader.IsValidJson(File.ReadAllText(baseTowerDefPath)))
            {
                ConsoleHandler.append("The Base Tower's TowerDefinition has invalid JSON. Creating a new one instead.");
                MakeNewTowerDef(baseTowerDefPath);
                return;
            }

            Tower_Class.Tower tower = Tower_Class.Tower.FromJson(baseTowerDefPath);
            tower.TypeName = TowerName;
            tower.SpriteUpgradeDefinition = TowerName + ".json";

            SaveTowerDef(savePath, tower);
        }

        private void SaveTowerDef(string path, Tower_Class.Tower tower)
        {
            ConsoleHandler.append("Saving TowerDefiniton");
            string text = tower.ToJson();

            StreamWriter serialize = new StreamWriter(path, false);
            serialize.Write(text);
            serialize.Close();
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

        private Tower_Class.UpgradeGateway[][] CreateDoubleArray_UpgradeGateway(long[] rankArray, long[] XpArray)
        {
            int upgradeCount = 4;
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
        private long[][] CreateDoubleArray_Long(long[] inputArray)
        {
            int upgradeCount = inputArray.Length / 2;
            long[] leftPath = new long[] { };
            long[] rightPath = new long[] { };

            for (int i = 0; i < upgradeCount; i++)
            {
                Array.Resize(ref leftPath, leftPath.Length + 1);
                leftPath[leftPath.Length - 1] = inputArray[i];

            }
            for (int i = 0; i < upgradeCount; i++)
            {
                Array.Resize(ref rightPath, rightPath.Length + 1);
                rightPath[rightPath.Length - 1] = inputArray[i];
            }

            long[][] upgrades = new long[][] { leftPath, rightPath };
            return upgrades;
        }

        #endregion


        #region TowerSpriteUpgrade Stuff
        private void AddTowerSpriteDef()
        {
            ConsoleHandler.append("Creating TowerSpriteUpgrade file");
            string towerSpriteDefPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerSpriteUpgradeDefinitions\\" + TowerName + ".json";
            if (File.Exists(towerSpriteDefPath))
                File.Delete(towerSpriteDefPath);

            if (UseBaseTower)
                MakeDupTowerSpriteUpgradeDef(towerSpriteDefPath);
            else
                MakeNewTowerSpriteUpgradeDef(towerSpriteDefPath);
        }

        private void MakeNewTowerSpriteUpgradeDef(string savePath)
        {
            ConsoleHandler.append("Creating a new TowerSpriteUpgrade file");
            var spritesDictionary = new Dictionary<string, string>();
            for (int i = 0; i < 7; i++)
                spritesDictionary.Add(i.ToString(), "");

            var spriteUpgradeLevels = new Dictionary<string, long>();
            spriteUpgradeLevels.Add("00", 0);
            spriteUpgradeLevels.Add("01", 0);
            spriteUpgradeLevels.Add("02", 0);
            spriteUpgradeLevels.Add("03", 0);
            spriteUpgradeLevels.Add("04", 0);
            spriteUpgradeLevels.Add("10", 0);
            spriteUpgradeLevels.Add("11", 0);
            spriteUpgradeLevels.Add("12", 0);
            spriteUpgradeLevels.Add("13", 0);
            spriteUpgradeLevels.Add("14", 0);
            spriteUpgradeLevels.Add("20", 0);
            spriteUpgradeLevels.Add("21", 0);
            spriteUpgradeLevels.Add("22", 0);
            spriteUpgradeLevels.Add("23", 0);
            spriteUpgradeLevels.Add("24", 0);
            spriteUpgradeLevels.Add("30", 0);
            spriteUpgradeLevels.Add("31", 0);
            spriteUpgradeLevels.Add("32", 0);
            spriteUpgradeLevels.Add("40", 0);
            spriteUpgradeLevels.Add("41", 0);
            spriteUpgradeLevels.Add("42", 0);
            

            TowerSpriteUpgrade spriteUpgrade = new TowerSpriteUpgrade()
            {
                Sprites = spritesDictionary,
                UpgradeLevels = spriteUpgradeLevels
            };
            SaveTowerSpriteDef(savePath, spriteUpgrade);
        }

        private void MakeDupTowerSpriteUpgradeDef(string savePath)
        {
            ConsoleHandler.append("Duplicating base tower's TowerSpriteUpgrade file");
            string baseTowerSpriteDefPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\TowerSpriteUpgradeDefinitions\\" + BaseTowerName_NoExt + ".json";
            if (!File.Exists(baseTowerSpriteDefPath))
            {
                ConsoleHandler.append("The Base TowerSpriteUpgradeDefinition doesn't exist. Creating a new one instead.");
                MakeNewTowerSpriteUpgradeDef(savePath);
                return;
            }
            if (!JSON_Reader.IsValidJson(File.ReadAllText(baseTowerSpriteDefPath)))
            {
                ConsoleHandler.append("The Base TowerSpriteUpgradeDefinition has invalid JSON. Creating a new one instead.");
                MakeNewTowerSpriteUpgradeDef(savePath);
                return;
            }

            if (File.Exists(savePath))
                File.Delete(savePath);

            ConsoleHandler.append("Saving TowerSpriteUpgrade file");
            File.Copy(baseTowerSpriteDefPath, savePath);
        }
        private void SaveTowerSpriteDef(string path, TowerSpriteUpgrade towerSprite)
        {
            ConsoleHandler.append("Saving TowerSpriteUpgrade file");
            string text = towerSprite.ToJson();

            StreamWriter serialize = new StreamWriter(path, false);
            serialize.Write(text);
            serialize.Close();
        }
        #endregion


        #region UpgradesFile stuff
        private void AddUpgradesFile()
        {
            ConsoleHandler.append("Creating UpgradeDefinitions file");
            string towerSpriteDefPath = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\UpgradeDefinitions\\" + TowerName + ".upgrades";
            if (File.Exists(towerSpriteDefPath))
                File.Delete(towerSpriteDefPath);

            if (UseBaseTower)
                DupUpgradesFile(towerSpriteDefPath);
            else
                MakeNewUpgradesFile(towerSpriteDefPath);
        }
        private void MakeNewUpgradesFile(string savePath)
        {
            ConsoleHandler.append("Creating new UpgradeDefinitions file");
            UpgradesFile upgrades = new UpgradesFile();
            upgrades.Upgrades = new Upgrade[8];
            for(int i = 0; i<upgrades.Upgrades.Length; i++)
            {
                upgrades.Upgrades[i] = new Upgrade();
                upgrades.Upgrades[i].Name = "";
                upgrades.Upgrades[i].TowerUpgrade = new TowerUpgrade();
                upgrades.Upgrades[i].WeaponUpgrade = new WeaponUpgrade();
                upgrades.Upgrades[i].WeaponUpgrade.TaskUpgrade = new TaskUpgrade[0];

            }

            SaveUpgradesFile(upgrades, savePath);
        }
        private void DupUpgradesFile(string savePath)
        {
            ConsoleHandler.append("Duplicating base tower's UpgradeDefinitions");
            string baseTowerUpgradeDef = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\UpgradeDefinitions\\" + BaseTowerName_NoExt + ".upgrades";

            if (!File.Exists(baseTowerUpgradeDef))
            {
                ConsoleHandler.append("The Base Tower's UpgradeDefinitions wasn't found. Creating a new one instead.");
                MakeNewUpgradesFile(savePath);
                return;
            }
            if (!JSON_Reader.IsValidJson(File.ReadAllText(baseTowerUpgradeDef)))
            {
                ConsoleHandler.append("The Base Tower's UpgradeDefinitions has invalid JSON. Creating a new one instead.");
                MakeNewUpgradesFile(savePath);
                return;
            }

            UpgradesFile upgrades = UpgradesFile.FromJson(baseTowerUpgradeDef);
            
            if(upgrades.Upgrades == null)
            {
                ConsoleHandler.append("The Base Tower's UpgradeDefinitions is empty. Creating a new one instead.");
                MakeNewUpgradesFile(savePath);
                return;
            }

            SaveUpgradesFile(upgrades, savePath);
        }
        private void SaveUpgradesFile(UpgradesFile upgrades, string savePath)
        {
            ConsoleHandler.append("Saving UpgradeDefinition file");
            string text = upgrades.ToJson();

            StreamWriter serialize = new StreamWriter(savePath, false);
            serialize.Write(text);
            serialize.Close();

        }
        #endregion



        #region Weapons Stuff
        private void AddWeapons()
        {
            ConsoleHandler.append("Adding WeaponDefinitions"); 
            
            string weaponDefinitions = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\WeaponDefinitions\\" + TowerName;
            if (Directory.Exists(weaponDefinitions))
                Directory.Delete(weaponDefinitions, true);


            string baseTowerWeaponDef = CurrentProjectVariables.PathToProjectFiles + "\\Assets\\JSON\\WeaponDefinitions\\" + BaseTowerName_NoExt;
            if (UseBaseTower)
            {
                if(Directory.Exists(baseTowerWeaponDef))
                {
                    if (!Directory.Exists(weaponDefinitions))
                        Directory.CreateDirectory(weaponDefinitions);

                    ConsoleHandler.append("Duplicating Base Tower's WeaponDefinitions");
                    GeneralMethods.CopyDirectory(baseTowerWeaponDef, weaponDefinitions);
                }
                else
                    ConsoleHandler.append("Base Tower's WeaponDefinitions were not found. Creating empty weapon definitions");
            }
            else
            {
                ConsoleHandler.append("Creating new WeaponDefinitions");
                Directory.CreateDirectory(weaponDefinitions);
            }
           
            foreach(var jetform in JetProps.get())
            {
                if(jetform.dirInfo.FullName == CurrentProjectVariables.PathToProjectFiles)
                {
                    ConsoleHandler.append(jetform.dirInfo.FullName);
                    jetform.treeView1.Nodes.Clear();
                    jetform.listView1.Items.Clear();
                    jetform.PopulateTreeView(CurrentProjectVariables.PathToProjectFiles);
                }
            }           
        }

        #endregion
    }
    class TowerList
    {
        public List<string> List { get; set; }
    }
}

