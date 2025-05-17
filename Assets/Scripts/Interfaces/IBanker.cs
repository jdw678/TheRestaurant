using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IBanker
    {
        float GetMoney();
        void AddMoney(float amount);
        void RemoveMoney(float amount);
    }
}
