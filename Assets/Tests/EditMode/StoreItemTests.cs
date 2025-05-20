using Assets.Scripts;
using TMPro;
using UnityEngine;
using NUnit.Framework;
using Assets.Tests.TestClasses;
using System.Linq;

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
        GameObject obj = new GameObject();
        itemText = obj.AddComponent<TextMeshProUGUI>();
        itemText.transform.SetParent(itemDisplayPrefab.transform);
        item = new StorableItem("test", 5);


        //store item set up
        storeItem = itemDisplayPrefab.AddComponent<StoreItem>();
        obj = new GameObject();
        storeItemText = obj.AddComponent<TextMeshProUGUI>();
        storeItemText.transform.SetParent(itemDisplayPrefab.transform);

    }

    [Test]
    public void StoreItemInitializeTest()
    {
        //reset all the objects
        SetUp();

        //Test Initialize
        Assert.DoesNotThrow(() => storeItem.Initialize(item, "storeItemTest", 5, storeItemText));

        //check state
        Assert.AreEqual(5, storeItem.GetPrice());
        Assert.AreEqual("storeItemTest", storeItem.GetName());
        Assert.IsTrue(storeItem.GetComponentsInChildren<TextMeshProUGUI>().Contains(storeItemText));
        Assert.AreEqual("$5", storeItemText.text);
    }

    [Test]
    public void StoreItemTextTest()
    {

        //reset all the objects
        SetUp();

        //init
        storeItem.Initialize(item, "store item test", 5, storeItemText);

        //check the text is set correctly
        Assert.AreEqual("$5", storeItemText.text);

        //check that the regular text is disabled
        Assert.IsFalse(itemText.IsActive());
    }
}
