using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    public class RaceCollection
    {
        [XmlArray("races")]
        [XmlArrayItem("item")]
        public List<Race> Races { get; set; }
    }

    public class Race
    {
        [XmlElement("id")]      public int Id { get; set; }
        [XmlElement("mask")]    public int Mask { get; set; }
        [XmlElement("side")]    public string Side { get; set; }
        [XmlElement("name")]    public string Name { get; set; }
    }
}
