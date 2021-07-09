namespace MyAutoPartsStore.Data.Models
{
    public class Product
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Weight { get; set; }
    }
}
