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
    public class RealmCollection : ResponseRoot
    {
        [XmlArray("realms")]
        [XmlArrayItem("item")]
        public List<Realm> Realms { get; set; }
    }
    
    public class Realm
    {
        [XmlElement("type")]        public RealmType Type { get; set; }
        [XmlElement("queue")]       public bool Queue { get; set; }
        [XmlElement("status")]      public bool Status { get; set; }
        [XmlElement("population")]  public RealmPopulation Population { get; set; }
        [XmlElement("name")]        public string Name { get; set; }
        [XmlElement("slug")]        public string Slug { get; set; }
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
