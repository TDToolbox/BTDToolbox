﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using BTDToolbox.Classes;
//
//    var specialtyBuildingClass = SpecialtyBuildingClass.FromJson(jsonString);

namespace BTDToolbox.Classes
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class SpecialtyBuildingClass
    {
        [JsonProperty("FileName")]
        public string FileName { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("Building")]
        public string Building { get; set; }

        [JsonProperty("Prices")]
        public long[] Prices { get; set; }

        [JsonProperty("RelatedTower")]
        public string RelatedTower { get; set; }

        [JsonProperty("Effects")]
        public Effects Effects { get; set; }
    }

    public partial class Effects
    {
        [JsonProperty("I")]
        public I I { get; set; }

        [JsonProperty("II")]
        public Ii Ii { get; set; }

        [JsonProperty("III")]
        public Iii Iii { get; set; }

        [JsonProperty("IV")]
        public Iv Iv { get; set; }

        [JsonProperty("X")]
        public I X { get; set; }
    }

    public partial class I
    {
        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("TowerModifiers")]
        public ITowerModifier[] TowerModifiers { get; set; }
    }

    public partial class ITowerModifier
    {
        [JsonProperty("TargetingFilter")]
        public TargetingFilter TargetingFilter { get; set; }

        [JsonProperty("Modifier")]
        public PurpleModifier Modifier { get; set; }
    }

    public partial class PurpleModifier
    {
        [JsonProperty("Cost")]
        public double[] Cost { get; set; }
    }

    public partial class TargetingFilter
    {
        [JsonProperty("Include")]
        public Include Include { get; set; }
    }

    public partial class Include
    {
        [JsonProperty("TowerType")]
        public string[] TowerType { get; set; }
    }

    public partial class Ii
    {
        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("TowerModifiers")]
        public IiTowerModifier[] TowerModifiers { get; set; }
    }

    public partial class IiTowerModifier
    {
        [JsonProperty("TargetingFilter")]
        public TargetingFilter TargetingFilter { get; set; }

        [JsonProperty("Modifier")]
        public FluffyModifier Modifier { get; set; }
    }

    public partial class FluffyModifier
    {
        [JsonProperty("FireDelay")]
        public double[] FireDelay { get; set; }

        [JsonProperty("Cooldown")]
        public double[] Cooldown { get; set; }
    }

    public partial class Iii
    {
        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("TowerModifiers")]
        public IiiTowerModifier[] TowerModifiers { get; set; }
    }

    public partial class IiiTowerModifier
    {
        [JsonProperty("TargetingFilter")]
        public TargetingFilter TargetingFilter { get; set; }

        [JsonProperty("Modifier")]
        public TentacledModifier Modifier { get; set; }
    }

    public partial class TentacledModifier
    {
        [JsonProperty("EnabledSubtasks")]
        public EnabledSubtask[][] EnabledSubtasks { get; set; }
    }

    public partial class Iv
    {
        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("TowerModifiers")]
        public IvTowerModifier[] TowerModifiers { get; set; }
    }

    public partial class IvTowerModifier
    {
        [JsonProperty("TargetingFilter")]
        public TargetingFilter TargetingFilter { get; set; }

        [JsonProperty("Modifier")]
        public StickyModifier Modifier { get; set; }
    }

    public partial class StickyModifier
    {
        [JsonProperty("WeaponTargetingArc")]
        public double[] WeaponTargetingArc { get; set; }
    }

    public partial struct EnabledSubtask
    {
        public bool? Bool;
        public long? Integer;
        public long[] IntegerArray;

        public static implicit operator EnabledSubtask(bool Bool) => new EnabledSubtask { Bool = Bool };
        public static implicit operator EnabledSubtask(long Integer) => new EnabledSubtask { Integer = Integer };
        public static implicit operator EnabledSubtask(long[] IntegerArray) => new EnabledSubtask { IntegerArray = IntegerArray };
    }

    public partial class SpecialtyBuildingClass
    {
        public static SpecialtyBuildingClass FromJson(string json) => JsonConvert.DeserializeObject<SpecialtyBuildingClass>(json, BTDToolbox.Classes.Specialty_Converter.Settings);
    }

    public static class Serialize_Specialty
    {
        public static string ToJson(this SpecialtyBuildingClass self) => JsonConvert.SerializeObject(self, BTDToolbox.Classes.Specialty_Converter.Settings);
    }

    internal static class Specialty_Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                EnabledSubtaskConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class EnabledSubtaskConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(EnabledSubtask) || t == typeof(EnabledSubtask?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    var integerValue = serializer.Deserialize<long>(reader);
                    return new EnabledSubtask { Integer = integerValue };
                case JsonToken.Boolean:
                    var boolValue = serializer.Deserialize<bool>(reader);
                    return new EnabledSubtask { Bool = boolValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<long[]>(reader);
                    return new EnabledSubtask { IntegerArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type EnabledSubtask");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (EnabledSubtask)untypedValue;
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value);
                return;
            }
            if (value.Bool != null)
            {
                serializer.Serialize(writer, value.Bool.Value);
                return;
            }
            if (value.IntegerArray != null)
            {
                serializer.Serialize(writer, value.IntegerArray);
                return;
            }
            throw new Exception("Cannot marshal type EnabledSubtask");
        }

        public static readonly EnabledSubtaskConverter Singleton = new EnabledSubtaskConverter();
    }
}