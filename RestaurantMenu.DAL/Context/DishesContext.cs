using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Restaurant_menu.Models;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;

namespace Restaurant_menu.Context
{
	/// <summary>
	/// Database context
	/// <list type="bullet">
	/// Dish
	/// </list>
	/// </summary>
	public class DishesContext : DbContext
	{
		public virtual DbSet<Dish> Dish { get; set; }
		private readonly string connection;

		public DishesContext(string connection)
		{
			var cultureInfo = new CultureInfo("en-US");
			cultureInfo.NumberFormat.NumberGroupSeparator = ".";
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;

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
