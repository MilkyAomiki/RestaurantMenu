using Microsoft.AspNetCore.Mvc;
using Restaurant_menu.Models;
using RestaurantMenu.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant_menu.Controllers
{
   
    public class Filter: Controller
    {
        private IMenu<Dish> _menu;
        public Filter()
        {
        }
        public IEnumerable<Dish> Filtration(string name, string filter)
        {
            dynamic result;
            switch (name)
            {
                case "name":
                    result = _menu.GetAll().Where(f => String.CompareOrdinal(f.Name, filter) > 0).OrderBy(f => String.CompareOrdinal(f.Name, filter));
                    return result;
                case "createdate":
                    result = _menu.GetAll().Where(f => String.CompareOrdinal(f.CreateDate.ToString(), filter) > 0).OrderBy(f => String.CompareOrdinal(f.CreateDate.ToString(), filter));
                    return result;
                case "consistence":
                    break;
                case "description":
                    break;
                case "price":
                    break;
                case "gram":
                    break;
                case "calorific":
                    break;
                case "cooktime":
                    break;
                default:
                    return null;
            }
            return null;

        }
    }
}
