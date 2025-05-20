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

        public IStorable Clone()
        {
            return Instantiate(gameObject, transform.position, Quaternion.identity).GetComponent<IStorable>();
        }

        public float GetAmount()
        {
            throw new NotImplementedException();
        }

        public GameObject GetDisplayImage()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public void SetAmount(float amount)
        {
            throw new NotImplementedException();
        }

        void UpdateText(float amount)
        {
            throw new NotImplementedException();
        }

        public void Initialize(TextMeshProUGUI text, float amount, GameObject displayPrefab)
        {
            throw new NotImplementedException();
        }
    }
}
