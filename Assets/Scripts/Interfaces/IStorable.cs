 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace Assets.Scripts.Interfaces
{
    public interface IStorable : IIdentifiable
    {
        RawImage GetDisplayImage();
        float GetAmount();
        void SetAmount(float amount);
        IStorable Clone();
    }
}
