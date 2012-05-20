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
            Quest q = JsonParser.Parse<Quest>(json);
            Assert.NotNull(q);

        }
    }
}
