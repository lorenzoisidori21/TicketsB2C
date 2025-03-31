namespace TicketsB2C.Services;

public interface IDiscountService
{
    decimal CalculateDiscount(int transportType, int quantity);
}