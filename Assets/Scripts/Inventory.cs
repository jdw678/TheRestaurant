using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteAlways]
public class Inventory : MonoBehaviour, IDisplayableStorer
{

    [SerializeField, Range(1, 20)] int columns = 1;
    [SerializeField, Range(1, 20)] int rows = 1;

    IStorable[,] inventory;

    [SerializeField] IDisplayable inventoryUI;
    bool updateUI;
    [SerializeField] bool resetInventory;

    public void Awake()
    {
        inventory = new IStorable[columns, rows];
        if (inventoryUI == null)
            GetUI();
    }

    private void Update()
    {

        if(inventory == null)
            inventory = new IStorable[columns, rows];

        if (inventoryUI == null)
            GetUI();

        if (updateUI)
        {
            inventoryUI.UpdateDisplay();
            updateUI = false;
        }
        
    }

    public void SetUI(IDisplayable inventoryUI)
    {
        this.inventoryUI = inventoryUI;
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
        {
            IStorable storedItem = inventory[column, row];
            //check if they are the same type of item. If so, add the amount
            if(storedItem.GetName().Equals(item.GetName()))
            {
                storedItem.SetAmount(storedItem.GetAmount() + item.GetAmount());
                return;
            }

            //otherwise error out
            throw new System.Exception("There is already an item in this spot! Either use an empty spot, or make sure you are adding an item with the same name!");
        }

        //create the new item and set it
        IStorable newItem = item.Clone();
        newItem.SetAmount(amount);
        inventory[column, row] = newItem;

        //update the old item's amount
        item.SetAmount(item.GetAmount() - amount);

        //update the display
        inventoryUI.UpdateDisplay();
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


        //remove the item if 0 left
        if (currentAmount - amount == 0)
            inventory[column, row] = null;

        //else remove the requested amount
        else entry.SetAmount(currentAmount - amount);


        //update the display
        inventoryUI.UpdateDisplay();
    }

    public int GetRows()
    {
        return rows;
    }

    public int GetColumns()
    {
        return columns;
    }

    public void SetRows(int rows)
    {
        if(rows < 0)
            rows = 0;
        
        this.rows = rows;
    }
    public void SetColumns(int columns)
    {
        if (columns < 0)
            columns = 0;

        this.columns = columns;

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
        Component[] components = gameObject.GetComponents<Component>();
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

        if (inventory != null)
            return;
        Debug.LogWarning($"Game Object \"{name}\" has an Inventory component but is missing a component of type IDisplayable!");
    }

    public override string ToString()
    {
        string str = $"Inventory Rows: {rows}\n" +
            $"Inventory Columns: {columns}\n" + 
            $"Inventory Contents: \n";

        foreach (IStorable item in inventory)
        {
            if (item == null)
                str += "Null";

            else str += $"\tItem: {item}, Amount: {item.GetAmount()}\n";
        }

        return str;
    }

    private void OnValidate()
    {
        if (resetInventory)
        {
            inventory = new IStorable[columns, rows];
            resetInventory = false;
        }

        if (inventory == null) return;

        //Resize();
        GetUI();

        if(inventoryUI == null) return;
        updateUI = true;
    }
}