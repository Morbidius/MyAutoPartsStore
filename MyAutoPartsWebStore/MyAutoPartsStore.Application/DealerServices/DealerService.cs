namespace MyAutoPartsStore.Services.DealersServices
{
    using MyAutoPartsStore.Data;
    using System.Linq;

    public class DealerService : IDealerService
    {
        private readonly MyAutoPartsStoreDbContext data;

        public DealerService(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public int GetDealerById(string userId)
            => this.data
                   .Dealers
                   .Where(d => d.UserId == userId)
                   .Select(d => d.Id)
                   .FirstOrDefault();

        public bool IsDealer(string userId)
            => this.data
                   .Dealers
                   .Any(d => d.UserId == userId);
    }
}
