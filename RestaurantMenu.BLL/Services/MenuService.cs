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
using Restaurant_menu.Models;

namespace RestaurantMenu.BLL.Services
{
    /// <summary>
    /// <inheritdoc cref="IMenu{T}"/>
    /// </summary>
    public class MenuService : IMenu<DishDTO>
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
        public void Create(DishDTO item)
        {
            Validator validator = new Validator();
            validator.ValidateName(item, _context);

            var entity = DishMap.GetDish(item);
            _context.Dish.Add(entity);
            _context.SaveChanges();
        }
        #endregion

        #region Read
        public DishDTO Get(int id)
        {
            var entity = _context.Dish.Find((short)id);
            var entityDTO = DishMap.GetDto(entity);
            return entityDTO;
        }
        /// <summary>
        /// Filtred all dishes and return them
        /// </summary>
        /// <param name="constraints">Filters</param>
        /// <param name="fieldForSort">Type of field to sort </param>
        /// <returns></returns>
        public IEnumerable<DishDTO> GetAll(List<ItemConstraint> constraints, FieldTypes fieldForSort, int pageNum, int pageSize)
        {
            #region Sort

            var sorted = fieldForSort switch
            {
                FieldTypes.Name => _context.Dish.OrderBy(p => p.Name).ToList(),
                FieldTypes.CreateDate => _context.Dish.OrderBy(p => p.CreateDate).ToList(),
                FieldTypes.Consistence => _context.Dish.OrderBy(p => p.Consist).ToList(),
                FieldTypes.Description => _context.Dish.OrderBy(p => p.Description).ToList(),
                FieldTypes.Price => _context.Dish.OrderBy(p => p.Price).ToList(),
                FieldTypes.Gram => _context.Dish.OrderBy(p => p.Gram).ToList(),
                FieldTypes.Calorific => _context.Dish.OrderBy(p => p.Calorific).ToList(),
                FieldTypes.CookTime => _context.Dish.OrderBy(p => p.CookTime).ToList(),
                FieldTypes.None => _context.Dish.ToList(),
                _ => _context.Dish.ToList()
            };
            _context.SaveChangesAsync();
            #endregion

            #region Filter

            List<Dish> approved = new List<Dish>();
            foreach (var filter in constraints)
            {
                var filtred = filter.Key switch
                {
                    FieldTypes.Name => _context.Dish.Where(f => f.Name.ToLower().Contains(filter.Value.ToLower())).ToList(),
                    FieldTypes.CreateDate => _context.Dish.Where(f => f.CreateDate.ToString().ToLower().Contains(filter.Value.ToLower())).ToList(),
                    FieldTypes.Consistence => _context.Dish.Where(f => f.Consist.ToLower().Contains(filter.Value.ToLower())).ToList(),
                    FieldTypes.Description => _context.Dish.Where(f => f.Description.ToLower().Contains(filter.Value.ToLower())).ToList(),
                    FieldTypes.Price => _context.Dish.Where(f => f.Price.ToString().ToLower().Contains(filter.Value.ToLower())).ToList(),
                    FieldTypes.Gram => _context.Dish.Where(f => f.Gram.ToString().ToLower().Contains(filter.Value.ToLower())).ToList(),
                    FieldTypes.Calorific => _context.Dish.Where(f => f.Calorific.ToString().ToLower().Contains(filter.Value.ToLower())).ToList(),
                    FieldTypes.CookTime => _context.Dish.Where(f => f.CookTime.ToString().ToLower().Contains(filter.Value.ToLower())).ToList(),
                    FieldTypes.None => _context.Dish.ToList(),
                    _ => _context.Dish.ToList()
                };

                if (approved.Count() == 0)
                {
                    approved = filtred;
                }
                else
                {
                    approved = approved.Intersect(filtred).ToList();
                }
            }
            #endregion

            #region Paging
            var page  = approved.Skip(pageNum - 1 * pageSize).Take(pageSize);
            #endregion

            return DishMap.GetDishes(page);
        }

    public IEnumerable<DishDTO> GetAll()
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
    public void Update(DishDTO item)
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

    /// <summary>
    /// Provides sorting and filtration functions
    /// </summary>

}
