using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    public class RaceCollection : ResponseRoot //,IList<Race>
    {
        [XmlArray("races")]
        [XmlArrayItem("item")]
        public List<Race> Races { get; set; }

        #region IList<Race> Members

        public int IndexOf(Race item)
        {
            return Races.IndexOf(item);
        }

        public void Insert(int index, Race item)
        {
            Races.Insert(index,item);
        }

        public void RemoveAt(int index)
        {
            Races.RemoveAt(index);
        }

        public Race this[int index]
        {
            get
            {
                return Races[index];
            }
            set
            {
                Races[index] = value;
            }
        }

        #endregion

        #region ICollection<Race> Members

        public void Add(Race item)
        {
            Races.Add(item);
        }

        public void Clear()
        {
            Races.Clear();
        }

        public bool Contains(Race item)
        {
            return Races.Contains(item);
        }

        public void CopyTo(Race[] array, int arrayIndex)
        {
            Races.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Races.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Race item)
        {
            return Races.Remove(item);
        }

        #endregion

        #region IEnumerable<Race> Members

        public IEnumerator<Race> GetEnumerator()
        {
            return Races.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members
        /*
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            System.Collections.IEnumerable i = (System.Collections.IEnumerable)Races;
            return i.GetEnumerator();
        }
         * */

        #endregion
    }

    public class Race
    {
        [XmlElement("id")]      public int Id { get; set; }
        [XmlElement("mask")]    public int Mask { get; set; }
        [XmlElement("side")]    public string Side { get; set; }
        [XmlElement("name")]    public string Name { get; set; }
    }
}
