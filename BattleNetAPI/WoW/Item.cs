using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace BattleNet.API.WoW
{
    [DataContract]
    public class ItemClassCollection : ResponseRoot
    {
        [XmlArray("classes")]
        [XmlArrayItem("item")]
        [DataMember(Name = "classes")]
        public List<Item.Class> Classes { get; set; }
    }

    [DataContract]
    public class Item : ResponseRoot
    {
        [DataContract]
        public class Class
        {
            [XmlElement("class")]
            [DataMember(Name="class")]
            public int Id { get; set; }

            [XmlElement("name")]
            [DataMember(Name="name")]
            public string Name { get; set; }

            public override string ToString()
            {
                return this.Name;
            }
        }

        [XmlElement("id")]
        [DataMember(Name="id")]
        public int Id { get; set; }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlElement("icon")]
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [XmlElement("quality")]
        [DataMember(Name = "quality")]
        public Quality Quality { get; set; }

        [XmlElement("tooltipParams")]
        [DataMember(Name = "tooltipParams")]
        public ItemToolTip TooltipParams { get; set; }

        // the rest are on the data api

        [XmlElement("disenchantingSkillRank")]
        [DataMember(Name = "disenchantingSkillRank")]
        public int DisenchantingSkillRank { get; set; }

        [XmlElement("description")]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [XmlElement("stackable")]
        [DataMember(Name = "stackable")]
        public int Stackable { get; set; }

        /// <summary>
        /// Does the item BIND on pickup / use / equip/ none
        /// 0 none
        /// 1 = BOP
        /// 2 = BOE/// 3 = BOU
        /// 4 = Quest
        /// </summary>
        [XmlElement("itemBind")]
        [DataMember(Name="itemBind")]
        public int ItemBind { get; set; }

        [XmlArray("bonusStats")]
        [XmlArrayItem("item")]
        [DataMember(Name = "bonusStats")]
        public List<BonusStat> BonusStats { get; set; }

        [XmlArray("itemSpells")]
        [XmlArrayItem("item")]
        [DataMember(Name = "itemSpells")]
        public List<ItemSpell> ItemSpells { get; set; }
        
        [XmlElement("buyPrice")]
        [DataMember(Name = "buyPrice")]
        public int BuyPrice { get; set; }
        
        [XmlElement("itemClass")]
        [DataMember(Name = "itemClass")]
        public ItemClass ItemClass { get; set; }
        
        [XmlElement("itemSubClass")]
        [DataMember(Name = "itemSubClass")]
        public int ItemSubClass { get; set; }
        
        [XmlElement("containerSlots")]
        [DataMember(Name = "containerSlots")]
        public int ContainerSlots { get; set; }
        
        [XmlElement("weaponInfo")]
        [DataMember(Name = "weaponInfo")]
        public WeaponInfo WeaponInfo { get; set; }
        
        [XmlElement("inventoryType")]
        [DataMember(Name = "inventoryType")]
        public InventoryType InventoryType { get; set; }
        
        [XmlElement("equippable")]
        [DataMember(Name = "equippable")]
        public bool Equippable { get; set; }
        
        [XmlElement("itemLevel")]
        [DataMember(Name = "itemLevel")]
        public int ItemLevel { get; set; }
        
        [XmlElement("maxCount")]
        [DataMember(Name = "maxCount")]
        public int MaxCount { get; set; }
        
        [XmlElement("maxDurability")]
        [DataMember(Name = "maxDurability")]
        public int MaxDurability { get; set; }
        
        [XmlElement("minFactionId")]
        [DataMember(Name = "minFactionId")]
        public int MinFactionId { get; set; }
        
        [XmlElement("minReputation")]
        [DataMember(Name = "minReputation")]
        public int MinReputation { get; set; }
        
        [XmlElement("sellPrice")]
        [DataMember(Name = "sellPrice")]
        public int SellPrice { get; set; }
        
        [XmlElement("requiredLevel")]
        [DataMember(Name = "requiredLevel")]
        public int RequiredLevel { get; set; }
        
        [XmlElement("requiredSkill")]
        [DataMember(Name = "requiredSkill")]
        public int RequiredSkill { get; set; }
        
        [XmlElement("requiredSkillRank")]
        [DataMember(Name = "requiredSkillRank")]
        public int RequiredSkillRank { get; set; }
        
        [XmlElement("itemSource")]
        [DataMember(Name = "itemSource")]
        public ItemSource ItemSource { get; set; }
       
        [XmlElement("baseArmor")]
        [DataMember(Name = "baseArmor")]
        public int BaseArmor { get; set; }
        
        [XmlElement("hasSockets")]
        [DataMember(Name = "hasSockets")]
        public bool HasSockets { get; set; }
        
        [XmlElement("isAuctionable")]
        [DataMember(Name = "isAuctionable")]
        public bool IsAuctionable { get; set; }
    }
    
    public enum SourceType
    {
        NONE,
        VENDOR,
        GAME_OBJECT_DROP,
        CREATED_BY_SPELL,
        REWARD_FOR_QUEST,
        // TODO: complete this list when blizzard does
    }

    [DataContract]
    public class ItemSource
    {
        [XmlElement("sourceId")]
        [DataMember(Name = "sourceId")]
        public int SourceId { get; set; }    
    
        [XmlElement("sourceType")]        
        public SourceType SourceType { get; set; }

        [DataMember(Name = "sourceType")]
        private string sourceType
        {
            get
            {
                return SourceType.ToString().ToUpper();
            }
            set
            {
                SourceType = (SourceType)Enum.Parse(typeof(SourceType), value, true);
            }
        }
    }

    [DataContract]
    public class WeaponInfo
    {
        [XmlArray("damage")]        
        [XmlArrayItem("item")]
        [DataMember(Name = "damage")]
        public List<DamageRange> Damage { get; set; }
        
        [XmlElement("weaponSpeed")]
        [DataMember(Name = "weaponSpeed")]
        public float WeaponSpeed { get; set; }
        
        [XmlElement("dps")]
        [DataMember(Name = "dps")]
        public float DPS { get; set; }
    }

    [DataContract]
    public class DamageRange
    {
        [XmlElement("minDamage")]
        [DataMember(Name = "minDamage")]

        public int MinDamage { get; set; }
        [XmlElement("maxDamage")]
        [DataMember(Name = "maxDamage")]
        public int MaxDamage { get; set; }
    }

    [DataContract]
    public enum ItemBind
    {        
        None = 0,
        OnUse = 1,
        OnEquipped = 2
    }

    [DataContract]
    public class BonusStat
    {        
        [XmlElement("stat")]
        [DataMember(Name="stat")]
        public int Stat { get; set; }

        [XmlElement("amount")]
        [DataMember(Name = "amount")]
        public int Amount { get; set; }

        [XmlElement("reforged")]
        [DataMember(Name = "reforged")]
        public bool Reforged { get; set; }
    }

    /*
       "itemSpells":[
    {
      "spellId":434,
      "spell":{
        "id":434,
        "name":"Food",
        "icon":"inv_misc_fork&knife",
        "description":"Restores 232 health over 21 sec.  Must remain seated while eating.",
        "castTime":"Instant cast",
        "cooldown":"1 min cooldown"
      },
      "nCharges":1,
      "consumable":true,
      "categoryId":11  <- what does that map to
    }
  ],
     */
    [DataContract]
    public class ItemSpell
    {
        [XmlElement("spellId")]
        [DataMember(Name="spellId")]
        public int SpellId { get; set; }
        
        [XmlElement("spell")]
        [DataMember(Name = "spell")]
        public Spell Spell { get; set; }

        [XmlElement("nCharges")]
        [DataMember(Name = "nCharges")]
        public int Charges { get; set; }

        [XmlElement("consumable")]
        [DataMember(Name = "consumable")]
        public bool Consumable { get; set; }

        /// <summary>
        /// Need to build an enum of the ids
        /// </summary>
        [XmlElement("categoryId")]
        [DataMember(Name = "categoryId")]
        public int CategoryId { get; set; }
    }

    [DataContract]
    public enum Quality
    {
        [XmlEnum("0")]
        Poor = 0,    // Gray

        [XmlEnum("1")]
        Common = 1,  // White

        [XmlEnum("2")]
        Uncommon =2, // Green

        [XmlEnum("3")]
        Rare    = 3, // Blue
        
        [XmlEnum("4")]
        Epic    = 4, // Purple

        [XmlEnum("5")]
        Legendary=5, // Orange

        [XmlEnum("6")]
        Artifact=6,  // Red
    }

    
    /// <summary>
    /// From https://us.battle.net/api/wow/data/item/classes
    /// you can also use BattleNetClient.Classes
    /// </summary>
    public enum ItemClass
    {
        [XmlEnum("0")]
        Consumable =0,

        [XmlEnum("1")]
        Container =1,

        [XmlEnum("2")]
        Weapon = 2,

        [XmlEnum("3")]
        Gem = 3,

        [XmlEnum("4")]
        Armor = 4,

        [XmlEnum("5")]
        Reagent = 5,

        [XmlEnum("6")]
        Projectile = 6,

        [XmlEnum("7")]
        TradeGoods = 7,

        // no 8

        [XmlEnum("9")]
        Recipe = 9,

        // no 10

        [XmlEnum("11")]
        Quiver = 11,

        [XmlEnum("12")]
        Quest = 12,

        [XmlEnum("13")]
        Key = 13,

        // no 14

        [XmlEnum("15")]
        Miscellaneous = 15,

        [XmlEnum("16")]
        Glyph = 16,
    }

    /// <summary>
    /// Slot where item can be equipped
    /// </summary>
    [DataContract]
    public enum InventoryType
    {
        [XmlEnum("0")]
        None=0,
        [XmlEnum("1")]
        Head =1,
        [XmlEnum("2")]
        Neck =2,
        [XmlEnum("3")]
        Sholders =3,
        [XmlEnum("4")]
        Shirt =4,
        [XmlEnum("5")]
        Chest =5,
        [XmlEnum("6")]
        Waist =6,
        [XmlEnum("7")]
        Legs =7,
        [XmlEnum("8")]
        Feet =8,
        [XmlEnum("9")]
        Wrists =9,
        [XmlEnum("10")]
        Hands =10,
        [XmlEnum("11")]
        Finger =11,
        [XmlEnum("12")]
        Trinket =12,
        [XmlEnum("13")]
        OneHand =13,
        [XmlEnum("14")]
        OffHand =14,
        [XmlEnum("15")]
        Bow =15,
        [XmlEnum("16")]
        Back =16,
        [XmlEnum("17")]
        TwoHand =17,
        [XmlEnum("18")]
        Bag =18,
        [XmlEnum("19")]
        Tabard =19,
        [XmlEnum("20")]
        Robe =20,
        [XmlEnum("21")]
        MainHand =21,
        [XmlEnum("22")]
        OffHandMisc =22,
        [XmlEnum("23")]
        Tome =23,
        [XmlEnum("24")]
        Ammunition =24,
        [XmlEnum("25")]
        Thrown =25,
        [XmlEnum("26")]
        Gun =26,

    }
}
