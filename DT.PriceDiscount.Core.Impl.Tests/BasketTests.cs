using System.Collections.Generic;
using DT.PriceDiscount.Core.Contracts;
using Moq;
using NUnit.Framework;

namespace DT.PriceDiscount.Core.Impl.Tests
{
    public class BasketTests
    {
        [Test]
        public void DiscountedTotal_Should_Return_0_When_Products_Not_Added()
        {
            //Arrange
            Mock<IOfferFactory> offerFactory = new Mock<IOfferFactory>();

            Basket objectUnderTest = new Basket(offerFactory.Object);

            //Act
            var result = objectUnderTest.GetDiscountedTotal(new List<IProduct>());

            //Assert
            Assert.AreEqual(0, result);
            offerFactory.Verify(_ => _.GetOffers(), Times.Never);
        }

        [Test]
        public void DiscountedTotal_Should_Return_OriginalTotal_When_Factory_Returns_0_Offers()
        {
            //Arrange
            Mock<IOfferFactory> offerFactory = new Mock<IOfferFactory>();
            offerFactory.Setup(_ => _.GetOffers()).Returns(new List<IOffer>());

            Product milk = new Product(ProductName.Milk, 2, 3, 0);
            Product bread = new Product(ProductName.Bread, 1, 1.5m, 0);


            Basket objectUnderTest = new Basket(offerFactory.Object);

            //Act
            var result = objectUnderTest.GetDiscountedTotal(new List<IProduct>
            {
                milk, bread
            });

            //Assert
            Assert.AreEqual(7.5m, result);
            offerFactory.Verify(_ => _.GetOffers(), Times.Once);
        }

        [TestCase(8, 1.15, 1, 1, 2, .8, 9)]
        [TestCase(1, 1.15, 1, 1, 1, .8, 2.95)]
        public void DiscountedTotal_Should_Return_DiscountedTotal_When_Factory_Returns_Offers(
            int milkQty,
            decimal milkPrice,
            int breadQty,
            decimal breadPrice,
            int butterQty,
            decimal butterPrice,
            decimal expectedDiscountedTotal)
        {
            //Arrange
            Product milk = new Product(ProductName.Milk, milkQty, milkPrice, 0);
            Product bread = new Product(ProductName.Bread, breadQty, breadPrice, 0);
            Product butter = new Product(ProductName.Butter, butterQty, butterPrice, 0);

            Mock<IOfferFactory> offerFactory = new Mock<IOfferFactory>();
            offerFactory.Setup(_ => _.GetOffers()).Returns(new List<IOffer>
            {
                new MilkOffer(),
                new ButterOffer()
            });

            Basket objectUnderTest = new Basket(offerFactory.Object);

            //Act
            var result = objectUnderTest.GetDiscountedTotal(new List<IProduct>
            {
                milk,bread,butter
            });

            //Assert
            Assert.AreEqual(expectedDiscountedTotal, result);
            offerFactory.Verify(_ => _.GetOffers(), Times.Once);
        }

    }
}