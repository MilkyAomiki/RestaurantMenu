using RestaurantMenu.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_menu.Model
{
    public class CreateModelView
    {
        public bool IsEdit { get; set; }
        public Dish Dish { get; set; }
    }
}
