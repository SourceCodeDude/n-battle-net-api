using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    [DataContract]
    public class AchievementCollection : ResponseRoot
    {
        [XmlArray("achievements")]
        [XmlArrayItem("item")]
        [DataMember(Name = "achievements")]
        public List<AchievementCategory> Achievements { get; set; }

        /// <summary>
        /// Search for an achievement by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Achievement this[int id]
        {
            get
            {
                Achievement match = null;
                if (Achievements != null)
                    foreach (AchievementCategory cat in Achievements)
                {
                    match = cat[id];
                    if (match != null) break;
                }

                return match;
            }
        }
        /// <summary>
        /// Search for an achievement by title
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Achievement this[string title]
        {
            get
            {
                Achievement match = null;
                if (Achievements != null)
                    foreach (AchievementCategory cat in Achievements)
                    {
                        match = cat[title];
                        if (match != null) break;
                    }

                return match;
            }
        }
    }

    [DataContract]
    public class AchievementCategory
    {
        [XmlElement("id")]
        [DataMember(Name="id")]
        public int Id { get; set; }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [XmlArray("achievements")]
        [XmlArrayItem("item")]
        [DataMember(Name = "achievements")]
        public List<Achievement> Achievements { get; set; }

        [XmlArray("categories")]
        [XmlArrayItem("item")]
        [DataMember(Name = "categories")]
        public List<AchievementCategory> Categories { get; set; }

        public override string ToString()
        {
            return "Achievements " + Name + "#" + Id;
        }

        public Achievement this[int id]
        {
            get
            {                
                Achievement match = Achievements.First( a => a.Id == id );
                if (match != null) return match;

                if (Categories!=null)
                foreach (AchievementCategory cat in Categories)
                {
                    match = cat[id];
                    if (match != null) break;
                }

                return match;
            }
        }

        /// <summary>
        /// Find first achievement with a title containing the text
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Achievement this[string title]
        {
            get
            {
                Achievement match = Achievements.First(a => a.Title.Contains(title));

                if (match != null) return match;

                if (Categories != null)
                    foreach (AchievementCategory cat in Categories)
                    {
                        match = cat[title];
                        if (match != null) break;
                    }

                return match;
            }
        }
    }

    [DataContract]
    public class Achievement
    {
        [XmlElement("id")]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [XmlElement("title")]
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [XmlElement("points")]
        [DataMember(Name = "points")]
        public int Points { get; set; }

        [XmlElement("description")]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [XmlElement("reward")]
        [DataMember(Name = "reward")]
        public string Reward { get; set; }

        [XmlElement("rewardItems")]
        [DataMember(Name = "rewardItems")]
        public List<Item> RewardItems { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; } 

        [DataMember(Name="criteria")]
        public List<Criteria> Criteria { get; set; } 

        public override string ToString()
        {
            return this.Title + ": " + this.Description;
        }
    }

    [DataContract]
    public class Criteria
    {
        [DataMember(Name="id")]
        public int Id { get; set; }

        [DataMember(Name="description")]
        public string Description { get; set; }

    }
}
