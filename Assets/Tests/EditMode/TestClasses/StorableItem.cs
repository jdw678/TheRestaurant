using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Tests.TestClasses
{
    internal class StorableItem : IStorable
    {
        string name = "Test item";
        float amount;


        public StorableItem(string name, float amount)
        {
            this.name = name;
            this.amount = amount;
        }

        public IStorable Clone()
        {
            return new StorableItem("Test item", amount);
        }

        public float GetAmount()
        {
            return amount;
        }

        public GameObject GetDisplayImage()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return name;
        }

        public void SetAmount(float amount)
        {
            this.amount = amount;
        }
    }
}
