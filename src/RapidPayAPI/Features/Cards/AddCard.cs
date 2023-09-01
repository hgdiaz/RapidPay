using AutoMapper;
using FluentValidation.Results;
using MediatR;
using RapidPayAPI.Domain;
using RapidPayAPI.Features.Cards.Exceptions;
using RapidPayAPI.Features.Cards.Validators;
using RapidPayAPI.ServiceManager;
using System.Diagnostics;

namespace RapidPayAPI.Features.Cards
{
    public class AddCard
    {
        //Input
        public class AddCardCommand : IRequest<CardResult>
        {
            public string Number { get; set; }
            public string CardHolderName { get; set; }
            public int ExpirationMonth { get; set; }
            public int ExpirationtYear { get; set; }
            public string CVC { get; set; }
            public double Balance { get; set; }
        }

        //Output
        public class CardResult
        {
            public int Id { get; set; }
        }

        //Handler
        public class Handler : IRequestHandler<AddCardCommand, CardResult>
        {
            private readonly IServiceManager _serviceManager;
            private readonly IMapper _mapper;
            private readonly ILogger<AddCard> _logger;

            public Handler(IServiceManager serviceManager, IMapper mapper, ILogger<AddCard> logger)
            {
                _serviceManager = serviceManager;
                _mapper = mapper;
                _logger = logger;
            }   

            public async Task<CardResult> Handle(AddCardCommand request, CancellationToken cancellationToken)
            {
                Card result = new Card();
                try
                {
                    //check if there's already a card with the same number
                    var exists = await _serviceManager.Card.GetByNumber(request.Number);
                    if (exists != null)
                        throw new CardExistsException(request.Number);

                    //validate the card
                    Card card = _mapper.Map<Card>(request);
                    AddCardValidator validator = new AddCardValidator();
                    ValidationResult validation = validator.Validate(card);
                    if (!validation.IsValid)
                    {
                        string message = string.Empty;
                        foreach (var failure in validation.Errors)
                        {
                            message += failure.ErrorMessage + ". ";
                        }
                        throw new CardDataNotValidException(message);
                    }

                    //save the card
                    result = await _serviceManager.Card.AddCard(card);
                    await _serviceManager.SaveAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("AddCard: " + ex.Message);
                    throw;
                }

                //returns the Id of the new record or else cero if not saved
                return new CardResult() { Id = result.Id };

            }
        }
    }
}
