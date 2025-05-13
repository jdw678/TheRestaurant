using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Tests.TestClasses
{
    internal class StorableItem : IStorable
    {
        string name = "Test item";


        public StorableItem(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }
    }
}
