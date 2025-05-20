using Assets.Scripts;
using TMPro;
using UnityEngine;
using NUnit.Framework;
using Assets.Tests.TestClasses;
using System.Linq;
using Assets.Scripts.Interfaces;

public class StoreItemTests
{

    GameObject itemDisplayPrefab;
    TextMeshProUGUI itemText;
    TextMeshProUGUI storeItemText;
    StorableItem item;
    StoreItem storeItem;

    public void SetUp()
    {
        //item set up
        itemDisplayPrefab = new GameObject();

        //create text obj
        GameObject obj = new GameObject("item");
        itemText = obj.AddComponent<TextMeshProUGUI>();

        //set text parent to itemDisplayPrefab
        itemText.transform.SetParent(itemDisplayPrefab.transform);

        //create item
        item = new StorableItem("test", 5, itemDisplayPrefab);


        //store item set up
        storeItem = itemDisplayPrefab.AddComponent<StoreItem>();
        
        //add text
        obj = new GameObject("store item");
        storeItemText = obj.AddComponent<TextMeshProUGUI>();

        //make it a child
        storeItemText.transform.SetParent(itemDisplayPrefab.transform);

    }

    [Test]
    public void StoreItemInitializeTest()
    {
        //reset all the objects
        SetUp();

        //Test Initialize
        Assert.DoesNotThrow(() => storeItem.Initialize(item, 5, storeItemText));

        //check state
        Assert.AreEqual(5, storeItem.GetPrice());
        Assert.AreEqual("test", storeItem.GetName());
        Assert.IsTrue(storeItem.GetComponentsInChildren<TextMeshProUGUI>().Contains(storeItemText));
        Assert.AreEqual("$5", storeItemText.text);
    }

    [Test]
    public void StoreItemTextTest()
    {

        //reset all the objects
        SetUp();

        //init
        storeItem.Initialize(item, 5, storeItemText);

        //check the text is set correctly
        Assert.AreEqual("$5", storeItemText.text);

        //check that the regular text is disabled
        Assert.IsFalse(itemText.IsActive());
    }

    [Test]
    public void CloneItemTest()
    {

        //reset all the objects
        SetUp();

        //init
        storeItem.Initialize(item, 5, storeItemText);

        IStorable clone = storeItem.Clone();

        //check clone state / type
        Assert.IsNotNull(clone);
        Assert.AreEqual("test", clone.GetName());
        Assert.AreNotEqual(storeItem, clone);
        Assert.AreNotSame(storeItem, clone);
    }
}
