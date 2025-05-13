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

    Dictionary<IStorable, int> inventory;
    HashSet<string> names;

    private void Awake()
    {
        inventory = new Dictionary<IStorable, int>(columns * rows);
        names = new HashSet<string>(columns * rows);
    }

    public string[] GetAllItemNames()
    {
        return names.ToArray();
    }

    public int GetItemAmount(string name)
    {
        if (!names.Contains(name))
            return 0;

        return inventory.First(x => x.Key.GetName().Equals(name)).Value;
    }

    public void ToggleDisplay(bool isDisplaying)
    {
        throw new System.NotImplementedException();
    }

    public void AddItem(IStorable item, int amount)
    {
        //get the key in inventory to make certain we are modifying the correct data
        IStorable entry = GetItem(item.GetName());

        //if it doesnt exist, add it
        if (entry is null)
        {
            names.Add(item.GetName());
            inventory.Add(item, amount);
            return;
        }

        //if exists, get the current amount and increment it
        int currentAmount = inventory[entry];
        inventory[entry] = amount + currentAmount;
    }

    public IStorable GetItem(string name)
    {
        //make certain it exists
        if (!names.Contains(name))
            return null;

        //search for the first (and only) entry of the name in the inventory and return it
        return inventory.First(x => x.Key.GetName().Equals(name)).Key;
    }

    public void RemoveItem(IStorable item, int amount)
    {
        //get the key in inventory to make certain we are modifying the correct data
        IStorable entry = GetItem(item.GetName());

        //check item exists
        if (entry is null)
            throw new System.Exception("That item is not in this inventory!");

        //check there is enough of the item to remove
        int currentAmount = inventory[entry];
        if (currentAmount < amount)
            throw new System.Exception("Not enough items in the inventory!\n" +
                "Amount Requested: " + amount + "\n" +
                "Amount Available: " + currentAmount);

        //check if this removes the item from the inventory
        if(currentAmount == amount)
        {
            names.Remove(entry.GetName());
            inventory.Remove(entry);
        }

        //by now it must be true that the inventory has more than enough items
        //remove the requested amount
        inventory[entry] = currentAmount - amount;
        
    }

    public override string ToString()
    {
        string str = $"Inventory Rows: {rows}\n" +
            $"Inventory Columns: {columns}\n" + 
            $"Inventory Contents: \n";

        foreach (KeyValuePair<IStorable, int> kvp in inventory)
        {
            str += $"\tName: {kvp.Key.GetName()}, Amount: {kvp.Value}\n";
        }

        return str;
    }

    private void OnValidate()
    {
        //get the new inventory size
        int size = columns * rows;

        //create a new inventory with the new size
        Dictionary<IStorable, int> newInventory = new Dictionary<IStorable, int>(size);
        names = new HashSet<string>(size);

        //loop through the old inventory and add items to the new inventory, until the new inventory runs out of space
        int i = 0;
        foreach(var kvp in inventory)
        {
            if (i == size)
                break;

            newInventory[kvp.Key] = kvp.Value;

            i++;
        }

    }
}