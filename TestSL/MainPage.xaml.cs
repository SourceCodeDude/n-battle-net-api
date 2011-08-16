using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace TestSL
{
    using BattleNet.API;
    using BattleNet.API.WoW;

    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            RunTest();
        }

        public void RunTest()
        {
            BattleNetClient client = new BattleNetClient();

            List<Realm> status = client.RealmStatus();
        }
    }
}
