using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNet.API.WoW
{
    public class QuestQuery : QueryBase
    {
        public int Id { get; set; }

        public override string ToString()
        {            
            return "quest/" + Id + "?" + base.ToString();
        }
    }
}
