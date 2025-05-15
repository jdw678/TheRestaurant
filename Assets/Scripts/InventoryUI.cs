using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IDisplayable
{
    IDisplayableStorer inventory;
    [SerializeField] GameObject container;
    [SerializeField] Image backgroundImage;
    [SerializeField, Range(1, 20)] int gap;


    public void ToggleDisplay(bool isDisplaying)
    {
        gameObject.SetActive(isDisplaying);
    }

    public void UpdateDsiplay()
    {
        if (inventory == null)
            return;

    }

    void GetInventory()
    {
        Component[] components = gameObject.GetComponentsInChildren<Component>();
        foreach (Component component in components)
        {
            if (component is IDisplayableStorer)
            {
                inventory = (IDisplayableStorer)component;
                return;
            }
        }

        inventory = null;
        Debug.LogError($"Game Object \"{name}\" has an InventoryUI component but is missing a component of type IDisplayableStorer!");
    }

     

    private void OnValidate()
    {
        GetInventory();

    }
}
