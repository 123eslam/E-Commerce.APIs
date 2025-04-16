namespace Domain.Entities.Basket
{
    public class CustomerBasket //Cart ==> Id , Items : Products
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
    }
}
