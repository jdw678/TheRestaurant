using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IStorer
    {
        public void AddItem(IStorable item, float amount, int column, int row);
        public void RemoveItem(int column, int row, float amount);
        public IStorable GetItem(int column, int row);
        public float GetItemAmount(int column, int row);
    }
}
