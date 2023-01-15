using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.Role;
using RMLXCast.Core.Domain.User;
using RMLXCast.Database;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Catalog.Category;
using RMLXCast.Services.Catalog.Stocks;
using RMLXCast.Services.Catalog.User;
using RMLXCast.Web.Initializers;
using RMLXCast.Web.ViewModelsFactories.ProductFactory;
using RMLXCast.Web.ViewModelsFactories.RolesFactory;
using RMLXCast.Web.ViewModelsFactories.UserFactory;

namespace ASPCORE_RML_XCast_Store
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString =  builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException("No connection string is provided!");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services
                .AddIdentity<ApplicationUser, ApplicationUserRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add services to the container.
            builder.Services
                .AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            // -----CUSTOM SERVICES-----

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductViewModelFactory, ProductViewModelFactory>();
            builder.Services.AddScoped<IUserViewModelFactory, UserViewModelFactory>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<IProductStockService, ProductStockService>();
            builder.Services.AddScoped<IRolesViewModelFactory, RolesViewModelFactory>();
            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

            // -----CUSTOM SERVICES-----


            var app = builder.Build();

            // -----CUSTOM INITIALIZERS-----

            await UserInitializer.InitializeAsync(app.Services);
            
            // -----CUSTOM INITIALIZERS-----

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapFallbackToController("ErrorNotFound", "Home");

            app.Run();
        }
    }
}