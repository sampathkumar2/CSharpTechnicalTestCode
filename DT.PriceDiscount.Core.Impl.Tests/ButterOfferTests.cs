using System.Collections.Generic;
using System.Linq;
using DT.PriceDiscount.Core.Contracts;
using NUnit.Framework;

namespace DT.PriceDiscount.Core.Impl.Tests
{
    public class ButterOfferTests
    {
        [Test]
        public void Method_Should_Return_Products_Without_Change_If_Does_Not_Contain_Butter()
        {
            //Arrange
            var milk = new Product(ProductName.Milk, 2, 2.5m, 0);
            var bread = new Product(ProductName.Bread, 3, 5.5m, 2);

            ButterOffer objectUnderTest = new ButterOffer();

            //Act
            var result = objectUnderTest.GetOfferDiscountAppliedProducts(new List<IProduct>
            {
                milk, bread
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            var resultMilk = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Milk));
            Assert.IsNotNull(resultMilk);
            Assert.AreEqual(2, resultMilk.Quantity);
            Assert.AreEqual(2.5m, resultMilk.Price);
            Assert.AreEqual(0, resultMilk.Discount);

            var resultBread = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Bread));
            Assert.IsNotNull(resultBread);
            Assert.AreEqual(3, resultBread.Quantity);
            Assert.AreEqual(5.5m, resultBread.Price);
            Assert.AreEqual(2, resultBread.Discount);
        }

        [Test]
        public void Method_Should_Return_Products_Without_Change_If_Does_Not_Contain_Bread()
        {
            //Arrange
            var milk = new Product(ProductName.Milk, 2, 2.5m, 0);
            var butter = new Product(ProductName.Butter, 3, 5.5m, 2);

            ButterOffer objectUnderTest = new ButterOffer();

            //Act
            var result = objectUnderTest.GetOfferDiscountAppliedProducts(new List<IProduct>
            {
                milk, butter
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            var resultMilk = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Milk));
            Assert.IsNotNull(resultMilk);
            Assert.AreEqual(2, resultMilk.Quantity);
            Assert.AreEqual(2.5m, resultMilk.Price);
            Assert.AreEqual(0, resultMilk.Discount);

            var resultButter = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Butter));
            Assert.IsNotNull(resultButter);
            Assert.AreEqual(3, resultButter.Quantity);
            Assert.AreEqual(5.5m, resultButter.Price);
            Assert.AreEqual(2, resultButter.Discount);
        }

        [Test]
        public void Method_Should_Return_Products_Without_Change_If_Does_Not_Contain_Butter_And_Bread()
        {
            //Arrange
            var milk = new Product(ProductName.Milk,2,2.5m,0);

            ButterOffer objectUnderTest = new ButterOffer();

            //Act
            var result = objectUnderTest.GetOfferDiscountAppliedProducts(new List<IProduct>
            {
                milk
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            var resultMilk = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Milk));
            Assert.IsNotNull(resultMilk);
            Assert.AreEqual(2, resultMilk.Quantity);
            Assert.AreEqual(2.5m, resultMilk.Price);
            Assert.AreEqual(0,resultMilk.Discount);
        }

        [Test]
        public void Method_Should_Return_Products_Without_Change_If_Butter_Qty_Less_Than_2()
        {
            //Arrange
            var milk = new Product(ProductName.Milk, 2, 2.5m, 0);
            var butter = new Product(ProductName.Butter, 1, 5.5m, 2);
            var bread = new Product(ProductName.Bread, 2, 3, 0);

            ButterOffer objectUnderTest = new ButterOffer();

            //Act
            var result = objectUnderTest.GetOfferDiscountAppliedProducts(new List<IProduct>
            {
                milk, butter, bread
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            var resultMilk = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Milk));
            Assert.IsNotNull(resultMilk);
            Assert.AreEqual(2, resultMilk.Quantity);
            Assert.AreEqual(2.5m, resultMilk.Price);
            Assert.AreEqual(0, resultMilk.Discount);

            var resultButter = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Butter));
            Assert.IsNotNull(resultButter);
            Assert.AreEqual(1, resultButter.Quantity);
            Assert.AreEqual(5.5m, resultButter.Price);
            Assert.AreEqual(2, resultButter.Discount);

            var resultBread = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Bread));
            Assert.IsNotNull(resultBread);
            Assert.AreEqual(2, resultBread.Quantity);
            Assert.AreEqual(3, resultBread.Price);
            Assert.AreEqual(0, resultBread.Discount);
        }

        [TestCase(2,1,3, 1.5)]
        [TestCase(3, 1, 3, 1.5)]
        [TestCase(4, 1, 3, 1.5)]
        [TestCase(4, 2, 3, 3)]
        [TestCase(5, 2, 3, 3)]
        public void Method_Should_Return_Products_With_Correct_Discount_For_Bread(
            int butterQuantity,
            int breadQuantity,
            decimal breadPrice,
            decimal expectedBreadDiscount)
        {
            //Arrange
            var milk = new Product(ProductName.Milk, 2, 2.5m, 0);
            var butter = new Product(ProductName.Butter, butterQuantity, 5.5m, 0);
            var bread = new Product(ProductName.Bread, breadQuantity, breadPrice, 0);

            ButterOffer objectUnderTest = new ButterOffer();

            //Act
            var result = objectUnderTest.GetOfferDiscountAppliedProducts(new List<IProduct>
            {
                milk, butter, bread
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            var resultMilk = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Milk));
            Assert.IsNotNull(resultMilk);
            Assert.AreEqual(2, resultMilk.Quantity);
            Assert.AreEqual(2.5m, resultMilk.Price);
            Assert.AreEqual(0, resultMilk.Discount);

            var resultButter = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Butter));
            Assert.IsNotNull(resultButter);
            Assert.AreEqual(butterQuantity, resultButter.Quantity);
            Assert.AreEqual(5.5m, resultButter.Price);
            Assert.AreEqual(0, resultButter.Discount);

            var resultBread = result.FirstOrDefault(_ => _.Name.Equals(ProductName.Bread));
            Assert.IsNotNull(resultBread);
            Assert.AreEqual(breadQuantity, resultBread.Quantity);
            Assert.AreEqual(breadPrice, resultBread.Price);
            Assert.AreEqual(expectedBreadDiscount, resultBread.Discount);
        }


    }
}