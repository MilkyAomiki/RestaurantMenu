using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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


		//TODO: Show previous state after create / show (/ delete?)
		[HttpGet("/")]
		//                           json Constraints             can be none 
		public IActionResult Index(string constraints, FieldTypes fieldTypeSort, bool desc, int page =1)
		{
			List<ItemConstraint> parsedconstraints = new List<ItemConstraint>();

			if (constraints != null)
			{
				
				JArray jArray = JArray.Parse(constraints);
				foreach (var filter in jArray)
				{
					if (!filter.HasValues)
					{
						continue;
					}
					var itemConstraint = filter.ToObject<ItemConstraint>();
					parsedconstraints.Add(itemConstraint);
				}

			}

			List<SortedItems> sortedItems = new List<SortedItems>();
			sortedItems.Add(new SortedItems { Key = FieldTypes.Name, IsSorted = FieldTypes.Name == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Name? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.CreateDate, IsSorted = FieldTypes.CreateDate == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.CreateDate ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Consistence, IsSorted = FieldTypes.Consistence == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Consistence ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Description, IsSorted = FieldTypes.Description == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Description ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Price, IsSorted = FieldTypes.Price == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Price ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Gram, IsSorted = FieldTypes.Gram == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Gram ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Calorific, IsSorted = FieldTypes.Calorific == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Calorific ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.CookTime, IsSorted = FieldTypes.CookTime == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.CookTime ? desc : false });

			//TODO: Paging after filtration

			int pageSize = 20;
			var source = _menu.GetAll(parsedconstraints, fieldTypeSort, desc, page, pageSize);
			var count = _menu.GetAll().Count();
			var currentItemsCount = source.Count;
			PageViewModel pageViewModel = new PageViewModel(currentItemsCount, page, pageSize);
			IndexViewModel indexViewModel = new IndexViewModel
			{
				ItemsCount = count,
				PageViewModel = pageViewModel,
				Dishes = source.Dishes,
				Filters = parsedconstraints,
				Sorting = sortedItems
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



