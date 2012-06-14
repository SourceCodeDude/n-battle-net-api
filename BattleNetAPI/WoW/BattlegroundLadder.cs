using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace BattleNet.API.WoW
{
    public class BattlegroundLadderQuery : QueryBase
    {
        public override string ToString()
        {
            return "pvp/ratedbg/ladder?" + base.ToString();
        }
    }

    [DataContract]
    public class BattlegroundLadder : ResponseRoot
    {
        [DataMember(Name = "bgRecord")]
        public List<BattlegroundRecord> BgRecord { get; set; }
    }
    public class BattlegroundRecord
    {
        [DataMember(Name = "rank")]
        public int Rank{get;set;}
        [DataMember(Name = "bgRating")]
        public int BgRating { get; set; }
        [DataMember(Name = "wins")]
        public int Wins { get; set; }
        [DataMember(Name = "losses")]
        public int Losses { get; set; }
        [DataMember(Name = "played")]
        public int Played { get; set; }

        [DataMember(Name = "realm")]
        public Realm Realm { get; set; }

        [DataMember(Name = "battlegroup")]
        public Battlegroup Battlegroup { get; set; }

        [DataMember(Name = "character")]
        public Character Character { get; set; }
                     
        public UnixTimestamp LastModified { get; set; }
        
        [DataMember(Name = "lastModified")]
        private long lastModified
        {
            get { return LastModified.ToMsec(); }
            set{
                LastModified = new UnixTimestamp(value);
            }
        }
    }

    public class Battlegroup
    {
        [DataMember(Name = "name")]
        public string Name{get;set;}
        [DataMember(Name = "slug")]
        public string Slug{get;set;}

    }
}
