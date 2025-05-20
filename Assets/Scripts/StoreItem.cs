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
        [SerializeField] Color color;

        public IStorable Clone()
        {
            return item.Clone();
        }

        public float GetAmount()
        {
            return item.GetAmount();
        }

        public GameObject GetDisplayImage()
        {
            return item.GetDisplayImage();
        }

        public string GetName()
        {
            return item.GetName();
        }

        public float GetPrice()
        {
            return price;
        }

        public void SetAmount(float amount)
        {
            throw new NotImplementedException();
        }

        void AddTextIfNecessary()
        {
            //get all text objects
            GameObject itemPrefab = item.GetDisplayImage();
            List<TextMeshProUGUI> texts = itemPrefab.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            //make sure this object is on the display prefab
            if (!texts.Contains(text))
            {
                text.transform.SetParent(itemPrefab.transform);

            }
        }
        List<TextMeshProUGUI> AddTextIfNecessary(List<TextMeshProUGUI> texts)
        {
            //make sure this object is on the display prefab
            if (!texts.Contains(text))
            {
                //add it
                GameObject obj = new GameObject("Price Text");
                text = obj.AddComponent<TextMeshProUGUI>();
                texts.Add(text);
            }

            return texts;
        }

        void UpdateTextAmount(float price)
        {
            AddTextIfNecessary();

            //set the text and color
            text.SetText($"{price:c0}");
            text.color = color;

            //Disable item count text
            DisableItemText(item);
        }

        void DisableItemText(IStorable item)
        {
            //get all text objects
            GameObject itemPrefab = item.GetDisplayImage();
            List<TextMeshProUGUI> texts = itemPrefab.GetComponentsInChildren<TextMeshProUGUI>().ToList();

            //make sure we have our text on it
            texts = AddTextIfNecessary(texts);

            //disable all texts but our own
            foreach(TextMeshProUGUI text in texts)
            {
                if (text == this.text)
                    continue;

                text.gameObject.SetActive(false);
            }
        }

        public void Initialize(IStorable item, float price, TextMeshProUGUI text)
        {
            this.item = item;
            this.price = price;
            this.text = text;

            UpdateTextAmount(price);

        }
    }
}
