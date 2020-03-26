using Restaurant_menu.Context;
using RestaurantMenu.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantMenu.BLL.Validation
{
    internal class Validator
    {
        internal void ValidateName(DishDTO item, DishesContext context)
        {
            foreach (var i in context.Dish)
            {
                if (i.Name.ToLower().Trim() == item.Name.ToLower().Trim() && i.Id != item.Id)
                {
                    throw new ValidationException(new ValidationResult("Field with given name is already exist", new List<string>() { "name" }), null, null);
                }
            }

        }

        internal void ValidateCreationDate(DishDTO changedItem, DishesContext context)
        {
            var item = context.Dish.Find(changedItem.Id);
            if (item.CreateDate != changedItem.CreateDate)
            {
                
                throw new ValidationException(new ValidationResult("Changing create date is not allowed", new List<string>() { "createDate" }), null, null);
            }
        }

        internal void IsExist(DishDTO item, DishesContext context)
        {
            if (context.Dish.Find(item.Id) == null)
            {
                throw new ArgumentNullException("Dish is not exist");
            }
        }
    }
}
