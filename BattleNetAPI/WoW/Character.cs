﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.Runtime.Serialization;
#if SILVERLIGHT
using System.Windows.Media;
#else
using System.Drawing;
#endif


namespace BattleNet.API.WoW
{
    /*
      Basic information: name, level, class, race, gender, faction, guild, achievement points
      Optional fields: equipped items, stats, reputation, primary and secondary skills, achievements/statistics,
      talents, titles, collected mounts and companions, quests, profession recipes, Hunter pets, PvP information
    */    
    [DataContract]
    public class Character : ResponseRoot
    {        
        [XmlElement("lastModified")]                        
        public UnixTimestamp LastModified { get; set; }
        
        [DataMember(Name = "lastModified")]
        private long lastModified
        {
            get { return LastModified.ToMsec(); }
            set{
                LastModified = new UnixTimestamp(value);
            }
        }
        [XmlElement("name")]
        [DataMember(Name="name")]
        public string Name { get; set; }

        [XmlElement("realm")]
        [DataMember(Name = "realm")]
        public string Realm { get; set; }

        [XmlElement("class")]
        [DataMember(Name = "class")]
        public int Class { get; set; }
        [XmlElement("race")]
        [DataMember(Name = "race")]
        public int Race { get; set; }
        [XmlElement("gender")]
        [DataMember(Name = "gender")]
        public int Gender { get; set; }
        [XmlElement("level")]
        [DataMember(Name = "level")]
        public int Level { get; set; }
        [XmlElement("achievementPoints")]
        [DataMember(Name = "achievementPoints")]
        public int AchievementPoints { get; set; }

        [XmlArray("feed")]
        [XmlArrayItem("item")]
        [DataMember(Name="feed")]
        public List<FeedEvent> Feed { get;set; }
        // Add http://us.battle.net/static-render/us/ infront to get image
        [XmlElement("thumbnail")]
        [DataMember(Name = "thumbnail")]
        public string Thumbnail { get; set; }

        // stats
        [XmlElement("stats", IsNullable = true)]
        [DataMember(Name = "stats")]
        public Stats Statistics { get; set; }

        [XmlArray("talents", IsNullable=true)]
        [XmlArrayItem("item")]
        [DataMember(Name = "talents")]
        public List<Talent> Talents { get; set; }

        /////////////////////////////
        // items
        /////////////////////////////
        [XmlElement("items", IsNullable = true)]
        [DataMember(Name = "items")]
        public CharacterItems Items { get; set; }

        // reputation
        [XmlArray("reputation", IsNullable = true)]
        [XmlArrayItem("item")]
        [DataMember(Name = "reputation")]
        public List<Reputation> Reputation { get; set; }

        // Titles
        [XmlArray("titles", IsNullable = true)]
        [XmlArrayItem("item")]
        [DataMember(Name = "titles")]
        public List<Title> Titles { get; set; }

        [XmlArray("quests", IsNullable = true)]
        [XmlArrayItem("item")]
        [DataMember(Name = "quests")]
        public List<int> Quests { get; set; }

        // professions
        [XmlElement("professions", IsNullable = true)]
        [DataMember(Name = "professions")]
        public Professions Professions { get; set; }

        // appearance
        [XmlElement("appearance", IsNullable = true)]
        [DataMember(Name = "appearance")]
        public Appearance Appearance { get; set; }


        // companions
        [XmlArray("companions", IsNullable = true)]
        [XmlArrayItem("item")]
        [DataMember(Name = "companions")]
        public List<int> Companions { get; set; }

        // mounts
        [XmlArray("mounts", IsNullable = true)]
        [XmlArrayItem("item")]
        [DataMember(Name = "mounts")]
        public List<int> Mounts { get; set; }

        // pets
        [XmlArray("pets", IsNullable = true)]
        [XmlArrayItem("item")]
        [DataMember(Name = "pets")]
        public List<Pet> Pets { get; set; }


        // achievements        
        [XmlElement("achievements", IsNullable = true)]
        [DataMember(Name = "achievements")]        
        public AchievementProgression Achievements { get; set; }

        [XmlElement("progression", IsNullable = true)]
        [DataMember(Name = "progression")] 
        public Progression Progression { get; set; }

        [XmlElement("guild", IsNullable = true)]
        [DataMember(Name = "guild")] 
        public GuildInfo Guild { get; set; }

        [XmlElement("pvp", IsNullable = true)]
        [DataMember(Name = "pvp")]
        public CharacterPvP PvP { get; set; }
    }

    [DataContract]
    public abstract class FeedEvent
    {

        [DataMember(Name="type")]
        public string Type { get; set; }
        [XmlElement("timestamp")]
        public UnixTimestamp Timestamp { get; set; }

        [DataMember(Name = "timestamp")]
        private long timestamp
        {
            get { return Timestamp.ToMsec(); }
            set
            {
                Timestamp = new UnixTimestamp(value);
            }
        }
    }

    [DataContract]
    public class AchievementFeedEvent : FeedEvent
    {
        [DataMember(Name="featOfStrength")]
        public bool FeatOfStrenth { get; set; }

        [DataMember(Name="achievement")]
        public Achievement Achievement { get; set; }
    }

    [DataContract]
    public class LootFeedEvent : FeedEvent
    {
        [DataMember(Name = "itemID")]
        public string ItemId { get; set; }
    }

    [DataContract]
    public class CriteriaFeedEvent : AchievementFeedEvent
    {
        [DataMember(Name = "criteria")]
        public Criteria Criteria { get; set; }
    }

    [DataContract]
    public class BosskillFeedEvent : CriteriaFeedEvent
    {
        [DataMember(Name = "quantity")]
        public int Quantity { get; set; }

        [DataMember(Name="name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class CharacterPvP
    {
        [XmlElement("ratedBattlegrounds")]
        [DataMember(Name = "ratedBattlegrounds")]
        public RatedBattlegrounds RatedBattlegrounds { get; set; }

        [XmlArray("arenaTeams")]
        [XmlArrayItem("item")]
        [DataMember(Name = "arenaTeams")]
        public List<CharacterArenaTeam> ArenaTeams { get; set; }

        [XmlElement("totalHonorableKills")]
        [DataMember(Name = "totalHonorableKills")]
        public int TotalHonorableKills { get; set; }
    }

    [DataContract]
    public class CharacterArenaTeam : ResponseRoot
    {

        [XmlElement("teamRating")]
        [DataMember(Name = "teamRating")]
        public int TeamRating { get; set; }

        [XmlElement("personalRating")]
        [DataMember(Name = "personalRating")]
        public int PersonalRating { get; set; }

        [XmlElement("size")]
        [DataMember(Name = "size")]
        public string teamSize
        {
            get
            {
                return TeamSize + "v" + TeamSize;
            }
            set
            {
                TeamSize = int.Parse(""+value[0]);
            }
        }

        [XmlIgnore]
        public int TeamSize { get; set; }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// We need this 'extra' guild class because 'members' is an array
    /// in the real one, and only a number in the Character one
    /// </summary>
    [DataContract]
    public class GuildInfo
    {
        [XmlElement("name")]
        [DataMember(Name="name")]
        public string Name { get; set; }
        
        [XmlElement("realm")]
        [DataMember(Name = "realm")]
        public string Realm { get; set; }

        [XmlElement("level")]
        [DataMember(Name = "level")]
        public int Level { get; set; }

        [XmlElement("members")]
        [DataMember(Name = "members")]
        public int Members { get; set; }

        [XmlElement("achievementPoints")]
        [DataMember(Name = "achievementPoints")]
        public int AchievementPoints { get; set; }

        [XmlElement("emblem")]
        [DataMember(Name = "emblem")]
        public Emblem Emblem { get; set; }

    }

    [DataContract]
    public class Emblem
    {
        [XmlElement("icon")]
        [DataMember(Name="icon")]
        public int Icon { get; set; }


        [XmlElement("iconColor")]
        [DataMember(Name = "iconColor")]
        public string iconColor
        {
            get
            {
                return IconColor.ToArgb().ToString("X08");
            }
            set
            {
                int argb = int.Parse(value, System.Globalization.NumberStyles.HexNumber);
                IconColor = Color.FromArgb(
                    (byte)(argb>>24),
                    (byte)(argb >> 16),
                    (byte)(argb >> 8),
                    (byte)(argb >> 0)
                    );
            }
        }

        [XmlIgnore]
        public Color IconColor
        {
            get;
            set;
        }

        [XmlElement("border")]
        [DataMember(Name = "border")]
        public int Border { get; set; }

        [XmlElement("borderColor")]
        [DataMember(Name = "borderColor")]
        public string borderColor
        {
            get
            {
                return BorderColor.ToArgb().ToString("X08");
            }
            set
            {
                int argb = int.Parse(value, System.Globalization.NumberStyles.HexNumber);
                BorderColor = Color.FromArgb(
                    (byte)(argb >> 24),
                    (byte)(argb >> 16),
                    (byte)(argb >> 8),
                    (byte)(argb >> 0)
                    );
                
            }
        }

        [XmlIgnore]
        public Color BorderColor { get; set; }

        [XmlElement("backgroundColor")]
        [DataMember(Name = "backgroundColor")]
        public string backgroundColor
        {
            get
            {
                return BackgroundColor.ToArgb().ToString("X08");
            }
            set
            {
                int argb = int.Parse(value, System.Globalization.NumberStyles.HexNumber);
                BackgroundColor = Color.FromArgb(
                    (byte)(argb >> 24),
                    (byte)(argb >> 16),
                    (byte)(argb >> 8),
                    (byte)(argb >> 0)
                    );
            }
        }

        public Color BackgroundColor { get; set; }
    }

    [DataContract]
    public enum Standing
    {
        [EnumMember(Value="0")]
        Hated = 0,
        [EnumMember(Value = "1")]
        Hostile = 1,
        [EnumMember(Value = "2")]
        Unfriendly = 2,
        [EnumMember(Value = "3")]
        Neutural = 3,
        [EnumMember(Value = "4")]
        Friendly = 4,
        [EnumMember(Value = "5")]
        Honored = 5,
        [EnumMember(Value = "6")]
        Revered = 6,
        [EnumMember(Value = "7")]
        Exalted = 7,
    }

    [DataContract]
    public class Reputation
    {
        [XmlElement("id")]     
        [DataMember(Name="id")]
        public int Id { get; set; }
        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlIgnore]
        [DataMember(Name = "standing")]
        public Standing Standing { get; set; }

        [XmlElement("standing")]
        //[EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int StandingId
        {
            get
            {
                return (int)Standing;
            }
            set
            {
                Standing = (Standing)value;
            }
        }
        [XmlElement("value")]
        [DataMember(Name = "value")]
        public int Value { get; set; }

        [XmlElement("max")]
        [DataMember(Name = "max")]
        public int Max { get; set; }
    }

    [DataContract]
    public class Progression
    {
        [XmlArray("raids")]
        [XmlArrayItem("item")]
        [DataMember(Name="raids")]
        public List<RaidProgression> raids { get; set; }
    }

    [DataContract]
    public class RaidProgression
    {
        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlElement("normal")]
        [DataMember(Name = "normal")]
        public int Normal { get; set; }

        [XmlElement("heroic")]
        [DataMember(Name = "heroic")]
        public int Heroic { get; set; }

        [XmlElement("id")]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        
        [XmlArray("bosses")]
        [XmlArrayItem("item")]
        [DataMember(Name = "bosses")]
        public List<BossProgression> bosses { get; set; }
    }

    [DataContract]
    public class BossProgression
    {
        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlElement("normalKills")]
        [DataMember(Name = "normalKills")]
        public int NormalKills { get; set; }

        [XmlElement("heroicKills")]
        [DataMember(Name = "heroicKills")]
        public int HeroicKills { get; set; }

        [XmlElement("id")]
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }

    [DataContract]
    public class Pet
    {
        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlElement("creature")]
        [DataMember(Name = "creature")]
        public int Creature { get; set; }

        [XmlElement("slot")]
        [DataMember(Name = "slot")]
        public int Slot { get; set; }


    }

    [DataContract]
    public class Professions
    {
        [XmlArray("primary")]
        [XmlArrayItem("item")]
        [DataMember(Name = "primary")]
        public List<Profession> Primary { get; set; }

        [XmlArray("secondary")]
        [XmlArrayItem("item")]
        [DataMember(Name = "secondary")]
        public List<Profession> Secondary { get; set; }
    }

    [DataContract]
    public class Profession
    {
        [XmlElement("id")]
        [DataMember(Name = "id")]
        public int Id { get; set; }
        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [XmlElement("icon")]
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
        [XmlElement("rank")]
        [DataMember(Name = "rank")]
        public int Rank { get; set; }
        [XmlElement("max")]
        [DataMember(Name = "max")]
        public int Max { get; set; }
        
        [XmlArray("recipes")]
        [XmlArrayItem("item")]
        [DataMember(Name = "recipes")]
        public List<int> Recipes { get; set; }

    }

    [DataContract]
    public class Appearance
    {
        [XmlElement("faceVariation")]
        [DataMember(Name = "faceVariation")]
        public int FaceVariation { get; set; }
        
        [XmlElement("skinColor")]
        [DataMember(Name = "skinColor")]
        public int SkinColor { get; set; }
        
        [XmlElement("hairVariation")]
        [DataMember(Name = "hairVariation")]
        public int HairVariation { get; set; }
        
        [XmlElement("hairColor")]
        [DataMember(Name = "hairColor")]
        public int HairColor { get; set; }
        
        [XmlElement("featureVariation")]
        [DataMember(Name = "featureVariation")]
        public int FeatureVariation { get; set; }
        
        [XmlElement("showHelm")]
        [DataMember(Name = "showHelm")]
        public bool ShowHelm { get; set; }
        
        [XmlElement("showCloak")]
        [DataMember(Name = "showCloak")]
        public bool ShowCloak { get; set; }
    }

    [DataContract]
    public class Stats
    {
        [XmlElement("health")]
        [DataMember(Name = "health")]
        public int Health { get; set; }

        [XmlElement("power")]
        [DataMember(Name = "power")]
        public int Power { get; set; }

        [XmlElement("powerType")]        
        public PowerType PowerType { get; set; }

        [DataMember(Name = "powerType")]
        private string powerType
        {
            get { return PowerType.ToString(); }
            set
            {
                string v = PowerTypeConverter.Translate(value);
                PowerType = (PowerType)Enum.Parse(typeof(PowerType), v, true);
            }
        }
        [XmlElement("str")]
        [DataMember(Name = "str")]
        public int Strength { get; set; }

        [XmlElement("agi")]
        [DataMember(Name = "agi")]
        public int Agility { get; set; }

        [XmlElement("sta")]
        [DataMember(Name = "sta")]
        public int Stamina { get; set; }

        [XmlElement("int")]
        [DataMember(Name = "int")]
        public int Intelect { get; set; }

        [XmlElement("spr")]
        [DataMember(Name = "spr")]
        public int Spirit { get; set; }

        [XmlElement("attackPower")]
        [DataMember(Name = "attackPower")]
        public int AttackPower { get; set; }

        [XmlElement("rangedAttackPower")]
        [DataMember(Name = "rangedAttackPower")]
        public int RangedAttackPower { get; set; }

        [XmlElement("mastery")]
        [DataMember(Name = "mastery")]
        public float Mastery { get; set; }

        [XmlElement("masteryRating")]
        [DataMember(Name = "masteryRating")]
        public int MasteryRating { get; set; }

        [XmlElement("crit")]
        [DataMember(Name = "crit")]
        public float CritPercent { get; set; }

        [XmlElement("critRating")]
        [DataMember(Name = "critRating")]
        public int CritRating { get; set; }

        [XmlElement("hitPercent")]
        [DataMember(Name = "hitPercent")]
        public float HitPercent { get; set; }

        [XmlElement("hitRating")]
        [DataMember(Name = "hitRating")]
        public int HitRating { get; set; }

        [XmlElement("hasteRating")]
        [DataMember(Name = "hasteRating")]
        public int HasteRating { get; set; }

        [XmlElement("expertiseRating")]
        [DataMember(Name = "expertiseRating")]
        public int ExpertiseRating { get; set; }

        [XmlElement("spellPower")]
        [DataMember(Name = "spellPower")]
        public int SpellPower { get; set; }

        [XmlElement("spellPen")]
        [DataMember(Name = "spellPen")]
        public int SpellPen { get; set; }

        [XmlElement("spellCrit")]
        [DataMember(Name = "spellCrit")]
        public float SpellCrit { get; set; }

        [XmlElement("spellCritRating")]
        [DataMember(Name = "spellCritRating")]
        public int SpellCritRating { get; set; }

        [XmlElement("spellHitPercent")]
        [DataMember(Name = "spellHitPercent")]
        public float SpellHitPercent { get; set; }

        [XmlElement("spellHitRating")]
        [DataMember(Name = "spellHitRating")]
        public int SpellHitRating { get; set; }

        [XmlElement("mana5")]
        [DataMember(Name = "mana5")]
        public float Mana5 { get; set; }

        [XmlElement("mana5Combat")]
        [DataMember(Name = "mana5Combat")]
        public float Mana5Combat { get; set; }

        [XmlElement("armor")]
        [DataMember(Name = "armor")]
        public int Armor { get; set; }

        [XmlElement("dodge")]
        [DataMember(Name = "dodge")]
        public float Dodge { get; set; }

        [XmlElement("dodgeRating")]
        [DataMember(Name = "dodgeRating")]
        public int DodgeRating { get; set; }

        [XmlElement("parry")]
        [DataMember(Name = "parry")]
        public float Parry { get; set; }

        [XmlElement("parryRating")]
        [DataMember(Name = "parryRating")]
        public int ParryRating { get; set; }

        [XmlElement("block")]
        [DataMember(Name = "block")]
        public float Block { get; set; }

        [XmlElement("blockRating")]
        [DataMember(Name = "blockRating")]
        public int BlockRating { get; set; }

        [XmlElement("resil")]
        [DataMember(Name = "resil")]
        public int Resil { get; set; }

        [XmlElement("mainHandDmgMin")]
        [DataMember(Name = "mainHandDmgMin")]
        public float MainHandDamageMin { get; set; }

        [XmlElement("mainHandDmgMax")]
        [DataMember(Name = "mainHandDmgMax")]
        public float MainHandDamageMax { get; set; }

        [XmlElement("mainHandSpeed")]
        [DataMember(Name = "mainHandSpeed")]
        public float MainHandSpeed { get; set; }

        [XmlElement("mainHandDps")]
        [DataMember(Name = "mainHandDps")]
        public float MainHandDps { get; set; }
        
        [XmlElement("mainHandExpertise")]
        [DataMember(Name = "mainHandExpertise")]
        public int MainHandExpertise { get; set; }

        [XmlElement("offHandDmgMin")]
        [DataMember(Name = "offHandDmgMin")]
        public float OffHandDamageMin { get; set; }

        [XmlElement("offHandDmgMax")]
        [DataMember(Name = "offHandDmgMax")]
        public float OffHandDamageMax { get; set; }

        [XmlElement("offHandSpeed")]
        [DataMember(Name = "offHandSpeed")]
        public float OffHandSpeed { get; set; }

        [XmlElement("offHandDps")]
        [DataMember(Name = "offHandDps")]
        public float OffHandDps { get; set; }

        [XmlElement("offHandExpertise")]
        [DataMember(Name = "offHandExpertise")]
        public int OffHandExpertise { get; set; }

        [XmlElement("rangedDmgMin")]
        [DataMember(Name = "rangedDmgMin")]
        public float RangedDamageMin { get; set; }

        [XmlElement("rangedDmgMax")]
        [DataMember(Name = "rangedDmgMax")]
        public float RangedDamageMax { get; set; }

        [XmlElement("rangedSpeed")]
        [DataMember(Name = "rangedSpeed")]
        public float RangedSpeed { get; set; }

        [XmlElement("rangedDps")]
        [DataMember(Name = "rangedDps")]
        public float RangedDps { get; set; }

        [XmlElement("rangedCrit")]
        [DataMember(Name = "rangedCrit")]
        public float RangedCrit { get; set; }

        [XmlElement("rangedCritRating")]
        [DataMember(Name = "rangedCritRating")]
        public int RangedCritRating { get; set; }

        [XmlElement("rangedHitPercent")]
        [DataMember(Name = "rangedHitPercent")]
        public float RangedHitPercent { get; set; }
        
        [XmlElement("rangedHitRating")]
        [DataMember(Name = "rangedHitRating")]
        public int RangedHitRating { get; set; }
    }

    [DataContract]
    public class Talent
    {
        [XmlElement("selected")]
        [DataMember(Name="selected")]
        public bool Selected { get; set; }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlElement("icon")]
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [XmlElement("build")]
        [DataMember(Name = "build")]
        public string Build { get; set; }

        [XmlElement("trees")]
        [DataMember(Name = "trees")]
        public List<Tree> Trees { get; set; }

        [XmlElement("glyphs")]
        [DataMember(Name = "glyphs")]
        public Glyphs Glyphs { get; set; }
    }

    [DataContract]
    public class Tree
    {
        [XmlElement("points")]
        [DataMember(Name="points")]
        public string Points { get; set; }

        [XmlElement("total")]
        [DataMember(Name = "total")]
        public int Total { get; set; }
    }

    [DataContract]
    public class Glyphs
    {
        [XmlArray("prime")]
        [XmlArrayItem("item")]
        [DataMember(Name = "prime")]
        public List<Glyph> Prime { get; set; }

        [XmlArray("major")]
        [XmlArrayItem("item")]
        [DataMember(Name = "major")]
        public List<Glyph> Major { get; set; }

        [XmlArray("minor")]
        [XmlArrayItem("item")]
        [DataMember(Name = "minor")]
        public List<Glyph> Minor { get; set; }
    }

    [DataContract]
    public class Glyph
    {
        [XmlElement("glyph")]
        [DataMember(Name = "glyph")]
        public int Id { get; set; }

        [XmlElement("item")]
        [DataMember(Name = "item")]
        public int ItemId { get; set; }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlElement("icon")]
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

    }

    [DataContract]
    public class CharacterItems
    {
        [XmlElement("averageItemLevel")]
        [DataMember(Name = "averageItemLevel")]        
        public int AverageItemLevel { get; set; }
        
        [XmlElement("averageItemLevelEquipped")]
        [DataMember(Name = "averageItemLevelEquipped")]
        public int averageItemLevelEquipped { get; set; }
        
        [XmlElement("head")]
        [DataMember(Name = "head")]
        public Item Head { get; set; }
        
        [XmlElement("neck")]
        [DataMember(Name = "neck")]
        public Item Neck { get; set; }
        
        [XmlElement("shoulder")]
        [DataMember(Name = "shoulder")]
        public Item Shoulder { get; set; }
        
        [XmlElement("shirt")]
        [DataMember(Name = "shirt")]
        public Item Shirt { get; set; }
        
        [XmlElement("back")]
        [DataMember(Name = "back")]
        public Item Back { get; set; }
        
        [XmlElement("chest")]
        [DataMember(Name = "chest")]
        public Item Chest { get; set; }
        
        [XmlElement("tabard")]
        [DataMember(Name = "tabard")]
        public Item Tabard { get; set; }
        
        [XmlElement("wrist")]
        [DataMember(Name = "wrist")]
        public Item Wrist { get; set; }
        
        [XmlElement("hands")]
        [DataMember(Name = "hands")]
        public Item Hands { get; set; }
        
        [XmlElement("waist")]
        [DataMember(Name = "waist")]
        public Item Waist { get; set; }
        
        [XmlElement("legs")]
        [DataMember(Name = "legs")]
        public Item Legs { get; set; }
        
        [XmlElement("feet")]
        [DataMember(Name = "feet")]
        public Item Feet { get; set; }
        
        [XmlElement("finger1")]
        [DataMember(Name = "finger1")]
        public Item Finger1 { get; set; }
        
        [XmlElement("finger2")]
        [DataMember(Name = "finger2")]
        public Item Finger2 { get; set; }
        
        [XmlElement("trinket1")]
        [DataMember(Name = "trinket1")]
        public Item Trinket1 { get; set; }
        
        [XmlElement("trinket2")]
        [DataMember(Name = "trinket2")]
        public Item Trinket2 { get; set; }
        
        [XmlElement("mainHand")]
        [DataMember(Name = "mainHand")]
        public Item MainHand { get; set; }
        
        [XmlElement("offHand")]
        [DataMember(Name = "offHand")]
        public Item OffHand { get; set; }
        
        [XmlElement("ranged")]
        [DataMember(Name = "ranged")]
        public Item Ranged { get; set; }
    }
    [DataContract]
    public class Title
    {
        [XmlElement("id")]
        [DataMember(Name="id")]
        public int Id { get; set; }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    [DataContract]
    public class ItemToolTip
    {
        [XmlElement("gem0")]
        [DataMember(Name = "gem0")]
        public int Gem0 { get; set; }
        
        [XmlElement("gem1")]
        [DataMember(Name = "gem1")]        
        public int Gem1 { get; set; }
        
        [XmlElement("gem2")]
        [DataMember(Name = "gem2")]        
        public int Gem2 { get; set; }
        
        [XmlElement("gem3")]
        [DataMember(Name = "gem3")]        
        public int Gem3 { get; set; }
        
        [XmlElement("extraSocket")]
        [DataMember(Name = "extraSocket")]        
        public bool ExtraSocket { get; set; }
        
        [XmlElement("enchant")]
        [DataMember(Name = "enchant")]
        
        public int Enchant { get; set; }
        
        [XmlElement("reforge")]
        [DataMember(Name = "reforge")]        
        public int Reforge { get; set; }
        
        [XmlArray("set")]
        [XmlArrayItem("item")]
        [DataMember(Name = "set")]
        public List<int> Set { get; set; }
        
        /// <summary>        
        /// if an item has been 'tinkered' by an engineering they have this.
        /// This is the spell id of the effect..
        /// </summary>
        [XmlElement("tinker")]
        [DataMember(Name = "tinker")]
        public int Tinker { get; set; }
    }


    [DataContract]
    public class RatedBattlegrounds
    {
        [XmlElement("personalRating")]
        [DataMember(Name = "personalRating")]
        public int PersonalRating { get; set; }

        [XmlElement("battlegrounds")]
        [DataMember(Name = "battlegrounds")]
        public List<Battleground> Battlegrounds { get; set; }
    }

    [DataContract]
    public class Battleground
    {
        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name{get;set;}

        [XmlElement("played")]
        [DataMember(Name = "played")]
        public int Played { get; set; }

        [XmlElement("won")]
        [DataMember(Name = "won")]
        public int Won { get; set; }

    }

}
