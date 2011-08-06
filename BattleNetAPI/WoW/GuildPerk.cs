using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace BattleNet.API.WoW
{
    [DataContract]
    public class GuildPerkCollection
    {
        [XmlArray("perks")]
        [XmlArrayItem("item")]
        [DataMember(Name="perks")]
        public List<GuildPerk> Perks { get; set; }
    }

    [DataContract]
    public class GuildPerk
    {
        [XmlElement("guildLevel")]
        [DataMember(Name="guildLevel")]
        public int GuildLevel { get; set; }

        [XmlElement("spell")]
        [DataMember(Name="spell")]
        public Spell Spell { get; set; }

    }
}
