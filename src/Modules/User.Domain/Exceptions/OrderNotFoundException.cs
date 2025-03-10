namespace User.Domain.Exceptions
{
    public class OrderNotFoundException(Guid orderId) : Exception($"Order with OrderId '{orderId}' not found.") { }
}
