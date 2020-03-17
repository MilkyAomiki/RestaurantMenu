using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant_menu.Controllers
{
    public class LogicController : Controller
    {
        private IMenu<Dish> _menu;

        public LogicController(IMenu<Dish> menu)
        {
            _menu = menu;
        }


        [HttpPost]
        public void Edit(short id, string name, string consist, string description, decimal price, int gram, double calorie, int cookTime)
        {
            Dish entity = new Dish
            {
                Id = id,
                Name = name,
                Consist = consist,
                Description = description,
                Price = price,
                Gram = gram,
                Calorific = calorie,
                CookTime = cookTime,
                CreateDate = DateTime.Now
            };
            _menu.Update(entity);

            Response.Redirect("/");
        }

        [HttpPost]
        public void Create(string name, string consist, string description, decimal price, int gram, double calorie, int cookTime)
        {
            var entity = new Dish
            {
                Consist = consist,
                Calorific = calorie,
                CookTime = cookTime,
                CreateDate = DateTime.Now,
                Description = description,
                Gram = gram,
                Name = name,
                Price = price
            };

            _menu.Create(entity);

            Response.Redirect("/");
        }

        [HttpPost]
        public int Delete(int id)
        {
            _menu.Delete(id);
            return _menu.GetAll().Count();
        }





    }
}
