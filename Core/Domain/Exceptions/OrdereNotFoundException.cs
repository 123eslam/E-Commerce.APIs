namespace Domain.Exceptions
{
    public sealed class OrdereNotFoundException : NotFoundException
    {
        public OrdereNotFoundException(Guid id) : base($"Order with id {id} not found.")
        {
        }
    }
}
