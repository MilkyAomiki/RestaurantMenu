using Microsoft.EntityFrameworkCore;
using Restaurant_menu.Models;

namespace Restaurant_menu.Context
{
	public class DishesContext : DbContext
	{
		public virtual DbSet<Dish> Dish { get; set; }
		private readonly string connection;

		public DishesContext(string connection)
		{
			this.connection = connection;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connection);
		}
	}
} 
