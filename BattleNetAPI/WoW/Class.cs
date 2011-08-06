using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.ComponentModel;
namespace BattleNet.API.WoW
{
    class PowerTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            else
                return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            string s = Translate(value as string);            
            return Enum.Parse(typeof(PowerType), s, true);
        }


        public static string Translate(string k)
        {
            switch (k)
            {
                case "focus": return "Focus";
                case "rage": return "Rage";
                case "mana": return "Mana";
                case "energy": return "Energy";
                case "runic-power": return "RunicPower";
                default: return k;
            }
        }
    }

    [TypeConverter(typeof(PowerTypeConverter))]
    [DataContract]
    public enum PowerType
    {
        [XmlEnum("focus")] 
        [EnumMember(Value="focus")]
        Focus,
        [XmlEnum("rage")]
        [EnumMember(Value = "rage")]
        Rage,
        [XmlEnum("mana")]
        [EnumMember(Value = "mana")]
        Mana,
        [XmlEnum("energy")]
        [EnumMember(Value = "energy")]
        Energy,
        [XmlEnum("runic-power")]
        [EnumMember(Value = "runic-power")]
        RunicPower,
    }
    
    [DataContract]
    public class ClassCollection : ResponseRoot //,IList<Class>, IXmlSerializable
    {
        List<Class> _ = null;
        [XmlArray("classes")]
        [XmlArrayItem("item")]   
        [DataMember(Name="classes")]
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

    [DataContract]
    public class Class : IComparable<Class>
    {
        [XmlElement("id")]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [XmlElement("mask")]
        [DataMember(Name = "mask")]
        public int Mask { get; set; }

        [XmlElement("powerType")]        
        public PowerType PowerType{get;set;}

        [DataMember(Name = "powerType")]
        private string powerType
        {
            get { 
            return PowerType.ToString();
            }
            set{
                PowerType = (WoW.PowerType)Enum.Parse(typeof(PowerType),PowerTypeConverter.Translate(value), true );
            }
        }

        [XmlElement("name")]
        [DataMember(Name = "name")]
        public string Name{get;set;}


        #region IComparable<Class> Members

        public int CompareTo(Class other)
        {
            return Id.CompareTo(other.Id);
        }

        #endregion
    }
}
