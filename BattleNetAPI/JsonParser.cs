using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using System.IO;
using BattleNet.API.WoW;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BattleNet.API
{
    public delegate void ParseErrorDelegate(string message);

    public interface IJsonParser
    {
        T Deserialize<T>(string txt);
        ParseErrorDelegate Error { get; set; }
    }

    class EventConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FeedEvent);                
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            switch(obj["type"].ToString())
            {
                case "LOOT":
                    return serializer.Deserialize<LootFeedEvent>(obj.CreateReader());
                case "ACHIEVEMENT":
                    return serializer.Deserialize<AchievementFeedEvent>(obj.CreateReader());
                case "CRITERIA":
                    return serializer.Deserialize<CriteriaFeedEvent>(obj.CreateReader());
                case "BOSSKILL":
                    return serializer.Deserialize<BosskillFeedEvent>(obj.CreateReader());
            }

            throw new InvalidOperationException("Unknown feed type " + obj["type"].ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
    public class JsonNetParser : IJsonParser
    {
        JsonConverter convert;
        public JsonNetParser()
        {
            convert = new EventConverter();
        }

        #region IJsonParser Members

        public T Deserialize<T>(string txt)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(txt, convert);
            }
            catch (Exception ex)
            {
                if (Error == null)
                {
                    throw ex;
                }
                else
                {
                    Error(ex.Message);
                    return default(T);
                }
            }
        }

        public ParseErrorDelegate Error
        {
            get;
            set;
        }

        #endregion
    }

    public class JsonParser
    {
             
        static JsonParser()
        {
            parser = new JsonNetParser();
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
            try
            {
                parser.Error = onError;
                return parser.Deserialize<T>(json);
            }
            catch (Exception ex)
            {
                if (onError != null)
                {
                    onError(ex.Message);
                    return default(T);
                }
                else
                {
                    // rethrow exception if no handler was defined
                    throw ex;
                    
                }
            }
        }
    }
}
