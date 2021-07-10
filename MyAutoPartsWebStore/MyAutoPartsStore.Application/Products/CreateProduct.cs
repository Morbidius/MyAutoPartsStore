namespace MyAutoPartsStore.Application.Products
{
    using MyAutoPartsStore.Data;
    using MyAutoPartsStore.Data.Models;

    public class CreateProduct
    {
        private MyAutoPartsStoreDbContext context;

        public CreateProduct(MyAutoPartsStoreDbContext context)
        {
            this.context = context;
        }

        public void ProductCreation(int id, string name, string descritpion, decimal price, int weight)
        {
            this.context.Products.Add(new Product
            {
                Id = id,
                Name =name,
                Description = descritpion,
                Price = price,
                Weight = weight,
            });
        }
    }
}
