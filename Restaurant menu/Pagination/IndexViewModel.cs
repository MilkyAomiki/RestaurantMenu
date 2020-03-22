using RestaurantMenu.BLL.DTO;
using System.Collections.Generic;

namespace Restaurant_menu.Pagination
{
    /// <summary>
    /// Model for Index View
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Total dishes count
        /// </summary>
        public int ItemsCount { get; set; }
        public List<short> VisibleItems { get; set; }
        public IEnumerable<Dish> Dishes { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
