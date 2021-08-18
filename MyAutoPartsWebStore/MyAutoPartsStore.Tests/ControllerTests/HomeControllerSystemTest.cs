namespace MyAutoPartsStore.Tests.ControllerTests
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using MyAutoPartsStore.Data.Models;
    using MyAutoPartsWebStore.Web;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class HomeControllerSystemTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public HomeControllerSystemTest(WebApplicationFactory<Startup> factory)
            => this.factory = factory;

        [Fact]
        public async Task IndexShouldReturnCorrectStatusCode()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var result = await client.GetAsync("/");

            // Arrange
            Assert.True(result.IsSuccessStatusCode);
        }
        
        private static IEnumerable<Category> GetCategories(int id)
            => Enumerable.Range(0, 6).Select(i => new Category
            {
                Id = id,
            });
    }
}
