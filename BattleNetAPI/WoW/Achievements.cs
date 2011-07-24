using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
namespace BattleNet.API.WoW
{
    // TODO: need to rename this. Shouldnt be any
    // pluralized class names
    public class Achievements
    {
        [XmlArray("achievementsCompleted")]
        [XmlArrayItem("item")]
        public List<int> achievementsCompleted { get; set; }

        [XmlArray("achievementsCompletedTimestamp")]
        [XmlArrayItem("item")]
        public List<UnixTimestamp> achievementsCompletedTimestamp { get; set; }

        [XmlArray("criteria")]
        [XmlArrayItem("item")]
        public List<int> criteria { get; set; }

        [XmlArray("criteriaQuantity")]
        [XmlArrayItem("item")]
        public List<long> criteriaQuantity { get; set; }

        [XmlArray("criteriaTimestamp")]
        [XmlArrayItem("item")]
        public List<UnixTimestamp> criteriaTimestamp { get; set; }

        [XmlArray("criteriaCreated")]
        [XmlArrayItem("item")]
        public List<UnixTimestamp> criteriaCreated { get; set; }
    }
}
