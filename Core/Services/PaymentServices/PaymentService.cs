using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Order_Entitie;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Shared.BasketDtos;
using Stripe;
using Product = Domain.Entities.Products.Product;
namespace Services.PaymentServices
{
    internal class PaymentService(IBasketRepository _basketRepository,
        IUnitOfWork _unitOfWork, IMapper _mapper, IConfiguration _configuration) : IPaymentService
    {
        //1] Set up stripe api key [secretKey]
        //2] Get the basket from the database
        //3] Basket.Item.Price = Product.Price [Update basket item price , get product.Price from data base]
        //4] Get deliveryMethod and shippingPrice
        //5] Retrive deliveryMethod from database and assign price of basket [shippingprice] = deliveryMethod.ShippingPrice
        //6] Total = subtotal + shippingPrice [(item.Price) * (item.Quantity) + deliveryMethod.ShippingPrice]
        //7] Create or Update PaymentIntent with sstripe
        //8] Save changes to the basket
        //9] Mapping basket ==> basketDto & return
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripeSettings")["SecretKey"];
            var basket = await _basketRepository.GetBasketAsync(basketId)
                ?? throw new BasketNotFoundException(basketId);
            foreach(var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
            }
            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundException();
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippngPrice = deliveryMethod.Price;

            var amount = (long)(basket.Items.Sum(i => i.Price * i.Quantity) + basket.ShippngPrice) * 100;
            var service = new PaymentIntentService();
            //if he want to create or update
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //Create
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await service.CreateAsync(createOptions);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.clientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                //Update
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }
            await _basketRepository.UpdateBasketAsync(basket);
            return _mapper.Map<BasketDto>(basket);
        }
    }
}
