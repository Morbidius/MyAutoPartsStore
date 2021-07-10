using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyAutoPartsWebStore.Web.Data;

[assembly: HostingStartup(typeof(MyAutoPartsWebStore.Web.Areas.Identity.IdentityHostingStartup))]
namespace MyAutoPartsWebStore.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MyAutoPartsWebStoreWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("MyAutoPartsWebStoreWebContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<MyAutoPartsWebStoreWebContext>();
            });
        }
    }
}