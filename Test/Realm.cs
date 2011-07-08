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
    class Realm
    {

        [Test]
        public void ParseRealmCollection()
        {
/*
  "realms":[
    {
      "type":"pvp",
      "queue":false,
      "status":true,
      "population":"low",
      "name":"Aegwynn",
      "slug":"aegwynn"
    },
    {
      "type":"pve",
      "queue":false,
      "status":true,
      "population":"high",
      "name":"Aerie Peak",
      "slug":"aerie-peak"
    },
 */
            string test = Test.Resource.realm1;
            
            RealmCollection rc =  JsonParser.Parse<RealmCollection>(test);

            Assert.IsNotNull(rc);
            Assert.IsNotNull(rc.Realms);
            Assert.AreEqual(2, rc.Realms.Count);

            Assert.AreEqual(RealmType.PVP, rc.Realms[0].type);
            Assert.AreEqual(false, rc.Realms[0].queue);
            Assert.AreEqual(true, rc.Realms[0].status);
            Assert.AreEqual(RealmPopulation.Low, rc.Realms[0].population);
            Assert.AreEqual("Aegwynn", rc.Realms[0].name);
            Assert.AreEqual("aegwynn", rc.Realms[0].slug);
        }

    }
}
