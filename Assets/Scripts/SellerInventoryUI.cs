using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteAlways]
    internal class SellerInventoryUI : MonoBehaviour, IDisplayable
    {

        IDisplayableSeller inventory;
        Inventory mockInventory;
        InventoryUI inventoryUI;
        [SerializeField] bool update;

        public void ToggleDisplay(bool isDisplaying)
        {
            inventory.ToggleDisplay(isDisplaying);
        }

        public void Update()
        {
            if(update && !Application.isPlaying)
                UpdateDisplay();
        }

        public void UpdateDisplay()
        {


        }


        void GetInventory()
        {

            var inventory = gameObject.GetComponentOfType<IDisplayableSeller>();
            if (inventory == null)
            {
                Debug.LogWarning($"Game Object \"{name}\" has a SellerInventoryUI component but is missing a component of type IDisplayableSeller!");
                return;
            }

            this.inventory = (IDisplayableSeller)inventory;
        }


        private void OnValidate()
        {
            GetInventory();
            update = true;
        }
    }
}
