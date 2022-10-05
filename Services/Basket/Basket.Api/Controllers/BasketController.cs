using Basket.Api.Entities;
using Basket.Api.gRPCServices;
using Basket.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountgRPCService _discountgRPCService;


        public BasketController(IBasketRepository basketRepository, DiscountgRPCService discountgRPCService = null)
        {
            _basketRepository = basketRepository;
            _discountgRPCService = discountgRPCService;
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
        {

            return Ok(await _basketRepository.GetUserBasket(username) ?? new ShoppingCart(username));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            // Get data from discount.gRPC and calculate final price of product
            foreach (var item in basket.Items)
            {
               var coupon = await _discountgRPCService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _basketRepository.UpdateUserBasket(basket));
        }

        [HttpDelete("{username}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _basketRepository.DeleteBasket(username);
            return Ok();
        }

    }
}
