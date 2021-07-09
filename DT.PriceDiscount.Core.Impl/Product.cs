using DT.PriceDiscount.Core.Contracts;

namespace DT.PriceDiscount.Core.Impl
{
    public class Product : IProduct
    {
        #region Properties

        public ProductName Name { get; }
        public int Quantity { get; set; }
        public decimal Price { get; }
        public decimal Discount { get; set; }

        #endregion

        #region Constructor

        public Product(ProductName name, int quantity, decimal price, decimal discount)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Discount = discount;
        }

        #endregion

    }
}