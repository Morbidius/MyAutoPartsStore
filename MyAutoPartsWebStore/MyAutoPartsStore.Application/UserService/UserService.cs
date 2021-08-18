namespace MyAutoPartsStore.Services.UserService
{
    using MyAutoPartsStore.Data;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly MyAutoPartsStoreDbContext data;

        public UserService(MyAutoPartsStoreDbContext data)
        {
            this.data = data;
        }

        public string GetUserEmailById(string userId)
            => data.Users.FirstOrDefault(x => x.Id == userId).Email;
    }
}
