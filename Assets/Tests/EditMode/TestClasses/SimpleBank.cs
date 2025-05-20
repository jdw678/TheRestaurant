using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Tests.EditMode.TestClasses
{
    internal class SimpleBank : IBanker
    {
        float money;

        public SimpleBank(float money)
        {
            this.money = money;
        }

        public void AddMoney(float amount)
        {
            money += amount;
        }

        public float GetMoney()
        {
            return money;
        }

        public void RemoveMoney(float amount)
        {
            money -= amount;
        }
    }
}
