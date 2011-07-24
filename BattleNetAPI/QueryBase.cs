using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

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
            System.Collections.Specialized.NameValueCollection q = new System.Collections.Specialized.NameValueCollection();
            BuildQuery(q);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < q.Count;i++ )
            {
                string key = q.GetKey(i);
                string value = q[i];
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
        protected virtual void BuildQuery(System.Collections.Specialized.NameValueCollection query)
        {
            if (Locale != null)
            {
                query.Add("locale", Locale.Name);
            }
        }
    }
}
