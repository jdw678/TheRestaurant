using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Tests.EditMode.TestClasses
{
    internal class DisplayableStorer : IDisplayableStorer
    {
        IStorable[,] items;
        public bool displaying;

        public void Initialize(int columns, int rows)
        {
            items = new IStorable[columns, rows];
        }
        bool IsOutsideOfItemsRange(int column, int row)
        {
            if (IsOutsideOfItemsRange(column, row))
                throw new IndexOutOfRangeException();

            return column >= GetColumns() || row >= GetColumns();
                
        }

        public void AddItem(IStorable item, float amount, int column, int row)
        {
            if (IsOutsideOfItemsRange(column, row))
                throw new IndexOutOfRangeException();

            if (items[column, row] != null)
                throw new Exception("Already has an item there");

            item.SetAmount(amount);
            items[column, row] = item;


        }

        public int GetColumns()
        {
            return items.GetLength(0);
        }

        public IStorable GetItem(int column, int row)
        {
            if (IsOutsideOfItemsRange(column, row))
                throw new IndexOutOfRangeException();
            return items[column, row];
        }

        public float GetItemAmount(int column, int row)
        {
            if(IsOutsideOfItemsRange(column, row))
                throw new IndexOutOfRangeException();

            return items[column, row].GetAmount();
        }

        public int GetRows()
        {
            return items.GetLength(1);
        }

        public void RemoveItem(int column, int row, float amount)
        {
            if (IsOutsideOfItemsRange(column, row))
                throw new IndexOutOfRangeException();

            if (items[column, row].GetAmount() < amount)
                throw new Exception("Not enough items");

            float newAmount = items[column, row].GetAmount() - amount;

            items[column, row].SetAmount(newAmount);

            if (items[column, row].GetAmount() == 0)
                items[column, row] = null;

        }

        public void ToggleDisplay(bool isDisplaying)
        {
            displaying = isDisplaying;
        }
    }
}
