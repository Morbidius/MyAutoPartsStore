using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyAutoPartsStore.Data;

[assembly: HostingStartup(typeof(MyAutoPartsWebStore.Web.Areas.Identity.IdentityHostingStartup))]
namespace MyAutoPartsWebStore.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MyAutoPartsStoreDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")));
            });
        }
    }
}