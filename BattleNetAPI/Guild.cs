using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
namespace BattleNet.API.WoW
{
    public class Guild
    {
        [Flags]
        public enum Fields
        {
            Basic = 0,
            Members,
            Achievements,            
        }

        #region Basic Fields
        public UnixTimestamp lastModified { get; set; }
        public string name { get; set; }
        public string realm { get; set; }
        public int level { get; set; }
        /// <summary>
        /// 0 = alliance
        /// 1 = horde
        /// </summary>
        public int side { get; set; }
        public int achievementPoints { get; set; }
        #endregion

        /////////////////////////////////////////////////        
        // Optional fields
        ////////////////////////////////////////////////

        #region optional fields

       
        [XmlArray("members")]
        [XmlArrayItem("item")]
        public List<Member> Members { get; set; }

        [XmlElement("achievements")]
        public Achievements Achievements { get; set; }
        #endregion
    }

    public class Member
    {
        public Character charcater { get; set; }
        public int rank { get; set; }
    }

}
