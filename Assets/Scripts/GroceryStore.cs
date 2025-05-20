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
    public class GroceryStore : MonoBehaviour, IDisplayable
    {
        [SerializeField] SellerInventory inventory;
        [SerializeField] Button buyButton;
        public float BuyItem(int column, int row, float amount)
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
    }
}
