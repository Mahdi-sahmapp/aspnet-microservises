using Basket.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.Api.Repository
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetUserBasket(string username);
        Task<ShoppingCart> UpdateUserBasket(ShoppingCart basket);
        Task DeleteBasket(string username);
    }
}
