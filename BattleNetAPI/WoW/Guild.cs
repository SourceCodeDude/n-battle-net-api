using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace BattleNet.API.WoW
{

    [DataContract]
    public class Guild : ResponseRoot
    {
        #region Basic Fields

        [XmlElement("lastModified")]            
        public UnixTimestamp LastModified { get; set; }

        [DataMember(Name = "lastModified")] 
        internal long lastModified
        {
            get
            {
                return LastModified.ToMsec();
            }
            set
            {
                LastModified = new UnixTimestamp(value);
            }
        }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlElement("realm")]
        [DataMember(Name = "realm")]
        public string Realm { get; set; }
        
        [XmlElement("level")]
        [DataMember(Name = "level")]
        public int Level { get; set; }

        /// <summary>
        /// 0 = alliance
        /// 1 = horde
        /// </summary>
        [XmlElement("side")]
        [DataMember(Name = "side")]
        public int _side
        {
            get
            {
                if (Side == WoW.Side.Alliance) return 0;
                if (Side == WoW.Side.Horde) return 1;
                // unknown
                return -1;
            }
            set
            {
                if (value == 0) Side = WoW.Side.Alliance;
                if (value == 1) Side = WoW.Side.Horde;
            }
        }

        [XmlElement("emblem")]
        [DataMember(Name = "emblem")]
        public Emblem Emblem { get; set; }

        [XmlIgnore]
        public Side Side { get; set; }

        [XmlElement("achievementPoints")]
        [DataMember(Name = "achievementPoints")]
        public int AchievementPoints { get; set; }
        #endregion

        /////////////////////////////////////////////////        
        // Optional fields
        ////////////////////////////////////////////////

        #region optional fields

        [XmlArray("members", IsNullable=true)]
        [XmlArrayItem("item")]
        [DataMember(Name="members")]
        public List<Member> Members { get; set; }

        [XmlElement("achievements", IsNullable = true)]
        [DataMember(Name = "achievements")]
        public AchievementProgression Achievements { get; set; }

        [DataMember(Name = "News")]
        public List<GuildEvent> News { get; set; }

        #endregion
    }

    [DataContract]
    public abstract class GuildEvent
    {
        [DataMember(Name="type")]
        public string Type { get; set; }

        [XmlElement("timestamp")]
        public UnixTimestamp Timestamp { get; set; }

        [DataMember(Name = "timestamp")]
        internal long timestamp
        {
            get
            {
                return Timestamp.ToMsec();
            }
            set
            {
                Timestamp = new UnixTimestamp(value);
            }
        }

    }

    [DataContract]
    public class GuildCreatedEvent : GuildEvent
    {
    };

    [DataContract]
    public class ItemLootedEvent : GuildEvent
    {
        [DataMember(Name = "character")]
        public string Character { get; set; }

        [DataMember(Name = "itemId")]
        public int ItemId { get; set; }
    };

    [DataContract]
    public class ItemPurchasedEvent : GuildEvent
    {
        [DataMember(Name = "character")]
        public string Character { get; set; }

        [DataMember(Name = "itemId")]
        public int ItemId { get; set; }
    };

    [DataContract]
    public class GuildLevelNews : GuildEvent
    {
        [DataMember(Name = "levelUp")]
        public int LevelUp { get; set; }
    };

    [DataContract]
    public class GuildAchievementNews : GuildEvent
    {
        /// <summary>
        /// Though there is a Character field here, it isn't used on the
        /// Armory site,  it just says "The guild earned the achievement..."
        /// </summary>
        [DataMember(Name = "character")]
        public string Character { get; set; }

        [DataMember(Name = "achievement")]
        public Achievement Achievement { get; set; }
    };

    [DataContract]
    public class PlayerdAchievementNews : GuildEvent
    {
        [DataMember(Name = "character")]
        public string Character { get; set; }

        [DataMember(Name = "achievement")]
        public Achievement Achievement { get; set; }
    };

    [DataContract]
    public class Member
    {
        [XmlElement("character")]
        [DataMember(Name = "character")]
        public Character Character { get; set; }

        [XmlElement("rank")]
        [DataMember(Name = "rank")]
        public int Rank { get; set; }

        public override string ToString()
        {
            return "{" + this.Character.Name + " " + Rank + "}";
        }
    }

}
