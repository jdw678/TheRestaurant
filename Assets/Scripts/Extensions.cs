using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal static class Extensions
    {

        public static IEnumerable<Component> GetComponentsOfType<T>(this GameObject obj)
        {
            return obj.GetComponents<Component>().Where(x => x is T);
        }

        public static Component GetComponentOfType<T>(this GameObject obj)
        {
            var component = obj.GetComponents<Component>().First(x => x is T);
            return component;
        }

    }
}
