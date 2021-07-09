using System.Collections.Generic;
using System.Linq;
using DT.PriceDiscount.Core.Contracts;
using NUnit.Framework;

namespace DT.PriceDiscount.Core.Impl.Tests
{
    public class MilkOfferTests
    {
        [Test]
        public void Method_Should_Return_Products_Without_Change_If_Does_Not_Contain_Milk()
        {
            //Arrange
            var bread = new Product(ProductName.Bread, 3, 5.5m, 2);

            MilkOffer objectUnderTest = new MilkOffer();

            //Act
            var result = objectUnderTest.GetOfferDiscountAppliedProducts(new List<IProduct>
            {
                 bread
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

            var resultBread = result.FirstOrDefault(_ => _.Name== ProductName.Bread);
            Assert.IsNotNull(resultBread);
            Assert.AreEqual(3, resultBread.Quantity);
            Assert.AreEqual(5.5m, resultBread.Price);
            Assert.AreEqual(2, resultBread.Discount);
        }

        [Test]
        public void Method_Should_Return_Products_Without_Change_If_Milk_Quantity_Is_Less_Than_4()
        {
            //Arrange
            var bread = new Product(ProductName.Bread, 3, 5.5m, 2);
            var milk = new Product(ProductName.Milk, 2, 1.15m, 0);

            MilkOffer objectUnderTest = new MilkOffer();

            //Act
            var result = objectUnderTest.GetOfferDiscountAppliedProducts(new List<IProduct>
            {
                bread, milk
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var resultBread = result.FirstOrDefault(_ => _.Name== ProductName.Bread);
            Assert.IsNotNull(resultBread);
            Assert.AreEqual(3, resultBread.Quantity);
            Assert.AreEqual(5.5m, resultBread.Price);
            Assert.AreEqual(2, resultBread.Discount);

            var resultMilk = result.FirstOrDefault(_ => _.Name== ProductName.Milk);
            Assert.IsNotNull(resultMilk);
            Assert.AreEqual(2, resultMilk.Quantity);
            Assert.AreEqual(1.15m, resultMilk.Price);
            Assert.AreEqual(0, resultMilk.Discount);
        }

        [TestCase(6, 1.15, 2.3)]
        [TestCase(8, 1.15, 2.3)]
        [TestCase(9, 1.15, 3.45)]
        public void Method_Should_Return_Products_With_Milk_Discount_Correctly_Set(
            int milkQuantity, decimal milkPrice, decimal expectedMilkDiscount)
        {
            //Arrange
            var bread = new Product(ProductName.Bread, 3, 5.5m, 2);
            var milk = new Product(ProductName.Milk, milkQuantity, milkPrice, 0);

            MilkOffer objectUnderTest = new MilkOffer();

            //Act
            var result = objectUnderTest.GetOfferDiscountAppliedProducts(new List<IProduct>
            {
                bread, milk
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

            var resultBread = result.FirstOrDefault(_ => _.Name == ProductName.Bread);
            Assert.IsNotNull(resultBread);
            Assert.AreEqual(3, resultBread.Quantity);
            Assert.AreEqual(5.5m, resultBread.Price);
            Assert.AreEqual(2, resultBread.Discount);

            var resultMilk = result.FirstOrDefault(_ => _.Name == ProductName.Milk);
            Assert.IsNotNull(resultMilk);
            Assert.AreEqual(milkQuantity, resultMilk.Quantity);
            Assert.AreEqual(milkPrice, resultMilk.Price);
            Assert.AreEqual(expectedMilkDiscount, resultMilk.Discount);
        }
    }
}