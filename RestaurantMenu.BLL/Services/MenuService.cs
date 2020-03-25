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
using Microsoft.EntityFrameworkCore;
using RestaurantMenu.BLL.Models;

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
            /// <param name="fieldForSort">Type of field to sort</param>
            /// <param name="pageNum">Current Page</param>
            /// <param name="pageSize"></param>
            /// <returns></returns>
            public MenuModel GetAll(List<ItemConstraint> constraints, FieldTypes fieldForSort, bool isDecs, int pageNum, int pageSize)
            {
                #region Sort

            
                var sorted = fieldForSort switch
                {
                    FieldTypes.Name => isDecs? _context.Dish.OrderByDescending(p=>p.Name): _context.Dish.OrderBy(p => p.Name),
                    FieldTypes.CreateDate => isDecs ? _context.Dish.OrderByDescending(p => p.CreateDate) : _context.Dish.OrderBy(p => p.CreateDate),
                    FieldTypes.Consistence => isDecs ? _context.Dish.OrderByDescending(p => p.Consist) : _context.Dish.OrderBy(p => p.Consist),
                    FieldTypes.Description => isDecs ? _context.Dish.OrderByDescending(p => p.Description) : _context.Dish.OrderBy(p => p.Description),
                    FieldTypes.Price => isDecs ? _context.Dish.OrderByDescending(p => p.Price) : _context.Dish.OrderBy(p => p.Price),
                    FieldTypes.Gram => isDecs ? _context.Dish.OrderByDescending(p => p.Gram) : _context.Dish.OrderBy(p => p.Gram),
                    FieldTypes.Calorific => isDecs ? _context.Dish.OrderByDescending(p => p.Calorific) : _context.Dish.OrderBy(p => p.Calorific),
                    FieldTypes.CookTime => isDecs ? _context.Dish.OrderByDescending(p => p.CookTime) : _context.Dish.OrderBy(p => p.CookTime),
                    FieldTypes.None => _context.Dish.OrderBy(p=>p.Id),
                    _ => _context.Dish.OrderBy(p => p.Id)
                };
           
                #endregion

                #region Filter

                List<Dish> approved = new List<Dish>();
                if (constraints.Count == 0)
                {
                    approved = _context.Dish.ToList();
                }
                foreach (var filter in constraints)
                {
                    var filtred = filter.Key switch
                    {
                        FieldTypes.Name => sorted.Where(f => f.Name.ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.CreateDate => sorted.Where(f => f.CreateDate.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Consistence => sorted.Where(f => f.Consist.ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Description => sorted.Where(f => f.Description.ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Price => sorted.Where(f => f.Price.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Gram => sorted.Where(f => f.Gram.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Calorific => sorted.Where(f => f.Calorific.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.CookTime => sorted.Where(f => f.CookTime.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.None => sorted,
                        _ => sorted
                    };

                    if (approved.Count() == 0)
                    {
                        approved = filtred.ToList();
                    }
                    else
                    {
                        approved = approved.Intersect(filtred).ToList();
                        if (approved.Count == 0)
                        {
                            break;
                        }
                    }
                }
                #endregion

                #region Paging

                var newPagesCount = (int)Math.Ceiling((Double)approved.Count() / pageSize);
                var page = pageNum <= newPagesCount ? approved.Skip((pageNum - 1) * pageSize).Take(pageSize) : approved.Take(pageSize);
                #endregion

                var model = new MenuModel
                {
                    Dishes = DishMap.GetDishes(page),
                    Count = approved.Count()
                };
                return model;
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
