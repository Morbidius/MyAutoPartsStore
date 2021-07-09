namespace MyAutoPartsStore.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using MyAutoPartsStore.Data.Models;

    public class MyAutoPartsStoreDbContext : IdentityDbContext
    {
        public MyAutoPartsStoreDbContext(DbContextOptions<MyAutoPartsStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
