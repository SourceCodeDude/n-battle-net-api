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
    class CharacterTest
    {
        private void VerifyBasicData(Character rc)
        {
            Assert.IsNotNull(rc);
            Assert.AreEqual("Medivh", rc.realm);
            Assert.AreEqual("Uther", rc.name);
            Assert.AreEqual(85, rc.level);
            Assert.AreEqual( new UnixTimestamp(1307596000000), rc.lastModified);
            Assert.AreEqual("medivh/1/1-avatar.jpg", rc.thumbnail);
            Assert.AreEqual(1, rc.race);
            Assert.AreEqual(9745, rc.achievementPoints);
            Assert.AreEqual(0, rc.gender);
            Assert.AreEqual(2, rc.Class);
        }

        string ReadData(string file)
        {
            return new StreamReader(File.Open(file, FileMode.Open)).ReadToEnd();
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
            string test = ReadData("data/character/guild.json");

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
            string test = ReadData("data/character/title.json");

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

        [Test]
        public void TestAchievementsData()
        {
            string test = ReadData("data/character/achievements.json");

            Character rc = JsonParser.Parse<Character>(test);

            VerifyBasicData(rc);

            // test achievements
            Assert.IsNotNull(rc.achievements);
            Assert.AreEqual(5, rc.achievements.achievementsCompleted.Count);
            Assert.AreEqual(5, rc.achievements.achievementsCompletedTimestamp.Count);
            Assert.AreEqual(5, rc.achievements.criteria.Count);
            Assert.AreEqual(5, rc.achievements.criteriaQuantity.Count);
            Assert.AreEqual(5, rc.achievements.criteriaTimestamp.Count);
            Assert.AreEqual(5, rc.achievements.criteriaCreated.Count);
        }


        [Test]
        public void TestAppearanceData()
        {
            string test = ReadData("data/character/appearance.json");

            Character rc = JsonParser.Parse<Character>(test);

            VerifyBasicData(rc);

            Appearance a = rc.Appearance;
            // test Appearance
            Assert.IsNotNull(a);
            Assert.AreEqual(2, a.faceVariation);
            Assert.AreEqual(0, a.skinColor);
            Assert.AreEqual(1, a.hairVariation);
            Assert.AreEqual(8, a.hairColor);
            Assert.AreEqual(2, a.featureVariation);
            Assert.AreEqual(true, a.showHelm);
            Assert.AreEqual(true, a.showCloak);
        }

        [Test]
        public void TestItemData()
        {
            string test = ReadData("data/character/items.json");

            Character rc = JsonParser.Parse<Character>(test);

            VerifyBasicData(rc);

            CharacterItems a = rc.Items;
            // test Appearance
            Assert.IsNotNull(a);
            Assert.AreEqual(353, a.averageItemLevel);
            Assert.AreEqual(335, a.averageItemLevelEquipped);
            Assert.IsNotNull(a.back);
            Assert.IsNotNull(a.chest);
            Assert.IsNotNull(a.feet);
            Assert.IsNotNull(a.finger1);
            Assert.IsNotNull(a.finger2);
            Assert.IsNotNull(a.hands);
            Assert.IsNotNull(a.head);
            Assert.IsNotNull(a.legs);
            Assert.IsNotNull(a.mainHand);
            Assert.IsNotNull(a.neck);
            Assert.IsNotNull(a.ranged);
            Assert.IsNotNull(a.shirt);
            Assert.IsNotNull(a.shoulder);
            Assert.IsNotNull(a.tabard);
            Assert.IsNotNull(a.trinket1);
            Assert.IsNotNull(a.trinket2);
            Assert.IsNotNull(a.waist);
        }
    }
}
