using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    public class Item : ResponseRoot
    {
        [XmlElement("id")]              public int Id { get; set; }
        [XmlElement("name")]            public string Name { get; set; }
        [XmlElement("icon")]            public string Icon { get; set; }
        [XmlElement("quality")]         public int Quality { get; set; }
        [XmlElement("tooltipParams")]   public ItemToolTip TooltipParams { get; set; }

        // the rest are on the data api

        [XmlElement("disenchantingSkillRank")]  public int disenchantingSkillRank { get; set; }
        [XmlElement("description")]             public string description { get; set; }
        [XmlElement("stackable")]               public int stackable { get; set; }

        // not sure what this is...
        //"itemBind":{"id":0},

        [XmlArray("bonusStats")]
        [XmlArrayItem("item")]              public List<object> BonusStats { get; set; }

        [XmlArray("itemSpells")]
        [XmlArrayItem("item")]              public List<object> ItemSpells { get; set; }
        [XmlElement("buyPrice")]            public int BuyPrice { get; set; }
        [XmlElement("itemClass")]           public ItemClass ItemClass { get; set; }
        [XmlElement("itemSubClass")]        public ItemSubClass ItemSubClass { get; set; }
        [XmlElement("containerSlots")]      public int ContainerSlots { get; set; }
        [XmlElement("weaponInfo")]          public WeaponInfo WeaponInfo { get; set; }
        [XmlElement("inventoryType")]       public int InventoryType { get; set; }
        [XmlElement("equippable")]          public bool Equippable { get; set; }
        [XmlElement("itemLevel")]           public int ItemLevel { get; set; }
        [XmlElement("maxCount")]            public int MaxCount { get; set; }
        [XmlElement("maxDurability")]       public int MaxDurability { get; set; }
        [XmlElement("minFactionId")]        public int MinFactionId { get; set; }
        [XmlElement("minReputation")]       public int MinReputation { get; set; }
        [XmlElement("sellPrice")]           public int SellPrice { get; set; }
        [XmlElement("requiredLevel")]       public int RequiredLevel { get; set; }
        [XmlElement("requiredSkill")]       public int RequiredSkill { get; set; }
        [XmlElement("requiredSkillRank")]   public int RequiredSkillRank { get; set; }
        [XmlElement("itemSource")]          public ItemSource ItemSource { get; set; }
        [XmlElement("baseArmor")]           public int BaseArmor { get; set; }
        [XmlElement("hasSockets")]          public bool HasSockets { get; set; }
        [XmlElement("isAuctionable")]       public bool IsAuctionable { get; set; }
    }

    public enum SourceType
    {
        NONE,
        // TODO: complete this list when blizzard does
    }
    public class ItemSource
    {
        [XmlElement("sourceId")]    public int SourceId { get; set; }        
        [XmlElement("sourceType")]  public SourceType SourceType { get; set; }
    }

    public class WeaponInfo
    {
        [XmlElement("damage")]      public DamageRange Damage { get; set; }
        [XmlElement("weaponSpeed")] public float weaponSpeed { get; set; }
        [XmlElement("dps")]         public float dps { get; set; }
    }

    public class DamageRange
    {
        [XmlElement("minDamage")]   public int MinDamage { get; set; }
        [XmlElement("maxDamage")]   public int MaxDamage { get; set; }
    }

    public class ItemClass
    {
        [XmlElement("class")]       public int Class{get;set;}
        [XmlElement("name")]        public string Name{get;set;}
    }
    public class ItemSubClass
    {
        [XmlElement("subclassId")]  public int subclassId { get; set; }
        [XmlElement("name")]        public string name { get; set; }
    }
}
