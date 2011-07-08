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
    class CharacterTest
    {
        private void VerifyBasicData(Character rc)
        {
            Assert.IsNotNull(rc);
            Assert.AreEqual("Medivh", rc.realm);
            Assert.AreEqual("Uther", rc.name);
            Assert.AreEqual(85, rc.level);
            Assert.AreEqual(1307596000000, rc.lastModified);
            Assert.AreEqual("medivh/1/1-avatar.jpg", rc.thumbnail);
            Assert.AreEqual(1, rc.race);
            Assert.AreEqual(9745, rc.achievementPoints);
            Assert.AreEqual(0, rc.gender);
            Assert.AreEqual(2, rc.Class);
        }
        [Test]
        public void TestBasicData()
        {
            string test = Test.Resource.char1;

            Character rc = JsonParser.Parse<Character>(test);
            VerifyBasicData(rc);

        }

        [Test]
        public void TestGuildData()
        {
            string test = Test.Resource.charGuild;

            Character rc = JsonParser.Parse<Character>(test);

            VerifyBasicData(rc);

            // test guild
            Assert.IsNotNull(rc.Guild);
            Assert.AreEqual("Chaos Theory", rc.Guild.name);
            Assert.AreEqual("Thunderhorn", rc.Guild.realm);
            Assert.AreEqual(25, rc.Guild.level);
            Assert.AreEqual(310, rc.Guild.members);
            Assert.AreEqual(850, rc.Guild.achievementPoints);
            Assert.IsNotNull(rc.Guild.Emblem);
            Assert.AreEqual(166,rc.Guild.Emblem.icon);
            Assert.AreEqual("ffffff00", rc.Guild.Emblem.iconColor);
            Assert.AreEqual(-1, rc.Guild.Emblem.border);
            Assert.AreEqual("ffffff00", rc.Guild.Emblem.borderColor);
            Assert.AreEqual("ffffff64", rc.Guild.Emblem.backgroundColor);
        }

        [Test]
        public void TestTitlesData()
        {
            string test = Test.Resource.charTitle;

            Character rc = JsonParser.Parse<Character>(test);

            VerifyBasicData(rc);

            // test titles
            Assert.IsNotNull(rc.Titles);
            Assert.AreEqual(2,rc.Titles.Count);

            Assert.AreEqual(81, rc.Titles[0].id);
            Assert.AreEqual("%s the Seeker", rc.Titles[0].name);
            Assert.AreEqual(74, rc.Titles[1].id);
            Assert.AreEqual("Elder %s", rc.Titles[1].name);
        }

        // TODO: add the rest of the optional fields here
    }
}
