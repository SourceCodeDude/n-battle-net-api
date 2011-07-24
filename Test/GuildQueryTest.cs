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
    class GuildQueryTest
    {
        [Test]
        public void TestSpaces()
        {
            GuildQuery q = new GuildQuery()
            {
                Name = "Name With Spaces",
                Realm = "Realm With Spaces",
            };

            string txt = q.ToString();

            // Need to use %20 for spaces and not +
            Assert.AreEqual("guild/Realm%20With%20Spaces/Name%20With%20Spaces?", txt);
        }

        [Test]
        public void TestExtended()
        {
            GuildQuery q = new GuildQuery()
            {
                Name = "Coådina",
                Realm = "Realm",
            };

            string txt = q.ToString();

            // test for UTF8 escaping
            Assert.AreEqual("guild/Realm/Co%c3%a5dina?", txt);
        }
    }
}
