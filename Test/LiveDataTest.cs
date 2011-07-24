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

        void client_ParseError(string message)
        {
            Assert.Fail(message);            
        }
    }
}
