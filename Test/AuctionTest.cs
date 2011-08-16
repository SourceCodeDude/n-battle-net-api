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
            BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.US);

            string txt = Util.ReadData("data/auction.json");
            // use the XML based parser
            JsonParser.UseJson = false;
            AuctionResponse r = JsonParser.Parse<AuctionResponse>(txt);            
            Assert.AreEqual(1, r.Files.Count);

            r.Files[0].Client = client;
            AuctionData data = r.Files[0].Data;
            Assert.NotNull(data);

            JsonParser.UseJson = true;
            AuctionResponse r1 = JsonParser.Parse<AuctionResponse>(txt);
            Assert.AreEqual(1, r1.Files.Count);

            r.Files[0].Client = client;
            data = r.Files[0].Data;
            Assert.NotNull(data);
            

        }
    }
}
