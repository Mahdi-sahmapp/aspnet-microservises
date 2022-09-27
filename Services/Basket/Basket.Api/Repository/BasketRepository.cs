using Basket.Api.Entities;
using System;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Basket.Api.Repository
{
    
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _RedisCash;

        public BasketRepository(IDistributedCache redisCash)
        {
            _RedisCash = redisCash;
        }

        public async Task<ShoppingCart> GetUserBasket(string username)
        {
            var basket = await _RedisCash.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateUserBasket(ShoppingCart basket)
        {
            await _RedisCash.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetUserBasket(basket.UserName);
        }
        public async Task DeleteBasket(string username)
        {
            await _RedisCash.RemoveAsync(username);
        }
    }
}
