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
		private readonly IMenu<Dish> _menu;

		public MainController(IMenu<Dish> menu)
		{
			_menu = menu;
		}

		[HttpGet("/")]
		public IActionResult Index(int page =1)
		{
			int pageSize = 20;

			var source = _menu.GetAll();
			var count = source.Count();
			var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
			IndexViewModel indexViewModel = new IndexViewModel
			{
				VisibleItems = items.Select(p => p.Id).ToList(),
				ItemsCount = count,
				PageViewModel =pageViewModel,
				Dishes = source
			};
			return View(indexViewModel);
		}

		[HttpGet("/Create")]
		public IActionResult Create()
		{
			return View("/Views/Main/Create.cshtml", new CreateViewModel{ IsEdit = false, Dish = null});
		}

		[HttpGet]
		public IActionResult Read(int id)
		{
			var dish = _menu.Get(id);

			return View(dish);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var dish = _menu.Get(id);
			return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = true, Dish = dish});
		}
	}

}



