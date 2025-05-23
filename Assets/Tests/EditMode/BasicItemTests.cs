using Assets.Scripts;
using Assets.Scripts.Interfaces;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BasicItemTests
{

    GameObject displayPrefab;
    TextMeshProUGUI text;
    BasicItem item;

    public void SetUp()
    {
        displayPrefab = new GameObject();
        GameObject obj = new GameObject();
        text = obj.AddComponent<TextMeshProUGUI>();
        text.transform.SetParent(displayPrefab.transform);
        item = displayPrefab.AddComponent<BasicItem>();

    }

    [Test]
    public void BasicItemInitializeTest()
    {

        //reset objects
        SetUp();

        //test initialize doesnt error
        Assert.DoesNotThrow(() => item.Initialize(text, 3, displayPrefab, "test"));

        //test state
        Assert.AreEqual(displayPrefab, item.GetDisplayImage());
        Assert.AreEqual(3, item.GetAmount());
        Assert.IsTrue(displayPrefab.GetComponentsInChildren<TextMeshProUGUI>().Contains(text));

    }

    [Test]
    public void BasicItemTextUpdates()
    {

        //reset objects
        SetUp();

        //test text is set up correctly on initialize
        item.Initialize(text, 3, displayPrefab, "test");
        Assert.AreEqual("3", text.text);

        //test text updates after changing the amount
        item.SetAmount(5);
        Assert.AreEqual("5", text.text);

    }

    [Test]
    public void CloneItemTest()
    {
        //reset objects
        SetUp();

        //test text is set up correctly on initialize
        item.Initialize(text, 3, displayPrefab, "test");

        //clone
        IStorable clone = item.Clone();

        //check state / type
        Assert.IsNotNull(clone);
        Assert.AreNotSame(item, clone);
        Assert.AreEqual("test", clone.GetName());
        Assert.AreEqual("3", clone.GetDisplayImage().GetComponentInChildren<TextMeshProUGUI>().text);
        Assert.AreNotSame(displayPrefab, clone.GetDisplayImage());
        Assert.AreEqual(3, clone.GetAmount());
    }
}
