using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Restaurant_menu.Models;

namespace Restaurant_menu.Controllers
{
	public class MainController: Controller
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
		public IActionResult Create()
		{
			return View(Model);
		}
	}


}
