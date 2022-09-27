using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Discount.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountApiController : ControllerBase
    {

        private readonly IDiscountRepository _discountrepository;

        public DiscountApiController(IDiscountRepository discountrepository)
        {
            _discountrepository = discountrepository;
        }

        [HttpGet("{productname}",Name ="GetDiscout1")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productname)
        {
            var coupon = await _discountrepository.GetDiscount(productname);
            return Ok(coupon);
        }

        [HttpPut("UpdateDicount")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            var resualt = await _discountrepository.UpdateDiscount(coupon);
            //return CreatedAtRoute("GetDiscout1",new { productname = coupon.ProductName},coupon);
            return Ok(resualt);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            var resualt = await _discountrepository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscout1", new { productname = coupon.ProductName }, coupon);
        }

        [HttpDelete("{productname}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscount(string productname)
        {
            var resualt = await _discountrepository.DeleteDiscount(productname);
            return Ok(resualt);
        }
    }
}
