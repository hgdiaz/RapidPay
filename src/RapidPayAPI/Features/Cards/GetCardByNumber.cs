using AutoMapper;
using MediatR;
using RapidPayAPI.Features.Cards.Exceptions;
using RapidPayAPI.ServiceManager;

namespace RapidPayAPI.Features.Cards
{
    public class GetCardByNumber
    {
        public class GetCardByNumberQuery : IRequest<CardResult>
        {
            public string Number { get; set; }
        }

        public class CardResult
        {
            public int Id { get; set; }
            public string Number { get; set; }
            public string CardHolderName { get; set; }
            public int ExpirationMonth { get; set; }
            public int ExpirationtYear { get; set; }
            public string CVC { get; set; }
            public double Balance { get; set; }
        }

        public class Handler : IRequestHandler<GetCardByNumberQuery, CardResult>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
            }

            public async Task<CardResult> Handle(GetCardByNumberQuery request, CancellationToken cancellationToken)
            {
                CardResult result = new CardResult();
                try
                {
                    var card = await _serviceManager.Card.GetByNumber(request.Number);
                    if (card == null)
                    { throw new NoCardException(request.Number); }

                    result = _mapper.Map<CardResult>(card);
                }
                catch (Exception)
                {
                    //logging can be used here (like Serilog) to save exception information
                    throw;
                }

                return result;
            }
        }
    }
}
