using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;
using System.ComponentModel;
using System.Diagnostics;
namespace BattleNet.API
{
    [Serializable]
    public struct UnixTimestamp : IXmlSerializable
    {
        private static DateTimeOffset origin = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        DateTimeOffset time;

        public UnixTimestamp(long msec)
        {
            time = DateTime.MinValue;
            FromMsec(msec);
        }

        public DateTimeOffset Time { get { return time; } }

        private void FromMsec(long msec)
        {
            time = origin.AddSeconds(msec / 1000.0);            
        }
        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            // there is no schema.. yet
            throw new NotImplementedException();
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            long utc = reader.ReadElementContentAsLong();
            FromMsec(utc);
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class ToHex
    {
        public static string ToHexString(byte[] data)
        {
            StringBuilder hex = new StringBuilder(data.Length * 2);

            for (int i = 0; i < data.Length; i++)
                hex.Append(data[i].ToString("X2"));

            return hex.ToString();
        }
    }
}
