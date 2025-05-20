using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Tests.EditMode.TestClasses;
using Assets.Tests.TestClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SellerInventoryTests
{
       

    [Test]
    public void BuyItem()
    {
        //create a test item
        SellableStorableItem item = new SellableStorableItem("Test item", 5, 10);

        //create an inventory
        GameObject gameObject = new GameObject();
        gameObject.AddComponent(typeof(SellerInventory));

        SellerInventory inventory = gameObject.GetComponent<SellerInventory>();
        inventory.SetUI(new EmptyDisplayable());

        inventory.Start();
        inventory.AddSellableItem(item, 0, 0);

        //check the state is set correctly
        Assert.AreEqual(5, inventory.GetItemAmount(0, 0));
        Assert.AreEqual(10, inventory.GetItem(0, 0).GetPrice());

        //assert that buy a reasonable amount does not error
        Assert.DoesNotThrow(() => inventory.BuyItem(0, 0, 2));

        //assert that buying MORE than the item amount does not error, as it should be able to sell infinite amounts of items
        Assert.DoesNotThrow(() => inventory.BuyItem(0, 0, 5));

        //assert that the right price is returned
        Assert.AreEqual(50, inventory.BuyItem(0, 0, 5));
    }
}