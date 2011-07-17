using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

using BattleNet.API.WoW;

namespace BattleNet.API
{
    public class JsonParser
    {
        static public T Parse<T>(string json)
        {
            Type t = typeof(T);
            byte[] b = ASCIIEncoding.ASCII.GetBytes(json);            
            XmlReader rd = JsonReaderWriterFactory.CreateJsonReader(b, new XmlDictionaryReaderQuotas());            
            XmlSerializer s = new XmlSerializer(t, new XmlRootAttribute("root"));

            /*
            StringBuilder sb = new StringBuilder();
            while (rd.Read())
            {
               sb.Append( rd.ReadOuterXml() );
            }

            return default(T);
            */

            T o = (T)s.Deserialize(rd);
            return o;
        }
    }
}
