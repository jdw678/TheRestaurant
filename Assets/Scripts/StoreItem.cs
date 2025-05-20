using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class StoreItem : MonoBehaviour, ISellableStorable
    {
        IStorable item;
        [SerializeField] float price;
        [SerializeField] TextMeshProUGUI text;

        public IStorable Clone()
        {
            throw new NotImplementedException();
        }

        public float GetAmount()
        {
            throw new NotImplementedException();
        }

        public GameObject GetDisplayImage()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public float GetPrice()
        {
            throw new NotImplementedException();
        }

        public void SetAmount(float amount)
        {
            throw new NotImplementedException();
        }

        void UpdateTextAmount(float price)
        {
            throw new NotImplementedException();
        }

        void DisableItemText(IStorable item)
        {
            throw new NotImplementedException();
        }

        public void Initialize(IStorable item, string name, float price, TextMeshProUGUI text)
        {
            throw new NotImplementedException();
        }
    }
}
