using System.Collections.Generic;

namespace DT.PriceDiscount.Core.Contracts
{
    public interface IBasket
    {
        //public void Add(IProduct product);
        public decimal GetDiscountedTotal(List<IProduct> products);
    }
}