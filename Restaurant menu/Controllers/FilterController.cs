using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;

namespace Restaurant_menu.Controllers
{
    public class FilterController : Controller
    {
        private IMenu<Dish> _menu;
        public FilterController(IMenu<Dish> menu)
        {
            _menu = menu;
        }

        [HttpPost]
        public IEnumerable<short> Filtration(string name, string filter)
        {
            dynamic result;
            switch (name)
            {
                case "name":
                    result = _menu.GetAll().Where(f => f.Name.ToLower().Contains(filter.ToLower())).Select(i => i.Id);
                    return result;
                case "createdate":
                    result = _menu.GetAll().Where(f => String.CompareOrdinal(f.CreateDate.ToString(), filter) > 0).OrderBy(f => String.CompareOrdinal(f.CreateDate.ToString(), filter)).Select(x => x);
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