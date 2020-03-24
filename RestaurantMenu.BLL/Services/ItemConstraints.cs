using RestaurantMenu.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.Services
{
    public class ItemConstraint
    {
        public FieldTypes Key { get; set; }
        public string Value { get; set; }
    }
    public class SortedItems
    {
        public FieldTypes Key { get; set; }
        public bool IsSorted { get; set; }
    }

}
