using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNet.API.WoW
{
    public enum TeamSize
    {
        Team2v2,
        Team3v3,
        Team5v5,
    }

    public class ArenaTeamQuery : QueryBase
    {
        public TeamSize TeamSize { get; set; }
        public string Realm { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            if (Realm == null || Realm.Trim() == "") throw new ArgumentNullException("Realm");
            if (Name == null || Name.Trim() == "") throw new ArgumentNullException("Name");

            string size = "";
            switch(TeamSize)
            {
                case WoW.TeamSize.Team2v2: size = "2v2"; break;
                case WoW.TeamSize.Team3v3: size = "3v3"; break;
                case WoW.TeamSize.Team5v5: size = "5v5"; break;
            }

            return "arena/" + Encode(Realm) + "/" + size + "/"+ Encode(Name) + "?" + base.ToString();            
        }

    }
}
