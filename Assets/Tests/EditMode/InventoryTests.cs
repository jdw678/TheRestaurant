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
        StorableItem item = new StorableItem("Test item");

        //create an inventory
        GameObject gameObject = new GameObject();
        gameObject.AddComponent(typeof(Inventory));
        Inventory inventory = gameObject.GetComponent<Inventory>();

        //add the test item
        inventory.AddItem(item, 5);

        //check the inventory state is correct
        Assert.AreEqual(item, inventory.GetItem("Test item"));
        Assert.AreEqual(5, inventory.GetItemAmount("Test item"));
        Assert.AreEqual(1, inventory.GetAllItemNames().Length);

    }

    [Test]
    public void RemoveItem()
    {
        //create a test item
        StorableItem item = new StorableItem("Test item");

        //create an inventory
        GameObject gameObject = new GameObject();
        gameObject.AddComponent(typeof(Inventory));
        Inventory inventory = gameObject.GetComponent<Inventory>();

        //add the test item
        inventory.AddItem(item, 5);

        //try to remove too many
        Assert.That(() => inventory.RemoveItem(item, 50), Throws.TypeOf<Exception>());

        //try to remove an item it doesn't have
        Assert.That(() => inventory.RemoveItem(new StorableItem("Non existant"), 10), Throws.TypeOf<Exception>());

        //remove less than the exact amount
        inventory.RemoveItem(item, 1);
        Assert.AreEqual(4, inventory.GetItemAmount(item.GetName()));

        //remove the exact amount left
        inventory.RemoveItem(item, 4);
        Assert.IsNull(inventory.GetItem(item.GetName()));
        Assert.IsEmpty(inventory.GetAllItemNames());

    }
}
