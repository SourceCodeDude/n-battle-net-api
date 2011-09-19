using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using BattleNet.API.WoW;
using BattleNet.API;

using System.IO;

namespace Test
{
    [TestFixture]
    class CacheTest
    {

        [Test]
        public void TestPrune()
        {
            // ensure we have a clean directory
            if (Directory.Exists("testCache"))
            {
                Directory.Delete("testCache", true);
            }
            Cache cache = new Cache("testCache");
            for (int i = 0; i < 110; i++)
            {
                MemoryStream ms = new MemoryStream( ASCIIEncoding.ASCII.GetBytes("test " + i));                
                CacheItem ci = cache.Insert(""+i, ms, DateTime.UtcNow, DateTime.MaxValue);                
            }

            for (int i = 0; i < 110; i++)
            {
                CacheItem ci  = cache.GetItem("" + i);
                if (i < 10)
                    Assert.IsNull(ci);
                else
                    Assert.IsNotNull(ci);

                //Console.WriteLine("Item "+i+" was: " + ci);
            }
        }
    }
}
