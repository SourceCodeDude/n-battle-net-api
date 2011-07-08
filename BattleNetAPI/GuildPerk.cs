using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    public class GuildPerkCollection
    {
        [XmlArray("perks")]
        [XmlArrayItem("item")]
        public List<GuildPerk> Perks { get; set; }
    }

    public class GuildPerk
    {
        public int guildLevel { get; set; }
        public Spell spell { get; set; }

    }
}
