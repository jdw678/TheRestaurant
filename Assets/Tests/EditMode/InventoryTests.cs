using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using Assets.Tests.TestClasses;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InventoryTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void AddItem()
    {
        //create a test item
        StorableItem item = new StorableItem("Test item", 5);

        //create an inventory
        GameObject gameObject = new GameObject();
        gameObject.AddComponent(typeof(Inventory));
        Inventory inventory = gameObject.GetComponent<Inventory>();

        //add the test item
        inventory.AddItem(item, 5, 0, 0);

        //check the inventory state is correct
        Assert.AreEqual(item, inventory.GetItem(0, 0));
        Assert.AreEqual(5, inventory.GetItemAmount(0 , 0));

    }

    [Test]
    public void RemoveItem()
    {
        //create a test item
        StorableItem item = new StorableItem("Test item", 5);

        //create an inventory
        GameObject gameObject = new GameObject();
        gameObject.AddComponent(typeof(Inventory));
        Inventory inventory = gameObject.GetComponent<Inventory>();

        //add the test item
        inventory.AddItem(item, 5, 0, 0);

        //try to remove too many
        Assert.That(() => inventory.RemoveItem(0, 0, 50), Throws.TypeOf<Exception>());

        //try to remove an item it doesn't have
        Assert.That(() => inventory.RemoveItem(1, 0, 10), Throws.TypeOf<Exception>());

        //remove less than the exact amount
        inventory.RemoveItem(0, 0, 1);
        Assert.AreEqual(4, inventory.GetItemAmount(0, 0));

        //remove the exact amount left
        inventory.RemoveItem(0, 0, 4);
        Assert.IsNull(inventory.GetItem(0, 0));

    }
}
