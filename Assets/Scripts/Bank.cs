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
    internal class Bank : MonoBehaviour, IDisplayableBanker
    {
        [SerializeField] float money = 0;
        [SerializeField] TextMeshProUGUI moneyText;

        public void AddMoney(float amount)
        {
            money += amount;
        }

        public float GetMoney()
        {
            return money;
        }

        public void RemoveMoney(float amount)
        {
            if (amount > money)
                throw new Exception($"Not enough money in bank. Money requested: {amount}, Money in bank: {money}");

            money -= amount;
        }

        public void ToggleDisplay(bool isDisplaying)
        {
            moneyText.gameObject.SetActive(isDisplaying);
        }

        public void UpdateDisplay()
        {
            moneyText.SetText($"{money:c0}");
        }

        private void OnValidate()
        {
            UpdateDisplay();
        }
    }
}
