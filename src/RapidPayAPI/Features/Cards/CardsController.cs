using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayAPI.Constants;
using RapidPayAPI.Features.Cards;
using RapidPayAPI.Features.Cards.Exceptions;
using RapidPayAPI.Features.Payments;

namespace RapidPayAPI.Features.CardsController
{
    [Route("api/[controller]")]
    [ApiController]    
    public class CardsController : ControllerBase
    {
        private readonly IMediator _mediator;


        public CardsController(IMediator mediator)
        {
            _mediator = mediator;            
        }

        /// <summary>
        /// Used for testing purposes
        /// Uncomment the lines below to get all cards (only for admin users)
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        //[HttpGet("GetCards")]
        //[Authorize(Roles = UserRoles.Admin)]
        //public async Task<ActionResult<IEnumerable<GetAllCards.CardResult>>> GetCards()
        //{
        //    var result = await _mediator.Send(new GetAllCards.GetCardsQuery());

        //    return Ok(result);
        //}

        [HttpGet("GetCardBalance/{number}")]
        [Authorize]
        public async Task<ActionResult<double>> GetCardBalance(string number)
        {
            try
            {
                GetCardByNumber.GetCardByNumberQuery query =
                        new GetCardByNumber.GetCardByNumberQuery() { Number = number };
                var result = await _mediator.Send(query);

                return Ok(result.Balance);
            }
            catch (NoCardException ex)
            {
                return NotFound(new
                {
                    ex.Message                     
                });
            }
        }

        [HttpPost("AddCard")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> AddCard(AddCard.AddCardCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Created("AddCard", result);
            }
            catch (CardExistsException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
            catch (CardDataNotValidException ex)
            {
                return BadRequest(new
                {
                    ex.Message
                });
            }
        }

        [HttpPost("Pay")]
        [Authorize]
        public async Task<ActionResult> Pay(PayCard.PayCardCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return Ok(result);
            }
            catch (InvalidCardException ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
            //used only with case 1 (when must verify card's balance)
            //catch (InsuficientBalanceException ex)
            //{
            //    return Conflict(new
            //    {
            //        ex.Message
            //    });
            //}
            catch (Exception ex)
            {
                return Conflict(new
                {
                    ex.Message
                });
            }
        }

    }
}
