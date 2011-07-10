using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    public class GuildRewardCollection
    {
        [XmlArray("rewards")]
        [XmlArrayItem("item")]
        public List<GuildReward> Rewards { get; set; }
    }

    public class GuildReward
    {
        [XmlElement("minGuildLevel")]       public int MinGuildLevel { get; set; }
        [XmlElement("minGuildRepLevel")]    public int MinGuildRepLevel { get; set; }

        [XmlArray("races")]
        [XmlArrayItem("item")]              public List<int> Races { get; set; }

        [XmlElement("achievement")]         public Achievement Achievement { get; set; }

        /// <summary>
        /// This seems to be the 'more' correct item instead of the
        /// one on the achievement
        /// </summary>
        [XmlElement("item")]                public Item Item { get; set; }
    }

    public class Achievement
    {
        [XmlElement("id")]          public int Id { get; set; }
        [XmlElement("title")]       public string Title { get; set; }
        [XmlElement("points")]      public int Points { get; set; }
        [XmlElement("description")] public string Description { get; set; }
        [XmlElement("reward")]      public string Reward { get; set; }
        [XmlElement("rewardItem")]  public Item RewardItem { get; set; }
    }
}
