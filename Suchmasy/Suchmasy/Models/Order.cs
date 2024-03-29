﻿namespace Suchmasy.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string RequestId { get; set; }
        public Request Request { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public string BuyerId { get; set; }
        public string BuyerEmail { get; set; }

        public DateTime PlacedOn { get; set; }

    }
}
