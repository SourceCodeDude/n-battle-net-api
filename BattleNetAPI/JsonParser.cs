using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

using BattleNet.API.WoW;
using System.Web.Script.Serialization;

namespace BattleNet.API
{
    public delegate void ParseErrorDelegate(string message);

    public class MyJavaScriptSerializer : JavaScriptSerializer
    {
        ParseErrorDelegate error;
        public MyJavaScriptSerializer(ParseErrorDelegate e) : base()
        {
            error = e;
        }

        public void OnError(string msg)
        {
            if (error != null)
            {
                error(msg);
            }
        }
    }
    public class JsonParser
    {
        static JavaScriptConverter[] converters;        
        static JsonParser()
        {
            converters = new JavaScriptConverter[] { new BattleNet.API.Converter.WowAPIConverter() };
            UseJson = true;
        }

        /// <summary>
        /// Parse as json, or convert to XML first.  Using Json is faster.  XML is tolerant to errors, but slow
        /// </summary>
        static bool UseJson { get; set; }
        static public T Parse<T>(string json)
        {
            return Parse<T>(json, null);
        }

        /// <summary>
        /// Convert Json string into an Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">json string</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        static public T Parse<T>(string json, ParseErrorDelegate onError)
        {
            try
            {
                if (UseJson)
                {
                    JavaScriptSerializer jss = new MyJavaScriptSerializer(onError);
                    // yeah.. its a big number
                    jss.MaxJsonLength = int.MaxValue;
                    jss.RegisterConverters(converters);                    
                    return (T)jss.Deserialize<T>(json);
                }
                else
                {
                    Type t = typeof(T);
                    byte[] b = ASCIIEncoding.ASCII.GetBytes(json);
                    XmlReader rd = JsonReaderWriterFactory.CreateJsonReader(b, new XmlDictionaryReaderQuotas());
                    XmlSerializer s = new XmlSerializer(t, new XmlRootAttribute("root"));
                    return (T)s.Deserialize(rd);
                }
            }
            catch (Exception ex)
            {
                if (onError != null)
                {
                    onError(ex.Message);
                }
                return default(T);
            }
            /*
            StringBuilder sb = new StringBuilder();
            while (rd.Read())
            {
               sb.Append( rd.ReadOuterXml() );
            }

            return default(T);
            */
        }
    }
}
