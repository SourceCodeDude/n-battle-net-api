using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNet.API.WoW
{
    public class ArenaTeamLadderQuery : QueryBase
    {
        public TeamSize TeamSize { get; set; }
        public string BattleGroup { get; set; }

        public int Page { get; set; }
        public int Size { get; set; }
        public bool Asc { get; set; }
        public override string ToString()
        {
            if (BattleGroup == null || BattleGroup.Trim() == "") throw new ArgumentNullException("BattleGroup");

            string size = "";
            switch (TeamSize)
            {
                case WoW.TeamSize.Team2v2: size = "2v2"; break;
                case WoW.TeamSize.Team3v3: size = "3v3"; break;
                case WoW.TeamSize.Team5v5: size = "5v5"; break;
            }

            return "pvp/arena/" + Encode(BattleGroup) + "/" + size + "?" + base.ToString();
        }

    }
}
