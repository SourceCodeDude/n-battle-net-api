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

            RaceCollection rc = JsonParser.Parse<RaceCollection>(test, null);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Races);
            Assert.AreEqual(12, rc.Races.Count);

            Assert.AreEqual(3,          rc.Races[0].Id);
            Assert.AreEqual(4,          rc.Races[0].Mask);
            Assert.AreEqual("Dwarf",    rc.Races[0].Name);
            Assert.AreEqual("alliance", rc.Races[0].Side);


            Assert.AreEqual(6,          rc.Races[1].Id);
            Assert.AreEqual(32,         rc.Races[1].Mask);
            Assert.AreEqual("Tauren",   rc.Races[1].Name);
            Assert.AreEqual("horde",    rc.Races[1].Side);
        }

        [Test]
        public void ParseClassCollection()
        {
            string test = Util.ReadData("data/classes.json");            

            ClassCollection rc = JsonParser.Parse<ClassCollection>(test);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Classes);
            Assert.AreEqual(10, rc.Classes.Count);

            Assert.AreEqual(3, rc.Classes[0].Id);
            Assert.AreEqual(4, rc.Classes[0].Mask);
            Assert.AreEqual("Hunter", rc.Classes[0].Name);
            Assert.AreEqual(PowerType.Focus, rc.Classes[0].PowerType);
        }


        [Test]
        public void ParseGuildRewards()
        {
            string test = Test.Resource.guildrewards;
            GuildRewardCollection rc = JsonParser.Parse<GuildRewardCollection>(test);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Rewards);
            Assert.AreEqual(2, rc.Rewards.Count);

            Assert.AreEqual(0, rc.Rewards[0].MinGuildLevel);
            Assert.AreEqual(6, rc.Rewards[0].MinGuildRepLevel);
            Assert.AreEqual(6, rc.Rewards[0].Races.Count);
            Assert.IsNotNull(rc.Rewards[0].Achievement);

            Assert.AreEqual(5035, rc.Rewards[0].Achievement.Id);
            Assert.AreEqual("Master Crafter", rc.Rewards[0].Achievement.Title);
            Assert.AreEqual(10, rc.Rewards[0].Achievement.Points);
            Assert.AreEqual("Craft 500 Epic items with an item level of at least 359.", rc.Rewards[0].Achievement.Description);
            Assert.AreEqual("Reward: Cloak of Coordination", rc.Rewards[0].Achievement.Reward);
            Assert.IsNotNull(rc.Rewards[0].Achievement.RewardItem);

            Assert.AreEqual(65360, rc.Rewards[0].Achievement.RewardItem.Id);
            Assert.AreEqual("Cloak of Coordination", rc.Rewards[0].Achievement.RewardItem.Name);
            Assert.AreEqual("inv_guild_cloak_alliance_c", rc.Rewards[0].Achievement.RewardItem.Icon);
            Assert.AreEqual(Quality.Epic, rc.Rewards[0].Achievement.RewardItem.Quality);
            Assert.IsNotNull(rc.Rewards[0].Achievement.RewardItem.TooltipParams);


            Assert.IsNotNull(rc.Rewards[0].Item);
            Assert.AreEqual(65274, rc.Rewards[0].Item.Id);
            Assert.AreEqual("Cloak of Coordination", rc.Rewards[0].Item.Name);
            Assert.AreEqual("inv_guild_cloak_horde_c", rc.Rewards[0].Item.Icon);
            Assert.AreEqual(Quality.Epic, rc.Rewards[0].Item.Quality);
            Assert.IsNotNull(rc.Rewards[0].Item.TooltipParams);
        }

        [Test]
        public void ParseGuildPerks()
        {
            string test = Test.Resource.perks;
            GuildPerkCollection rc = JsonParser.Parse<GuildPerkCollection>(test);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Perks);
            Assert.AreEqual(2, rc.Perks.Count);

            Assert.AreEqual(2, rc.Perks[0].GuildLevel);
            Assert.IsNotNull(rc.Perks[0].Spell);
            Assert.AreEqual(78631, rc.Perks[0].Spell.Id);
            Assert.AreEqual("Fast Track", rc.Perks[0].Spell.Name);
            Assert.AreEqual("Rank 1", rc.Perks[0].Spell.Subtext);
            Assert.AreEqual("achievement_guildperk_fasttrack", rc.Perks[0].Spell.Icon);
            Assert.AreEqual("Experience gained from killing monsters and completing quests increased by 5%.", rc.Perks[0].Spell.Description);            
        }
    }
}
