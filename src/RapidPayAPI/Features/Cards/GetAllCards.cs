using AutoMapper;
using MediatR;
using RapidPayAPI.Features.Cards.Exceptions;
using RapidPayAPI.ServiceManager;

namespace RapidPayAPI.Features.Cards
{
    public class GetAllCards
    {
        public class GetCardsQuery : IRequest<IEnumerable<CardResult>>
        {
        }

        public class CardResult
        {
            public int Id { get; set; }
            public string Number { get; set; }
            public string CardHolderName { get; set; }
            public double Balance { get; set; }
        }

        public class Handler : IRequestHandler<GetCardsQuery, IEnumerable<CardResult>>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;
            private readonly ILogger<GetAllCards> _logger;

            public Handler(IServiceManager serviceManager, IMapper mapper, ILogger<GetAllCards> logger)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
                _logger = logger;
            }

            public async Task<IEnumerable<CardResult>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<CardResult> result = new List<CardResult>();
                try
                {
                    var cards = await _serviceManager.Card.GetAllCardsAsync();

                    result = _mapper.Map<IEnumerable<CardResult>>(cards);
                }
                catch (Exception ex)
                { 
                    _logger.LogInformation("GetAllCards: " + ex.Message);
                }

                return result;
            }
        }
    }
}
