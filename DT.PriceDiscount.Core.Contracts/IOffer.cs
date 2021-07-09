using System.Collections.Generic;

namespace DT.PriceDiscount.Core.Contracts
{
    public interface IOffer
    {
        public List<IProduct> GetOfferDiscountAppliedProducts(List<IProduct> products);
    }
}