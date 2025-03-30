namespace TicketsB2C.Services;

internal class NoDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscountPercentage(int transportType, int quantity)
    {
        return 0;
    }
}