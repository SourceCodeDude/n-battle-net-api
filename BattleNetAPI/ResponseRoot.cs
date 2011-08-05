using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using System.ComponentModel;
using System.Runtime.Serialization;

namespace BattleNet.API
{
    class StatusTypeConverter : TypeConverter
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
            return Enum.Parse(typeof(Status), s, true);
        }


        string Translate(string k)
        {
            switch (k)
            {
                case "nok": return "NotOk";
                default: return k;
            }
        }
    }

    [TypeConverter(typeof(StatusTypeConverter))]
    public enum Status
    {
        Ok,
        [XmlEnum("nok")]
        NotOk,
    }

    /// <summary>
    /// Base class for a response
    /// </summary>
    [XmlRoot("root")]
    [DataContract]
    public class ResponseRoot
    {
        [XmlElement("status")]
        public Status Status { get; set; }
        [XmlElement("reason", IsNullable = true)]
        public string Reason { get; set; }

    }
}
