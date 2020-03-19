using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;

namespace Restaurant_menu.Controllers
{
    /// <summary>
    ///  Не сочетаются поля в сортировке.
    /// Сочетаются сортировка и фильтрация.
    /// </summary>
    public enum FieldTypes
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
        public IEnumerable<short> Sorting(FieldTypes name)
        {
            dynamic result;
            switch (name)
            {

                case FieldTypes.Name:
                     result = _menu.GetAll().OrderBy(n => n.Name).Select(i => i.Id);
                    return result;
                case FieldTypes.CreateDate:
                     result = _menu.GetAll().OrderBy(n => n.CreateDate).Select(i => i.Id);
                    return result;
                case FieldTypes.Consistence:
                     result = _menu.GetAll().OrderBy(n => n.Consist).Select(i => i.Id);
                    return result;
                case FieldTypes.Description:
                     result = _menu.GetAll().OrderBy(n => n.Description).Select(i => i.Id);
                    return result;
                case FieldTypes.Price:
                     result = _menu.GetAll().OrderBy(n => n.Price).Select(i => i.Id);
                    return result;
                case FieldTypes.Gram:
                    result = _menu.GetAll().OrderBy(n => n.Gram).Select(i => i.Id);
                    return result;
                case FieldTypes.Calorific:
                    result = _menu.GetAll().OrderBy(n =>  Decimal.Multiply(n.Calorific, Decimal.Divide(Convert.ToDecimal(n.Gram), Convert.ToDecimal(100)))).Select(i => i.Id);
                    return result;
                case FieldTypes.CookTime:
                    result = _menu.GetAll().OrderBy(n => n.CookTime).Select(i => i.Id);
                    return result;
                default:
                    return null;
            }
        }

        [HttpPost]
        public IEnumerable<short> Filtration(FieldTypes name, string filter)
        {
            dynamic result;
            switch (name)
            {
                case FieldTypes.Name:
                    result = filter == null
                        ? _menu.GetAll().Select(x => x.Id)
                        : _menu.GetAll().Where(f => f.Name.ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case FieldTypes.CreateDate:
                    result = filter == null
                        ? _menu.GetAll().Select(x => x.Id)
                        : _menu.GetAll().Where(f => f.CreateDate.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);
                    return result;
                case FieldTypes.Consistence:
                    result = filter == null
                        ? _menu.GetAll().Select(x => x.Id)
                        : _menu.GetAll().Where(f => f.Consist.ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case FieldTypes.Description:
                    result = filter == null
                        ? _menu.GetAll().Select(x => x.Id)
                        : _menu.GetAll().Where(f => f.Description.ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case FieldTypes.Price:
                    result = filter == null
                       ? _menu.GetAll().Select(x => x.Id)
                       : _menu.GetAll().Where(f => f.Price.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case FieldTypes.Gram:
                    result = filter == null
                      ? _menu.GetAll().Select(x => x.Id)
                      : _menu.GetAll().Where(f => f.Gram.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case FieldTypes.Calorific:
                    result = filter == null
                     ? _menu.GetAll().Select(x => x.Id)
                     : _menu.GetAll().Where(f => f.Calorific.ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);

                    return result;
                case FieldTypes.CookTime:
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