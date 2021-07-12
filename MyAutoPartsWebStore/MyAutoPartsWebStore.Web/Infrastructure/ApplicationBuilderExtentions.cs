namespace MyAutoPartsWebStore.Web.Infrastructure
{
    using MyAutoPartsStore.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using MyAutoPartsStore.Data.Models;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scoppedServices = app.ApplicationServices.CreateScope();

            var data = scoppedServices.ServiceProvider.GetService<MyAutoPartsStoreDbContext>();
           
            data.Database.Migrate();

            CreateCategories(data);

            return app;
        }

        private static void CreateCategories(MyAutoPartsStoreDbContext data)
        {
            if (data.Categories.Any())
            {
                return;
            }

            data.Categories.AddRange(new[]
            {
                new Category {Name = "Lubricants"},
                new Category {Name = "Filters"},
                new Category {Name = "Antifreezes"},
                new Category {Name = "Batteries"},
                new Category {Name = "Tuning"},
                new Category {Name = "Tires"},
            });

            data.SaveChanges();
        }
    }
}
