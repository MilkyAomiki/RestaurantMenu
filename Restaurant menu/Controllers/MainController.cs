using Microsoft.AspNetCore.Mvc;
using Restaurant_menu.Model;
using Restaurant_menu.Pagination;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Services;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant_menu.Controllers
{
	public class MainController : Controller
	{
		private readonly IMenu<DishDTO> _menu;

		public MainController(IMenu<DishDTO> menu)
		{
			_menu = menu;
		}

		[HttpGet("/")]
		public IActionResult Index(List<ItemConstraint> constraints, FieldTypes fieldTypeSort, int page =1)
		{
			int pageSize = 20;

			var source = _menu.GetAll(constraints, fieldTypeSort, page);
			var count = _menu.GetAll().Count();
			PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
			IndexViewModel indexViewModel = new IndexViewModel
			{
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



