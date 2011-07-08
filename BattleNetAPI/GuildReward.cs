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
        public int minGuildLevel { get; set; }
        public int minGuildRepLevel { get; set; }

        [XmlArray("races")]
        [XmlArrayItem("item")]
        public List<int> races { get; set; }

        public Achievement achievement { get; set; }

        /// <summary>
        /// This seems to be the 'more' correct item instead of the
        /// one on the achievement
        /// </summary>
        public Item item { get; set; }
    }

    public class Achievement
    {
        public int id { get; set; }
        public string title { get; set; }
        public int points { get; set; }
        public string description { get; set; }
        public string reward { get; set; }
        public Item rewardItem { get; set; }

    }
}
