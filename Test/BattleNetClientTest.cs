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
    class BattleNetClientTest
    {
        [Test]
        public void TestGetIcon()
        {
            using (BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.US))
            {
                Image img = client.GetIcon("inv_misc_necklacea9.jpg");
                Assert.NotNull(img);
            }
        }

        [Test]
        public void TestGetThumbnail()
        {
            using (BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.US))
            {
                Image img = client.GetThumbnail("medivh/66/3930434-avatar.jpg");
                Assert.NotNull(img);
            }
        }

        [Test]
        public void TestClasses()
        {
            using (BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.US))
            {
                List<Class> classes = client.Classes.Classes;
                Assert.NotNull(classes);
            }
        }

        /*
        [Test]
        public void GetChar()
        {
            using (BattleNetClient client = new BattleNetClient(BattleNet.API.WoW.Region.US))
            {
                Character c = client.GetCharacter("Thunderhorn", "Femor");
                Assert.NotNull(c);
            }

        }
         */


    }
}
