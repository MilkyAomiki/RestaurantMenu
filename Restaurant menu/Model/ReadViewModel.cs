using RestaurantMenu.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_menu.Model
{
    public class ReadViewModel
    {
        public DishDTO Dish { get; set; }

        /// <summary>
        /// Index page URL
        /// </summary>
        public string URL { get; set; }
    }
}
