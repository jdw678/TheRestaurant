using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class Inventory : MonoBehaviour, IDisplayableStorer
{

    [SerializeField, Range(1, 20)] int columns;
    [SerializeField, Range(1, 20)] int rows;

    IStorable[,] inventory;

    [SerializeField] IDisplayable inventoryUI;

    private void Awake()
    {
        inventory = new IStorable[columns, rows];
    }


    public float GetItemAmount(int column, int row)
    {
        if (inventory[column, row] == null)
            return 0;

        return inventory[column, row].GetAmount();
    }

    public void ToggleDisplay(bool isDisplaying)
    {
        inventoryUI.ToggleDisplay(isDisplaying);
    }

    public void AddItem(IStorable item, float amount, int column, int row)
    {
        if (inventory[column, row] != null)
            throw new System.Exception("There is already an item in this spot!");

        //make sure we have enough
        if(amount > item.GetAmount())
            amount = item.GetAmount();

        //create the new item and set it
        IStorable newItem = item.Clone();
        newItem.SetAmount(amount);
        inventory[column, row] = newItem;

        //update the old item's amount
        item.SetAmount(item.GetAmount() - amount);

        //update the display
        inventoryUI.UpdateDsiplay();
    }

    public IStorable GetItem(int column, int row)
    {

        return inventory[column, row];
    }

    public void RemoveItem(int column, int row, float amount)
    {
        //get the item
        IStorable entry = GetItem(column, row);

        //check item exists
        if (entry is null)
            throw new System.Exception($"There is no item stored at {column}, {row} in inventory {name}.");

        //check there is enough of the item to remove
        float currentAmount = entry.GetAmount();
        if (currentAmount < amount)
            throw new System.Exception("Not enough items in the inventory!\n" +
                "Amount Requested: " + amount + "\n" +
                "Amount Available: " + currentAmount);


        //remove the requested amount
        entry.SetAmount(currentAmount - amount);

        //update the display
        inventoryUI.UpdateDsiplay();
    }

    public int GetRows()
    {
        return rows;
    }

    public int GetColumns()
    {
        return columns;
    }

    void Resize()
    {

        //check to see if the inventory size is still the same or not
        int oldColumns = inventory.GetLength(0);
        int oldRows = inventory.GetLength(1);
        if ( oldColumns == columns && oldRows == rows)
            return;


        //create a new inventory with the new size
        IStorable[,] newInventory = new IStorable[columns, rows];

        //get a Dictionary of all the items (item, count) to:
        //  A) combine similar items, B) not double copy any items, and C) not miss any items
        Dictionary<IStorable, float> itemsDic = new Dictionary<IStorable, float>(oldColumns * oldRows);

        //add all the items to the dictionary
        foreach(IStorable item in inventory)
        {
            if (itemsDic.ContainsKey(item))
                itemsDic[item] = itemsDic[item] + item.GetAmount();
            else
                itemsDic.Add(item, item.GetAmount());
        }

        IStorable[] items = itemsDic.Keys.ToArray();

        //fill the new inventory
        for (int x = 0; x < columns; x++)
        {
            if (x * rows >= items.Length)
                break;

            for (int y = 0; y < rows; y++)
            {
                if (x * rows + y >= items.Length)
                    break;

                newInventory[x, y] = items[x * rows + y];
            }

        }
    }

    void GetUI()
    {

        Component[] components = gameObject.GetComponentsInChildren<Component>();
        foreach (Component component in components)
        {
            //skip inventory components
            if (component is IDisplayableStorer)
                continue;

            if (component is IDisplayable)
            {
                inventoryUI = (IDisplayable)component;
                return;
            }
        }

        inventory = null;
        Debug.LogError($"Game Object \"{name}\" has an Inventory component but is missing a component of type IDisplayable!");
    }

    public override string ToString()
    {
        string str = $"Inventory Rows: {rows}\n" +
            $"Inventory Columns: {columns}\n" + 
            $"Inventory Contents: \n";

        foreach (IStorable item in inventory)
        {
            str += $"\tItem: {item}, Amount: {item.GetAmount()}\n";
        }

        return str;
    }

    private void OnValidate()
    {

        if(inventory == null) return;

        Resize();
        GetUI();
    }
}