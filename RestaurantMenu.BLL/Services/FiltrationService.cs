using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantMenu.BLL.Services
{
    /// <summary>
    /// <inheritdoc cref="IFiltration"/>
    /// </summary>
    public class FiltrationService : IFiltration
    {

        private readonly IMenu<Dish> _menu;
        private readonly ITools _tools;

        public FiltrationService(IMenu<Dish> menu, ITools tools)
        {
            _menu = menu;
            _tools = tools;
        }

        public IEnumerable<short> Sort(FieldTypes name)
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
                    result = _menu.GetAll().OrderBy(n => _tools.CalculateCalorific(n.Calorific, n.Gram)).Select(i => i.Id);
                    return result;
                case FieldTypes.CookTime:
                    result = _menu.GetAll().OrderBy(n => n.CookTime).Select(i => i.Id);
                    return result;
                default:
                    return null;
            }
        }

        public IEnumerable<short> FilterOut(FieldTypes name, string filter)
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
                     : _menu.GetAll().Where(f => _tools.CalculateCalorific(f.Calorific, f.Gram).ToString().ToLower().Contains(filter.ToLower())).Select(i => i.Id);

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
