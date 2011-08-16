using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace BattleNet.API.WoW
{
    [Flags]
    public enum CharacterFields
    {
        Basic = 0,
        Stats,
        Talents,
        Items,
        Reputation,
        Titles,
        Professions,
        Appearance,
        Companions,
        Mounts,
        Pets,
        Achievements,
        Progression,
        Guild,
        PVP,
        Quests,

        All = Guild | Progression | Achievements | Pets | Mounts | Companions | PVP |
              Appearance | Professions | Titles | Reputation | Items | Talents | Stats|
              Quests,
    }

    public class CharacterQuery : QueryBase
    {
        public string Realm { get; set; }
        public string Name { get; set; }

        public CharacterFields Fields { get; set; }

        public override string ToString()
        {
            if (Realm == null || Realm.Trim() == "") throw new ArgumentNullException("Realm");
            if (Name == null || Name.Trim() == "") throw new ArgumentNullException("Name");

            return "character/" + Encode(Realm) + "/" + Encode(Name) + "?" + base.ToString();            
        }

        override protected void BuildQuery(IDictionary<string, string> query)
        {
            List<string> args = new List<string>();
            if ((Fields & CharacterFields.Achievements) == CharacterFields.Achievements) args.Add("achievements");
            if ((Fields & CharacterFields.Appearance) == CharacterFields.Appearance) args.Add("appearance");
            if ((Fields & CharacterFields.Companions) == CharacterFields.Companions) args.Add("companions");
            if ((Fields & CharacterFields.Items) == CharacterFields.Items) args.Add("items");
            if ((Fields & CharacterFields.Mounts) == CharacterFields.Mounts) args.Add("mounts");
            if ((Fields & CharacterFields.Pets) == CharacterFields.Pets) args.Add("pets");
            if ((Fields & CharacterFields.Professions) == CharacterFields.Professions) args.Add("professions");
            if ((Fields & CharacterFields.Progression) == CharacterFields.Progression) args.Add("Progression");
            if ((Fields & CharacterFields.Reputation) == CharacterFields.Reputation) args.Add("reputation");
            if ((Fields & CharacterFields.Stats) == CharacterFields.Stats) args.Add("stats");
            if ((Fields & CharacterFields.Talents) == CharacterFields.Talents) args.Add("talents");
            if ((Fields & CharacterFields.Titles) == CharacterFields.Titles) args.Add("titles");
            if ((Fields & CharacterFields.Guild) == CharacterFields.Guild) args.Add("guild");
            if ((Fields & CharacterFields.PVP) == CharacterFields.Guild) args.Add("pvp");
            if ((Fields & CharacterFields.Quests) == CharacterFields.Quests) args.Add("quests");
            

            string _f = string.Join(",", args.ToArray());

            query.Add("fields", _f);

            base.BuildQuery(query);
        }
    }
}
