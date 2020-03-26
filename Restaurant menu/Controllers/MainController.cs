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
using System.ComponentModel.DataAnnotations;
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

        #region CRUD


        [HttpPost]
        public IActionResult EditItem([Bind(Prefix = "Dish")] DishDTO entity)
        {
            if (!ModelState.IsValid)
            {
                return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = true, Dish = entity });
            }

            if (entity.Name == null && entity.Consist == null && entity.Description == null && entity.Calorific == 0)
            {
                var form = Request.Form;
                entity.Name = form.FirstOrDefault(p => p.Key == "Dish.Name").Value;
                entity.Description = form.FirstOrDefault(p => p.Key == "Dish.Description").Value;
                entity.Consist = form.FirstOrDefault(p => p.Key == "Dish.Consist").Value;
                entity.Price = Convert.ToDecimal(form.FirstOrDefault(p => p.Key == "Dish.Price").Value);
                entity.Gram = int.Parse(form.FirstOrDefault(p => p.Key == "Dish.Gram").Value);
                entity.Calorific = Convert.ToDecimal(form.FirstOrDefault(p => p.Key == "Dish.Calorific").Value);
                entity.CookTime = int.Parse(form.FirstOrDefault(p => p.Key == "Dish.CookTime").Value);

            }
            try
            {
                entity.CreateDate = _menu.Get(entity.Id).CreateDate;
                _menu.Update(entity);

                Response.Redirect(Request.Path.ToString());
            }

            catch (ValidationException e)
            {
                if (e.ValidationResult.MemberNames.Contains("name"))
                {
                    ModelState.AddModelError("Name", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = true, Dish = entity });
                }
                else if (e.ValidationResult.MemberNames.Contains("createDate"))
                {
                    ModelState.AddModelError("CreateDate", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = true, Dish = entity });
                }

            }
            catch (Exception p)
            {
                ModelState.AddModelError("", p.Message);
            };
            return Redirect("/");
        }



        [HttpPost]
        public IActionResult CreateItem([Bind(Prefix = "Dish")] DishDTO entity)
        {
            if (ModelState.Where(p => p.Key == "Dish.Id").FirstOrDefault().Value.Errors.Count != 0)
            {
                ModelState.Where(p => p.Key == "Dish.Id").FirstOrDefault().Value.ValidationState = Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid;
            }
            if (!ModelState.IsValid)
            {


                return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = false, Dish = entity });
            }
            if (entity.Name == null && entity.Consist == null && entity.Description == null && entity.Calorific == 0 && entity.Id == 0)
            {
                var form = Request.Form;

                entity.Name = form.FirstOrDefault(p => p.Key == "Dish.Name").Value;
                entity.Description = form.FirstOrDefault(p => p.Key == "Dish.Description").Value;
                entity.Consist = form.FirstOrDefault(p => p.Key == "Dish.Consist").Value;
                entity.Price = Convert.ToDecimal(form.FirstOrDefault(p => p.Key == "Dish.Price").Value);
                entity.Gram = int.Parse(form.FirstOrDefault(p => p.Key == "Dish.Gram").Value);
                entity.Calorific = Convert.ToDecimal(form.FirstOrDefault(p => p.Key == "Dish.Calorific").Value);
                entity.CookTime = int.Parse(form.FirstOrDefault(p => p.Key == "Dish.CookTime").Value);
            }
            try
            {

                entity.CreateDate = DateTime.Now;
                _menu.Create(entity);
                Response.Redirect("/");
            }

            catch (ValidationException e)
            {
                if (e.ValidationResult.MemberNames.Contains("name"))
                {
                    ModelState.AddModelError("Name", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = false, Dish = entity });
                }
                else if (e.ValidationResult.MemberNames.Contains("createDate"))
                {
                    ModelState.AddModelError("CreateDate", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = false, Dish = entity });
                }

            }catch(Exception p)
            {
                ModelState.AddModelError("", p.Message);
            }
            return Content(" ");
        }

        [HttpPost]
        public int DeleteItem(int id)
        {

            _menu.Delete(id);
            return _menu.GetTotalAmount();
        }





        #endregion

    }


}



