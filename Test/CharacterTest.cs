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
            Assert.AreEqual("Medivh", rc.Realm);
            Assert.AreEqual("Uther", rc.Name);
            Assert.AreEqual(85, rc.Level);
            Assert.AreEqual( new UnixTimestamp(1307596000000), rc.LastModified);
            Assert.AreEqual("medivh/1/1-avatar.jpg", rc.Thumbnail);
            Assert.AreEqual(1, rc.Race);
            Assert.AreEqual(9745, rc.AchievementPoints);
            Assert.AreEqual(0, rc.Gender);
            Assert.AreEqual(2, rc.Class);
        }

        string ReadData(string file)
        {
            return new StreamReader(File.Open(file, FileMode.Open)).ReadToEnd();
        }

        [Test]
        public void TestBasicData()
        {
            string test = ReadData("data/character/char1.json");

            JsonParser.UseJson = false;
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
            Assert.AreEqual("Chaos Theory", rc.Guild.Name);
            Assert.AreEqual("Thunderhorn", rc.Guild.Realm);
            Assert.AreEqual(25, rc.Guild.Level);
            Assert.AreEqual(310, rc.Guild.Members);
            Assert.AreEqual(850, rc.Guild.AchievementPoints);
            Assert.IsNotNull(rc.Guild.Emblem);
            Assert.AreEqual(166,rc.Guild.Emblem.Icon);
            Assert.AreEqual( System.Drawing.Color.FromArgb(255,255,255,0), rc.Guild.Emblem.IconColor);
            Assert.AreEqual(-1, rc.Guild.Emblem.Border);
            Assert.AreEqual(System.Drawing.Color.FromArgb(255, 255, 255, 0), rc.Guild.Emblem.BorderColor);
            Assert.AreEqual(System.Drawing.Color.FromArgb(255, 255, 255, 0x64), rc.Guild.Emblem.BackgroundColor);
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

            Assert.AreEqual(81, rc.Titles[0].Id);
            Assert.AreEqual("%s the Seeker", rc.Titles[0].Name);
            Assert.AreEqual(74, rc.Titles[1].Id);
            Assert.AreEqual("Elder %s", rc.Titles[1].Name);
        }

        [Test]
        public void TestAchievementsData()
        {
            string test = ReadData("data/character/achievements.json");

            Character rc = JsonParser.Parse<Character>(test);

            VerifyBasicData(rc);

            // test achievements
            Assert.IsNotNull(rc.Achievements);
            Assert.AreEqual(5, rc.Achievements.achievementsCompleted.Count);
            Assert.AreEqual(5, rc.Achievements.AchievementsCompletedTimestamp.Count);
            Assert.AreEqual(5, rc.Achievements.Criteria.Count);
            Assert.AreEqual(5, rc.Achievements.CriteriaQuantity.Count);
            Assert.AreEqual(5, rc.Achievements.CriteriaTimestamp.Count);
            Assert.AreEqual(5, rc.Achievements.CriteriaCreated.Count);
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
            Assert.AreEqual(2, a.FaceVariation);
            Assert.AreEqual(0, a.SkinColor);
            Assert.AreEqual(1, a.HairVariation);
            Assert.AreEqual(8, a.HairColor);
            Assert.AreEqual(2, a.FeatureVariation);
            Assert.AreEqual(true, a.ShowHelm);
            Assert.AreEqual(true, a.ShowCloak);
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
            Assert.AreEqual(353, a.AverageItemLevel);
            Assert.AreEqual(335, a.averageItemLevelEquipped);
            Assert.IsNotNull(a.Back);
            Assert.IsNotNull(a.Chest);
            Assert.IsNotNull(a.Feet);
            Assert.IsNotNull(a.Finger1);
            Assert.IsNotNull(a.Finger2);
            Assert.IsNotNull(a.Hands);
            Assert.IsNotNull(a.Head);
            Assert.IsNotNull(a.Legs);
            Assert.IsNotNull(a.MainHand);
            Assert.IsNotNull(a.Neck);
            Assert.IsNotNull(a.Ranged);
            Assert.IsNotNull(a.Shirt);
            Assert.IsNotNull(a.Shoulder);
            Assert.IsNotNull(a.Tabard);
            Assert.IsNotNull(a.Trinket1);
            Assert.IsNotNull(a.Trinket2);
            Assert.IsNotNull(a.Waist);
        }

        [Test]
        public void TestAll()
        {
            string test = ReadData("data/character/all.json");

            Character rc = JsonParser.Parse<Character>(test);

            //VerifyBasicData(rc);

            CharacterItems a = rc.Items;
            // test Appearance
            Assert.IsNotNull(a);
            Assert.AreEqual(353, a.AverageItemLevel);
            Assert.AreEqual(352, a.averageItemLevelEquipped);
            Assert.IsNotNull(a.Back);
            Assert.IsNotNull(a.Chest);
            Assert.IsNotNull(a.Feet);
            Assert.IsNotNull(a.Finger1);
            Assert.IsNotNull(a.Finger2);
            Assert.IsNotNull(a.Hands);
            Assert.IsNotNull(a.Head);
            Assert.IsNotNull(a.Legs);
            Assert.IsNotNull(a.MainHand);
            Assert.IsNotNull(a.Neck);
            Assert.IsNotNull(a.Ranged);
            Assert.IsNull(a.Shirt);
            Assert.IsNotNull(a.Shoulder);
            Assert.IsNotNull(a.Tabard);
            Assert.IsNotNull(a.Trinket1);
            Assert.IsNotNull(a.Trinket2);
            Assert.IsNotNull(a.Waist);
        }
    }
}
