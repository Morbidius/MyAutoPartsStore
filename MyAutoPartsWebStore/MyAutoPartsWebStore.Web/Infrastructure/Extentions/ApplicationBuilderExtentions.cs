namespace MyAutoPartsWebStore.Web.Infrastructure.Extentions
{
    using MyAutoPartsStore.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using MyAutoPartsStore.Data.Models;
    using System;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    using static MyAutoPartsWebStore.Web.Areas.Admin.AdminConstants;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scoppedServices = app.ApplicationServices.CreateScope();
            var services = scoppedServices.ServiceProvider;

            var data = scoppedServices.ServiceProvider.GetRequiredService<MyAutoPartsStoreDbContext>();

            MigrateDatabase(services);

            CreateCategories(services);
            CreateAdministrator(services);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyAutoPartsStoreDbContext>();

            data.Database.Migrate();
        }

        private static void CreateCategories(IServiceProvider services)
        {
            var data = services.GetRequiredService<MyAutoPartsStoreDbContext>();

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

        private static void CreateAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
               .Run(async () =>
               {
                   if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                   {
                       return;
                   }

                   var role = new IdentityRole { Name = AdministratorRoleName };

                   await roleManager.CreateAsync(role);

                   const string adminEmail = "pesho@mapi.com";
                   const string adminPassword = "root123";

                   var user = new User
                   {
                       Email = adminEmail,
                       UserName = adminEmail,
                       FullName = "Admin"
                   };

                   await userManager.CreateAsync(user, adminPassword);

                   await userManager.AddToRoleAsync(user, role.Name);
               })
               .GetAwaiter()
               .GetResult();
        }
    }
}
