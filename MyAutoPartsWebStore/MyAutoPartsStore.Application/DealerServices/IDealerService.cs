namespace MyAutoPartsStore.Services.DealersServices
{
    public interface IDealerService
    {
        public bool IsDealer(string userId);

        public int GetDealerById(string userId);
    }
}
