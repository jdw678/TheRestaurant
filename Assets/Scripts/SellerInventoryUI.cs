using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class SellerInventoryUI : MonoBehaviour, IDisplayable
    {

        IDisplayableStorer inventory;

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
