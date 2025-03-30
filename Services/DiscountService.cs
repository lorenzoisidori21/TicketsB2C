namespace TicketsB2C.Services;

public class DiscountService : IDiscountService
{
    private readonly List<IDiscountStrategy> _strategies;

    public DiscountService(IEnumerable<IDiscountStrategy> strategies)
    {
        _strategies = strategies.ToList();
    }

    public decimal CalculateDiscount(int transportType, int quantity)
    {
        var strategy = GetBestDiscountStrategy(transportType, quantity);
        return strategy.CalculateDiscountPercentage(transportType, quantity);
    }

    private IDiscountStrategy GetBestDiscountStrategy(int transportType, int quantity)
    {
        // Example rule: Return the highest discount strategy
        return _strategies.OrderByDescending(s => s.CalculateDiscountPercentage(transportType, quantity)).FirstOrDefault()?? new NoDiscountStrategy();
    }
}