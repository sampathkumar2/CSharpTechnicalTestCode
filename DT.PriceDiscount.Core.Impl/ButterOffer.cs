using System;
using System.Collections.Generic;
using System.Linq;
using DT.PriceDiscount.Core.Contracts;

namespace DT.PriceDiscount.Core.Impl
{
    public class ButterOffer : IOffer
    {
        public List<IProduct> GetOfferDiscountAppliedProducts(List<IProduct> products)
        {
            List<IProduct> outputList = new List<IProduct>();
            foreach (var product in products)
                outputList.Add(new Product(product.Name, product.Quantity, product.Price, product.Discount));

            var butter = outputList.FirstOrDefault(_ => _.Name == ProductName.Butter);
            var bread =  outputList.FirstOrDefault(_ => _.Name == ProductName.Bread);

            if (butter == null || bread == null)
                return outputList;
            if (butter.Quantity < 2 || bread.Quantity == 0)
                return outputList;

            int butterCoupleCount = butter.Quantity / 2;

            if (bread.Quantity >= butterCoupleCount)
                bread.Discount = (bread.Price / 2) * butterCoupleCount;
            else
                bread.Discount = (bread.Price / 2) * bread.Quantity;

            return outputList;
        }
    }
}