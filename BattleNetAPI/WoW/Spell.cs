using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
namespace BattleNet.API.WoW
{
    public class Spell
    {
        [XmlElement("id")]          public int Id { get; set; }
        [XmlElement("name")]        public string Name { get; set; }
        [XmlElement("subtext")]     public string Subtext { get; set; }
        [XmlElement("icon")]        public string Icon { get; set; }
        [XmlElement("description")] public string Description { get; set; }
        [XmlElement("range")]       public string Range { get; set; }
        [XmlElement("castTime")]    public int CastTime { get; set; }
        [XmlElement("cooldown")]    public string Cooldown { get; set; }
    }
}
