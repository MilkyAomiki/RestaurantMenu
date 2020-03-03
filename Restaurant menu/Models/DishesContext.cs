using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurant_menu.Models
{
	public partial class DishesContext : DbContext
	{

		public virtual DbSet<Dish> Dish { get; set; }

		public DishesContext(DbContextOptions<DishesContext> options)
			: base(options)
		{
			
		}
	}
}
