using Restaurant_menu.Context;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantMenu.BLL.Services
{
    public class MenuService : IMenu<Dish>
    {
        private readonly DishesContext _context;
        public MenuService(DishesContext context)
        {
            _context = context;
        }

        #region Create
        public void Create(Dish item)
        {
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
            var entity = DishMap.GetDish(item);
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
