using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleNet.API
{
    public class QueryBase
    {
        /// <summary>
        /// Locale to return text strings back.  Note that only certain ones are
        /// valid depending on the server
        /// </summary>
        public System.Globalization.CultureInfo Locale { get; set; }

        override public String ToString()
        {
            IDictionary<string, string> q = new Dictionary<string, string>();
            BuildQuery(q);

            StringBuilder sb = new StringBuilder();

            foreach(KeyValuePair<string,string> kvp in q)            
            {
                string key = kvp.Key;
                string value = kvp.Value;
                if (value == "") continue;                
                sb.AppendFormat("{0}={1}&",
                    Encode(key),
                    Encode(value));                
            }

            return sb.ToString();
        }

        protected string Encode(string s)
        {
            return HttpUtility.UrlPathEncode(s);
        }
        protected virtual void BuildQuery(IDictionary<string, string> query)
        {
            if (Locale != null)
            {
                query.Add("locale", Locale.Name);
            }
        }
    }
}
