using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    // TODO: need to rename this. Shouldnt be any
    // pluralized class names
    [DataContract]
    public class Achievements
    {
        [XmlArray("achievementsCompleted")]
        [XmlArrayItem("item")]
        [DataMember(Name="achievementsCompleted")]
        public List<int> achievementsCompleted { get; set; }

        [XmlArray("achievementsCompletedTimestamp")]
        [XmlArrayItem("item")]        
        public List<UnixTimestamp> AchievementsCompletedTimestamp { get; set; }
        
        [DataMember(Name = "achievementsCompletedTimestamp")]
        internal List<long> achievementsCompletedTimestamp
        {
            get
            {
                return null;
            }
            set
            {
                AchievementsCompletedTimestamp = new List<UnixTimestamp>();
                foreach (long l in value)
                {
                    AchievementsCompletedTimestamp.Add(new UnixTimestamp(l));
                }
            }
        }

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
