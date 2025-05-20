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
        GameObject gameObject;

        public StorableItem(string name, float amount)
        {
            this.name = name;
            this.amount = amount;
            gameObject = new GameObject();
        }

        public StorableItem(string name, float amount, GameObject gameObject)
        {
            this.name = name;
            this.amount = amount;
            this.gameObject = gameObject;
        }

        public IStorable Clone()
        {
            return new StorableItem(name, amount);
        }

        public float GetAmount()
        {
            return amount;
        }

        public GameObject GetDisplayImage()
        {
            return gameObject;
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
