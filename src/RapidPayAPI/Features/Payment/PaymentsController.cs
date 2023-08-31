using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayAPI.Constants;
using RapidPayAPI.Features.Payments;
using System.Data;

namespace RapidPayAPI.Features.PaymentsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;


        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Used for testing purposes
        /// Uncomment the lines below to get all payments for a card (only for admin users)
        /// </summary>
        /// <param name="cardnumber"></param>
        /// <returns></returns>
        //[HttpGet("GetPaymentsByCard/{cardnumber}")]
        //[Authorize]
        //public async Task<ActionResult<IEnumerable<GetPaymentsByCard.PaymentResult>>> GetPaymentsByCard(string cardnumber)
        //{

        //    GetPaymentsByCard.GetPaymentsByCardQuery query =
        //            new GetPaymentsByCard.GetPaymentsByCardQuery() { CardNumber = cardnumber };
        //    var result = await _mediator.Send(query);

        //    return Ok(result);

        //}

    }
}
