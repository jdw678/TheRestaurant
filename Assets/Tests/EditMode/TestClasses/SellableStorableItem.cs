using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Tests.EditMode.TestClasses
{
    internal class SellableStorableItem : ISellableStorable
    {

        float amount;
        string name;
        float price;

        public SellableStorableItem(string name, float amount, float price)
        {
            this.amount = amount;
            this.name = name;
            this.price = price;
        }

        public IStorable Clone()
        {
            return new SellableStorableItem(name, amount, price);
        }

        public float GetAmount()
        {
            return amount;
        }

        public Image GetDisplayImage()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return name;
        }

        public float GetPrice()
        {
            return price;
        }

        public void SetAmount(float amount)
        {
            this.amount = amount;
        }
    }
}
