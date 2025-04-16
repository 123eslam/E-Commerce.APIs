using Domain.Entities.Basket;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //Get Basket
        Task<CustomerBasket?> GetBasketAsync(string Id);
        //Delete Basket
        Task<bool> DeleteBasketAsync(string Id);
        //Create or Update Basket
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null);
    }
}
