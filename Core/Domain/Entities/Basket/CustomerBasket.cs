namespace Domain.Entities.Basket
{
    public class CustomerBasket //Cart ==> Id , Items : Products
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClintSecret { get; set; } 
        public decimal? ShippngPrice { get; set; }
        public int? DeliveryMethodId { get; set; }
    }
}
