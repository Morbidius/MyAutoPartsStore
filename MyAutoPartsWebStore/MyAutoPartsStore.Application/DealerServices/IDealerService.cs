namespace MyAutoPartsStore.Services.DealersServices
{
    public interface IDealerService
    {
        int Become(
            string Name,
            string CompanyName,
            string PhoneNumber,
            string UserId);

        public bool IsDealer(string userId);

        public int GetDealerById(string userId);
    }
}
