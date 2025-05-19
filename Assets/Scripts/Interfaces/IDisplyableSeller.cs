using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    public interface IDisplayableSeller : ISeller
    {
        void ToggleDisplay(bool isDisplaying);
        int GetRows();
        int GetColumns();
    }
}
