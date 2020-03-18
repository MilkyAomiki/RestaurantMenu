using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;

namespace Restaurant_menu.Controllers
{
    public enum FiledTypes
    {
        Name = 111,
        CreateDate = 222,
        Consistence = 333,
        Description = 444,
        Price = 555,
        Gram = 666,
        Calorific = 777,
        CookTime = 888,
    }
    public class FilterController : Controller
    {
        private IMenu<Dish> _menu;
        public FilterController(IMenu<Dish> menu)
        {
            _menu = menu;
        }

        [HttpPost]
        public IEnumerable<short> Sorting(string name)
        {
            dynamic result;
            switch (name)
            {

                case "hName":
                     result = _menu.GetAll().OrderBy(n => n.Name).Select(i => i.Id);
                    return result;
                case "hCreateDate":
                     result = _menu.GetAll().OrderBy(n => n.CreateDate).Select(i => i.Id);
                    return result;
                case "hConsistence":
                     result = _menu.GetAll().OrderBy(n => n.Consist).Select(i => i.Id);
                    return result;
                case "hDescription":
                     result = _menu.GetAll().OrderBy(n => n.Description).Select(i => i.Id);
                    return result;
                case "hPrice":
                     result = _menu.GetAll().OrderBy(n => n.Price).Select(i => i.Id);
                    return result;
                case "hGram":
                    result = _menu.GetAll().OrderBy(n => n.Gram).Select(i => i.Id);
                    return result;
                case "hCalorific":
                    result = _menu.GetAll().OrderBy(n => n.Calorific).Select(i => i.Id);
                    return result;
                case "hCookTime":
                    result = _menu.GetAll().OrderBy(n => n.CookTime).Select(i => i.Id);
                    return result;
                default:
                    return null;
            }
        }

        [HttpPost]
        public IEnumerable<short> Filtration(string name, string filter)
        {
            dynamic result;
            switch (name)
            {
                case "name":
                    result = filter == null
                        ? _menu.GetAll().Select(x => x.Id)
                        : _menu.GetAll().Where(f => f.Name.ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case "createdate":
                    result = filter == null
                        ? _menu.GetAll().Select(x => x.Id)
                        : _menu.GetAll().Where(f => f.CreateDate.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);
                    return result;
                case "consistence":
                    result = filter == null
                        ? _menu.GetAll().Select(x => x.Id)
                        : _menu.GetAll().Where(f => f.Consist.ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case "description":
                    result = filter == null
                        ? _menu.GetAll().Select(x => x.Id)
                        : _menu.GetAll().Where(f => f.Description.ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case "price":
                    result = filter == null
                       ? _menu.GetAll().Select(x => x.Id)
                       : _menu.GetAll().Where(f => f.Price.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case "gram":
                    result = filter == null
                      ? _menu.GetAll().Select(x => x.Id)
                      : _menu.GetAll().Where(f => f.Gram.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case "calorific":
                    result = filter == null
                     ? _menu.GetAll().Select(x => x.Id)
                     : _menu.GetAll().Where(f => f.Calorific.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case "cooktime":
                    result = filter == null
                     ? _menu.GetAll().Select(x => x.Id)
                     : _menu.GetAll().Where(f => f.CookTime.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                default:
                    return null;
            }

        }
    }
}