using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace BattleNet.API.WoW
{
    public enum PowerType
    {
        [XmlEnum("focus")] Focus,
        [XmlEnum("rage")] Rage,
        [XmlEnum("mana")] Mana,
        [XmlEnum("energy")] Energy,
        [XmlEnum("runic-power")] RunicPower,
    }
    
    public class ClassCollection : ResponseRoot //,IList<Class>, IXmlSerializable
    {
        List<Class> _ = null;
        [XmlArray("classes")]
        [XmlArrayItem("item")]                
        public List<Class> Classes 
        { 
            get{ return _;}
            set{ _ = value; }
        }


        #region IList<Class> Members

        public int IndexOf(Class item)
        {
            return Classes.IndexOf(item);
        }

        public void Insert(int index, Class item)
        {
            Classes.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            Classes.RemoveAt(index);
        }

        public Class this[int index]
        {
            get
            {
                return Classes[index];
            }
            set
            {
                Classes[index] = value;
            }
        }

        #endregion

        #region ICollection<Class> Members

        public void Add(Class item)
        {
            Classes.Add(item);
        }

        public void Clear()
        {
            Classes.Clear();
        }

        public bool Contains(Class item)
        {
            return Classes.Contains(item);
        }

        public void CopyTo(Class[] array, int arrayIndex)
        {
            Classes.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Classes.Count;  }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Class item)
        {
            return Classes.Remove(item);
        }

        #endregion

        #region IEnumerable<Class> Members

        public IEnumerator<Class> GetEnumerator()
        {
            return Classes.GetEnumerator();
        }

        #endregion
/*
        #region IEnumerable Members        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            System.Collections.IEnumerable i = (System.Collections.IEnumerable)this;
            return i.GetEnumerator();
        }        
        #endregion
 */
    }

    public class Class : IComparable<Class>
    {
        [XmlElement("id")]
        public int Id { get; set; }
        [XmlElement("mask")]
        public int Mask { get; set; }
        [XmlElement("powerType")]
        public PowerType PowerType{get;set;}
        [XmlElement("name")]
        public string Name{get;set;}


        #region IComparable<Class> Members

        public int CompareTo(Class other)
        {
            return Id.CompareTo(other.Id);
        }

        #endregion
    }
}
