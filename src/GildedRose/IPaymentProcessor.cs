namespace GildedRose
{
    public interface IPaymentProcessor
    {
        PaymentResult PayFor(Order order);
    }
}