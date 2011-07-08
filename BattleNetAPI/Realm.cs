using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
namespace BattleNet.API.WoW
{
    /**
     * <root>
     *  <realms>
     *      <item>
     *          <type/>
     *          <queue/>
     *          <status/>
     *          <population/>
     *          <name/>
     *          <slug/>
     *      </item>
     *  </realms>
     * </root>
     * 
     */    
    public class RealmCollection
    {
        [XmlArray("realms")]
        [XmlArrayItem("item")]
        public List<Realm> Realms { get; set; }
    }
    
    public class Realm
    {
        public RealmType type { get; set; }
        public bool queue { get; set; }
        public bool status { get; set; }
        public RealmPopulation population { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
    }

    public enum RealmPopulation
    {
        [XmlEnum("low")]
        Low,
        [XmlEnum("medium")]
        Medium,
        [XmlEnum("high")]
        High,
    }
    public enum RealmType
    {
        [XmlEnum("pve")]
        PVE,
        [XmlEnum("pvp")]
        PVP,
        [XmlEnum("rp")]
        RP,
        [XmlEnum("rppvp")]
        RPPVP
    }
}
