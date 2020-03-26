using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantMenu.BLL.DI;
using RestaurantMenu.BLL.DTO;
using RestaurantMenu.BLL.Interfaces;
using RestaurantMenu.BLL.Services;
using System.Globalization;
using System.Threading;

namespace Restaurant_menu
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }


		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			string connection = Configuration.GetConnectionString("DefaultConnection");
			var diModule = new DependencyModule(connection);
			services.AddScoped<IMenu<DishDTO>>(options => { return diModule.ConfigureMenuService(); });
			services.AddMvc(options => { options.EnableEndpointRouting = false;});
			services.AddTransient<ITools, ToolsService>();

		}


		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var cultureInfo = new CultureInfo("en-US");
			cultureInfo.NumberFormat.NumberGroupSeparator = ".";
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();
			app.UseMvc(route =>
			{
				route.MapRoute("default", "{controller=MainController}/{action=Index}/{id?}");
				route.MapRoute("create", "{controller=MainController}/{action=Create}/{id?}");
			});
		}
	}
}
