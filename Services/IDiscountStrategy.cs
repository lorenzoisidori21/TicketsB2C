namespace TicketsB2C.Services;

public interface IDiscountStrategy
{
    decimal CalculateDiscountPercentage(int transportType, int quantity);
}