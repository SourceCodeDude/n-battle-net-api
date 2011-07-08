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
        [XmlArrayItem("items")]                
        public List<Class> Classes { get; set; }

    }
    public class Class
    {
        public int id { get; set; }
        public int mask { get; set; }
        public PowerType powerType{get;set;}
        public string name{get;set;}

    }
}
