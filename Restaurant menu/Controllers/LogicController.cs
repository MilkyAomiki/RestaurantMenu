using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using System;

namespace Restaurant_menu.Controllers
{
	public class LogicController: Controller
    {
        public IMenu<Dish> Menu { get; set; }

        public LogicController(IMenu<Dish> menu)
        {
            Menu = menu;
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
			Menu.Update(entity);

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

			Menu.Create(entity);

			Response.Redirect("/");
		}

		public void Delete(int id)
		{
			Menu.Delete(id);
			Response.Redirect("/");
		}

	}
}
