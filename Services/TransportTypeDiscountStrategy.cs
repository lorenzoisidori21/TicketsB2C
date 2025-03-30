namespace TicketsB2C.Services;

public class TransportTypeDiscountStrategy : IDiscountStrategy
{
    private readonly Dictionary<int, int> _configs;
    public TransportTypeDiscountStrategy(Dictionary<int, int> configs)
    {
        _configs = configs;
    }

    public decimal CalculateDiscountPercentage(int transportType, int quantity)
    {
        foreach (var (type, discount) in _configs)
            if (transportType == type)
                return discount;

        return 0;
    }
}