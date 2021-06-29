using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SwimmingStore.Models;
using SwimmingStore.Models.Repository;

namespace SwimmingStore
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        private IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:SwimmingStoreConnection"]);
            });

            services.AddScoped<IStoreRepository, EFStoreRepository>();

            services.AddScoped<IOrderRepository, EFOrderRepository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache();

            services.AddSession();

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute("catpage",
                "{category}/Page{productPage:int}",
                new { Controller = "Home", action = "Index" });

            endpoints.MapControllerRoute("page", "Page{productPage:int}",
                new { Controller = "Home", action = "Index", productPage = 1 });

            endpoints.MapControllerRoute("category", "{category}",
                new { Controller = "Home", action = "Index", productPage = 1 });

            endpoints.MapControllerRoute("pagination",
                "Products/Page{productPage}",
                new { Controller = "Home", action = "Index", productPage = 1 });

            endpoints.MapDefaultControllerRoute();

            endpoints.MapRazorPages();
        });

            SeedData.EnsurePopulated(app);
        }
}
}
