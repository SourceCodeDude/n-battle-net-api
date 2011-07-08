using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Net;
using BattleNet.API.WoW;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            BattleNetClient bc = new BattleNetClient( Region.US);
            List<Race> races = bc.Races;            
            List<Realm> r = bc.RealmStatus("Thunderhorn", "Deathwing");
            bc.Dispose();
        }     
    }

    
}
