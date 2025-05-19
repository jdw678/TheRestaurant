using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteAlways]
    public class SellerInventory : MonoBehaviour, IDisplayableSeller
    {
        [SerializeField, Range(1, 20)] int columns = 1;
        [SerializeField, Range(1, 20)] int rows = 1;
        [SerializeField] bool resetInventory;
        bool updateDisplay;
        ISellableStorable[,] inventory;
        IDisplayable inventoryUI;
        [SerializeField] BasicItem[] basicItems;

        public void Awake()
        {
            ResetSellableItems();
        }

        public void Update()
        {

            if (resetInventory)
            {
                ResetSellableItems();
                resetInventory = false;

            }

            if(updateDisplay)
            {
                inventoryUI.UpdateDisplay();
                updateDisplay = false;
            }
        }

        bool IsInRange(int column, int row)
        {
            return column < columns && row < rows;
        }

        bool IsNull(int column, int row)
        {
            return inventory[column, row] == null;
        }

        public void AddSellableItem(ISellableStorable item, int column, int row)
        {
            if(!IsInRange(column, row))
                throw new IndexOutOfRangeException();

            if (!IsNull(column, row))
                throw new Exception($"Item already listed at column: {column}, row: {row}");

            inventory[column, row] = item;
        }

        public void ResetSellableItems()
        {
            inventory = new ISellableStorable[columns, rows];

        }

        public float BuyItem(int column, int row, float amount)
        {
            if (!IsInRange(column, row))
                throw new IndexOutOfRangeException();

            if (IsNull(column, row))
                throw new Exception($"No item listed at column: {column}, row: {row}");

            return inventory[column, row].GetPrice() * amount;
            
        }

        public ISellable GetItem(int column, int row)
        {
            if (!IsInRange(column, row))
                throw new IndexOutOfRangeException();

            if (IsNull(column, row))
                throw new Exception($"No item listed at column: {column}, row: {row}");

            return inventory[column, row];
        }

        public float GetItemAmount(int column, int row)
        {
            if (!IsInRange(column, row))
                throw new IndexOutOfRangeException();

            if (IsNull(column, row))
                throw new Exception($"No item listed at column: {column}, row: {row}");

            return inventory[column, row].GetAmount();
        }

        public void ToggleDisplay(bool isDisplaying)
        {
            inventoryUI.ToggleDisplay(isDisplaying);
        }

        public int GetRows()
        {
            return rows;
        }

        public int GetColumns()
        {
            return columns;
        }


        public void AddItem(IStorable item, float amount, int column, int row)
        {
            if (!(item is ISellableStorable))
                throw new Exception($"Trying to add item {item} to SellerInventory {this} but the item is not of type ISellableStorable!");

            AddSellableItem((ISellableStorable)item, column, row);
        }

        public void RemoveItem(int column, int row, float amount)
        {
            inventory[column, row] = null;
        }

        IStorable IStorer.GetItem(int column, int row)
        {
            if(inventory == null)
                return null;
            return inventory[column, row];
        }


        void GetUI()
        {
            Component[] components = GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component is IDisplayableSeller)
                    continue;

                if (component is IDisplayable)
                {
                    inventoryUI = (IDisplayable)component;
                    return;
                }
            }


            Debug.LogWarning($"Game Object \"{name}\" has a SellerInventory component but is missing a component of type IDisplayable!");
        }

        private void OnValidate()
        {
            GetUI();

            if (inventory == null || columns != inventory.GetLength(0) || rows != inventory.GetLength(1))
                resetInventory = true;

            updateDisplay = true;
        }
    }
}
