using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNet.API.WoW
{
    public class AuctionQuery : QueryBase
    {
        public string Realm { get; set; }
        public override string ToString()
        {
            if (Realm == null || Realm.Trim() == "") throw new ArgumentNullException("Realm");

            return "auction/data/" + Encode(Realm) + base.ToString();
        }
    }

    public class RacesQuery : QueryBase
    {
        public override string ToString()
        {
            return "data/character/races" + base.ToString();
        }
    }

    public class ClassesQuery : QueryBase
    {
        public override string ToString()
        {
            return "data/character/classes" + base.ToString();
        }
    }

    public class GuildRewardsQuery : QueryBase
    {
        public override string ToString()
        {
            return "data/guild/rewards" + base.ToString();
        }
    }
    public class GuildPerksQuery : QueryBase
    {
        public override string ToString()
        {
            return "data/guild/perks" + base.ToString();
        }
    }

    public class ItemQuery : QueryBase
    {
        public int ItemId { get; set; }
        public override string ToString()
        {
            if (ItemId <= 0) throw new ArgumentException("ItemId must be a positive number");

            return "item/" + ItemId + base.ToString();
        }
    }

}
