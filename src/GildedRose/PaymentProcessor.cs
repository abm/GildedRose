namespace GildedRose
{
    public class PaymentProcessor : IPaymentProcessor
    {
        public PaymentResult PayFor(Order order)
        {
            // We're just going to simulate some payments results.
            // Really, we'd just be using a payment processor.
            if (order.Customer.Name.Contains("Bankrupt"))
                return PaymentResult.OutOfFunds;
            else if (order.Customer.Name.Contains("New"))
                return PaymentResult.NoPaymentMethod;
            else
                return PaymentResult.Successful;
        }
    }
}