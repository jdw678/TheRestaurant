using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface ISelector<T>
    {
        public T GetSelectedItem();
        public void SelectItem(int column, int row);
    }
}
