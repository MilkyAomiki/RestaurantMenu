using RestaurantMenu.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.Models
{
    public class MenuModel
    {
        public IEnumerable<DishDTO> Dishes { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }
}
