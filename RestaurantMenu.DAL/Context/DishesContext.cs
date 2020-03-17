using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Restaurant_menu.Models;
using System.Diagnostics.CodeAnalysis;

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

		public DishesContext([NotNullAttribute] DbContextOptions options) : base(options)
		{
			var extension = options.FindExtension<SqlServerOptionsExtension>();
			connection = extension.ConnectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connection);
		}
	}
}
