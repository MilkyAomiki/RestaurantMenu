using RestaurantMenu.BLL.DTO;
using System.Collections.Generic;

namespace RestaurantMenu.BLL.Mapper

{/// <summary>
/// Intended for remap object Model - DTO
/// </summary>
    public static class DishMap
    {
        public static Restaurant_menu.Models.Dish GetDish(DishDTO item)
        {
            var entity = new Restaurant_menu.Models.Dish
            {
                Id = item.Id,
                CreateDate = item.CreateDate,
                Name = item.Name,
                Consist = item.Consist,
                Description = item.Description,
                Price = item.Price,
                Gram = item.Gram,
                Calorific = item.Calorific,
                CookTime = item.CookTime
            };

            return entity;
        }
        public static DishDTO GetDto(Restaurant_menu.Models.Dish item)
        {
            var entity = new DishDTO
            {
                Id = item.Id,
                CreateDate = item.CreateDate,
                Name = item.Name,
                Consist = item.Consist,
                Description = item.Description,
                Price = item.Price,
                Gram = item.Gram,
                Calorific = item.Calorific,
                CookTime = item.CookTime
            };

            return entity;
        }

        public static IEnumerable<DishDTO> GetDishes(IEnumerable<Restaurant_menu.Models.Dish> items)
        {
            List<DishDTO> dishes = new List<DishDTO>();

            foreach (var item in items)
            {
                var entity = new DishDTO
                {
                    Id = item.Id,
                    CreateDate = item.CreateDate,
                    Name = item.Name,
                    Consist = item.Consist,
                    Description = item.Description,
                    Price = item.Price,
                    Gram = item.Gram,
                    Calorific = item.Calorific,
                    CookTime = item.CookTime
                };

                dishes.Add(entity);
            }
            return dishes;
        }

    }
}
