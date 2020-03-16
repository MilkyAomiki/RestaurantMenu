using System.Collections.Generic;

namespace RestaurantMenu.BLL.Interfaces
{
    /// <summary>
    /// Tool for work with DAL layer
    /// </summary>
    public interface IMenu<T>
    {
        void Create(T item);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T item);
        void Delete(int id);
    }
}
