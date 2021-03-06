﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using BattleNet.API.WoW;
using BattleNet.API;

namespace Test
{
    [TestFixture]
    class AchievementsTest
    {

        [Test]
        public void TestSingleParse()
        {
            string json = Util.ReadData("data/achievement/2144.json");
            Achievement t;

            t = JsonParser.Parse<Achievement>(json);
            Assert.NotNull(t);

        }


        [Test]
        public void TestParse()
        {
            string json = Util.ReadData("data/achievements.json");
            AchievementCollection t;

            t = JsonParser.Parse<AchievementCollection>(json);
            Assert.NotNull(t);

        }

        [Test]
        public void TestIndex()
        {
            string json = Util.ReadData("data/achievements.json");
            AchievementCollection t;

            t = JsonParser.Parse<AchievementCollection>(json);
            Assert.NotNull(t);

            Achievement ach = t[5012];
            Assert.NotNull(ach);

            ach = t["Classic Dungeonmaster"];
            Assert.NotNull(t);
            
        }
    }
}
