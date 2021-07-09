using System;

namespace DT.PriceDiscount.Core.Contracts
{
    public interface IProduct
    {
        public ProductName Name { get; }
        public int Quantity { get; set; }
        public decimal Price { get; }
        public decimal Discount { get; set; }
    }
}
