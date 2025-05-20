using Assets.Scripts;
using Assets.Tests.EditMode.TestClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SellerInventoryTests
{
    GameObject testObj;
    SellerInventory seller;
    DisplayableStorer inventory;
    SellableStorableItem[,] items;
    SellableStorableItem testItem;

    void SetUp()
    {
        testObj = new GameObject();
        seller = testObj.AddComponent<SellerInventory>();
        inventory = new DisplayableStorer(2, 2);
        items = new SellableStorableItem[2, 2];

        testItem = new SellableStorableItem("test", 2, 15);
        items[0, 1] = testItem;
    }


    [Test]
    public void InitializeTest()
    {
        SetUp();

        Assert.DoesNotThrow(() => seller.Initialize(inventory, items));
    }

    [Test]
    public void SelectItem()
    {
        SetUp();

        seller.Initialize(inventory, items);

        Assert.DoesNotThrow(() => seller.SelectItem(0, 1));
        Assert.AreEqual(testItem, seller.GetSelectedItem());
    }
}
