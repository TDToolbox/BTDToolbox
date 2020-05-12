
namespace BTDToolbox.Classes.JSON_Classes
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UpgradesFile
    {
        [JsonProperty("Upgrades")]
        public Upgrade[] Upgrades { get; set; }
    }

    public partial class Upgrade
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("WeaponUpgrade", NullValueHandling = NullValueHandling.Ignore)]
        public WeaponUpgrade WeaponUpgrade { get; set; }

        [JsonProperty("TowerUpgrade", NullValueHandling = NullValueHandling.Ignore)]
        public TowerUpgrade TowerUpgrade { get; set; }
    }

    public partial class TowerUpgrade
    {
        [JsonProperty("Replace", NullValueHandling = NullValueHandling.Ignore)]
        public Replace Replace { get; set; }

        [JsonProperty("Weapons", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Weapons { get; set; }
    }

    public partial class Replace
    {
        [JsonProperty("CanTargetCamo")]
        public bool CanTargetCamo { get; set; }
    }

    public partial class WeaponUpgrade
    {
        [JsonProperty("SlotIndices", NullValueHandling = NullValueHandling.Ignore)]
        public long[] SlotIndices { get; set; }

        [JsonProperty("Adjust", NullValueHandling = NullValueHandling.Ignore)]
        public Adjust Adjust { get; set; }

        [JsonProperty("TaskUpgrade")]
        public TaskUpgrade[] TaskUpgrade { get; set; }

        [JsonProperty("SlotIndex", NullValueHandling = NullValueHandling.Ignore)]
        public long? SlotIndex { get; set; }
    }

    public partial class Adjust
    {
        [JsonProperty("TargetRange")]
        public long TargetRange { get; set; }
    }

    public partial class TaskUpgrade
    {
        [JsonProperty("Operation")]
        public string Operation { get; set; }

        [JsonProperty("Type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("Movement", NullValueHandling = NullValueHandling.Ignore)]
        public Movement Movement { get; set; }

        [JsonProperty("NumPersists", NullValueHandling = NullValueHandling.Ignore)]
        public long? NumPersists { get; set; }

        [JsonProperty("Location", NullValueHandling = NullValueHandling.Ignore)]
        public long[] Location { get; set; }

        [JsonProperty("RequiresMinUpgradeCounters", NullValueHandling = NullValueHandling.Ignore)]
        public long[] RequiresMinUpgradeCounters { get; set; }
    }

    public partial class Movement
    {
        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("CutOffDistance")]
        public long CutOffDistance { get; set; }
    }

    public partial class UpgradesFile
    {
        public static UpgradesFile FromJson(string json)
        {
            if(json.EndsWith(".upgrades"))
            {
                json = File.ReadAllText(json);
            }
            return JsonConvert.DeserializeObject<UpgradesFile>(json, BTDToolbox.Classes.JSON_Classes.Converter.Settings);
        }
    }
}
