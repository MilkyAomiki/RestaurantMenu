using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant_menu.Controllers
{
    public class FilterController : Controller
    {
        private readonly IFiltration _filtration;

        public FilterController(IFiltration filtration)
        {
            _filtration = filtration;
        }

        /// <summary>
        /// Sort fields by ascending.
        /// </summary>
        /// <param name="name"> Type of field</param>
        /// <returns>Sorted list of dishes</returns>
        [HttpPost]
        public IEnumerable<short> Sorting(FieldTypes name)
        {
           return _filtration.Sort(name);
        }

        /// <summary>
        /// Filter list by one parameter
        /// </summary>
        /// <param name="name">Type of field</param>
        /// <param name="filter">Filter template</param>
        /// <returns></returns>
        [HttpPost]
        public IEnumerable<short> Filtration(FieldTypes name, string filter)
        {
            return _filtration.FilterOut(name, filter);
        }
    }
}