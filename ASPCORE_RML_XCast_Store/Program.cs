using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.User;
using RMLXCast.Database;
using RMLXCast.Services.Catalog;
using RMLXCast.Web.ViewModelsFactories.ProductFactory;
using RMLXCast.Web.ViewModelsFactories.UserFactory;

namespace ASPCORE_RML_XCast_Store
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString =  builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException("No connection string is provided!");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
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

            // -----CUSTOM SERVICES-----


            var app = builder.Build();

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