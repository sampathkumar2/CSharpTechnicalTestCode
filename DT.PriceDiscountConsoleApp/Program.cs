using System;
using System.Collections.Generic;
using DT.PriceDiscount.Core.Contracts;
using DT.PriceDiscount.Core.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace DT.PriceDiscountConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup the DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IOfferFactory, OfferFactory>()
                .AddSingleton<IBasket, Basket>()
                .BuildServiceProvider();

            //Our entry point is the IBasket interface
            //where we add products and get the sum discounted total
            //assumptions: no more than 3 relevant products
            //(in real world these would be validated and received from UI etc)
            var butter = new Product(ProductName.Butter, 0, 0.8m, 0);
            var milk = new Product(ProductName.Milk, 0, 1.15m, 0);
            var bread = new Product(ProductName.Bread, 0, 1m, 0);

            //Scenarios as described in the test requirement

            #region Scenario I

            // Scenario I (Given the basket has 1 bread, 1 butter and 1 milk when I total the basket then the total should be
            //£2.95 )
            bread.Quantity = 1;
            butter.Quantity = 1;
            milk.Quantity = 1;

            var basket = serviceProvider.GetService<IBasket>();

            var discountTotal1 = basket.GetDiscountedTotal(new List<IProduct> { bread, butter, milk });

            if (discountTotal1 == 2.95m)
                Console.WriteLine($"Scenario I discount total as expected {discountTotal1}");
            else
                Console.WriteLine($"Scenario I discount total not as expected {discountTotal1}");

            #endregion

            #region Scenario II

            // Scenario II (Given the basket has 2 butter and 2 bread when I
            // total the basket then the total should be £3.10 )
            bread.Quantity = 2;
            butter.Quantity = 2;
            //milk.Quantity = 1;


            var discountTotal2 = basket.GetDiscountedTotal(new List<IProduct> { bread, butter });

            if (discountTotal2 == 3.10m)
                Console.WriteLine($"Scenario II discount total as expected {discountTotal2}");
            else
                Console.WriteLine($"Scenario II discount total not as expected {discountTotal2}");

            #endregion

            #region Scenario III

            // Scenario III (Given the basket has 4 milk
            // when I total the basket then the total should be £3.45)
            //bread.Quantity = 2;
            //butter.Quantity = 2;
            milk.Quantity = 4;


            var discountTotal3 = basket.GetDiscountedTotal(new List<IProduct> { milk });

            if (discountTotal3 == 3.45m)
                Console.WriteLine($"Scenario III discount total as expected {discountTotal3}");
            else
                Console.WriteLine($"Scenario III discount total not as expected {discountTotal3}");

            #endregion

            #region Scenario IV

            // Scenario IV (Given the basket has 2 butter, 1 bread and 8 milk
            // when I total the basket then the total should be £9.00)
            bread.Quantity = 1;
            butter.Quantity = 2;
            milk.Quantity = 8;


            var discountTotal4 = basket.GetDiscountedTotal(new List<IProduct> { bread, butter, milk });

            if (discountTotal4 == 9m)
                Console.WriteLine($"Scenario IV discount total as expected {discountTotal4}");
            else
                Console.WriteLine($"Scenario IV discount total not as expected {discountTotal4}");

            #endregion

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
