using Restaurant_menu.Context;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Mapper;
using System;
using System.Collections.Generic;
using RestaurantMenu.BLL.Validation;
using System.Linq;
using System.Globalization;
using System.Threading;

namespace RestaurantMenu.BLL.Services
{
    public class MenuService : IMenu<Dish>
    {
        private readonly DishesContext _context;
        public MenuService(DishesContext context)
        {
            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.NumberGroupSeparator = ".";
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            _context = context;
        }

        #region Create
        public void Create(Dish item)
        {
            Validator validator = new Validator();
            validator.ValidateName(item, _context);

            var entity = DishMap.GetDish(item);
            _context.Dish.Add(entity);
            _context.SaveChanges();
        }
        #endregion

        #region Read
        public Dish Get(int id)
        {
            var entity = _context.Dish.Find((short)id);
            var entityDTO = DishMap.GetDto(entity);
            return entityDTO;
        }

        public IEnumerable<Dish> GetAll()
        {
            var items = _context.Dish;
            return DishMap.GetDishes(items);
        }

        public IEnumerable<Restaurant_menu.Models.Dish> Find(Func<Restaurant_menu.Models.Dish, Boolean> predicate)
        {
            return _context.Dish.Where(predicate).ToList();
        }
        #endregion

        #region Update
        public void Update(Dish item)
        {
            Validator validator = new Validator();
            validator.IsExist(item, _context);
            validator.ValidateCreationDate(item, _context);
            validator.ValidateName(item, _context);

            var entity = DishMap.GetDish(item);
            _context.Dish.Find(item.Id).Calorific = item.Calorific;
            _context.Dish.Find(item.Id).Consist = item.Consist;
            _context.Dish.Find(item.Id).Name = item.Name;
            _context.Dish.Find(item.Id).Price = item.Price;
            _context.Dish.Find(item.Id).Gram = item.Gram;
            _context.Dish.Find(item.Id).Description = item.Description;
            _context.SaveChanges();
        }
        #endregion

        #region Delete
        public void Delete(int id)
        {
            var dish = _context.Dish.Find((short)id);
            _context.Dish.Remove(dish);
            _context.SaveChanges();
        }
        #endregion
    }
}
