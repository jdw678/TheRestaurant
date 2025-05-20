using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Tests.EditMode.TestClasses
{
    internal class SimpleSelector<T> : ISelector<T>
    {
        T[,] items;
        T selectedItem;

        public SimpleSelector(T[,] items)
        {
            this.items = items;
        }

        public T GetSelectedItem()
        {
            return selectedItem;
        }

        public void SelectItem(int column, int row)
        {
            selectedItem = items[column, row];
        }
    }
}
