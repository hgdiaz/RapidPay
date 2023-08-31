﻿using AutoMapper;
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

            public Handler(IServiceManager serviceManager, IMapper mapper)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
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
                    //logging can be used here (like Serilog) to save exception information

                }

                return result;
            }
        }
    }
}
