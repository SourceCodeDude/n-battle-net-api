using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Net;
using System.Net.Cache;

using System.Drawing;
using System.Security.Cryptography;
using System.Reflection;

namespace BattleNet.API.WoW
{
    public enum Region
    {
        US,
        EU,
        KR,
        TW,
        CN,        
    }

    public class BattleNetClient : IDisposable
    {
        /// <summary>
        /// Called when there is an error parseing the json
        /// </summary>
        public event ParseErrorDelegate ParseError;

        Cache dataCache;
        Cache iconCache;
        Cache thumbNailCache;

        Uri baseUri;
        Uri iconUri;
        Uri thumbnailUri;

        public bool UseCache{get;set;}

        byte[] privateKey = null;
        public string PrivateKey
        {
            get
            {
                return Encoding.UTF8.GetString(privateKey);                
            }
            set
            {                
                privateKey = Encoding.UTF8.GetBytes(value);
            }
        }
        public string PublicKey { get; set; }

        public System.Globalization.CultureInfo Locale { get; set; }
        public BattleNetClient(Region r)
            : this(r, System.Globalization.CultureInfo.CurrentCulture)
        {        
        }

        public BattleNetClient(Region r,System.Globalization.CultureInfo loc)
        {
            UseCache = true;

            // default to current culture
            Locale = loc;

            dataCache = new Cache("./cache");
            iconCache = new Cache("./icons");
            thumbNailCache = new Cache("./thumb");
            
            switch (r)
            {
                case Region.EU:
                    baseUri = new Uri("http://eu.battle.net/api/wow/");
                    iconUri = new Uri("http://eu.media.blizzard.com/wow/icons/");
                    thumbnailUri = new Uri("http://eu.battle.net/static-render/eu/");
                    break;
                case Region.KR:
                    baseUri = new Uri("http://kr.battle.net/api/wow/");
                    iconUri = new Uri("http://kr.media.blizzard.com/wow/icons/");
                    thumbnailUri = new Uri("http://kr.battle.net/static-render/kr/");
                    break;
                case Region.TW:
                    baseUri = new Uri("http://tw.battle.net/api/wow/");
                    iconUri = new Uri("http://tw.media.blizzard.com/wow/icons/");
                    thumbnailUri = new Uri("http://tw.battle.net/static-render/tw/");
                    break;
                case Region.CN:
                    baseUri = new Uri("http://cn.battle.net/api/wow/");
                    // FIXME: what is the path for this.. this isnt correct..
                    iconUri = new Uri("http://cn.media.battle.net/wow/icons/");
                    thumbnailUri = new Uri("http://us.battle.net/static-render/us/");
                    break;
                case Region.US:
                default:    // fallback to the US region
                    baseUri = new Uri("http://us.battle.net/api/wow/");
                    iconUri = new Uri("http://us.media.blizzard.com/wow/icons/");
                    thumbnailUri = new Uri("http://us.battle.net/static-render/us/");
                    break;
            }
        }

        ~BattleNetClient()
        {
            Dispose();
            thumbNailCache = iconCache = dataCache = null;
        }

        #region IDisposable Members


        public void ClearCache()
        {
            dataCache.Clear();
            iconCache.Clear();
            thumbNailCache.Clear();
        }

        public void Dispose()
        {
            if(thumbNailCache!=null) thumbNailCache.Flush();
            if (iconCache != null) iconCache.Flush();
            if (dataCache != null) dataCache.Flush();
            thumbNailCache = iconCache = dataCache = null;
        }

        #endregion

        /// <summary>
        /// Retrieve a thumbnail image for an avatar
        /// </summary>
        /// <param name="path">thumbnail path in a Character object</param>
        /// <returns></returns>
        public Image GetThumbnail(string path)
        {
            // "http://us.battle.net/static-render/us/" + "medivh/66/3930434-avatar.jpg"
            Uri fullPath = new Uri(thumbnailUri,path);            
            Stream st = GetUrl(fullPath, thumbNailCache);

            Image img = new Bitmap(st);
            return img;
        }

        /// <summary>
        /// Download the icon for a item
        /// </summary>
        /// <param name="path">path to icon, ie inv_misc_necklacea9.jpg</param>
        /// <param name="large">true of a 56x56 and false for 18x18</param>
        /// <returns></returns>
        public Image GetIcon(string path, bool large=true)
        {
            string size = large ? "56/" : "18/";

            Uri fullPath = new Uri(iconUri, size + path);
            // baseUri + "static-render/us"
            Stream st = GetUrl(fullPath, iconCache);

            Image img = new Bitmap(st);            
            return img;
        }

        public Stream GetUrl(string url, Cache cache)
        {
            return GetUrl(new Uri(baseUri, url), cache);
        }

        private Stream GetUrl(Uri url, Cache cache)
        {
            HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
            string key = url.ToString();

            CacheItem ci = null;
            
            if(UseCache) ci = cache.GetItem(key);            

            // item was already cache?
            // see if the server has a newer copy by sending
            // if-modified-since
            if (ci != null)
            {
                req.IfModifiedSince = ci.LastUpdated;
            }
            
            // do any authentication
            Authenticate(req);
            WebResponse res=null;
            try
            {
                // only get a request if it wasnt cached,
                // or the cache item is expired.. and even then
                // we ask the serer if it has a newer version of the
                // item or not
                if (ci == null || ci.Expire < DateTime.UtcNow)
                {
                    res = req.GetResponse();
                    if (UseCache)
                    {
                        ci = cache.Insert(key, res.GetResponseStream());

                        // response had a cache expires header, so lets use it
                        // this usualy happens for the DATA APIs
                        string exp = res.Headers["Expires"];
                        if (exp != null)
                        {
                            ci.Expire = DateTime.Parse(exp).ToUniversalTime();
                        }
                    }
                    else
                    {
                        //Console.WriteLine("Bypass cache");
                    }
                }
                else
                {
                    //Console.WriteLine("Not Expired " + url);
                }
            }
            catch (WebException ex)
            {                
                HttpWebResponse h = ex.Response as HttpWebResponse;
                if (h.StatusCode != HttpStatusCode.NotModified)
                {
                    //throw ex;
                    string txt = new StreamReader( h.GetResponseStream() ).ReadToEnd();
                    ResponseRoot r = JsonParser.Parse<ResponseRoot>(txt);
                    throw new ResponseException(r.Status, r.Reason);

                }
                //Console.WriteLine("HIT");                
            }

            if (UseCache)
            {
                return ci.Value;
            }
            else
            {
                return res.GetResponseStream();
            }
        }

        internal T GetObject<T>(string url)
        {
            Stream st = GetUrl(url, dataCache);
            string json = new StreamReader(st).ReadToEnd();
            return JsonParser.Parse<T>(json, Parse_Error);            
        }

        private void Parse_Error(string msg)
        {
            ParseErrorDelegate pe = ParseError;
            if (pe != null)
            {
                pe(msg);
            }
        }

        #region Data Access API
        
        public IList<AuctionFile> GetAuctions(string realm)
        {
            AuctionResponse resp = GetObject<AuctionResponse>("auction/data/" + HttpUtility.UrlPathEncode(realm));
            foreach (AuctionFile f in resp.Files)
            {
                f.Client = this;
            }
            return resp.Files;
        }

        public RaceCollection Races
        {
            get
            {
                RacesQuery q = new RacesQuery();
                RaceCollection o = GetObject<RaceCollection>(q.ToString());
                // sort by id
                o.Races.Sort();
                return o;               
            }
        }

        public ClassCollection Classes
        {
            get
            {
                ClassesQuery q = new ClassesQuery();
                ClassCollection o = GetObject<ClassCollection>(q.ToString());
                // sort by id
                o.Classes.Sort();
                return o;
            }
        }

        public Item GetItem(int id)
        {
            return GetItem(new ItemQuery()
            {
                ItemId = id
            });
        }

        public Item GetItem(ItemQuery query)
        {
            return GetObject<Item>(query.ToString());
        }

        public List<GuildReward> GuildRewards
        {
            get
            {
                GuildRewardsQuery q = new GuildRewardsQuery();
                GuildRewardCollection o = GetObject<GuildRewardCollection>(q.ToString());
                return o.Rewards;
            }
        }

        public List<GuildPerk> GuildPerks
        {
            get
            {
                GuildPerksQuery q = new GuildPerksQuery();
                GuildPerkCollection o = GetObject<GuildPerkCollection>(q.ToString());
                return o.Perks;
            }
        }

        #endregion

        public List<Realm> RealmStatus()
        {
            return RealmStatus(new RealmQuery() { Locale = Locale });
        }
        public List<Realm> RealmStatus(params string[] slugs)
        {
            return RealmStatus((IEnumerable<string>)slugs);
        }

        public List<Realm> RealmStatus(IEnumerable<string> slugs)
        {
            return RealmStatus(new RealmQuery()
                {
                    Realms = new List<string>(slugs),
                    Locale = Locale
                });   
        }

        public List<Realm> RealmStatus(RealmQuery query)
        {
                
            RealmCollection rc = GetObject<RealmCollection>( query.ToString() );

            return rc.Realms;
        }
        
        /// <summary>
        /// Get Character with basic fields
        /// </summary>
        /// <param name="realm"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Character GetCharacter(string realm, string name)
        {
            return GetCharacter(realm, name, CharacterFields.Basic);
        }

        public Guild GetGuild(string realm, string name)
        {
            return GetGuild(realm, name, GuildFields.Basic);
        }

        public Guild GetGuild(string realm, string name, GuildFields fields)
        {
            return GetGuild( new GuildQuery(){
                Realm = realm,
                Name = name,
                Fields = fields,
                Locale = Locale
            });
        }

        public Guild GetGuild(GuildQuery query)
        {
            string url = query.ToString();
            
            Guild g = GetObject<Guild>(url);

            if (g.Status == Status.Ok)
            {
                return g;
            }
            else
            {
                throw new ResponseException(g.Status, g.Reason);
            }
        }

        /// <summary>
        /// Get Character with optional fields
        /// </summary>
        /// <param name="realm"></param>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public Character GetCharacter(string realm, string name, CharacterFields fields)
        {
            return GetCharacter(new CharacterQuery
            {
                Realm = realm,
                Name = name,
                Fields = fields,
                Locale = Locale,
            });
        }

        public Character GetCharacter(CharacterQuery query)
        {            
            string url =  query.ToString();

            Character r = GetObject<Character>(url);
            
            if (r!=null && r.Status == Status.Ok)
            {
                return r;
            }
            else
            {
                throw new ResponseException(r.Status, r.Reason);               
            }
        }


        public void Authenticate(HttpWebRequest req)
        {
            // no key set, so dont even try
            if (privateKey == null) return;

            // use the same date through out
            DateTime date = DateTime.Now.ToUniversalTime();

            //  To make this v2.0 to v3.5 friendly we have to use a little reflection
            // to set the "Date" header.  v4.0 gives us the Date property to do it.
            //            

            // use the Protected method to set the value
            Type type = req.Headers.GetType();
            BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
            MethodInfo method = type.GetMethod("AddWithoutValidate", flags);
            method.Invoke(req.Headers, new[] { "Date", date.ToString("r") });

            // this is .net v4.0 only
            //req.Date = DateTime.Now;
            

            string dateStr = date.ToString("r");
            string stringToSign = req.Method + "\n" +
                dateStr + "\n" +
                req.RequestUri.LocalPath+"\n";
            
            byte[] key= new byte[32];
            byte[] buffer = Encoding.UTF8.GetBytes(stringToSign);
            HMACSHA1 hmac = new HMACSHA1(privateKey);
            byte[] hash = hmac.ComputeHash(buffer);
            string sig = Convert.ToBase64String(hash);

            string auth = "BNET " +  PublicKey + ":" + sig;

            req.Headers["Authorization"] = auth;            
        }
    }

    
}
