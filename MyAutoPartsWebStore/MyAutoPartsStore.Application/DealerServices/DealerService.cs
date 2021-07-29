namespace MyAutoPartsStore.Services.DealersServices
{
    using MyAutoPartsStore.Data;
    using System;
    using System.Linq;

    public class DealerService : IDealerService
    {
        private readonly MyAutoPartsStoreDbContext data;

        public DealerService(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public bool IsDealer(string userId)
            => this.data
                .Dealers
                .Any(d => d.UserId == userId);
    }
}
