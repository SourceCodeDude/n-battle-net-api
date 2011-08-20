using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using BattleNet.API.WoW;
using BattleNet.API;

namespace Test
{
    [TestFixture]
    class QuestTest
    {
        [Test]
        public void TestParse()
        {
            string json = Util.ReadData("data/quest.json");

            JsonParser.UseJson = true;
            Quest q = JsonParser.Parse<Quest>(json);
            Assert.NotNull(q);

            JsonParser.UseJson = false;
            q = JsonParser.Parse<Quest>(json);
            Assert.NotNull(q);

        }
    }
}
