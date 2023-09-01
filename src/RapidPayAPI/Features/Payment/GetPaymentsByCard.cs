using AutoMapper;
using MediatR;
using RapidPayAPI.Domain;
using RapidPayAPI.Features.Cards;
using RapidPayAPI.Features.Cards.Exceptions;
using RapidPayAPI.ServiceManager;

namespace RapidPayAPI.Features.Payments
{
    public class GetPaymentsByCard
    {
        public class GetPaymentsByCardQuery : IRequest<IEnumerable<PaymentResult>>
        {
            public string CardNumber { get; set; }
        }

        public class PaymentResult
        {
            public int Id { get; set; }
            public string CardNumber { get; set; }
            public DateTime TransactionDate { get; set; }
            public double Fee { get; set; }
            public double Amount { get; set; }

            public double TotalAmount { get => Amount + Fee; }
        }

        public class Handler : IRequestHandler<GetPaymentsByCardQuery, IEnumerable<PaymentResult>>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;
            private readonly ILogger<GetPaymentsByCard> _logger;

            public Handler(IServiceManager serviceManager, IMapper mapper, ILogger<GetPaymentsByCard> logger)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<IEnumerable<PaymentResult>> Handle(GetPaymentsByCardQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<PaymentResult> result = new List<PaymentResult>();
                try
                {
                    var payments = await _serviceManager.Payment.GetPaymentsByCardAsync(request.CardNumber);
                    result = _mapper.Map<IEnumerable<PaymentResult>>(payments);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("GetPaymentsByCard: " + ex.Message);
                    throw;
                }

                return result;
            }
        }
    }
}
