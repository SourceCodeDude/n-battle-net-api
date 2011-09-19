using System;
namespace BattleNet.API
{
    public interface ICache
    {
        string CachePath { get; }
        void Clear();
        void Flush();
        CacheItem GetItem(string key);
        CacheItem Insert(string key, System.IO.Stream val);
        CacheItem Insert(string key, System.IO.Stream val, DateTime created, DateTime expire);

        int MaxItems { get; set; }
    }
}
