using System.Collections;
using TicketsB2C.Services;

namespace TicketsB2cUnitTests
{
    public class DiscountServiceTests
    {
        [Fact]
        public void DiscountServiceWithoutStrategyShouldReturnNoDiscount()
        {
            IEnumerable<IDiscountStrategy> noStrategies = new List<IDiscountStrategy>();
            var discountService = new DiscountService(noStrategies);
            var percentageOff = discountService.CalculateDiscount(1, 1);
            Assert.Equal(0, percentageOff);
        }
        [Fact]
        public void DiscountServiceWithQuantityDiscountStrategyShouldWork()
        {
            IEnumerable<IDiscountStrategy> noStrategies = new List<IDiscountStrategy>()
            {
                new QuantityDiscountStrategy(new Dictionary<int, int>(){{5,5}, {10,15}})
            };
            var discountService = new DiscountService(noStrategies);
            var percentageOff = discountService.CalculateDiscount(1, 1);
            Assert.Equal(0, percentageOff);
            percentageOff = discountService.CalculateDiscount(1, 5);
            Assert.Equal(5, percentageOff);
            percentageOff = discountService.CalculateDiscount(1, 10);
            Assert.Equal(15, percentageOff);
        }
    }
}
