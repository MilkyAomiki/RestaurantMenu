using RestaurantMenu.BLL.DTO;

namespace Restaurant_menu.Model
{
    /// <summary>
    /// Model for Create / Update  View
    /// </summary>
    public class CreateViewModel
    {
        /// <summary>
        /// Is it view for edit item?
        /// </summary>
        public bool IsEdit { get; set; }

        /// <summary>
        /// Item for edit
        /// </summary>
        public DishDTO Dish { get; set; }

        /// <summary>
        ///  Index page URL 
        /// </summary>
        public string URL { get; set; }
    }
}
