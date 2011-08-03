using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Script.Serialization;

namespace BattleNet.API
{
    public class MyJavaScriptSerializer : JavaScriptSerializer, IJsonParser
    {
        static JavaScriptConverter[] converters;

        static MyJavaScriptSerializer()
        {
            converters = new JavaScriptConverter[] { new BattleNet.API.Converter.WowAPIConverter() };
        }
        
        public MyJavaScriptSerializer() : this(null)
        {

        }

        public MyJavaScriptSerializer(ParseErrorDelegate e)
            : base()
        {
            this.MaxJsonLength = int.MaxValue;
            this.RegisterConverters(converters);                    

            Error = e;
        }

        public ParseErrorDelegate Error { get; set; }
        public void OnError(string msg)
        {
            if (Error != null)
            {
                Error(msg);
            }
        }
    }
}
