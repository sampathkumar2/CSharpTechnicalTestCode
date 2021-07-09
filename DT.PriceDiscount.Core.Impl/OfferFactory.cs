using System.Collections.Generic;
using System.Linq;
using DT.PriceDiscount.Core.Contracts;

namespace DT.PriceDiscount.Core.Impl
{
    public class OfferFactory : IOfferFactory
    {
        #region Fields

        private IList<IOffer> _offers = new List<IOffer>();

        #endregion

        #region Constructor

        public OfferFactory()
        {
            SetupOffers();
        }

        #endregion

        #region Public Methods

        public IList<IOffer> GetOffers()
        {
            if (_offers == null || !_offers.Any())
            {
                SetupOffers();
            }

            return _offers;
        }

        #endregion

        #region Private Methods

        private void SetupOffers()
        {
            _offers.Add(new ButterOffer());
            _offers.Add(new MilkOffer());
        }

        #endregion
    }
}