using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace BattleNet.API.WoW
{
    [DataContract]
    public class Spell
    {
        [XmlElement("id")]
        [DataMember(Name="id")]
        public int Id { get; set; }
        
        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }
        
        [XmlElement("subtext")]
        [DataMember(Name = "subtext")]
        public string Subtext { get; set; }
        
        [XmlElement("icon")]
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
        
        [XmlElement("description")]
        [DataMember(Name = "description")]
        public string Description { get; set; }
        
        [XmlElement("range")]
        [DataMember(Name = "range")]
        public string Range { get; set; }
        
        [XmlElement("castTime")]
        [DataMember(Name = "castTime")]
        public int CastTime { get; set; }
        
        [XmlElement("cooldown")]
        [DataMember(Name = "cooldown")]
        public string Cooldown { get; set; }
    }
}
