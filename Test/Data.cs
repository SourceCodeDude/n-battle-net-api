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
    class Data
    {
        [Test]
        public void ParseRaceCollection()
        {
            string test = Test.Resource.race1;

            RaceCollection rc = JsonParser.Parse<RaceCollection>(test);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.races);
            Assert.AreEqual(12, rc.races.Count);

            Assert.AreEqual(3,          rc.races[0].id);
            Assert.AreEqual(4,          rc.races[0].mask);
            Assert.AreEqual("Dwarf",    rc.races[0].name);
            Assert.AreEqual("alliance", rc.races[0].side);


            Assert.AreEqual(6,          rc.races[1].id);
            Assert.AreEqual(32,         rc.races[1].mask);
            Assert.AreEqual("Tauren",   rc.races[1].name);
            Assert.AreEqual("horde",    rc.races[1].side);
        }

        [Test]
        public void ParseClassCollection()
        {
            string test = Test.Resource.classes;

            ClassCollection rc = JsonParser.Parse<ClassCollection>(test);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Classes);
            Assert.AreEqual(10, rc.Classes.Count);

            Assert.AreEqual(3, rc.Classes[0].id);
            Assert.AreEqual(4, rc.Classes[0].mask);
            Assert.AreEqual("Hunter", rc.Classes[0].name);
            Assert.AreEqual(PowerType.Focus, rc.Classes[0].powerType);
        }


        [Test]
        public void ParseGuildRewards()
        {
            string test = Test.Resource.guildrewards;
            GuildRewardCollection rc = JsonParser.Parse<GuildRewardCollection>(test);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Rewards);
            Assert.AreEqual(2, rc.Rewards.Count);

            Assert.AreEqual(0, rc.Rewards[0].minGuildLevel);
            Assert.AreEqual(6, rc.Rewards[0].minGuildRepLevel);
            Assert.AreEqual(6, rc.Rewards[0].races.Count);
            Assert.IsNotNull(rc.Rewards[0].achievement);

            Assert.AreEqual(5035, rc.Rewards[0].achievement.id);
            Assert.AreEqual("Master Crafter", rc.Rewards[0].achievement.title);
            Assert.AreEqual(10, rc.Rewards[0].achievement.points);
            Assert.AreEqual("Craft 500 Epic items with an item level of at least 359.", rc.Rewards[0].achievement.description);
            Assert.AreEqual("Reward: Cloak of Coordination", rc.Rewards[0].achievement.reward);
            Assert.IsNotNull(rc.Rewards[0].achievement.rewardItem);

            Assert.AreEqual(65360, rc.Rewards[0].achievement.rewardItem.id);
            Assert.AreEqual("Cloak of Coordination", rc.Rewards[0].achievement.rewardItem.name);
            Assert.AreEqual("inv_guild_cloak_alliance_c", rc.Rewards[0].achievement.rewardItem.icon);
            Assert.AreEqual(4, rc.Rewards[0].achievement.rewardItem.quality);
            Assert.IsNotNull(rc.Rewards[0].achievement.rewardItem.tooltipParams);


            Assert.IsNotNull(rc.Rewards[0].item);
            Assert.AreEqual(65274, rc.Rewards[0].item.id);
            Assert.AreEqual("Cloak of Coordination", rc.Rewards[0].item.name);
            Assert.AreEqual("inv_guild_cloak_horde_c", rc.Rewards[0].item.icon);
            Assert.AreEqual(4, rc.Rewards[0].item.quality);
            Assert.IsNotNull(rc.Rewards[0].item.tooltipParams);
        }

        [Test]
        public void ParseGuildPerks()
        {
            string test = Test.Resource.perks;
            GuildPerkCollection rc = JsonParser.Parse<GuildPerkCollection>(test);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Perks);
            Assert.AreEqual(2, rc.Perks.Count);

            Assert.AreEqual(2, rc.Perks[0].guildLevel);
            Assert.IsNotNull(rc.Perks[0].spell);
            Assert.AreEqual(78631, rc.Perks[0].spell.id);
            Assert.AreEqual("Fast Track", rc.Perks[0].spell.name);
            Assert.AreEqual("Rank 1", rc.Perks[0].spell.subtext);
            Assert.AreEqual("achievement_guildperk_fasttrack", rc.Perks[0].spell.icon);
            Assert.AreEqual("Experience gained from killing monsters and completing quests increased by 5%.", rc.Perks[0].spell.description);            
        }
    }
}
