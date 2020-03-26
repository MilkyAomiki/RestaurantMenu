using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantMenu.BLL.Mapper
{
    public class DisplayNameAttribute : Attribute
    {
        private readonly string _name;
        public DisplayNameAttribute(string name)
        {
            _name = name;
        }

        public string Name { get => _name; }


    }
}
