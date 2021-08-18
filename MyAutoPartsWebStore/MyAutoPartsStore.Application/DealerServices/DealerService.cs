namespace MyAutoPartsStore.Services.DealersServices
{
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using System.Linq;

    public class DealerService : IDealerService
    {
        private readonly MyAutoPartsStoreDbContext data;

        public DealerService(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public int Become(string Name, string CompanyName, string PhoneNumber, string UserId)
        {
            var newDealer = new Dealer
            {
                Name = Name,
                CompanyName = CompanyName,
                PhoneNumber = PhoneNumber,
                UserId = UserId,
            };

            this.data.Dealers.Add(newDealer);
            this.data.SaveChanges();

            return newDealer.Id;
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
