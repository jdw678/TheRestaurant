using Assets.Scripts;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
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
        text = new TextMeshProUGUI();
        text.transform.parent = displayPrefab.transform;
        item = displayPrefab.AddComponent<BasicItem>();

    }

    [Test]
    public void BasicItemInitializeTest()
    {

        //reset objects
        SetUp();

        //test initialize doesnt error
        Assert.DoesNotThrow(() => item.Initialize(text, 3, displayPrefab));

        //test state
        Assert.AreEqual(displayPrefab, item.GetDisplayImage());
        Assert.AreEqual(3, item.GetAmount());

    }

    [Test]
    public void BasicItemTextUpdates()
    {

        //reset objects
        SetUp();

        //test text is set up correctly on initialize
        item.Initialize(text, 3, displayPrefab);
        Assert.AreEqual("3", text.text);

        //test text updates after changing the amount
        item.SetAmount(5);
        Assert.AreEqual("5", text.text);

    }
}
