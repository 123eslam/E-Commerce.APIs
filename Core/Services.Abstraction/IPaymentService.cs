using Shared.BasketDtos;

namespace Services.Abstraction
{
    public interface IPaymentService
    {
        //Create or update paymentIntent
        public Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
    }
}
