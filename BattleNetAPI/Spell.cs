using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNet.API.WoW
{
    public class Spell
    {
        public int id { get; set; }
        public string name { get; set; }
        public string subtext { get; set; }
        public string icon { get; set; }
        public string description { get; set; }

        public string range { get; set; }
        public int castTime { get; set; }
        public string cooldown { get; set; }
    }
}
