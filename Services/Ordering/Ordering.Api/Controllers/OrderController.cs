using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oredering.Application.Features.Orders.Command.CheckoutOrder;
using Oredering.Application.Features.Orders.Command.DeleteOrder;
using Oredering.Application.Features.Orders.Command.UpdateOrder;
using Oredering.Application.Features.Orders.Queries.GetOrderList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class OrderController : ControllerBase
    {
        #region Constructor
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Get All Order
        [HttpGet("{userName}", Name = "GetOrders")]
        [ProducesResponseType(typeof(IEnumerable<OrdersVm>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrderByUserName(string userName)
        {
            // request
            var query = new GetOrderListQuery(userName);
            var Orders = await _mediator.Send(query);

            return Ok(Orders);
        }
        #endregion

        #region Checkout Order
        //testing
        [HttpPost(Name = "CheckOutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCammand command)
        {
            var resualt = await _mediator.Send(command);
            return Ok(resualt);
        }
        #endregion

        #region Update Order
        [HttpPut(Name ="UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion

        #region delete order
        [HttpDelete("{id}",Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            var command = new DeleteOrderCommand {Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
        #endregion
    }
}
