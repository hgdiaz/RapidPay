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

            public Handler(IServiceManager serviceManager, IMapper mapper, IFeeService feeService)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
                _feeService = feeService;
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

                    //get the fee and check the balace
                    var fee = _feeService.GetFee();
                    if ((request.Amount + fee) > card.Balance)
                        throw new InsuficientBalanceException();

                    //modify the card's balance 
                    card.Balance -= (request.Amount + fee);
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
                catch (InvalidCardException)
                {
                    //logging can be used here (like Serilog) to save exception information
                    throw;
                }
                catch (InsuficientBalanceException)
                {
                    //logging can be used here (like Serilog) to save exception information
                    throw;
                }
                catch (Exception ex)
                {
                    //logging can be used here (like Serilog) to save exception information
                    throw;
                }

                //returns the result of the operation
                return result;

            }
        }
    }
}
