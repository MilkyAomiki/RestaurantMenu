using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Restaurant_menu.Context;

namespace RestaurantMenu.DAL.Context
{
    class DishesContextFactory : IDesignTimeDbContextFactory<DishesContext>
    {
        public DishesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DishesContext>();
            optionsBuilder.UseSqlServer("Data Source=ACER-PC58\\SQLEXPRESS;Integrated Security=True; Database=Dishes; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            return new DishesContext(optionsBuilder.Options);



        }
    }
}
