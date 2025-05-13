using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IStorer
    {
        public void AddItem(IStorable item, int amount);
        public void RemoveItem(IStorable item, int amount);
        public IStorable GetItem(string name);
        public int GetItemAmount(string name);
        public string[] GetAllItemNames();
    }
}
