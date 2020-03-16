using Restaurant_menu.Context;
using RestaurantMenu.BLL.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.BLL.DI
{
    public class DependencyModule
    {
        string _connection;
        public DependencyModule(string connection)
        {
            _connection = connection;
        }
        public MenuService ConfigureMenuService()
        {
            var context = new DishesContext(_connection);
            return new MenuService(context);
        }
    }
}
