using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Runtime.Serialization;

namespace BattleNet.API
{
    public class CacheCollection
    {
        Dictionary<string, Cache> caches = new Dictionary<string, Cache>();

        public CacheCollection()
        {
        }
        public Cache this[string name]
        {
            get 
            {
                Cache cache = null;
                caches.TryGetValue(name, out cache);
                return cache;
            }
            set
            {
                caches[name] = value;
            }
        }
    }
    /// <summary>
    /// Simple cache to store retrieved data
    /// </summary>
    public class Cache
    {
        string cachePath;
        Dictionary<string, CacheItem> cache = new Dictionary<string, CacheItem>();

        public string CachePath
        {
            get { return cachePath; }
        }
        /// <summary>
        /// Opens a cache in the selected diretory
        /// </summary>
        /// <param name="storePath"></param>
        public Cache(string storePath)
        {
            cachePath = storePath;
            Prime();
        }

        ~Cache()
        {
            Flush();
        }

        /// <summary>
        /// Prime the cache from the disk
        /// </summary>
        void Prime()
        {
            lock (cache)
            {
                cache.Clear();                
                if (!Directory.Exists(cachePath))
                {
                    Directory.CreateDirectory(cachePath);
                }
                using (Stream st = File.Open(cachePath + "/cache.index", FileMode.OpenOrCreate))
                {
                    while (st.Position != st.Length)
                    {
                        CacheItem c = CacheItem.Create(st, this);                        
                        cache.Add(c.Key, c);
                    }
                }                
            }
        }

        void Flush()
        {
            // flush cache to disk
            lock (cache)
            {                
                using (Stream st = File.Open(cachePath + "/cache.index", FileMode.Create))
                {
                    foreach (CacheItem ci in cache.Values)
                    {
                        ci.Write(st);                        
                    }
                    st.Flush();
                }
            }
        }

        public CacheItem Insert(string key, Stream val)
        {
            return Insert(key, val, DateTime.UtcNow);
        }

        public CacheItem Insert(string key, Stream val, DateTime created)
        {
            CacheItem ci = null;
            if (cache.TryGetValue(key, out ci))
            {
                // existing item, just update the value
                ci.Value = val;
            }
            else
            {
                
                // new cache item
                ci = new CacheItem(this,key, val, DateTime.MinValue, TimeSpan.Zero, created);
                cache[key] = ci;
            }

            return ci;
        }


        public CacheItem GetItem(string key)
        {
            CacheItem ci = null;
            if (cache.TryGetValue(key, out ci))
            {
                ci.LastAccessed = DateTime.UtcNow;
            }
            return ci;
        }
    }

    /// <summary>
    /// Item in the Cache
    /// </summary>    
    public class CacheItem 
    {
        
        static SHA1 hashAlgo = new SHA1CryptoServiceProvider();

        private string key;
        
        Cache cache;        
        string hashedFileName;
        TimeSpan idle;

        public static CacheItem Create(Stream st, Cache c)
        {
            BinaryReader r = new BinaryReader(st);
            {
                string key = r.ReadString();
                DateTime abs = DateTime.FromBinary(r.ReadInt64());
                TimeSpan idle = TimeSpan.FromTicks(r.ReadInt64());                
                CacheItem ci = new CacheItem(c, key, abs, idle);

                ci.LastAccessed = DateTime.FromBinary(r.ReadInt64());
                ci.Created = DateTime.FromBinary(r.ReadInt64());
                return ci;
            }
        }
        public void Write(Stream st)
        {
            BinaryWriter w = new BinaryWriter(st);
            {
                w.Write(this.key);
                w.Write( Expire.ToBinary() );
                w.Write( IdleTime.Ticks );
                w.Write( LastAccessed.ToBinary() );
                w.Write( Created.ToBinary() );
            }
        }
        private CacheItem(Cache c, string key, DateTime abs, TimeSpan idle)
        {
            cache = c;
            Key = key;

            if (idle == TimeSpan.Zero)
            {
                Expire = abs;
            }
            else
            {
                Expire = Created + idle;
            }
        }

        public CacheItem(Cache c, string key, Stream val, DateTime abs, TimeSpan idle) :
            this(c,key,val,abs,idle, DateTime.UtcNow)
        {
        }

        public CacheItem(
            Cache c,string key, Stream val, DateTime abs, TimeSpan idle, DateTime created)
        {
            cache = c;
            Key = key;
             
            
            Value = val;
            LastAccessed = Created = created;
            if (idle == TimeSpan.Zero)
            {
                Expire = abs;
            }
            else
            {
                Expire = Created + idle;
            }
        }

        public string Key
        {
            get { return key; }
            protected set
            {
                key = value;
                // file name to store
                hashedFileName = ToHex.ToHexString(hashAlgo.ComputeHash(Encoding.ASCII.GetBytes(key)));
            }
        }
        /// <summary>
        /// How long after the last Update/Access before the item is deleted
        /// </summary>
        public TimeSpan IdleTime { get { return idle; }
            set
            {
                // cant use Negative values
                if (value >= TimeSpan.Zero)
                {
                    idle = value;
                    Expire = LastAccessed + IdleTime;
                }
            }
        }

        /// <summary>
        /// Absolute time in UTC when the item is removed
        /// </summary>
        public DateTime Expire { get; set; }

        /// <summary>
        /// When this was created (can set this to LastModified for http)
        /// </summary>
        public DateTime Created { get; protected set; }

        /// <summary>
        /// Time last Accessed/Updated
        /// </summary>
        public DateTime LastAccessed { get; set; }

        public Stream Value{
            get
            {
                LastAccessed = DateTime.UtcNow;
                return File.Open(cache.CachePath + "/"+hashedFileName, FileMode.Open);
            }
            set
            {                
                // flush to disk
                using (Stream s = File.Open(cache.CachePath + "/"+hashedFileName, FileMode.Create))
                {                                        
                    byte[] buff = new byte[4096];
                    int r = 0;                    
                    while( (r = value.Read(buff,0,buff.Length)) > 0)
                    {
                        s.Write(buff,0, r);
                    }
                    s.Flush();
                }

                LastAccessed = DateTime.UtcNow;

                // an idle time was specified.. so update with it
                if (IdleTime != TimeSpan.Zero)
                {
                    Expire = LastAccessed + IdleTime;
                }
            }
        }

    }
}
