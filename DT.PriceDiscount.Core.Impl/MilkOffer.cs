using System;
using System.Collections.Generic;
using System.Linq;
using DT.PriceDiscount.Core.Contracts;

namespace DT.PriceDiscount.Core.Impl
{
    public class MilkOffer : IOffer
    {
        public List<IProduct> GetOfferDiscountAppliedProducts(List<IProduct> products)
        {
            List<IProduct> outputList = new List<IProduct>();
            foreach (var product in products)
                outputList.Add(new Product(product.Name, product.Quantity, product.Price, product.Discount));

            var milk = outputList.FirstOrDefault(_ => _.Name == ProductName.Milk);

            if (milk == null || milk.Quantity < 4)
                return outputList;

            int milkTripletCount = milk.Quantity / 3;

            milk.Discount = milkTripletCount * milk.Price;

            return outputList;

        }
    }
}