using Restaurant_menu.Context;
using RestaurantMenu.BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.DI
{
    /// <summary>
    /// Dependency injection module <br/>
    /// <i> Calling from Startup.cs </i>
    /// </summary>
    public class DependencyModule
    {
        string _connection;
        public DependencyModule(string connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// Provides parameters for MenuService
        /// </summary>
        /// <returns></returns>
        public MenuService ConfigureMenuService()
        {
            var context = new DishesContext(_connection);
            return new MenuService(context);
        }
    }
}
