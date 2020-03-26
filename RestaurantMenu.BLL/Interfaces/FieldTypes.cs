using RestaurantMenu.BLL.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.Interfaces
{
    /// <summary>
    /// Types of field <i>(Dish parameters)</i> 
    /// </summary>
    public enum FieldTypes
    {
        [DisplayName("Название")]
        Name = 111,
        [DisplayName("Дата создания")]
        CreateDate = 222,
        [DisplayName("Состав")]
        Consistence = 333,
        [DisplayName("Описание")]
        Description = 444,
        [DisplayName("Цена")]
        Price = 555,
        [DisplayName("Граммовка")]
        Gram = 666,
        [DisplayName("Калорийность")]
        Calorific = 777,
        [DisplayName("Время приготовления")]
        CookTime = 888,
        None = 000
    }
}
