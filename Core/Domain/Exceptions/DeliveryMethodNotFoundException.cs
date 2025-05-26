namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException : NotFoundException
    {
        public DeliveryMethodNotFoundException(int id) : base($"Delivery method with id: {id} was not found.")
        {
        }
        public DeliveryMethodNotFoundException() : base("No Delivery method with selected.")
        {
            
        }
    }
}
