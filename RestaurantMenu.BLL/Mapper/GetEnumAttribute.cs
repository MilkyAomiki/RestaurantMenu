using RestaurantMenu.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantMenu.BLL.Mapper
{
    public static class GetEnumAttribute
    {
        
        public static string GetName(this Enum enumItem)
        {
            return enumItem.GetType()
                .GetField(enumItem.ToString()).
                CustomAttributes.FirstOrDefault().
                ConstructorArguments.FirstOrDefault().
                Value.ToString();

        }
    }
}
