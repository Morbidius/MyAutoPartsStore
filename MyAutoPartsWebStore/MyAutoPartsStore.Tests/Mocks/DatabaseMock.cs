namespace MyAutoPartsStore.Tests.Mocks
{
    using Microsoft.EntityFrameworkCore;
    using MyAutoPartsStore.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class DatabaseMock
    {
        public static MyAutoPartsStoreDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<MyAutoPartsStoreDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new MyAutoPartsStoreDbContext(dbContextOptions);
            }
        }
    }
}
