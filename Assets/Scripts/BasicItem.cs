using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [Serializable]
    internal class BasicItem : ISellableStorable
    {
        [SerializeField] Image displayImage;
        [SerializeField] string name;
        [SerializeField] float price;
        int amount;


        BasicItem(Image displayImage, string name, float price, int amount)
        {
            this.displayImage = displayImage;
            this.name = name;
            this.price = price;
            this.amount = amount;
        }

        public float GetPrice()
        {
            return price;
        }

        public Image GetDisplayImage()
        {
            return displayImage;
        }

        public float GetAmount()
        {
            return amount;
        }

        public void SetAmount(float amount)
        {
            this.amount = (int)amount;
        }

        public IStorable Clone()
        {
            return new BasicItem(displayImage, name, price, amount);
        }

        public string GetName()
        {
            return name;
        }
    }
}
