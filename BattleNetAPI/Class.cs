using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    public enum PowerType
    {
        [XmlEnum("focus")] Focus,
        [XmlEnum("rage")] Rage,
        [XmlEnum("mana")] Mana,
        [XmlEnum("energy")] Energy,
        [XmlEnum("runic-power")] RunicPower,
    }

    public class ClassCollection
    {
        [XmlArray("classes")]
        [XmlArrayItem("item")]                
        public List<Class> Classes { get; set; }

    }
    public class Class
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("mask")]
        public int Mask { get; set; }
        [XmlElement("powerType")]
        public PowerType PowerType{get;set;}
        [XmlElement("name")]
        public string Name{get;set;}

    }
}
