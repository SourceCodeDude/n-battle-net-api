﻿using System;
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
    }
}
