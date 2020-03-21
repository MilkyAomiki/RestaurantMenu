using RestaurantMenu.BLL.Interfaces;
using System;

namespace RestaurantMenu.BLL.Services
{
    /// <summary>
    /// <inheritdoc cref="ITools"/>
    /// </summary>
    public class ToolsService : ITools
    {
        public decimal CalculateCalorific(decimal calorific, int gram)
        {
            var calories = Decimal.Multiply(calorific, Decimal.Divide(Convert.ToDecimal(gram), Convert.ToDecimal(100)));
            return Convert.ToDecimal(decimal.Parse(calories.ToString()).ToString("G29"));
        }

    }
}
