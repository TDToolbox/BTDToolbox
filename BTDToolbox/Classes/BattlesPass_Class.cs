﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using BTDToolbox.Classes;
//
//    var battlesPassClass = BattlesPassClass.FromJson(jsonString);

namespace BTDToolbox.Classes
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class BattlesPassClass
    {
        [JsonProperty("Versions")]
        public VersionElement[] Versions { get; set; }
    }

    public partial class VersionElement
    {
        [JsonProperty("Version")]
        public VersionVersion Version { get; set; }
    }

    public partial class VersionVersion
    {
        [JsonProperty("version number")]
        public string VersionNumber { get; set; }

        [JsonProperty("Windows")]
        public string Windows { get; set; }

        [JsonProperty("Mac")]
        public string Mac { get; set; }

        [JsonProperty("Android")]
        public string Android { get; set; }

        [JsonProperty("IOS")]
        public string Ios { get; set; }
    }

    public partial class BattlesPassClass
    {
        public static BattlesPassClass FromJson(string json) => JsonConvert.DeserializeObject<BattlesPassClass>(json, BTDToolbox.Classes.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this BattlesPassClass self) => JsonConvert.SerializeObject(self, BTDToolbox.Classes.Converter.Settings);
    }
}