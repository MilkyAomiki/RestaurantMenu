using Microsoft.AspNetCore.Mvc;
using Restaurant_menu.Model;
using Restaurant_menu.Pagination;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using System.Linq;

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
		public IActionResult Index(int page =1)
		{
			int pageSize = 20;

			var source = Menu.GetAll();
			var count = source.Count();
			var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
			IndexViewModel indexViewModel = new IndexViewModel
			{
				ItemsCount = count,
				PageViewModel =pageViewModel,
				Dishes = items
			};
			return View(indexViewModel);
		}

		[HttpGet("/Create")]
		public IActionResult Create()
		{
			return View("/Views/Main/Create.cshtml", new CreateModelView{ IsEdit = false, Dish = null});
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
			return View("/Views/Main/Create.cshtml", new CreateModelView { IsEdit = true, Dish = dish});
		}
	}

}



