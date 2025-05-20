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
    public class Store : MonoBehaviour, IDisplayable
    {
        [SerializeField] ISelector<ISellableStorable> inventory;
        [SerializeField] Button buyButton;
        public ISellableStorable BuyItem(IBanker playerBank)
        {
            throw new NotImplementedException();
        }

        public void ToggleDisplay(bool isDisplaying)
        {
            throw new NotImplementedException();
        }

        public void UpdateDisplay()
        {
            throw new NotImplementedException();
        }

        public void Initialize(ISelector<ISellableStorable> inventory, Button buyButton)
        {

        }
    }
}
