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

using System.Collections.ObjectModel;
using System.Reflection;
using NUnit.Framework;

using System.Web.Script.Serialization;

using BattleNet.API;

namespace Test
{

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            string[] my_args = { Assembly.GetExecutingAssembly().Location };

            int returnCode = NUnit.ConsoleRunner.Runner.Main(my_args);

            if (returnCode != 0)
                Console.Beep();
        }
    }

    
}
