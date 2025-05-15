 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IStorable
    {
        UnityEngine.UI.Image GetDisplayImage();
        float GetAmount();
        void SetAmount(float amount);
        IStorable Clone();
    }
}
