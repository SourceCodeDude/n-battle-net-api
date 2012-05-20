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
    class ItemTest
    {
        [Test]
        public void TestItemClasses()
        {
            BattleNetClient c = new BattleNetClient();
            List<Item.Class> classes = c.ItemClasses;
        }

        [Test]
        public void TestParse()
        {
            string txt = Util.ReadData("data/item/basic.json");
            Item itm;

            itm = JsonParser.Parse<Item>(txt);
            CheckItem(itm);

        }
        public void CheckItem(Item itm)
        {
            Assert.NotNull(itm);
            Assert.AreEqual(38268, itm.Id);
            Assert.AreEqual("Give to a Friend", itm.Description);
            Assert.AreEqual("Spare Hand", itm.Name);
            Assert.AreEqual("inv_gauntlets_09", itm.Icon);            
            Assert.AreEqual(1, itm.Stackable);
            Assert.AreEqual(0, itm.ItemBind);
            Assert.NotNull(itm.BonusStats);
            Assert.AreEqual(5, itm.BonusStats.Count);
            Assert.AreEqual(35, itm.BonusStats[0].Stat);
            Assert.AreEqual(false, itm.BonusStats[0].Reforged);
            Assert.AreEqual(11, itm.BonusStats[0].Amount);

            Assert.NotNull(itm.ItemSpells);
            Assert.AreEqual(1, itm.ItemSpells.Count);
            Assert.AreEqual(434, itm.ItemSpells[0].SpellId);
            Assert.AreEqual(1, itm.ItemSpells[0].Charges);
            Assert.AreEqual(true, itm.ItemSpells[0].Consumable);
            Assert.AreEqual(11, itm.ItemSpells[0].CategoryId);

            Assert.AreEqual(12, itm.BuyPrice);
            Assert.AreEqual(ItemClass.Weapon, itm.ItemClass);
            Assert.AreEqual(14, itm.ItemSubClass);
            Assert.AreEqual(0, itm.ContainerSlots);
            Assert.AreEqual(2, itm.WeaponInfo.Damage[0].MaxDamage);
            Assert.AreEqual(1, itm.WeaponInfo.Damage[0].MinDamage);
            Assert.AreEqual(2.5, itm.WeaponInfo.WeaponSpeed);            
            Assert.AreEqual(0.6, itm.WeaponInfo.DPS,0.0001 );
            Assert.AreEqual(InventoryType.OneHand, itm.InventoryType);
            Assert.AreEqual(true, itm.Equippable);
            Assert.AreEqual(1, itm.ItemLevel);
            Assert.AreEqual(0, itm.MaxCount);
            Assert.AreEqual(16, itm.MaxDurability);
            Assert.AreEqual(0, itm.MinFactionId);
            Assert.AreEqual(0, itm.MinReputation);
            Assert.AreEqual(Quality.Poor , itm.Quality);
            Assert.AreEqual(2, itm.SellPrice);
            Assert.AreEqual(0, itm.RequiredSkill);
            Assert.AreEqual(70, itm.RequiredLevel);
            Assert.AreEqual(0, itm.RequiredSkillRank);

            Assert.AreEqual(0, itm.ItemSource.SourceId);
            Assert.AreEqual(SourceType.NONE, itm.ItemSource.SourceType );
            Assert.AreEqual(0, itm.BaseArmor);
            Assert.AreEqual(false, itm.HasSockets);
            Assert.AreEqual(true, itm.IsAuctionable);


            // extras that are not inclueded
            Assert.AreEqual(0, itm.DisenchantingSkillRank);
        }
    }
}
