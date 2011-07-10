﻿using System;
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

using System.Security.Cryptography;

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
        
        FailoverCache cache;
        Uri baseUri;

        byte[] privateKey = null;
        public string PrivateKey
        {
            get
            {
                StringBuilder hex = new StringBuilder(privateKey.Length * 2);

                for (int i = 0; i < privateKey.Length; i++) 
                    hex.Append(privateKey[i].ToString("X2"));

                return hex.ToString();  
            }
            set
            {
                int NumberChars = value.Length;
                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                    bytes[i / 2] = Convert.ToByte(value.Substring(i, 2), 16);
                privateKey = bytes;
            }
        }
        public string PublicKey { get; set; }

        public BattleNetClient(Region r)
        {
            cache = new FailoverCache("battlenet.cache");
            switch (r)
            {
                case Region.EU:
                    baseUri = new Uri("http://eu.battle.net/api/wow/"); 
                    break;
                case Region.KR:
                    baseUri = new Uri("http://kr.battle.net/api/wow/");
                    break;
                case Region.TW:
                    baseUri = new Uri("http://tw.battle.net/api/wow/");
                    break;
                case Region.CN:
                    baseUri = new Uri("http://cn.battle.net/api/wow/");
                    break;
                case Region.US:
                default:    // fallback to the US region
                    baseUri = new Uri("http://us.battle.net/api/wow/");
                    break;
            }
        }

        private string GetUrl(string url)
        {
            return GetUrl(new Uri(baseUri, url));
        }
        private string GetUrl(Uri url)
        {
            string txt = null;
             //cache[url.AbsolutePath];
            if (txt == null)
            {
                HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);
                Authenticate(req);
                req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                WebResponse res = req.GetResponse();
                                
                Stream st = res.GetResponseStream();
                StreamReader r = new StreamReader(st);
                txt = r.ReadToEnd();

                cache[url.AbsolutePath] = txt;
            }
            return txt;
        }

        private T GetObject<T>(string url)
        {            
            string json = GetUrl(url);
            return JsonParser.Parse<T>(json);            
        }

        
        public List<Race> Races
        {
            get
            {
                RaceCollection o = GetObject<RaceCollection>("data/character/races");
                return o.Races;               
            }
        }

        public List<Class> Classes
        {
            get
            {
                ClassCollection o = GetObject<ClassCollection>("data/character/classes");
                return o.Classes;
            }
        }

        public Item FindItem(int id)
        {
            return GetObject<Item>("data/item/" + id);
        }

        public List<GuildReward> GuildRewards
        {
            get
            {
                GuildRewardCollection o = GetObject<GuildRewardCollection>("data/guild/rewards");
                return o.Rewards;
            }
        }

        public List<GuildPerk> GuildPerks
        {
            get
            {
                GuildPerkCollection o = GetObject<GuildPerkCollection>("data/guild/perks");
                return o.Perks;
            }
        }
        
        public List<Realm> RealmStatus(params string[] slugs)
        {
            return RealmStatus((IEnumerable<string>)slugs);
        }

        public List<Realm> RealmStatus(IEnumerable<string> slugs)
        {
            string[] s = slugs.ToArray();
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = HttpUtility.UrlEncode(s[i]);
            }            
            RealmCollection rc = GetObject<RealmCollection>("realm/status?realms=" + string.Join(",",s) );

            return rc.Realms;
        }
        public Realm RealmStatus(string slug)
        {
            slug = HttpUtility.UrlEncode(slug);
            RealmCollection rc = GetObject<RealmCollection>("realm/status?realms=" + slug);

            if (rc.Status == Status.Ok)
            {
                if (rc.Realms.Count == 1)
                    return rc.Realms[0];
                else
                {
                    throw new RealmNotFoundException(slug + " not found");
                }
            }

            throw new ResponseException(rc.Status, rc.Reason);
        }

        /// <summary>
        /// Get Character with basic fields
        /// </summary>
        /// <param name="realm"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Character FindCharacter(string realm, string name)
        {
            return FindCharacter(realm, name, Character.Fields.Basic);
        }

        public Guild FindGuild(string realm, string name)
        {
            return FindGuild(realm, name, Guild.Fields.Basic);
        }

        public Guild FindGuild(string realm, string name, Guild.Fields fields)
        {
            List<string> args = new List<string>();
            if ((fields & Guild.Fields.Achievements) == Guild.Fields.Achievements) args.Add("achievements");
            if ((fields & Guild.Fields.Members) == Guild.Fields.Members) args.Add("members");

            string _f = string.Join(",", args.ToArray());

            string url = "guild/" + realm + "/" + name;
            if (_f != "") url += "?fields=" + _f;            
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
        public Character FindCharacter(string realm, string name, Character.Fields fields)
        {
            List<string> args = new List<string>();
            if ((fields & Character.Fields.Achievements) == Character.Fields.Achievements) args.Add("achievements");
            if ((fields & Character.Fields.Appearance) == Character.Fields.Appearance) args.Add("appearance");
            if ((fields & Character.Fields.Companions) == Character.Fields.Companions) args.Add("companions");
            if ((fields & Character.Fields.Items) == Character.Fields.Items) args.Add("items");
            if ((fields & Character.Fields.Mounts) == Character.Fields.Mounts) args.Add("mounts");
            if ((fields & Character.Fields.Pets) == Character.Fields.Pets) args.Add("pets");
            if ((fields & Character.Fields.Professions) == Character.Fields.Professions) args.Add("professions");
            if ((fields & Character.Fields.Progression) == Character.Fields.Progression) args.Add("Progression");
            if ((fields & Character.Fields.Reputation) == Character.Fields.Reputation) args.Add("reputation");
            if ((fields & Character.Fields.Stats) == Character.Fields.Stats) args.Add("stats");
            if ((fields & Character.Fields.Talents) == Character.Fields.Talents) args.Add("talents");
            if ((fields & Character.Fields.Titles) == Character.Fields.Titles) args.Add("titles");
            if ((fields & Character.Fields.Guild) == Character.Fields.Guild) args.Add("guild");

            string _f = string.Join(",", args.ToArray() );

            string url = "character/" + realm + "/" + name;
            if (_f != "") url += "?fields=" + _f;

            Character r = GetObject<Character>(url);
            // ?fields=achievements
            if (r.Status == Status.Ok)
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

            // BOOO!!! http://stackoverflow.com/questions/1140553/asp-net-httpwebrequest-date-header-workaround
            // apparently HttpWebRequest will NEVER send the DATE header
            // MS only fixed this in .NET 4.0, so thats what we have to use..
            req.Date = DateTime.Now;
            // this is the only reason for 4.0

            string date = req.Date.ToUniversalTime().ToString("r");
            string stringToSign = req.Method + "\n" +
                date + "\n" +
                req.RequestUri.PathAndQuery+"\n";
                      

            byte[] key= new byte[32];
            byte[] buffer = Encoding.UTF8.GetBytes(stringToSign);
            HMACSHA1 hmac = new HMACSHA1(privateKey);
            byte[] hash = hmac.ComputeHash(buffer);
            string sig = Convert.ToBase64String(hash);

            string auth = "BNET " + "publickey" + ":" + sig;

            req.Headers["Authorization"] = auth;
            Console.WriteLine(req.Headers);
        }
        #region IDisposable Members

        public void Dispose()
        {
            cache.SaveIndex();
        }

        #endregion
    }

    
}
