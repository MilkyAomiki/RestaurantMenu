using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.Interfaces
{
    /// <summary>
    /// Types of field <i>(Dish parameters)</i> 
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
        None = 000
    }
    public interface IFiltration
    {

        /// <summary>
        /// Filter list by one parameter
        /// </summary>
        /// <param name="name">Type of field</param>
        /// <param name="filter">Filter template</param>
        /// <returns></returns>
        IEnumerable<short> FilterOut(FieldTypes name, string filter);


        /// <summary>
        /// Sort fields by ascending.
        /// </summary>
        /// <param name="name"> Type of field</param>
        /// <returns>Sorted list of dishes</returns>
        IEnumerable<short> Sort(FieldTypes name);
    }
}
