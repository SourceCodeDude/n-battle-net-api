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
        [DataMember(Name="members")]
        public List<Member> Members { get; set; }

        [XmlElement("achievements", IsNullable = true)] public Achievements Achievements { get; set; }
        
        #endregion
    }

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
