using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace BattleNet.API.WoW
{
    [DataContract]
    public class ArenaTeamLadder : ResponseRoot
    {
        [DataMember(Name = "arenateam")]
        public List<ArenaTeam> Teams { get; set; }
    }
}
