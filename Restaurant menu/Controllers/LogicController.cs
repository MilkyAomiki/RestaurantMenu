using Microsoft.AspNetCore.Mvc;
using Restaurant_menu.Model;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Restaurant_menu.Controllers
{
    public class LogicController : Controller
    {
        private IMenu<Dish> _menu;

        public LogicController(IMenu<Dish> menu)
        {
            _menu = menu;
        }


        [HttpPost]
        public IActionResult Edit(Dish entity)
        {
            //if (_menu.Get(entity.Id).Name == entity.Name )
            //{
            //    ModelState.AddModelError("Name", "Name is already exist");
            //    return View("/Views/Main/Create.cshtml", entity);
            //}
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
                    return View("/Views/Main/Create.cshtml", new CreateModelView { IsEdit = true, Dish = entity});
                }
                else if (e.ValidationResult.MemberNames.Contains("createDate"))
                {
                    ModelState.AddModelError("CreateDate", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateModelView { IsEdit = true, Dish = entity});
                }

            };
            return Redirect("/");
        }
        
           

        [HttpPost]
        public IActionResult Create(Dish entity)
        {
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
                    return View("/Views/Main/Create.cshtml", new CreateModelView{IsEdit = false, Dish = entity});
                }
                else if (e.ValidationResult.MemberNames.Contains("createDate"))
                {
                    ModelState.AddModelError("CreateDate", e.ValidationResult.ErrorMessage);
                    return View("/Views/Main/Create.cshtml", new CreateModelView { IsEdit = false, Dish = entity});
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
