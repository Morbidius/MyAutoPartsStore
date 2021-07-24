﻿namespace MyAutoPartsStore.Data
{
    public class DataConstants
    {
        public class Product 
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 30;

            public const int DescriptionMinLength = 10;

            public const decimal MinPrice = 0.1m;
            public const decimal MaxPrice = 1000000m;

            public const int SizeCapacityMinLength = 1;
            public const int SizeCapacityMaxLength = 100;

            public const float MinWeight = 0.01f;
            public const float MaxWeight = 1000f;
        }

        public class Category
        {
            public const int NameMaxLength = 35;
        }

        public class Dealer
        {
            public const int NameMaxLength = 30;

            public const int PhoneNumberMaxLength = 20;

            public const int CompanyNameMaxLength = 50;
        }
    }
}
