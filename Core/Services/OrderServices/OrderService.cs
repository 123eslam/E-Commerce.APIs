using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Basket;
using Domain.Entities.Order_Entitie;
using Domain.Entities.Products;
using Domain.Exceptions;
using Services.Abstraction;
using Shared.OrderDtos;

namespace Services.OrderServices
{
    internal class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            //Address ==> Address [request.AddressDto] 
            var address = _mapper.Map<Address>(request.AddressDto);
            //OrderItems ==> Basket [reqest.BasketId] ==> BasketItems ==> OrderItems
            var basket = await _basketRepository.GetBasketAsync(request.BasketId) 
                ?? throw new BasketNotFoundException(request.BasketId);
            var orderItems = new List<OrderItem>();
            foreach(var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }
            //DeliveryMethod ==> DeliveryMethods [request.DeliveryMethodId]
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>()
                .GetByIdAsync(request.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(request.DeliveryMethodId);
            //SubTotal ==> OrderItems.Sum(x => x.Price * x.Quantity)
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);
            //Create Order
            var order = new Order(userEmail, address, orderItems, deliveryMethod, subTotal);
            //Save in Database
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            //Map ,return OrderResult
            return _mapper.Map<OrderResult>(order);
        }
        private OrderItem CreateOrderItem(BasketItem item, Product product) =>
            new OrderItem
                (
                    new ProductInOrderItem(product.Id, product.Name, product.PictureUrl),
                    item.Quantity,
                    product.Price
                );

        public Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
