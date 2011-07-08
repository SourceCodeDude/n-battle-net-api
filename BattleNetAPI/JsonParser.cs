using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

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
            T o = (T)s.Deserialize(rd);
            return o;
        }
    }
}
