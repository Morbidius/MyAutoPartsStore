﻿namespace MyAutoPartsStore.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; init; }

        public string Name { get; set; }

        public string IconHtml { get; set; }

        public IEnumerable<Product> Products { get; init; } = new HashSet<Product>();
    }
}
