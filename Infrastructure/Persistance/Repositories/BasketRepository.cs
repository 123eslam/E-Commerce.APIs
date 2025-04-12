using Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistance.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string Id) =>
            await _database.KeyDeleteAsync(Id);
            

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var value = await _database.StringGetAsync(Id); //JSON File
            if(value.IsNullOrEmpty) return null; 
            return JsonSerializer.Deserialize<CustomerBasket>(value!); //Deserialize JSON to CustomerBasket
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            //add ,Update ==>Serialize to JSON
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(15));
            return isCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null;
        }
    }
}
