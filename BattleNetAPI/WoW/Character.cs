using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Drawing;

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
        internal long lastModified
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
        public Achievements Achievements { get; set; }

        [XmlElement("progression", IsNullable = true)]
        [DataMember(Name = "progression")] 
        public Progression Progression { get; set; }

        [XmlElement("guild", IsNullable = true)]
        [DataMember(Name = "guild")] 
        public GuildInfo Guild { get; set; }

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
                IconColor = Color.FromArgb(int.Parse(value, System.Globalization.NumberStyles.HexNumber));
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
                BorderColor = Color.FromArgb(int.Parse(value, System.Globalization.NumberStyles.HexNumber));
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
                BackgroundColor = Color.FromArgb(int.Parse(value, System.Globalization.NumberStyles.HexNumber));
            }
        }

        public Color BackgroundColor { get; set; }
    }

    /*
     * Base stats = stats
Equipped Items = items
Reputation = reputation
Talents = talents
Titles = titles
Mounts = mounts
Companions = companions
Hunter Pets = pets (one thing of note is that these provide just the creature id, not what class they are so it will list the pet as creature: 41166 but will not say that it is a core hound pet)
Achievements = achievements
Professions = professions
appearance = appearance


 Guild:
Roster = members
Achievements = achievements

PvP:
Roster = members
     */

    public enum Standing
    {
        Hated = 0,
        Hostile = 1,
        Unfriendly = 2,
        Neutural = 3,
        Friendly = 4,
        Honored = 5,
        Revered = 6,
        Exalted = 7,


    }

    public class Reputation
    {
        [XmlElement("id")]      public int Id { get; set; }
        [XmlElement("name")]    public string Name { get; set; }        
        [XmlIgnore]             public Standing Standing { get; set; }

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
        [XmlElement("value")]   public int Value { get; set; }
        [XmlElement("max")]     public int Max { get; set; }
    }


    public class Progression
    {
        [XmlArray("raids")]
        [XmlArrayItem("item")]
        public List<RaidProgression> raids { get; set; }
    }

    public class RaidProgression
    {
        [XmlElement("name")]    public string Name { get; set; }
        [XmlElement("normal")]  public int Normal { get; set; }
        [XmlElement("heroic")]  public int Heroic { get; set; }
        [XmlElement("id")]      public int Id { get; set; }
        [XmlArray("bosses")]
        [XmlArrayItem("item")]
        public List<BossProgression> bosses { get; set; }
    }

    public class BossProgression
    {
        [XmlElement("name")]        public string Name { get; set; }
        [XmlElement("normalKills")] public int NormalKills { get; set; }
        [XmlElement("heroicKills")] public int HeroicKills { get; set; }
        [XmlElement("id")]          public int Id { get; set; }
    }
    public class Pet
    {
        [XmlElement("name")]        public string Name { get; set; }
        [XmlElement("creature")]    public int Creature { get; set; }
        [XmlElement("slot")]        public int Slot { get; set; }


    }

    public class Professions
    {
        [XmlArray("primary")]
        [XmlArrayItem("item")]
        public List<Profession> Primary { get; set; }

        [XmlArray("secondary")]
        [XmlArrayItem("item")]
        public List<Profession> Secondary { get; set; }
    }

    public class Profession
    {
        [XmlElement("id")]      public int Id { get; set; }
        [XmlElement("name")]    public string Name { get; set; }
        [XmlElement("icon")]    public string Icon { get; set; }
        [XmlElement("rank")]    public int Rank { get; set; }
        [XmlElement("max")]     public int Max { get; set; }
        
        [XmlArray("recipes")]
        [XmlArrayItem("item")]
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

    public class Stats
    {
        [XmlElement("health")]      public int Health { get; set; }
        [XmlElement("power")]       public int Power  { get; set; }
        [XmlElement("powerType")]   public PowerType PowerType { get; set; }
        [XmlElement("str")]         public int Strength { get; set; }
        [XmlElement("agi")]         public int Agility { get; set; }
        [XmlElement("sta")]         public int Stamina { get; set; }
        [XmlElement("int")]         public int Intelect { get; set; }
        [XmlElement("spr")]         public int Spirit { get; set; }
        [XmlElement("attackPower")] public int AttackPower { get; set; }
        [XmlElement("rangedAttackPower")]public int RangedAttackPower { get; set; }
        [XmlElement("mastery")]     public float Mastery { get; set; }
        [XmlElement("masteryRating")]public int MasteryRating { get; set; }
        [XmlElement("crit")]        public float CritPercent { get; set; }
        [XmlElement("critRating")]  public int CritRating { get; set; }
        [XmlElement("hitPercent")]  public float HitPercent { get; set; }
        [XmlElement("hitRating")]   public int HitRating { get; set; }
        [XmlElement("hasteRating")] public int HasteRating { get; set; }
        [XmlElement("expertiseRating")] public int ExpertiseRating { get; set; }
        [XmlElement("spellPower")]  public int SpellPower { get; set; }
        [XmlElement("spellPen")]    public int SpellPen { get; set; }
        [XmlElement("spellCrit")]   public float SpellCrit { get; set; }
        [XmlElement("spellCritRating")] public int SpellCritRating { get; set; }
        [XmlElement("spellHitPercent")] public float SpellHitPercent { get; set; }
        [XmlElement("spellHitRating")]  public int SpellHitRating { get; set; }
        [XmlElement("mana5")]       public float Mana5 { get; set; }
        [XmlElement("mana5Combat")] public float Mana5Combat { get; set; }
        [XmlElement("armor")]       public int Armor { get; set; }
        [XmlElement("dodge")]       public float Dodge { get; set; }
        [XmlElement("dodgeRating")] public int DodgeRating { get; set; }
        [XmlElement("parry")]       public float Parry { get; set; }
        [XmlElement("parryRating")] public int ParryRating { get; set; }
        [XmlElement("block")]       public float Block { get; set; }
        [XmlElement("blockRating")] public int BlockRating { get; set; }
        [XmlElement("resil")]       public int Resil { get; set; }
        [XmlElement("mainHandDmgMin")] public float MainHandDamageMin { get; set; }
        [XmlElement("mainHandDmgMax")] public float MainHandDamageMax { get; set; }
        [XmlElement("mainHandSpeed")] public float MainHandSpeed { get; set; }
        [XmlElement("mainHandDps")] public float MainHandDps { get; set; }
        [XmlElement("mainHandExpertise")]public int MainHandExpertise { get; set; }
        [XmlElement("offHandDmgMin")]   public float OffHandDamageMin { get; set; }
        [XmlElement("offHandDmgMax")]   public float OffHandDamageMax { get; set; }
        [XmlElement("offHandSpeed")]    public float OffHandSpeed { get; set; }
        [XmlElement("offHandDps")]      public float OffHandDps { get; set; }
        [XmlElement("offHandExpertise")] public int OffHandExpertise { get; set; }
        [XmlElement("rangedDmgMin")]    public float RangedDamageMin { get; set; }
        [XmlElement("rangedDmgMax")]    public float RangedDamageMax { get; set; }
        [XmlElement("rangedSpeed")]     public float RangedSpeed { get; set; }
        [XmlElement("rangedDps")]       public float RangedDps { get; set; }
        [XmlElement("rangedCrit")]      public float RangedCrit { get; set; }
        [XmlElement("rangedCritRating")]public int RangedCritRating { get; set; }
        [XmlElement("rangedHitPercent")]public float RangedHitPercent { get; set; }
        [XmlElement("rangedHitRating")] public int RangedHitRating { get; set; }
    }

    public class Talent
    {
        [XmlElement("selected")]
        public bool Selected { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("icon")]
        public string Icon { get; set; }
        [XmlElement("build")]
        public string Build { get; set; }
        [XmlElement("trees")]
        public List<Tree> Trees { get; set; }
        [XmlElement("glyphs")]
        public Glyphs Glyphs { get; set; }
    }

    public class Tree
    {
        public string points { get; set; }
        public int total { get; set; }
    }

    public class Glyphs
    {
        [XmlArray("prime")]
        [XmlArrayItem("item")]
        public List<Glyph> Prime { get; set; }

        [XmlArray("major")]
        [XmlArrayItem("item")]
        public List<Glyph> Major { get; set; }

        [XmlArray("minor")]
        [XmlArrayItem("item")]
        public List<Glyph> Minor { get; set; }
    }

    public class Glyph
    {
        [XmlElement("glyph")]   public int Id { get; set; }
        [XmlElement("item")]    public int ItemId { get; set; }
        [XmlElement("name")]    public string Name { get; set; }
        [XmlElement("icon")]    public string Icon { get; set; }

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

}
