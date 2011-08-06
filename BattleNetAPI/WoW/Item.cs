using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace BattleNet.API.WoW
{
    [DataContract]
    public class Item : ResponseRoot
    {
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
        public int Quality { get; set; }

        [XmlElement("tooltipParams")]
        [DataMember(Name = "tooltipParams")]
        public ItemToolTip TooltipParams { get; set; }

        // the rest are on the data api

        [XmlElement("disenchantingSkillRank")]
        [DataMember(Name = "disenchantingSkillRank")]
        public int disenchantingSkillRank { get; set; }

        [XmlElement("description")]
        [DataMember(Name = "description")]
        public string description { get; set; }

        [XmlElement("stackable")]
        [DataMember(Name = "stackable")]
        public int stackable { get; set; }

        // not sure what this is...
        //"itemBind":{"id":0},

        [XmlArray("bonusStats")]
        [XmlArrayItem("item")]
        [DataMember(Name = "bonusStats")]
        public List<object> BonusStats { get; set; }

        [XmlArray("itemSpells")]
        [XmlArrayItem("item")]
        [DataMember(Name = "itemSpells")]
        public List<object> ItemSpells { get; set; }
        
        [XmlElement("buyPrice")]
        [DataMember(Name = "buyPrice")]
        public int BuyPrice { get; set; }
        
        [XmlElement("itemClass")]
        [DataMember(Name = "itemClass")]
        public ItemClass ItemClass { get; set; }
        
        [XmlElement("itemSubClass")]
        [DataMember(Name = "itemSubClass")]
        public ItemSubClass ItemSubClass { get; set; }
        
        [XmlElement("containerSlots")]
        [DataMember(Name = "containerSlots")]
        public int ContainerSlots { get; set; }
        
        [XmlElement("weaponInfo")]
        [DataMember(Name = "weaponInfo")]
        public WeaponInfo WeaponInfo { get; set; }
        
        [XmlElement("inventoryType")]
        [DataMember(Name = "inventoryType")]
        public int InventoryType { get; set; }
        
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
        // TODO: complete this list when blizzard does
    }

    [DataContract]
    public class ItemSource
    {
        [XmlElement("sourceId")]
        [DataMember(Name = "sourceId")]
        public int SourceId { get; set; }        
        [XmlElement("sourceType")]
        [DataMember(Name = "sourceType")]
        public SourceType SourceType { get; set; }
    }

    [DataContract]
    public class WeaponInfo
    {
        [XmlElement("damage")]
        [DataMember(Name = "damage")]
        public DamageRange Damage { get; set; }
        
        [XmlElement("weaponSpeed")]
        [DataMember(Name = "weaponSpeed")]
        public float weaponSpeed { get; set; }
        
        [XmlElement("dps")]
        [DataMember(Name = "dps")]
        public float dps { get; set; }
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
    public class ItemClass
    {
        [XmlElement("class")]
        [DataMember(Name = "class")]
        public int Class { get; set; }
        
        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    public class ItemSubClass
    {
        [XmlElement("subclassId")]
        [DataMember(Name = "subclassId")]
        public int subclassId { get; set; }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string name { get; set; }
    }
}
