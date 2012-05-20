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
    class GuildTest
    {
        string ReadData(string file)
        {
            return new StreamReader(File.Open(file, FileMode.Open)).ReadToEnd();
        }

        [Test]
        public void TestUTF8Encoding()
        {
            /*
             * Test to make sure text is handled as UTF8 correctly
             */            

            string test = ReadData("data/guild/utf8.json");
            Guild g;
            
            // From the EU guild "Гордунни", "Аструм"
            g = JsonParser.Parse<Guild>(test);

            Assert.NotNull(g);
            Assert.NotNull(g.Members);
            Assert.AreEqual(224,g.Members.Count);
            Assert.AreEqual("Гордунни", g.Realm);
            Assert.AreEqual("Аструм", g.Name);
            Assert.AreEqual("Корвин", g.Members[0].Character.Name);
            
        }
    }
}
