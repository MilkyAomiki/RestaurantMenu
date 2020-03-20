using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_menu.Controllers
{
    /// <summary>
    /// Provide a little tools for better code
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Calculate calorific in a dish
        /// </summary>
        /// <param name="calorific"> Calories per 100 gram </param>
        /// <param name="gram"> Grams of food </param>
        /// <returns> Dish calorific </returns>
        public static decimal CalculateCalorific(decimal calorific, int gram)
        {
           var calories = Decimal.Multiply(calorific, Decimal.Divide(Convert.ToDecimal(gram), Convert.ToDecimal(100)));
            return Convert.ToDecimal( decimal.Parse(calories.ToString()).ToString("G29"));
        }

    }
}
