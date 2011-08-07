using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

using NUnit.Framework;
using BattleNet.API.WoW;
using BattleNet.API;

namespace Test
{
    [TestFixture]
    class AuctionTest
    {

        [Test]
        public void TestGetAuctions()
        {
            string txt = Util.ReadData("data/auction.json");
            // use the XML based parser
            JsonParser.UseJson = false;
            AuctionResponse r = JsonParser.Parse<AuctionResponse>(txt);

            Assert.AreEqual(1, r.Files.Count);

            JsonParser.UseJson = true;
            AuctionResponse r1 = JsonParser.Parse<AuctionResponse>(txt);
            Assert.AreEqual(1, r1.Files.Count);

        }
    }
}
