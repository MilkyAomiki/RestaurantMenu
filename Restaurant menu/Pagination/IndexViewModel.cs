using RestaurantMenu.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_menu.Pagination
{
    public class IndexViewModel
    {
        public int ItemsCount { get; set; }
        public IEnumerable<Dish> Dishes { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
