using AutoMapper;
using MediatR;
using RapidPayAPI.Domain;
using RapidPayAPI.Features.Cards.Exceptions;
using RapidPayAPI.Features.Payments;
using RapidPayAPI.ServiceManager;
using System.Diagnostics;

namespace RapidPayAPI.Features.Cards
{
    public class PayCard
    {
        //Input
        public class PayCardCommand : IRequest<PayCardResult>
        {
            public string Number { get; set; }
            public string CVC { get; set; }
            public double Amount { get; set; }
        }

        //Output
        public class PayCardResult
        {
            public bool PaymentSuccess { get; set; }
        }

        //Handler
        public class Handler : IRequestHandler<PayCardCommand, PayCardResult>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;
            private readonly IFeeService _feeService; 
            private readonly ILogger<PayCard> _logger;

            public Handler(IServiceManager serviceManager, IMapper mapper, IFeeService feeService, ILogger<PayCard> logger)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
                _feeService = feeService;
                _logger = logger;
            }

            public async Task<PayCardResult> Handle(PayCardCommand request, CancellationToken cancellationToken)
            {
                PayCardResult result = new PayCardResult() { PaymentSuccess = false };

                try
                {
                    //check if there's already a card with the same number
                    var card = await _serviceManager.Card.GetByNumber(request.Number);
                    if (card == null)
                        throw new InvalidCardException();

                    //check the CVC 
                    if (request.CVC != card.CVC)
                        throw new InvalidCardException();

                    //get the fee 
                    var fee = _feeService.GetFee();

                    //
                    //case 1:
                    // in case the card works as a debit card, we must check if the balance 
                    // if enough to make the current pay 
                    //
                    //check the balace
                    //if ((request.Amount + fee) > card.Balance)
                    //    throw new InsuficientBalanceException();
                    //modify the card's balance 
                    //card.Balance -= (request.Amount + fee);

                    //
                    //case 2:
                    // in this case, we just increase the balance and don't check
                    // if there's enough money to make the current pay
                    //
                    //modify the card's balance 
                    card.Balance += (request.Amount + fee);


                    await _serviceManager.Card.UpdateCard(card);
                    //save the payment information
                    Payment payment = new Payment()
                    {
                        Card = card,
                        Fee = fee,
                        Amount = request.Amount
                    };
                    await _serviceManager.Payment.AddPaymentAsync(payment);
                    //commit to DB
                    await _serviceManager.SaveAsync();
                    
                    result.PaymentSuccess = true;
                    
                }
                catch (InvalidCardException ex)
                {
                    _logger.LogInformation("PayCard: " + ex.Message);
                    throw;
                }
                //used only with case 1
                //catch (InsuficientBalanceException ex)
                //{
                //    _logger.LogInformation("PayCard: " + ex.Message);
                //    throw;
                //}
                catch (Exception ex)
                {
                    _logger.LogInformation("PayCard: " + ex.Message);
                    throw;
                }

                //returns the result of the operation
                return result;

            }
        }
    }
}
