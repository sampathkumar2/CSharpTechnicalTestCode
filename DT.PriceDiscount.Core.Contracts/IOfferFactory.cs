using System.Collections.Generic;

namespace DT.PriceDiscount.Core.Contracts
{
    public interface IOfferFactory
    {
        public IList<IOffer> GetOffers();
    }
}