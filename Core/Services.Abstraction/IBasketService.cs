using Shared.BasketDtos;

namespace Services.Abstraction
{
    public interface IBasketService
    {
        //Get
        Task<BasketDto?> GetBasketAsync(string basketId);
        //Delete
        Task<bool> DeleteBasketAsync(string basketId);
        //Add ,Update
        Task<BasketDto?> UpdateBasketAsync(BasketDto basket);
    }
}
