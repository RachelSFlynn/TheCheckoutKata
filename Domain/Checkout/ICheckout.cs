namespace Domain.Checkout;

public interface ICheckout
{
    void Scan(string item);
    decimal GetTotalPrice();
}
