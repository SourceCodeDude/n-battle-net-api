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
        [DataMember(Name = "criteria")]
        public List<int> Criteria { get; set; }

        [XmlArray("criteriaQuantity")]
        [XmlArrayItem("item")]
        [DataMember(Name = "criteriaQuantity")]
        public List<long> CriteriaQuantity { get; set; }

        [XmlArray("criteriaTimestamp")]
        [XmlArrayItem("item")]
        public List<UnixTimestamp> CriteriaTimestamp { get; set; }

        [DataMember(Name="criteriaTimestamp")]
        internal List<long> criteriaTimestamp 
        {
            get { return null; }
            set
            {
                List<UnixTimestamp> r = new List<UnixTimestamp>();
                foreach (long l in value)
                {
                    r.Add(new UnixTimestamp(l));
                }
                CriteriaTimestamp = r;
            }
        }

        [XmlArray("criteriaCreated")]
        [XmlArrayItem("item")]
        public List<UnixTimestamp> CriteriaCreated { get; set; }

        [DataMember(Name = "criteriaCreated")]
        private List<long> criteriaCreated {
            get { return null; }
            set
            {
                List<UnixTimestamp> r = new List<UnixTimestamp>();
                foreach (long l in value)
                {
                    r.Add(new UnixTimestamp(l));
                }
                CriteriaCreated = r;
            }
        }
    }
}
