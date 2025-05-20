using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Assets.Tests.EditMode.TestClasses;
using NUnit.Framework;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StoreTests
{
    GameObject testObj;
    Store store;
    Button buyButton;
    SimpleSelector<ISellableStorable> selector;
    ISellableStorable[,] items;
    SellableStorableItem testItem = new SellableStorableItem("test", 1, 10);
    SimpleBank bank;

    void SetUp()
    {
        testObj = new GameObject();
        store = testObj.AddComponent<Store>();
        buyButton = testObj.AddComponent<Button>();
        items = new ISellableStorable[,] { { testItem } };
        selector = new SimpleSelector<ISellableStorable>(items);
        bank = new SimpleBank(100);
    }

    [Test]
    public void InitializeTest()
    {
        SetUp();

        Assert.DoesNotThrow(() => store.Initialize(selector, buyButton));
        Assert.IsTrue(store.gameObject.GetComponents<Button>().Contains(buyButton));
    }

    [Test]
    public void BuyItemTest()
    {
        SetUp();

        store.Initialize(selector, buyButton);

        selector.SelectItem(0, 0);


        //make sure it takes money
        Assert.DoesNotThrow(() => store.BuyItem(bank));
        Assert.AreEqual(90f, bank.GetMoney());

        //make sure a copy is made
        Assert.AreNotSame(testItem, store.BuyItem(bank));
    }

}
