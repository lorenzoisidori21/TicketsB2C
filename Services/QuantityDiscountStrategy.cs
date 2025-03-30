namespace TicketsB2C.Services;

public class QuantityDiscountStrategy : IDiscountStrategy
{
    private readonly Dictionary<int, int> _configs;

    public QuantityDiscountStrategy(Dictionary<int, int> configs)
    {
        _configs = configs.OrderByDescending(kvp => kvp.Key).ToDictionary();
    }

    public decimal CalculateDiscountPercentage(int transportType, int quantity)
    {
        foreach (var (threshold, discount) in _configs)
        {
            if (quantity >= threshold)
            {
                return discount;
            }
        }
        return 0;
    }
}