using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Serialization;

using System.IO;
using BattleNet.API.WoW;
using System.Reflection;
namespace BattleNet.API
{
    public delegate void ParseErrorDelegate(string message);

    public interface IJsonParser
    {
        T Deserialize<T>(string txt);
        ParseErrorDelegate Error { get; set; }
    }

    public class XmlJsonParser : IJsonParser
    {

        #region IJsonParser Members

        public ParseErrorDelegate Error { get; set; }

        public T Deserialize<T>(string json)
        {
            Type t = typeof(T);
            byte[] b = System.Text.UTF8Encoding.UTF8.GetBytes(json);
            
            /*
            System.Runtime.Serialization.Json.DataContractJsonSerializer s = new DataContractJsonSerializer(t);
            T ret = (T)s.ReadObject(new MemoryStream(b));
            return ret;
            */
            XmlReader rd = JsonReaderWriterFactory.CreateJsonReader(b, new XmlDictionaryReaderQuotas());
            XmlSerializer s = new XmlSerializer(t, new XmlRootAttribute("root"));
            return (T)s.Deserialize(rd);
             
            
        }

        #endregion
    }
    public class JsonParser
    {
             
        static JsonParser()
        {
            UseJson = true;
        }


        private static bool useJson;
        /// <summary>
        /// Parse as json, or convert to XML first.  Using Json is faster.  XML is tolerant to errors, but slow
        /// </summary>
        static public bool UseJson
        {
            get { return useJson;  }
            set
            {
                if (value)
                {
                    try
                    {
                        // To remove dependancy on the System.Web.Extensions dll we make an extra assembly for json parsing.                    
                        // if it exists we can use it.. if not.. we fall back to xml
                        //
                        Assembly asm = Assembly.Load("BattleNet.API.Json, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                        Type t = asm.GetType("BattleNet.API.MyJavaScriptSerializer");
                        object o = Activator.CreateInstance(t);
                        parser = (IJsonParser)o;
                    }
                    catch (Exception)
                    {
                        // fall back to xml
                        parser = new XmlJsonParser();
                        value = false;
                    }
                }
                else
                {
                    parser = new XmlJsonParser();
                }
                useJson = value;
            }
        }
        static public T Parse<T>(string json)
        {
            return Parse<T>(json, null);
        }

        static IJsonParser parser = null;
        /// <summary>
        /// Convert Json string into an Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json">json string</param>
        /// <param name="onError"></param>
        /// <returns></returns>
        static public T Parse<T>(string json, ParseErrorDelegate onError)
        {
            //try
            {
                parser.Error = onError;
                return (T)parser.Deserialize<T>(json);
            }
            //catch (Exception ex)
            {
                if (onError != null)
                {
                    //onError(ex.Message);
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
