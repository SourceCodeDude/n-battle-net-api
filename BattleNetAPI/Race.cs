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
        public List<Race> races { get; set; }
    }

    public class Race
    {
        public int id { get; set; }
        public int mask { get; set; }
        public string side { get; set; }
        public string name { get; set; }
    }
}
