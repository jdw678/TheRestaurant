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
    public class SellerInventory : MonoBehaviour, IDisplayable, ISelector<ISellableStorable>
    {
        IDisplayableStorer inventory;
        ISellableStorable[,] items;
        ISellableStorable selectedItem;


        public void SelectItem(int column, int row)
        {
            if (items[column, row] == null)
                return;

            selectedItem = items[column, row];
        }

        public ISellableStorable GetSelectedItem()
        {
            return selectedItem;
        }

        public void Initialize(IDisplayableStorer inventory, ISellableStorable[,] items)
        {
            //initialize the inventory for displaying
            int columns = items.GetLength(0);
            int rows = items.GetLength(1);

            if (columns > inventory.GetColumns() || rows > inventory.GetRows())
                throw new Exception("Inventory does not have enough space!");

            for(int x = 0; x < columns; x++)
            {
                for(int y = 0; y < rows; y++)
                {
                    if (items[x, y] == null)
                        continue;

                    inventory.AddItem(items[x, y], 1, x, y);
                }
            }

            this.inventory = inventory;
            this.items = items;
        }

        public void ToggleDisplay(bool isDisplaying)
        {
            inventory.ToggleDisplay(isDisplaying);
        }

        public void UpdateDisplay()
        {

            //reset the display
            Initialize(inventory, items);
        }
    }
}
