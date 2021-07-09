using System.Collections.Generic;
using System.Linq;
using DT.PriceDiscount.Core.Contracts;

namespace DT.PriceDiscount.Core.Impl
{
    public class Basket : IBasket
    {
        #region Fields

        private IList<IProduct> _products = new List<IProduct>();
        private readonly IOfferFactory _offerFactory;

        #endregion

        #region Constructor

        public Basket(IOfferFactory offerFactory)
        {
            _offerFactory = offerFactory;
        }


        #endregion

        #region Public Methods

        //public void Add(IProduct product)
        //{
        //    _products.Add(product); //in real implementation definitely would have more checks
        //}

        public decimal GetDiscountedTotal(List<IProduct> products)
        {
            if (products == null || !products.Any())
            {
                //log
                return 0;
            }

            //using factory pattern to get various offers
            //so that it is extensible (in case many more offers come in the future)

            var offers = _offerFactory.GetOffers();

            if (offers == null || !offers.Any())
                return products.Sum(_ => (_.Quantity * _.Price));

            foreach (var offer in offers)
                products = offer.GetOfferDiscountAppliedProducts(products);

            return products.Sum(_ => (_.Quantity * _.Price) - _.Discount);
        }

        #endregion

    }
}