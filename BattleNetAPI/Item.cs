using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    public class Item : ResponseRoot
    {
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int quality { get; set; }

        public ItemToolTip tooltipParams { get; set; }

        // the rest are on the data api

        public int disenchantingSkillRank { get; set; }
        public string description { get; set; }
        public int stackable { get; set; }

        // not sure what this is...
        //"itemBind":{"id":0},

        [XmlArray("bonusStats")]
        [XmlArrayItem("item")]
        public List<object> BonusStats { get; set; }


        [XmlArray("itemSpells")]
        [XmlArrayItem("item")]
        public List<object> ItemSpells { get; set; }
        public int buyPrice { get; set; }
        public ItemClass itemClass { get; set; }
        public ItemSubClass itemSubClass { get; set; }
        public int containerSlots { get; set; }
        public WeaponInfo weaponInfo { get; set; }

        public int inventoryType { get; set; }
        public bool equippable { get; set; }
        public int itemLevel { get; set; }
        public int maxCount { get; set; }
        public int maxDurability { get; set; }
        public int minFactionId { get; set; }
        public int minReputation { get; set; }        
        public int sellPrice { get; set; }
        public int requiredLevel { get; set; }
        public int requiredSkill { get; set; }
        public int requiredSkillRank { get; set; }

        public ItemSource itemSource { get; set; }

        public int baseArmor { get; set; }
        public bool hasSockets { get; set; }
        public bool isAuctionable { get; set; }
    }

    public class ItemSource
    {
        public int sourceId { get; set; }
        // TODO: change this to an ENUM  "NONE", 
        public string sourceType { get; set; }
    }

    public class WeaponInfo
    {
        public DamageRange Damage { get; set; }
        public float weaponSpeed { get; set; }
        public float dps { get; set; }
    }

    public class DamageRange
    {
        public int minDamage { get; set; }
        public int maxDamage { get; set; }
    }

    public class ItemClass
    {
        public int @class{get;set;}
        public string name{get;set;}
    }
    public class ItemSubClass
    {
        public int subclassId { get; set; }        
        public string name { get; set; }
    }
}
