using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
namespace BattleNet.API.WoW
{
    public class Guild : ResponseRoot
    {
        [Flags]
        public enum Fields
        {
            Basic = 0,
            Members,
            Achievements,  
          
            All = Members | Achievements,
        }

        #region Basic Fields

        [XmlElement("lastModified")]public UnixTimestamp LastModified { get; set; }
        [XmlElement("name")]        public string Name { get; set; }
        [XmlElement("realm")]       public string Realm { get; set; }
        [XmlElement("level")]       public int Level { get; set; }
        /// <summary>
        /// 0 = alliance
        /// 1 = horde
        /// </summary>
        [XmlElement("side")]                public int Side { get; set; }
        [XmlElement("achievementPoints")]   public int AchievementPoints { get; set; }
        #endregion

        /////////////////////////////////////////////////        
        // Optional fields
        ////////////////////////////////////////////////

        #region optional fields

        [XmlArray("members", IsNullable=true)]
        [XmlArrayItem("item")]
        public List<Member> Members { get; set; }

        [XmlElement("achievements", IsNullable = true)] public Achievements Achievements { get; set; }
        
        #endregion
    }

    public class Member
    {
        [XmlElement("character")]   public Character Charcater { get; set; }
        [XmlElement("rank")]        public int Rank { get; set; }
    }

}
