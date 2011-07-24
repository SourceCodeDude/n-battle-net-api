using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BattleNet.API.WoW
{
    [Flags]
    public enum GuildFields
    {
        Basic = 0,
        Members,
        Achievements,

        All = Members | Achievements,
    }

    public class GuildQuery : QueryBase
    {
        public GuildFields Fields { get; set; }
        public string Realm { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            if (Realm == null || Realm.Trim() == "") throw new ArgumentNullException("Realm");
            if (Name == null || Name.Trim() == "") throw new ArgumentNullException("Name");
            return "guild/" + Encode(Realm) + "/" + Encode(Name) + "?" + base.ToString();
        }

        override protected void BuildQuery(System.Collections.Specialized.NameValueCollection query)
        {
            List<string> args = new List<string>();
            if ((Fields & GuildFields.Achievements) == GuildFields.Achievements) args.Add("achievements");
            if ((Fields & GuildFields.Members) == GuildFields.Members) args.Add("members");

            string _f = string.Join(",", args.ToArray());

            query.Add("fields", _f);

            base.BuildQuery(query);
        }
    }
}
