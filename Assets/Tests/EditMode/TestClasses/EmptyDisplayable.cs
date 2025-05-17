using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Tests.EditMode.TestClasses
{
    internal class EmptyDisplayable : IDisplayable
    {
        public void ToggleDisplay(bool isDisplaying)
        {
            return;
        }

        public void UpdateDisplay()
        {
            return;
        }
    }
}
