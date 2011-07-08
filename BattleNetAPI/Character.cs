using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
namespace BattleNet.API.WoW
{

    public enum Status
    {
        Ok,
        [XmlEnum("nok")]
        NotOk,
    }

    [XmlRoot("root")]
    public class CharacterRoot : Character
    {
        public Status status { get; set; }
        public string reason { get; set; }
    }

    /*
         Basic information: name, level, class, race, gender, faction, guild, achievement points
        Optional fields: equipped items, stats, reputation, primary and secondary skills, achievements/statistics,
         * talents, titles, collected mounts and companions, quests, profession recipes, Hunter pets, PvP information
         */
    public class Character
    {
        [Flags]
        public enum Fields
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
        }

        public ulong lastModified { get; set; }
        public string name { get; set; }
        public string realm { get; set; }

        [XmlElement("class")]
        public int Class { get; set; }
        public int race { get; set; }
        public int gender { get; set; }
        public int level { get; set; }
        public int achievementPoints { get; set; }

        // Add http://us.battle.net/static-render/us/ infront to get image
        public string thumbnail { get; set; }

        // stats
        [XmlElement("stats", IsNullable = true)]
        public Stats stats { get; set; }

        [XmlArray("talents", IsNullable=true)]
        [XmlArrayItem("item")]
        public List<Talent> Talents { get; set; }

        /////////////////////////////
        // items
        /////////////////////////////
        [XmlElement("items", IsNullable = true)]
        public CharacterItems Items { get; set; }

        // reputation
        [XmlArray("reputation", IsNullable = true)]
        [XmlArrayItem("item")]
        public List<Reputation> Reputation { get; set; }

        // Titles
        [XmlArray("titles", IsNullable = true)]
        [XmlArrayItem("item")]
        public List<Title> Titles { get; set; }

        // professions
        [XmlElement("professions", IsNullable = true)]
        public Professions professions { get; set; }

        // appearance
        [XmlElement("appearance", IsNullable = true)]
        public Appearance Appearance { get; set; }


        // companions
        [XmlArray("companions", IsNullable = true)]
        [XmlArrayItem("item")]
        public List<int> companions { get; set; }

        [XmlArray("mounts", IsNullable = true)]
        [XmlArrayItem("item")]
        public List<int> Mounts { get; set; }

        // pets
        [XmlArray("pets", IsNullable = true)]
        [XmlArrayItem("item")]
        public List<Pet> Pets { get; set; }


        // achievements        
        [XmlElement("achievements", IsNullable = true)]
        public Achievements achievements { get; set; }

        [XmlElement("progression", IsNullable = true)]
        public Progression progression { get; set; }

        [XmlElement("guild")]
        public GuildInfo Guild { get; set; }

    }

    /// <summary>
    /// We need this 'extra' guild class because 'members' is an array
    /// in the real one, and only a number in the Character one
    /// </summary>
    public class GuildInfo
    {        
        public string name { get; set; }
        public string realm { get; set; }
        public int level { get; set; }
        public int members { get; set; }
        public int achievementPoints { get; set; }

        [XmlElement("emblem")]
        public Emblem Emblem { get; set; }

    }

    public class Emblem
    {
        public int icon { get; set; }
        public string iconColor { get; set; }
        public int border { get; set; }
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }
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
    // TODO: need to rename this
    public class Achievements
    {
        [XmlArray("achievementsCompleted")]
        [XmlArrayItem("item")]
        public List<int> achievementsCompleted { get; set; }

        [XmlArray("achievementsCompletedTimestamp")]
        [XmlArrayItem("item")]
        public List<long> achievementsCompletedTimestamp { get; set; }

        [XmlArray("criteria")]
        [XmlArrayItem("item")]
        public List<int> criteria { get; set; }

        [XmlArray("criteriaQuantity")]
        [XmlArrayItem("item")]
        public List<long> criteriaQuantity { get; set; }

        [XmlArray("criteriaTimestamp")]
        [XmlArrayItem("item")]
        public List<long> criteriaTimestamp { get; set; }

        [XmlArray("criteriaCreated")]
        [XmlArrayItem("item")]
        public List<long> criteriaCreated { get; set; }
    }

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
        public int id { get; set; }
        public string name { get; set; }
        [XmlIgnore]
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
        public int value { get; set; }
        public int max { get; set; }
    }


    public class Progression
    {
        [XmlArray("raids")]
        [XmlArrayItem("item")]
        public List<RaidProgression> raids { get; set; }
    }

    public class RaidProgression
    {
        public string name { get; set; }
        public int normal { get; set; }
        public int heroic { get; set; }
        public int id { get; set; }
        [XmlArray("bosses")]
        [XmlArrayItem("item")]
        public List<BossProgression> bosses { get; set; }
    }

    public class BossProgression
    {
        public string name { get; set; }
        public int normalKills { get; set; }
        public int heroicKills { get; set; }
        public int id { get; set; }
    }
    public class Pet
    {
        public string name { get; set; }
        public int creature { get; set; }
        public int slot { get; set; }


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
        public int id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int rank { get; set; }
        public int max { get; set; }

        [XmlArray("recipes")]
        [XmlArrayItem("item")]
        public List<int> recipes { get; set; }

    }


    public class Appearance
    {
        public int faceVariation { get; set; }
        public int skinColor { get; set; }
        public int hairVariation { get; set; }
        public int hairColor { get; set; }
        public int featureVariation { get; set; }
        public bool showHelm { get; set; }
        public bool showCloak { get; set; }
    }

    public class Stats
    {
        public int health;
        public int power;
        public string powerType;

        [XmlElement("str")]
        public int Strength { get; set; }

        [XmlElement("agi")]
        public int Agility { get; set; }

        [XmlElement("sta")]
        public int Stamina { get; set; }

        [XmlElement("int")]
        public int Intelect { get; set; }

        [XmlElement("spr")]
        public int Spirit { get; set; }

        [XmlElement("attackPower")]
        public int AttackPower { get; set; }

        [XmlElement("rangedAttackPower")]
        public int RangedAttackPower { get; set; }

        [XmlElement("mastery")]
        public float Mastery { get; set; }

        [XmlElement("masteryRating")]
        public int MasteryRating { get; set; }

        [XmlElement("crit")]
        public float CritPercent { get; set; }

        [XmlElement("critRating")]
        public int critRating { get; set; }

        [XmlElement("hitPercent")]
        public float hitPercent { get; set; }

        [XmlElement("hitRating")]
        public int hitRating { get; set; }

        [XmlElement("hasteRating")]
        public int hasteRating { get; set; }

        [XmlElement("expertiseRating")]
        public int expertiseRating { get; set; }

        [XmlElement("spellPower")]
        public int spellPower { get; set; }

        [XmlElement("spellPen")]
        public int spellPen { get; set; }

        [XmlElement("spellCrit")]
        public float spellCrit { get; set; }

        [XmlElement("spellCritRating")]
        public int spellCritRating { get; set; }

        [XmlElement("spellHitPercent")]
        public float spellHitPercent { get; set; }

        [XmlElement("spellHitRating")]
        public int spellHitRating { get; set; }

        [XmlElement("mana5")]
        public float mana5 { get; set; }

        [XmlElement("mana5Combat")]
        public float mana5Combat { get; set; }

        [XmlElement("armor")]
        public int armor { get; set; }

        [XmlElement("dodge")]
        public float dodge { get; set; }

        [XmlElement("dodgeRating")]
        public int dodgeRating { get; set; }

        [XmlElement("parry")]
        public float parry { get; set; }

        [XmlElement("parryRating")]
        public int parryRating { get; set; }

        [XmlElement("block")]
        public float block { get; set; }

        [XmlElement("blockRating")]
        public int blockRating { get; set; }

        [XmlElement("resil")]
        public int resil { get; set; }

        [XmlElement("mainHandDmgMin")]
        public float mainHandDmgMin { get; set; }

        [XmlElement("mainHandDmgMax")]
        public float mainHandDmgMax { get; set; }

        [XmlElement("mainHandSpeed")]
        public float mainHandSpeed { get; set; }

        [XmlElement("mainHandDps")]
        public float mainHandDps { get; set; }

        [XmlElement("mainHandExpertise")]
        public int mainHandExpertise { get; set; }

        [XmlElement("offHandDmgMin")]
        public float offHandDmgMin { get; set; }

        [XmlElement("offHandDmgMax")]
        public float offHandDmgMax { get; set; }

        [XmlElement("offHandSpeed")]
        public float offHandSpeed { get; set; }

        [XmlElement("offHandDps")]
        public float offHandDps { get; set; }

        [XmlElement("offHandExpertise")]
        public int offHandExpertise { get; set; }

        [XmlElement("rangedDmgMin")]
        public float rangedDmgMin { get; set; }

        [XmlElement("rangedDmgMax")]
        public float rangedDmgMax { get; set; }

        [XmlElement("rangedSpeed")]
        public float rangedSpeed { get; set; }

        [XmlElement("rangedDps")]
        public float rangedDps { get; set; }

        [XmlElement("rangedCrit")]
        public float rangedCrit { get; set; }

        [XmlElement("rangedCritRating")]
        public int rangedCritRating { get; set; }

        [XmlElement("rangedHitPercent")]
        public float rangedHitPercent { get; set; }

        [XmlElement("rangedHitRating")]
        public int rangedHitRating { get; set; }
    }

    public class Talent
    {
        public bool selected { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string build { get; set; }
        public List<Tree> trees { get; set; }
        public GlyphCollection glyphs { get; set; }
    }

    public class Tree
    {
        public string points { get; set; }
        public int total { get; set; }
    }

    public class GlyphCollection
    {
        [XmlArray("prime")]
        [XmlArrayItem("item")]
        public List<Glyph> prime { get; set; }

        [XmlArray("major")]
        [XmlArrayItem("item")]
        public List<Glyph> major { get; set; }

        [XmlArray("minor")]
        [XmlArrayItem("item")]
        public List<Glyph> minor { get; set; }
    }

    public class Glyph
    {
        public int glyph { get; set; }
        public int item { get; set; }
        public string name { get; set; }
        public string icon { get; set; }

    }

    public class CharacterItems
    {
        public int averageItemLevel { get; set; }
        public int averageItemLevelEquipped { get; set; }

        public Item head { get; set; }
        public Item neck { get; set; }
        public Item shoulder { get; set; }
        public Item back { get; set; }
        public Item chest { get; set; }
        public Item tabard { get; set; }
        public Item wrist { get; set; }
        public Item hands { get; set; }
        public Item waist { get; set; }
        public Item legs { get; set; }
        public Item feet { get; set; }
        public Item finger1 { get; set; }
        public Item finger2 { get; set; }
        public Item trinket1 { get; set; }
        public Item trinket2 { get; set; }
        public Item mainHand { get; set; }
        public Item offHand { get; set; }
        public Item ranged { get; set; }
    }

    public class Title
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ItemToolTip
    {
        public int gem0 { get; set; }
        public int gem1 { get; set; }
        public int gem2 { get; set; }
        public int gem3 { get; set; }
        public bool extraSocket { get; set; }
        public int enchange { get; set; }
        public int reforge { get; set; }
    }

}
