using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Restaurant_menu.Model;
using Restaurant_menu.Pagination;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
		public IActionResult Index(string Name, string CreateDate, string Consistence, string Description, string Price, string Gram, string Calorific, string CookTime, FieldTypes fieldTypeSort, bool desc, int page =1)
		{
		
			//Filters
			List<ItemConstraint> constraints = new List<ItemConstraint>();

			constraints.Add(new ItemConstraint { Key = FieldTypes.Name, Value = Name?? "" });
			constraints.Add(new ItemConstraint { Key = FieldTypes.CreateDate, Value = CreateDate?? "" });
			constraints.Add(new ItemConstraint { Key = FieldTypes.Consistence, Value = Consistence?? "" });
			constraints.Add(new ItemConstraint { Key = FieldTypes.Description, Value = Description?? "" });
			constraints.Add(new ItemConstraint { Key = FieldTypes.Price, Value = Price?? "" });
			constraints.Add(new ItemConstraint { Key = FieldTypes.Gram, Value = Gram?? "" });
			constraints.Add(new ItemConstraint { Key = FieldTypes.Calorific, Value = Calorific?? "" });
			constraints.Add(new ItemConstraint { Key = FieldTypes.CookTime, Value = CookTime?? "" });
			
			//Sort fields
			List<SortedItems> sortedItems = new List<SortedItems>();

			sortedItems.Add(new SortedItems { Key = FieldTypes.Name, IsSorted = FieldTypes.Name == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Name? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.CreateDate, IsSorted = FieldTypes.CreateDate == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.CreateDate ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Consistence, IsSorted = FieldTypes.Consistence == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Consistence ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Description, IsSorted = FieldTypes.Description == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Description ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Price, IsSorted = FieldTypes.Price == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Price ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Gram, IsSorted = FieldTypes.Gram == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Gram ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.Calorific, IsSorted = FieldTypes.Calorific == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.Calorific ? desc : false });
			sortedItems.Add(new SortedItems { Key = FieldTypes.CookTime, IsSorted = FieldTypes.CookTime == fieldTypeSort, Desc = fieldTypeSort == FieldTypes.CookTime ? desc : false });


			int pageSize = 20;
			var source = _menu.GetAll(constraints, fieldTypeSort, desc, page, pageSize);
			var count = source.TotalCount;
			var currentItemsCount = source.Count;
			PageViewModel pageViewModel = new PageViewModel(currentItemsCount, page, pageSize);
			IndexViewModel indexViewModel = new IndexViewModel
			{
				ItemsCount = count,
				PageViewModel = pageViewModel,
				Dishes = source.Dishes,
				Filters = constraints,
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



