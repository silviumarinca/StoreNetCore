using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices
                                    .CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();
            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if(!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product{
                        Name="Kayak",
                        Description="A boat for one person",
                        Category="WaterSports",
                        Price=275
                    },
                    new Product{
                        Name="LifeJacket",
                        Description="Protective and fashionable",
                        Category="WaterSports",
                        Price=48.95M
                    },
                    new Product{
                        Name = "Soccer Ball",
                        Description="FiFa-aproved size and weight",
                        Category="Soccer",
                        Price=19.50m
                    },
                    new Product{
                        Name="Corner Flags",
                        Description="Give your playing field a profssional touch",
                        Category="Soccer",
                        Price=34.95M
                    },
                    new Product{
                        Name="Stadium",
                        Description="Flat-packed 35,000-seat stadium",
                        Category="Soccer",
                        Price=79500
                    },
                    new Product{
                        Name="Thinking Cap",
                        Description="Improve brain efficiency by 75%",
                        Category="Chess",
                        Price= 16
                    },
                    new Product{
                        Name="Unsteady chair",
                        Description="Secretly give your opponent a disadvantage",
                        Category = "Chess",
                        Price =75
                    },
                    new Product{
                        Name="Bling Bing King",
                        Description="Gold-Plated, Diamond-studded King",
                        Category="Chess",
                        Price=1200
                    }

                );
                context.SaveChanges();
            }


        }
        
    }
}