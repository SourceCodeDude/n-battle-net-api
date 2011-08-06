using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace BattleNet.API.WoW
{
    [DataContract]
    public class RealmCollection : ResponseRoot
    {
        [XmlArray("realms")]
        [XmlArrayItem("item")]
        [DataMember(Name="realms")]
        public List<Realm> Realms { get; set; }
    }
    
    [DataContract]
    public class Realm
    {
        [XmlElement("type")]        
        public RealmType Type { get; set; }

        [DataMember(Name = "type")]
        private string type
        {
            get { return Type.ToString(); }
            set { Type = (RealmType)Enum.Parse(typeof(RealmType), value, true); }
        }

        [XmlElement("queue")]
        [DataMember(Name = "queue")]
        public bool Queue { get; set; }
        
        [XmlElement("status")]
        [DataMember(Name = "status")]
        public bool Status { get; set; }
        
        [XmlElement("population")]        
        public RealmPopulation Population { get; set; }

        [DataMember(Name = "population")]
        private string population
        {
            get { return Population.ToString(); }
            set { Population = (RealmPopulation)Enum.Parse(typeof(RealmPopulation), value, true); }
        }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [XmlElement("slug")]
        [DataMember(Name = "slug")]
        public string Slug { get; set; }
    }

    [DataContract]
    public enum RealmPopulation
    {
        [XmlEnum("low")]
        [EnumMember(Value="low")]
        Low,
        [XmlEnum("medium")]
        [EnumMember(Value = "medium")]
        Medium,
        [XmlEnum("high")]
        [EnumMember(Value = "high")]
        High,
    }

    [DataContract]
    public enum RealmType
    {
        [XmlEnum("pve")]
        [EnumMember(Value = "pve")]
        PVE,
        [XmlEnum("pvp")]
        [EnumMember(Value = "pvp")]
        PVP,
        [XmlEnum("rp")]
        [EnumMember(Value = "rp")]
        RP,
        [XmlEnum("rppvp")]
        [EnumMember(Value = "rppvp")]
        RPPVP
    }
}
