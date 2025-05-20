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
    public class BasicItem : MonoBehaviour, IStorable
    {

        [SerializeField] TextMeshProUGUI text;
        [SerializeField] float amount;
        [SerializeField] GameObject displayPrefab;
        [SerializeField] string itemName;

        public IStorable Clone()
        {
            return Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<IStorable>();
        }

        public float GetAmount()
        {
            return amount;
        }

        public GameObject GetDisplayImage()
        {
            return displayPrefab;
        }

        public string GetName()
        {
            return itemName;
        }

        public void SetAmount(float amount)
        {
            this.amount = amount;
            UpdateText(amount);
        }

        void UpdateText(float amount)
        {
            //get the text object
            if (text == null)
                text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

            //if no text object add one
            if(text == null)
            {
                GameObject obj = new GameObject("Text");
                text = obj.AddComponent<TextMeshProUGUI>();
                text.transform.SetParent(transform);
            }

            //set the text
            text.text = $"{amount:n0}";
        }

        public void Initialize(TextMeshProUGUI text, float amount, GameObject displayPrefab, string itemName)
        {
            this.text = text;
            this.amount = amount;
            this.displayPrefab = displayPrefab;
            this.itemName = itemName;

            this.text.text = $"{amount:n0}";
        }
    }
}
