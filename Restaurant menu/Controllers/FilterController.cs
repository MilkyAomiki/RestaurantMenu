using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using System;
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
            var kk = HttpContext.Request.Form.Keys;
            var filters = new Dictionary<FieldTypes, string>();
            try
            {
                JArray array = JArray.Parse(kk.ElementAt(0));

                foreach (var item in array)
                {
                    var key = item.SelectToken("name").ToString();
                    var value = item.SelectToken("value").ToString();

                    filters.Add(GetFieldType(key), value);

                    //foreach (var it in (JObject)item)
                    //{
                        
                    //    var k = it.Key;
                    //    var v = it.Value;
                    //}   
                }
                
            }
            catch (Exception e)
            {
                return null;
            }
            var idofitems = new List<short>();
            foreach (var f in filters)
            {
                if (idofitems.Count == 0)
                {
                    idofitems = _filtration.FilterOut(f.Key, f.Value).ToList();
                }
                else
                {
                    idofitems = _filtration.FilterOut(f.Key, f.Value).Intersect(idofitems).ToList();
                }
                
            }

            return idofitems;
        }

        public FieldTypes GetFieldType(string stringType)
        {
            return stringType switch
            {
                "Name" => FieldTypes.Name,
                "CreateDate" => FieldTypes.CreateDate,
                "Consistence" => FieldTypes.Consistence,
                "Description" => FieldTypes.Description,
                "Price" => FieldTypes.Price,
                "Gram" => FieldTypes.Gram,
                "Calorific" => FieldTypes.Calorific,
                "CookTime" => FieldTypes.CookTime,
                _ => 0
            };
        }

    }
}