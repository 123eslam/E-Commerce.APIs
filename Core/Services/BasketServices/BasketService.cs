using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Exceptions;
using Services.Abstraction;
using Shared.BasketDtos;

namespace Services.BasketServices
{
    public class BasketService(IBasketRepository _basketRepository ,IMapper _mapper) : IBasketService
    {
        public async Task<bool> DeleteBasketAsync(string basketId) =>
            await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<BasketDto?> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return basket is null ?
                throw new BasketNotFoundException(basketId) :
                _mapper.Map<BasketDto>(basket);
        }

        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basket)
        {
            var customerBasket = await _basketRepository.UpdateBasketAsync(_mapper.Map<CustomerBasket>(basket));
            return customerBasket is null ?
                throw new Exception("Can not update the basket now !!") :
                _mapper.Map<BasketDto>(customerBasket);
        }
    }
}
