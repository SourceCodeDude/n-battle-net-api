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
    public class ArenaTeam : ResponseRoot
    {
        [XmlElement("realm")]
        [DataMember(Name = "realm")]
        public string Realm { get; set; }

        [XmlElement("ranking")]
        [DataMember(Name = "ranking")]
        public int Ranking { get; set; }

        [XmlElement("rating")]
        [DataMember(Name = "rating")]
        public int Rating { get; set; }

        [XmlElement("teamsize")]
        [DataMember(Name = "teamsize")]
        public int TeamSize { get; set; }

        [XmlElement("created")]
        [DataMember(Name = "created")]
        public string _created {
            get
            {
                return Created.ToString("yyyy-MM-dd");
            }
            set
            {
                Created = DateTime.Parse(value);
            }
        }

        [XmlIgnore]
        public DateTime Created{get;set;}

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlElement("gamesPlayed")]
        [DataMember(Name = "gamesPlayed")]
        public string GamesPlayed { get; set; }

        [XmlElement("gamesWon")]
        [DataMember(Name = "gamesWon")]
        public string GamesWon { get; set; }

        [XmlElement("gamesLost")]
        [DataMember(Name = "gamesLost")]
        public string GamesLost { get; set; }

        [XmlElement("sessionGamesPlayed")]
        [DataMember(Name = "sessionGamesPlayed")]
        public string SessionGamesPlayed { get; set; }

        [XmlElement("sessionGamesWon")]
        [DataMember(Name = "sessionGamesWon")]
        public string SessionGamesWon { get; set; }

        [XmlElement("sessionGamesLost")]
        [DataMember(Name = "sessionGamesLost")]
        public string SessionGamesLost { get; set; }


        [XmlElement("lastSessionRanking")]
        [DataMember(Name = "lastSessionRanking")]
        public int LastSessionRanking { get; set; }

        [XmlElement("side")]
        [DataMember(Name="side")]
        public Side Side { get; set; }

        [XmlElement("currentWeekRanking")]
        [DataMember(Name = "currentWeekRanking")]
        public int CurrentWeekRanking { get; set; }

        [XmlArray("members")]
        [XmlArrayItem("item")]
        [DataMember(Name = "members")]
        public List<ArenaTeamMember> Members { get; set; }
    }

    [DataContract]
    public class ArenaTeamMember
    {
        [XmlElement("character")]
        [DataMember(Name="character")]
        public Character Character { get; set; }

        [XmlElement("rank")]
        [DataMember(Name="rank")]
        public int Rank { get; set; }

        [XmlElement("gamesPlayed")]
        [DataMember(Name="gamesPlayed")]
        public int GamesPlayed { get; set; }

        [XmlElement("gamesWon")]
        [DataMember(Name="gamesWon")]
        public int GamesWon { get; set; }

        [XmlElement("gamesLost")]
        [DataMember(Name="gamesLost")]
        public int GamesLost { get; set; }

        [XmlElement("sessionGamesPlayed")]
        [DataMember(Name="sessionGamesPlayed")]
        public int SessionGamesPlayed { get; set; }

        [XmlElement("sessionGamesWon")]
        [DataMember(Name="sessionGamesWon")]
        public int SessionGamesWon { get; set; }

        [XmlElement("sessionGamesLost")]
        [DataMember(Name="sessionGamesLost")]
        public int SessionGamesLost { get; set; }

        [XmlElement("personalRating")]
        [DataMember(Name="personalRating")]
        public int PersonalRating { get; set; }
    }
}
