namespace MyAutoPartsStore.Data
{
    public class DataConstants
    {
        public class Product 
        {
            public const int ProductNameMinLength = 1;
            public const int ProductNameMaxLength = 30;

            public const int ProductDescriptionMinLength = 10;

            public const decimal ProductMinPrice = 0.1m;
            public const decimal ProductMaxPrice = 1000000m;

            public const int ProductSizeCapacityMinLength = 1;
            public const int ProductSizeCapacityMaxLength = 100;

            public const float ProductMinWeight = 0.01f;
            public const float ProductMaxWeight = 1000f;
        }
    }
}
