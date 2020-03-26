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

        public int GetTotalAmount()
        {
            return _context.Dish.Count();
        }
        public int GetPageAmount(int currentPage, int pageSize)
        {
            return _context.Dish.Skip((currentPage - 1) * pageSize).Take(pageSize).Count();
        }

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


            //for (int i = 0; i < customList.Count(); i++)
            //{
                
            //    var sourceItem = customList.ElementAt(i).Calorific;
            //    var customItem = Decimal.Multiply(sourceItem, Decimal.Divide(Convert.ToDecimal(customList.ElementAt(i).Gram), Convert.ToDecimal(100)));
            //    customList.ElementAt(i).Calorific = Convert.ToDecimal(decimal.Parse(customItem.ToString()).ToString("G29"));
            //}


            //void m()
            //{
            //    IQueryable<Dish> query;

            //    switch (order.Field)
            //    {
            //        case "Name":
            //            query = cntx.Dish.OrderBy(x => x.Name);
            //            break;
            //        default:
            //            query = cntx.Dish.OrderBy(x => x.Id);
            //            break;
            //    }

            //    if (filters.Count > 0)
            //    {
            //        foreach (var filter in filters)
            //        {
            //            switch (filter.Name)
            //            {
            //                case "Name":
            //                    query = query.Where(x => x.Name = filter.Value);
            //                    break;
            //            }
            //        }

            //    }

            //    query = query.Skip(20).Take(20).ToList();
            //}


            #region Filter

            IQueryable<Dish> approved;
            if (!constraints.Any())
            {
                approved = _context.Dish;
            }
            else if (!constraints.Where(p=>p.Value.Any()).Any())
            {
                approved = _context.Dish;
            }
            else
            {
                approved = Enumerable.Empty<Dish>().AsQueryable();
                foreach (var filter in constraints)
                {


                    var filtred = filter.Key switch
                    {
                        FieldTypes.Name => _context.Dish.Where(f => f.Name.ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.CreateDate => _context.Dish.Where(f => f.CreateDate.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Consistence => _context.Dish.Where(f => f.Consist.ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Description => _context.Dish.Where(f => f.Description.ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Price => _context.Dish.Where(f => f.Price.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Gram => _context.Dish.Where(f => f.Gram.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.Calorific => _context.Dish.Where(f => f.TotalCalorific.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.CookTime => _context.Dish.Where(f => f.CookTime.ToString().ToLower().Contains(filter.Value.ToLower())),
                        FieldTypes.None => _context.Dish,
                        _ => _context.Dish
                    };


                    if (!approved.Any())
                    {
                        approved = filtred;
                    }
                    else
                    {
                        //Why
                        approved = approved.Intersect(filtred);
                        if (!approved.Any())
                        {
                            break;
                        }
                    }
                }
            }


            #endregion


            #region Sort

            //438 ms
            var sorted = fieldForSort switch
            {
                FieldTypes.Name => isDecs ? approved.OrderByDescending(p => p.Name) : approved.OrderBy(p => p.Name),
                FieldTypes.CreateDate => isDecs ? approved.OrderByDescending(p => p.CreateDate) : approved.OrderBy(p => p.CreateDate),
                FieldTypes.Consistence => isDecs ? approved.OrderByDescending(p => p.Consist) : approved.OrderBy(p => p.Consist),
                FieldTypes.Description => isDecs ? approved.OrderByDescending(p => p.Description) : approved.OrderBy(p => p.Description),
                FieldTypes.Price => isDecs ? approved.OrderByDescending(p => p.Price) : approved.OrderBy(p => p.Price),
                FieldTypes.Gram => isDecs ? approved.OrderByDescending(p => p.Gram) : approved.OrderBy(p => p.Gram),
                FieldTypes.Calorific => isDecs ? approved.OrderByDescending(p => p.TotalCalorific) : approved.OrderBy(p => p.TotalCalorific),
                FieldTypes.CookTime => isDecs ? approved.OrderByDescending(p => p.CookTime) : approved.OrderBy(p => p.CookTime),
                FieldTypes.None => approved.OrderBy(p => p.Id),
                _ => approved.OrderBy(p => p.Id)
            };
            
            #endregion

            #region Paging
            
            var newPagesCount = (int)Math.Ceiling((Double)sorted.Count() / pageSize);
            var page = pageNum <= newPagesCount ? sorted.Skip((pageNum - 1) * pageSize).Take(pageSize) : sorted.Take(pageSize);

            #endregion

            var model = new MenuModel
            {
                Dishes = DishMap.GetDishes(page),
                Count = sorted.Count(),
                TotalCount = _context.Dish.Count()
            };
            return model;
        }

        public IEnumerable<DishDTO> GetAll()
        {
            return DishMap.GetDishes(_context.Dish);
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


}
