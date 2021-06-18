using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwimmingStore.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Phelps Solid",
                        Description = "In bold solids, the Comp Brief offers a fitted brief style for comfortable speed and support.",
                        Category = "Swimwear",
                        Price = 630
                    },
                    new Product
                    {
                        Name = "Speedo Placement Digital V",
                        Description = "This jammer features a science fiction-inspired design on a black background and is ideal for your training sessions.",
                        Category = "Swimwear",
                        Price = 1050
                    },
                    new Product
                    {
                        Name = "Speedo Multi",
                        Description = "Stand out from the crowd with this vibrant multicolour patterned Pullbuoy.",
                        Category = "Training",
                        Price = 450
                    },
                    new Product
                    {
                        Name = "Arena Pullkick Pro",
                        Description = "Improve and control your body posture with arenas exclusive Pull Kick Pro.",
                        Category = "Training",
                        Price = 395
                    },
                    new Product
                    {
                        Name = "Arena Cobra Ultra Swipe Mirror Goggles",
                        Description = "The innovative arena unisex Goggles Cobra Ultra Swipe Mirror are perfect for competitive swimmers.",
                        Category = "Equipment",
                        Price = 1250
                    },
                    new Product
                    {
                        Name = "TYR Tracer x RZR Mirror Swimming Goggles",
                        Description = "Race like the pros in TYR Tracer-X RZR Mirrored Racing Adult Goggles.",
                        Category = "Equipment",
                        Price = 1500
                    },
                    new Product
                    {
                        Name = "Speedo Pace Swimming Cap",
                        Description = "Our swim caps are ideal for regular swimming and create a smooth, sleek outline in the water.",
                        Category = "Equipment",
                        Price = 250
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
