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
        [SerializeField] GameObject displayPrefab;
        [SerializeField] Texture2D image;
        [SerializeField] string name;
        [SerializeField] float price;
        int amount;


        BasicItem(GameObject displayPrefab, Texture2D image, string name, float price, int amount)
        {
            this.displayPrefab = displayPrefab;
            this.image = image; 
            this.name = name;
            this.price = price;
            this.amount = amount;
        }

        public float GetPrice()
        {
            return price;
        }

        public GameObject GetDisplayImage()
        {
            displayPrefab.GetComponent<RawImage>().texture = image;
            return displayPrefab;
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
            return new BasicItem(displayPrefab, image, name, price, amount);
        }

        public string GetName()
        {
            return name;
        }


    }
}
