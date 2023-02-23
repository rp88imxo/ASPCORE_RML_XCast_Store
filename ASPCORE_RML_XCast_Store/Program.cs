using MediatR;
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
using RMLXCast.Web.Services.Cart;
using RMLXCast.Web.Services.ProductImagesService;
using RMLXCast.Web.ViewModelsFactories.CartFactory;
using RMLXCast.Web.ViewModelsFactories.ProductCategoryFactory;
using RMLXCast.Web.ViewModelsFactories.ProductFactory;
using RMLXCast.Web.ViewModelsFactories.RolesFactory;
using RMLXCast.Web.ViewModelsFactories.ShopProducts;
using RMLXCast.Web.ViewModelsFactories.UserFactory;
using System.Reflection;

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

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "XCastSession";
                options.Cookie.MaxAge = TimeSpan.FromDays(7.0);
            });

            builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

            // -----CUSTOM SERVICES-----

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductViewModelFactory, ProductViewModelFactory>();
            builder.Services.AddScoped<IUserViewModelFactory, UserViewModelFactory>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<IProductStockService, ProductStockService>();
            builder.Services.AddScoped<IRolesViewModelFactory, RolesViewModelFactory>();
            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
            builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
            builder.Services.AddScoped<IProductCategoryFactory, ProductCategoryFactory>();
            builder.Services.AddScoped<IShopProductViewModelFactory, ShopProductViewModelFactory>();
            builder.Services.AddScoped<IProductImagesService, ProductImagesService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<ICartViewModelFactory, CartViewModelFactory>();

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
                pattern: "{controller=Shop}/{action=Products}");
            app.MapFallbackToController("ErrorNotFound", "Shop");

            app.UseSession();

            app.Run();
        }
    }
}