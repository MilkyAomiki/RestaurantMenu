using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;

namespace Restaurant_menu.Controllers
{
	public class MainController : Controller
	{
		public IMenu<Dish> Menu { get; set; }

		public MainController(IMenu<Dish> menu)
		{
			Menu = menu;
		}

		[HttpGet("/")]
		public IActionResult Index()
		{
			return View(Menu.GetAll());
		}

		[HttpGet("/Create")]
		public IActionResult Create()
		{
			return View("/Views/Main/Create.cshtml", new Dish {Id = -1});
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			var dish = Menu.Get(id);

			return View(dish);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var dish = Menu.Get(id);
			return View("/Views/Main/Create.cshtml", dish);
		}
	}

}



