using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNet.API.WoW
{
    public class RealmQuery : QueryBase
    {
        public IEnumerable<string> Realms { get; set; }

        public override string ToString()
        {
            return "realm/status?" + base.ToString();
        }

        protected override void BuildQuery(System.Collections.Specialized.NameValueCollection query)
        {
            if (Realms!=null)
            {
                query.Add("realms", string.Join(",", Realms));
            }
            base.BuildQuery(query);
        }
    }
}
