using System.Linq;
using NUnit.Framework;

namespace DT.PriceDiscount.Core.Impl.Tests
{
    public class OfferFactoryTests
    {
        [Test]
        public void Method_Should_Return_2_Offers()
        {
            //Arrange
            OfferFactory objectUnderTest = new OfferFactory();

            //Act
            var result = objectUnderTest.GetOffers();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count, "Should have been 2 offers");
            Assert.IsTrue(result.Any(_=> typeof(MilkOffer) == _.GetType()));
            Assert.IsTrue(result.Any(_ => typeof(ButterOffer) == _.GetType()));

        }

        [Test]
        public void Method_Should_Always_Return_Same_Instances()
        {
            //Arrange
            OfferFactory objectUnderTest = new OfferFactory();

            //Acts & Asserts
            var result = objectUnderTest.GetOffers();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count, "Should have been 2 offers");
            
            var milkOffer1 = result.FirstOrDefault(_ => typeof(MilkOffer) == _.GetType());
            var butterOffer1 = result.FirstOrDefault(_ => typeof(ButterOffer) == _.GetType());
            Assert.IsNotNull(milkOffer1);
            Assert.IsNotNull(butterOffer1);

            //Second Call
            result = objectUnderTest.GetOffers();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count, "Should have been 2 offers");

            var milkOffer2 = result.FirstOrDefault(_ => typeof(MilkOffer) == _.GetType());
            var butterOffer2 = result.FirstOrDefault(_ => typeof(ButterOffer) == _.GetType());
            Assert.IsNotNull(milkOffer2);
            Assert.IsNotNull(butterOffer2);

            //This proves they are the same instances
            Assert.AreEqual(milkOffer1, milkOffer2);
            Assert.AreEqual(butterOffer1, butterOffer2);

        }
    }
}