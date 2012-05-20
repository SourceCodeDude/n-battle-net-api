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

        [DataMember(Name = "wintergrasp")]
        public PvPAreaStatus WinterGrasp { get; set; }

        [DataMember(Name = "tol-barad")]
        public PvPAreaStatus TolBarad { get; set; }

    }

    [DataContract]
    public class PvPAreaStatus
    {
        [DataMember(Name = "area")]
        public int Area { get; set; }

        [DataMember(Name = "controlling-faction")]
        public Side ControllingFaction { get; set; }

        [DataMember(Name = "status")]
        public PvPZoneStatus Status { get; set; }

        /// <summary>
        /// Time of the next battle
        /// </summary>
        [XmlElement("next")]
        public UnixTimestamp Next { get; set; }

        [DataMember(Name = "next")]
        internal long next
        {
            get
            {
                return Next.ToMsec();
            }
            set
            {
                Next = new UnixTimestamp(value);
            }
        }
    }

    [DataContract]
    public enum PvPZoneStatus
    {
        Unknown = -1,
        Idle = 0,
        Populating = 1,
        Active = 2,
        Concluded = 3,
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
