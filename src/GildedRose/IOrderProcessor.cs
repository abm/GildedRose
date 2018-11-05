namespace GildedRose
{
    public interface IOrderProcessor
    {
        OrderResult Place(Order order);
    }
}