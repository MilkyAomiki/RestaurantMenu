using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantMenu.BLL.Mapper
{
    public class DisplayName : Attribute
    {
        private readonly string _name;
        public DisplayName(string name)
        {
            _name = name;
        }

        public string Name { get => _name; }


    }
}
