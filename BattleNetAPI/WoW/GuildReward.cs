using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace BattleNet.API.WoW
{
    [DataContract]    
    public class GuildRewardCollection : ResponseRoot
    {
        [XmlArray("rewards")]
        [XmlArrayItem("item")]
        [DataMember(Name="rewards")]
        public List<GuildReward> Rewards { get; set; }
    }

    [DataContract]
    public class GuildReward
    {
        [XmlElement("minGuildLevel")]
        [DataMember(Name="minGuildLevel")]
        public int MinGuildLevel { get; set; }
        
        [XmlElement("minGuildRepLevel")]
        [DataMember(Name="minGuildRepLevel")]
        public int MinGuildRepLevel { get; set; }

        [XmlArray("races")]
        [XmlArrayItem("item")]
        [DataMember(Name="races")]
        public List<int> Races { get; set; }

        [XmlElement("achievement")]
        [DataMember(Name="achievement")]
        public Achievement Achievement { get; set; }

        /// <summary>
        /// This seems to be the 'more' correct item instead of the
        /// one on the achievement
        /// </summary>
        [XmlElement("item")]
        [DataMember(Name="item")]
        public Item Item { get; set; }
    }

    [DataContract]
    public class Achievement
    {
        [XmlElement("id")]
        [DataMember(Name="id")]
        public int Id { get; set; }
        
        [XmlElement("title")]
        [DataMember(Name="title")]
        public string Title { get; set; }
        
        [XmlElement("points")]
        [DataMember(Name="points")]
        public int Points { get; set; }
        
        [XmlElement("description")]
        [DataMember(Name="description")]
        public string Description { get; set; }
        
        [XmlElement("reward")]
        [DataMember(Name="reward")]
        public string Reward { get; set; }
        
        [XmlElement("rewardItem")]
        [DataMember(Name="rewardItem")]
        public Item RewardItem { get; set; }
    }
}
