using System;
using System.Collections.Generic;
using System.Text;

using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace BattleNet.API.WoW
{
    [DataContract]
    public class Quest : ResponseRoot
    {        
        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name="title")]
        public string Title { get; set; }

        [DataMember(Name = "reqLevel")]
        public int RequiredLevel { get; set; }

        [DataMember(Name = "suggestedPartyMembers")]
        public int SuggestedPartyMembers { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }
    }
}
