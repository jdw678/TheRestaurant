using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface ISeller
    {
        float BuyItem(int column, int row, float amount);
        float GetItemAmount(int column, int row);
        ISellable GetItem(int column, int row);
    }
}
