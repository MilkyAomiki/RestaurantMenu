using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.Interfaces
{
    /// <summary>
    /// Provide a little tools for better code
    /// </summary>
    public interface ITools
    {

        /// <summary>
        /// Calculate calorific in a dish
        /// </summary>
        /// <param name="calorific"> Calories per 100 gram </param>
        /// <param name="gram"> Grams of food </param>
        /// <returns> Dish calorific </returns>
        decimal CalculateCalorific(decimal calorific, int gram);
    }
}
