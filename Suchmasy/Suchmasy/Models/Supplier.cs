﻿namespace Suchmasy.Models
{
    public class Supplier
    {
        public string Id { get; set; }
        public string BrandName { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public decimal UnitPrice { get; set; }
        public bool Enabled { get; set; }
        public int PaymentTerms { get; set; }
    }
}
