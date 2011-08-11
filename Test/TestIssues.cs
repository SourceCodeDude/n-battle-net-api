using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BattleNet.API.WoW;
using BattleNet.API;
using System.Drawing;

namespace Test
{
    [TestFixture]
    class TestIssues
    {
        [Test]
        public void Issue7()
        {            
            using (BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.US))
            {
                client.UseCache = false;
                Character ch = client.GetCharacter("Thunderhorn", "Femor");
                List<AuctionFile> auctions = (List<AuctionFile>)client.GetAuctions("Thunderhorn");
                Guild guild = client.GetGuild("Thunderhorn", "Chaos%20Theory");

                Image ic = client.GetIcon("inv_helmet_robe_dungeonrobe_c_03");
                Image tb = client.GetThumbnail("thunderhorn/227/10569699-avatar.jpg");
                List<Realm> realms = client.RealmStatus(new string[] { "Thunderhorn" });
            }


            try
            {                
                DateTimeOffset time = new DateTimeOffset(DateTime.MinValue, TimeSpan.FromHours(+2));

                Assert.Fail("Shouldnt have gotten here");
            }
            catch (Exception ex)
            {

            }            
        }

        /// <summary>
        /// Timeouts and unable to connect should correctly throw an exception instead of
        /// returning null
        /// </summary>
        [Test]        
        public void Issue9()
        {
            // setup a invalid proxy to cause request to timeout
            System.Net.IWebProxy oldProxy = System.Net.WebRequest.DefaultWebProxy;
            System.Net.WebRequest.DefaultWebProxy = new System.Net.WebProxy("127.0.0.1", false);

            BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.US);

            try
            {
                object status = client.RealmStatus();

                Assert.Fail("Should have got a WebException but didn't");
            }
            catch (System.Net.WebException ex)
            {
                Assert.AreEqual(System.Net.WebExceptionStatus.ConnectFailure, ex.Status);

            }
            // restore original proxy
            System.Net.WebRequest.DefaultWebProxy = oldProxy;

        }

        /// <summary>
        /// Memory leak
        /// </summary>
        [Test]
        public void Issue12()
        {
            BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.US);

            Guild g = client.GetGuild("burning-blade", "rival city", GuildFields.Members);
            Character _c = client.GetCharacter("burning-blade","Fairy");
            
            // current number of assemblies loades
            int loaded = AppDomain.CurrentDomain.GetAssemblies().Length;            

            foreach (Member gm in g.Members)
            {

                try
                {
                    Character c = client.GetCharacter("burning-blade", gm.Character.Name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                int newLoaded = AppDomain.CurrentDomain.GetAssemblies().Length;
                    
                Assert.AreEqual(loaded,newLoaded, "Extra assemblies being loaded.. and not unloaded");

                // we only need to load one character
                break;
            }
        }

        [Test]
        public void Issue14()
        {
            using (BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.EU))
            {
                client.UseCache = true;
                object o1 = client.RealmStatus();
                // second call threw an exception about the Cache file still being open
                object o2 = client.RealmStatus();
            }

        }
    }            
}
