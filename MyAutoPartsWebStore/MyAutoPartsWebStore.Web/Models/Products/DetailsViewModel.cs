namespace MyAutoPartsWebStore.Web.Models.Products
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public string SizeCapacity { get; init; }

        public float Weight { get; init; }

        public string ImageUrl { get; init; }
    }
}
