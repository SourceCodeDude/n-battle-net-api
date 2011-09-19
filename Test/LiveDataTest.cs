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
    
    /// <summary>
    /// Connect to US server and test actual
    /// </summary>
    /// [TestFixture]   
    public class LiveDataTest
    {
        BattleNetClient client = new BattleNetClient(Region.US);
        public LiveDataTest()
        {
            client.UseCache = false;
            client.ParseError += new ParseErrorDelegate(client_ParseError);
        }

        /*
        [Test]
        public void TestAuthentication()
        {
            client.PublicKey = "123PUBLIC456";
            client.PrivateKey = "987PRIVATE65";
            Stream st = client.GetUrl("http://everynothing.net/bnetauthtest.php/法拉希姆?test=1", null);
            TextReader r = new StreamReader(st);
            string txt = r.ReadToEnd();
        }
        */
        

        [Test]
        public void CharacterTest()
        {            
            
            Character ch = client.GetCharacter("burning-blade", "Fairy", CharacterFields.All);

            Assert.NotNull(ch);
        }

        [Test]
        public void GuildTest()
        {                                    
            Guild g = client.GetGuild("burning-blade", "rival city", GuildFields.All);
            Assert.NotNull(g);            
        }

        [Test]
        public void RealmTest()
        {
            List<Realm> r = client.RealmStatus();

            Assert.NotNull(r);
        }

        [Test]
        public void AuctionTest()
        {            
            // high population server should always have autions
            IList<AuctionFile> auctions = client.GetAuctions("Magtheridon");
            foreach (AuctionFile a in auctions)
            {
                AuctionData d = a.Data;
                Assert.NotNull(d.Realm);
                Assert.NotNull(d.Alliance);
                Assert.NotNull(d.Horde);
                Assert.NotNull(d.Neutral);
            }            
        }

        [Test]
        public void BattleNtCN()
        {
            BattleNetClient cn = new BattleNetClient(Region.CN);
            Character c=cn.GetCharacter("法拉希姆","风之神仙");
            Assert.NotNull(c);
            System.Drawing.Image img = cn.GetThumbnail(c.Thumbnail);
            Assert.NotNull(img);

        }

        [Test]
        public void TestArena()
        {
            ArenaTeam at = client.GetArenaTeam("BoneChewer", TeamSize.Team2v2, "Samurai Jack");

            Assert.NotNull(at);
        }

        [Test]
        public void TestGuildAchiev()
        {
            AchievementCollection a = client.GuildAchievements;
            Assert.NotNull(a);
        }

        [Test]
        public void TestCharacterdAchiev()
        {
            AchievementCollection a = client.CharacterAchievements;
            Assert.NotNull(a);
        }


        [Test]
        public void TestQuest()
        {
            Quest q = client.GetQuest(new QuestQuery() { Id = 123 });
            Assert.NotNull(q);
        }
        void client_ParseError(string message)
        {
            Assert.Fail(message);            
        }


    }
}
