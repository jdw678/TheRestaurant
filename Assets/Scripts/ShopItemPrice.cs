using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scripts
{
    [ExecuteAlways]
    public class ShopItemPrice : MonoBehaviour
    {
        [SerializeField] float price = -1;
        [SerializeField] TextMeshProUGUI textObj;
        private void Awake()
        {
            ISellableStorable item = (ISellableStorable)transform.parent.gameObject.GetComponentOfType<ISellableStorable>();
            if (item == null)
                return;


            textObj.SetText($"{item.GetPrice():c0}");
            price = item.GetPrice();

        }
        private void Update()
        {
            if (price.Equals(-1))
                Awake();

        }

        private void OnValidate()
        {
            if (price.Equals(-1))
                Awake();
        }

    }
}
