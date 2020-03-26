using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Services;
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
        public List<ItemConstraint> Filters { get; set; }
        public List<SortedItems> Sorting { get; set; }
        public IEnumerable<DishDTO> Dishes { get; set; }
        public PageViewModel PageViewModel { get; set; }

        
    }
}
