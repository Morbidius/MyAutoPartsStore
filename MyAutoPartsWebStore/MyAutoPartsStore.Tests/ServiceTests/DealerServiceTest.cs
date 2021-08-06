namespace MyAutoPartsStore.Tests.ServiceTests
{
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsStore.Services.DealersServices;
    using MyAutoPartsStore.Tests.Mocks;
    using Xunit;

    public class DealerServiceTest
    {
        private const string userId = "TestUserId";
        private const string falseUserId = "TestUserId2";

        [Fact]
        public void IsDealerReturningTrueWhenUserIsDealer()
        {
            //Arrange
            using var data = this.GetDealerData();

            var dealerService = new DealerService(data);

            //Act
            var result = dealerService.IsDealer(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDealerReturningFalseWhenUserIsDealer()
        {
            //Arrange
            using var data = DatabaseMock.Instance;


            data.Dealers.Add(new Dealer { UserId = userId });
            data.SaveChanges();

            var dealerService = new DealerService(data);

            //Act
            var result = dealerService.IsDealer(falseUserId);

            //Assert
            Assert.False(result);
        }

        private MyAutoPartsStoreDbContext GetDealerData()
        {
            var data = DatabaseMock.Instance;


            data.Dealers.Add(new Dealer { UserId = userId });
            data.SaveChanges();

            return data;
        }
    }
}
