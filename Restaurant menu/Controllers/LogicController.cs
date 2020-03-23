using Microsoft.AspNetCore.Mvc;
using Restaurant_menu.Model;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Restaurant_menu.Controllers
{
    /// <summary>
    /// CRUD
    /// </summary>
    public class LogicController : Controller
    {
        private readonly IMenu<DishDTO> _menu;

        public LogicController(IMenu<DishDTO> menu)
        {
            _menu = menu;
        }

        [HttpPost]
        public IActionResult Edit(DishDTO entity)
        {
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

                Response.Redirect("/");
            }

            catch (ValidationException e)
            {
                if (e.ValidationResult.MemberNames.Contains("name"))
                {
                    ModelState.AddModelError("Name", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = true, Dish = entity});
                }
                else if (e.ValidationResult.MemberNames.Contains("createDate"))
                {
                    ModelState.AddModelError("CreateDate", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = true, Dish = entity});
                }

            };
            return Redirect("/");
        }
        
           

        [HttpPost]
        public IActionResult Create(DishDTO entity)
        {
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
                entity.CreateDate = DateTime.Now;
            }
            try
            {

                _menu.Create(entity);
                Response.Redirect("/");
            }

            catch (ValidationException e)
            {
                if (e.ValidationResult.MemberNames.Contains("name"))
                {
                    ModelState.AddModelError("Name", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateViewModel{IsEdit = false, Dish = entity});
                }
                else if (e.ValidationResult.MemberNames.Contains("createDate"))
                {
                    ModelState.AddModelError("CreateDate", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateViewModel { IsEdit = false, Dish = entity});
                }

            };
            return Content(" ");
        }

        [HttpPost]
        public int Delete(int id)
        {
            _menu.Delete(id);
            return _menu.GetAll().Count();
        }





    }
}
