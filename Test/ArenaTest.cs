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
    class ArenaTest
    {

        [Test]
        public void TestBasic()
        {
            string json = Util.ReadData("data/arena/arena.json");
            ArenaTeam t;
            
            t = JsonParser.Parse<ArenaTeam>(json);
            Assert.NotNull(t);

        }

    }
}
