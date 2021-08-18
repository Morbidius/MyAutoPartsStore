namespace MyAutoPartsStore.Tests.Data
{
    using MyAutoPartsStore.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class Products
    {
        private static IEnumerable<Product> FivePublicProducts(int id)
            => Enumerable.Range(0, 6).Select(i => new Product
            {
                IsAllowed = true,
            });
    }
}
