using AutoMapper;
using RapidPayAPI.Domain;

namespace RapidPayAPI.Features.Cards
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Card, GetAllCards.CardResult>();
            CreateMap<Card, GetCardByNumber.CardResult>();
            CreateMap<AddCard.AddCardCommand, Card>();            

        }
    }
}
