using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant_menu.Models;

namespace Restaurant_menu.Controllers
{
	public class MainController : Controller
	{
		public DishesContext Model { get; set; }

		public MainController(DishesContext model)
		{
			Model = model;
		}

		[HttpGet("/")]
		public IActionResult Index()
		{
			return View(Model);
		}

		[HttpGet("/Create")]
		public IActionResult CreatePage()
		{
			return View("/Views/Main/Create.cshtml", Model);
		}

		[HttpGet]
		public IActionResult Read(short id)
		{
			var dish = Model.Dish.Find(id);

			return View(dish);
		}
		[HttpGet("/Edit")]
		public IActionResult Edit(short id)
		{
			var dish = Model.Dish.Find(id);
			return View("/Views/Main/Create.cshtml", dish);
		}

		[HttpPost]
		public void Create(string name, string consist, string description, decimal price, int gram, double calorie, int cookTime) 
		{
			var dish = new Dish
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

			Model.Dish.Add(dish);
			Model.SaveChanges();

			Response.Redirect("/");
		}
	
		public void Delete(short id)
		{
			var dish = Model.Dish.Find(id);
			Model.Dish.Remove(dish);

			Model.SaveChanges();
			Response.Redirect("/");
		}
		
	}

}



